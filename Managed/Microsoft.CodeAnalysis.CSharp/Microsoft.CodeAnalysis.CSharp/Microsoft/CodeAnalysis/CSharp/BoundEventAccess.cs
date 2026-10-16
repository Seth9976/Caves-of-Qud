using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000094 RID: 148
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundEventAccess : BoundExpression
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000538AF File Offset: 0x00051AAF
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.EventSymbol;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000538B7 File Offset: 0x00051AB7
		public BoundEventAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, EventSymbol eventSymbol, bool isUsableAsField, LookupResultKind resultKind, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.EventAccess, syntax, type, hasErrors || receiverOpt.HasErrors())
		{
			this.ReceiverOpt = receiverOpt;
			this.EventSymbol = eventSymbol;
			this.IsUsableAsField = isUsableAsField;
			this._ResultKind = resultKind;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000538F2 File Offset: 0x00051AF2
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x000538FA File Offset: 0x00051AFA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00053902 File Offset: 0x00051B02
		public EventSymbol EventSymbol { get; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0005390A File Offset: 0x00051B0A
		public bool IsUsableAsField { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00053912 File Offset: 0x00051B12
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0005391A File Offset: 0x00051B1A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitEventAccess(this);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00053924 File Offset: 0x00051B24
		public BoundEventAccess Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, EventSymbol eventSymbol, bool isUsableAsField, LookupResultKind resultKind, TypeSymbol type)
		{
			if (receiverOpt != this.ReceiverOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(eventSymbol, this.EventSymbol) || isUsableAsField != this.IsUsableAsField || resultKind != this.ResultKind || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundEventAccess boundEventAccess = new BoundEventAccess(this.Syntax, receiverOpt, eventSymbol, isUsableAsField, resultKind, type, base.HasErrors);
				boundEventAccess.CopyAttributes(this);
				return boundEventAccess;
			}
			return this;
		}

		// Token: 0x040004C3 RID: 1219
		private readonly LookupResultKind _ResultKind;
	}
}
