using ImprivateDinner.Application.Common.Interfaces.Services;

namespace ImprivateDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}