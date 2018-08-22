using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatisticSystem
{
    // magazine - bulletReceiver
    internal class SlotStatistic : Statistic
    {
        public DateTime TimeToReload;
        public int StartNumberOfBullets;
        public int NumberOfBullets;
        public int NumberOfBulletRested;
        public bool IsAutomaticLoad;
        public List<DateTime> TimeOpenedTrigger = new List<DateTime>();

        public SlotStatistic()
        {
            MyType = this.GetType();
        }
    }
}