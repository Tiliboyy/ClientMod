using MelonLoader;
using Il2CppSystem.IO;
using UnityEngine;
using Il2CppGameCore;
using HarmonyLib;
using Il2Cpp;
using Il2CppCentralAuth;
using Il2CppSystem;
using Console = System.Console;
using Exception = System.Exception;
using Object = UnityEngine.Object;

namespace ClientMod
{
    [HarmonyLib.HarmonyPatch(typeof(Il2CppGameCore.Console), nameof(Il2CppGameCore.Console.TypeCommand))]
    public class HarmonyPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(string cmd)
        {
            try
            {
                ClientMod.Instance.LoggerInstance.Msg("test");
                Debug.Log("Im here!");
                foreach (var allAssetName in ClientMod.Instance.Il2CppAssetBundle.AllAssetNames())
                {
                    ClientMod.Instance.LoggerInstance.Msg(allAssetName);

                }
                var asset = ClientMod.Instance.Il2CppAssetBundle.LoadAsset<GameObject>("Assets/v1.prefab");
            
                var obj = GameObject.Instantiate(asset);

                obj.name = "Whooo whoo";
                ClientMod.Instance.LoggerInstance.Msg(obj.name);

                ClientMod.Instance.LoggerInstance.Msg(obj.transform.position);
                return true;
            }
            catch (Exception e)
            {
                ClientMod.Instance.LoggerInstance.Error(e);
                throw;
            }
            
        }

    }
    [HarmonyLib.HarmonyPatch(typeof(CentralAuthManager), nameof(CentralAuthManager.Sign))]

    public class AuthBasePatches
    {

        [HarmonyPrefix]
        public static bool OnCentralAuthManagerSign(ref string __result, string ticket)
        {
            CentralAuthManager.Authenticated = true;
            __result = "TICKET";
            return false;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(NewServerBrowser), nameof(NewServerBrowser.OnEnable))]

    public class ServerListPatches
    {
        [HarmonyLib.HarmonyPatch(typeof(NewServerBrowser), nameof(NewServerBrowser.OnEnable))]
        [HarmonyPrefix]
        public static bool OnServerListEnable(NewServerBrowser __instance)
        {
            ClientMod.Instance.LoggerInstance.Msg("ServerList Enable!");
            var filter = __instance.GetComponent<ServerFilter>();
            var gameObject = GameObject.Find("New Main Menu/Servers/Auth Status");
            gameObject.SetActive(false);
            return true;
        }

        [HarmonyPrefix]
        [HarmonyLib.HarmonyPatch(typeof(NewsLoader), nameof(NewsLoader.Start))]
        public static bool Prefix(NewsLoader __instance)
        {
            
            __instance._announcements = new Il2CppSystem.Collections.Generic.List<NewsLoader.Announcement>();
            __instance._announcements.Clear();
            __instance._announcements.Add(new NewsLoader.Announcement(
                $"<color=#dad467>Tilenzio Client</color>",
                "<b><size=20>Welcome to Tilenzio, a SCP:SL modded version.</size></b>\n" +
                "\n<color=#ec0c02>Content is subject to constant changes.</color>",
                "25.05.24",
                "https://scopesl.com", null));
            __instance.ShowAnnouncement(0);

            return false;
        }
    }
    
}