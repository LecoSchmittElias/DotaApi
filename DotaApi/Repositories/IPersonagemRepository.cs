using DotaApi.Entities;

namespace DotaApi.Repositories
{
    public interface IPersonagemRepository
    {
        public void InsertPersonagem(PersonagemEntity personagem);

        public PersonagemEntity SelectNome(PersonagemEntity personagem);

        public PersonagemEntity SelectId(PersonagemEntity personagem);

        public void RemoverPersonagem(PersonagemEntity personagem);

        public List<PersonagemEntity> SelectPersonagem(PersonagemGetEntity personagem);

        public void ModificarPersonagem(PersonagemEntity personagemInserido, PersonagemEntity personagemEncontrado);


    }
}
