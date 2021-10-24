using AutoMapper;
using DataBaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.CQRS
{
    public class BaseContext
    {
        private protected readonly TestDataBaseContext context;
        private protected readonly IMapper mapper;

        public BaseContext(TestDataBaseContext context, IMapper _mapper)
        {
            this.context = context;
            mapper = _mapper;
        }
    }
}
