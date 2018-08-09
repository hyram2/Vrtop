using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticSystem;
using UnityEngine;

namespace StatisticSystem.FSM.FSMComponents
{
    class EnemyComponentFsmEvent :  ComponentFsmEventBase<TargetStatistic>
    {
        public TargetStatistic Get()
        {
            return null;
        }

        public override IEnumerator EndRoutine()
        {
            //do something...

            return base.EndRoutine();

        }
    }
}
