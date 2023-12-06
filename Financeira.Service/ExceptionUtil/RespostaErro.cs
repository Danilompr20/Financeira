

namespace Financeira.Service.ExceptionUtil
{
    public  class RespostaErro
    {
        public List<string> MensagemErro { get; set; }
        public RespostaErro(string mensagemErro)
        {
            MensagemErro = new List<string>();
            MensagemErro.Add(mensagemErro);
        }
        public RespostaErro(List<string> mensagemErro)
        {
            MensagemErro = mensagemErro;
        }
    }
}
