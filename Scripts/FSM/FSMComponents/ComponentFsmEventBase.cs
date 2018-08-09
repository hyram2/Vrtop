using System;
using System.Collections;

namespace StatisticSystem.FSM.FSMComponents
{
    class ComponentFsmEventBase<T> : IEventStatistic where T : Statistic
    {
        internal T _myStatistic;
        internal IEnumerator _myRoutine;

        public Guid GetId()
        {
            return _myStatistic.Id;
        }

        public IEnumerator StartRoutine()
        {
            _myRoutine =  Routine();
            return _myRoutine;
        }

        public virtual IEnumerator Routine()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerator EndRoutine()
        {
            throw new NotImplementedException();
        }
    }
}