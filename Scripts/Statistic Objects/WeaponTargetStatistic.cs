using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatisticSystem
{
    internal class WeaponTargetStatistic : Statistic
    {
      //target -> dictionary of targetPointPosition and rayHitPosition
        public Dictionary<TargetStatistic, Dictionary<Vector3,Vector3>> PathOfVisionInTarget = new Dictionary<TargetStatistic, Dictionary<Vector3,Vector3>>();
        public List<SlotStatistic> MagazineStatistics = new List<SlotStatistic>();
        public List<DateTime> ShotsWithoutMagazine = new List<DateTime>();

        public WeaponTargetStatistic()
        {
            MyType = this.GetType();
        }
    }
}