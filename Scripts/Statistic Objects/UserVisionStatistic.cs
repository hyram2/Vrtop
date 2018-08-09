using System;
using StatisticSystem.Enum;
using UnityEngine;

namespace StatisticSystem
{
    class UserVisionStatistic : Statistic
    {
        public DateTime InitialTimeInFov = DateTime.Now;
        public DateTime InitialTimeTargetInFocus;
        public DateTime EndTimeOfTargetInFocus;
        public ETargetType TypeOfTarget;
    }
}