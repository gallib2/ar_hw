using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithColliders : MonoBehaviour {

    [Header("Debug")]
    public bool debug;

    [Header("Configuration")]
    public InteractWithCollider[] lookingForCollider;

    [Header("Events")]
    public Shenkar.Dweiss.EvenEmpty onFullInteractionEnter, onFullInteractionExit;


    [System.Serializable]
    public class InteractWithCollider
    {
        public Collider cldr;
        [HideInInspector]public bool isInteracting;
    }

    private void Awake()
    {
        onFullInteractionEnter.AddListener(() => { if (debug) Debug.Log("Full interaction enter"); });
        onFullInteractionExit.AddListener(() => { if (debug) Debug.Log("Full interaction exit"); });

    }

    private int CountInteractingColliders()
    {
        int interactingCount = 0;
        for (int i = 0; i < lookingForCollider.Length; i++)
        {
            if (lookingForCollider[i].isInteracting) ++interactingCount;
        }
        return interactingCount;
    }

    private void UpdateCollider(Collider cldr, bool enter)
    {
        int interactingStartCount = CountInteractingColliders();
        for (int i = 0; i < lookingForCollider.Length; i++)
        {
            if (lookingForCollider[i].cldr == cldr)
            {
                lookingForCollider[i].isInteracting = enter;
            }
        }
        int interactingEndCount = CountInteractingColliders();

        if (interactingStartCount != interactingEndCount)
        {
            if (interactingEndCount == lookingForCollider.Length)
            {
                onFullInteractionEnter.Invoke();
            }
            else  if (interactingStartCount == lookingForCollider.Length &&
                interactingEndCount + 1 == lookingForCollider.Length)
            {
                onFullInteractionExit.Invoke();
            }
        }
    }

    public void EnterInteraction(Collider cldr)
    {
        UpdateCollider(cldr, true);
    }

    public void ExitInteraction(Collider cldr)
    {
        UpdateCollider(cldr, false);
    }


}
