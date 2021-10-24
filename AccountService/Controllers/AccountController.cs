using AccountService.CQRS.Commands;
using DataBaseLayer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        /// <summary>
        /// Добавление нового юзера в БД
        /// </summary>
        /// <param name="model">Модель данных с валидацией Fluent Validator</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateUser.CreateUserModel model)
        {
            Account account = await mediator.Send(model);

            account.Profile = new Profile()
            {
                Id = account.Id
            };

            return Ok(account);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            bool deletedAccount = await mediator.Send(new RemoveUser.RemoveUserModel() { userId = id });


            // В случае если надо было вернуть удаленно юзера, то переделать, но смысла не вижу в задаче

            return deletedAccount;
        }

        [HttpPut]
        public async Task<bool> Update(UpdateUser.UpdateUserModel model)
        {
            bool updated = await mediator.Send(model);

            // В случае если надо было вернуть измененного юзера, то переделать, но смысла не вижу в задаче


            return updated;
        }

    }
}
