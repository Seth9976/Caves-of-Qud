using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000209 RID: 521
	internal sealed class CapturedToFrameSymbolReplacement : CapturedSymbolReplacement
	{
		// Token: 0x060025AC RID: 9644 RVA: 0x000CDDF3 File Offset: 0x000CBFF3
		public CapturedToFrameSymbolReplacement(LambdaCapturedVariable hoistedField, bool isReusable)
			: base(isReusable)
		{
			this.HoistedField = hoistedField;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000CDE04 File Offset: 0x000CC004
		public override BoundExpression Replacement(SyntaxNode node, Func<NamedTypeSymbol, BoundExpression> makeFrame)
		{
			BoundExpression boundExpression = makeFrame.Invoke(this.HoistedField.ContainingType);
			FieldSymbol fieldSymbol = this.HoistedField.AsMember((NamedTypeSymbol)boundExpression.Type);
			return new BoundFieldAccess(node, boundExpression, fieldSymbol, null, false);
		}

		// Token: 0x040010E7 RID: 4327
		public readonly LambdaCapturedVariable HoistedField;
	}
}
