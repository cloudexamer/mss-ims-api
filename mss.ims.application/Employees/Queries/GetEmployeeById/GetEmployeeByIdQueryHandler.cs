using MediatR;
using Microsoft.EntityFrameworkCore;
using mss.ims.application.Common.Interfaces;
using mss.ims.application.Employees.Dtos;
using mss.ims.application.Employees.Dtos.mss.ims.application.Employees.Dtos;

namespace mss.ims.application.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;

        public GetEmployeeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}