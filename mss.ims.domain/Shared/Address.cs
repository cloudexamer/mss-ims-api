namespace mss.ims.domain.Shared
{
    public class Address
    {
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        private Address()
        {
        }

        public Address(
            string addressLine1,
            string addressLine2,
            string city,
            string state,
            string postalCode,
            string country)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        public void Update(
            string addressLine1,
            string addressLine2,
            string city,
            string state,
            string postalCode,
            string country)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }
    }
}