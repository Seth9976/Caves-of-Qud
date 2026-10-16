using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A5 RID: 165
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUnconvertedConditionalOperator : BoundExpression
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0005543A File Offset: 0x0005363A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00055442 File Offset: 0x00053642
		public override object Display
		{
			get
			{
				if (base.Type != null)
				{
					return base.Display;
				}
				return MessageID.IDS_FeatureTargetTypedConditional.Localize();
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00055464 File Offset: 0x00053664
		public BoundUnconvertedConditionalOperator(SyntaxNode syntax, BoundExpression condition, BoundExpression consequence, BoundExpression alternative, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, ErrorCode noCommonTypeError, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.UnconvertedConditionalOperator, syntax, type, hasErrors || condition.HasErrors() || consequence.HasErrors() || alternative.HasErrors())
		{
			this.Condition = condition;
			this.Consequence = consequence;
			this.Alternative = alternative;
			this.ConstantValueOpt = constantValueOpt;
			this.NoCommonTypeError = noCommonTypeError;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x000554C0 File Offset: 0x000536C0
		public BoundExpression Condition { get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x000554C8 File Offset: 0x000536C8
		public BoundExpression Consequence { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x000554D0 File Offset: 0x000536D0
		public BoundExpression Alternative { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x000554D8 File Offset: 0x000536D8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x000554E0 File Offset: 0x000536E0
		public ErrorCode NoCommonTypeError { get; }

		// Token: 0x06000CCB RID: 3275 RVA: 0x000554E8 File Offset: 0x000536E8
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUnconvertedConditionalOperator(this);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000554F4 File Offset: 0x000536F4
		public BoundUnconvertedConditionalOperator Update(BoundExpression condition, BoundExpression consequence, BoundExpression alternative, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, ErrorCode noCommonTypeError, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (condition != this.Condition || consequence != this.Consequence || alternative != this.Alternative || constantValueOpt != this.ConstantValueOpt || noCommonTypeError != this.NoCommonTypeError || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundUnconvertedConditionalOperator boundUnconvertedConditionalOperator = new BoundUnconvertedConditionalOperator(this.Syntax, condition, consequence, alternative, constantValueOpt, noCommonTypeError, type, base.HasErrors);
				boundUnconvertedConditionalOperator.CopyAttributes(this);
				return boundUnconvertedConditionalOperator;
			}
			return this;
		}
	}
}
