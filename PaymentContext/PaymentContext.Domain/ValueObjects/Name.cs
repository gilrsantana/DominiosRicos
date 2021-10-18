using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "Name.FirstName", "Nome inválido")
                .IsNotNullOrEmpty(LastName, "Name.LastName", "Sobrenome inválido")
                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome precisa conter pelo menos 3 caracteres")
                .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome precisa conter pelo menos 3 caracteres")
                .IsLowerOrEqualsThan(FirstName, 40, "Name.FirstName", "Nome deve conter no máximo 40 caracteres")
                .IsLowerOrEqualsThan(LastName, 40, "Name.LastName", "Sobrenome deve conter no máximo 40 caracteres")
            );
        }

        public string CompletName()
        {
            return $"{FirstName} {LastName}";
        }

    }
}