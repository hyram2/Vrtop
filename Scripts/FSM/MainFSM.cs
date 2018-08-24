using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;
namespace StatisticSystem.FSM
{
    class MainFSM : MonoBehaviour //need to be singleton
    {
        public Text textoDeEstatisticas;
        public Button PegarEstatisticas;

        private readonly List<IEventStatistic> _eventRoutines = new List<IEventStatistic>();
        private readonly StatisticConversor _statisticConversor = new StatisticConversor();

        void Start()
        {
            PegarEstatisticas.onClick.AddListener(() =>
            {
               FindObjectOfType<MainFSM>().EndStatistic();
            });
        }


        public bool RemoveRoutine(Guid idRoutine)
        {
            var eventStatistics = _eventRoutines.FirstOrDefault(statistic => statistic.GetId() == idRoutine);

            if (eventStatistics != null)
            {
                StopCoroutine(eventStatistics.EndRoutine());
                var newStatistic = eventStatistics.GetStatistic();
                _statisticConversor.SaveStatisticObj(newStatistic);
            }
            else
            {
                Debug.LogWarning("We got a problem here!- FALA PRO HYRAM!! Solução DTO");
            }

            return true;
        }

        public void AddRoutine(IEventStatistic newRoutine)
        {
            print(newRoutine.GetId());
            print(newRoutine.GetType().Name);
             _eventRoutines.Add(newRoutine);
             StartCoroutine(newRoutine.StartRoutine());
        }

        public void EndStatistic()
        {
            //StopAllRoutines
            _eventRoutines.All(e => RemoveRoutine(e.GetId()));
            textoDeEstatisticas.text = _statisticConversor.GetAnalizes();
        }
    }
}
