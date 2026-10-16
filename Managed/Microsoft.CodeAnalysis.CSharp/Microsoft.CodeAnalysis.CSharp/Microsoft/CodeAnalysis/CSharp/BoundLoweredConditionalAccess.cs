using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001BE RID: 446
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLoweredConditionalAccess : BoundExpression
	{
		// Token: 0x06001F8E RID: 8078 RVA: 0x000A2A00 File Offset: 0x000A0C00
		public BoundLoweredConditionalAccess(SyntaxNode syntax, BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol hasValueMethodOpt, BoundExpression whenNotNull, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenNullOpt, int id, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.LoweredConditionalAccess, syntax, type, hasErrors || receiver.HasErrors() || whenNotNull.HasErrors() || whenNullOpt.HasErrors())
		{
			this.Receiver = receiver;
			this.HasValueMethodOpt = hasValueMethodOpt;
			this.WhenNotNull = whenNotNull;
			this.WhenNullOpt = whenNullOpt;
			this.Id = id;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x000A2A60 File Offset: 0x000A0C60
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x000A2A68 File Offset: 0x000A0C68
		public BoundExpression Receiver { get; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x000A2A70 File Offset: 0x000A0C70
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol HasValueMethodOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x000A2A78 File Offset: 0x000A0C78
		public BoundExpression WhenNotNull { get; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000A2A80 File Offset: 0x000A0C80
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression WhenNullOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000A2A88 File Offset: 0x000A0C88
		public int Id { get; }

		// Token: 0x06001F95 RID: 8085 RVA: 0x000A2A90 File Offset: 0x000A0C90
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLoweredConditionalAccess(this);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000A2A9C File Offset: 0x000A0C9C
		public BoundLoweredConditionalAccess Update(BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol hasValueMethodOpt, BoundExpression whenNotNull, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenNullOpt, int id, TypeSymbol type)
		{
			if (receiver != this.Receiver || !SymbolEqualityComparer.ConsiderEverything.Equals(hasValueMethodOpt, this.HasValueMethodOpt) || whenNotNull != this.WhenNotNull || whenNullOpt != this.WhenNullOpt || id != this.Id || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundLoweredConditionalAccess boundLoweredConditionalAccess = new BoundLoweredConditionalAccess(this.Syntax, receiver, hasValueMethodOpt, whenNotNull, whenNullOpt, id, type, base.HasErrors);
				boundLoweredConditionalAccess.CopyAttributes(this);
				return boundLoweredConditionalAccess;
			}
			return this;
		}
	}
}
