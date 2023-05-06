using Twilio;
using Twilio.Clients;
using Twilio.Http;

namespace App.Api.Brokers;

public class SmsProviderBroker : ISmsProviderBroker
{
    private readonly ITwilioRestClient _twilioClient;

    public SmsProviderBroker(ITwilioRestClient twilioClient)
    {
        _twilioClient = twilioClient;
    }

    public ValueTask<bool> SendSmsAsync(string phoneNumber, string message, string from, string to)
    {
    }
}