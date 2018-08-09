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

        public void RemoveRoutine(Guid idRoutine)
        {
            var eventStatistics = _eventRoutines.FirstOrDefault(statistic => statistic.GetId() == idRoutine);
            if (eventStatistics != null) StopCoroutine(eventStatistics.EndRoutine());
        }

        public void AddRoutine(IEventStatistic newRoutine)
        {
             _eventRoutines.Add(newRoutine);
             StartCoroutine(newRoutine.StartRoutine());
        }
        
    }
}
