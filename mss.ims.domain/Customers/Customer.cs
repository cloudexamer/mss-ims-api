
using mss.ims.domain.Common;
using mss.ims.domain.Shared;

namespace mss.ims.domain.Customers;

public class Customer : AuditableEntity
{
    public string CustomerNumber { get; private set; }
    public string Name { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public bool IsActive { get; private set; }
    public Address Address { get; private set; }

    private Customer() { }

    public Customer(string customerNumber, string name, string? email, string? phone, Address address)
    {
        CustomerNumber = customerNumber;
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        IsActive = true;
    }

    public void Update(string name, string? email, string? phone, Address address)
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
}