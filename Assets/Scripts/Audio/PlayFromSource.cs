using UnityEngine;

public class PlayFromSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlayAudio(AudioSO audioSO)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("PlayFromSource: No AudioSource assigned.");
            return;
        }

        if (audioSO == null)
        {
            Debug.LogWarning("PlayFromSource: No AudioClipData assigned.");
            return;
        }

        if (audioSO.soundEffects == null || audioSO.soundEffects.Count == 0)
        {
            Debug.LogWarning("PlayFromSource: AudioClipData contains no audio clips.");
            return;
        }
        int randomIndex = Random.Range(0, audioSO.soundEffects.Count);
        audioSource.clip = audioSO.soundEffects[randomIndex];

        // Pick a random pitch
        audioSource.pitch = Random.Range(
            audioSO.minPitch,
            audioSO.maxPitch
        );

        // Play
        audioSource.Play();
        Debug.Log($"audio was played {gameObject}");
    }

}
