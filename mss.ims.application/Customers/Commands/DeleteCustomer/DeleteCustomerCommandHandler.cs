using MediatR;
using mss.ims.application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace mss.ims.application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (customer == null)
                throw new Exception("Customer not found.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
