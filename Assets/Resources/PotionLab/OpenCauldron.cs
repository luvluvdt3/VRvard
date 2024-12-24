using UnityEngine;
using UnityEngine.Events;

public class OpenCauldron : MonoBehaviour
{
    public UnityEvent OnMagicCollision;
    public bool DestroyedOnTriggered;

    void OnCollisionEnter(Collision other)
    {
        OnMagicCollision.Invoke();
        if (DestroyedOnTriggered)
            Destroy(this); 
    }
}
