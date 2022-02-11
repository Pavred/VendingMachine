using App.Repositories;
using NUnit.Framework;
using Moq;
using App.DataAccess;
using AutoFixture;
using System;
using FluentAssertions;

namespace App.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private CustomerService _customerService;
        private Mock<ICompanyRepository> _companyRepository;
        private Mock<ICustomerCreditService> _customerCreditService;
        private Mock<ICoreDataAcccess> _dataAcccess;
        private readonly IFixture _fixture = new Fixture();
 

        public CustomerServiceTests()
        {
            _companyRepository = new Mock<ICompanyRepository>();
            _customerCreditService = new Mock<ICustomerCreditService>();
            _dataAcccess = new Mock<ICoreDataAcccess>();

            _customerService = new CustomerService(_companyRepository.Object,_customerCreditService.Object, _dataAcccess.Object);
        }

        [Test]
        public void AddCustomer_ShouldAddCustomer_WithValidInputs()
        {
           
            // Arrange
            const int companyId = 1;
            const string firname = "Pavitha";
            const string surname = "Sandeep";
            const string email = "Abc@test.com";
            var dateOfBirth = new DateTime(1993, 1, 1);
            
            var company = _fixture.Build<Company>().With(c => c.Id, companyId).Create();
            _companyRepository.Setup(x => x.GetById(companyId)).Returns(company);
            _customerCreditService.Setup(x => x.GetCreditLimit(firname,surname,dateOfBirth)).Returns(700);

            // Act

            var result = _customerService.AddCustomer(firname,surname,email,dateOfBirth,companyId);

            // Assert
            result.Should().BeTrue();
            _dataAcccess.Verify(x => x.AddCustomer(It.IsAny<Customer>()),Times.Once);
        }

        [Test]
        [TestCase("", "Surname", "abc.test@test.com", "1993, 1, 1")]
        [TestCase("ABC", "", "abc.test@test.com", "1993, 1, 1")]
        [TestCase("ABC", "Surname", "", "1993, 1, 1")]
        [TestCase("ABC", "Surname", "abc", "1993, 1, 1")]

        public void AddCustomer_ShouldNotAddCustomer_WithInValidInputs(string firname, string surname
            , string email, DateTime? dateOfBirth)
        {
            // Arrange
            const int companyId = 1;


            var company = _fixture.Build<Company>().With(c => c.Id, companyId).Create();
            _companyRepository.Setup(x => x.GetById(companyId)).Returns(company);
            _customerCreditService.Setup(x => x.GetCreditLimit(firname, surname, dateOfBirth.Value)).Returns(700);

            // Act

            var result = _customerService.AddCustomer(firname, surname, email, dateOfBirth.Value, companyId);

            // Assert
            result.Should().BeFalse();
        }
    }
}
