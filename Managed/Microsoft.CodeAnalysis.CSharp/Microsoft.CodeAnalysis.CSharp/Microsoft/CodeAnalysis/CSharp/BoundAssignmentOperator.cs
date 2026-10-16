using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C8 RID: 200
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAssignmentOperator : BoundExpression
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x0005D78A File Offset: 0x0005B98A
		public BoundAssignmentOperator(SyntaxNode syntax, BoundExpression left, BoundExpression right, TypeSymbol type, bool isRef = false, bool hasErrors = false)
			: this(syntax, left, right, isRef, type, hasErrors)
		{
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0005D79B File Offset: 0x0005B99B
		public BoundAssignmentOperator(SyntaxNode syntax, BoundExpression left, BoundExpression right, bool isRef, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.AssignmentOperator, syntax, type, hasErrors || left.HasErrors() || right.HasErrors())
		{
			this.Left = left;
			this.Right = right;
			this.IsRef = isRef;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0005D7D3 File Offset: 0x0005B9D3
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0005D7DB File Offset: 0x0005B9DB
		public BoundExpression Left { get; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0005D7E3 File Offset: 0x0005B9E3
		public BoundExpression Right { get; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0005D7EB File Offset: 0x0005B9EB
		public bool IsRef { get; }

		// Token: 0x060010F3 RID: 4339 RVA: 0x0005D7F3 File Offset: 0x0005B9F3
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAssignmentOperator(this);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0005D7FC File Offset: 0x0005B9FC
		public BoundAssignmentOperator Update(BoundExpression left, BoundExpression right, bool isRef, TypeSymbol type)
		{
			if (left != this.Left || right != this.Right || isRef != this.IsRef || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAssignmentOperator boundAssignmentOperator = new BoundAssignmentOperator(this.Syntax, left, right, isRef, type, base.HasErrors);
				boundAssignmentOperator.CopyAttributes(this);
				return boundAssignmentOperator;
			}
			return this;
		}
	}
}
