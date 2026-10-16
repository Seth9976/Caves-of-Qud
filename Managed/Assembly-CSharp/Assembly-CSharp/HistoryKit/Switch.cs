using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;

namespace HistoryKit
{
	// Token: 0x02000367 RID: 871
	public class Switch
	{
		// Token: 0x0600205F RID: 8287 RVA: 0x0009F128 File Offset: 0x0009D328
		public static int RandomWhere(params bool[] predicates)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < predicates.Length; i++)
			{
				if (predicates[i])
				{
					list.Add(i);
				}
			}
			if (list.Count > 0)
			{
				return list.GetRandomElement<int>(null);
			}
			return -1;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0009F168 File Offset: 0x0009D368
		public static int SwitchByWeights(params int[] args)
		{
			int num = args.Sum();
			int num2 = Stat.Random(1, num);
			int num3 = 0;
			if (args.Length == 0)
			{
				return 0;
			}
			for (int i = 0; i < args.Length; i++)
			{
				if (num2 <= args[i] + num3)
				{
					return i;
				}
				num3 += args[i];
			}
			return 0;
		}
	}
}
