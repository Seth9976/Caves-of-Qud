using System;
using System.Text;
using XRL.World;
using XRL.World.Capabilities;

namespace XRL.Rules
{
	// Token: 0x020005E1 RID: 1505
	public class Strings
	{
		// Token: 0x060040BA RID: 16570 RVA: 0x0014054C File Offset: 0x0013E74C
		public static string HealthStatusColor(GameObject gameObject)
		{
			if (gameObject.baseHitpoints == 0)
			{
				return "Y";
			}
			int num = gameObject.hitpoints * 100 / gameObject.baseHitpoints;
			if (num >= 100)
			{
				return "Y";
			}
			if (num >= 66)
			{
				return "G";
			}
			if (num >= 33)
			{
				return "W";
			}
			if (num >= 15)
			{
				return "R";
			}
			return "r";
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x001405AC File Offset: 0x0013E7AC
		public static int WoundLevelN(GameObject GO)
		{
			if (GO == null)
			{
				return -2;
			}
			if (GO.HasTagOrProperty("NoDamageReadout"))
			{
				return -2;
			}
			if (!GO.HasStat("Hitpoints"))
			{
				return -1;
			}
			if (The.Player == null)
			{
				return -2;
			}
			int num = 100;
			if (GO.baseHitpoints != 0)
			{
				num = GO.hitpoints * 100 / GO.baseHitpoints;
			}
			if (Scanning.HasScanningFor(The.Player, GO))
			{
				return 0;
			}
			if (num < 15)
			{
				return 1;
			}
			if (num < 33)
			{
				return 2;
			}
			if (num < 66)
			{
				return 3;
			}
			if (num < 100)
			{
				return 4;
			}
			return 5;
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x00140630 File Offset: 0x0013E830
		public static string WoundLevel(GameObject GO)
		{
			if (GO == null)
			{
				return "";
			}
			if (GO.HasTagOrProperty("NoDamageReadout"))
			{
				return "";
			}
			if (!GO.HasStat("Hitpoints"))
			{
				return "Undamaged";
			}
			if (The.Player == null)
			{
				return "";
			}
			int num = 100;
			if (GO.baseHitpoints != 0)
			{
				num = GO.hitpoints * 100 / GO.baseHitpoints;
			}
			if (Scanning.HasScanningFor(The.Player, GO))
			{
				Strings.SB.Length = 0;
				Strings.SB.Append("{{");
				if (num < 15)
				{
					Strings.SB.Append('r');
				}
				else if (num < 33)
				{
					Strings.SB.Append('R');
				}
				else if (num < 66)
				{
					Strings.SB.Append('W');
				}
				else if (num < 100)
				{
					Strings.SB.Append('G');
				}
				else
				{
					Strings.SB.Append('Y');
				}
				Strings.SB.Append('|').Append(GO.hitpoints).Append('/')
					.Append(GO.baseHitpoints)
					.Append("}} {{b|")
					.Append('\u0004')
					.Append("}}")
					.Append(Stats.GetCombatAV(GO))
					.Append(" {{K|")
					.Append('\t')
					.Append("}}")
					.Append(Stats.GetCombatDV(GO));
				return Strings.SB.ToString();
			}
			if (GO.HasTag("Creature") && GO.IsOrganic)
			{
				if (num < 15)
				{
					return "{{r|Badly Wounded}}";
				}
				if (num < 33)
				{
					return "{{R|Wounded}}";
				}
				if (num < 66)
				{
					return "{{W|Injured}}";
				}
				if (num < 100)
				{
					return "{{G|Fine}}";
				}
				return "{{Y|Perfect}}";
			}
			else
			{
				if (num < 15)
				{
					return "{{r|Badly Damaged}}";
				}
				if (num < 33)
				{
					return "{{R|Damaged}}";
				}
				if (num < 66)
				{
					return "{{W|Lightly Damaged}}";
				}
				if (num < 100)
				{
					return "{{G|Fine}}";
				}
				return "{{Y|Perfect}}";
			}
		}

		// Token: 0x04001FC0 RID: 8128
		public static StringBuilder SB = new StringBuilder(2048);
	}
}
