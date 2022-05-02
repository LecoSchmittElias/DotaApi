using DotaApi.Dtos;
using DotaApi.Enums;
namespace DotaApi.Entities
{
    public class PersonagemGetEntity: PersonagemEntity
    {
        public PersonagemGetEntity(Guid? id, EntradaDto? personagem)
        {
            if (id != null) Id = (Guid)id;
            if (!string.IsNullOrEmpty(personagem.Nome)) Nome = personagem.Nome;
            if (!string.IsNullOrEmpty(personagem.Funcao)) Funcao = personagem.Funcao;
            if (personagem.AtributoPrimario != null) AtributoPrimario = (PersonagemEnum.Atributo)personagem.AtributoPrimario;
            if (personagem.EstiloAtaque != null) EstiloAtaque = (PersonagemEnum.Estilo)personagem.EstiloAtaque;

            AtributoSecundario = personagem.AtributoSecundario;
            Imagem = personagem.Imagem;
            dificuldade = personagem.Dificuldade;
        }
    }
}
