using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A4 RID: 164
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConditionalOperator : BoundExpression
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x000552BE File Offset: 0x000534BE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x000552C6 File Offset: 0x000534C6
		public bool IsDynamic
		{
			get
			{
				return this.Condition.Kind == BoundKind.UnaryOperator && ((BoundUnaryOperator)this.Condition).OperatorKind.IsDynamic();
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000552F0 File Offset: 0x000534F0
		public BoundConditionalOperator(SyntaxNode syntax, bool isRef, BoundExpression condition, BoundExpression consequence, BoundExpression alternative, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol naturalTypeOpt, bool wasTargetTyped, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ConditionalOperator, syntax, type, hasErrors || condition.HasErrors() || consequence.HasErrors() || alternative.HasErrors())
		{
			this.IsRef = isRef;
			this.Condition = condition;
			this.Consequence = consequence;
			this.Alternative = alternative;
			this.ConstantValueOpt = constantValueOpt;
			this.NaturalTypeOpt = naturalTypeOpt;
			this.WasTargetTyped = wasTargetTyped;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0005535D File Offset: 0x0005355D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00055365 File Offset: 0x00053565
		public bool IsRef { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0005536D File Offset: 0x0005356D
		public BoundExpression Condition { get; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00055375 File Offset: 0x00053575
		public BoundExpression Consequence { get; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0005537D File Offset: 0x0005357D
		public BoundExpression Alternative { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00055385 File Offset: 0x00053585
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0005538D File Offset: 0x0005358D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeSymbol NaturalTypeOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00055395 File Offset: 0x00053595
		public bool WasTargetTyped { get; }

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0005539D File Offset: 0x0005359D
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConditionalOperator(this);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000553A8 File Offset: 0x000535A8
		public BoundConditionalOperator Update(bool isRef, BoundExpression condition, BoundExpression consequence, BoundExpression alternative, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol naturalTypeOpt, bool wasTargetTyped, TypeSymbol type)
		{
			if (isRef != this.IsRef || condition != this.Condition || consequence != this.Consequence || alternative != this.Alternative || constantValueOpt != this.ConstantValueOpt || !TypeSymbol.Equals(naturalTypeOpt, this.NaturalTypeOpt, TypeCompareKind.ConsiderEverything) || wasTargetTyped != this.WasTargetTyped || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundConditionalOperator boundConditionalOperator = new BoundConditionalOperator(this.Syntax, isRef, condition, consequence, alternative, constantValueOpt, naturalTypeOpt, wasTargetTyped, type, base.HasErrors);
				boundConditionalOperator.CopyAttributes(this);
				return boundConditionalOperator;
			}
			return this;
		}
	}
}
