using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource audioSource;
    public TextMeshProUGUI valueText;
    public AudioMixMode mixMode;

    public void OnChangeVolume(float value)
    {
        valueText.SetText($"{value.ToString("N4")}");

        switch (mixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
                audioSource.volume = value;
                break;
            case AudioMixMode.LinearMixerVolume:
                mixer.SetFloat("Volume", (-80 + value * 100));
                break;
            case AudioMixMode.LogarithmicMixerVolume:
                mixer.SetFloat("Volume", Mathf.Log10(value)* 20);
                break;
        }
    }


    public enum AudioMixMode 
    {
        LinearAudioSourceVolume,
        LinearMixerVolume,
        LogarithmicMixerVolume
    }
}
