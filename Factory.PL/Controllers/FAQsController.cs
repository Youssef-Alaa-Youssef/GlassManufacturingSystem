//using Microsoft.AspNetCore.Mvc;
//using Factory.BLL.InterFaces;
//using Microsoft.AspNetCore.Authorization;
//using Factory.PL.ViewModels.Home;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System;
//using Factory.DAL.Models.Support;

//namespace Factory.Controllers
//{
//    [Authorize(Roles = "Administrator")]
//    public class FAQsController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public FAQsController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var faqs = await _unitOfWork.GetRepository<FAQS>().GetAllAsync();
//            return View(faqs);
//        }

//        public async Task<IActionResult> Details(int id)
//        {
//            var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
//            if (faq == null)
//            {
//                return NotFound();
//            }

//            return View(faq);
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(FAQSViewModel faqViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                var faq = new FAQS
//                {
//                    Question = faqViewModel.Question,
//                    Answer = faqViewModel.Answer,
//                    CreatedAt = DateTime.UtcNow,
//                    UserId = userId ?? string.Empty
//                };

//                try
//                {
//                    await _unitOfWork.GetRepository<FAQS>().AddAsync(faq);
//                    TempData["Success"] = "FAQ created successfully!";
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (Exception ex)
//                {
//                    TempData["Error"] = string.Format("An error occurred while creating the FAQ. Exception: {0}", ex.Message);
//                }
//            }

//            return View(faqViewModel);
//        }

//        public async Task<IActionResult> Edit(int id)
//        {
//            var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
//            if (faq == null)
//            {
//                return NotFound();
//            }

//            var faqViewModel = new FAQSViewModel
//            {
//                Id = faq.Id,
//                Question = faq.Question,
//                Answer = faq.Answer,
//                CreatedAt = faq.CreatedAt,
//            };

//            return View(faqViewModel);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, FAQSViewModel faqViewModel)
//        {
//            if (id != faqViewModel.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
//                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

//                if (faq == null)
//                {
//                    return NotFound();
//                }

//                faq.Question = faqViewModel.Question;
//                faq.Answer = faqViewModel.Answer;
//                faq.CreatedAt = faqViewModel.CreatedAt; 
//                faq.UserId = userId ?? string.Empty;

//                try
//                {
//                    await _unitOfWork.GetRepository<FAQS>().UpdateAsync(faq);
//                    TempData["Success"] = "FAQ updated successfully!";
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (Exception ex)
//                {
//                    TempData["Error"] = string.Format("An error occurred while updating the FAQ. Exception: {0}", ex.Message);
//                }
//            }

//            return View(faqViewModel);
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
//            if (faq == null)
//            {
//                return NotFound();
//            }

//            return View(faq);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var faq = await _unitOfWork.GetRepository<FAQS>().GetByIdAsync(id);
//            if (faq != null)
//            {
//                try
//                {
//                    await _unitOfWork.GetRepository<FAQS>().RemoveAsync(faq);
//                    TempData["Success"] = "FAQ deleted successfully!";
//                }
//                catch (Exception ex)
//                {
//                    TempData["Error"] = string.Format("An error occurred while deleting the FAQ. Exception: {0}", ex.Message);
//                }
//            }
//            else
//            {
//                TempData["Error"] = "FAQ not found.";
//            }

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
