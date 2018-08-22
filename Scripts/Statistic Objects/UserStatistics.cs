using System.Collections.Generic;

namespace StatisticSystem
{
    class UserStatistics : Statistic
    {
        public int NumberOfKills;

        public UserStatistics()
        {
            MyType = this.GetType();
        }
    }

}