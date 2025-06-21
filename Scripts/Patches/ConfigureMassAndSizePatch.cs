using HarmonyLib;
using UnboundLib;
using UnityEngine;

namespace PlayerSizePatch.Patches {
    [HarmonyPatch(typeof(CharacterStatModifiers), "ConfigureMassAndSize")]
    internal class ConfigureMassAndSizePatch {
        private static void Postfix(CharacterStatModifiers __instance) {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            float sizeMultiplier = (float)Traverse.Create(__instance).Field("sizeMultiplier").GetValue();

            float scaleMultiplier = 1.2f * Mathf.Pow(data.maxHealth / 100f * 1.2f, 0.2f) * sizeMultiplier;
            scaleMultiplier = Mathf.Clamp(scaleMultiplier, -ConfigMenu.CapPlayerSize.Value, ConfigMenu.CapPlayerSize.Value);
            if(Mathf.Abs(scaleMultiplier) < ConfigMenu.ScaleThreshold.Value) {
                scaleMultiplier = Mathf.Sign(scaleMultiplier) * ConfigMenu.ScaleThreshold.Value;
            }
            data.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);

            float mass = (float)data.playerVel.GetFieldValue("mass");
            data.playerVel.SetFieldValue("mass", Mathf.Clamp(mass, 0.3f, 1e32f));
        }
    }
}
