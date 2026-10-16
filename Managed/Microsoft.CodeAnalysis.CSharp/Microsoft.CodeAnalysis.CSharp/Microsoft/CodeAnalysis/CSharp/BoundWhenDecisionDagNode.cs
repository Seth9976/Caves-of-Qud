using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001AF RID: 431
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundWhenDecisionDagNode : BoundDecisionDagNode
	{
		// Token: 0x06001F4B RID: 8011 RVA: 0x000A232C File Offset: 0x000A052C
		public BoundWhenDecisionDagNode(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<BoundPatternBinding> bindings, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenExpression, BoundDecisionDagNode whenTrue, [global::System.Runtime.CompilerServices.Nullable(2)] BoundDecisionDagNode whenFalse, bool hasErrors = false)
			: base(BoundKind.WhenDecisionDagNode, syntax, hasErrors || whenExpression.HasErrors() || whenTrue.HasErrors() || whenFalse.HasErrors())
		{
			this.Bindings = bindings;
			this.WhenExpression = whenExpression;
			this.WhenTrue = whenTrue;
			this.WhenFalse = whenFalse;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000A237F File Offset: 0x000A057F
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<BoundPatternBinding> Bindings
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x000A2387 File Offset: 0x000A0587
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression WhenExpression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x000A238F File Offset: 0x000A058F
		public BoundDecisionDagNode WhenTrue { get; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x000A2397 File Offset: 0x000A0597
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundDecisionDagNode WhenFalse
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000A239F File Offset: 0x000A059F
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitWhenDecisionDagNode(this);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x000A23A8 File Offset: 0x000A05A8
		public BoundWhenDecisionDagNode Update([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<BoundPatternBinding> bindings, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenExpression, BoundDecisionDagNode whenTrue, [global::System.Runtime.CompilerServices.Nullable(2)] BoundDecisionDagNode whenFalse)
		{
			if (bindings != this.Bindings || whenExpression != this.WhenExpression || whenTrue != this.WhenTrue || whenFalse != this.WhenFalse)
			{
				BoundWhenDecisionDagNode boundWhenDecisionDagNode = new BoundWhenDecisionDagNode(this.Syntax, bindings, whenExpression, whenTrue, whenFalse, base.HasErrors);
				boundWhenDecisionDagNode.CopyAttributes(this);
				return boundWhenDecisionDagNode;
			}
			return this;
		}
	}
}
