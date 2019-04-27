using CVREBeatSaberPlugin.SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVREReplayMod
{
    [Serializable]
    public class Replay
    {
        [Serializable]
        public class SongData
        {
            static Saber a;
            static Saber b;

            public SaberHistoryData.TimeAndPos saberA;
            public SaberHistoryData.TimeAndPos saberB;
            public SongData()
            {
                if (a == null || b == null)
                {
                    SetSabers(GameObject.FindObjectOfType<PlayerController>());
                }
                float timeA = (float)typeof(Saber).GetField("_time", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(a);
                float timeB = (float)typeof(Saber).GetField("_time", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(b);

                saberA.topPos = a.saberBladeTopPos;
                saberA.bottomPos = a.saberBladeBottomPos;
                saberA.time = timeA;
                
                saberB.topPos = b.saberBladeTopPos;
                saberB.bottomPos = b.saberBladeBottomPos;
                saberB.time = timeB;
                Logger.log.Debug("Added SongData! At times: (" + timeA + ", " + timeB + ")");
            }
            public static void SetSabers(PlayerController controller)
            {
                Logger.log.Debug("Setting Sabers for SongData with controller: " + controller);
                if (controller != null)
                {
                    a = controller.SaberForType(Saber.SaberType.SaberA);
                    b = controller.SaberForType(Saber.SaberType.SaberB);
                }
            }
        }
        public List<SongData> Data { get; }
        public BeatmapLevelSO BeatmapData { get; }

        public Replay()
        {
            Data = new List<SongData>();
            BeatmapData = UnityEngine.Object.FindObjectOfType<BeatmapLevelSO>();
        }
        public Replay(BeatmapLevelSO so)
        {
            Data = new List<SongData>();
            BeatmapData = so;
        }
        public void Add(SongData d)
        {
            Data.Add(d);
        }
        public void Add()
        {
            Data.Add(new SongData());
        }

        public void WriteReplay(string file)
        {
            // FInd out how to do this at some point, hopefully using JSONs
        }
        public static Replay LoadReplay(string file)
        {
            // Find out how to do this at some point, hopefully using JSONs
            return null;
        }
    }
}
