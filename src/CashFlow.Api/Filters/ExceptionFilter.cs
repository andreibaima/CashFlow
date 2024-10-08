﻿using CashFlow.Communication.Response;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is CashFlowException)
            {
                HandleProjectExcption(context);
            } else
            {
                ThrowUnkowErro(context);
            }
        }

        private void HandleProjectExcption(ExceptionContext context) {
            if(context.Exception is ErrorOnValidationException)
            {
                //var ex = context.Exception as ErrorOnValidationException; caso não seja, retorna nullo
                var ex = (ErrorOnValidationException)context.Exception; // caso não fosse retornaria erro

                var errorResponse = new ResponseErrorJson(ex.Erros);

                //
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
            } else
            {
                var errorResponse = new ResponseErrorJson(context.Exception.Message);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        private void ThrowUnkowErro(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);


            // -> no controller return StatusCode(StatusCodes.Status500InternalServerError, msg)
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
