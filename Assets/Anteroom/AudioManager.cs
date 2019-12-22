using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;


    public void Play(int numClip)
    {
        source.clip = clips[numClip];
        source.Play();
        Debug.Log("そうです");
    }
}