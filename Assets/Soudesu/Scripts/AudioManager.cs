using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource source; 
    public AudioClip[] clips;
    private int numClip;
    //public Text soudesu;

    public void Play()
    {
        if (Random.Range(0, 100) >= 5)
            numClip = Random.Range(0, this.clips.Length - 1);
        else
            numClip = this.clips.Length - 1; // 最後の音声ファイルだけ出現率を5%に設定.

        if (source.isPlaying)
            return;

        source.clip = clips[numClip];
        source.Play();
        //soudesu.gameObject.SetActive(true);
    }
}