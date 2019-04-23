using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Shenkar.Dweiss
{
    public class Raycast : MonoBehaviour
    {

        public bool debug;

        [Header("Configuration")]
        public LayerMask layer = -1;// Mask = everything
        public string tagToCompare;

        [Header("Events")]
        public Shenkar.Dweiss.EventCollider onRaycastEnter, onRaycastExit;

        private HashSet<Collider> raycastCldrs = new HashSet<Collider>();

        private Transform t;
        private void Awake()
        {
            t = transform;
        }

        private void OnDrawGizmosSelected()
        {
            Awake();
            RaycastHit[] raycast = Physics.RaycastAll(t.position, t.forward, float.MaxValue, layer);

            Gizmos.color = Color.white;
            Gizmos.DrawRay(t.position, t.forward * 100);

            for (int i = 0; i < raycast.Length; i++)
            {
                if (IsMyTag(raycast[i].collider))
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(t.position,
                        raycast[i].collider.transform.position);
                }
            }
        }

        private bool IsMyTag(Collider collider)
        {
            return string.IsNullOrEmpty(tagToCompare) ||
                    collider.gameObject.CompareTag(tagToCompare);
        }

        private void UpdateRaycastColliders(HashSet<Collider> newCldrs)
        {
            //Find all added cldrs
            foreach (Collider cldr in newCldrs)
            {
                if (raycastCldrs.Contains(cldr) == false)
                {
                    onRaycastEnter.Invoke(cldr);
                }
            }
            //Find all removed Cldrs
            foreach (Collider cldr in raycastCldrs)
            {
                if (newCldrs.Contains(cldr) == false)
                {
                    onRaycastExit.Invoke(cldr);
                }
            }
            //Save new cldrs
            raycastCldrs = newCldrs;
        }

        void FixedUpdate()
        {
            var newRaycastCldrs = new HashSet<Collider>();
            RaycastHit[] raycast = Physics.RaycastAll(t.position, t.forward, float.MaxValue, layer);

            if (debug) Debug.DrawRay(t.position, t.forward * 100, Color.white);

            for (int i = 0; i < raycast.Length; i++)
            {
                if (IsMyTag(raycast[i].collider))
                {
                    newRaycastCldrs.Add(raycast[i].collider);
                    if (debug)
                    {
                        Debug.DrawLine(t.position, raycast[i].collider.transform.position, Color.red);
                        Debug.Log("Hit " + raycast[i].collider.transform.name);
                    }
                }
            }

            UpdateRaycastColliders(newRaycastCldrs);
        }


    }
}