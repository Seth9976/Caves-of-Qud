using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000172 RID: 370
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTupleBinaryOperator : BoundExpression
	{
		// Token: 0x06001DF0 RID: 7664 RVA: 0x000A0066 File Offset: 0x0009E266
		public BoundTupleBinaryOperator(SyntaxNode syntax, BoundExpression left, BoundExpression right, BinaryOperatorKind operatorKind, TupleBinaryOperatorInfo.Multiple operators, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.TupleBinaryOperator, syntax, type, hasErrors || left.HasErrors() || right.HasErrors())
		{
			this.Left = left;
			this.Right = right;
			this.OperatorKind = operatorKind;
			this.Operators = operators;
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x000A00A6 File Offset: 0x0009E2A6
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x000A00AE File Offset: 0x0009E2AE
		public BoundExpression Left { get; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x000A00B6 File Offset: 0x0009E2B6
		public BoundExpression Right { get; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x000A00BE File Offset: 0x0009E2BE
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x000A00C6 File Offset: 0x0009E2C6
		public TupleBinaryOperatorInfo.Multiple Operators { get; }

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000A00CE File Offset: 0x0009E2CE
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTupleBinaryOperator(this);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000A00D8 File Offset: 0x0009E2D8
		public BoundTupleBinaryOperator Update(BoundExpression left, BoundExpression right, BinaryOperatorKind operatorKind, TupleBinaryOperatorInfo.Multiple operators, TypeSymbol type)
		{
			if (left != this.Left || right != this.Right || operatorKind != this.OperatorKind || operators != this.Operators || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundTupleBinaryOperator boundTupleBinaryOperator = new BoundTupleBinaryOperator(this.Syntax, left, right, operatorKind, operators, type, base.HasErrors);
				boundTupleBinaryOperator.CopyAttributes(this);
				return boundTupleBinaryOperator;
			}
			return this;
		}
	}
}
