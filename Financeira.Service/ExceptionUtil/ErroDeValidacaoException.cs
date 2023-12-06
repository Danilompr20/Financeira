

namespace Financeira.Service.ExceptionUtil
{
    public  class ErroDeValidacaoException:FinanciamentoException
    {
        public List<string> MensagemErro { get; set; }
        public ErroDeValidacaoException(List<string> mensagemErro)
        {
            MensagemErro = mensagemErro;
        }
    }
}
