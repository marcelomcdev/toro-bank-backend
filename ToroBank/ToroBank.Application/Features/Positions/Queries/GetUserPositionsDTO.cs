﻿using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Positions.Queries
{
    public record GetUserPositionDTO(
        List<Position> Positions,
        decimal CheckingAccountAmount,
        decimal Consolidated,
        decimal Investments
        )
    {
        public static GetUserPositionDTO ToDto(UserPosition userPosition)
        {
            return new GetUserPositionDTO(
           userPosition.Positions,
           userPosition.CheckingAccountAmount,
           userPosition.Consolidated,
           userPosition.Investments
           );
        }
    }

   
}
