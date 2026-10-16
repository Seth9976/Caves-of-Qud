using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001BD RID: 445
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConditionalAccess : BoundExpression
	{
		// Token: 0x06001F88 RID: 8072 RVA: 0x000A296A File Offset: 0x000A0B6A
		public BoundConditionalAccess(SyntaxNode syntax, BoundExpression receiver, BoundExpression accessExpression, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ConditionalAccess, syntax, type, hasErrors || receiver.HasErrors() || accessExpression.HasErrors())
		{
			this.Receiver = receiver;
			this.AccessExpression = accessExpression;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x000A299D File Offset: 0x000A0B9D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001F8A RID: 8074 RVA: 0x000A29A5 File Offset: 0x000A0BA5
		public BoundExpression Receiver { get; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x000A29AD File Offset: 0x000A0BAD
		public BoundExpression AccessExpression { get; }

		// Token: 0x06001F8C RID: 8076 RVA: 0x000A29B5 File Offset: 0x000A0BB5
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConditionalAccess(this);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000A29BE File Offset: 0x000A0BBE
		public BoundConditionalAccess Update(BoundExpression receiver, BoundExpression accessExpression, TypeSymbol type)
		{
			if (receiver != this.Receiver || accessExpression != this.AccessExpression || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundConditionalAccess boundConditionalAccess = new BoundConditionalAccess(this.Syntax, receiver, accessExpression, type, base.HasErrors);
				boundConditionalAccess.CopyAttributes(this);
				return boundConditionalAccess;
			}
			return this;
		}
	}
}
