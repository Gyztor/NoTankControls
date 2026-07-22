using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine;

namespace NoTankControls
{
    public class NoTankControls : ResoniteMod
    {
        public override string Name => "NoTankControls";
        public override string Author => "zyntaks";
        public override string Version => "2.0.0";
        public override string Link => "https://github.com/Gyztor/NoTankControls";

        public static ModConfiguration Config;

        [AutoRegisterConfigKey]
		private static ModConfigurationKey<bool> MOD_ENABLED = new ModConfigurationKey<bool>("MOD_ENABLED", "Mod Enabled:", () => true);

        public override void OnEngineInit()
        {
            Config = GetConfiguration();
            Harmony harmony = new Harmony("U-xyla.NoTankControls");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(InteractionHandler))]
        [HarmonyPatch("BeforeInputUpdate")]
        class NoTankControlsPatch
        {
            private static void Postfix(InteractionHandler __instance, ref InteractionHandlerInputs ____inputs)
            {
                if (Config.GetValue(MOD_ENABLED)) {
                    ____inputs.Axis.RegisterBlocks = false;
                }
            }
        }
    }
}