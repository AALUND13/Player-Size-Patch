using BepInEx.Configuration;
using TMPro;
using UnboundLib;
using UnboundLib.Utils.UI;
using UnityEngine;

namespace PlayerSizePatch {
    internal class ConfigMenu {
        public static ConfigEntry<float> CapPlayerSize;
        public static ConfigEntry<float> ScaleThreshold;

        public static void RegisterMenu() {
            CapPlayerSize = PlayerSizePatch.instance.Config.Bind(PlayerSizePatch.modName, "CapPlayerSize", 10f, "Cap the player size to this value, 0 is no cap");
            ScaleThreshold = PlayerSizePatch.instance.Config.Bind(PlayerSizePatch.modName, "ScaleThreshold", 0.3f, "Minimum scale threshold for player size");

            Unbound.RegisterMenu(PlayerSizePatch.modName, () => { }, CreateMenu, null, false);
        }

        private static void CreateMenu(GameObject menu) {
            MenuHandler.CreateText("<b>Player Size Patch</b>", menu, out _, 70);
            AddBlank(menu, 50);

            MenuHandler.CreateSlider("Cap Player Size", menu, 30, 0, 25, CapPlayerSize.Value, value => CapPlayerSize.Value = value, out _);
            MenuHandler.CreateText("Tip: Set the value to 0 to disable the cap", menu, out _, 20);
            AddBlank(menu, 30);

            MenuHandler.CreateSlider("Scale Threshold", menu, 30, 0.1f, 1f, ScaleThreshold.Value, value => ScaleThreshold.Value = value, out _);
            MenuHandler.CreateText("Tip: Set the value to 0 to disable the threshold", menu, out _, 20);
        }

        private static void AddBlank(GameObject menu, int size = 30) {
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, size);
        }
    }
}
