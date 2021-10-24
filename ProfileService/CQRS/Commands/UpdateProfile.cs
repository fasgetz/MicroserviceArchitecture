using AutoMapper;
using DataBaseLayer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileService.CQRS.Commands
{
    public class UpdateProfile
    {
        // Модель данных
        public class UpdateProfileModel : IRequest<bool>
        {
            /// <summary>
            /// ID USER
            /// </summary>
            public int id { get; set; }

            /// <summary>
            /// Имя
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// Почта
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            /// ID DEPARTMENT
            /// </summary>
            public byte? idDepartment { get; set; }
        }

        // Валидатор FluentValidation
        public class UpdateProfileValidator : AbstractValidator<UpdateProfileModel>
        {
            public UpdateProfileValidator()
            {
                RuleFor(x => x.FirstName)
                    .NotNull().WithMessage("Имя не может быть пустым")
                    .MinimumLength(1).WithMessage("Минимальная длина  имени должна быть больше 1 символа!")
                    .MaximumLength(30).WithMessage("Максимальная длина имени может быть 30 символов!");

                RuleFor(x => x.LastName)
                    .NotNull().WithMessage("Название имени не может быть пустым")                    
                    .MinimumLength(1).WithMessage("Минимальная длина названия имени должна быть больше 1 символа!")
                    .MaximumLength(30).WithMessage("Максимальная длина имени может быть 30 символов!");

                RuleFor(x => x.idDepartment).NotNull().WithMessage("Отдел должен быть заполнен");
            }
        }

        public class UpdateProfileHandler : IRequestHandler<UpdateProfileModel, bool>
        {
            private readonly TestDataBaseContext context;

            public UpdateProfileHandler(TestDataBaseContext context)
            {
                this.context = context;
            }

            public async Task<bool> Handle(UpdateProfileModel request, CancellationToken cancellationToken)
            {

                var account = await context.Accounts.Include(i => i.Profile).FirstOrDefaultAsync(i => i.Id == request.id);

                if (account == null)
                {
                    return false;
                }

                if (account.Profile == null)
                {
                    account.Profile = new DataBaseLayer.Profile();
                }

                account.Profile.FirstName = request.FirstName;
                account.Profile.LastName = request.LastName;
                account.Profile.IdDepartment = request.idDepartment;

                await context.SaveChangesAsync();

                return true;
            }

        }
    }
}
