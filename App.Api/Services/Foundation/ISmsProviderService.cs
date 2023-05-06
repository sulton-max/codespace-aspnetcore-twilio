namespace App.Api.Services.Foundation;

public interface ISmsProviderService
{
    ValueTask<bool> SendSmsAsync(string phoneNumber, string message, string from, string to);
}