using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Devdog.LosPro
{
    [RequireInterface(typeof(IObserver))]
    public class ObserverAggroBehaviour : MonoBehaviour, IObserverCallbacks, IListenerCallbacks, IObserverAggro
    {
        public Vector3 startLocation { get; protected set; }
        public float currentAggro { get; set; }
        public float maxAggro = 100f;

        [HideInInspector]
        public UnityEngine.Component[] aggroComponents = new UnityEngine.Component[0];
        private IObserverAggroModule[] _aggroModules = new IObserverAggroModule[0];
        public IEnumerable<IObserverAggroModule> aggroModules
        {
            get { return _aggroModules; }
        }

        private IObserver _observer;
        public IObserver observer
        {
            get
            {
                if (_observer == null)
                {
                    _observer = GetComponent<IObserver>();
                }

                return _observer;
            }
        }

        [SerializeField]
        private float aggroCheckUpdateInterval = 1f;

        public float defaultAggroValue = 0f;

        public Dictionary<ISightTarget, SightTargetAggroInfo> aggroDict { get; protected set; }

        private static readonly List<IObserverAggroCallbacks> _callbacks = new List<IObserverAggroCallbacks>(); 
        private static readonly List<ISightTarget> _toForget = new List<ISightTarget>();
        private ISightTarget _targetWithHighestAggro;

        public ObserverAggroBehaviour()
        {
             aggroDict = new Dictionary<ISightTarget, SightTargetAggroInfo>();
        }

        protected virtual void Awake()
        {
            startLocation = transform.position;
            SetAggroModules();

            InvokeRepeating("UpdateAggro", Random.Range(0f, aggroCheckUpdateInterval), aggroCheckUpdateInterval);
        }

        protected virtual void UpdateAggro()
        {
            ForgetTargetsNow(); // In case some were queued up.
            var before = _targetWithHighestAggro;

            _targetWithHighestAggro = null;
            float targetsHighestAggro = 0f;

            foreach (var keyValuePair in aggroDict)
            {
//                if (keyValuePair.Key.enabled == false)
//                {
//                    if (_targetWithHighestAggro == keyValuePair.Key)
//                    {
//                        _targetWithHighestAggro = null;
//                    }
//
//                    continue;
//                }

                if (keyValuePair.Key.IsDestroyed())
                {
                    ForgetTarget(keyValuePair.Key);
                    continue;
                }

                keyValuePair.Value.modulesValue = 0f;

                foreach (var module in _aggroModules)
                {
                    if (ReferenceEquals(module, null) || module.Equals(null) || module.enabled == false)
                    {
                        continue;
                    }

                    keyValuePair.Value.modulesValue += module.CalculateAggroForTarget(keyValuePair.Key);

                    if (_targetWithHighestAggro == null || _targetWithHighestAggro.IsDestroyed() || keyValuePair.Value.finalValue > targetsHighestAggro)
                    {
                        _targetWithHighestAggro = keyValuePair.Key;
                        targetsHighestAggro = keyValuePair.Value.finalValue;
                    }
                }
            }

            if (before != _targetWithHighestAggro)
            {
                NotifyTargetWithHighestAggroChanged(_targetWithHighestAggro, targetsHighestAggro);
            }

            ForgetTargetsNow(); // After updating some modules may want to forget a target.
        }

        protected void NotifyTargetWithHighestAggroChanged(ISightTarget target, float aggro)
        {
            gameObject.GetComponents(_callbacks);
            foreach (var callback in _callbacks)
            {
                callback.OnTargetWithHighestAggroChanged(target, aggro);
            }
        }

        protected virtual void SetAggroModules()
        {
            _aggroModules = new IObserverAggroModule[aggroComponents.Length];
            for (int i = 0; i < _aggroModules.Length; i++)
            {
                _aggroModules[i] = (IObserverAggroModule)aggroComponents[i];

                if (_aggroModules[i].enabled)
                {
                    _aggroModules[i].Init(this);
                }
            }
        }

        [Obsolete("Use ChangeAggroForTarget instead.")]
        public void AddAggroToTarget(ISightTarget target, float changeAmount)
        {
            ChangeAggroForTarget(target, changeAmount);
        }

        public void ChangeAggroForTarget(ISightTarget target, float changeAmount)
        {
            var t = GetAggroForTarget(target);
            if (t != null)
            {
                SetAggroForTarget(target, t.addValue + changeAmount);
            }
            else
            {
                SetAggroForTarget(target, changeAmount);
            }
        }

        public void SetAggroForTarget(ISightTarget target, float amount)
        {
            if (aggroDict.ContainsKey(target) == false)
            {
                aggroDict.Add(target, new SightTargetAggroInfo(target, defaultAggroValue)
                {
                    addValue = amount
                });
            }
            else
            {
                aggroDict[target].addValue = amount;
            }
        }

        public SightTargetAggroInfo GetTargetWithHighestAggro()
        {
            var highest = new KeyValuePair<ISightTarget, SightTargetAggroInfo>();
            foreach (var cur in aggroDict)
            {
                if (cur.Key.IsDestroyed())
                {
                    continue;
                }

                if (highest.Value == null || cur.Value.finalValue > highest.Value.finalValue)
                {
                    highest = cur;
                }
            }

            return highest.Value;
        }

        public SightTargetAggroInfo GetAggroForTarget(ISightTarget target)
        {
            if (aggroDict.ContainsKey(target) == false || target.IsDestroyed())
            {
                return null;
            }

            return aggroDict[target];
        }

        public void ForgetTarget(ISightTarget target)
        {
            _toForget.Add(target);
        }

        protected void ForgetTargetsNow()
        {
            foreach (var sightTarget in _toForget)
            {
                aggroDict.Remove(sightTarget);
            }

            _toForget.Clear();
        }

        protected void AddTargetToAggroList(ISightTarget target)
        {
            // Could've been forced before with AddAggroToTarget
            if (aggroDict.ContainsKey(target) == false)
            {
                var aggroBehaviour = target.gameObject.GetComponent<SightTargetAggroBehaviour>();
                if (aggroBehaviour != null)
                {
                    aggroDict.Add(target, new SightTargetAggroInfo(target, aggroBehaviour.aggroConfig.defaultAggro)
                    {
                        multiplier = aggroBehaviour.aggroConfig.aggroMultiplier
                    });
                }
                else
                {
                    aggroDict.Add(target, new SightTargetAggroInfo(target, defaultAggroValue));
                }
            }
        }

        public void OnTargetCameIntoRange(SightTargetInfo info)
        {
//            if (_removeDict.ContainsKey(info.target))
//            {
//                Debug.Log("Target came back into range, resetting timer of: " + (_removeDict[info.target] - LosUtility.time));
//            }

            if (info.isDetected)
            {
                AddTargetToAggroList(info.target);
            }
        }
        
        public void OnTargetWentOutOfRange(SightTargetInfo info)
        {
            ForgetTarget(info.target);
            ForgetTargetsNow();
        }

        public void OnTargetDestroyed(SightTargetInfo info)
        {
            ForgetTarget(info.target);
            ForgetTargetsNow();
        }

        public void OnTryingToDetectTarget(SightTargetInfo info) { }
        public void OnDetectingTarget(SightTargetInfo info) { }

        public void OnDetectedTarget(SightTargetInfo info)
        {
            AddTargetToAggroList(info.target);
        }

        public void OnStopDetectingTarget(SightTargetInfo info) { }
        public void OnUnDetectedTarget(SightTargetInfo info) { }


        public void OnHeardTarget(AudioSourceInfo info)
        {
            if (info.audioSource.emitter != null)
            {
                var target = info.audioSource.emitter.GetComponentInParent<ISightTarget>();
                if (target != null)
                {
                    AddTargetToAggroList(target);
                }
            }
        }
    }
}