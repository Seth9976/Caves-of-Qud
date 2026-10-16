using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D9 RID: 217
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundMakeRefOperator : BoundExpression
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0005E425 File Offset: 0x0005C625
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Operand);
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0005E432 File Offset: 0x0005C632
		public BoundMakeRefOperator(SyntaxNode syntax, BoundExpression operand, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.MakeRefOperator, syntax, type, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0005E452 File Offset: 0x0005C652
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0005E45A File Offset: 0x0005C65A
		public BoundExpression Operand { get; }

		// Token: 0x06001174 RID: 4468 RVA: 0x0005E462 File Offset: 0x0005C662
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitMakeRefOperator(this);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0005E46B File Offset: 0x0005C66B
		public BoundMakeRefOperator Update(BoundExpression operand, TypeSymbol type)
		{
			if (operand != this.Operand || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundMakeRefOperator boundMakeRefOperator = new BoundMakeRefOperator(this.Syntax, operand, type, base.HasErrors);
				boundMakeRefOperator.CopyAttributes(this);
				return boundMakeRefOperator;
			}
			return this;
		}
	}
}
