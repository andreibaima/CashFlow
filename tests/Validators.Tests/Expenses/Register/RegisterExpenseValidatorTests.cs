using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Request;
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

            var request = new RequestRegisterExpenseJson
            {
                Amount = 100,
                Date = DateTime.Now.AddDays(-1),
                Description = "Description",
                Title = "",
                PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
            };


            //Act ->execuçãod as ações

            var result = validator.Validate(request);

            //Assert -> resultado esperado

            Assert.True(result.IsValid);
        }

         
    }
}
