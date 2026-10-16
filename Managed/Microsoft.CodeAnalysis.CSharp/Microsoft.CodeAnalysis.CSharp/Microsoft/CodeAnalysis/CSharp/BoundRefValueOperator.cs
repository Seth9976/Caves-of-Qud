using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000DA RID: 218
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundRefValueOperator : BoundExpression
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0005E4A1 File Offset: 0x0005C6A1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Operand);
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0005E4AE File Offset: 0x0005C6AE
		public BoundRefValueOperator(SyntaxNode syntax, NullableAnnotation nullableAnnotation, BoundExpression operand, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.RefValueOperator, syntax, type, hasErrors || operand.HasErrors())
		{
			this.NullableAnnotation = nullableAnnotation;
			this.Operand = operand;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0005E4D6 File Offset: 0x0005C6D6
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0005E4DE File Offset: 0x0005C6DE
		public NullableAnnotation NullableAnnotation { get; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0005E4E6 File Offset: 0x0005C6E6
		public BoundExpression Operand { get; }

		// Token: 0x0600117B RID: 4475 RVA: 0x0005E4EE File Offset: 0x0005C6EE
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitRefValueOperator(this);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0005E4F7 File Offset: 0x0005C6F7
		public BoundRefValueOperator Update(NullableAnnotation nullableAnnotation, BoundExpression operand, TypeSymbol type)
		{
			if (nullableAnnotation != this.NullableAnnotation || operand != this.Operand || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundRefValueOperator boundRefValueOperator = new BoundRefValueOperator(this.Syntax, nullableAnnotation, operand, type, base.HasErrors);
				boundRefValueOperator.CopyAttributes(this);
				return boundRefValueOperator;
			}
			return this;
		}
	}
}
