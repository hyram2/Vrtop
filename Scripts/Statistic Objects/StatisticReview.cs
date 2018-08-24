using System.Collections.Generic;

namespace StatisticSystem
{
    public class StatisticReview
    {
        public string Name;

        public int[] IntervaloEntreDisparosTodos;
        public float IntervaloEntreDisparosMedia;

        public decimal PercentualDeAcertos;

        public float RaioDePrecisaoMedia;

        public float[] RaiosDePrecisaoMediaPorAlvo;

        public int[] TemposDeReacao;
        public int[] TemposDeAquisicao;

        public float PercentualTempoDeReacaoMedia;
        public float PercentualTempoDeAquisicaoMedia;


        public int QuantidadeDeRecargasTaticas;
        public int QuantidadeDeRecargasDeEmergencia;

        public int ErroTirosComArmaApenasAlimentada;
        public int ErroTirosComArmaAberta;

        public int NumeroDeAbates;

        public StatisticReview(string name, int[] intervaloEntreDisparosTodos, float intervaloEntreDisparosMedia, decimal percentualDeAcertos, float raioDePrecisaoMedia, float[] raiosDePrecisaoMediaPorAlvo, int[] temposDeReacao, int[] temposDeAquisicao, float percentualTempoDeReacaoMedia, float percentualTempoDeAquisicaoMedia, int quantidadeDeRecargasTaticas, int quantidadeDeRecargasDeEmergencia, int erroTirosComArmaApenasAlimentada, int erroTirosComArmaAberta, int numeroDeAbates)
        {
            Name = name;
            IntervaloEntreDisparosTodos = intervaloEntreDisparosTodos;
            IntervaloEntreDisparosMedia = intervaloEntreDisparosMedia;
            PercentualDeAcertos = percentualDeAcertos;
            RaioDePrecisaoMedia = raioDePrecisaoMedia;
            RaiosDePrecisaoMediaPorAlvo = raiosDePrecisaoMediaPorAlvo;
            TemposDeReacao = temposDeReacao;
            TemposDeAquisicao = temposDeAquisicao;
            PercentualTempoDeReacaoMedia = percentualTempoDeReacaoMedia;
            PercentualTempoDeAquisicaoMedia = percentualTempoDeAquisicaoMedia;
            QuantidadeDeRecargasTaticas = quantidadeDeRecargasTaticas;
            QuantidadeDeRecargasDeEmergencia = quantidadeDeRecargasDeEmergencia;
            ErroTirosComArmaApenasAlimentada = erroTirosComArmaApenasAlimentada;
            ErroTirosComArmaAberta = erroTirosComArmaAberta;
            NumeroDeAbates = numeroDeAbates;
        }
    }
}