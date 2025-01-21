using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource stepsAudio;
    [SerializeField] private Vector2 minMaxStepPitch;

    public void PlayStepAudio()
    {
        stepsAudio.pitch = Random.Range(minMaxStepPitch.x, minMaxStepPitch.y);
        stepsAudio.Play();
    }
    
    public void PlayJumpAudio() => jumpAudio.Play();
}
