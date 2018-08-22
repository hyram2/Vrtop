﻿using System;
using System.Collections.Generic;
using StatisticSystem.Enum;

namespace StatisticSystem
{
    internal class TargetStatistic : Statistic
    {
        public List<ShotStatistic> BulletReceiverShotStatistics = new List<ShotStatistic>();
        public DateTime InitialTimeInFov = DateTime.Now;
        public DateTime InitialTimeTargetInFocus;
        public DateTime EndTimeOfTargetInFocus;
        public ETargetType TypeOfTarget;

        public TargetStatistic()
        {
            MyType = this.GetType();
        }
    }
}