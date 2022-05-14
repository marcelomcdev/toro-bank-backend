using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Users;
using ToroBank.Application.Features.Users.Queries.GetUserById;


namespace ToroBank.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] GetUserByIdQuery cmd)
        {
            var response = await _mediator.Send(cmd);
            return Created($"/[controller]/{response.Data}", null);
        }
    }
}
