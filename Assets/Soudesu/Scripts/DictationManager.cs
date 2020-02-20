// https://qiita.com/noir_neo/items/e51f2b503883d9b26c07
// https://github.com/noir-neo/UniSpeech
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UniSpeech.Soudesu
{
    public class DictationManager : MonoBehaviour, ISpeechRecognizer
    {
        public UIController ui;
        private AudioManager audioManager;
        private string prevTranscription = "prevTranscription";

        private Scene scene;
        private float timeSinceLastInput;
        public float timeToReload = 60f;


        void Start()
        {
            SpeechRecognizer.CallbackGameObjectName = gameObject.name;
            SpeechRecognizer.RequestRecognizerAuthorization();
            //ui.UpdateButton("認証を待っています");

            audioManager = this.gameObject.GetComponent<AudioManager>();
            scene = SceneManager.GetActiveScene();
        }

        private void Update()
        {
            // 一定時間経過後に入力を認識しなくなる問題があるため，定期的にシーンをリロードしてみる.
            if (timeSinceLastInput < timeToReload)
            {
                timeSinceLastInput += Time.deltaTime;
            }
            else if (timeSinceLastInput >= timeToReload)
            {
                timeSinceLastInput = 0;
                this.ReloadScene();
            }
        }

        /// <summary>
        /// マイクから何かしらの入力がある度にこのメソッドが呼ばれる.
        /// </summary>
        /// <param name="transcription">Startボタンを押してからの累計のメッセージが入る.</param>
        public void OnRecognized(string transcription)
        {
            if (audioManager.source.isPlaying)
            {
                //Debug.Log("isPlaying");
                return;
            }

            // フィードバック回避.
            if (transcription.Length > 4)
            {
                if((transcription.Substring(transcription.Length - 4) == "そうです") ||
                   (transcription.Substring(transcription.Length - 4) == "ですです") ||
                   (transcription.Substring(transcription.Length - 2) == "です"))
                    return;
            }
                
            // 停止ボタン押下直後になぜか呼ばれてしまう現象回避.
            if (transcription == prevTranscription)
                return;

            timeSinceLastInput = 0;

            // Debug.Log("OnRecognized: " + transcription);
            // ui.UpdateText(transcription);
            prevTranscription = transcription;

            audioManager.Play();
        }

        public void OnError(string description)
        {
            ui.onClick = StartRecord;
            //ui.UpdateButton("開始");
            ui.OnClick(); // 自動で音声認識を再開.
        }

        public void OnAuthorized()
        {
            ui.onClick = StartRecord;
            //ui.UpdateButton("開始");
            ui.OnClick();
        }

        public void OnUnauthorized()
        {
            //ui.UpdateButton("認証に失敗しました");
        }

        public void OnAvailable()
        {
            ui.onClick = StartRecord;
            //ui.UpdateButton("開始");
            ui.OnClick();
        }

        public void OnUnavailable()
        {
            //ui.UpdateButton("エラー");
        }

        private void StartRecord()
        {
            if (SpeechRecognizer.StartRecord())
            {
                //ui.UpdateButton("リセット");
                ui.onClick = StopRecord;
            }
        }

        private void StopRecord()
        {
            if (SpeechRecognizer.StopRecord())
            {
                //ui.UpdateButton("リセットしています…");
            }
        }

        private void ReloadScene()
        {
            //Debug.Log("Reload");
            SceneManager.LoadScene(scene.name);
        }
    }
}