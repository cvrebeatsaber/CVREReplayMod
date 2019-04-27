using IPA;
using IPA.Config;
using IPA.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace CVREReplayMod
{
    public class Plugin : IBeatSaberPlugin
    {
        internal static Ref<PluginConfig> config;
        internal static IConfigProvider configProvider;

        internal static Recorder recorder;

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        {
            Logger.log = logger;
            configProvider = cfgProvider;

            config = cfgProvider.MakeLink<PluginConfig>((p, v) =>
            {
                if (v.Value == null || v.Value.RegenerateConfig)
                    p.Store(v.Value = new PluginConfig() { RegenerateConfig = false });
                config = v;
            });
        }

        public void OnApplicationStart()
        {
            Logger.log.Debug("OnApplicationStart");
        }

        public void OnApplicationQuit()
        {
            Logger.log.Debug("OnApplicationQuit");
        }

        public void OnFixedUpdate()
        {

        }

        public void OnUpdate()
        {

        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GameCore")
            {
                if (recorder == null)
                {
                    // Record while we are in the game.
                    Logger.log.Debug("Creating recorder...");
                    recorder = new GameObject("Recorder").AddComponent<Recorder>();
                } else
                {
                    // Recorder already exists, we want to playback our replay
                    Logger.log.Debug("Playing back replay...");
                }
            }
            else if (nextScene.name == "MenuCore")
            {
                if (prevScene.name == "GameCore")
                {
                    Logger.log.Debug("Displaying button to view replay...");
                    // Went from Game to Menu, we can create our button to view the replay here.
                }
            } else
            {
                Logger.log.Debug("Destroying recorder...");
                Object.Destroy(recorder);
                recorder = null;
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {

        }

        public void OnSceneUnloaded(Scene scene)
        {

        }
    }
}
