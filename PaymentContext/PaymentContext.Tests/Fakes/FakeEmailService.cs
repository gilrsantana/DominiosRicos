using PaymentContext.Domain.Services;

namespace PaymentContext.Tests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public void Send(string to, string email, string subject, string body) { }
    }
}