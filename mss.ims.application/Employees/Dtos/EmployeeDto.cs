namespace mss.ims.application.Employees.Dtos
{
    namespace mss.ims.application.Employees.Dtos
    {
        public class EmployeeDto
        {
            public Guid Id { get; set; }
            public string EmployeeNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName { get; set; }

            public string Email { get; set; }
            public string Phone { get; set; }

            public string Department { get; set; }
            public string JobTitle { get; set; }

            public DateTime HireDateUtc { get; set; }
            public bool IsActive { get; set; }

            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
        }
    }
}
