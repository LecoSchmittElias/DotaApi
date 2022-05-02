﻿using DotaApi.Dtos;
using DotaApi.Enums;

namespace DotaApi.Services
{
    public interface IPersonagemService
    {
        public RetornoDto InserirPersonagem(EntradaDto? personagem);

        public RetornoDto PegarPersonagem(Guid? id, EntradaDto? personagem);

        public RetornoDto DeletarPersonagem(Guid? id);

        public RetornoDto MudarPersonagem(Guid? id, EntradaDto? personagem);
    }
}