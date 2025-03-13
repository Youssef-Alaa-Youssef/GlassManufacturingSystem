using Factory.BLL.InterFaces;
using Factory.DAL.Models.OrderList;
using Factory.DAL.Models.Warehouses;
using Factory.PL.ViewModels.OrderList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZXing;
using ZXing.Common;

namespace Factory.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize()]
        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetAllAsync();
            var orderViewModels = orders.Select(MapToViewModel).OrderByDescending(o=>o.Id).ToList();
            return View(orderViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }

        [Authorize()]
        public async Task<IActionResult> Create()
        {
            var model = await CreateOrderViewModel();
            return View(model);
        }
        [Authorize()]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = await CreateOrderViewModel();
                return View(model);
            }

            try
            {
                var order = MapToModel(orderViewModel);

                await _unitOfWork.GetRepository<Order>().AddAsync(order);

                TempData["Success"] = "Order created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.InnerException?.Message ?? ex.Message}";

                var model = await CreateOrderViewModel();
                return View(model);
            }
        }

        private static Order MapToModel(OrderViewModel viewModel, Order? order = null)
        {
            order ??= new Order();
            order.CustomerName = viewModel.CustomerName;
            order.CustomerReference = viewModel.CustomerReference;
            order.ProjectName = viewModel.ProjectName;
            order.Date = viewModel.Date;
            order.JobNo = viewModel.JobNo;
            order.Address = viewModel.Address;
            order.Priority = viewModel.Priority;
            order.FinishDate = viewModel.FinishDate;
            order.IsAccepted = viewModel.IsAccepted;
            order.Signature = viewModel.Signature;
            order.SelectedMachines = viewModel.SelectedMachines;
            order.TotalSQM = viewModel.TotalSQM;
            order.TotalLM = viewModel.TotalLM;

            order.Items = viewModel.Items
                .Where(i => !string.IsNullOrEmpty(i.ItemName) && i.Quantity > 0)
                .Select(i => new OrderItem
                {
                    Id = i.Id,
                    ItemName = i.ItemName,
                    Width = i.Width,
                    Height = i.Height,
                    StepWidth = i.StepWidth,
                    StepHeight = i.StepHeight,
                    Quantity = i.Quantity,
                    SQM = i.SQM,
                    TotalLM = i.TotalLM,
                    CustomerReference = i.CustomerReference,
                    Description = i.Description
                }).ToList();

            return order;
        }
        private async Task<OrderViewModel> CreateOrderViewModel()
        {
            var maxJobNo = (await _unitOfWork.GetRepository<Order>()
                              .GetAllAsync(order => order.JobNo != null))
                              .OrderByDescending(o => o.Id);

            string newJobNo = "JOB-0001";
            if (maxJobNo.Any())
            {
                var lastJobNo = maxJobNo.First().JobNo;
                if (lastJobNo != null && lastJobNo.StartsWith("JOB-"))
                {
                    if (int.TryParse(lastJobNo.Substring(4), out int jobNumber))
                    {
                        newJobNo = $"JOB-{(jobNumber + 1):D4}";
                    }
                }
            }

            var items = await _unitOfWork.GetRepository<Item>().GetAllAsync();

            var model = new OrderViewModel
            {
                JobNo = newJobNo,
                Date = DateTime.Now,
                FinishDate = DateTime.Now.AddDays(10),
                Priority = "High",
                IsAccepted = true,
                Signature = User.Identity?.Name,
                Items = items.Select(item => new OrderItemViewModel
                {
                    Id = item.Id,  // Assuming Item has an Id property
                    ItemName = item.NameEn,  // Assuming Item has a Name property
                }).ToList()
            };


            return model;
        }

        [Authorize()]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }
        [Authorize()]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid) return View(orderViewModel);

            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null) return NotFound();

            try
            {
                MapToModel(orderViewModel, order);
                await _unitOfWork.GetRepository<Order>().UpdateAsync(order);
                TempData["Success"] = "Order updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return View(orderViewModel);
            }
        }

        [Authorize()]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            return order == null ? NotFound() : View(order);
        }
        [Authorize()]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order != null)
            {
                await _unitOfWork.GetRepository<Order>().RemoveAsync(order);
                TempData["Success"] = "Order deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Order not found.";
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize()]
        public async Task<IActionResult> Optimization(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            var Items = await _unitOfWork.GetRepository<Item>().GetAllAsync();
            ViewBag.Item = Items;
            if (order == null) return NotFound();
            return View(MapToViewModel(order));
        }

        private static OrderViewModel MapToViewModel(Order order) => new()
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CustomerReference = order.CustomerReference,
            ProjectName = order.ProjectName,
            Date = order.Date,
            JobNo = order.JobNo,
            Address = order.Address,
            Priority = order.Priority,
            FinishDate = order.FinishDate,
            IsAccepted = order.IsAccepted,
            SelectedMachines = order.SelectedMachines,
            TotalSQM = order.TotalSQM,
            TotalLM = order.TotalLM,
            Items = order.Items.Select(i => new OrderItemViewModel
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Width = i.Width,
                Height = i.Height,
                Quantity = i.Quantity,
                CustomerReference = i.CustomerReference,
                Description = i.Description
            }).ToList()
        };

        [Authorize()]
        public async Task<IActionResult> GlassLabel(int id)
        {
            var order = await _unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var labelViewModel = MapToLabelViewModel(order);

            return View(labelViewModel);
        }

        private LabelViewModel MapToLabelViewModel(Order order)
        {
            return new LabelViewModel
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                JobNo = order.JobNo,
                OrderDate = order.Date.ToString("d"),
                Barcode = GenerateBarcode(order.Id),
                Items = order.Items.Select(item => new OrderItemViewModel
                {
                    Id = item.Id,
                    ItemName = item.ItemName,
                    Quantity = item.Quantity,
                    CustomerReference = item.CustomerReference
                }).ToList()
            };
        }


        private string GenerateBarcode(int orderId)
        {
            var barcodeWriter = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 50,
                    Width = 200
                }
            };

            var pixelData = barcodeWriter.Write(orderId.ToString());
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
                }
            }
        }
    }
}