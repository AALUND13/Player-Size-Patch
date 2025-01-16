using BepInEx.Configuration;
using TMPro;
using UnboundLib;
using UnboundLib.Utils.UI;
using UnityEngine;

namespace PlayerSizePatch {
    internal class ConfigMenu {
        public static ConfigEntry<float> CapPlayerSizePatch;

        public static void RegisterMenu() {
            CapPlayerSizePatch = PlayerSizePatch.instance.Config.Bind(PlayerSizePatch.modName, "CapPlayerSizePatch", 10f, "Cap the player size to this value, 0 is no cap");

            Unbound.RegisterMenu(PlayerSizePatch.modName, () => { }, CreateMenu, null, false);
        }

        private static void CreateMenu(GameObject menu) {
            MenuHandler.CreateText("<b>Player Size Patch</b>", menu, out _, 70);
            AddBlank(menu, 50);

            MenuHandler.CreateSlider("Cap Player Size", menu, 30, 0, 25, CapPlayerSizePatch.Value, value => CapPlayerSizePatch.Value = value, out _);
            MenuHandler.CreateText("Tip: Set the value to 0 to disable the cap", menu, out _, 20);
        }

        private static void AddBlank(GameObject menu, int size = 30) {
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, size);
        }
    }
}
