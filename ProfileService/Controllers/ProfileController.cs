using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProfileService.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPut]
        public async Task<bool> Update(UpdateProfile.UpdateProfileModel model)
        {
            bool updated = await mediator.Send(model);

            // В случае если надо было вернуть измененного юзера, то переделать, но смысла не вижу в задаче


            return updated;
        }
    }
}
