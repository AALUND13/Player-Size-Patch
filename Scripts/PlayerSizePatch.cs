using BepInEx;
using HarmonyLib;

namespace PlayerSizePatch {
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(modId, modName, "1.0.0")]
    [BepInProcess("Rounds.exe")]
    public class PlayerSizePatch : BaseUnityPlugin {
        private const string modId = "com.aalund13.rounds.playersizepatch";
        internal const string modName = "Player Size Patch";
        internal const string modInitials = "PSP";

        internal static PlayerSizePatch instance;

        void Awake() {
            instance = this;
            new Harmony(modId).PatchAll();

            Debug.Log($"{modName} loaded!");
        }
        void Start() {
            ConfigMenu.RegisterMenu();

            Debug.Log($"{modName} started!");
        }
    }
}