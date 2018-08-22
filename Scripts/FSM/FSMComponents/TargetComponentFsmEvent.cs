using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticSystem;
using StatisticSystem.Enum;
using UnityEngine;
using VRInteraction;

namespace StatisticSystem.FSM.FSMComponents
{
    class TargetComponentFsmEvent : MonoComponentFsmEventBase<TargetStatistic>
    {
        //referente ao pontos centrais dos alvos de cada target
        public GameObject[] PointTarget;

        public ETargetType TargetType;

        public new void Start()
        {
            base.Start();
            _myStatistic.TypeOfTarget = TargetType;
        }

        //TODO:TESTAR!! Pode dar o erro obvio em que perde-se a referencia da instancia quando destroi o objeto
        public TargetStatistic Get()
        {
            return _myStatistic;
        }

        public new void OnDestroy()
        {
            UserStatistic.Instance.Statistics.NumberOfKills++;
            base.OnDestroy();
        }

        public void OnRenderObject()
        {
            _myStatistic.InitialTimeInFov = DateTime.Now;
        }
        public void Damage(DamageInfo info)
        {
            try{
                var shotStatistic = new ShotStatistic
                {
                    PointOfCollision = info.hitInfo.point,
                    PointTargetPosition = PointTarget.FirstOrDefault().transform.position,//try catch é por causa disso, pode ser alguém esqueça do ponto
                    TimeOfShot = DateTime.Now,
                    FailShot = _myStatistic.TypeOfTarget == ETargetType.Hostage
                        ? EFailShot.HitHostage
                        : (EFailShot?) null
                    

                };
                _myStatistic.BulletReceiverShotStatistics.Add(shotStatistic);
                
            }catch(Exception e) {
                Debug.LogWarning(e.Message);
            }

        }

    }
}
