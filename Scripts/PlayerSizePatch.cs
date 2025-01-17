using BepInEx;
using HarmonyLib;
using Photon.Pun;
using UnboundLib;
using UnboundLib.Networking;

namespace PlayerSizePatch {
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(modId, modName, "1.0.1")]
    [BepInProcess("Rounds.exe")]
    public class PlayerSizePatch : BaseUnityPlugin {
        private const string modId = "com.aalund13.rounds.playersizepatch";
        internal const string modName = "Player Size Patch";
        internal const string modInitials = "PSP";

        internal static PlayerSizePatch instance;

        void Awake() {
            instance = this;
            new Harmony(modId).PatchAll();
            
            Unbound.RegisterHandshake(modId, HandshakeCompleted);

            Debug.Log($"{modName} loaded!");
        }
        void Start() {
            ConfigMenu.RegisterMenu();

            Debug.Log($"{modName} started!");
        }

        private static void HandshakeCompleted() {
            if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode) {
                NetworkingManager.RPC(typeof(PlayerSizePatch), nameof(SyncSettings), ConfigMenu.CapPlayerSizePatch.Value);
            }
        }

        [UnboundRPC]
        private static void SyncSettings(float capPlayerSize) {
            ConfigMenu.CapPlayerSizePatch.Value = capPlayerSize;
        }
    }
}