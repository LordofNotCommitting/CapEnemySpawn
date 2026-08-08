using HarmonyLib;
using MGSC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace CapEnemySpawn
{
    [HarmonyPatch(typeof(MissionSystem))] // Replace TargetClassName with the actual class name
    public static class CapTechDisplay
    {

        static int tech_Cap = Plugin.ConfigGeneral.ModData.GetConfigValue<int>("Faction_Missiongen_Tech_Cap", 10);
        // Overload 1: Uses string locationId
        [HarmonyPatch(nameof(MissionSystem.GetFactionEquipmentId),
            new Type[] { typeof(Factions), typeof(string), typeof(Mission), typeof(string), typeof(int) },
            new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out, ArgumentType.Out })]

        // Overload 2: Uses LocationMetadata locationMetadata
        [HarmonyPatch(nameof(MissionSystem.GetFactionEquipmentId),
            new Type[] { typeof(Factions), typeof(LocationMetadata), typeof(Mission), typeof(string), typeof(int) },
            new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out, ArgumentType.Out })]

        [HarmonyPostfix]
        public static void Postfix(ref int baseTechLevel, Mission mission)
        {
            if (!mission.IsStoryMission)
            {
                if (baseTechLevel >= tech_Cap)
                {
                    baseTechLevel = tech_Cap;
                }
            }
        }
    }
}


