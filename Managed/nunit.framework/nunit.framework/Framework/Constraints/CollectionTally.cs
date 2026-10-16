using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F5 RID: 245
	public class CollectionTally
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x00012634 File Offset: 0x00010834
		public CollectionTally(NUnitEqualityComparer comparer, IEnumerable c)
		{
			this.comparer = comparer;
			foreach (object obj in c)
			{
				this.list.Add(obj);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000126A0 File Offset: 0x000108A0
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000126B0 File Offset: 0x000108B0
		private bool ItemsEqual(object expected, object actual)
		{
			Tolerance @default = Tolerance.Default;
			return this.comparer.AreEqual(expected, actual, ref @default);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000126D4 File Offset: 0x000108D4
		public bool TryRemove(object o)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.ItemsEqual(this.list[i], o))
				{
					this.list.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001271C File Offset: 0x0001091C
		public bool TryRemove(IEnumerable c)
		{
			foreach (object obj in c)
			{
				if (!this.TryRemove(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040001A2 RID: 418
		private readonly List<object> list = new List<object>();

		// Token: 0x040001A3 RID: 419
		private readonly NUnitEqualityComparer comparer;
	}
}
