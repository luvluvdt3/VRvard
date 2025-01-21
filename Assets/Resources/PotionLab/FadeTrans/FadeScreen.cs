using System.Collections;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public float fadeDuration = 2.0f;
    public Color fadeColor;
    private Renderer rend;
    public AudioSource audioSource;
    public bool fadeOnStart = true;    
    
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeInThenOut();
        }
            
    }
    
    private void FadeIn()
    {
        StartCoroutine(FadeRoutine(0, 1));
    }
    
    private void FadeOut()
    {
        StartCoroutine(FadeRoutine(1, 0));
    }
    
    public void FadeInThenOut()
    {
        StartCoroutine(FadeInOutRoutine());
    }
    
    private IEnumerator FadeInOutRoutine()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        yield return FadeRoutine(0, 1);
        yield return new WaitForSeconds(3f);
        yield return FadeRoutine(1, 0);
    }
    
    private IEnumerator FadeRoutine (float alphaIn, float alphaOut)
    {
        float timer = 0;

        while (timer < fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);
            timer += Time.deltaTime;
            yield return null;
        }
        
        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
    }
    
}
