using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001AE RID: 430
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTestDecisionDagNode : BoundDecisionDagNode
	{
		// Token: 0x06001F45 RID: 8005 RVA: 0x000A2290 File Offset: 0x000A0490
		public BoundTestDecisionDagNode(SyntaxNode syntax, BoundDagTest test, BoundDecisionDagNode whenTrue, BoundDecisionDagNode whenFalse, bool hasErrors = false)
			: base(BoundKind.TestDecisionDagNode, syntax, hasErrors || test.HasErrors() || whenTrue.HasErrors() || whenFalse.HasErrors())
		{
			this.Test = test;
			this.WhenTrue = whenTrue;
			this.WhenFalse = whenFalse;
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x000A22CF File Offset: 0x000A04CF
		public BoundDagTest Test { get; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x000A22D7 File Offset: 0x000A04D7
		public BoundDecisionDagNode WhenTrue { get; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x000A22DF File Offset: 0x000A04DF
		public BoundDecisionDagNode WhenFalse { get; }

		// Token: 0x06001F49 RID: 8009 RVA: 0x000A22E7 File Offset: 0x000A04E7
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTestDecisionDagNode(this);
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000A22F0 File Offset: 0x000A04F0
		public BoundTestDecisionDagNode Update(BoundDagTest test, BoundDecisionDagNode whenTrue, BoundDecisionDagNode whenFalse)
		{
			if (test != this.Test || whenTrue != this.WhenTrue || whenFalse != this.WhenFalse)
			{
				BoundTestDecisionDagNode boundTestDecisionDagNode = new BoundTestDecisionDagNode(this.Syntax, test, whenTrue, whenFalse, base.HasErrors);
				boundTestDecisionDagNode.CopyAttributes(this);
				return boundTestDecisionDagNode;
			}
			return this;
		}
	}
}
