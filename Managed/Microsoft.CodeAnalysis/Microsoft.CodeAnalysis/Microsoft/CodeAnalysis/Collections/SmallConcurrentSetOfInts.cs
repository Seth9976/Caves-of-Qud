using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x020004A1 RID: 1185
	[NullableContext(1)]
	[Nullable(0)]
	internal class SmallConcurrentSetOfInts
	{
		// Token: 0x06002EC9 RID: 11977 RVA: 0x00083D04 File Offset: 0x00081F04
		public SmallConcurrentSetOfInts()
		{
			this._v1 = (this._v2 = (this._v3 = (this._v4 = int.MinValue)));
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x00083D40 File Offset: 0x00081F40
		private SmallConcurrentSetOfInts(int initialValue)
		{
			this._v1 = initialValue;
			this._v2 = (this._v3 = (this._v4 = int.MinValue));
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00083D77 File Offset: 0x00081F77
		public bool Contains(int i)
		{
			return SmallConcurrentSetOfInts.Contains(this, i);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00083D80 File Offset: 0x00081F80
		private static bool Contains(SmallConcurrentSetOfInts set, int i)
		{
			SmallConcurrentSetOfInts smallConcurrentSetOfInts = set;
			while (smallConcurrentSetOfInts._v1 != i && smallConcurrentSetOfInts._v2 != i && smallConcurrentSetOfInts._v3 != i && smallConcurrentSetOfInts._v4 != i)
			{
				smallConcurrentSetOfInts = smallConcurrentSetOfInts._next;
				if (smallConcurrentSetOfInts == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x00083DC0 File Offset: 0x00081FC0
		public bool Add(int i)
		{
			return SmallConcurrentSetOfInts.Add(this, i);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x00083DCC File Offset: 0x00081FCC
		private static bool Add(SmallConcurrentSetOfInts set, int i)
		{
			bool flag = false;
			while (!SmallConcurrentSetOfInts.AddHelper(ref set._v1, i, ref flag) && !SmallConcurrentSetOfInts.AddHelper(ref set._v2, i, ref flag) && !SmallConcurrentSetOfInts.AddHelper(ref set._v3, i, ref flag) && !SmallConcurrentSetOfInts.AddHelper(ref set._v4, i, ref flag))
			{
				SmallConcurrentSetOfInts smallConcurrentSetOfInts = set._next;
				if (smallConcurrentSetOfInts == null)
				{
					SmallConcurrentSetOfInts smallConcurrentSetOfInts2 = new SmallConcurrentSetOfInts(i);
					smallConcurrentSetOfInts = Interlocked.CompareExchange<SmallConcurrentSetOfInts>(ref set._next, smallConcurrentSetOfInts2, null);
					if (smallConcurrentSetOfInts == null)
					{
						return true;
					}
				}
				set = smallConcurrentSetOfInts;
			}
			return flag;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x00083E48 File Offset: 0x00082048
		private static bool AddHelper(ref int slot, int i, ref bool added)
		{
			int num = slot;
			if (num == -2147483648)
			{
				num = Interlocked.CompareExchange(ref slot, i, int.MinValue);
				if (num == -2147483648)
				{
					added = true;
					return true;
				}
			}
			return num == i;
		}

		// Token: 0x04001282 RID: 4738
		private int _v1;

		// Token: 0x04001283 RID: 4739
		private int _v2;

		// Token: 0x04001284 RID: 4740
		private int _v3;

		// Token: 0x04001285 RID: 4741
		private int _v4;

		// Token: 0x04001286 RID: 4742
		[Nullable(2)]
		private SmallConcurrentSetOfInts _next;

		// Token: 0x04001287 RID: 4743
		private const int unoccupied = -2147483648;
	}
}
