using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _doc;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _doc = new Document("70446678058", EDocumentType.CPF);
            _email = new Email("batman@dc.com");
            _address = new Address("Santana", 
                                        "11",
                                         null, 
                                         null, 
                                         "Free", 
                                         "Gothan City", 
                                         "New Jersey", 
                                         "USA", 
                                         "22334455");
            _student = new Student(_name, _doc, _email);
            _subscription = new Subscription(null);  
            
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            //First test
            //Assert.Fail();
            var payment = new CreditCardPayment("Bruce Wayne", 
                                                    "5345209440610145", 
                                                    "214511414788457896523114", 
                                                    DateTime.Now.AddDays(10), 
                                                    DateTime.Now.AddYears(2), 
                                                    250.00m, 
                                                    250.00m, 
                                                    "Bruce Wayne", 
                                                    _doc, 
                                                    _address, 
                                                    _email);
            
            _subscription.AddPayment(payment);
            
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenHadInactiveSubscription()
        { 
            var payment = new CreditCardPayment("Bruce Wayne", 
                                                    "5345209440610145", 
                                                    "214511414788457896523114", 
                                                    DateTime.Now.AddDays(10), 
                                                    DateTime.Now.AddYears(2), 
                                                    250.00m, 
                                                    250.00m, 
                                                    "Bruce Wayne", 
                                                    _doc, 
                                                    _address, 
                                                    _email);
            
            _subscription.AddPayment(payment);
             
            Assert.IsTrue(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            //First test
            //Assert.Fail();
            _student.AddSubscription(_subscription);
            Assert.IsFalse(_student.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenSubscriptionHasPayment()
        {
            //First test
            //Assert.Fail();
            var subscription = new Subscription(null);
            var payment = new CreditCardPayment("Bruce Wayne", 
                                                    "5345209440610145", 
                                                    "214511414788457896523114", 
                                                    DateTime.Now.AddDays(10), 
                                                    DateTime.Now.AddYears(2), 
                                                    250.00m, 
                                                    250.00m, 
                                                    "Bruce Wayne", 
                                                    _doc, 
                                                    _address, 
                                                    _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.IsValid);
        }
    }
} 