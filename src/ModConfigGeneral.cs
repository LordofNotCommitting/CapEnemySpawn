using MGSC;
using ModConfigMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CapEnemySpawn
{
    // Token: 0x02000006 RID: 6
    public class ModConfigGeneral
    {
        // Token: 0x0600001D RID: 29 RVA: 0x00002840 File Offset: 0x00000A40
        public ModConfigGeneral(string ModName, string ConfigPath)
        {
            this.ModName = ModName;
            this.ModData = new ModConfigData(ConfigPath);
            this.ModData.AddConfigHeader("STRING:General Settings", "general");
            this.ModData.AddConfigValue("general", "about", "STRING:<color=#f51b1b>New missions</color> will have their  enemy count capped as if their power is X (if their power is higher than X).\n");
            this.ModData.AddConfigValue("general", "Faction_Missiongen_Power_Cap", 30000, 500, 100000, "STRING:Set Mission Faction Power Cap", "STRING:New missions will have their enemy count capped as if their power is X (if their power is higher than X).");
            this.ModData.AddConfigValue("general", "about2", "STRING:<color=#f51b1b>The game must be restarted after setting then saving this config to take effect.</color>\n");
            this.ModData.RegisterModConfigData(ModName);
        }
        // Token: 0x04000011 RID: 17
        private string ModName;

        // Token: 0x04000012 RID: 18
        public ModConfigData ModData;

    }
}
