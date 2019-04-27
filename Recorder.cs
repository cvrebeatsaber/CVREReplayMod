using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CVREReplayMod
{
    public class Recorder : MonoBehaviour
    {
        protected Replay replay;
        private void Awake()
        {
            Logger.log.Debug("Attempting to find BeatmapLevelSO...");
            BeatmapLevelSO d = FindObjectOfType<BeatmapLevelSO>();
            if (d == null)
            {
                Logger.log.Debug("Could not find BeatmapLevelSO! Destroying immediately!");
                Destroy(this);
            }
            else
            {
                Logger.log.Debug("Creating new Replay object...");
                replay = new Replay(d);
            }
        }
        private void LateUpdate()
        {
            if (replay != null)
            {
                replay.Add();
            }
        }
    }
}
