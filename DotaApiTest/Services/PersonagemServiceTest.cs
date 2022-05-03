using DotaApi.Dtos;
using DotaApi.Entities;
using DotaApi.Enums;
using DotaApi.Utils.Extensions;
using DotaApi.Repositories;
using DotaApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotaApiTest.Service
{
    public class PersonagemServiceTest
    {
        private Mock<IPersonagemRepository> _Repository;
        private IPersonagemService _service;

        public void Setup()
        {
            _Repository = new Mock<IPersonagemRepository>();
            _service = new PersonagemService(_Repository.Object);
        }

        private PersonagemEntity auxilioTestEntity()
        {
            var personagem = new PersonagemEntity()
            {
                Id = Guid.Parse("1e58171f-6d8c-4c89-b387-d43971d86134"),
                Nome = "Jhin",
                Funcao = "Carry",
                EstiloAtaque = PersonagemEnum.Estilo.Ranged,
                Dificuldade = PersonagemEnum.Dificuldade.Medio,
                AtributoPrimario = PersonagemEnum.Atributo.Agilidade,
                AtributoSecundario = PersonagemEnum.Atributo.Forca,
                Imagem = "teste"
            };

            return personagem;
        }

        private EntradaDto auxilioTestDto()
        {
            var personagem = new EntradaDto()
            {
                Nome = "Jhin",
                Funcao = "Carry",
                EstiloAtaque = PersonagemEnum.Estilo.Ranged,
                Dificuldade = PersonagemEnum.Dificuldade.Medio,
                AtributoPrimario = PersonagemEnum.Atributo.Agilidade,
                AtributoSecundario = PersonagemEnum.Atributo.Forca,
                Imagem = "f"
            };

            return personagem;
        }

        private EntradaDto auxilioTestPutDto()
        {
            var personagem = new EntradaDto()
            {
                Nome = "John",
                Funcao = "adc",
                EstiloAtaque = PersonagemEnum.Estilo.Melee,
                Dificuldade = PersonagemEnum.Dificuldade.Dificil,
                AtributoPrimario = PersonagemEnum.Atributo.Forca,
                AtributoSecundario = PersonagemEnum.Atributo.Inteligencia,
                Imagem = "f"
            };

            return personagem;
        }


        [Fact]
        public void Adicionar_Personagem_Erro_Nome_Repository()
        {
            Setup();

            _Repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.InserirPersonagem(auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.BadRequest,null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectNome(It.IsAny<PersonagemEntity>()), Times.Once());


        }


        [Fact]
        public void Adicionar_Personagem_Sucesso_Nome_Repository()
        {
            Setup();

            var resultado = _service.InserirPersonagem(auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Criado, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectNome(It.IsAny<PersonagemEntity>()), Times.Once());

            _Repository.Verify(x => x.InsertPersonagem(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Pegar_Personagem_Erro_Repository()
        {
            Setup();

            _Repository.Setup(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>())).Returns(new List<PersonagemEntity>() { });

            var resultado = _service.PegarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>()), Times.Once());

        }

        [Fact]
        public void Pegar_Personagem_Sucesso_Repository()
        {
            Setup();

            _Repository.Setup(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>())).Returns(new List<PersonagemEntity>() { new PersonagemEntity() { } });

            var resultado = _service.PegarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Encontrado, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>()), Times.Once());


        }

        [Fact]
        public void Deletar_Personagem_Erro_Repository()
        {
            Setup();

            var resultado = _service.DeletarPersonagem(Guid.NewGuid());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

        [Fact]
        public void Deletar_Personagem_Sucesso_Repository()
        {
            Setup();

            _Repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.DeletarPersonagem(Guid.NewGuid());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Ok, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Patch_Personagem_Erro_Id_Repository()
        {
            Setup();

            var resultado = _service.AtualizarPersonagem(Guid.Parse("1e58171f-6d8c-4c89-b387-d43971d86134"), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Patch_Personagem_Erro_Nome_Repository()
        {
            Setup();

             _Repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            _Repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.AtualizarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.BadRequest, null, "Ja temos um heroi cadastrado com esse nome!");

            Assert.Equal(resultadoEsperado.Mensagem, resultado.Mensagem);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

        [Fact]
        public void Patch_Personagem_Sucesso_Repository()
        {
            Setup();

            _Repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            //_Repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.AtualizarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Ok, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Put_Personagem_Erro_Id_Repository()
        {
            Setup();

            var resultado = _service.MudarPersonagem(Guid.Parse("1e58171f-6d8c-4c89-b387-d43971d86134"), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Put_Personagem_Erro_Nome_Repository()
        {
            Setup();

            _Repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            _Repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.MudarPersonagem(Guid.NewGuid(), auxilioTestPutDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.BadRequest, null, "Ja temos um heroi cadastrado com esse nome!");

            Assert.Equal(resultadoEsperado.Mensagem, resultado.Mensagem);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

        [Fact]
        public void Put_Personagem_Sucesso_Repository()
        {
            Setup();

            _Repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            //_Repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.MudarPersonagem(Guid.NewGuid(), auxilioTestPutDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Ok, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _Repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

    }
}
