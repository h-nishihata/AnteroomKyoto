using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    public Text soudesuText;


    public void Play(int numClip)
    {
        source.clip = clips[numClip];
        source.Play();
        soudesuText.gameObject.SetActive(true);
    }
}