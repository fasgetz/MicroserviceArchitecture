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

    /// <summary>
    /// Обновление данных
    /// </summary>
    public class UpdateUser
    {
        // Модель данных
        public class UpdateUserModel : IRequest<bool>
        {
            /// <summary>
            /// ID USER
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// Имя
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Почта
            /// </summary>
            public string Email { get; set; }
        }

        // Валидатор FluentValidation
        public class UpdateUserValidator : AbstractValidator<UpdateUserModel>
        {
            public UpdateUserValidator()
            {
                RuleFor(x => x.Name)
                    .NotNull().WithMessage("Название имени не может быть пустым")
                    .MinimumLength(1).WithMessage("Минимальная длина названия имени должна быть больше 1 символа!")
                    .MaximumLength(30).WithMessage("Максимальная длина имени может быть 30 символов!");

                RuleFor(x => x.Email)
                    .NotNull().WithMessage("Название имени не может быть пустым")
                    .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Введите настоящий email")
                    .MinimumLength(1).WithMessage("Минимальная длина названия имени должна быть больше 1 символа!")
                    .MaximumLength(30).WithMessage("Максимальная длина имени может быть 30 символов!");
            }
        }

        public class UpdateUserHandler : BaseContext, IRequestHandler<UpdateUserModel, bool>
        {
            public UpdateUserHandler(TestDataBaseContext context, IMapper _mapper) : base(context, _mapper)
            {

            }

            public async Task<bool> Handle(UpdateUserModel request, CancellationToken cancellationToken)
            {

                var account = await context.Accounts.FirstOrDefaultAsync(i => i.Id == request.id);

                if (account == null)
                {
                    return false;
                }


                account.Email = request.Email;
                account.Name = request.Name;

                await context.SaveChangesAsync();

                return true;
            }

        }
    }
}
