using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Address { get; private set; }
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract<Email>()
            .Requires()
            .IsEmail(Address, "Email.Address", "E-mail inválido")
            );
        }

    }
}