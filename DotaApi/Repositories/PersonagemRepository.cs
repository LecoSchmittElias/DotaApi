using DotaApi.Entities;
using DotaApi.Enums;

namespace DotaApi.Repositories
{
    public class PersonagemRepository : IPersonagemRepository
    {
        public List<PersonagemEntity> Personagens { get; set; } = new List<PersonagemEntity>()
        {
            new PersonagemEntity()
            {
            Id = Guid.Parse("b301341c-7a52-4792-be27-937b103ea6ca"),
            Nome = "Razor",
            Funcao = "Carry",
            EstiloAtaque = PersonagemEnum.Estilo.Ranged,
            Dificuldade = PersonagemEnum.Dificuldade.Facil,
            AtributoPrimario = PersonagemEnum.Atributo.Agilidade,
            AtributoSecundario = PersonagemEnum.Atributo.Forca,
            Imagem = "https://cdn.cloudflare.steamstatic.com/apps/dota2/videos/dota_react/heroes/renders/razor.png"
            },
            new PersonagemEntity()
            {
            Id = Guid.Parse("dd5555cc-51a9-44fe-bd84-a971a1fd239b"),
            Nome = "Tusk",
            Funcao = "Initiator",
            EstiloAtaque = PersonagemEnum.Estilo.Melee,
            Dificuldade = PersonagemEnum.Dificuldade.Facil,
            AtributoPrimario = PersonagemEnum.Atributo.Forca,
            AtributoSecundario = PersonagemEnum.Atributo.Inteligencia,
            Imagem = "https://cdn.cloudflare.steamstatic.com/apps/dota2/videos/dota_react/heroes/renders/tusk.png"
            }

        };

        public void InsertPersonagem(PersonagemEntity personagem) => Personagens.Add(personagem);

        public PersonagemEntity SelectNome(PersonagemEntity personagem) => Personagens.Find(x => x.Nome.ToUpper() == personagem.Nome.ToUpper());

        public PersonagemEntity SelectId(PersonagemEntity personagem) => Personagens.Find(x => x.Id == personagem.Id);

        public List<PersonagemEntity> SelectPersonagem(PersonagemGetEntity personagem) => Personagens.FindAll(x => x.Id == (personagem.Id != Guid.Empty ? personagem.Id : x.Id)
                                                                                                           && x.Nome.ToLower() == (!string.IsNullOrEmpty(personagem.Nome) ? personagem.Nome.ToLower() : x.Nome.ToLower())
                                                                                                           && x.Funcao.ToLower() == (!string.IsNullOrEmpty(personagem.Funcao) ? personagem.Funcao.ToLower() : x.Funcao.ToLower())
                                                                                                           && x.Dificuldade == (personagem.Dificuldade != null ? personagem.Dificuldade : x.Dificuldade)
                                                                                                           && x.EstiloAtaque == (personagem.EstiloAtaque != 0 ? personagem.EstiloAtaque : x.EstiloAtaque)
                                                                                                           && x.AtributoPrimario == (personagem.AtributoPrimario != 0 ? personagem.AtributoPrimario : x.AtributoPrimario)
                                                                                                           && x.AtributoSecundario == (personagem.AtributoSecundario != null ? personagem.AtributoSecundario : x.AtributoSecundario)
                                                                                                           && x.Imagem.ToLower() == (!string.IsNullOrEmpty(personagem.Imagem) ? personagem.Imagem.ToLower() : x.Imagem.ToLower()));

        public void RemoverPersonagem(PersonagemEntity personagem) => Personagens.Remove(personagem);

        public void ModificarPersonagem(PersonagemEntity personagemInserido, PersonagemEntity personagemEncontrado)
        {
            var localizacao = Personagens.IndexOf(personagemEncontrado);

            Personagens[localizacao].EstiloAtaque = personagemInserido.EstiloAtaque;
            Personagens[localizacao].Nome = personagemInserido.Nome;
            Personagens[localizacao].Funcao = personagemInserido.Funcao;
            Personagens[localizacao].AtributoPrimario = personagemInserido.AtributoPrimario;
            Personagens[localizacao].AtributoSecundario = personagemInserido.AtributoSecundario;
            Personagens[localizacao].Imagem = personagemInserido.Imagem;
            Personagens[localizacao].Dificuldade = personagemInserido.Dificuldade;
        }

    }
}
