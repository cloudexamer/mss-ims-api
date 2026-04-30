using MediatR;
using Microsoft.EntityFrameworkCore;
using mss.ims.application.Common.Interfaces;
using mss.ims.application.Customers.Dtos;

namespace mss.ims.application.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomerByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Where(x => x.Id == request.Id)
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    CustomerNumber = x.CustomerNumber,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    AddressLine1 = x.Address.AddressLine1,
                    AddressLine2 = x.Address.AddressLine2,
                    City = x.Address.City,
                    State = x.Address.State,
                    PostalCode = x.Address.PostalCode,
                    Country = x.Address.Country,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}