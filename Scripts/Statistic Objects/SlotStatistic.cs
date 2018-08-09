using System.Collections.Generic;
using UnityEngine;

namespace StatisticSystem
{
    class SlotStatistic : Statistic
    {
        public List<ShotStatistic> Shots;
        public Time TimeToReload;
        public int NumberOfBullets;
        public int NumberOfBulletRested;
    }
}