using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public  Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        
        // Utilizar IReadOnlyCollection ao invés de List para que somente 
        // dentro da classe Student seja possível adicionar algo na lista.  
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }
        
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions =  new List<Subscription>();
            // Ou se agrupam estas notificações de erro dentro da própria classe ou no value object.
            // A linha abaixo agrupa todos os erros das propriedades.
            AddNotifications(name, document, email);    
        }

        public void AddSubscription(Subscription subscription)
        {
            
            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsFalse(hasSubscriptionActive, 
                    "Student.Subscriptions", 
                    "Você já tem uma assinatura ativa")
                .AreNotEquals(0, subscription.Payments.Count, 
                    "Studente.Subscription.Payments", 
                    "Esta assinatura não possui pagamentos")
            );

            if (subscription.IsValid)
                _subscriptions.Add(subscription);

            // alternativa
            // if(hasSubscriptionActive)
            //     AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa");
        }
    }
}