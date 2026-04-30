namespace mss.ims.domain.Vendors
{
    using mss.ims.domain.Common;
    using mss.ims.domain.Shared;

    public class Vendor : AuditableEntity
    {
        public string VendorNumber { get; private set; }
        public string Name { get; private set; }

        public string Email { get; private set; }
        public string Phone { get; private set; }
        public Address Address { get; private set; }

        public bool IsActive { get; private set; }

        private Vendor()
        {
        }

        public Vendor(
            string vendorNumber,
            string name,
            string email,
            string phone,
            Address address)
        {
            VendorNumber = vendorNumber;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            IsActive = true;
        }

        public void Update(
            string name,
            string email,
            string phone,
            Address address)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}