using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shenkar.Dweiss
{
    [ExecuteInEditMode]
    public class AlignWith : MonoBehaviour
    {
        public Transform source, pointB;


#if UNITY_EDITOR
        [UnityEditor.CustomEditor(typeof(AlignWith))]
        [UnityEditor.CanEditMultipleObjects]
        public class AlignWithCustomEditor : UnityEditor.Editor
        {
            public override void OnInspectorGUI()
            {

                DrawDefaultInspector();

                if (GUILayout.Button("Rotate accordingly"))
                {
                    ((AlignWith)target).Set();
                }
            }
        }


#endif

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if (source) Gizmos.DrawLine(source.position, transform.position);
            Gizmos.color = Color.red;
            if (source && pointB) Gizmos.DrawLine(source.position, pointB.position);
        }

        public void Set()
        {
            Vector3 dirAndDist = transform.position - source.position;
            var dirB2A = pointB.position - source.position;

            transform.position = source.position + dirB2A.normalized * dirAndDist.magnitude;
            transform.LookAt(pointB);


        }
    }
}