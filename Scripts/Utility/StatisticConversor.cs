using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Security.Cryptography;
using NUnit.Framework.Api;
using StatisticSystem;
using StatisticSystem.Enum;
using StatisticSystem.FSM.FSMComponents;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    class StatisticConversor
    {
        public List<WeaponTargetStatistic> WeaponTargetStatistics = new List<WeaponTargetStatistic>();
        public List<TargetStatistic> TargetStatistics = new List<TargetStatistic>();
        public List<SlotStatistic> SlotStatistics = new List<SlotStatistic>();
        public List<ShotStatistic> ShotStatistics = new List<ShotStatistic>();
        public UserStatistics UserStatistics = new UserStatistics();
        private StatisticReview review;

        public string Name;

        public List<int> IntervaloEntreDisparosTodos = new List<int>();
        public float IntervaloEntreDisparosMedia;

        public decimal PercentualDeAcertos;

        public float RaioDePrecisaoMedia;

        public List<float> RaiosDePrecisaoMediaPorAlvo = new List<float>();

        public List<int> TemposDeReacao = new List<int>();
        public List<int> TemposDeAquisicao = new List<int>();

        public float PercentualTempoDeReacaoMedia;
        public float PercentualTempoDeAquisicaoMedia;


        public int QuantidadeDeRecargasTaticas =0;
        public int QuantidadeDeRecargasDeEmergencia =0;

        public int ErroTirosComArmaApenasAlimentada = 0;
        public int ErroTirosComArmaAberta = 0;

        public int NumeroDeAbates = 0;

        public void SaveStatisticObj(Statistic statistic)
        {
            switch (statistic.MyType.Name)
            {
                case "WeaponTargetStatistic":
                    WeaponTargetStatistics.Add(SaveWeaponTargetStatistic(statistic));
                    return;
                case "TargetStatistic":
                    TargetStatistics.Add(SaveTargetStatistic(statistic));
                    return;
                case "SlotStatistic":
                    SlotStatistics.Add(SaveSlotStatistic(statistic));
                    return;
                case "ShotStatistic":
                    ShotStatistics.Add(SaveShotStatistic(statistic));
                    return;
            }
        }

        #region Tradutores
        
        public WeaponTargetStatistic SaveWeaponTargetStatistic(Statistic s)
        {
            return new WeaponTargetStatistic((WeaponTargetStatistic)s);
        }

        public TargetStatistic SaveTargetStatistic(Statistic s)
        {
            return new TargetStatistic((TargetStatistic)s);
        }

        public SlotStatistic SaveSlotStatistic(Statistic s)
        {
            return new SlotStatistic((SlotStatistic)s);
        }

        public ShotStatistic SaveShotStatistic(Statistic s)
        {
            return new ShotStatistic((ShotStatistic)s);
        }

        #endregion

        #region Análises

        public string GetAnalizes()
        {

            #region Organizando dados para humanos lerem
            
            foreach (var w in WeaponTargetStatistics)
            {
                w.MagazineStatistics = w.MagazineStatistics.OrderBy(m => m.TimeToLoad).ToList();
                SlotStatistics.AddRange(w.MagazineStatistics);
            }

            TargetStatistics = TargetStatistics.OrderBy(t => t.InitialTimeInFov).ToList();
            foreach (var t in TargetStatistics)
            {
                t.BulletReceiverShotStatistics = t.BulletReceiverShotStatistics.OrderBy(b => b.TimeOfShot).ToList();
                ShotStatistics.AddRange(t.BulletReceiverShotStatistics);
            }

            UserStatistics = UserStatistic.Instance.Statistics;
            Name = UserStatistic.Instance.Name;

            #endregion

            foreach (var target in TargetStatistics)
            {
              var shots = target.BulletReceiverShotStatistics.Select(s => s.TimeOfShot).ToList();
              var diferencasMilisegundos = new List<int>();
              DateTime? tempDate = null;
                foreach (var shotTime in shots)
                {
                    if (tempDate.HasValue)
                    {
                        var s = (shotTime - tempDate.Value).Milliseconds;
                        diferencasMilisegundos.Add(s);
                    }
                    tempDate = shotTime;
                }
                IntervaloEntreDisparosTodos.AddRange(diferencasMilisegundos);
            }

            int count = 0;
            foreach (var intervalo in IntervaloEntreDisparosTodos)
            {
                IntervaloEntreDisparosMedia += intervalo;
                count++;
            }
            IntervaloEntreDisparosMedia = IntervaloEntreDisparosMedia / count;


            //  a=todas(bulletsInicial-bulletsfinal) ---100   
            //  b=(alvos-inimigos.quantidadedebalas)  --- x
            // mediadeacertos =  (bX100)/a
            int NumeroDeBalasAtiradas = 0;



            foreach (var w in WeaponTargetStatistics)
            {
               ErroTirosComArmaApenasAlimentada += w.ShotsWithoutMagazine.Count;
               w.MagazineStatistics.ForEach(m => ErroTirosComArmaAberta += m.TimeOpenedTrigger.Count);
               QuantidadeDeRecargasDeEmergencia += w.MagazineStatistics.Select(m => m.NumberOfBullets < 5).ToList().Count;
               QuantidadeDeRecargasTaticas += w.MagazineStatistics.Select(m => m.NumberOfBullets >= 5).ToList().Count;

                w.MagazineStatistics.Select(b => b.StartNumberOfBullets - b.NumberOfBullets).ToList().ForEach(c=> NumeroDeBalasAtiradas += c);
            }
            decimal NumeroDeBalasQueAtingiramUmInimigo = 0;
            decimal NumeroDeBalasQueAtingiramUmRefem = 0;

            List<float> RaioDePrecisao = new List<float>();
            

            foreach (var t in TargetStatistics)
            {
               
                if (t.TypeOfTarget == ETargetType.Enemy)
                {

                    NumeroDeBalasQueAtingiramUmInimigo += t.BulletReceiverShotStatistics.Count;
                    t.BulletReceiverShotStatistics.Select(b =>
                        Vector3.Distance(b.PointOfCollision, b.PointTargetPosition)).ToList().ForEach(d=> RaioDePrecisao.Add(d));
                }
                else
                {
                    NumeroDeBalasQueAtingiramUmRefem += t.BulletReceiverShotStatistics.Count;
                }

                // (shotTime - tempDate.Value).Milliseconds;
                var tempoDeReacao = (t.BulletReceiverShotStatistics.Select(c => c.TimeOfShot).FirstOrDefault()- t.InitialTimeTargetInFocus).Milliseconds;
                TemposDeReacao.Add(tempoDeReacao);
                var tempoDeAquisicao = (t.InitialTimeInFov - t.InitialTimeTargetInFocus).Milliseconds;
                TemposDeAquisicao.Add(tempoDeAquisicao);
            }
            PercentualDeAcertos = (NumeroDeBalasQueAtingiramUmInimigo * 100) / NumeroDeBalasAtiradas;

            float precisaototal = 0f;
            RaioDePrecisao.ForEach(d=>precisaototal+= d);
            RaioDePrecisaoMedia = precisaototal / RaioDePrecisao.Count;
            RaiosDePrecisaoMediaPorAlvo = RaioDePrecisao;

            var reacaototal = 0f;
            TemposDeReacao.ForEach(d => reacaototal+= d);
            var reacaoMedia = reacaototal/ TemposDeReacao.Count;
            PercentualTempoDeReacaoMedia = reacaoMedia;

            var aquisicaototal = 0f;
            TemposDeAquisicao.ForEach(d => aquisicaototal+= d);
            var aquisicaoMedia = aquisicaototal/ TemposDeAquisicao.Count;
            PercentualTempoDeAquisicaoMedia = aquisicaoMedia;


            NumeroDeAbates = UserStatistics.NumberOfKills;


            review = new StatisticReview(Name,IntervaloEntreDisparosTodos.ToArray(),IntervaloEntreDisparosMedia,PercentualDeAcertos,RaioDePrecisaoMedia,RaiosDePrecisaoMediaPorAlvo.ToArray(),TemposDeReacao.ToArray(),TemposDeAquisicao.ToArray(),PercentualTempoDeReacaoMedia,PercentualTempoDeAquisicaoMedia,QuantidadeDeRecargasTaticas,QuantidadeDeRecargasDeEmergencia,ErroTirosComArmaApenasAlimentada,ErroTirosComArmaAberta,NumeroDeAbates);

            return review.ToString();
        }



        #endregion
    }
}
