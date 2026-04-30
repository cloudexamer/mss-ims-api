using MediatR;
using Microsoft.EntityFrameworkCore;
using mss.ims.application.Common.Interfaces;
using mss.ims.application.Customers.Dtos;

namespace mss.ims.application.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
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
                .ToListAsync(cancellationToken);
        }
    }
}