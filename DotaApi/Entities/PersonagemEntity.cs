using DotaApi.Dtos;
using DotaApi.Enums;
namespace DotaApi.Entities
{
    public class PersonagemEntity
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Funcao { get; set; }

        public PersonagemEnum.Dificuldade? dificuldade { get; set; }

        public PersonagemEnum.Estilo EstiloAtaque { get; set; }

        public PersonagemEnum.Atributo AtributoPrimario { get; set; }

        public  PersonagemEnum.Atributo? AtributoSecundario { get; set; }

        public string? Imagem { get; set; }

        public PersonagemEntity() { }

        public PersonagemEntity(EntradaDto personagem)
        {
            Id = Guid.NewGuid();
            Nome = personagem.Nome;
            Funcao = personagem.Funcao;
            AtributoPrimario = (PersonagemEnum.Atributo)personagem.AtributoPrimario;
            AtributoSecundario = personagem.AtributoSecundario;
            Imagem = personagem.Imagem;
            EstiloAtaque = (PersonagemEnum.Estilo)personagem.EstiloAtaque;
            dificuldade = personagem.Dificuldade;
        }

        public PersonagemEntity(Guid id)
        {
            Id = id;
        }

        public PersonagemEntity(Guid id, EntradaDto personagem)
        {
            Id = id;
            Nome = personagem.Nome;
            Funcao = personagem.Funcao;
            AtributoPrimario = (PersonagemEnum.Atributo)personagem.AtributoPrimario;
            AtributoSecundario = personagem.AtributoSecundario;
            Imagem = personagem.Imagem;
            EstiloAtaque = (PersonagemEnum.Estilo)personagem.EstiloAtaque;
            dificuldade = personagem.Dificuldade;
        }

    }
}
