using MediatR;
using mss.ims.application.Employees.Dtos;
using mss.ims.application.Employees.Dtos.mss.ims.application.Employees.Dtos;

namespace mss.ims.application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery : IRequest<List<EmployeeDto>>
    {
    }
}