using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVREReplayMod
{
    public class ReplayPlayer : MonoBehaviour
    {
        private static PlayerController controller;
        private static BeatmapLevelSO beatmap;

        private Replay replay;
        private int playbackScale;
        private int index;
        
        private void Awake()
        {
            index = 0;
            playbackScale = 1;
        }
        public void Setup(Replay toPlay)
        {
            replay = toPlay;
        }
        private void Update()
        {
            // NEED TO ACTUALLY ENTER THE SONG!
            if (beatmap == null)
            {
                beatmap = FindObjectOfType<BeatmapLevelSO>();
                if (beatmap == null)
                {
                    Logger.log.Debug("Waiting to start playback at right time...");
                    return;
                }
                Logger.log.Debug("Attempting to set the correct information for the current song...");


                Logger.log.Debug("Currently ONLY does non-practice mode runs");
                FindObjectOfType<SoloFreePlayFlowCoordinator>().StartLevel(null, false);

                //var fields = typeof(BeatmapLevelSO).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //foreach (var f in fields)
                //{
                //    f.SetValue(beatmap, f.GetValue(replay.BeatmapData));
                //}
                //Logger.log.Debug("Finished setting all fields of the song BeatmapLevelSO data to the replay version!");
            }
            if (Complete())
            {
                Logger.log.Debug("Replay complete!");
                Destroy(this);
                return;
            }
            if (index == 0)
            {
                Logger.log.Debug("Beginning replay!");
                Logger.log.Debug("Currently does not ignore user-saber input, this will need to be resolved.");
            }
            if (controller == null)
            {
                controller = FindObjectOfType<PlayerController>();
            }
            if (replay != null)
            {
                UpdateSaber(controller.leftSaber);
                UpdateSaber(controller.rightSaber);
            }

            index++;
        }
        private void UpdateSaber(Saber saber)
        {
            Transform top = (Transform)typeof(Saber).GetField("_topPos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(saber);
            Transform bot = (Transform)typeof(Saber).GetField("_bottomPos", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(saber);

            SaberHistoryData.TimeAndPos pos;
            switch (saber.saberType)
            {
                case Saber.SaberType.SaberA:
                    pos = replay.Data[index].saberA;
                    break;
                case Saber.SaberType.SaberB:
                default:
                    pos = replay.Data[index].saberB;
                    break;
            }

            top.position = pos.topPos;
            bot.position = pos.bottomPos;
        }
        public bool Complete()
        {
            return replay.Data.Count <= index;
        }
    }
}
