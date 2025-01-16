using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void ToggleAudioPause(AudioSource audioSource)
    {
        if(audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }
}