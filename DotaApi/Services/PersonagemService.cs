using DotaApi.Dtos;
using DotaApi.Repositories;
using DotaApi.Utils.Extensions;
using DotaApi.Enums;
using DotaApi.Entities;

namespace DotaApi.Services
{
    public class PersonagemService : IPersonagemService
    {
        public readonly IPersonagemRepository _personagemRepository;

        public PersonagemService(IPersonagemRepository personagemRepository)
        {
            _personagemRepository = personagemRepository;
        }

        private (string, bool) Verificador(EntradaDto personagem)
        {
            (string, bool) integridadeInicial = personagem.VerificarDadosEspecificosNulos();

            if (integridadeInicial.Item2 == false) return (integridadeInicial.Item1, integridadeInicial.Item2);

            (string, bool) integridadeFinal = personagem.VerificarDadosTipo();

            if (integridadeFinal.Item2 == false) return integridadeFinal;

            return ("", true);
        }

        public RetornoDto InserirPersonagem(EntradaDto? personagem)
        {
            var verificador = Verificador(personagem);
            if (verificador.Item2 == false) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, verificador.Item1);

            var personagemInserido = new PersonagemEntity(personagem);


            if (_personagemRepository.SelectNome(personagemInserido) != null) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, "Ja temos um heroi cadastrado com esse nome!");

            try
            {
                _personagemRepository.InsertPersonagem(personagemInserido);

                return new RetornoDto(SistemaEnum.Retorno.Criado, _personagemRepository);

            }
            catch (Exception ex)
            {
                return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, $"Exceção gerada!: {ex}");
            }

        }

        public RetornoDto PegarPersonagem(Guid? id, EntradaDto? personagem)
        {
            (string, bool) integridadeInicial = personagem.VerificarDadosNulos();

            if (id == null && integridadeInicial.Item2 == false) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, integridadeInicial.Item1);

            var personagemInserido = new PersonagemGetEntity(id, personagem);

            try
            {
                var personagemEncontrado = _personagemRepository.SelectPersonagem(personagemInserido);

                if (personagemEncontrado.Count == 0) return new RetornoDto(SistemaEnum.Retorno.NotFound, null);

                return new RetornoDto(SistemaEnum.Retorno.Encontrado, personagemEncontrado);
            }
            catch (Exception ex)
            {
                return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, $"Exceção gerada!: {ex}");
            }

        }


        public RetornoDto DeletarPersonagem(Guid? id)
        {
            if (id == null) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null);

            var personagemInserido = new PersonagemEntity((Guid)id);

            var personagemEncontrado = _personagemRepository.SelectId(personagemInserido);

            if(personagemEncontrado == null) return new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            try
            {
                _personagemRepository.RemoverPersonagem(personagemEncontrado);
                return new RetornoDto(SistemaEnum.Retorno.Ok, null, $"{personagemEncontrado.Nome} foi deletado!");
            }
            catch (Exception ex)
            {
                return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, $"Exceção gerada!: {ex}");
            }
        }


        public RetornoDto MudarPersonagem(Guid? id, EntradaDto? personagem)
        {
            if (id == null) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null);

            var verificador = Verificador(personagem);
            if (verificador.Item2 == false) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, verificador.Item1);

            var personagemInserido = new PersonagemEntity((Guid)id, personagem);

            var personagemEncontrado = _personagemRepository.SelectId(personagemInserido);

            if (personagemEncontrado == null) return new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            var modificacaoCerta = personagemInserido.VerificarAtualizacaoTotal(personagemEncontrado);

            if (modificacaoCerta.Item2 == false) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null,modificacaoCerta.Item1);

            if (_personagemRepository.SelectNome(personagemInserido) != null) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, "Ja temos um heroi cadastrado com esse nome!");

            try
            {
                var nomeReserva = personagemEncontrado.Nome;
                _personagemRepository.ModificarPersonagem(personagemInserido, personagemEncontrado);
                return new RetornoDto(SistemaEnum.Retorno.Ok, null, $"{nomeReserva} foi alterado!");
            }
            catch(Exception ex)
            {
                return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, $"Exceção gerada!: {ex}");
            }
        }


        public RetornoDto AtualizarPersonagem(Guid? id, EntradaDto? personagem)
        {
            if (id == null) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null);

            var verificador = Verificador(personagem);
            if (verificador.Item2 == false) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, verificador.Item1);

            var personagemInserido = new PersonagemEntity(id.Value, personagem);

            var personagemEncontrado = _personagemRepository.SelectId(personagemInserido);

            if (personagemEncontrado == null) return new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            var modificacaoCerta = personagemInserido.VerificarAtualizacaoParcial(personagemEncontrado);

            if (modificacaoCerta.Item2 == false) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, modificacaoCerta.Item1);

            if (_personagemRepository.SelectNome(personagemInserido) != null) return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, "Ja temos um heroi cadastrado com esse nome!");

            try
            {
                var nomeReserva = personagemEncontrado.Nome;
                _personagemRepository.ModificarPersonagem(personagemInserido, personagemEncontrado);
                return new RetornoDto(SistemaEnum.Retorno.Ok, null, $"{nomeReserva} foi alterado!");
            }
            catch (Exception ex)
            {
                return new RetornoDto(SistemaEnum.Retorno.BadRequest, null, $"Exceção gerada!: {ex}");
            }
        }
    }
}
