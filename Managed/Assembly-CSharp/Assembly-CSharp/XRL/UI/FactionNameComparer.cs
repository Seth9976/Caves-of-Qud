using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014B1 RID: 5297
	public class FactionNameComparer : IComparer<string>
	{
		// Token: 0x0600D013 RID: 53267 RVA: 0x004CA290 File Offset: 0x004C8490
		public int Compare(string F1, string F2)
		{
			if (F1 == F2)
			{
				return 0;
			}
			int num = Factions.Get(F1).DisplayName.CompareTo(Factions.Get(F2).DisplayName);
			if (num != 0)
			{
				return num;
			}
			return F1.CompareTo(F2);
		}
	}
}
