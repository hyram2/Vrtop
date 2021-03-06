﻿using System;
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
            return null;
        }

        public virtual IEnumerator EndRoutine()
        {
            return _myRoutine;
        }

        public Statistic GetStatistic()
        {
            return _myStatistic;
        }
    }
}