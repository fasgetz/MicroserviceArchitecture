using AutoMapper;
using DataBaseLayer;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.CQRS.Commands
{
    public class RemoveUser
    {
        // Модель данных
        public class RemoveUserModel : IRequest<bool>
        {
            /// <summary>
            /// Айди юзера в бд
            /// </summary>
            public int userId { get; set; }
        }


        // Валидатор FluentValidation
        public class CreateDepartmentValidator : AbstractValidator<RemoveUserModel>
        {
            public CreateDepartmentValidator()
            {
                RuleFor(x => x.userId)
                    .NotNull().WithMessage("Айди не может быть пустым")
                    .InclusiveBetween(1, int.MaxValue);
            }
        }


        public class RemoveUserHandler : BaseContext, IRequestHandler<RemoveUserModel, bool>
        {
            public RemoveUserHandler(TestDataBaseContext context, IMapper _mapper) : base(context, _mapper)
            {

            }

            public async Task<bool> Handle(RemoveUserModel request, CancellationToken cancellationToken)
            {
                var account = await context.Accounts.FirstOrDefaultAsync(i => i.Id == request.userId);

                if (account == null)
                {
                    return false;
                }

                context.Accounts.Remove(account);
                await context.SaveChangesAsync();

                return true;
            }


        }
    }
}
