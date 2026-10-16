using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000091 RID: 145
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPropertyAccess : BoundExpression
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x000533F2 File Offset: 0x000515F2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.PropertySymbol;
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000533FA File Offset: 0x000515FA
		public BoundPropertyAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol propertySymbol, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.PropertyAccess, syntax, type, hasErrors || receiverOpt.HasErrors())
		{
			this.ReceiverOpt = receiverOpt;
			this.PropertySymbol = propertySymbol;
			this._ResultKind = resultKind;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0005342D File Offset: 0x0005162D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00053435 File Offset: 0x00051635
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0005343D File Offset: 0x0005163D
		public PropertySymbol PropertySymbol { get; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00053445 File Offset: 0x00051645
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0005344D File Offset: 0x0005164D
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPropertyAccess(this);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00053458 File Offset: 0x00051658
		public BoundPropertyAccess Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol propertySymbol, LookupResultKind resultKind, TypeSymbol type)
		{
			if (receiverOpt != this.ReceiverOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(propertySymbol, this.PropertySymbol) || resultKind != this.ResultKind || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundPropertyAccess boundPropertyAccess = new BoundPropertyAccess(this.Syntax, receiverOpt, propertySymbol, resultKind, type, base.HasErrors);
				boundPropertyAccess.CopyAttributes(this);
				return boundPropertyAccess;
			}
			return this;
		}

		// Token: 0x040004B1 RID: 1201
		private readonly LookupResultKind _ResultKind;
	}
}
