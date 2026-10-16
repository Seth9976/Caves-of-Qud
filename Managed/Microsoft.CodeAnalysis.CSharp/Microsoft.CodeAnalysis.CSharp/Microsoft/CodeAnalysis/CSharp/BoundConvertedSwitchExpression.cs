using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001AC RID: 428
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConvertedSwitchExpression : BoundSwitchExpression
	{
		// Token: 0x06001F39 RID: 7993 RVA: 0x000A20E8 File Offset: 0x000A02E8
		public BoundConvertedSwitchExpression(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol naturalTypeOpt, bool wasTargetTyped, Conversion conversion, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms, BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol defaultLabel, bool reportedNotExhaustive, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ConvertedSwitchExpression, syntax, expression, switchArms, decisionDag, defaultLabel, reportedNotExhaustive, type, hasErrors || expression.HasErrors() || switchArms.HasErrors<BoundSwitchExpressionArm>() || decisionDag.HasErrors())
		{
			this.NaturalTypeOpt = naturalTypeOpt;
			this.WasTargetTyped = wasTargetTyped;
			this.Conversion = conversion;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x000A2140 File Offset: 0x000A0340
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x000A2148 File Offset: 0x000A0348
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeSymbol NaturalTypeOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x000A2150 File Offset: 0x000A0350
		public bool WasTargetTyped { get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x000A2158 File Offset: 0x000A0358
		public Conversion Conversion { get; }

		// Token: 0x06001F3E RID: 7998 RVA: 0x000A2160 File Offset: 0x000A0360
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConvertedSwitchExpression(this);
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000A216C File Offset: 0x000A036C
		public BoundConvertedSwitchExpression Update([global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol naturalTypeOpt, bool wasTargetTyped, Conversion conversion, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms, BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] LabelSymbol defaultLabel, bool reportedNotExhaustive, TypeSymbol type)
		{
			if (!TypeSymbol.Equals(naturalTypeOpt, this.NaturalTypeOpt, TypeCompareKind.ConsiderEverything) || wasTargetTyped != this.WasTargetTyped || conversion != this.Conversion || expression != base.Expression || switchArms != base.SwitchArms || decisionDag != base.DecisionDag || !SymbolEqualityComparer.ConsiderEverything.Equals(defaultLabel, base.DefaultLabel) || reportedNotExhaustive != base.ReportedNotExhaustive || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundConvertedSwitchExpression boundConvertedSwitchExpression = new BoundConvertedSwitchExpression(this.Syntax, naturalTypeOpt, wasTargetTyped, conversion, expression, switchArms, decisionDag, defaultLabel, reportedNotExhaustive, type, base.HasErrors);
				boundConvertedSwitchExpression.CopyAttributes(this);
				return boundConvertedSwitchExpression;
			}
			return this;
		}
	}
}
