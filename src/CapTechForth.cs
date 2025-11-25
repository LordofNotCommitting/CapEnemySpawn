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

namespace CapEnemySpawn
{

    [HarmonyPatch(typeof(UnitGenerationSystem), nameof(UnitGenerationSystem.GenerateUnitsByCount))]
    public class CapTechForth
    {
        //steam mod ID 3594238447
        static int tech_Cap = Plugin.ConfigGeneral.ModData.GetConfigValue<int>("Faction_Missiongen_Tech_Cap", 10);

        public static void Prefix(Statistics statistics, Difficulty difficulty, List<string> locationIds, string factionId, int unitsCount, ref int techLevel, UnitGenerationConditions conditions = default(UnitGenerationConditions))
        {
            //if tech above threshold
            if ((techLevel) > tech_Cap)
            {
                //make bonusTechLevel negative so it enforce set tech level during spawn
                techLevel = tech_Cap;
                //Plugin.Logger.Log("fourth, tech spawned with " + techLevel);
            }
        }
        
    }
}


