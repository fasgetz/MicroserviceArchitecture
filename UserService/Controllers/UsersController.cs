using DataBaseLayer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.CQRS.Users.Queries;

namespace UserService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;


        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            IEnumerable<Account> users = await mediator.Send(new GetAllUsers());

            return users;
        }
    }
}
