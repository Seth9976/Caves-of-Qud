using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D3 RID: 211
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDeconstructionAssignmentOperator : BoundExpression
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0005DED2 File Offset: 0x0005C0D2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Left, this.Right);
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0005DEE5 File Offset: 0x0005C0E5
		public BoundDeconstructionAssignmentOperator(SyntaxNode syntax, BoundTupleExpression left, BoundConversion right, bool isUsed, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DeconstructionAssignmentOperator, syntax, type, hasErrors || left.HasErrors() || right.HasErrors())
		{
			this.Left = left;
			this.Right = right;
			this.IsUsed = isUsed;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0005DF1D File Offset: 0x0005C11D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0005DF25 File Offset: 0x0005C125
		public BoundTupleExpression Left { get; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x0005DF2D File Offset: 0x0005C12D
		public BoundConversion Right { get; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0005DF35 File Offset: 0x0005C135
		public bool IsUsed { get; }

		// Token: 0x06001142 RID: 4418 RVA: 0x0005DF3D File Offset: 0x0005C13D
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDeconstructionAssignmentOperator(this);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0005DF48 File Offset: 0x0005C148
		public BoundDeconstructionAssignmentOperator Update(BoundTupleExpression left, BoundConversion right, bool isUsed, TypeSymbol type)
		{
			if (left != this.Left || right != this.Right || isUsed != this.IsUsed || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDeconstructionAssignmentOperator boundDeconstructionAssignmentOperator = new BoundDeconstructionAssignmentOperator(this.Syntax, left, right, isUsed, type, base.HasErrors);
				boundDeconstructionAssignmentOperator.CopyAttributes(this);
				return boundDeconstructionAssignmentOperator;
			}
			return this;
		}
	}
}
