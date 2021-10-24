using AccountService.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Mapping
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUser.CreateUserModel, DataBaseLayer.Account>();
            CreateMap<UpdateUser.UpdateUserModel, DataBaseLayer.Account>();
        }
    }
}
