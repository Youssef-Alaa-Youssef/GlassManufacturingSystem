using Microsoft.AspNetCore.Mvc;
using Factory.DAL.Models.Support;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Factory.BLL.InterFaces;
using Factory.DAL.ViewModels.Support;

namespace Factory.Controllers
{
    public class SupportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Index: Management dashboard for support tickets and FAQs
        [Authorize]
        public async Task<IActionResult> Index()
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

        // Tickets: List all support tickets
        [Authorize]
        public async Task<IActionResult> Tickets()
        {
            var tickets = await _unitOfWork.GetRepository<SupportTicket>().GetAllAsync();
            return View(tickets);
        }

        // Chat: List all responses for a specific ticket
        [Authorize]
        public async Task<IActionResult> Chat(int id)
        {
            var ticket = await _unitOfWork.GetRepository<SupportTicket>().GetByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var responses = await _unitOfWork.GetRepository<SupportResponse>()
                .GetAllAsync(r => r.SupportTicketId == id);

            var viewModel = new SupportChatViewModel
            {
                Ticket = ticket,
                Responses = responses
            };

            return View(viewModel);
        }

        // FAQ: List all FAQs
        [Authorize]
        public async Task<IActionResult> FAQ()
        {
            var faqs = await _unitOfWork.GetRepository<FAQS>().GetAllAsync();
            return View(faqs);
        }

        // Create Ticket: Display form to create a new support ticket
        [Authorize]
        public IActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket(SupportTicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
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

                try
                {
                    await _unitOfWork.GetRepository<SupportTicket>().AddAsync(ticket);
                    TempData["Success"] = "Ticket created successfully!";
                    return RedirectToAction(nameof(Tickets));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the ticket. Exception: {ex.Message}";
                }
            }

            return View(ticketViewModel);
        }

        // Create Response: Display form to create a new response for a ticket
        [Authorize]
        public IActionResult CreateResponse(int ticketId)
        {
            var viewModel = new SupportResponseViewModel
            {
                SupportTicketId = ticketId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResponse(SupportResponseViewModel responseViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var response = new SupportResponse
                {
                    ResponseText = responseViewModel.ResponseText,
                    CreatedAt = DateTime.UtcNow,
                    SupportTicketId = responseViewModel.SupportTicketId,
                    RespondedByUserId = userId ?? string.Empty
                };

                try
                {
                    await _unitOfWork.GetRepository<SupportResponse>().AddAsync(response);
                    TempData["Success"] = "Response added successfully!";
                    return RedirectToAction(nameof(Chat), new { id = responseViewModel.SupportTicketId });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while adding the response. Exception: {ex.Message}";
                }
            }

            return View(responseViewModel);
        }

        // Create FAQ: Display form to create a new FAQ
        [Authorize]
        public IActionResult CreateFAQ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFAQ(FAQSViewModel faqViewModel)
        {
            if (ModelState.IsValid)
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

                try
                {
                    await _unitOfWork.GetRepository<FAQS>().AddAsync(faq);
                    TempData["Success"] = "FAQ created successfully!";
                    return RedirectToAction(nameof(FAQ));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while creating the FAQ. Exception: {ex.Message}";
                }
            }

            return View(faqViewModel);
        }

        // Edit FAQ: Display form to edit an existing FAQ
        [Authorize]
        public async Task<IActionResult> EditFAQ(int id)
        {
            var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
            if (faq == null)
            {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFAQ(int id, FAQSViewModel faqViewModel)
        {
            if (id != faqViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
                if (faq == null)
                {
                    return NotFound();
                }

                faq.Question = faqViewModel.Question;
                faq.Answer = faqViewModel.Answer;
                faq.Category = faqViewModel.Category;
                faq.UpdatedAt = DateTime.UtcNow;

                try
                {
                    await _unitOfWork.GetRepository<FAQS>().UpdateAsync(faq);
                    TempData["Success"] = "FAQ updated successfully!";
                    return RedirectToAction(nameof(FAQ));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while updating the FAQ. Exception: {ex.Message}";
                }
            }

            return View(faqViewModel);
        }

        // Delete FAQ: Display confirmation page to delete an FAQ
        [Authorize(Policy = "Delete")]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        [HttpPost, ActionName("DeleteFAQ")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFAQConfirmed(int id)
        {
            var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
            if (faq != null)
            {
                try
                {
                    await _unitOfWork.GetRepository<FAQS>().RemoveAsync(faq);
                    TempData["Success"] = "FAQ deleted successfully!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"An error occurred while deleting the FAQ. Exception: {ex.Message}";
                }
            }
            else
            {
                TempData["Error"] = "FAQ not found.";
            }

            return RedirectToAction(nameof(FAQ));
        }
    }
}