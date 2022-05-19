using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Users;
using ToroBank.Application.Features.Users.Queries.GetUserById;
using ToroBank.WebApi.Helpers;

namespace ToroBank.WebApi.Controllers
{
    public class UserController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the current user's data for use in most application operations.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] GetUserByIdQuery cmd)
        {
            var response = await _mediator.Send(cmd);
            return Ok(response.Data);
        }
    }
}
