// https://qiita.com/noir_neo/items/e51f2b503883d9b26c07

using UnityEngine;
using UnityEngine.UI;

namespace UniSpeech.Sample
{
    public class UniSpeechSample : MonoBehaviour, ISpeechRecognizer
    {
        public UniSpeechSampleUI ui;
        private AudioManager audioManager;
        public float timeSinceLastInput;
        private float inputTimeOut = 3f;
        public bool hasDetectedUser;
        public Text debugText;

        void Start()
        {
            SpeechRecognizer.CallbackGameObjectName = gameObject.name;
            SpeechRecognizer.RequestRecognizerAuthorization();
            ui.UpdateButton("Requesting authorization", false);
            audioManager = this.gameObject.GetComponent<AudioManager>();
        }

        void Update()
        {
            if (!hasDetectedUser)
                return;

            if (timeSinceLastInput < inputTimeOut)
            {
                timeSinceLastInput += Time.deltaTime;
            }
            else
            {
                audioManager.Play(Random.Range(0, 0));
                debugText.gameObject.SetActive(true);
                hasDetectedUser = false;
                Debug.Log("Succeed !");
            }
        }

        /// <summary>
        /// マイクから何かしらの入力がある度にこのメソッドが呼ばれる.
        /// </summary>
        public void OnRecognized(string transcription)
        {
            // transcriptionにはStartボタンを押してからの累計のメッセージが入る.
            Debug.Log("OnRecognized: " + transcription);

            // Update()でカウンターを持っておき，このメソッドが呼ばれるとリセットされるようにする.
            // カウンターが一定時間を過ぎると「そうです」が再生されるようにする.
            hasDetectedUser = true;
            timeSinceLastInput = 0;
            ui.UpdateText(transcription);
        }

        public void OnError(string description)
        {
            // Debug.Log("OnError: " + description);
            // ui.UpdateText("Error: " + description);
            ui.onClick = StartRecord;
            ui.UpdateButton("Start", true);
        }

        public void OnAuthorized()
        {
            //Debug.Log("OnAuthorized");
            ui.onClick = StartRecord;
            ui.UpdateButton("Start", true);
        }

        public void OnUnauthorized()
        {
            //Debug.Log("OnUnauthorized");
            ui.UpdateButton("Unauthorized", false);
        }

        public void OnAvailable()
        {
            //Debug.Log("OnAvailable");
            ui.onClick = StartRecord;
            ui.UpdateButton("Start", true);
        }

        public void OnUnavailable()
        {
            //Debug.Log("OnUnavailable");
            ui.UpdateButton("Not Available", false);
        }

        private void StartRecord()
        {
            if (SpeechRecognizer.StartRecord())
            {
                ui.UpdateButton("Stop", true);
                ui.onClick = StopRecord;
            }
        }

        private void StopRecord()
        {
            if (SpeechRecognizer.StopRecord())
            {
                ui.UpdateButton("Stopping", false);
            }
            debugText.gameObject.SetActive(false);
        }
    }
}

