using MediatR;
using mss.ims.application.Common.Interfaces;
using mss.ims.domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace mss.ims.application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (customer == null)
                throw new Exception("Customer not found.");

            customer.Update(
                request.Name,
                request.Email,
                request.Phone,
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