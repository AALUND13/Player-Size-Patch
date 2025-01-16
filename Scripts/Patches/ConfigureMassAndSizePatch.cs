using HarmonyLib;
using UnboundLib;
using UnityEngine;

namespace PlayerSizePatch.Patches {
    [HarmonyPatch(typeof(CharacterStatModifiers), "ConfigureMassAndSize")]
    internal class ConfigureMassAndSizePatch {
        private static void Postfix(CharacterStatModifiers __instance) {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            float sizeMultiplier = (float)Traverse.Create(__instance).Field("sizeMultiplier").GetValue();

            __instance.transform.localScale = Vector3.one * Mathf.Clamp(1.2f * Mathf.Pow(data.maxHealth / 100f * 1.2f, 0.2f) * sizeMultiplier, 0.3f, ConfigMenu.CapPlayerSizePatch.Value);

            float mass = (float)data.playerVel.GetFieldValue("mass");
            data.playerVel.SetFieldValue("mass", Mathf.Clamp(mass, 0.3f, 1e32f));
        }
    }
}
