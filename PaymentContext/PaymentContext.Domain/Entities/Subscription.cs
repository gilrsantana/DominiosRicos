using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;

        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }
        
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            _payments = new List<Payment>();
        }

        public void AddPayment(Payment payment)
        {
            _payments.Add(payment);
            
            AddNotifications(new Contract<Subscription>()
                .Requires()
                .IsLowerThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura")
            );

        }

        public void Activate(bool activate)
        {
            if (activate)
            {
                Active = true;
                LastUpdateDate = DateTime.Now;
            }
            else
            {
                Active = false;
                LastUpdateDate = DateTime.Now;
            }
        }

        
    }
}