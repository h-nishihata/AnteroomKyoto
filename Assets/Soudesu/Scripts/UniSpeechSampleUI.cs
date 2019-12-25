﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace UniSpeech.Sample
{
    public class UniSpeechSampleUI : MonoBehaviour
    {
        public Text text;
        public Button button;
        public Text buttonText;

        public Action onClick = () => { };

        public void OnClick()
        {
            onClick();
        }

        public void UpdateText(string text)
        {
            this.text.text = text;
        }

        /// <summary>
        /// 状態を表示する.
        /// </summary>
        public void UpdateButton(string text, bool interactive)
        {
            buttonText.text = text;
            button.interactable = interactive;
        }

        public void ShowButton(bool isActive)
        {
            button.gameObject.SetActive(isActive);
        }
    }
}

