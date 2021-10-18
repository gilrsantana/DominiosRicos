using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();
            for (var i = 0; i < 2; i++)
            {
                _students.Add(new Student(new Name("Aluno", "Numero " + i.ToString()), 
                                          new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                                          new Email(i.ToString() + "@email.com")
                                          ));   
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var expression = StudentQueries.GetStudentInfo("12345678910");
            var student = _students.AsQueryable().Where(expression).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        public void ShouldReturnStudentWhenDocumentExists()
        {
            var expression = StudentQueries.GetStudentInfo("1111111111");
            var student = _students.AsQueryable().Where(expression).FirstOrDefault();

            Assert.AreNotEqual(null, student);
        }
    }
}