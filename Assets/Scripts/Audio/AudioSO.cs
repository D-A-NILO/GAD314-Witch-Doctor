using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Audio", menuName = "Audio")]

public class AudioSO : ScriptableObject
{
    public List<AudioClip> soundEffects;

    public float minPitch = 1;
    public float maxPitch = 10;
}
