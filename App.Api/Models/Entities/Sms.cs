using App.Api.Models.Common;

namespace App.Api.Models.Entities;

public class Sms : IEntity
{
    public long Id { get; set; }

    public string To { get; set; } = string.Empty;

    public string From { get; set; } = string.Empty;

    public string Direction { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public int? ErrorCode { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public DateTime SentDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public DateTime CreatedDate { get; set; }
}