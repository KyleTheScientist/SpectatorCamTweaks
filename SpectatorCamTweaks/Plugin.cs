using BepInEx;
using SpectatorCamTweaks.Patches;
using System;
using UnityEngine;

namespace SpectatorCamTweaks
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void OnEnable()
        {
            ShoulderCamPatch.Enable();
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            ShoulderCamPatch.Disable();
            HarmonyPatches.RemoveHarmonyPatches();
        }
    }
}
