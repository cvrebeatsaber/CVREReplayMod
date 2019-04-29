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
        private const string ControllerPanel = "ResultsViewController/SmallButtons";
        private const string CopyButton = "CreditsButton";
        public static void CreateButton(string text, UnityAction onClick, string hintText)
        {
            Logger.log.Debug("Creating button with message: " + text);
            RectTransform panel = GameObject.Find(ControllerPanel).transform as RectTransform;

            if (panel == null)
            {
                return;
            }

            Button button = BeatSaberUI.CreateUIButton(panel, CopyButton, onClick, text);
            panel.Find(CopyButton).SetAsLastSibling();

            BeatSaberUI.AddHintText(button.transform as RectTransform, hintText);
        }
    }
}
