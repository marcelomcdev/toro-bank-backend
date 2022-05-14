using ToroBank.Domain.Entities;

namespace ToroBank.Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdDTO(
        int Id,
        int AccountNumber, 
        string Name,
        string CPF,
        decimal Balance
        )
    {
        public static GetUserByIdDTO ToDto(User user)
        {
            return new GetUserByIdDTO(
                user.Id,
                user.AccountNumber,
                user.Name,                
                user.CPF,
                user.Balance
                );
        }
    }

}
