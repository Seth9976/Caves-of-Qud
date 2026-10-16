using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200027D RID: 637
	[NullableContext(1)]
	[Nullable(0)]
	internal class SyntaxNodeLocationComparer : IComparer<SyntaxNode>
	{
		// Token: 0x06001967 RID: 6503 RVA: 0x0004C2FE File Offset: 0x0004A4FE
		public SyntaxNodeLocationComparer(Compilation compilation)
		{
			this._compilation = compilation;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0004C30D File Offset: 0x0004A50D
		[NullableContext(2)]
		public int Compare(SyntaxNode x, SyntaxNode y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				return this._compilation.CompareSourceLocations(x.GetLocation(), y.GetLocation());
			}
		}

		// Token: 0x04000A1B RID: 2587
		private readonly Compilation _compilation;
	}
}
