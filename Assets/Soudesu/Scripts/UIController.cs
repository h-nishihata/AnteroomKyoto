using System;
using UnityEngine;
using UnityEngine.UI;

namespace UniSpeech.Soudesu
{
    public class UIController : MonoBehaviour
    {
        public Text text;
        public Button button;
        public Text buttonText;

        public Action onClick = () => { };


        public void OnClick()
        {
            onClick();
        }
         
        /// <summary>
        /// 喋った内容を画面に表示する.
        /// </summary>
        public void UpdateText(string text)
        {
            this.text.text = text;
        }

        /// <summary>
        /// ボタンに状態を表示する.
        /// </summary>
        public void UpdateButton(string text)
        {
            buttonText.text = text;
        }
    }
}