using Microsoft.AspNetCore.Mvc;
using Factory.DAL.Models.Notifications;
using Factory.PL.Services.Notify;
using Factory.BLL.InterFaces;
using Factory.DAL.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Factory.PL.Hubs;

namespace Factory.PL.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub> hubContext,INotificationService notificationService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var notifications = await _unitOfWork.GetRepository<Notification>()
                .Query()
                .Where(n => !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }

        public async Task<IActionResult> Add()
        {
            var users = await _unitOfWork.GetRepository<ApplicationUser>().GetAllAsync();
            ViewBag.Users = users.Select(u => new { u.Id, FullName = $"{u.FullName} - {u.UserName}" });  

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Notification notification)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    notification.CreatedAt = DateTime.UtcNow;

                    await _unitOfWork.GetRepository<Notification>().AddAsync(notification);
                    await _unitOfWork.SaveChangesAsync();

                    await _hubContext.Clients.User(notification.UserId)
                        .SendAsync("ReceiveNotification", new
                        {
                            notification.Message,
                            notification.Type,
                            notification.IconClass,
                            notification.CreatedAt
                        });

                    TempData["Success"] = "Notification added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while adding the notification: " + ex.Message;
                }
            }
            else
            {
                TempData["Error"] = "There were errors in your submission. Please check the form and try again.";
            }

            var users = await _unitOfWork.GetRepository<ApplicationUser>().GetAllAsync();
            ViewBag.Users = users.Select(u => new { u.Id, FullName = $"{u.FullName} - {u.UserName}" });

            return View(notification);
        }

        [HttpGet("api/notification/unread")]
        public async Task<IActionResult> GetUnreadNotifications()
        {
            var notifications = await _unitOfWork.GetRepository<Notification>()
                .Query()
                .Where(n => !n.IsRead) 
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => new
                {
                    n.Id,
                    n.Message,
                    n.Type,
                    n.IconClass,
                    n.CreatedAt
                })
                .ToListAsync();

            return Json(notifications);
        }

        [HttpPost("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _unitOfWork.GetRepository<Notification>().GetByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            notification.IsRead = true;
            await _unitOfWork.SaveChangesAsync();

            return Ok(); 
        }

        public async Task<IActionResult> Details(int id)
        {
            var notification = await _unitOfWork.GetRepository<Notification>()
                .Query()
                .Include(n => n.UserId) 
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }
    }
}