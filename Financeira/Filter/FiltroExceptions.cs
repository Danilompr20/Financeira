using Financeira.Service.ExceptionUtil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Financeira.Filter
{
    public class FiltroExceptions : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           if (context.Exception is FinanciamentoException) 
            {
                TratarfinanciamentoException(context);
            }
            else
            {
                LancarErroDesconehecido(context);
            }
        }

        private void TratarfinanciamentoException(ExceptionContext context)
        {
            if (context.Exception is ErroDeValidacaoException)
            {
                TratarErroValidacaoException(context);
            }
        }

        private void TratarErroValidacaoException(ExceptionContext context)
        {
            var erroValidacao = context.Exception as ErroDeValidacaoException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new RespostaErro(erroValidacao.MensagemErro));
        }

        private void LancarErroDesconehecido(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode =(int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new RespostaErro("Erro Desconhecido"));
        }
    }
}
