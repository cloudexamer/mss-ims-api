namespace mss.ims.api.Features.Customers
{
    using global::mss.ims.application.Customers.Commands.CreateCustomer;
    using global::mss.ims.application.Customers.Commands.DeleteCustomer;
    using global::mss.ims.application.Customers.Commands.UpdateCustomer;
    using global::mss.ims.application.Customers.Queries.GetCustomerById;
    using global::mss.ims.application.Customers.Queries.GetCustomers;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    namespace mss.ims.api.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CustomersController : ControllerBase
        {
            private readonly IMediator _mediator;

            public CustomersController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(new GetCustomersQuery(), cancellationToken);
                return Ok(result);
            }

            [HttpGet("{id:guid}")]
            public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
            {
                var result = await _mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command, CancellationToken cancellationToken)
            {
                var id = await _mediator.Send(command, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = id }, id);
            }

            [HttpPut("{id:guid}")]
            public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command, CancellationToken cancellationToken)
            {
                if (id != command.Id)
                    return BadRequest("Route id and request id do not match.");

                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }

            [HttpDelete("{id:guid}")]
            public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
            {
                await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
                return NoContent();
            }
        }
    }
}
