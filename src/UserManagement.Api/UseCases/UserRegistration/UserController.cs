using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.UseCases.UserRegistration;

namespace UserManagement.Api.UseCases.UserRegistration
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return new CreatedResult($"/user/profile/{result.Id}", result);
        }
    }
}
