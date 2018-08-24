using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
using StatisticSystem.Enum;
using UnityEngine;
using VRInteraction;

namespace StatisticSystem.FSM.FSMComponents
{
    //VRGunHandler VRGunHandlerRef

    [RequireComponent(typeof(VRGunHandlerRef))]
    class WeaponTargetComponentFsmEvent : MonoComponentFsmEventBase<WeaponTargetStatistic>
    {
        public float SecondsBetweenRayCast = 0.1f;
        private Guid? _lastTargetId;
        [SerializeField]
        private System.Object _gunRef;

        private AudioSource _soundSource;

        public bool heldBy;
        private Guid? _currentMagazineStatisticId;

        public new void Start()
        {

            _myStatistic = new WeaponTargetStatistic();
            base.Start();
            _gunRef = GetComponent<VRGunHandlerRef>();
            if (_gunRef.gunHandler.fireRate < SecondsBetweenRayCast)
                SecondsBetweenRayCast = _gunRef.gunHandler.fireRate;

            if (_soundSource == null) _soundSource = gameObject.AddComponent<AudioSource>();

        }

        public override IEnumerator Routine()
        {
            while (true)
            {
                yield return new WaitForSeconds(SecondsBetweenRayCast);

                if (!heldBy) continue;
                //Todo:Refatorar isso

                #region VisionStatistic
                
                var layerMask = 1 << 8;
                layerMask = ~layerMask;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    
                    if (hit.transform.tag == "Enemy" || hit.transform.tag == "Hostage")
                    {
                        var hitTarget = hit.transform.GetComponent<TargetComponentFsmEvent>();

                        //caso nao exista ref
                        //TODO:Testar
                        if (_myStatistic.PathOfVisionInTarget.All(pair => pair.Key.Id != hitTarget.GetId()))
                        {
                            _myStatistic.PathOfVisionInTarget.Add(hitTarget.Get(), new Dictionary<Vector3,Vector3> { { hitTarget.PointTarget[0].transform.position, hit.point } });
                            //TODO:Testar
                            hitTarget._myStatistic.InitialTimeTargetInFocus=DateTime.Now; 
                        }
                        else
                        {
                            //TODO:Testar
                            var targetStatistic = _myStatistic.PathOfVisionInTarget.FirstOrDefault(pair => pair.Key.Id == hitTarget.GetId());
                            targetStatistic.Value.Add(hitTarget.PointTarget[0].transform.position, hit.point);
                        }
                        _lastTargetId = hitTarget.GetId();
                    }
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                   
                }
                else
                {
                    if (_lastTargetId != null)
                    {
                        var lastTargetStatistic = _myStatistic.PathOfVisionInTarget
                            .FirstOrDefault(pair => pair.Key.Id == _lastTargetId.Value).Key;
                        lastTargetStatistic.EndTimeOfTargetInFocus = DateTime.Now;
                    }
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
              
                }

                #endregion


                #region Magazine
                ////motivo: nao Mexer na classe do VRInteraction

                if (_soundSource.clip == _gunRef.gunHandler.loadMagazine)
                {

                    var magazineStatistic = _myStatistic.MagazineStatistics.FirstOrDefault(m =>
                    {
                        return _currentMagazineStatisticId != null && m.Id == _currentMagazineStatisticId.Value;
                    });

                    if (_soundSource.time < SecondsBetweenRayCast + 0.1)
                    {
                        magazineStatistic = new SlotStatistic();
                        //get new Magazine
                        magazineStatistic.TimeToLoad = DateTime.Now;
                        _currentMagazineStatisticId = magazineStatistic.Id;
                        _myStatistic.MagazineStatistics.Add(magazineStatistic);
                        var newMagazine = _gunRef.gunHandler.currentMagazine;
                        magazineStatistic.StartNumberOfBullets = newMagazine.bulletCount;

                    }

                }

                if (_soundSource.clip == _gunRef.gunHandler.unloadMagazine)
                {
                    var magazineStatistic = _myStatistic.MagazineStatistics
                        .FirstOrDefault(m => _currentMagazineStatisticId != null && m.Id == _currentMagazineStatisticId.Value);
                    if (_soundSource.time < SecondsBetweenRayCast + 0.1)
                    {
                        if (magazineStatistic != null)
                        {
                            magazineStatistic.TimeToReload = DateTime.Now;
                            _currentMagazineStatisticId = null;
                        }
                    }
                }

                if (_soundSource.clip == _gunRef.gunHandler.dryFireSound)
                {
                    var magazineStatistic = _myStatistic.MagazineStatistics
                        .FirstOrDefault(m =>
                            _currentMagazineStatisticId != null && m.Id == _currentMagazineStatisticId.Value);
                    if (_soundSource.time < SecondsBetweenRayCast + 0.1)
                    {
                        if (magazineStatistic != null)
                        {
                            magazineStatistic.TimeOpenedTrigger.Add(DateTime.Now);
                        }
                        else
                        {
                            _myStatistic.ShotsWithoutMagazine.Add(DateTime.Now);
                        }
                    }
                }

                if (_soundSource.clip == _gunRef.gunHandler.fireSound)
                {
                    var magazineStatistic = _myStatistic.MagazineStatistics
                        .FirstOrDefault(m =>
                            _currentMagazineStatisticId != null && m.Id == _currentMagazineStatisticId.Value);
                    if (_soundSource.time < SecondsBetweenRayCast + 0.1)
                    {
                        var magazine = _gunRef.gunHandler.currentMagazine;
                        if (magazineStatistic == null)
                        {
                            magazineStatistic = new SlotStatistic();
                            magazineStatistic.StartNumberOfBullets = magazine.bulletCount + _gunRef.gunHandler.bulletsPerShot;
                        }
                        magazineStatistic.NumberOfBullets = magazine.bulletCount;
                    }
                }

                #endregion
            }
            // ReSharper disable once IteratorNeverReturns
        }

    }
}