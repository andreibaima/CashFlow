namespace CashFlow.Communication.Response
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages { get; set; }

        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessages = [errorMessage]; //new List<string> { errorMessage };
        }
        public ResponseErrorJson(List<string> errors)
        {
            ErrorMessages = errors;
        }
    }
}
