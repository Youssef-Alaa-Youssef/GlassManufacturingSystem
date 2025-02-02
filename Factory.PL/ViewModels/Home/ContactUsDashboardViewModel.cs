using Factory.DAL.Models.Home;

namespace Factory.PL.ViewModels
{
    public class ContactUsDashboardViewModel
    {
        public List<ContactUs> Contacts { get; set; } = new List<ContactUs>();
        public List<ContactStats> ContactStats { get; set; } = new List<ContactStats>();
    }

    public class ContactStats
    {
        public string Category { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
