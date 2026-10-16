using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200020B RID: 523
	internal sealed class CapturedToExpressionSymbolReplacement : CapturedSymbolReplacement
	{
		// Token: 0x060025B0 RID: 9648 RVA: 0x000CDE94 File Offset: 0x000CC094
		public CapturedToExpressionSymbolReplacement(BoundExpression replacement, ImmutableArray<StateMachineFieldSymbol> hoistedFields, bool isReusable)
			: base(isReusable)
		{
			this._replacement = replacement;
			this.HoistedFields = hoistedFields;
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000CDEAB File Offset: 0x000CC0AB
		public override BoundExpression Replacement(SyntaxNode node, Func<NamedTypeSymbol, BoundExpression> makeFrame)
		{
			return this._replacement;
		}

		// Token: 0x040010E9 RID: 4329
		private readonly BoundExpression _replacement;

		// Token: 0x040010EA RID: 4330
		public readonly ImmutableArray<StateMachineFieldSymbol> HoistedFields;
	}
}
