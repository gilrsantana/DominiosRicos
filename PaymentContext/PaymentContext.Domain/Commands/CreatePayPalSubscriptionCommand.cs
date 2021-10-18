using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreatePayPalSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Document { get;  set; }
        public string Email { get;  set; }
        public string TransactionCode { get;  set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get;  set; }
        public DateTime ExpireDate { get;  set; }
        public decimal Total { get;  set; }
        public decimal TotalPaid { get;  set; }
        public string Payer { get;  set; }
        public string PayerDocument { get;  set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }
        public string Street { get;  set; }
        public string Number { get;  set; }
        public string Complement { get;  set; }
        public string Reference { get; set; }
        public string Neighborhood { get;  set; }
        public string City { get;  set; }
        public string State { get;  set; }
        public string Country { get; set; }
        public string Zip { get;  set; }

        public void Validate()
        {
            AddNotifications(new Contract<CreatePayPalSubscriptionCommand>()
                .Requires()
                .IsNotNullOrEmpty(FirstName, "Name.FirstName", "Nome inválido")
                .IsNotNullOrEmpty(LastName, "Name.LastName", "Sobrenome inválido")
                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome precisa conter pelo menos 3 caracteres")
                .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome precisa conter pelo menos 3 caracteres")
                .IsLowerOrEqualsThan(FirstName, 40, "Name.FirstName", "Nome deve conter no máximo 40 caracteres")
                .IsLowerOrEqualsThan(LastName, 40, "Name.LastName", "Sobrenome deve conter no máximo 40 caracteres")
            );
        }
    }
}