using AutoMapper;
using DataBaseLayer;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountService.CQRS.Commands
{

    /// <summary>
    /// Создание изера
    /// </summary>
    public class CreateUser
    {
        // Модель данных
        public class CreateUserModel : IRequest<Account>
        {
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
        public class CreateDepartmentValidator : AbstractValidator<CreateUserModel>
        {
            public CreateDepartmentValidator()
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

        public class CreateUserHandler : BaseContext, IRequestHandler<CreateUserModel, Account>
        {
            public CreateUserHandler(TestDataBaseContext context, IMapper _mapper) : base(context, _mapper)
            {

            }

            public async Task<Account> Handle(CreateUserModel request, CancellationToken cancellationToken)
            {
                var accountDtoMapped = mapper.Map<DataBaseLayer.Account>(request);

                context.Accounts.Add(accountDtoMapped);
                await context.SaveChangesAsync();

                return accountDtoMapped;
            }

        }
    }
}
