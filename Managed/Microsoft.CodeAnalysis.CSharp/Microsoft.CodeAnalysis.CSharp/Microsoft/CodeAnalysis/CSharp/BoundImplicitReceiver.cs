using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001CA RID: 458
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundImplicitReceiver : BoundExpression
	{
		// Token: 0x06001FD1 RID: 8145 RVA: 0x000A31DD File Offset: 0x000A13DD
		public BoundImplicitReceiver(SyntaxNode syntax, TypeSymbol type, bool hasErrors)
			: base(BoundKind.ImplicitReceiver, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000A31ED File Offset: 0x000A13ED
		public BoundImplicitReceiver(SyntaxNode syntax, TypeSymbol type)
			: base(BoundKind.ImplicitReceiver, syntax, type)
		{
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x000A31FC File Offset: 0x000A13FC
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000A3204 File Offset: 0x000A1404
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitImplicitReceiver(this);
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000A320D File Offset: 0x000A140D
		public BoundImplicitReceiver Update(TypeSymbol type)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundImplicitReceiver boundImplicitReceiver = new BoundImplicitReceiver(this.Syntax, type, base.HasErrors);
				boundImplicitReceiver.CopyAttributes(this);
				return boundImplicitReceiver;
			}
			return this;
		}
	}
}
