using CashFlow.Communication.Enums;
using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
        {
            Validade(request);
            return new ResponseRegisteredExpenseJson();
        }

        private void Validade(RequestRegisterExpenseJson request)
        {
            var validator = new RegisterExpenseValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var erroMensages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(erroMensages);
            }
        }
    }
}
