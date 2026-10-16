using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000208 RID: 520
	internal abstract class CapturedSymbolReplacement
	{
		// Token: 0x060025AA RID: 9642 RVA: 0x000CDDE4 File Offset: 0x000CBFE4
		public CapturedSymbolReplacement(bool isReusable)
		{
			this.IsReusable = isReusable;
		}

		// Token: 0x060025AB RID: 9643
		public abstract BoundExpression Replacement(SyntaxNode node, Func<NamedTypeSymbol, BoundExpression> makeFrame);

		// Token: 0x040010E6 RID: 4326
		public readonly bool IsReusable;
	}
}
