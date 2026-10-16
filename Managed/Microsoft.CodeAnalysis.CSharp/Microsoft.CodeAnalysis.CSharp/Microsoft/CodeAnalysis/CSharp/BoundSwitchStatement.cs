using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000BF RID: 191
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSwitchStatement : BoundStatement, IBoundSwitchStatement
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00056F71 File Offset: 0x00055171
		BoundNode IBoundSwitchStatement.Value
		{
			get
			{
				return this.Expression;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00056F79 File Offset: 0x00055179
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundStatementList> IBoundSwitchStatement.Cases
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return StaticCast<BoundStatementList>.From<BoundSwitchSection>(this.SwitchSections);
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00056F88 File Offset: 0x00055188
		public BoundSwitchStatement(SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> innerLocals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalFunctionSymbol> innerLocalFunctions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchSection> switchSections, BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] BoundSwitchLabel defaultLabel, GeneratedLabelSymbol breakLabel, bool hasErrors = false)
			: base(BoundKind.SwitchStatement, syntax, hasErrors || expression.HasErrors() || switchSections.HasErrors<BoundSwitchSection>() || decisionDag.HasErrors() || defaultLabel.HasErrors())
		{
			this.Expression = expression;
			this.InnerLocals = innerLocals;
			this.InnerLocalFunctions = innerLocalFunctions;
			this.SwitchSections = switchSections;
			this.DecisionDag = decisionDag;
			this.DefaultLabel = defaultLabel;
			this.BreakLabel = breakLabel;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00056FFC File Offset: 0x000551FC
		public BoundExpression Expression { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00057004 File Offset: 0x00055204
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> InnerLocals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0005700C File Offset: 0x0005520C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalFunctionSymbol> InnerLocalFunctions
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00057014 File Offset: 0x00055214
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundSwitchSection> SwitchSections
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0005701C File Offset: 0x0005521C
		public BoundDecisionDag DecisionDag { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00057024 File Offset: 0x00055224
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundSwitchLabel DefaultLabel
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0005702C File Offset: 0x0005522C
		public GeneratedLabelSymbol BreakLabel { get; }

		// Token: 0x06000D9E RID: 3486 RVA: 0x00057034 File Offset: 0x00055234
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSwitchStatement(this);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00057040 File Offset: 0x00055240
		public BoundSwitchStatement Update(BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> innerLocals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalFunctionSymbol> innerLocalFunctions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchSection> switchSections, BoundDecisionDag decisionDag, [global::System.Runtime.CompilerServices.Nullable(2)] BoundSwitchLabel defaultLabel, GeneratedLabelSymbol breakLabel)
		{
			if (expression != this.Expression || innerLocals != this.InnerLocals || innerLocalFunctions != this.InnerLocalFunctions || switchSections != this.SwitchSections || decisionDag != this.DecisionDag || defaultLabel != this.DefaultLabel || !SymbolEqualityComparer.ConsiderEverything.Equals(breakLabel, this.BreakLabel))
			{
				BoundSwitchStatement boundSwitchStatement = new BoundSwitchStatement(this.Syntax, expression, innerLocals, innerLocalFunctions, switchSections, decisionDag, defaultLabel, breakLabel, base.HasErrors);
				boundSwitchStatement.CopyAttributes(this);
				return boundSwitchStatement;
			}
			return this;
		}
	}
}
