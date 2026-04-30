using MediatR;
using mss.ims.application.Common.Interfaces;
using mss.ims.domain.Customers;
using mss.ims.domain.Shared;

namespace mss.ims.application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(
                request.CustomerNumber,
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

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }
    }
}