using DotaApi.Dtos;
using DotaApi.Entities;
using DotaApi.Enums;
using DotaApi.Repositories;
using DotaApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DotaApiTest.Service
{
    public class PersonagemServiceTest
    {
        private Mock<IPersonagemRepository> _repository;
        private PersonagemService _service;

        public PersonagemServiceTest()
        {
            _repository = new Mock<IPersonagemRepository>();
            _service = new PersonagemService(_repository.Object);
        }

        [Fact]
        public void DeletarPersonagem_Id_Deve_Estar_Nulo()
        {
            var id = default(Guid?);

            var result = _service.DeletarPersonagem(id);

            Assert.NotNull(result);
            Assert.Equal(SistemaEnum.Retorno.BadRequest, result.Status);
            Assert.Equal("Algo deu errado, tente novamente!", result.Mensagem);
            Assert.Null(result.Retorno);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Never);

            _repository.Verify(x => x.RemoverPersonagem(It.IsAny<PersonagemEntity>()), Times.Never);
        }

        [Fact]
        public void DeletarPersonagem_Nao_Deve_Encontrar_Personagem()
        {
            var id = Guid.NewGuid();

            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(default(PersonagemEntity));

            var result = _service.DeletarPersonagem(id);

            Assert.NotNull(result);
            Assert.Equal(SistemaEnum.Retorno.NotFound, result.Status);
            Assert.Equal("Item não encontrado!", result.Mensagem);
            Assert.Null(result.Retorno);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once);

            _repository.Verify(x => x.SelectId(It.Is<PersonagemEntity>(x => x.Id == id)), Times.Once);

            _repository.Verify(x => x.RemoverPersonagem(It.IsAny<PersonagemEntity>()), Times.Never);
        }

        [Fact]
        public void DeletarPersonagem_Deve_Deletar_Personagem()
        {
            var id = Guid.NewGuid();

            var entity = new PersonagemEntity(id)
            {
                Nome = "Nome"
            };

            var expectedMessage = $"{entity.Nome} foi deletado!";

            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(entity);

            var result = _service.DeletarPersonagem(id);

            Assert.NotNull(result);
            Assert.Equal(SistemaEnum.Retorno.Ok, result.Status);
            Assert.Equal(expectedMessage, result.Mensagem);
            Assert.Null(result.Retorno);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once);

            _repository.Verify(x => x.SelectId(It.Is<PersonagemEntity>(x => x.Id == id)), Times.Once);

            _repository.Verify(x => x.RemoverPersonagem(It.IsAny<PersonagemEntity>()), Times.Once);

            _repository.Verify(x => x.RemoverPersonagem(It.Is<PersonagemEntity>(x => x.Id == id)), Times.Once);
        }

        [Fact]
        public void DeletarPersonagem_Deve_Lancar_Exception()
        {
            var id = Guid.NewGuid();

            var entity = new PersonagemEntity(id)
            {
                Nome = "Nome"
            };

            var expectedMessage = $"{entity.Nome} foi deletado!";

            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(entity);

            var exceptionMessage = "Ocorreu uma excecao";

            _repository.Setup(x => x.RemoverPersonagem(It.IsAny<PersonagemEntity>())).Throws(() => new Exception(exceptionMessage));

            var result = _service.DeletarPersonagem(id);

            Assert.NotNull(result);
            Assert.Equal(SistemaEnum.Retorno.BadRequest, result.Status);
            Assert.Contains("Exceção gerada!:", result.Mensagem);
            Assert.Null(result.Retorno);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once);

            _repository.Verify(x => x.SelectId(It.Is<PersonagemEntity>(x => x.Id == id)), Times.Once);

            _repository.Verify(x => x.RemoverPersonagem(It.IsAny<PersonagemEntity>()), Times.Once);

            _repository.Verify(x => x.RemoverPersonagem(It.Is<PersonagemEntity>(x => x.Id == id)), Times.Once);
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
            _repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.InserirPersonagem(auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.BadRequest, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);
        }


        [Fact]
        public void Adicionar_Personagem_Sucesso_Nome_Repository()
        {
            var resultado = _service.InserirPersonagem(auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Criado, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectNome(It.IsAny<PersonagemEntity>()), Times.Once());

            _repository.Verify(x => x.InsertPersonagem(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Pegar_Personagem_Erro_Repository()
        {
            _repository.Setup(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>())).Returns(new List<PersonagemEntity>() { });

            var resultado = _service.PegarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>()), Times.Once());

        }

        [Fact]
        public void Pegar_Personagem_Sucesso_Repository()
        {
            _repository.Setup(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>())).Returns(new List<PersonagemEntity>() { new PersonagemEntity() { } });

            var resultado = _service.PegarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Encontrado, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectPersonagem(It.IsAny<PersonagemGetEntity>()), Times.Once());


        }

        [Fact]
        public void Deletar_Personagem_Erro_Repository()
        {
            var resultado = _service.DeletarPersonagem(Guid.NewGuid());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

        [Fact]
        public void Deletar_Personagem_Sucesso_Repository()
        {
            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.DeletarPersonagem(Guid.NewGuid());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Ok, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Patch_Personagem_Erro_Id_Repository()
        {
            var resultado = _service.AtualizarPersonagem(Guid.Parse("1e58171f-6d8c-4c89-b387-d43971d86134"), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Patch_Personagem_Erro_Nome_Repository()
        {
            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            _repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.AtualizarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.BadRequest, null, "Ja temos um heroi cadastrado com esse nome!");

            Assert.Equal(resultadoEsperado.Mensagem, resultado.Mensagem);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

        [Fact]
        public void Patch_Personagem_Sucesso_Repository()
        {
            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.AtualizarPersonagem(Guid.NewGuid(), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Ok, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Put_Personagem_Erro_Id_Repository()
        {
            var resultado = _service.MudarPersonagem(Guid.Parse("1e58171f-6d8c-4c89-b387-d43971d86134"), auxilioTestDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.NotFound, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }


        [Fact]
        public void Put_Personagem_Erro_Nome_Repository()
        {

            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            _repository.Setup(x => x.SelectNome(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.MudarPersonagem(Guid.NewGuid(), auxilioTestPutDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.BadRequest, null, "Ja temos um heroi cadastrado com esse nome!");

            Assert.Equal(resultadoEsperado.Mensagem, resultado.Mensagem);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

        [Fact]
        public void Put_Personagem_Sucesso_Repository()
        {
            _repository.Setup(x => x.SelectId(It.IsAny<PersonagemEntity>())).Returns(auxilioTestEntity());

            var resultado = _service.MudarPersonagem(Guid.NewGuid(), auxilioTestPutDto());

            var resultadoEsperado = new RetornoDto(SistemaEnum.Retorno.Ok, null);

            Assert.Equal(resultadoEsperado.Status, resultado.Status);

            _repository.Verify(x => x.SelectId(It.IsAny<PersonagemEntity>()), Times.Once());

        }

    }
}
