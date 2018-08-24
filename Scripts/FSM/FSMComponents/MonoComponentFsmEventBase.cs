using System;
using System.Collections;
using UnityEngine;

namespace StatisticSystem.FSM.FSMComponents
{
    class MonoComponentFsmEventBase<T> : MonoBehaviour, IEventStatistic where T : Statistic
    {
        [NonSerialized]
        internal T _myStatistic;
        [NonSerialized]
        internal IEnumerator _myRoutine;
        [SerializeField]
        private MainFSM _mainFsm;


        public void Start()
        {
            _mainFsm = FindObjectOfType<MainFSM>();
            _mainFsm.AddRoutine(this);
        }

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
            yield return new WaitForSeconds(0.1f);
        }

        public virtual IEnumerator EndRoutine()
        {
            return _myRoutine;
        }

        public Statistic GetStatistic()
        {
            return _myStatistic;
        }

        public void OnDestroy()
        {
            _mainFsm.RemoveRoutine(GetId());
        }
    }
}