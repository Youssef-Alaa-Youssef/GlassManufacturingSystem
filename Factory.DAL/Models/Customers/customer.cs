namespace Factory.DAL.Models.Customers
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string FirstNameEnglish { get; set; } = string.Empty;
        public string LastNameEnglish { get; set; } = string.Empty;
        public string FirstNameArabic { get; set; } = string.Empty;
        public string LastNameArabic { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }
        public string CustomerType { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
