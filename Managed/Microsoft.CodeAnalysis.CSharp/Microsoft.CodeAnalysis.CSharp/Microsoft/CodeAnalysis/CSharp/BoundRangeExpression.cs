using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000170 RID: 368
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundRangeExpression : BoundExpression
	{
		// Token: 0x06001DE5 RID: 7653 RVA: 0x0009FF6D File Offset: 0x0009E16D
		public BoundRangeExpression([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, BoundExpression leftOperandOpt, BoundExpression rightOperandOpt, MethodSymbol methodOpt, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.RangeExpression, syntax, type, hasErrors || leftOperandOpt.HasErrors() || rightOperandOpt.HasErrors())
		{
			this.LeftOperandOpt = leftOperandOpt;
			this.RightOperandOpt = rightOperandOpt;
			this.MethodOpt = methodOpt;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0009FFA5 File Offset: 0x0009E1A5
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0009FFAD File Offset: 0x0009E1AD
		public BoundExpression LeftOperandOpt { get; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0009FFB5 File Offset: 0x0009E1B5
		public BoundExpression RightOperandOpt { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0009FFBD File Offset: 0x0009E1BD
		public MethodSymbol MethodOpt { get; }

		// Token: 0x06001DEA RID: 7658 RVA: 0x0009FFC5 File Offset: 0x0009E1C5
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitRangeExpression(this);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0009FFD0 File Offset: 0x0009E1D0
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundRangeExpression Update(BoundExpression leftOperandOpt, BoundExpression rightOperandOpt, MethodSymbol methodOpt, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSymbol type)
		{
			if (leftOperandOpt != this.LeftOperandOpt || rightOperandOpt != this.RightOperandOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(methodOpt, this.MethodOpt) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundRangeExpression boundRangeExpression = new BoundRangeExpression(this.Syntax, leftOperandOpt, rightOperandOpt, methodOpt, type, base.HasErrors);
				boundRangeExpression.CopyAttributes(this);
				return boundRangeExpression;
			}
			return this;
		}
	}
}
