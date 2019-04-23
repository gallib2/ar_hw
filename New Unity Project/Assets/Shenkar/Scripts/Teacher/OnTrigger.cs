using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dweiss.Common
{
    public class OnTrigger : MonoBehaviour
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

        public void OnTriggerEnter(Collider cldr)
        {
            if ((string.IsNullOrEmpty(tagToCompare) || cldr.gameObject.CompareTag(tagToCompare)) &&
                HasLayer(layer,cldr.gameObject.layer))
            {
                if (debug) Debug.Log(name + " OnTriggerEnter success with " + cldr);
                onColliderEnter.Invoke(cldr);
            }
        }
        public void OnTriggerExit(Collider cldr)
        {
            if ((string.IsNullOrEmpty(tagToCompare) || cldr.gameObject.CompareTag(tagToCompare)) &&
                HasLayer(layer, cldr.gameObject.layer))
            {
                if (debug) Debug.Log(name + " OnTriggerExit success with " + cldr);
                onColliderExit.Invoke(cldr);
            }
        }
    }
}