﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToroBank.Application.Features.Assets;
using ToroBank.Application.Features.Authentication;
using ToroBank.Application.Features.Authentication.Commands;
using ToroBank.Application.Features.PurchaseOrder.Commands;
using ToroBank.WebApi.Helpers;

namespace ToroBank.WebApi.Controllers
{

    
    public class AuthController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository, IMediator mediator)
        {
            _authRepository = authRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Makes the purchase order for a chosen asset and processes the balance and inclusion of a new asset in the customer's list.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] AuthUserCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return Created($"/[controller]/{response.Data}", response.Data);
        }
    }
}