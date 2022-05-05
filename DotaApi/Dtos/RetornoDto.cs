using DotaApi.Enums;

namespace DotaApi.Dtos
{
    public class RetornoDto
    {

        public SistemaEnum.Retorno? Status { get; set; }

        public string Mensagem { get; set; }

        public object Retorno { get; set; }

        public RetornoDto(SistemaEnum.Retorno status, object? retorno)
        {
            Status = status;
            Retorno = retorno;

            switch (Status)
            {
                case SistemaEnum.Retorno.Criado: Mensagem = "Criado com sucesso!"; break;
                case SistemaEnum.Retorno.NotFound: Mensagem = "Item não encontrado!"; break;
                case SistemaEnum.Retorno.BadRequest: Mensagem = "Algo deu errado, tente novamente!"; break;
                case SistemaEnum.Retorno.Ok: Mensagem = "Fechou meu chapa :)"; break;
                case SistemaEnum.Retorno.Encontrado: Mensagem = "Item encontrado"; break;
                default: Mensagem = string.Empty; break;
            }
        }

        public RetornoDto(SistemaEnum.Retorno status, object retorno, string mensagem)
        {
            Status = status;
            Retorno = retorno;
            Mensagem = mensagem;
        }
    }
}
