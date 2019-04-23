using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustCubeEvent : MonoBehaviour {

    public bool debug;

    [Header("Configuration")]
    public Collider toRaycastCldr;
    public Collider toHitTriggerCldr;

    [Header("Events")]
    public Shenkar.Dweiss.EvenEmpty onEnter;
    public Shenkar.Dweiss.EvenEmpty onExit;

    private bool hitRaycast, hitTrigger, lastHitBothSuccess;

    private Transform t;
    private void Awake()
    {
        t = transform;
    }

    void CheckHitSucces()
    {
        if(lastHitBothSuccess == false)
        {
            lastHitBothSuccess = hitRaycast && hitTrigger;
            if (lastHitBothSuccess) onEnter.Invoke();
        } else
        {
            lastHitBothSuccess = hitRaycast && hitTrigger;
            if (lastHitBothSuccess == false) onExit.Invoke();
        }
    }
	void FixedUpdate () {
        RaycastHit[] raycast = Physics.RaycastAll(t.position, t.forward, float.MaxValue);

        if (debug) Debug.DrawRay(t.position, t.forward * 100, Color.white);

        hitRaycast = false;
        for (int i = 0; i < raycast.Length; i++)
        {
            if (debug)
            {
                Debug.DrawLine(t.position, raycast[i].collider.transform.position, Color.red);
                Debug.Log("Raycast " + raycast[i].collider.transform.name);
            }

            if (raycast[i].collider == toRaycastCldr)
            {
                hitRaycast = true;
                
            }
        }

        CheckHitSucces();
    }

    public void OnTriggerEnter(Collider cldr)
    {
        if (cldr == toHitTriggerCldr)
        {
            if (debug) Debug.Log(name + " OnTriggerEnter " + cldr.name);
            hitTrigger = true;
            CheckHitSucces();
        }
    }
    public void OnTriggerExit(Collider cldr)
    {
        if (cldr == toHitTriggerCldr)
        {
            if (debug) Debug.Log(name + " OnTriggerEnter " + cldr.name);
            hitTrigger = false;
            CheckHitSucces();
        }
    }
}
