using MediatR;
using mss.ims.application.Customers.Dtos;

namespace mss.ims.application.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<List<CustomerDto>>
    {
    }
}
