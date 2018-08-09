using System.Collections.Generic;

namespace StatisticSystem
{
    class UserStatistics : Statistic
    {
        public int NumberOfKills;
        public List<SlotStatistic> SlotStatistics;
        public List<UserVisionStatistic> UserVisionStatistics;
        public List<WeaponTargetStatistic> WeaponTargetStatistics;
    }
}