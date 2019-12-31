using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    public Text soudesu;
    public string[] soudesuPhrases;


    public void Play(int numClip)
    {
        if (source.isPlaying)
            return;

        source.clip = clips[numClip];
        source.Play();

        soudesu.text = soudesuPhrases[numClip];
        soudesu.gameObject.SetActive(true);
    }
}