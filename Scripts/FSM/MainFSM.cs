using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StatisticSystem.FSM
{
    class MainFSM : MonoBehaviour //need to be singleton
    {
        private readonly List<IEventStatistic> _eventRoutines = new List<IEventStatistic>();

        private readonly List<Statistic> _statistics = new List<Statistic>();
        public bool RemoveRoutine(Guid idRoutine)
        {
            var eventStatistics = _eventRoutines.FirstOrDefault(statistic => statistic.GetId() == idRoutine);

            if (eventStatistics != null)
            {
                StopCoroutine(eventStatistics.EndRoutine());
                var newStatistic = eventStatistics.GetStatistic();
                _statistics.Add(newStatistic);
            }
            else
            {
                Debug.LogWarning("We got a problem here!- FALA PRO HYRAM!!");
            }

            return true;
        }

        public void AddRoutine(IEventStatistic newRoutine)
        {
             _eventRoutines.Add(newRoutine);
             StartCoroutine(newRoutine.StartRoutine());
        }

        public void EndStatistic()
        {
            //StopAllRoutines
            _eventRoutines.All(e => RemoveRoutine(e.GetId()));

            foreach (var statistic in _statistics)
            {

            }

        }
    }
}
