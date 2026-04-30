using MediatR;
using mss.ims.application.Common.Interfaces;
using mss.ims.domain.Employees;
using mss.ims.domain.Shared;

namespace mss.ims.application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
                request.EmployeeNumber,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.Department,
                request.JobTitle,
                request.HireDateUtc,
                new Address(
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.State,
                    request.PostalCode,
                    request.Country));

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}