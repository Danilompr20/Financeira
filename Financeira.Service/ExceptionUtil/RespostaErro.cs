

namespace Financeira.Service.ExceptionUtil
{
    public  class RespostaErro
    {
        public List<string> Reprovado { get; set; }
        public RespostaErro(string mensagemErro)
        {
            Reprovado = new List<string>();
            Reprovado.Add(mensagemErro);
        }
        public RespostaErro(List<string> mensagemErro)
        {
            Reprovado = mensagemErro;
        }
    }
}
