using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Authentication.Queries
{
    public record GetIdByTokenDTO(int Id)
    {
        public static GetIdByTokenDTO ToDto(int userId)
        {
            return new GetIdByTokenDTO(userId);
        }
    }


}
