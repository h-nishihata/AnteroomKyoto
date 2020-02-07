using UnityEngine;
//using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource source; 
    public AudioClip[] clips;
    private int numClip;
    //public Text soudesu;

    public void Play()
    {
        if (source.isPlaying)
            return;

        if (Random.Range(0, 100) >= 1)
            numClip = Random.Range(0, this.clips.Length - 1);
        else
            numClip = this.clips.Length - 1; // 一番長い19番をシークレットに設定.

        source.clip = clips[numClip];
        source.Play();
        //soudesu.gameObject.SetActive(true);
    }
}