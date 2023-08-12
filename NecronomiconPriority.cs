using BepInEx;
using BepInEx.Logging;
using Game;
using Game.Characters;
using Game.Interface;
using Game.Services;
using Game.Simulation;
using HarmonyLib;
using Home.Common.Dialog;
using Home.HomeScene;
using Home.Services;
using Home.Shared;

using Server.Shared.Info;
using Server.Shared.State;
using Services;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TownieLib;

namespace NecronomiconPriority;

using System.Diagnostics;

using Server.Shared.Extensions;



[BepInPlugin(ModBuildInfo.GUID, ModBuildInfo.Name, ModBuildInfo.Version)]
[BepInProcess("TownOfSalem2.exe")]
public class NecronomiconPriority : BaseUnityPlugin
{



    public static ManualLogSource modLog = new ManualLogSource(ModBuildInfo.Name);

    //private static readonly MethodInfo _userJoinMethod = typeof(Service).Get("OnCharacterCreated", BindingFlags.Static | BindingFlags.Public);
    internal void Awake()
    {
        BepInEx.Logging.Logger.Sources.Add(modLog);
        try
        {
            Harmony.CreateAndPatchAll(typeof(NecronomiconPriority));
        }
        catch (Exception e)
        {
            Logger.LogError(e);
        }

        // Plugin startup logic
        modLog.LogInfo($"{ModBuildInfo.GUID} is loaded!");
        SceneManager.sceneLoaded += OnSceneLoaded;

    }



    private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {

    }


    public static void InvokePlayerJoin(TosCharacter __instance)
    {
        OnPlayerJoin?.Invoke(__instance);
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static event Action<TosCharacter> OnPlayerJoin;



    [HarmonyPatch(typeof(RoleListItem), "SetRole")]
    [HarmonyPostfix]
    public static void PatchSetRoles(RoleListItem __instance, ref Role ___role)
    {


        var NecronomiconPriority =
            Traverse.Create(typeof(SharedRoleData)).Field<List<Role>>("necronomiconPriority").Value;
        foreach (var _Necro in NecronomiconPriority)
        {
            if (___role == _Necro)
            {
                var oldText = __instance.roleLabel.text;
                modLog.LogInfo($"Completed [{NecronomiconPriority.IndexOf(_Necro) + 1}] {___role}.");
                __instance.roleLabel.SetText($"{NecronomiconPriority.IndexOf(_Necro) + 1} {oldText}");
            }
        }

    }

    [HarmonyPatch(typeof(TosAbilityPanel), "Update")]
    [HarmonyPostfix]

    public static void PatchAbilityPanel(TosAbilityPanel __instance)
    {

        var NecronomiconPriority =
            Traverse.Create(typeof(SharedRoleData)).Field<List<Role>>("necronomiconPriority").Value;

        foreach (var _Players in __instance.playerListPlayers)
        {
            foreach (var _Necro in NecronomiconPriority)
            {

                if (_Players.playerRole == _Necro)
                {
                    if (!_Players.playerRoleText.text.Contains($" [{NecronomiconPriority.IndexOf(_Necro) + 1}] {_Players.playerRole.ToColorizedDisplayString(_Players.playerRole.GetFaction())}"))
                    {
                        _Players.playerRoleText.text =
                            $" [{NecronomiconPriority.IndexOf(_Necro) + 1}] {_Players.playerRole.ToColorizedDisplayString(_Players.playerRole.GetFaction())}";
                    }
                }

            }
        }
    }
}

