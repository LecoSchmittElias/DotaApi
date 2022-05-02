using DotaApi.Enums;
namespace DotaApi.Dtos
{
    public class EntradaDto
    {
        public string? Nome { get; set; }

        public string? Funcao { get; set; }

        public PersonagemEnum.Dificuldade? Dificuldade { get; set; }

        public PersonagemEnum.Estilo? EstiloAtaque { get; set; }

        public PersonagemEnum.Atributo? AtributoPrimario { get; set; }

        public PersonagemEnum.Atributo? AtributoSecundario { get; set; }

        public string? Imagem { get; set; }
    }
}
