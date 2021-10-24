using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataBaseLayer;
using Microsoft.EntityFrameworkCore;

namespace UserService.CQRS.Users.Queries
{

    /// <summary>
    /// Запрос на получение всех юзеров
    /// </summary>
    public class GetAllUsers : IRequest<IEnumerable<Account>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsers, IEnumerable<Account>>
        {
            private readonly TestDataBaseContext _context;

            public GetAllUsersQueryHandler(TestDataBaseContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Account>> Handle(GetAllUsers request, CancellationToken cancellationToken)
            {
                var users = await _context.Accounts.Include(i => i.Profile.Department).Select(i => new Account() 
                { 
                    Id = i.Id, 
                    Email = i.Email,
                    Name = i.Name,
                    Profile = i.Profile != null ? i.Profile : new Profile() { Id = i.Id }
                }).ToListAsync();

                return users;
            }
        }
    }
}
