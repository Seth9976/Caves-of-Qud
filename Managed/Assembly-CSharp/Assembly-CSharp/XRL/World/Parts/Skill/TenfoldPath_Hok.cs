using System;
using System.Collections.Generic;
using XRL.World.Tinkering;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013AF RID: 5039
	[Serializable]
	public class TenfoldPath_Hok : BaseInitiatorySkill
	{
		// Token: 0x0600C2A5 RID: 49829 RVA: 0x00471AC9 File Offset: 0x0046FCC9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetTinkeringBonusEvent.ID || ID == PooledEvent<ModifyBitCostEvent>.ID;
		}

		// Token: 0x0600C2A6 RID: 49830 RVA: 0x00471AE8 File Offset: 0x0046FCE8
		public override bool HandleEvent(ModifyBitCostEvent E)
		{
			char c = '\0';
			int num = -1;
			int num2 = 0;
			foreach (KeyValuePair<char, int> keyValuePair in E.Bits)
			{
				int bitTier = BitType.GetBitTier(keyValuePair.Key);
				if (num == -1 || bitTier >= num)
				{
					num = bitTier;
					c = keyValuePair.Key;
				}
				num2 += keyValuePair.Value;
			}
			if (num2 > 1)
			{
				BitCost bits = E.Bits;
				char c2 = c;
				int num3 = bits[c2];
				bits[c2] = num3 - 1;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2A7 RID: 49831 RVA: 0x00471B90 File Offset: 0x0046FD90
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.Type == "BonusMod")
			{
				E.Bonus += 50;
			}
			else if (E.Type == "Repair")
			{
				E.Bonus++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004DA2 RID: 19874
		public const int BONUS_MOD_CHANCE = 50;
	}
}
