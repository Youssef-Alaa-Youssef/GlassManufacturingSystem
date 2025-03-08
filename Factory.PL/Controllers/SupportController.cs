using Factory.BLL.InterFaces;
using Factory.DAL.Models.Auth;
using Factory.DAL.Models.Support;
using Factory.DAL.ViewModels.Support;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Factory.Controllers
{
    [Authorize]
    public class SupportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SupportController> _logger;

        public SupportController(IUnitOfWork unitOfWork, ILogger<SupportController> logger, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        #region Dashboard & Overview

        public async Task<IActionResult> Index()
        {
            try
            {
                var tickets = await _unitOfWork.GetRepository<SupportTicket>().GetAllAsync();
                var faqs = await _unitOfWork.GetRepository<FAQS>().GetAllAsync();

                var viewModel = new SupportManagementViewModel
                {
                    Tickets = tickets,
                    FAQs = faqs
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving support dashboard data");
                TempData["Error"] = "An error occurred while loading the support dashboard.";
                return View(new SupportManagementViewModel());
            }
        }

        #endregion

        #region Ticket Management

        public async Task<IActionResult> Tickets()
        {
            try
            {
                var tickets = await _unitOfWork.GetRepository<SupportTicket>()
                    .GetAllAsync(includeProperties: "Responses");

                return View(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tickets");
                TempData["Error"] = "An error occurred while retrieving tickets.";
                return View(Enumerable.Empty<SupportTicket>());
            }
        }

        public IActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket(SupportTicketViewModel ticketViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ticketViewModel);
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var ticket = new SupportTicket
                {
                    Title = ticketViewModel.Title,
                    Description = ticketViewModel.Description,
                    CustomerName = ticketViewModel.CustomerName,
                    CustomerEmail = ticketViewModel.CustomerEmail,
                    Priority = ticketViewModel.Priority,
                    Type = ticketViewModel.Type,
                    CreatedAt = DateTime.UtcNow,
                    Status = "Open",
                    AssignedToUserId = userId ?? string.Empty
                };

                await _unitOfWork.GetRepository<SupportTicket>().AddAsync(ticket);

                _logger.LogInformation("Ticket created successfully. Ticket ID: {TicketId}", ticket.Id);
                TempData["Success"] = "Ticket created successfully!";
                return RedirectToAction(nameof(Tickets));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ticket");
                TempData["Error"] = "An error occurred while creating the ticket.";
                return View(ticketViewModel);
            }
        }

        #endregion

        #region Ticket Chat & Responses

        public async Task<IActionResult> Chat(int id)
        {
            try
            {
                var ticket = await _unitOfWork.GetRepository<SupportTicket>().GetByIdAsync(id);
                if (ticket == null)
                {
                    _logger.LogWarning("Ticket not found. ID: {TicketId}", id);
                    return NotFound();
                }

                var responses = await _unitOfWork.GetRepository<SupportResponse>()
                    .GetAllAsync(r => r.SupportTicketId == id,
                                includeProperties: "RespondedByUser");

                var viewModel = new SupportChatViewModel
                {
                    Ticket = ticket,
                    Responses = responses.OrderBy(r => r.CreatedAt).ToList(),
                    NewResponse = new SupportResponseViewModel
                    {
                        SupportTicketId = id
                    }
                };

                // Add current user ID to ViewBag for highlighting own responses
                ViewBag.CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving chat for ticket. ID: {TicketId}", id);
                TempData["Error"] = "An error occurred while retrieving the ticket conversation.";
                return RedirectToAction(nameof(Tickets));
            }
        }

        public async Task<IActionResult> GetResponses(int id)
        {
            var responses = await _unitOfWork.GetRepository<SupportResponse>()
                .GetAllAsync(r => r.SupportTicketId == id, includeProperties: "RespondedByUser");

            return PartialView("_ResponsesPartial", responses);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResponse(SupportResponseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid model state." });
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { success = false, message = "User not authenticated." });
                }

                var ticket = await _unitOfWork.GetRepository<SupportTicket>()
                    .GetByIdAsync(model.SupportTicketId);

                if (ticket == null)
                {
                    return NotFound(new { success = false, message = "Ticket not found." });
                }

                var response = new SupportResponse
                {
                    ResponseText = model.ResponseText,
                    CreatedAt = DateTime.UtcNow,
                    SupportTicketId = model.SupportTicketId,
                    RespondedByUserId = userId
                };

                await _unitOfWork.GetRepository<SupportResponse>().AddAsync(response);

                if (ticket.Status == "Open")
                {
                    ticket.Status = "InProgress";
                    await _unitOfWork.GetRepository<SupportTicket>().UpdateAsync(ticket);
                }

                _logger.LogInformation("Response added to ticket. Ticket ID: {TicketId}, Response ID: {ResponseId}",
                    model.SupportTicketId, response.Id);

                return Json(new { success = true, message = "Response submitted successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding response to ticket. ID: {TicketId}", model.SupportTicketId);
                return StatusCode(500, new { success = false, message = "An error occurred while submitting your response." });
            }
        }
        #endregion

        #region FAQ Management

        public async Task<IActionResult> FAQ()
        {
            try
            {
                var faqs = await _unitOfWork.GetRepository<FAQS>().GetAllAsync(includeProperties: "User");
                return View(faqs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving FAQs");
                TempData["Error"] = "An error occurred while retrieving FAQs.";
                return View(Enumerable.Empty<FAQS>());
            }
        }

        public IActionResult CreateFAQ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFAQ(FAQSViewModel faqViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(faqViewModel);
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var faq = new FAQS
                {
                    Question = faqViewModel.Question,
                    Answer = faqViewModel.Answer,
                    Category = faqViewModel.Category,
                    CreatedAt = DateTime.UtcNow,
                    UserId = userId ?? string.Empty
                };

                await _unitOfWork.GetRepository<FAQS>().AddAsync(faq);

                _logger.LogInformation("FAQ created successfully. FAQ ID: {FaqId}", faq.Id);
                TempData["Success"] = "FAQ created successfully!";
                return RedirectToAction(nameof(FAQ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating FAQ");
                TempData["Error"] = "An error occurred while creating the FAQ.";
                return View(faqViewModel);
            }
        }

        public async Task<IActionResult> EditFAQ(int id)
        {
            try
            {
                var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
                if (faq == null)
                {
                    _logger.LogWarning("FAQ not found. ID: {FaqId}", id);
                    return NotFound();
                }

                var viewModel = new FAQSViewModel
                {
                    Id = faq.Id,
                    Question = faq.Question,
                    Answer = faq.Answer,
                    Category = faq.Category
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving FAQ for editing. ID: {FaqId}", id);
                TempData["Error"] = "An error occurred while retrieving the FAQ.";
                return RedirectToAction(nameof(FAQ));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFAQ(int id, FAQSViewModel faqViewModel)
        {
            if (id != faqViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(faqViewModel);
            }

            try
            {
                var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
                if (faq == null)
                {
                    _logger.LogWarning("FAQ not found for update. ID: {FaqId}", id);
                    return NotFound();
                }

                faq.Question = faqViewModel.Question;
                faq.Answer = faqViewModel.Answer;
                faq.Category = faqViewModel.Category;
                faq.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.GetRepository<FAQS>().UpdateAsync(faq);

                _logger.LogInformation("FAQ updated successfully. FAQ ID: {FaqId}", faq.Id);
                TempData["Success"] = "FAQ updated successfully!";
                return RedirectToAction(nameof(FAQ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating FAQ. ID: {FaqId}", id);
                TempData["Error"] = "An error occurred while updating the FAQ.";
                return View(faqViewModel);
            }
        }

        [Authorize(Policy = "CustomerSupport_Delete")]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            try
            {
                var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
                if (faq == null)
                {
                    _logger.LogWarning("FAQ not found for deletion. ID: {FaqId}", id);
                    return NotFound();
                }

                return View(faq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving FAQ for deletion. ID: {FaqId}", id);
                TempData["Error"] = "An error occurred while retrieving the FAQ.";
                return RedirectToAction(nameof(FAQ));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CustomerSupport_Delete")]
        public async Task<IActionResult> DeleteFAQConfirmed(int id)
        {
            try
            {
                var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
                if (faq == null)
                {
                    _logger.LogWarning("FAQ not found for deletion confirmation. ID: {FaqId}", id);
                    TempData["Error"] = "FAQ not found.";
                    return RedirectToAction(nameof(FAQ));
                }

                await _unitOfWork.GetRepository<FAQS>().RemoveAsync(faq);

                _logger.LogInformation("FAQ deleted successfully. FAQ ID: {FaqId}", id);
                TempData["Success"] = "FAQ deleted successfully!";
                return RedirectToAction(nameof(FAQ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting FAQ. ID: {FaqId}", id);
                TempData["Error"] = "An error occurred while deleting the FAQ.";
                return RedirectToAction(nameof(FAQ));
            }
        }

        #endregion
    }
}