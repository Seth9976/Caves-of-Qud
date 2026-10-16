using System;
using XRL.World;

namespace XRL.Rules
{
	// Token: 0x020005E0 RID: 1504
	public class Stats
	{
		// Token: 0x060040B3 RID: 16563 RVA: 0x001404AE File Offset: 0x0013E6AE
		[Obsolete("Use GameObject.GetMaxCarriedWeight() instead")]
		public static int GetMaxWeight(GameObject GO)
		{
			return GO.GetMaxCarriedWeight();
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x001404B8 File Offset: 0x0013E6B8
		public static int GetCombatDV(GameObject GO)
		{
			if (GO == null)
			{
				return 0;
			}
			Statistic stat = GO.GetStat("DV");
			if (stat == null)
			{
				return 0;
			}
			int num = 6 + stat.Value + GO.StatMod("Agility", 0);
			if (!GO.IsMobile())
			{
				num = -10;
			}
			return num;
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x001404FD File Offset: 0x0013E6FD
		public static int GetCombatAV(GameObject GO)
		{
			return GO.Stat("AV", 0);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x0014050B File Offset: 0x0013E70B
		public static int GetCombatMA(GameObject Object)
		{
			return 4 + Object.Stat("MA", 0) + Object.StatMod("Willpower", 0);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x00140528 File Offset: 0x0013E728
		[Obsolete("The activeAttack parameter has been deprecated, use Mental.PerformAttack instead")]
		public static int GetCombatMA(GameObject GO, bool activeAttack)
		{
			return Stats.GetCombatMA(GO);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x00140530 File Offset: 0x0013E730
		public static int GetSecondaryAttackChance(GameObject Object)
		{
			return Object.GetIntProperty("SecondaryAttackChance", RuleSettings.BASE_SECONDARY_ATTACK_CHANCE);
		}

		// Token: 0x04001FBE RID: 8126
		[Obsolete("Use XRL.Rules.RuleSettings.MAXIMUM_CARRIED_WEIGHT_PER_STRENGTH instead")]
		public const int MAX_WEIGHT_PER_STRENGTH = 15;

		// Token: 0x04001FBF RID: 8127
		[Obsolete("Use XRL.Rules.RuleSettings.BASE_SECONDARY_ATTACK_CHANCE instead")]
		public const int BASE_SECONDARY_ATTACK_CHANCE = 15;
	}
}
