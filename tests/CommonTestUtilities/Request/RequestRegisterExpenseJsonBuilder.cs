using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Request;

namespace CommonTestUtilities.Request
{
    public class RequestRegisterExpenseJsonBuilder
    {
        public static RequestRegisterExpenseJson Build()
        {
            /*var faker = new Faker();

            var request = new RequestRegisterExpenseJson
            {
                Title = faker.Commerce.Product(),
                Date = faker.Date.Past(),
            };*/

            return new Faker<RequestRegisterExpenseJson>()
                .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
                .RuleFor(r => r.Date, faker => faker.Date.Past())
                .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>())
                .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 100));

        }
    }
}
