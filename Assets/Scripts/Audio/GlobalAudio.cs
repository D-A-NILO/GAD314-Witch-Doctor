using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GlobalAudio : MonoBehaviour
{
    public static GlobalAudio I;
    [HideInInspector] public AudioSource source;

    void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
