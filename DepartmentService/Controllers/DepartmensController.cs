using DataBaseLayer;
using DepartmentService.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepartmentService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator mediator;

        public DepartmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
            IEnumerable<Department> departments = await mediator.Send(new GetAllDepartmens());

            return departments;
        }
    }
}
