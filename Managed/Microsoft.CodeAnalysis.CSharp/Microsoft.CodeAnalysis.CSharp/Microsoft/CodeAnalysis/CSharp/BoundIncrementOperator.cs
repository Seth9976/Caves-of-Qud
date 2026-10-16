using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200009A RID: 154
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundIncrementOperator : BoundExpression
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00053F3B File Offset: 0x0005213B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.MethodOpt;
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00053F44 File Offset: 0x00052144
		public BoundIncrementOperator(CSharpSyntaxNode syntax, UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, Conversion operandConversion, Conversion resultConversion, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operatorKind, operand, methodOpt, operandConversion, resultConversion, resultKind, default(ImmutableArray<MethodSymbol>), type, hasErrors)
		{
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00053F70 File Offset: 0x00052170
		public BoundIncrementOperator Update(UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, Conversion operandConversion, Conversion resultConversion, LookupResultKind resultKind, TypeSymbol type)
		{
			return this.Update(operatorKind, operand, methodOpt, operandConversion, resultConversion, resultKind, this.OriginalUserDefinedOperatorsOpt, type);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00053F94 File Offset: 0x00052194
		public BoundIncrementOperator(SyntaxNode syntax, UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, Conversion operandConversion, Conversion resultConversion, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.IncrementOperator, syntax, type, hasErrors || operand.HasErrors())
		{
			this.OperatorKind = operatorKind;
			this.Operand = operand;
			this.MethodOpt = methodOpt;
			this.OperandConversion = operandConversion;
			this.ResultConversion = resultConversion;
			this._ResultKind = resultKind;
			this.OriginalUserDefinedOperatorsOpt = originalUserDefinedOperatorsOpt;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00053FEF File Offset: 0x000521EF
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00053FF7 File Offset: 0x000521F7
		public UnaryOperatorKind OperatorKind { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00053FFF File Offset: 0x000521FF
		public BoundExpression Operand { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00054007 File Offset: 0x00052207
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol MethodOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0005400F File Offset: 0x0005220F
		public Conversion OperandConversion { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00054017 File Offset: 0x00052217
		public Conversion ResultConversion { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0005401F File Offset: 0x0005221F
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00054027 File Offset: 0x00052227
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> OriginalUserDefinedOperatorsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0005402F File Offset: 0x0005222F
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitIncrementOperator(this);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00054038 File Offset: 0x00052238
		public BoundIncrementOperator Update(UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, Conversion operandConversion, Conversion resultConversion, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type)
		{
			if (operatorKind != this.OperatorKind || operand != this.Operand || !SymbolEqualityComparer.ConsiderEverything.Equals(methodOpt, this.MethodOpt) || operandConversion != this.OperandConversion || resultConversion != this.ResultConversion || resultKind != this.ResultKind || originalUserDefinedOperatorsOpt != this.OriginalUserDefinedOperatorsOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundIncrementOperator boundIncrementOperator = new BoundIncrementOperator(this.Syntax, operatorKind, operand, methodOpt, operandConversion, resultConversion, resultKind, originalUserDefinedOperatorsOpt, type, base.HasErrors);
				boundIncrementOperator.CopyAttributes(this);
				return boundIncrementOperator;
			}
			return this;
		}

		// Token: 0x040004DD RID: 1245
		private readonly LookupResultKind _ResultKind;
	}
}
