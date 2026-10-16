using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000184 RID: 388
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundReadOnlySpanFromArray : BoundExpression
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x000A0AD0 File Offset: 0x0009ECD0
		public BoundReadOnlySpanFromArray(SyntaxNode syntax, BoundExpression operand, MethodSymbol conversionMethod, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ReadOnlySpanFromArray, syntax, type, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
			this.ConversionMethod = conversionMethod;
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x000A0AF8 File Offset: 0x0009ECF8
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x000A0B00 File Offset: 0x0009ED00
		public BoundExpression Operand { get; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000A0B08 File Offset: 0x0009ED08
		public MethodSymbol ConversionMethod { get; }

		// Token: 0x06001E62 RID: 7778 RVA: 0x000A0B10 File Offset: 0x0009ED10
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitReadOnlySpanFromArray(this);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000A0B1C File Offset: 0x0009ED1C
		public BoundReadOnlySpanFromArray Update(BoundExpression operand, MethodSymbol conversionMethod, TypeSymbol type)
		{
			if (operand != this.Operand || !SymbolEqualityComparer.ConsiderEverything.Equals(conversionMethod, this.ConversionMethod) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundReadOnlySpanFromArray boundReadOnlySpanFromArray = new BoundReadOnlySpanFromArray(this.Syntax, operand, conversionMethod, type, base.HasErrors);
				boundReadOnlySpanFromArray.CopyAttributes(this);
				return boundReadOnlySpanFromArray;
			}
			return this;
		}
	}
}
