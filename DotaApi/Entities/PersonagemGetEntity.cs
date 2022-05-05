using DotaApi.Dtos;
namespace DotaApi.Entities
{
    public class PersonagemGetEntity : PersonagemEntity
    {
        public PersonagemGetEntity(Guid? id, EntradaDto? personagem)
        {
            if (id.HasValue) Id = id.Value;
            if (!string.IsNullOrEmpty(personagem.Nome)) Nome = personagem.Nome;
            if (!string.IsNullOrEmpty(personagem.Funcao)) Funcao = personagem.Funcao;
            if (personagem.AtributoPrimario.HasValue) AtributoPrimario = personagem.AtributoPrimario.Value;
            if (personagem.EstiloAtaque.HasValue) EstiloAtaque = personagem.EstiloAtaque.Value;


            AtributoSecundario = personagem.AtributoSecundario;
            Imagem = personagem.Imagem;
            Dificuldade = personagem.Dificuldade;
        }
    }
}
