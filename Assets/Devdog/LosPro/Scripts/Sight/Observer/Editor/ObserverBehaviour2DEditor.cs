using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace Devdog.LosPro.Editors
{
    [CustomEditor(typeof(ObserverBehaviour2D), true)]
    public class ObserverBehaviour2DEditor : Editor
    {

        public void OnEnable()
        {
            var t = (ObserverBehaviour2D)target;
            if (t.eyes == null)
            {
                t.eyes = t.transform;
                EditorUtility.SetDirty(t.eyes);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var t = (ObserverBehaviour2D)target;
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
            var observer = transform.GetComponentInParent<ObserverBehaviour2D>();
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
                    var forward = Quaternion.AngleAxis(-angleDegrees, -observer.eyes.forward) * -observer.eyes.right;

                    Handles.color = innerColor;
                    Handles.DrawSolidArc(observer.eyes.position, -observer.eyes.forward, forward, angleDegrees * 2, observer.config.viewingDistance);

                    Handles.color = outerColor;
                    Handles.DrawWireArc(observer.eyes.position, -observer.eyes.forward, forward, angleDegrees * 2, observer.config.viewingDistance);

                    Handles.color = before;
                }
            }
        }
    }
}