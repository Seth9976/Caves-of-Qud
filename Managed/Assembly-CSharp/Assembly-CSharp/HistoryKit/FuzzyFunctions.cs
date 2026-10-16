using System;
using XRL.Rules;

namespace HistoryKit
{
	// Token: 0x02000368 RID: 872
	public class FuzzyFunctions
	{
		// Token: 0x06002062 RID: 8290 RVA: 0x0009F1B3 File Offset: 0x0009D3B3
		public static Func<T> DoThisButRarelyDoThat<T>(Func<T> Primary, Func<T> Secondary, string Chance)
		{
			if (If.Chance(Chance, null, null))
			{
				return Secondary;
			}
			return Primary;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0009F1C2 File Offset: 0x0009D3C2
		public static Func<T> DoThisButRarelyDoThat<T>(Func<T> Primary, Func<T> Secondary, int ChanceOneIn)
		{
			if (Stat.Random(1, ChanceOneIn) == 1)
			{
				return Secondary;
			}
			return Primary;
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0009F1D4 File Offset: 0x0009D3D4
		public static void DoThisAndThatInRandomOrder(Action This, Action That)
		{
			If.CoinFlip(delegate
			{
				This();
				That();
			}, delegate
			{
				This();
				That();
			});
		}
	}
}
