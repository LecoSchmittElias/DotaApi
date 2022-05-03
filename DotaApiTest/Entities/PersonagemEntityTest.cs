using DotaApi.Dtos;
using DotaApi.Entities;
using DotaApi.Repositories;
using DotaApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotaApiTest.Entity
{
    public class PersonagemEntityTest
    {

        [Fact]
        public void Test_Entidade()
        {
            var _entity = new PersonagemEntity();

            var id = Guid.NewGuid();
            var atributoPrimario = DotaApi.Enums.PersonagemEnum.Atributo.Forca;
            var atributoSecundario = DotaApi.Enums.PersonagemEnum.Atributo.Agilidade;
            var estiloAtaque = DotaApi.Enums.PersonagemEnum.Estilo.Melee;
            var dificuldade = DotaApi.Enums.PersonagemEnum.Dificuldade.Medio;
            var nome = "Tusk";
            var imagem = "img.src";
            var funcao = "Carry";

            _entity.Id = id;
            _entity.AtributoPrimario = atributoPrimario;
            _entity.AtributoSecundario = atributoSecundario;
            _entity.EstiloAtaque = estiloAtaque;
            _entity.Dificuldade = dificuldade;
            _entity.Nome = nome;
            _entity.Imagem = imagem;
            _entity.Funcao = funcao;

            Assert.Equal(id, _entity.Id);
            Assert.Equal(atributoPrimario, _entity.AtributoPrimario);
            Assert.Equal(atributoSecundario, _entity.AtributoSecundario);
            Assert.Equal(estiloAtaque, _entity.EstiloAtaque);
            Assert.Equal(dificuldade, _entity.Dificuldade);
            Assert.Equal(nome, _entity.Nome);
            Assert.Equal(imagem, _entity.Imagem);
            Assert.Equal(funcao, _entity.Funcao);
        }
    }
}
  