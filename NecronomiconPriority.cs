using Game.Characters;
using Game.Interface;
using HarmonyLib;
using SalemModLoader;
using Server.Shared.State;
using Server.Shared.Extensions;

namespace NecronomiconPriority
{

    [SML.Mod.SalemMod]
    public class NecronomiconPriority
    {

        public void Start()
        {
            try
            {
                Harmony.CreateAndPatchAll(typeof(NecronomiconPriority));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Necronomicon Priority is loaded!");

        }





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
                    Console.WriteLine($"Completed [{NecronomiconPriority.IndexOf(_Necro) + 1}] {___role}.");
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

}