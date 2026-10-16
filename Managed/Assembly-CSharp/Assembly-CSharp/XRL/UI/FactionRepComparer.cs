using System;
using System.Collections.Generic;

namespace XRL.UI
{
	// Token: 0x020014AF RID: 5295
	public class FactionRepComparer : IComparer<string>
	{
		// Token: 0x0600D00F RID: 53263 RVA: 0x004CA1DC File Offset: 0x004C83DC
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
			return F1.CompareTo(F2);
		}
	}
}
