using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000098 RID: 152
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUserDefinedConditionalLogicalOperator : BoundBinaryOperatorBase
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00053C1C File Offset: 0x00051E1C
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.LogicalOperator;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00053C24 File Offset: 0x00051E24
		public BoundUserDefinedConditionalLogicalOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression left, BoundExpression right, MethodSymbol logicalOperator, MethodSymbol trueOperator, MethodSymbol falseOperator, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operatorKind, logicalOperator, trueOperator, falseOperator, resultKind, originalUserDefinedOperatorsOpt, left, right, type, hasErrors)
		{
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00053C4C File Offset: 0x00051E4C
		public BoundUserDefinedConditionalLogicalOperator Update(BinaryOperatorKind operatorKind, MethodSymbol logicalOperator, MethodSymbol trueOperator, MethodSymbol falseOperator, LookupResultKind resultKind, BoundExpression left, BoundExpression right, TypeSymbol type)
		{
			return this.Update(operatorKind, logicalOperator, trueOperator, falseOperator, resultKind, this.OriginalUserDefinedOperatorsOpt, left, right, type);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00053C74 File Offset: 0x00051E74
		public BoundUserDefinedConditionalLogicalOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, MethodSymbol logicalOperator, MethodSymbol trueOperator, MethodSymbol falseOperator, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, BoundExpression left, BoundExpression right, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.UserDefinedConditionalLogicalOperator, syntax, left, right, type, hasErrors || left.HasErrors() || right.HasErrors())
		{
			this.OperatorKind = operatorKind;
			this.LogicalOperator = logicalOperator;
			this.TrueOperator = trueOperator;
			this.FalseOperator = falseOperator;
			this._ResultKind = resultKind;
			this.OriginalUserDefinedOperatorsOpt = originalUserDefinedOperatorsOpt;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00053CD5 File Offset: 0x00051ED5
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00053CDD File Offset: 0x00051EDD
		public MethodSymbol LogicalOperator { get; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00053CE5 File Offset: 0x00051EE5
		public MethodSymbol TrueOperator { get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00053CED File Offset: 0x00051EED
		public MethodSymbol FalseOperator { get; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00053CF5 File Offset: 0x00051EF5
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00053CFD File Offset: 0x00051EFD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> OriginalUserDefinedOperatorsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00053D05 File Offset: 0x00051F05
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUserDefinedConditionalLogicalOperator(this);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00053D10 File Offset: 0x00051F10
		public BoundUserDefinedConditionalLogicalOperator Update(BinaryOperatorKind operatorKind, MethodSymbol logicalOperator, MethodSymbol trueOperator, MethodSymbol falseOperator, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, BoundExpression left, BoundExpression right, TypeSymbol type)
		{
			if (operatorKind != this.OperatorKind || !SymbolEqualityComparer.ConsiderEverything.Equals(logicalOperator, this.LogicalOperator) || !SymbolEqualityComparer.ConsiderEverything.Equals(trueOperator, this.TrueOperator) || !SymbolEqualityComparer.ConsiderEverything.Equals(falseOperator, this.FalseOperator) || resultKind != this.ResultKind || originalUserDefinedOperatorsOpt != this.OriginalUserDefinedOperatorsOpt || left != base.Left || right != base.Right || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundUserDefinedConditionalLogicalOperator boundUserDefinedConditionalLogicalOperator = new BoundUserDefinedConditionalLogicalOperator(this.Syntax, operatorKind, logicalOperator, trueOperator, falseOperator, resultKind, originalUserDefinedOperatorsOpt, left, right, type, base.HasErrors);
				boundUserDefinedConditionalLogicalOperator.CopyAttributes(this);
				return boundUserDefinedConditionalLogicalOperator;
			}
			return this;
		}

		// Token: 0x040004D0 RID: 1232
		private readonly LookupResultKind _ResultKind;
	}
}
