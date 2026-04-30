using MediatR;
using mss.ims.application.Employees.Dtos;
using mss.ims.application.Employees.Dtos.mss.ims.application.Employees.Dtos;

namespace mss.ims.application.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public Guid Id { get; set; }

        public GetEmployeeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}