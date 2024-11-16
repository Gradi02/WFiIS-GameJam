using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class sounds
{
    public string name;

    public AudioClip clip;

    public bool playonawake;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}