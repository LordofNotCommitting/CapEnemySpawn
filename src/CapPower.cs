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

    [HarmonyPatch(typeof(MissionFactory), nameof(MissionFactory.InitDropPoints))]
    public class CapPower
    {
        //steam mod ID 3594238447
        static int power_Cap = Plugin.ConfigGeneral.ModData.GetConfigValue<int>("Faction_Missiongen_Power_Cap", 1);
        //[HarmonyPatch(typeof(WoundSlotRecord), "ImplantSocketsDefault", MethodType.Getter)]
        static Logger temp_log = new Logger();
        public static void Postfix(ref MissionFactory __instance, ref Mission mission, Faction victim)
        {
            Difficulty difficulty = __instance._state.Get<Difficulty>();
            MissionDifficultyRecord missionDifficultyRecord = Data.MissionDifficulty.Get(mission.MissionDifficulty);
            PrizeByRatingRecord prizeByRatingRecord = Data.PrizesByRatings.Get(mission.ProcMissionType, mission.MissionDifficulty);

            //temp_log.Log("Mission gen faction power detected as " + victim.Power +". Setting it as " + power_Cap);
            float temp_power = Mathf.Min(victim.Power, (float)power_Cap);

            int monstersPointsLimit = Mathf.RoundToInt(((float)missionDifficultyRecord.MonsterPointsPerStage + temp_power * prizeByRatingRecord.PowerToMonsterPointsMult) * difficulty.Preset.MonsterPoints);
            foreach (KeyValuePair<string, DungeonGenerationPlan> keyValuePair in mission.LocationPlans)
            {
                keyValuePair.Value.MonstersPointsLimit = monstersPointsLimit;
            }


        }
    }
}


