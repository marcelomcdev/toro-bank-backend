using ToroBank.Application.Common.Interfaces.Services;

namespace ToroBank.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
