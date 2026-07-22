using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace NoTankControls
{
    public class NoTankControls : ResoniteMod
    {
        private static Assembly ModAssembly => typeof(NoTankControls).Assembly;
        public override string Name => ModAssembly.GetCustomAttribute<AssemblyTitleAttribute>()!.Title;
	    public override string Author => ModAssembly.GetCustomAttribute<AssemblyCompanyAttribute>()!.Company;
	    public override string Version => ModAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;

	    public override string Link => ModAssembly.GetCustomAttributes<AssemblyMetadataAttribute>().First(meta => meta.Key == "RepositoryUrl").Value!;

        public static ModConfiguration Config;

        [AutoRegisterConfigKey]
		private static ModConfigurationKey<bool> MOD_ENABLED = new ModConfigurationKey<bool>("MOD_ENABLED", "Mod Enabled:", () => true);

        public override void OnEngineInit()
        {
            Config = GetConfiguration();
            Harmony harmony = new Harmony($"{ModAssembly.GetCustomAttributes<AssemblyMetadataAttribute>().First(meta => meta.Key == "PackageId").Value!}");
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