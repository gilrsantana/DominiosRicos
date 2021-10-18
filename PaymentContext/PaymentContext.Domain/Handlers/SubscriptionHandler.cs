using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>,
                                       IHandler<CreateBoletoSubscriptionCommand>,
                                       IHandler<CreatePayPalSubscriptionCommand>,
                                       IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se documento está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se e-mail está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar  VO
            var name = new Name(command.FirstName, command.LastName);
            var doc = new Document("70446678058", EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,
                                        command.Number,
                                        command.Complement,
                                        command.Reference,
                                        command.Neighborhood,
                                        command.City,
                                        command.State,
                                        command.Country,
                                        command.Zip);

            // Gerar Entidades
            var student = new Student(name, doc, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode,
                                            command.BoletoNumber,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            command.Payer,
                                            new Document(command.PayerDocument, command.PayerDocumentType),
                                            address,
                                            email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Aplicar as validações
            AddNotifications(name, doc, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.CompletName(), student.Email.Address, "Bem vindo ao Portal", "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail Fast Validation
            // Precisa implementar
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se documento está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se e-mail está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar  VO
            var name = new Name(command.FirstName, command.LastName);
            var doc = new Document("70446678058", EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,
                                        command.Number,
                                        command.Complement,
                                        command.Reference,
                                        command.Neighborhood,
                                        command.City,
                                        command.State,
                                        command.Country,
                                        command.Zip);

            // Gerar Entidades
            var student = new Student(name, doc, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            command.Payer,
                                            new Document(command.PayerDocument, command.PayerDocumentType),
                                            address,
                                            email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, doc, email, address, student, subscription, payment);

            // Checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar a assinatura");

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.CompletName(), student.Email.Address, "Bem vindo ao Portal", "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail Fast Validation
            // Precisa implementar
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // Verificar se documento está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se e-mail está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar  VO
            var name = new Name(command.FirstName, command.LastName);
            var doc = new Document("70446678058", EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street,
                                        command.Number,
                                        command.Complement,
                                        command.Reference,
                                        command.Neighborhood,
                                        command.City,
                                        command.State,
                                        command.Country,
                                        command.Zip);

            // Gerar Entidades
            var student = new Student(name, doc, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(command.CardHolderName,
                                                command.CardNumber,
                                                command.LastTransactionNumber,
                                                command.PaidDate,
                                                command.ExpireDate,
                                                command.Total,
                                                command.TotalPaid,
                                                command.Payer,
                                                new Document(command.PayerDocument, command.PayerDocumentType),
                                                address,
                                                email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, doc, email, address, student, subscription, payment);

            // Checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar a assinatura");

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.CompletName(), student.Email.Address, "Bem vindo ao Portal", "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}