using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200008F RID: 143
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLocal : BoundExpression
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0005309F File Offset: 0x0005129F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ConstantValueOpt;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x000530A7 File Offset: 0x000512A7
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.LocalSymbol;
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000530AF File Offset: 0x000512AF
		public BoundLocal(SyntaxNode syntax, LocalSymbol localSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: this(syntax, localSymbol, BoundLocalDeclarationKind.None, constantValueOpt, false, type, hasErrors)
		{
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000530C0 File Offset: 0x000512C0
		public BoundLocal Update(LocalSymbol localSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, TypeSymbol type)
		{
			return this.Update(localSymbol, this.DeclarationKind, constantValueOpt, this.IsNullableUnknown, type);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000530D7 File Offset: 0x000512D7
		public BoundLocal(SyntaxNode syntax, LocalSymbol localSymbol, BoundLocalDeclarationKind declarationKind, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, bool isNullableUnknown, TypeSymbol type, bool hasErrors)
			: base(BoundKind.Local, syntax, type, hasErrors)
		{
			this.LocalSymbol = localSymbol;
			this.DeclarationKind = declarationKind;
			this.ConstantValueOpt = constantValueOpt;
			this.IsNullableUnknown = isNullableUnknown;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00053104 File Offset: 0x00051304
		public BoundLocal(SyntaxNode syntax, LocalSymbol localSymbol, BoundLocalDeclarationKind declarationKind, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, bool isNullableUnknown, TypeSymbol type)
			: base(BoundKind.Local, syntax, type)
		{
			this.LocalSymbol = localSymbol;
			this.DeclarationKind = declarationKind;
			this.ConstantValueOpt = constantValueOpt;
			this.IsNullableUnknown = isNullableUnknown;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0005312F File Offset: 0x0005132F
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00053137 File Offset: 0x00051337
		public LocalSymbol LocalSymbol { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0005313F File Offset: 0x0005133F
		public BoundLocalDeclarationKind DeclarationKind { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00053147 File Offset: 0x00051347
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ConstantValue ConstantValueOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0005314F File Offset: 0x0005134F
		public bool IsNullableUnknown { get; }

		// Token: 0x06000BAD RID: 2989 RVA: 0x00053157 File Offset: 0x00051357
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLocal(this);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00053160 File Offset: 0x00051360
		public BoundLocal Update(LocalSymbol localSymbol, BoundLocalDeclarationKind declarationKind, [global::System.Runtime.CompilerServices.Nullable(2)] ConstantValue constantValueOpt, bool isNullableUnknown, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(localSymbol, this.LocalSymbol) || declarationKind != this.DeclarationKind || constantValueOpt != this.ConstantValueOpt || isNullableUnknown != this.IsNullableUnknown || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundLocal boundLocal = new BoundLocal(this.Syntax, localSymbol, declarationKind, constantValueOpt, isNullableUnknown, type, base.HasErrors);
				boundLocal.CopyAttributes(this);
				return boundLocal;
			}
			return this;
		}
	}
}
