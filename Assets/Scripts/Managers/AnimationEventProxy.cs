using UnityEngine;
using UnityEngine.Events;

public class AnimationEventProxy : MonoBehaviour
{
    [SerializeField] private UnityEvent eventToInvoke;
    public void InvokeEvent() => eventToInvoke?.Invoke();
}