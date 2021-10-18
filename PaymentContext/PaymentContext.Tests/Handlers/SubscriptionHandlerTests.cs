using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Fakes;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "12345678910";
            command.Email = "teste@email.com";
            command.BarCode = "888888888888888";
            command.BoletoNumber = "55555555555555";
            command.PaymentNumber = "2222222";
            command.PaidDate = DateTime.Now.AddDays(1);
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 99m;
            command.TotalPaid = 99m;
            command.Payer = "Wayne Coorp";
            command.PayerDocument = "65656565656565";
            command.PayerDocumentType = EDocumentType.CNPJ;
            command.PayerEmail = "batman@dc.com";
            command.Street = "wefreww";
            command.Number = "5645";
            command.Complement = "wewewew";
            command.Reference = "werwer";
            command.Neighborhood = "werwer";
            command.City = "werwe";
            command.State = "werwer";
            command.Country = "wer";
            command.Zip = "21212121";

            handler.Handle(command);
            Assert.IsFalse(handler.IsValid);
        }
    }
}