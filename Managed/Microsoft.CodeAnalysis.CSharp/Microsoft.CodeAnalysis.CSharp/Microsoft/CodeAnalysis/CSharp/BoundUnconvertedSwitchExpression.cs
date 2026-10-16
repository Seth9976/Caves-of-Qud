using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000EA RID: 234
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUnconvertedSwitchExpression : BoundSwitchExpression
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0005F294 File Offset: 0x0005D494
		public override object Display
		{
			get
			{
				if (base.Type != null)
				{
					return base.Display;
				}
				return MessageID.IDS_FeatureSwitchExpression.Localize();
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0005F2B4 File Offset: 0x0005D4B4
		public BoundUnconvertedSwitchExpression(SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms, BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol defaultLabel, bool reportedNotExhaustive, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.UnconvertedSwitchExpression, syntax, expression, switchArms, decisionDag, defaultLabel, reportedNotExhaustive, type, hasErrors || expression.HasErrors() || switchArms.HasErrors<BoundSwitchExpressionArm>() || decisionDag.HasErrors())
		{
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0005F2F2 File Offset: 0x0005D4F2
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUnconvertedSwitchExpression(this);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0005F2FC File Offset: 0x0005D4FC
		public BoundUnconvertedSwitchExpression Update(BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms, BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol defaultLabel, bool reportedNotExhaustive, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (expression != base.Expression || switchArms != base.SwitchArms || decisionDag != base.DecisionDag || !SymbolEqualityComparer.ConsiderEverything.Equals(defaultLabel, base.DefaultLabel) || reportedNotExhaustive != base.ReportedNotExhaustive || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundUnconvertedSwitchExpression boundUnconvertedSwitchExpression = new BoundUnconvertedSwitchExpression(this.Syntax, expression, switchArms, decisionDag, defaultLabel, reportedNotExhaustive, type, base.HasErrors);
				boundUnconvertedSwitchExpression.CopyAttributes(this);
				return boundUnconvertedSwitchExpression;
			}
			return this;
		}
	}
}
