using CustomUI.BeatSaber;
using Oculus.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CVREReplayMod
{
    class Util
    {
        private const string CopyButton = "RestartButton";
        public static void CreateButton(string text, UnityAction onClick, string hintText)
        {
            Logger.log.Debug("Creating button with message: " + text);
            ResultsViewController controller = UnityEngine.Object.FindObjectOfType<ResultsViewController>();
            RectTransform panel = controller.GetComponent<RectTransform>();

            if (panel == null)
            {
                return;
            }
            Logger.log.Debug("Non-null panel!");

            Button button = BeatSaberUI.CreateUIButton(panel, CopyButton, onClick, text);
            panel.Find(CopyButton).SetAsLastSibling();

            BeatSaberUI.AddHintText(button.transform as RectTransform, hintText);
        }
    }
}
