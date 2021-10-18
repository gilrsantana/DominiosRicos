using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var boleto = new CreateBoletoSubscriptionCommand();
            boleto.FirstName = "";
            boleto.Validate();
            Assert.IsFalse(boleto.IsValid);
        }
        
    }
}