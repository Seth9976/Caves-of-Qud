using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000096 RID: 150
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundBinaryOperator : BoundBinaryOperatorBase
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00053A3B File Offset: 0x00051C3B
		public override ConstantValue ConstantValue
		{
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00053A43 File Offset: 0x00051C43
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.MethodOpt;
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00053A4C File Offset: 0x00051C4C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, BoundExpression left, BoundExpression right, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operatorKind, constantValueOpt, methodOpt, resultKind, originalUserDefinedOperatorsOpt, left, right, type, hasErrors)
		{
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00053A70 File Offset: 0x00051C70
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, BoundExpression left, BoundExpression right, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operatorKind, constantValueOpt, methodOpt, resultKind, default(ImmutableArray<MethodSymbol>), left, right, type, hasErrors)
		{
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00053A9C File Offset: 0x00051C9C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundBinaryOperator Update(BinaryOperatorKind operatorKind, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, BoundExpression left, BoundExpression right, TypeSymbol type)
		{
			return this.Update(operatorKind, constantValueOpt, methodOpt, resultKind, this.OriginalUserDefinedOperatorsOpt, left, right, type);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00053AC0 File Offset: 0x00051CC0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundBinaryOperator(SyntaxNode syntax, BinaryOperatorKind operatorKind, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, BoundExpression left, BoundExpression right, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.BinaryOperator, syntax, left, right, type, hasErrors || left.HasErrors() || right.HasErrors())
		{
			this.OperatorKind = operatorKind;
			this.ConstantValueOpt = constantValueOpt;
			this.MethodOpt = methodOpt;
			this._ResultKind = resultKind;
			this.OriginalUserDefinedOperatorsOpt = originalUserDefinedOperatorsOpt;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00053B19 File Offset: 0x00051D19
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00053B21 File Offset: 0x00051D21
		public ConstantValue ConstantValueOpt { get; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00053B29 File Offset: 0x00051D29
		public MethodSymbol MethodOpt { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00053B31 File Offset: 0x00051D31
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00053B39 File Offset: 0x00051D39
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> OriginalUserDefinedOperatorsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00053B41 File Offset: 0x00051D41
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitBinaryOperator(this);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00053B4C File Offset: 0x00051D4C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public BoundBinaryOperator Update(BinaryOperatorKind operatorKind, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, BoundExpression left, BoundExpression right, TypeSymbol type)
		{
			if (operatorKind != this.OperatorKind || constantValueOpt != this.ConstantValueOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(methodOpt, this.MethodOpt) || resultKind != this.ResultKind || originalUserDefinedOperatorsOpt != this.OriginalUserDefinedOperatorsOpt || left != base.Left || right != base.Right || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundBinaryOperator boundBinaryOperator = new BoundBinaryOperator(this.Syntax, operatorKind, constantValueOpt, methodOpt, resultKind, originalUserDefinedOperatorsOpt, left, right, type, base.HasErrors);
				boundBinaryOperator.CopyAttributes(this);
				return boundBinaryOperator;
			}
			return this;
		}

		// Token: 0x040004C8 RID: 1224
		private readonly LookupResultKind _ResultKind;
	}
}
