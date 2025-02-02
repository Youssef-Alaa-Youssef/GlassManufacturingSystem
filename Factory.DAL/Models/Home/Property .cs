using Factory.DAL.Enums;

namespace Factory.DAL.Models.Home
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int SquareFootage { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string PropertyType { get; set; } = string.Empty;
        public DateTime ListedDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Features { get; set; } = string.Empty;
        public List<string> PhotoUrls { get; set; } = new List<string>();
        public PropertyStatus Status { get; set; }
        public bool HasGarage { get; set; }
        public bool HasPool { get; set; }
        public bool IsFurnished { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public string AgentEmail { get; set; } = string.Empty;
        public string AgentPhone { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public bool IsVisabled { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string IpAddress { get; set; } = string.Empty;
    }
}
