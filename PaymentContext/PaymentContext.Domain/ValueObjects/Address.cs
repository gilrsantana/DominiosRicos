using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string Reference { get; set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; set; }
        public string Zip { get; private set; }
        
        public Address(string street, 
            string number, 
            string complement, 
            string reference, 
            string neighborhood, 
            string city, 
            string state, 
            string country, 
            string zip)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Reference = reference;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            Zip = zip;

            AddNotifications(new Contract<Address>()
                .Requires()
                .IsNullOrWhiteSpace(Street, "Address.Street", "Rua inválido")
                .IsNullOrWhiteSpace(Number, "Address.Number", "Número inválido")
                .IsNullOrWhiteSpace(Neighborhood, "Address.Neighborhood", "Bairro inválido")
                .IsNullOrWhiteSpace(City, "Address.City", "Cidade inválida")
                .IsNullOrWhiteSpace(State, "Address.State", "Estado inválido")
                .IsNullOrWhiteSpace(Country, "Address.Country", "País inválido")
                .IsNullOrWhiteSpace(Zip, "Address.Zip", "CEP inválido")

                .IsGreaterThan(Street, 3, "Address.Street", "Rua precisa conter pelo menos 3 caracteres")
                .IsGreaterThan(Neighborhood, 3, "Address.Neighborhood", "Bairro precisa conter pelo menos 3 caracteres")
                .IsGreaterThan(City, 3, "Address.City", "Cidade precisa conter pelo menos 3 caracteres")
                .IsGreaterThan(State, 3, "Address.State", "Estado precisa conter pelo menos 3 caracteres")
                .IsGreaterThan(Country, 3, "Address.Country", "País precisa conter pelo menos 3 caracteres")
                .IsGreaterThan(Zip, 8, "Address.Zip", "CEP precisa conter 8 caracteres")

                .IsLowerThan(Street, 50, "Address.Street", "Rua deve conter no máximo 50 caracteres")
                .IsLowerThan(Neighborhood, 40, "Address.Neighborhood", "Bairro deve conter no máximo 40 caracteres")
                .IsLowerThan(City, 50, "Address.City", "Cidade deve conter no máximo 50 caracteres")
                .IsLowerThan(State, 40, "Address.State", "Estado deve conter no máximo 40 caracteres")
                .IsLowerThan(Country, 40, "Address.Country", "País precisa deve conter no máximo 40 caracteres")
                .IsLowerThan(Zip, 8, "Address.Zip", "CEP deve conter no máximo 8 caracteres")
            );
        }

    }
}