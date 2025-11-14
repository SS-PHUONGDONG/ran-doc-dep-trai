using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;
    public AudioClip musicClip;
    public AudioClip coinClip;
    public AudioClip winClip;

    // Update is called once per frame
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }
}
