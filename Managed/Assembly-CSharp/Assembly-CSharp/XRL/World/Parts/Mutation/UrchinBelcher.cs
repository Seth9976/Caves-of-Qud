using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001406 RID: 5126
	[Serializable]
	public class UrchinBelcher : Belcher
	{
		// Token: 0x0600C711 RID: 50961 RVA: 0x00488F8E File Offset: 0x0048718E
		public UrchinBelcher()
		{
			this.EventKey = "CommandBelchUrchins";
			this.Description = "You belch forth various urchins.";
			this.BelchTable = "UrchinsToBelch";
			this.CommandName = "Belch Urchins";
			this.CommandDescription = "You belch forth various urchins.";
		}

		// Token: 0x0600C712 RID: 50962 RVA: 0x00488FD0 File Offset: 0x004871D0
		public override string GetLevelText(int Level)
		{
			return "You belch urchins in a nearby area.\n" + "Number of urchins: 1d2+" + (Level / 4).ToString() + " \n" + "Range: " + this.GetRange(Level).ToString() + "\n" + "Radius: " + this.GetRadius().ToString() + "\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds\n";
		}

		// Token: 0x0600C713 RID: 50963 RVA: 0x00489058 File Offset: 0x00487258
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			if (Level / 4 == 0)
			{
				stats.Set("Amount", "1d2", !flag, 0);
			}
			else
			{
				stats.Set("Amount", "1d2+" + (Level / 4).ToString(), !flag, 0);
			}
			stats.Set("Range", this.GetRange(Level), !flag, 0);
			stats.Set("Radius", this.GetRadius(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}
	}
}
