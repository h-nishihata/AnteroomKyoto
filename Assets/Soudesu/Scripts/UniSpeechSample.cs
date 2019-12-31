// https://qiita.com/noir_neo/items/e51f2b503883d9b26c07
// https://github.com/noir-neo/UniSpeech
using UnityEngine;

namespace UniSpeech.Sample
{
    public class UniSpeechSample : MonoBehaviour, ISpeechRecognizer
    {
        public UniSpeechSampleUI ui;
        private AudioManager audioManager;

        private bool hasDetectedUser;
        private float timeSinceLastInput;
        public float inputTimeOut = 10f;


        void Start()
        {
            SpeechRecognizer.CallbackGameObjectName = gameObject.name;
            SpeechRecognizer.RequestRecognizerAuthorization();
            ui.UpdateButton("認証を待っています", false);
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
                hasDetectedUser = false; 
                ui.UpdateText("何か語りかけてみてください");
            }
        }


        /// <summary>
        /// マイクから何かしらの入力がある度にこのメソッドが呼ばれる.
        /// </summary>
        /// <param name="transcription">Startボタンを押してからの累計のメッセージが入る.</param>
        public void OnRecognized(string transcription)
        {
            //Debug.Log("OnRecognized: " + transcription);

            // フィードバック回避.
            if ((transcription.Length > 4) && (transcription.Substring(transcription.Length - 4) == "そうです"))
                return;

            hasDetectedUser = true;
            timeSinceLastInput = 0;
            ui.UpdateText(transcription);
            audioManager.Play(Random.Range(0, audioManager.clips.Length));
        }

        public void OnError(string description)
        {
            ui.SetButtonStatus(true);
            ui.onClick = StartRecord;
            ui.UpdateButton("開始", true);
            ui.OnClick(); // 自動で音声認識を開始.
        }

        public void OnAuthorized()
        {
            ui.SetButtonStatus(true);
            ui.onClick = StartRecord;
            ui.UpdateButton("開始", true);
            ui.OnClick();
        }

        public void OnUnauthorized()
        {
            ui.SetButtonStatus(true);
            ui.UpdateButton("認証に失敗しました", false);
        }

        public void OnAvailable()
        {
            ui.SetButtonStatus(true);
            ui.onClick = StartRecord;
            ui.UpdateButton("開始", true);
            ui.OnClick();
        }

        public void OnUnavailable()
        {
            ui.SetButtonStatus(true);
            ui.UpdateButton("エラー", false);
        }

        private void StartRecord()
        {
            if (SpeechRecognizer.StartRecord())
            {
                ui.SetButtonStatus(false);
                ui.UpdateButton("停止", true);
                ui.onClick = StopRecord;
            }
        }

        private void StopRecord()
        {
            if (SpeechRecognizer.StopRecord())
            {
                ui.SetButtonStatus(false);
                ui.UpdateButton("停止中…", false);
            }
        }
    }
}

