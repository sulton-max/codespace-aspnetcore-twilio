namespace App.Api.Brokers;

public interface ISmsProviderBroker
{
    ValueTask<bool> SendSmsAsync(string phoneNumber, string message, string from, string to);
}