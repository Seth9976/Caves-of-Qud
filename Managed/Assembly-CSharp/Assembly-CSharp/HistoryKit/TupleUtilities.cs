using System;
using System.Collections.Generic;

namespace HistoryKit
{
	// Token: 0x02000365 RID: 869
	public static class TupleUtilities<A, B>
	{
		// Token: 0x06002058 RID: 8280 RVA: 0x0009F03C File Offset: 0x0009D23C
		public static A[] GetFirstArray(List<Tuple<A, B>> list)
		{
			A[] array = new A[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = list[i].Item1;
			}
			return array;
		}
	}
}
