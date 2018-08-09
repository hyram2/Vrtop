using System.Collections.Generic;
using UnityEngine;

namespace StatisticSystem
{
    class WeaponTargetStatistic : Statistic
    {
      public Dictionary<TargetStatistic, List<Vector2>> PathOfVisionInTarget;
    }
}