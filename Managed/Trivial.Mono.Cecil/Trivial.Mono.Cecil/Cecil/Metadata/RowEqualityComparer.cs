using System;
using System.Collections.Generic;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000EB RID: 235
	internal sealed class RowEqualityComparer : IEqualityComparer<Row<string, string>>, IEqualityComparer<Row<uint, uint>>, IEqualityComparer<Row<uint, uint, uint>>
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x0001FC16 File Offset: 0x0001DE16
		public bool Equals(Row<string, string> x, Row<string, string> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001FC40 File Offset: 0x0001DE40
		public int GetHashCode(Row<string, string> obj)
		{
			string col = obj.Col1;
			string col2 = obj.Col2;
			return ((col != null) ? col.GetHashCode() : 0) ^ ((col2 != null) ? col2.GetHashCode() : 0);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001FC74 File Offset: 0x0001DE74
		public bool Equals(Row<uint, uint> x, Row<uint, uint> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001FC94 File Offset: 0x0001DE94
		public int GetHashCode(Row<uint, uint> obj)
		{
			return (int)(obj.Col1 ^ obj.Col2);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001FCA3 File Offset: 0x0001DEA3
		public bool Equals(Row<uint, uint, uint> x, Row<uint, uint, uint> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2 && x.Col3 == y.Col3;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001FCD1 File Offset: 0x0001DED1
		public int GetHashCode(Row<uint, uint, uint> obj)
		{
			return (int)(obj.Col1 ^ obj.Col2 ^ obj.Col3);
		}
	}
}
