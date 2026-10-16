using System;
using XRL.Rules;

namespace HistoryKit
{
	// Token: 0x02000366 RID: 870
	public class If
	{
		// Token: 0x06002059 RID: 8281 RVA: 0x0009F07A File Offset: 0x0009D27A
		public static bool CoinFlip(Action OnTrue = null, Action OnFalse = null)
		{
			if (Stat.Random(1, 100) <= 50)
			{
				if (OnTrue != null)
				{
					OnTrue();
				}
				return true;
			}
			if (OnFalse != null)
			{
				OnFalse();
			}
			return false;
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0009F09D File Offset: 0x0009D29D
		public static bool Chance(string chance, Action OnTrue = null, Action OnFalse = null)
		{
			if (Stat.Chance(chance))
			{
				if (OnTrue != null)
				{
					OnTrue();
				}
				return true;
			}
			if (OnFalse != null)
			{
				OnFalse();
			}
			return false;
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0009F0BC File Offset: 0x0009D2BC
		public static bool Chance(int chance, Action OnTrue = null, Action OnFalse = null)
		{
			if (Stat.Chance(chance))
			{
				if (OnTrue != null)
				{
					OnTrue();
				}
				return true;
			}
			if (OnFalse != null)
			{
				OnFalse();
			}
			return false;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0009F0DB File Offset: 0x0009D2DB
		public static bool OneIn(int chance, Action OnTrue = null, Action OnFalse = null)
		{
			if (Stat.Random(1, chance) == 1)
			{
				if (OnTrue != null)
				{
					OnTrue();
				}
				return true;
			}
			if (OnFalse != null)
			{
				OnFalse();
			}
			return false;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0009F0FC File Offset: 0x0009D2FC
		public static bool d100(int amount, Action OnTrue = null, Action OnFalse = null)
		{
			if (Stat.Random(1, 100) <= amount)
			{
				if (OnTrue != null)
				{
					OnTrue();
				}
				return true;
			}
			if (OnFalse != null)
			{
				OnFalse();
			}
			return false;
		}
	}
}
