using UnityEngine;
using UnityEngine.Events;

public class OpenCauldron : MonoBehaviour
{
    public UnityEvent OnMagicCollision;
    public bool DestroyedOnTriggered;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision detected with " + other.gameObject.name);
        OnMagicCollision.Invoke();
        if (DestroyedOnTriggered)
            Destroy(this); 
    }

    
}
