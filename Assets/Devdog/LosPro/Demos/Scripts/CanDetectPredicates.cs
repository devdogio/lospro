using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Devdog.General;

namespace Devdog.LosPro.Demo
{
    public class CanDetectPredicates : MonoBehaviour
    {
        private ISight _sight;
        private void Awake()
        {
            _sight = GetComponent<IObserver>().sight;
            _sight.canDetectPredicates = new List<Predicate<SightTargetInfo>>();
            _sight.canDetectPredicates.Add(CanDetectTarget);
//            _sight.canDetectPredicates.Add(CanDetectTargetFalse);
        }

        private bool CanDetectTarget(SightTargetInfo obj)
        {
            return true;
        }

//        private bool CanDetectTargetFalse(SightTargetInfo obj)
//        {
//            return false;
//        }
    }
}