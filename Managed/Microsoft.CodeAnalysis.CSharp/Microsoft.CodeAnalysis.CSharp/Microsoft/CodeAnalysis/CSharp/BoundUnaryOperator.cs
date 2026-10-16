using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000099 RID: 153
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUnaryOperator : BoundExpression
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00053DC6 File Offset: 0x00051FC6
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00053DCE File Offset: 0x00051FCE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.MethodOpt;
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00053DD8 File Offset: 0x00051FD8
		public BoundUnaryOperator(SyntaxNode syntax, UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operatorKind, operand, constantValueOpt, methodOpt, resultKind, default(ImmutableArray<MethodSymbol>), type, hasErrors)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00053E01 File Offset: 0x00052001
		public BoundUnaryOperator Update(UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, TypeSymbol type)
		{
			return this.Update(operatorKind, operand, constantValueOpt, methodOpt, resultKind, this.OriginalUserDefinedOperatorsOpt, type);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00053E18 File Offset: 0x00052018
		public BoundUnaryOperator(SyntaxNode syntax, UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.UnaryOperator, syntax, type, hasErrors || operand.HasErrors())
		{
			this.OperatorKind = operatorKind;
			this.Operand = operand;
			this.ConstantValueOpt = constantValueOpt;
			this.MethodOpt = methodOpt;
			this._ResultKind = resultKind;
			this.OriginalUserDefinedOperatorsOpt = originalUserDefinedOperatorsOpt;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00053E6B File Offset: 0x0005206B
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00053E73 File Offset: 0x00052073
		public UnaryOperatorKind OperatorKind { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00053E7B File Offset: 0x0005207B
		public BoundExpression Operand { get; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00053E83 File Offset: 0x00052083
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00053E8B File Offset: 0x0005208B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol MethodOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00053E93 File Offset: 0x00052093
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00053E9B File Offset: 0x0005209B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> OriginalUserDefinedOperatorsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00053EA3 File Offset: 0x000520A3
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUnaryOperator(this);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00053EAC File Offset: 0x000520AC
		public BoundUnaryOperator Update(UnaryOperatorKind operatorKind, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type)
		{
			if (operatorKind != this.OperatorKind || operand != this.Operand || constantValueOpt != this.ConstantValueOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(methodOpt, this.MethodOpt) || resultKind != this.ResultKind || originalUserDefinedOperatorsOpt != this.OriginalUserDefinedOperatorsOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundUnaryOperator boundUnaryOperator = new BoundUnaryOperator(this.Syntax, operatorKind, operand, constantValueOpt, methodOpt, resultKind, originalUserDefinedOperatorsOpt, type, base.HasErrors);
				boundUnaryOperator.CopyAttributes(this);
				return boundUnaryOperator;
			}
			return this;
		}

		// Token: 0x040004D6 RID: 1238
		private readonly LookupResultKind _ResultKind;
	}
}
