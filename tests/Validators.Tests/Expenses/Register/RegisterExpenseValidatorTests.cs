using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Request;
using CashFlow.Exception;
using CommonTestUtilities.Request;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validators.Tests.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact] //entendi que é um teste
        public void Sucess()
        {
            //Arrange (instancias para testes)
            var validator = new RegisterExpenseValidator();

            var request = RequestRegisterExpenseJsonBuilder.Build();

            //Act ->execução das ações

            var result = validator.Validate(request);

            //Assert -> resultado esperado

            result.IsValid.Should().BeTrue();
        }
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Error_Title_Empty(string title)
        {
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Title = title;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));

        }

        [Fact]
        public void Error_DateFuture()
        {
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE));

        }

        [Fact]
        public void Error_Payment_Type_Invalid()
        {
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.PaymentType = (PaymentType)700;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Error_Amount_Invalid(decimal amount)
        {
            var validator = new RegisterExpenseValidator();
            var request = RequestRegisterExpenseJsonBuilder.Build();
            request.Amount = amount;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));

        }

        /*public void Sucess()
        {
            //Arrange (instancias para testes)
            var validator = new RegisterExpenseValidator();

            var request = new RequestRegisterExpenseJson
            {
                Amount = 100,
                Date = DateTime.Now.AddDays(-1),
                Description = "Description",
                Title = "",
                PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
            };


            //Act ->execução das ações

            var result = validator.Validate(request);

            //Assert -> resultado esperado

            Assert.True(result.IsValid);
        }*/


    }
}
