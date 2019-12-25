﻿// https://qiita.com/noir_neo/items/e51f2b503883d9b26c07
// https://github.com/noir-neo/UniSpeech
using UnityEngine;

namespace UniSpeech.Sample
{
    public class UniSpeechSample : MonoBehaviour, ISpeechRecognizer
    {
        public UniSpeechSampleUI ui;
        private AudioManager audioManager;
        //private bool hasDetectedUser;
        //private float timeSinceLastInput;
        //public float inputTimeOut = 0.5f; // ユーザの音声入力が途絶えてから「そうです」を再生するまでの時間.


        void Start()
        {
            SpeechRecognizer.CallbackGameObjectName = gameObject.name;
            SpeechRecognizer.RequestRecognizerAuthorization();
            ui.UpdateButton("認証を待っています", false);
            audioManager = this.gameObject.GetComponent<AudioManager>();
        }
        /*
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
                // timeSinceLastInputが一定時間を過ぎると「そうです」が再生される.
                audioManager.Play(Random.Range(0, 0));
                hasDetectedUser = false;
            }
        }
        */

        /// <summary>
        /// マイクから何かしらの入力がある度にこのメソッドが呼ばれる.
        /// </summary>
        public void OnRecognized(string transcription)
        {
            // transcriptionにはStartボタンを押してからの累計のメッセージが入る.
            // Debug.Log("OnRecognized: " + transcription);

            // フィードバックループ回避.
            if ((transcription.Length > 4) && (transcription.Substring(transcription.Length - 4) == "そうです"))
                return;
            //hasDetectedUser = true;
            //timeSinceLastInput = 0;
            ui.UpdateText(transcription);
            audioManager.Play(Random.Range(0, audioManager.clips.Length));
        }

        public void OnError(string description)
        {
            ui.onClick = StartRecord;
            ui.UpdateButton("開始", true);
            ui.OnClick(); // 自動で音声認識を開始.
        }

        public void OnAuthorized()
        {
            ui.onClick = StartRecord;
            ui.UpdateButton("開始", true);
            ui.OnClick();
        }

        public void OnUnauthorized()
        {
            ui.UpdateButton("認証に失敗しました", false);
        }

        public void OnAvailable()
        {
            ui.onClick = StartRecord;
            ui.UpdateButton("開始", true);
            ui.OnClick();
        }

        public void OnUnavailable()
        {
            ui.UpdateButton("エラー", false);
        }

        private void StartRecord()
        {
            if (SpeechRecognizer.StartRecord())
            {
                ui.UpdateButton("停止", true);
                ui.onClick = StopRecord;
            }
        }

        private void StopRecord()
        {
            if (SpeechRecognizer.StopRecord())
            {
                ui.UpdateButton("停止中…", false);
            }
        }
    }
}

