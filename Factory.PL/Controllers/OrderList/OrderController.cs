//using Microsoft.AspNetCore.Mvc;
//using Factory.PL.ViewModels.OrderList;
//using Factory.DAL.Models.OrderList;
//using Factory.BLL.InterFaces;
//using Factory.DAL.Models.Warehouses; // Assuming IUnitOfWork and repositories are set up

//namespace Factory.PL.Controllers
//{
//    public class OrderController : Controller
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public OrderController(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        // GET: Order/Create
//        public async Task<IActionResult> Create()
//        {
//            var model = new OrderViewModel
//            {
//                Items = await _unitOfWork.GetRepository<Item>().GetAllAsync() // Get available items
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(OrderViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Process selected machines
//                var selectedMachines = model.SelectedMachines.Where(x => !string.IsNullOrEmpty(x)).ToList();

//                // Fix: Check for the count of selected machines
//                if (selectedMachines == 0)
//                {
//                    ModelState.AddModelError("SelectedMachines", "Please select at least one machine.");
//                    return View(model);
//                }

//                // Process items and calculate
//                foreach (var item in model.Items)
//                {
//                    item.SQM = CalculateSQM(item.Width, item.Height);
//                    item.TotalSQM = item.SQM * item.Quantity;
//                    item.TotalLM = CalculateLM(item.Width, item.Quantity);
//                }

//                var order = new Order
//                {
//                    CustomerName = model.CustomerName,
//                    CustomerReference = model.CustomerReference,
//                    ProjectName = model.ProjectName,
//                    Date = model.Date,
//                    JobNo = model.JobNo,
//                    Address = model.Address,
//                    Priority = model.Priority,
//                    FinishDate = model.FinishDate,
//                    Signature = model.Signature,
//                    IsAccepted = model.IsAccepted,
//                    Items = model.Items.Select(i => new OrderItem
//                    {
//                        ItemName = i.ItemName,
//                        Width = i.Width,
//                        Height = i.Height,
//                        Quantity = i.Quantity,
//                        SQM = i.SQM,
//                        TotalSQM = i.TotalSQM,
//                        TotalLM = i.TotalLM,
//                        CustomerReference = i.CustomerReference
//                    }).ToList()
//                };

//                try
//                {
//                    // Save the order
//                    await _unitOfWork.GetRepository<Order>().AddAsync(order);
//                    return RedirectToAction("Index", "Home"); // Redirect after successful creation
//                }
//                catch (Exception ex)
//                {
//                    // Add error message if something goes wrong
//                    ModelState.AddModelError("", $"There was an error while creating the order: {ex.Message}");
//                    return View(model);
//                }
//            }

//            return View(model); // Return the model with validation errors
//        }

//        private decimal CalculateSQM(decimal width, decimal height)
//        {
//            return (width / 1000) * (height / 1000);
//        }

//        private decimal CalculateLM(decimal width, int quantity)
//        {
//            return (width / 1000) * quantity;
//        }
//    }
//}
