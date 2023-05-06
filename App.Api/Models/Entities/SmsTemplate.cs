using App.Api.Models.Common;

namespace App.Api.Models.Entities;

public class SmsTemplate : IEntity
{
    public long Id { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? From { get; set; }
}