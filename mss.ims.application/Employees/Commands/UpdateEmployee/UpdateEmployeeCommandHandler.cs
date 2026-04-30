using MediatR;
using Microsoft.EntityFrameworkCore;
using mss.ims.application.Common.Interfaces;
using mss.ims.domain.Shared;

namespace mss.ims.application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (employee == null)
                throw new Exception("Employee not found.");

            employee.Update(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.Department,
                request.JobTitle,
                new Address(
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.State,
                    request.PostalCode,
                    request.Country));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}