using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dweiss.Common
{
    public class OnCollision : MonoBehaviour
    {
        public bool debug;

        [Header("Configuration")]
        public LayerMask layer = new LayerMask() { value = -1 /*Everything*/ };
        [Tooltip("If empty will not check for tag")]
        public string tagToCompare = "";


        [Header("Events")]
        public Shenkar.Dweiss.EventCollider onColliderEnter;
        public Shenkar.Dweiss.EventCollider onColliderExit;


        private void Reset()
        {
            layer.value = -1;
        }

        public static bool HasLayer(LayerMask mask, int layerValue)
        {
            //Debug.LogFormat("raw: {0} | {1}  log: {2} | {3}", mask.value, layerValue, Mathf.Log(mask.value, 2f), Mathf.Log(layerValue));
            return ((1 << layerValue) & mask.value) != 0;
        }

        public void OnCollisionEnter(Collision cldr)
        {
            if ((string.IsNullOrEmpty(tagToCompare) || cldr.gameObject.CompareTag(tagToCompare)) &&
                HasLayer(layer,cldr.gameObject.layer))
            {
                if (debug) Debug.Log(name + " OnCollisionEnter success with " + cldr.collider);
                onColliderEnter.Invoke(cldr.collider);
            }
        }
        public void OnCollisionExit(Collision cldr)
        {
            if ((string.IsNullOrEmpty(tagToCompare) || cldr.gameObject.CompareTag(tagToCompare)) &&
                HasLayer(layer, cldr.gameObject.layer))
            {
                if (debug) Debug.Log(name + " OnCollisionExit success with " + cldr.collider);
                onColliderExit.Invoke(cldr.collider);
            }
        }
    }
}