using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MedicalLoginSystemConsole.Services
{
    public static class SmsService
    {
        public static string LatestCode { get; private set; }

        private static readonly string accountSid = "AC8883a1e050ceef2b55e104ec4f112738";
        private static readonly string authToken = "eb7c9d1ea5ee5befb4ee0e84725454e4";
        private static readonly string fromPhone = "+13434537782";

        public static void SendCode(string toPhone)
        {
            TwilioClient.Init(accountSid, authToken);

            var random = new Random();
            LatestCode = random.Next(100000, 999999).ToString();

            MessageResource.Create(
                body: $"Your verification code is {LatestCode}",
                from: new PhoneNumber(fromPhone),
                to: new PhoneNumber(toPhone)
            );
        }
    }
}