using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200020A RID: 522
	internal sealed class CapturedToStateMachineFieldReplacement : CapturedSymbolReplacement
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x000CDE44 File Offset: 0x000CC044
		public CapturedToStateMachineFieldReplacement(StateMachineFieldSymbol hoistedField, bool isReusable)
			: base(isReusable)
		{
			this.HoistedField = hoistedField;
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000CDE54 File Offset: 0x000CC054
		public override BoundExpression Replacement(SyntaxNode node, Func<NamedTypeSymbol, BoundExpression> makeFrame)
		{
			BoundExpression boundExpression = makeFrame.Invoke(this.HoistedField.ContainingType);
			FieldSymbol fieldSymbol = this.HoistedField.AsMember((NamedTypeSymbol)boundExpression.Type);
			return new BoundFieldAccess(node, boundExpression, fieldSymbol, null, false);
		}

		// Token: 0x040010E8 RID: 4328
		public readonly StateMachineFieldSymbol HoistedField;
	}
}
