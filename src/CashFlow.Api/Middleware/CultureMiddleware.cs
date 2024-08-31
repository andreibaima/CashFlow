using System.Globalization;

namespace CashFlow.Api.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        //requestDelegate -> como se fosse uma permissão, se pode ou não continuar o fluxo
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //verifica todos idiomas que o dotnet da suporte, para caso envio um que não existe deixamos o padrão
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("en");

            if (string.IsNullOrWhiteSpace(requestedCulture) == false 
                && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
