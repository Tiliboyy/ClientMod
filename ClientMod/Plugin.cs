using System.IO;
using System.Reflection;

using ClientMod;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using MelonLoader;
using Directory = Il2CppSystem.IO.Directory;
using Logger = UnityEngine.Yoga.Logger;

[assembly: MelonInfo(typeof(ClientMod.ClientMod), "ClientMod", "0.0.1", "Tiliboyy")]
[assembly: MelonGame("Northwood", "SCPSL")]
namespace ClientMod
{
    public class ClientMod : MelonMod
    {
        public static ClientMod Instance;
        public Il2CppAssetBundle Il2CppAssetBundle;

        public override void OnInitializeMelon()
        {
            HarmonyInstance.PatchAll();
            Il2CppAssetBundle = Il2CppAssetBundleManager.LoadFromFile(Path.Combine(Directory.GetCurrentDirectory(), "v1"));
            Instance = this;
            LoggerInstance.Msg("Im here!");
        }

    }


}
