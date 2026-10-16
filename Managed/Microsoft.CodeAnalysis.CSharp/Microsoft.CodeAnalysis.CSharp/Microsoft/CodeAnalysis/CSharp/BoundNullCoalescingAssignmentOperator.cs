using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B8 RID: 184
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNullCoalescingAssignmentOperator : BoundExpression
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x00056B84 File Offset: 0x00054D84
		internal bool IsNullableValueTypeAssignment
		{
			get
			{
				TypeSymbol type = this.LeftOperand.Type;
				return type != null && type.IsNullableType() && type.GetNullableUnderlyingType().Equals(this.RightOperand.Type);
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00056BC6 File Offset: 0x00054DC6
		public BoundNullCoalescingAssignmentOperator(SyntaxNode syntax, BoundExpression leftOperand, BoundExpression rightOperand, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.NullCoalescingAssignmentOperator, syntax, type, hasErrors || leftOperand.HasErrors() || rightOperand.HasErrors())
		{
			this.LeftOperand = leftOperand;
			this.RightOperand = rightOperand;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x00056BF6 File Offset: 0x00054DF6
		public BoundExpression LeftOperand { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00056BFE File Offset: 0x00054DFE
		public BoundExpression RightOperand { get; }

		// Token: 0x06000D78 RID: 3448 RVA: 0x00056C06 File Offset: 0x00054E06
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNullCoalescingAssignmentOperator(this);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00056C0F File Offset: 0x00054E0F
		public BoundNullCoalescingAssignmentOperator Update(BoundExpression leftOperand, BoundExpression rightOperand, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (leftOperand != this.LeftOperand || rightOperand != this.RightOperand || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundNullCoalescingAssignmentOperator boundNullCoalescingAssignmentOperator = new BoundNullCoalescingAssignmentOperator(this.Syntax, leftOperand, rightOperand, type, base.HasErrors);
				boundNullCoalescingAssignmentOperator.CopyAttributes(this);
				return boundNullCoalescingAssignmentOperator;
			}
			return this;
		}
	}
}
