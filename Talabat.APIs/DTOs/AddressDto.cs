namespace Talabat.APIs.DTOs
{
    public class AddressDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
