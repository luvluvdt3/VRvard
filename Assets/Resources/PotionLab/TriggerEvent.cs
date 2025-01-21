using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    //input : objectName , Event to invoke later
    //check if objectName of trigger is the same, if its, invoke the event
    
    public string objectName;
    public UnityEvent eventToInvoke;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == objectName)
        {
            eventToInvoke.Invoke();
        }
    }
}
