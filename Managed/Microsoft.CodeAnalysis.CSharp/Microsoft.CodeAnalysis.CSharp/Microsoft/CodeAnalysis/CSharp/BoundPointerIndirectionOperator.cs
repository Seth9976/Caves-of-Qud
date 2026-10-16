using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F4 RID: 244
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPointerIndirectionOperator : BoundExpression
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00063C5E File Offset: 0x00061E5E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Operand);
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00063C6B File Offset: 0x00061E6B
		public BoundPointerIndirectionOperator(SyntaxNode syntax, BoundExpression operand, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.PointerIndirectionOperator, syntax, type, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00063C8B File Offset: 0x00061E8B
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00063C93 File Offset: 0x00061E93
		public BoundExpression Operand { get; }

		// Token: 0x060012CD RID: 4813 RVA: 0x00063C9B File Offset: 0x00061E9B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPointerIndirectionOperator(this);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00063CA4 File Offset: 0x00061EA4
		public BoundPointerIndirectionOperator Update(BoundExpression operand, TypeSymbol type)
		{
			if (operand != this.Operand || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundPointerIndirectionOperator boundPointerIndirectionOperator = new BoundPointerIndirectionOperator(this.Syntax, operand, type, base.HasErrors);
				boundPointerIndirectionOperator.CopyAttributes(this);
				return boundPointerIndirectionOperator;
			}
			return this;
		}
	}
}
