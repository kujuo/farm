using UnityEngine;
using System.Collections;
 
public static class AudioFadeOut {
 
    public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime, float val=-1) {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0) {
            audioSource.volume += val*startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }
 
}