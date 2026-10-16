using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000AAE RID: 2734
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060061D4 RID: 25044 RVA: 0x0000259F File Offset: 0x0000079F
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x060061D5 RID: 25045 RVA: 0x001470A0 File Offset: 0x001452A0
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x001470B0 File Offset: 0x001452B0
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x04003A0E RID: 14862
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
