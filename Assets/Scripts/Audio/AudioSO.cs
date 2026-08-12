using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Audio", menuName = "Audio")]

public class AudioSO : ScriptableObject
{
    public List<AudioClip> soundEffects;

    public float minPitch = 1;
    public float maxPitch = 10;


    public void PlayFromSource(AudioSource source)
    {
        if (source == null)
        {
            Debug.LogWarning("PlayFromSource: No AudioSource assigned.");
            return;
        }

        if (source == null)
        {
            Debug.LogWarning("PlayFromSource: No AudioClipData assigned.");
            return;
        }

        if (soundEffects == null || soundEffects.Count == 0)
        {
            Debug.LogWarning("PlayFromSource: AudioClipData contains no audio clips.");
            return;
        }
        int randomIndex = Random.Range(0, soundEffects.Count);
        source.clip = soundEffects[randomIndex];

        // Pick a random pitch
        source.pitch = Random.Range(
            minPitch,
            maxPitch
        );

        // Play
        source.Play();
        Debug.Log($"audio was played {source.gameObject}");
    }

    

    public void PlayGlobal()
    {
        if(GlobalAudio.I == null || GlobalAudio.I.source)
        {
            Debug.Log("No Global Audio Source");
            return;
        }

        PlayFromSource(GlobalAudio.I.source);
    }
}
