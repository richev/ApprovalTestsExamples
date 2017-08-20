namespace Example.Services.Models
{
    public class AddressModel
    {
        public AddressType Type { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Postcode { get; set; }
    }
}
