using MediatR;
using Microsoft.EntityFrameworkCore;
using mss.ims.application.Common.Interfaces;
using mss.ims.application.Employees.Dtos;
using mss.ims.application.Employees.Dtos.mss.ims.application.Employees.Dtos;

namespace mss.ims.application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetEmployeesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    EmployeeNumber = x.EmployeeNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Phone = x.Phone,
                    Department = x.Department,
                    JobTitle = x.JobTitle,
                    HireDateUtc = x.HireDateUtc,
                    IsActive = x.IsActive,
                    AddressLine1 = x.Address.AddressLine1,
                    AddressLine2 = x.Address.AddressLine2,
                    City = x.Address.City,
                    State = x.Address.State,
                    PostalCode = x.Address.PostalCode,
                    Country = x.Address.Country
                })
                .ToListAsync(cancellationToken);
        }
    }
}