using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200010C RID: 268
	internal class LexicalOrderSymbolComparer : IComparer<Symbol>
	{
		// Token: 0x060015A3 RID: 5539 RVA: 0x00074CE3 File Offset: 0x00072EE3
		private LexicalOrderSymbolComparer()
		{
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00074CEC File Offset: 0x00072EEC
		public int Compare(Symbol x, Symbol y)
		{
			if (x == y)
			{
				return 0;
			}
			LexicalSortKey lexicalSortKey = x.GetLexicalSortKey();
			LexicalSortKey lexicalSortKey2 = y.GetLexicalSortKey();
			int num = LexicalSortKey.Compare(lexicalSortKey, lexicalSortKey2);
			if (num != 0)
			{
				return num;
			}
			num = x.Kind.ToSortOrder() - y.Kind.ToSortOrder();
			if (num != 0)
			{
				return num;
			}
			return string.CompareOrdinal(x.Name, y.Name);
		}

		// Token: 0x04000631 RID: 1585
		public static readonly LexicalOrderSymbolComparer Instance = new LexicalOrderSymbolComparer();
	}
}
