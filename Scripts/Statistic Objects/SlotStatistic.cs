using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatisticSystem
{
    // magazine - bulletReceiver
    internal class SlotStatistic : Statistic
    {
        public DateTime TimeToUnload;
        public DateTime TimeToLoad;
        public int StartNumberOfBullets;
        public int NumberOfBullets;
        public int NumberOfBulletRested;
        public bool IsAutomaticLoad;
        public List<DateTime> TimeOpenedTrigger = new List<DateTime>();

        public SlotStatistic()
        {
            MyType = this.GetType();
        }
        public SlotStatistic(SlotStatistic oldSlotStatistic)
        {
            Id = oldSlotStatistic.Id;
            TimeToUnload = oldSlotStatistic.TimeToUnload;
            StartNumberOfBullets = oldSlotStatistic.StartNumberOfBullets;
            NumberOfBullets = oldSlotStatistic.NumberOfBullets;
            NumberOfBulletRested = oldSlotStatistic.NumberOfBulletRested;
            IsAutomaticLoad = oldSlotStatistic.IsAutomaticLoad;
            TimeOpenedTrigger = oldSlotStatistic.TimeOpenedTrigger;
        }
    }
}