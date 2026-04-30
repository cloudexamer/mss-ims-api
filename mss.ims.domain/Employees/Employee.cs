using mss.ims.domain.Common;
using mss.ims.domain.Shared;

namespace mss.ims.domain.Employees
{
    public class Employee : AuditableEntity
    {
        public string EmployeeNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName
        {
            get { return (FirstName + " " + LastName).Trim(); }
        }

        public string Email { get; private set; }
        public string Phone { get; private set; }

        public string Department { get; private set; }
        public string JobTitle { get; private set; }

        public DateTime HireDateUtc { get; private set; }
        public bool IsActive { get; private set; }
        public Address Address { get; private set; }

        private Employee()
        {
        }

        public Employee(
            string employeeNumber,
            string firstName,
            string lastName,
            string email,
            string phone,
            string department,
            string jobTitle,
            DateTime hireDateUtc,
            Address address)
        {
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Department = department;
            JobTitle = jobTitle;
            HireDateUtc = hireDateUtc;
            IsActive = true;
            Address = address;
        }

        public void Update(
            string firstName,
            string lastName,
            string email,
            string phone,
            string department,
            string jobTitle,
            Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Department = department;
            JobTitle = jobTitle;
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