using System;
using StatisticSystem.Enum;
using UnityEngine;

namespace StatisticSystem
{
    internal class ShotStatistic : Statistic
    {
       public DateTime TimeOfShot;
       public Vector3 PointOfCollision;//x+y
       public Vector3 PointTargetPosition;//x+y
       public EFailShot? FailShot;

        public ShotStatistic()
        {
            MyType = this.GetType();
        }
        public ShotStatistic(ShotStatistic oldShotStatistic)
        {
            Id = oldShotStatistic.Id;
            TimeOfShot = oldShotStatistic.TimeOfShot;
            PointOfCollision = oldShotStatistic.PointOfCollision;
            PointTargetPosition = oldShotStatistic.PointTargetPosition;
            FailShot = oldShotStatistic.FailShot;
        }
        
        
    }
}