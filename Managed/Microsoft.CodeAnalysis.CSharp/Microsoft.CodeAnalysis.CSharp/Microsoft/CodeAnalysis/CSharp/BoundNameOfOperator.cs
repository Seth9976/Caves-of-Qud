using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000AC RID: 172
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNameOfOperator : BoundExpression
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00055ABD File Offset: 0x00053CBD
		public override ConstantValue ConstantValue
		{
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00055AC5 File Offset: 0x00053CC5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Argument);
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00055AD2 File Offset: 0x00053CD2
		public BoundNameOfOperator(SyntaxNode syntax, BoundExpression argument, ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.NameOfOperator, syntax, type, hasErrors || argument.HasErrors())
		{
			this.Argument = argument;
			this.ConstantValueOpt = constantValueOpt;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00055AFD File Offset: 0x00053CFD
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00055B05 File Offset: 0x00053D05
		public BoundExpression Argument { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x00055B0D File Offset: 0x00053D0D
		public ConstantValue ConstantValueOpt { get; }

		// Token: 0x06000D0A RID: 3338 RVA: 0x00055B15 File Offset: 0x00053D15
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNameOfOperator(this);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00055B20 File Offset: 0x00053D20
		public BoundNameOfOperator Update(BoundExpression argument, ConstantValue constantValueOpt, TypeSymbol type)
		{
			if (argument != this.Argument || constantValueOpt != this.ConstantValueOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundNameOfOperator boundNameOfOperator = new BoundNameOfOperator(this.Syntax, argument, constantValueOpt, type, base.HasErrors);
				boundNameOfOperator.CopyAttributes(this);
				return boundNameOfOperator;
			}
			return this;
		}
	}
}
