using System;
using System.Collections.Generic;

namespace XRL.UI
{
	// Token: 0x020014B0 RID: 5296
	public class FactionRepComparerRandomSortEqual : IComparer<string>
	{
		// Token: 0x0600D011 RID: 53265 RVA: 0x004CA234 File Offset: 0x004C8434
		public int Compare(string F1, string F2)
		{
			if (F1 == F2)
			{
				return 0;
			}
			int num = The.Game.PlayerReputation.Get(F2).CompareTo(The.Game.PlayerReputation.Get(F1));
			if (num != 0)
			{
				return num;
			}
			if (!50.in100())
			{
				return -1;
			}
			return 1;
		}
	}
}
