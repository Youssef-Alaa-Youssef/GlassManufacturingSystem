using Factory.DAL.Models.OrderList;
using Factory.PL.ViewModels.OrderList;
namespace Factory.PL.Helper
{
    public static class OrderMapper
    {
        public static OrderViewModel MapToViewModel(Order order) => new()
        {
            Rank = order.Rank,
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
            Items = order.Items?.Select(i => new OrderItemViewModel
            {
                Id = i.Id,
                ItemName = i.ItemName,
                Width = i.Width,
                Height = i.Height,
                Quantity = i.Quantity,
                CustomerReference = i.CustomerReference,
                Description = i.Description
            }).ToList() ?? new List<OrderItemViewModel>()
        };
    }
}
