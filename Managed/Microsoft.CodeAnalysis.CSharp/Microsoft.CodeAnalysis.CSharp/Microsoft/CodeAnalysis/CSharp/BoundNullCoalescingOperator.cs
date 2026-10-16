using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000173 RID: 371
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNullCoalescingOperator : BoundExpression
	{
		// Token: 0x06001DF8 RID: 7672 RVA: 0x000A013B File Offset: 0x0009E33B
		public BoundNullCoalescingOperator(SyntaxNode syntax, BoundExpression leftOperand, BoundExpression rightOperand, Conversion leftConversion, BoundNullCoalescingOperatorResultKind operatorResultKind, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.NullCoalescingOperator, syntax, type, hasErrors || leftOperand.HasErrors() || rightOperand.HasErrors())
		{
			this.LeftOperand = leftOperand;
			this.RightOperand = rightOperand;
			this.LeftConversion = leftConversion;
			this.OperatorResultKind = operatorResultKind;
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x000A017B File Offset: 0x0009E37B
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000A0183 File Offset: 0x0009E383
		public BoundExpression LeftOperand { get; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000A018B File Offset: 0x0009E38B
		public BoundExpression RightOperand { get; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000A0193 File Offset: 0x0009E393
		public Conversion LeftConversion { get; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000A019B File Offset: 0x0009E39B
		public BoundNullCoalescingOperatorResultKind OperatorResultKind { get; }

		// Token: 0x06001DFE RID: 7678 RVA: 0x000A01A3 File Offset: 0x0009E3A3
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNullCoalescingOperator(this);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000A01AC File Offset: 0x0009E3AC
		public BoundNullCoalescingOperator Update(BoundExpression leftOperand, BoundExpression rightOperand, Conversion leftConversion, BoundNullCoalescingOperatorResultKind operatorResultKind, TypeSymbol type)
		{
			if (leftOperand != this.LeftOperand || rightOperand != this.RightOperand || leftConversion != this.LeftConversion || operatorResultKind != this.OperatorResultKind || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundNullCoalescingOperator boundNullCoalescingOperator = new BoundNullCoalescingOperator(this.Syntax, leftOperand, rightOperand, leftConversion, operatorResultKind, type, base.HasErrors);
				boundNullCoalescingOperator.CopyAttributes(this);
				return boundNullCoalescingOperator;
			}
			return this;
		}
	}
}
