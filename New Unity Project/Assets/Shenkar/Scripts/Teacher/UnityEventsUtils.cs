using UnityEngine;
using UnityEngine.Events;

namespace Shenkar.Dweiss
{
    [System.Serializable]
    public class EvenEmpty : UnityEvent { }

    [System.Serializable]
    public class EventString : UnityEvent<string> { }

    [System.Serializable]
    public class EventBool : UnityEvent<bool> { }

    [System.Serializable]
    public class EventFloat : UnityEvent<float> { }

    [System.Serializable]
    public class EventInt : UnityEvent<int> { }

    [System.Serializable]
    public class EventVector3 : UnityEvent<Vector3> { }

    [System.Serializable]
    public class EventScriptableObject : UnityEvent<ScriptableObject> { }

    [System.Serializable]
    public class EventMonoBehaviour : UnityEvent<UnityEngine.MonoBehaviour> { }


    [System.Serializable]
    public class EventGameObject : UnityEvent<UnityEngine.GameObject> { }


    [System.Serializable]
    public class EventCollider : UnityEvent<Collider> { }

    [System.Serializable]
    public class EventColliders : UnityEvent<Collider[]> { }

}