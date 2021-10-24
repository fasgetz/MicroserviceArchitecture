using DataBaseLayer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentService.CQRS.Queries
{
    public class GetAllDepartmens : IRequest<IEnumerable<Department>>
    {
        public class GetAllDepartmensQueryHandler : IRequestHandler<GetAllDepartmens, IEnumerable<Department>>
        {
            private readonly TestDataBaseContext _context;

            public GetAllDepartmensQueryHandler(TestDataBaseContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Department>> Handle(GetAllDepartmens request, CancellationToken cancellationToken)
            {
                var departmens = await _context.Departments.ToListAsync();

                return departmens;
            }
        }
    }
}
