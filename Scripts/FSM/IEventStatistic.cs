using System;
using System.Collections;

namespace StatisticSystem.FSM
{
    interface IEventStatistic
    {
        Guid GetId();
        IEnumerator StartRoutine();
        IEnumerator Routine();
        IEnumerator EndRoutine();
        Statistic GetStatistic();
    }
}
