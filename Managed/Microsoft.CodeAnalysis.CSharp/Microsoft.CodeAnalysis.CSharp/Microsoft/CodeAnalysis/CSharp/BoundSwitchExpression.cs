using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001AA RID: 426
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundSwitchExpression : BoundExpression
	{
		// Token: 0x06001F2B RID: 7979 RVA: 0x000A1F93 File Offset: 0x000A0193
		protected BoundSwitchExpression(BoundKind kind, SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms, BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol defaultLabel, bool reportedNotExhaustive, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(kind, syntax, type, hasErrors)
		{
			this.Expression = expression;
			this.SwitchArms = switchArms;
			this.DecisionDag = decisionDag;
			this.DefaultLabel = defaultLabel;
			this.ReportedNotExhaustive = reportedNotExhaustive;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x000A1FC8 File Offset: 0x000A01C8
		public BoundExpression Expression { get; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x000A1FD0 File Offset: 0x000A01D0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundSwitchExpressionArm> SwitchArms
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x000A1FD8 File Offset: 0x000A01D8
		public BoundDecisionDag DecisionDag { get; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x000A1FE0 File Offset: 0x000A01E0
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public LabelSymbol DefaultLabel
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x000A1FE8 File Offset: 0x000A01E8
		public bool ReportedNotExhaustive { get; }
	}
}
