using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor
        [TestMethod]
        public void ShouldReturnErrorIsCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("29143306000189")]
        [DataRow("73074966000147")]
        [DataRow("98036731000191")]
        [DataRow("59630783000130")]
        [DataRow("37829312000112")]
        public void ShouldReturnSuccessIsCNPJIsValid(string cnpj)
        {
            var doc = new Document(cnpj, EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }
        
        [TestMethod]
        public void ShouldReturnErrorIsCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }
        
        [TestMethod]
        [DataTestMethod]
        [DataRow("32644837006")]
        [DataRow("01196193002")]
        [DataRow("51435823001")]
        [DataRow("84346677037")]
        [DataRow("88335364087")]
        public void ShouldReturnSuccessIsCPFIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}