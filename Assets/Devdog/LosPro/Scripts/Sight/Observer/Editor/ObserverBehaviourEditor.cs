using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(ObserverBehaviour), true)]
    public class ObserverBehaviourEditor : Editor
    {
        public void OnEnable()
        {
            var t = (ObserverBehaviour)target;
            if (t.eyes == null)
            {
                t.eyes = t.transform;
                EditorUtility.SetDirty(t.eyes);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var t = (ObserverBehaviour)target;
            var collider = t.sightTrigger;

            if (Mathf.Approximately(collider.radius, t.config.viewingDistance) == false)
            {
                collider.radius = t.config.viewingDistance;
                EditorUtility.SetDirty(collider);
            }
        }

        [DrawGizmo(GizmoType.Selected)]
        protected static void GizmoTest(Transform transform, GizmoType gizmoType)
        {
            var observer = transform.GetComponentInParent<ObserverBehaviour>();
            if (observer != null && (transform == observer.eyes || transform == observer.transform))
            {
                var before = Handles.color;

                var innerColor = Color.cyan;
                innerColor.a = 0.15f;

                var outerColor = Color.blue;
                outerColor.a = 0.4f;
                if (observer.eyes != null)
                {
                    float angleRadians = Mathf.Acos(observer.config.fieldOfViewDotValue);
                    float angleDegrees = angleRadians * Mathf.Rad2Deg;
                    var forward = Quaternion.AngleAxis(-angleDegrees, observer.eyes.up) * observer.eyes.forward;

                    Handles.color = innerColor;
                    Handles.DrawSolidArc(observer.eyes.position, observer.eyes.up, forward, angleDegrees * 2, observer.config.viewingDistance);

                    Handles.color = outerColor;
                    Handles.DrawWireArc(observer.eyes.position, observer.eyes.up, forward, angleDegrees * 2, observer.config.viewingDistance);

                    Handles.color = before;
                }
            }
        }
    }
}