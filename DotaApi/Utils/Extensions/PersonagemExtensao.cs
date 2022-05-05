using DotaApi.Dtos;
using DotaApi.Entities;

namespace DotaApi.Utils.Extensions
{
    public static class PersonagemExtensao
    {
        public static (string, bool) VerificarDadosEspecificosNulos(this EntradaDto dadosVerificar)
        {

            if (dadosVerificar == null) return ("Todos os dados estão nulos", false);

            if (string.IsNullOrEmpty(dadosVerificar.Nome)) return ("Nome está nulo ou vazio", false);

            if (string.IsNullOrEmpty(dadosVerificar.Funcao)) return ("Funcao está vazia ou nula", false);

            if (string.IsNullOrEmpty(dadosVerificar.EstiloAtaque.ToString())) return ("Estilo está nulo", false);

            if (string.IsNullOrEmpty(dadosVerificar.AtributoPrimario.ToString())) return ("Atributo primario está nulo", false);

            return ("", true);
        }


        public static (string, bool) VerificarDadosTipo(this EntradaDto dadosVerificar)
        {
            int[] auxiliar3 = new int[3] { 1, 2, 3 };
            int[] auxiliar2 = new int[2] { 1, 2 };

            if (!Array.Exists(auxiliar2, x => x == (int)dadosVerificar.EstiloAtaque)) return ("Estilo invalido", false);

            if (!Array.Exists(auxiliar3, x => x == (int)dadosVerificar.AtributoPrimario)) return ("Atributo primario invalido", false);

            if (dadosVerificar.Dificuldade != null)
            {
                if (!Array.Exists(auxiliar3, x => x == (int)dadosVerificar.Dificuldade)) return ("Dificuldade invalido", false);
            }

            if (dadosVerificar.AtributoSecundario != null)
            {
                if (!Array.Exists(auxiliar3, x => x == (int)dadosVerificar.AtributoSecundario)) return ("Atributo segundario invalido", false);
            }


            return ("", true);
        }

        public static (string, bool) VerificarDadosNulos(this EntradaDto? dadosVerificar)
        {
            if (string.IsNullOrEmpty(dadosVerificar.Nome))
            {
                if (string.IsNullOrEmpty(dadosVerificar.Funcao))
                {
                    if (dadosVerificar.EstiloAtaque == null)
                    {
                        if (dadosVerificar.AtributoPrimario == null)
                        {
                            if (dadosVerificar.AtributoSecundario == null)
                            {
                                if (dadosVerificar.Dificuldade == null)
                                {
                                    if (string.IsNullOrEmpty(dadosVerificar.Imagem)) return ("Dados encontram-se nulos!", false);
                                }
                            }
                        }
                    }
                }
            }

            return ("", true);
        }


        public static (string, bool) VerificarAtualizacaoTotal(this PersonagemEntity personagemEntrada, PersonagemEntity personagemEncontrado)
        {
            if (personagemEntrada.Nome.ToLower() == personagemEncontrado.Nome.ToLower()) return ("Nome está igual ao anterior", false);

            if (personagemEntrada.Funcao.ToLower() == personagemEncontrado.Funcao.ToLower()) return ("Funcao está igual ao anterior", false);

            if (personagemEntrada.Dificuldade == personagemEncontrado.Dificuldade) return ("dificuldade está igual ao anterior", false);

            if (personagemEntrada.EstiloAtaque == personagemEncontrado.EstiloAtaque) return ("EstiloAtaque está igual ao anterior", false);

            if (personagemEntrada.AtributoPrimario == personagemEncontrado.AtributoPrimario) return ("AtributoPrimario está igual ao anterior", false);

            if (personagemEntrada.AtributoSecundario == personagemEncontrado.AtributoSecundario) return ("AtributoSecundario está igual ao anterior", false);

            if (personagemEntrada.Imagem.ToLower() == personagemEncontrado.Imagem.ToLower()) return ("Imagem está igual ao anterior", false);

            return ("", true);

        }


        public static (string, bool) VerificarAtualizacaoParcial(this PersonagemEntity personagemEntrada, PersonagemEntity personagemEncontrado)
        {
            if (personagemEntrada.Nome.ToLower() == personagemEncontrado.Nome.ToLower())
            {
                if (personagemEntrada.Funcao.ToLower() == personagemEncontrado.Funcao.ToLower())
                {
                    if (personagemEntrada.Dificuldade == personagemEncontrado.Dificuldade)
                    {
                        if (personagemEntrada.EstiloAtaque == personagemEncontrado.EstiloAtaque)
                        {
                            if (personagemEntrada.AtributoPrimario == personagemEncontrado.AtributoPrimario)
                            {
                                if (personagemEntrada.AtributoSecundario == personagemEncontrado.AtributoSecundario)
                                {
                                    if (personagemEntrada.Imagem.ToLower() == personagemEncontrado.Imagem.ToLower())
                                    {
                                        return ("Todos os itens são iguais ao anterior!", false);
                                    }
                                }
                            }
                        }
                    }

                }
            }

            if (personagemEntrada.Nome.ToLower() != personagemEncontrado.Nome.ToLower())
            {
                if (personagemEntrada.Funcao.ToLower() != personagemEncontrado.Funcao.ToLower())
                {
                    if (personagemEntrada.Dificuldade != personagemEncontrado.Dificuldade)
                    {
                        if (personagemEntrada.EstiloAtaque != personagemEncontrado.EstiloAtaque)
                        {
                            if (personagemEntrada.AtributoPrimario != personagemEncontrado.AtributoPrimario)
                            {
                                if (personagemEntrada.AtributoSecundario != personagemEncontrado.AtributoSecundario)
                                {
                                    if (personagemEntrada.Imagem.ToLower() != personagemEncontrado.Imagem.ToLower())
                                    {
                                        return ("Todos os itens são diferentes ao anterior!", false);
                                    }
                                }
                            }
                        }
                    }

                }
            }

            return ("", true);

        }
    }
}
