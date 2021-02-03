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

        [HttpPost]
        [Route("register")]
        public async Task Register([FromBody]UserRegistrationRequest request)
        {

        }
    }
}
