using Factory.PL.Models;
using Factory.PL.Services.Order;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Factory.PL.Controllers.OrderList
{
    public class InvoiceController : Controller
    {
        private readonly IOrderService _orderService;

        // Set license according to your organization's status
        static InvoiceController()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public InvoiceController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Invoice/Details/{orderId}")]
        public async Task<IActionResult> Details(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return NotFound("Order ID is required.");
            }

            try
            {
                var orderData = await _orderService.GetOrderByIdAsync(orderId);
                if (orderData == null)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }

                var viewModel = new InvoiceViewModel
                {
                    OrderData = orderData
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet("Invoice/Download/{orderId}")]
        public async Task<IActionResult> Download(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return NotFound("Order ID is required.");
            }

            try
            {
                var orderData = await _orderService.GetOrderByIdAsync(orderId);
                if (orderData == null)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }

                var invoiceBytes = GenerateInvoicePdf(orderData);

                return File(
                    fileContents: invoiceBytes,
                    contentType: "application/pdf",
                    fileDownloadName: $"Invoice_{orderId}_{DateTime.Now:yyyyMMdd}.pdf"
                );
            }
            catch (Exception ex)
            {
                // Log the exception
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        [HttpGet("Invoice/Preview/{orderId}")]
        public async Task<IActionResult> Preview(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                return NotFound("Order ID is required.");
            }

            try
            {
                var orderData = await _orderService.GetOrderByIdAsync(orderId);
                if (orderData == null)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }

                var invoiceBytes = GenerateInvoicePdf(orderData);

                return File(invoiceBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                // Log the exception
                return View("Error", new ErrorViewModel { Message = ex.Message });
            }
        }

        private byte[] GenerateInvoicePdf(OrderData order)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // Page setup
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                    // Header
                    page.Header().Element(ComposeHeader);

                    // Content
                    page.Content().Element(container => ComposeContent(container, order));

                    // Footer
                    page.Footer().Element(ComposeFooter);
                });
            });

            using var stream = new MemoryStream();
            document.GeneratePdf(stream);
            return stream.ToArray();
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                // Company logo
                row.ConstantItem(150).Height(50).Placeholder();

                // Company details
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Your Company Name").Bold().FontSize(14);
                    column.Item().Text("123 Business Avenue, City, Country");
                    column.Item().Text("Tel: +1 234 567 890 | Email: contact@yourcompany.com");
                    column.Item().Text("VAT ID: 123456789");
                });
            });
        }

        private void ComposeContent(IContainer container, OrderData order)
        {
            container.PaddingVertical(1, Unit.Centimetre).Column(column =>
            {
                // Title
                column.Item().BorderBottom(1).PaddingBottom(5).Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text($"INVOICE").Bold().FontSize(20);
                        col.Item().Text($"#{order.InvoiceNumber}");
                    });

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignRight().Text("ISSUE DATE").Bold();
                        col.Item().AlignRight().Text($"{order.OrderDate:yyyy-MM-dd}");
                        col.Item().AlignRight().Text("DUE DATE").Bold();
                        col.Item().AlignRight().Text($"{order.DueDate:yyyy-MM-dd}");
                    });
                });

                // Customer and order information
                column.Item().PaddingTop(10).Grid(grid =>
                {
                    grid.Columns(2);
                    grid.Spacing(10);

                    grid.Item().Column(col =>
                    {
                        col.Item().Text("BILL TO").Bold();
                        col.Item().Text(order.Customer.Name);
                        col.Item().Text(order.Customer.Address);
                        col.Item().Text(order.Customer.Email);
                        col.Item().Text(order.Customer.Phone);
                    });

                    grid.Item().Column(col =>
                    {
                        col.Item().Text("ORDER DETAILS").Bold();
                        col.Item().Text($"Order ID: {order.OrderId}");
                        col.Item().Text($"Payment Method: {order.PaymentMethod}");
                        col.Item().Text($"Status: {order.Status}");
                    });
                });

                // Items table
                column.Item().PaddingTop(15).Element(container => ComposeTable(container, order.Items));

                // Summary
                column.Item().AlignRight().PaddingTop(10).Column(col =>
                {
                    col.Item().Text("PAYMENT SUMMARY").Bold();
                    col.Item().Grid(grid =>
                    {
                        grid.Columns(2);
                        grid.Item().Text("Subtotal:");
                        grid.Item().AlignRight().Text($"{order.Subtotal:C2}");
                        grid.Item().Text("Tax:");
                        grid.Item().AlignRight().Text($"{order.Tax:C2}");
                        grid.Item().Text("Shipping:");
                        grid.Item().AlignRight().Text($"{order.Shipping:C2}");
                        grid.Item().Text("Total:").Bold();
                        grid.Item().AlignRight().Text($"{order.Total:C2}").Bold();
                    });
                });

                // Notes
                column.Item().PaddingTop(15).Element(container =>
                {
                    container.Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                    {
                        col.Item().Text("NOTES").Bold();
                        col.Item().Text(order.Notes);
                        col.Item().PaddingTop(5).Text("Thank you for your business!").Italic();
                    });
                });
            });
        }

        private void ComposeTable(IContainer container, List<OrderItem> items)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(30);    // Item #
                    columns.RelativeColumn(3);     // Description
                    columns.RelativeColumn(1);     // Quantity
                    columns.RelativeColumn(1);     // Unit Price
                    columns.RelativeColumn(1);     // Total
                });

                // Add header row
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Grey.Medium).Padding(2).Text("#").Bold().FontColor(Colors.White);
                    header.Cell().Background(Colors.Grey.Medium).Padding(2).Text("DESCRIPTION").Bold().FontColor(Colors.White);
                    header.Cell().Background(Colors.Grey.Medium).Padding(2).Text("QTY").Bold().FontColor(Colors.White);
                    header.Cell().Background(Colors.Grey.Medium).Padding(2).Text("UNIT PRICE").Bold().FontColor(Colors.White);
                    header.Cell().Background(Colors.Grey.Medium).Padding(2).Text("AMOUNT").Bold().FontColor(Colors.White);
                });

                // Add items
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    var background = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten3;

                    table.Cell().Background(background).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(i + 1);
                    table.Cell().Background(background).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(item.Description);
                    table.Cell().Background(background).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2).AlignRight().Text(item.Quantity);
                    table.Cell().Background(background).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2).AlignRight().Text($"{item.UnitPrice:C2}");
                    table.Cell().Background(background).BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2).AlignRight().Text($"{item.Total:C2}");
                }
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().BorderTop(1).BorderColor(Colors.Grey.Medium).PaddingTop(5).Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Payment Terms: Due within 30 days").FontSize(9);
                        col.Item().Text("Please include the invoice number with your payment").FontSize(9);
                    });

                    row.RelativeItem().AlignRight().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
                });
            });
        }
    }
}
namespace Factory.PL.Models
{
    public class InvoiceViewModel
    {
        public OrderData OrderData { get; set; }
    }

    public class ErrorViewModel
    {
        public string Message { get; set; }
    }
}
namespace Factory.PL.Models
{
    public class OrderData
    {
        public string OrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public CustomerInfo Customer { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
        public string Notes { get; set; }
    }

    public class CustomerInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class OrderItem
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}