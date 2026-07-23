using HarmonyLib;
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using BepisResoniteWrapper;
using FrooxEngine;
using System.Linq;
using System.Reflection;
using BepInEx.Configuration;

namespace NoTankControls
{
    [ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
    [BepInDependency(BepInExResoniteShim.PluginMetadata.GUID)]
    public class NoTankControls : BasePlugin
    {
        internal static ConfigEntry<bool> NoTankControlsEnabled;

        public override void Load()
        {
            NoTankControlsEnabled = Config.Bind("General", "Enabled", true, new ConfigDescription("Enables NoTankControls"));
            HarmonyInstance.PatchAll();
        }

        [HarmonyBefore("BepInEx.Plugin.art0007i.InspectorScroll", "me.art0007i.InspectorScroll")] 
        [HarmonyPatch(typeof(InteractionHandler))]
        [HarmonyPatch("BeforeInputUpdate")]
        class NoTankControlsPatch
        {
            [HarmonyPatch]
            public static void Postfix(InteractionHandler __instance, ref InteractionHandlerInputs ____inputs)
            {
                if (NoTankControlsEnabled.Value) {
                    ____inputs.Axis.RegisterBlocks = false;
                }
            }
        }
    }
}