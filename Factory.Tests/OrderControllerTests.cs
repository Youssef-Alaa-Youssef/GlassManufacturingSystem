using Factory.BLL.InterFaces;
using Factory.Controllers;
using Factory.DAL.Models.OrderList;
using Factory.PL.ViewModels.OrderList;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class OrderControllerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly OrderController _controller;

    public OrderControllerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _controller = new OrderController(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewWithOrders()
    {
        var orders = new List<Order>
        {
            new Order { Id = 1, CustomerName = "John Doe", CustomerReference = "Ref123" },
            new Order { Id = 2, CustomerName = "Jane Smith", CustomerReference = "Ref456" }
        };
        _mockUnitOfWork.Setup(u => u.GetRepository<Order>().GetAllAsync()).ReturnsAsync(orders);

        var result = await _controller.Index() as ViewResult;
        Assert.NotNull(result);
        var model = Assert.IsType<List<OrderViewModel>>(result.Model);
        Assert.Equal(2, model.Count);
    }

    [Fact]
    public async Task Details_OrderExists_ReturnsView()
    {
        var order = new Order { Id = 1, CustomerName = "John Doe", CustomerReference = "Ref123" };
        _mockUnitOfWork.Setup(u => u.GetRepository<Order>().GetByIdAsync(1)).ReturnsAsync(order);

        var result = await _controller.Details(1) as ViewResult;

        Assert.NotNull(result);
        var model = Assert.IsType<OrderViewModel>(result.Model);
        Assert.Equal(order.Id, model.Id);
    }

    [Fact]
    public async Task Details_OrderDoesNotExist_ReturnsNotFound()
    {
        _mockUnitOfWork.Setup(u => u.GetRepository<Order>().GetByIdAsync(1)).ReturnsAsync((Order)null);

        var result = await _controller.Details(1);

        Assert.IsType<NotFoundResult>(result);
    }
}
