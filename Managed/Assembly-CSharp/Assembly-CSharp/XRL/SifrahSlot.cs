using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL
{
	/// This class is not used in the base game.
	// Token: 0x0200051A RID: 1306
	[Serializable]
	public class SifrahSlot : SifrahSlotConfiguration
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x0011160B File Offset: 0x0010F80B
		public bool Solved
		{
			get
			{
				return this.Moves.Count > 0 && this.Moves[this.Moves.Count - 1] == this.Token;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x0011163D File Offset: 0x0010F83D
		public int SolvedOnTurn
		{
			get
			{
				if (!this.Solved)
				{
					return -1;
				}
				return this.Moves.Count;
			}
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x00111654 File Offset: 0x0010F854
		public SifrahSlot()
		{
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x0011166E File Offset: 0x0010F86E
		public SifrahSlot(SifrahSlotConfiguration From)
			: base(From)
		{
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x00111689 File Offset: 0x0010F889
		public SifrahSlot(SifrahSlotConfiguration From, int NumberOfTokens)
			: this(From)
		{
			this.Token = Stat.Random(0, NumberOfTokens - 1);
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x001116A4 File Offset: 0x0010F8A4
		public static List<SifrahSlot> GenerateListFromConfigurations(List<SifrahSlotConfiguration> Configurations, int NumberOfSlots, int NumberOfTokens)
		{
			if (NumberOfSlots > Configurations.Count)
			{
				NumberOfSlots = Configurations.Count;
			}
			bool flag = false;
			int i = 0;
			int count = Configurations.Count;
			while (i < count)
			{
				if (Configurations[i].UptakeOrder != 0)
				{
					flag = true;
					break;
				}
				i++;
			}
			List<int> list = null;
			Dictionary<SifrahSlot, int> sortOrder = null;
			if (flag)
			{
				list = new List<int>(Configurations.Count);
				int j = 0;
				int count2 = Configurations.Count;
				while (j < count2)
				{
					list.Add(0);
					j++;
				}
				int k = 0;
				int count3 = Configurations.Count;
				while (k < count3)
				{
					list[Configurations[k].UptakeOrder] = k;
					k++;
				}
				sortOrder = new Dictionary<SifrahSlot, int>(NumberOfSlots);
			}
			List<SifrahSlot> list2 = new List<SifrahSlot>(NumberOfSlots);
			for (int l = 0; l < NumberOfSlots; l++)
			{
				int num = (flag ? list[l] : l);
				SifrahSlot sifrahSlot = new SifrahSlot(Configurations[num], NumberOfTokens);
				list2.Add(sifrahSlot);
				if (flag)
				{
					sortOrder[sifrahSlot] = num;
				}
			}
			if (flag)
			{
				list2.Sort((SifrahSlot a, SifrahSlot b) => sortOrder[a].CompareTo(sortOrder[b]));
			}
			return list2;
		}

		// Token: 0x04001C0B RID: 7179
		public int Token;

		// Token: 0x04001C0C RID: 7180
		public List<int> Moves = new List<int>();

		// Token: 0x04001C0D RID: 7181
		public int CurrentMove = -1;
	}
}
