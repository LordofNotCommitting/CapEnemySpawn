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

    [HarmonyPatch(typeof(SpawnSystem), nameof(SpawnSystem.SpawnMonsters))]
    [HarmonyPatch(new Type[] {
        typeof(Statistics),
        typeof(Difficulty),
        typeof(TurnController),
        typeof(DungeonGeneratedDebugData),
        typeof(MapGrid),
        typeof(Creatures),
        typeof(RaidMetadata),
        typeof(PerkFactory),
        typeof(List<string>),     // locationIds
        typeof(Faction),
        typeof(Mission),
        typeof(float),
        typeof(int),
        typeof(bool),
        typeof(string)
    })]

    public class CapTechSecond
    {
        //steam mod ID 3594238447
        static int tech_Cap = Plugin.ConfigGeneral.ModData.GetConfigValue<int>("Faction_Missiongen_Tech_Cap", 10);

        public static void Prefix(Statistics statistics, Difficulty difficulty, TurnController turnController, DungeonGeneratedDebugData debugData, MapGrid mapGrid, Creatures creatures, RaidMetadata raidMetadata, PerkFactory perkFactory, List<string> locationIds, Faction faction, Mission mission, float pointsToSpawn, ref int bonusTechLevel, bool ignorePrevTechLevels = false, string debugLabel = null)
        {
            //if tech above threshold
            if ((faction.CurrentTechLevel + bonusTechLevel) > tech_Cap)
            {
                //make bonusTechLevel negative so it enforce set tech level during spawn
                bonusTechLevel = tech_Cap - faction.CurrentTechLevel;
            }
        }
        /*
        public static void Postfix(Statistics statistics, Difficulty difficulty, TurnController turnController, DungeonGeneratedDebugData debugData, MapGrid mapGrid, Creatures creatures, RaidMetadata raidMetadata, PerkFactory perkFactory, List<string> locationIds, Faction faction, Mission mission, float pointsToSpawn, ref int bonusTechLevel, bool ignorePrevTechLevels = false, string debugLabel = null)
        {
            Plugin.Logger.Log("tech spawned with " + faction.CurrentTechLevel + bonusTechLevel);
        }
        */
    }
}


