using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200019E RID: 414
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundForEachStatement : BoundLoopStatement
	{
		// Token: 0x06001EDA RID: 7898 RVA: 0x000A1730 File Offset: 0x0009F930
		public BoundForEachStatement(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] ForEachEnumeratorInfo enumeratorInfoOpt, Conversion elementConversion, BoundTypeExpression iterationVariableType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> iterationVariables, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression iterationErrorExpressionOpt, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] BoundForEachDeconstructStep deconstructionOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundAwaitableInfo awaitOpt, BoundStatement body, bool @checked, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors = false)
			: base(BoundKind.ForEachStatement, syntax, breakLabel, continueLabel, hasErrors || iterationVariableType.HasErrors() || iterationErrorExpressionOpt.HasErrors() || expression.HasErrors() || deconstructionOpt.HasErrors() || awaitOpt.HasErrors() || body.HasErrors())
		{
			this.EnumeratorInfoOpt = enumeratorInfoOpt;
			this.ElementConversion = elementConversion;
			this.IterationVariableType = iterationVariableType;
			this.IterationVariables = iterationVariables;
			this.IterationErrorExpressionOpt = iterationErrorExpressionOpt;
			this.Expression = expression;
			this.DeconstructionOpt = deconstructionOpt;
			this.AwaitOpt = awaitOpt;
			this.Body = body;
			this.Checked = @checked;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000A17D3 File Offset: 0x0009F9D3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ForEachEnumeratorInfo EnumeratorInfoOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000A17DB File Offset: 0x0009F9DB
		public Conversion ElementConversion { get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x000A17E3 File Offset: 0x0009F9E3
		public BoundTypeExpression IterationVariableType { get; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000A17EB File Offset: 0x0009F9EB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> IterationVariables
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x000A17F3 File Offset: 0x0009F9F3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression IterationErrorExpressionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000A17FB File Offset: 0x0009F9FB
		public BoundExpression Expression { get; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000A1803 File Offset: 0x0009FA03
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundForEachDeconstructStep DeconstructionOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x000A180B File Offset: 0x0009FA0B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundAwaitableInfo AwaitOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000A1813 File Offset: 0x0009FA13
		public BoundStatement Body { get; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x000A181B File Offset: 0x0009FA1B
		public bool Checked { get; }

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000A1823 File Offset: 0x0009FA23
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitForEachStatement(this);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000A182C File Offset: 0x0009FA2C
		public BoundForEachStatement Update([global::System.Runtime.CompilerServices.Nullable(2)] ForEachEnumeratorInfo enumeratorInfoOpt, Conversion elementConversion, BoundTypeExpression iterationVariableType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> iterationVariables, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression iterationErrorExpressionOpt, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] BoundForEachDeconstructStep deconstructionOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundAwaitableInfo awaitOpt, BoundStatement body, bool @checked, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel)
		{
			if (enumeratorInfoOpt != this.EnumeratorInfoOpt || elementConversion != this.ElementConversion || iterationVariableType != this.IterationVariableType || iterationVariables != this.IterationVariables || iterationErrorExpressionOpt != this.IterationErrorExpressionOpt || expression != this.Expression || deconstructionOpt != this.DeconstructionOpt || awaitOpt != this.AwaitOpt || body != this.Body || @checked != this.Checked || !SymbolEqualityComparer.ConsiderEverything.Equals(breakLabel, base.BreakLabel) || !SymbolEqualityComparer.ConsiderEverything.Equals(continueLabel, base.ContinueLabel))
			{
				BoundForEachStatement boundForEachStatement = new BoundForEachStatement(this.Syntax, enumeratorInfoOpt, elementConversion, iterationVariableType, iterationVariables, iterationErrorExpressionOpt, expression, deconstructionOpt, awaitOpt, body, @checked, breakLabel, continueLabel, base.HasErrors);
				boundForEachStatement.CopyAttributes(this);
				return boundForEachStatement;
			}
			return this;
		}
	}
}
