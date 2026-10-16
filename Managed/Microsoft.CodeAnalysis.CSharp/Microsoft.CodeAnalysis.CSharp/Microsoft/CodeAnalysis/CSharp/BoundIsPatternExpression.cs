using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D4 RID: 468
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundIsPatternExpression : BoundExpression
	{
		// Token: 0x06002002 RID: 8194 RVA: 0x000A3714 File Offset: 0x000A1914
		public BoundIsPatternExpression(SyntaxNode syntax, BoundExpression expression, BoundPattern pattern, bool isNegated, BoundDecisionDag decisionDag, LabelSymbol whenTrueLabel, LabelSymbol whenFalseLabel, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.IsPatternExpression, syntax, type, hasErrors || expression.HasErrors() || pattern.HasErrors() || decisionDag.HasErrors())
		{
			this.Expression = expression;
			this.Pattern = pattern;
			this.IsNegated = isNegated;
			this.DecisionDag = decisionDag;
			this.WhenTrueLabel = whenTrueLabel;
			this.WhenFalseLabel = whenFalseLabel;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x000A377B File Offset: 0x000A197B
		public BoundExpression Expression { get; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x000A3783 File Offset: 0x000A1983
		public BoundPattern Pattern { get; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x000A378B File Offset: 0x000A198B
		public bool IsNegated { get; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x000A3793 File Offset: 0x000A1993
		public BoundDecisionDag DecisionDag { get; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x000A379B File Offset: 0x000A199B
		public LabelSymbol WhenTrueLabel { get; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x000A37A3 File Offset: 0x000A19A3
		public LabelSymbol WhenFalseLabel { get; }

		// Token: 0x06002009 RID: 8201 RVA: 0x000A37AB File Offset: 0x000A19AB
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitIsPatternExpression(this);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000A37B4 File Offset: 0x000A19B4
		public BoundIsPatternExpression Update(BoundExpression expression, BoundPattern pattern, bool isNegated, BoundDecisionDag decisionDag, LabelSymbol whenTrueLabel, LabelSymbol whenFalseLabel, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (expression != this.Expression || pattern != this.Pattern || isNegated != this.IsNegated || decisionDag != this.DecisionDag || !SymbolEqualityComparer.ConsiderEverything.Equals(whenTrueLabel, this.WhenTrueLabel) || !SymbolEqualityComparer.ConsiderEverything.Equals(whenFalseLabel, this.WhenFalseLabel) || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundIsPatternExpression boundIsPatternExpression = new BoundIsPatternExpression(this.Syntax, expression, pattern, isNegated, decisionDag, whenTrueLabel, whenFalseLabel, type, base.HasErrors);
				boundIsPatternExpression.CopyAttributes(this);
				return boundIsPatternExpression;
			}
			return this;
		}
	}
}
