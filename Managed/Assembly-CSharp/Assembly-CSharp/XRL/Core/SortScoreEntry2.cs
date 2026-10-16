using System;
using System.Collections.Generic;

namespace XRL.Core
{
	// Token: 0x02000635 RID: 1589
	public class SortScoreEntry2 : Comparer<ScoreEntry2>
	{
		// Token: 0x06004288 RID: 17032 RVA: 0x00160038 File Offset: 0x0015E238
		public override int Compare(ScoreEntry2 x, ScoreEntry2 y)
		{
			if (x.Score < y.Score)
			{
				return 1;
			}
			if (x.Score > y.Score)
			{
				return -1;
			}
			return x.Details.CompareTo(y.Details);
		}
	}
}
