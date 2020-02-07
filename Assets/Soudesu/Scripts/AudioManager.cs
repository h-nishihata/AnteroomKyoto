using UnityEngine;
//using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource source; 
    public AudioClip[] clips;
    private int numClip;
    //public Text soudesu;
    private bool isWaiting;
    private float waitCount;
    private float waitDuration = 2f;

<<<<<<< HEAD

=======
>>>>>>> 47db98d7f9dcc49006ebbd8551555ca200c8e7e7
    private void Update()
    {
        if (isWaiting)
        {
            if (waitCount < source.clip.length + waitDuration)
            {
                waitCount += Time.deltaTime;
            }
            else
            {
                isWaiting = false;
                waitCount = 0;
            }
        }
    }

    public void Play()
    {
<<<<<<< HEAD
=======
        //if (source.isPlaying)
>>>>>>> 47db98d7f9dcc49006ebbd8551555ca200c8e7e7
        if (isWaiting)
            return;

        if (Random.Range(0, 100) >= 1)
            numClip = Random.Range(0, this.clips.Length - 1);
        else
            numClip = this.clips.Length - 1; // 一番長い19番をシークレットに設定.

        source.clip = clips[numClip];
        source.Play();
        //soudesu.gameObject.SetActive(true);
        isWaiting = true;
    }
}