using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200009B RID: 155
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundCompoundAssignmentOperator : BoundExpression
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x000540D8 File Offset: 0x000522D8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.Operator.Method;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000540E8 File Offset: 0x000522E8
		public BoundCompoundAssignmentOperator(SyntaxNode syntax, BinaryOperatorSignature @operator, BoundExpression left, BoundExpression right, Conversion leftConversion, Conversion finalConversion, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: this(syntax, @operator, left, right, leftConversion, finalConversion, resultKind, default(ImmutableArray<MethodSymbol>), type, hasErrors)
		{
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00054114 File Offset: 0x00052314
		public BoundCompoundAssignmentOperator Update(BinaryOperatorSignature @operator, BoundExpression left, BoundExpression right, Conversion leftConversion, Conversion finalConversion, LookupResultKind resultKind, TypeSymbol type)
		{
			return this.Update(@operator, left, right, leftConversion, finalConversion, resultKind, this.OriginalUserDefinedOperatorsOpt, type);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00054138 File Offset: 0x00052338
		public BoundCompoundAssignmentOperator(SyntaxNode syntax, BinaryOperatorSignature @operator, BoundExpression left, BoundExpression right, Conversion leftConversion, Conversion finalConversion, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.CompoundAssignmentOperator, syntax, type, hasErrors || left.HasErrors() || right.HasErrors())
		{
			this.Operator = @operator;
			this.Left = left;
			this.Right = right;
			this.LeftConversion = leftConversion;
			this.FinalConversion = finalConversion;
			this._ResultKind = resultKind;
			this.OriginalUserDefinedOperatorsOpt = originalUserDefinedOperatorsOpt;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0005419C File Offset: 0x0005239C
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x000541A4 File Offset: 0x000523A4
		public BinaryOperatorSignature Operator { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x000541AC File Offset: 0x000523AC
		public BoundExpression Left { get; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x000541B4 File Offset: 0x000523B4
		public BoundExpression Right { get; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x000541BC File Offset: 0x000523BC
		public Conversion LeftConversion { get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x000541C4 File Offset: 0x000523C4
		public Conversion FinalConversion { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000541CC File Offset: 0x000523CC
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000541D4 File Offset: 0x000523D4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> OriginalUserDefinedOperatorsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000541DC File Offset: 0x000523DC
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitCompoundAssignmentOperator(this);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x000541E8 File Offset: 0x000523E8
		public BoundCompoundAssignmentOperator Update(BinaryOperatorSignature @operator, BoundExpression left, BoundExpression right, Conversion leftConversion, Conversion finalConversion, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> originalUserDefinedOperatorsOpt, TypeSymbol type)
		{
			if (@operator != this.Operator || left != this.Left || right != this.Right || leftConversion != this.LeftConversion || finalConversion != this.FinalConversion || resultKind != this.ResultKind || originalUserDefinedOperatorsOpt != this.OriginalUserDefinedOperatorsOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundCompoundAssignmentOperator boundCompoundAssignmentOperator = new BoundCompoundAssignmentOperator(this.Syntax, @operator, left, right, leftConversion, finalConversion, resultKind, originalUserDefinedOperatorsOpt, type, base.HasErrors);
				boundCompoundAssignmentOperator.CopyAttributes(this);
				return boundCompoundAssignmentOperator;
			}
			return this;
		}

		// Token: 0x040004E4 RID: 1252
		private readonly LookupResultKind _ResultKind;
	}
}
