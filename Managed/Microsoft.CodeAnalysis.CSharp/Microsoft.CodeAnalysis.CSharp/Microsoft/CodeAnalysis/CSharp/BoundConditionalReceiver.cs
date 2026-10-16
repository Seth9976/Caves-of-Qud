using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001BF RID: 447
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConditionalReceiver : BoundExpression
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x000A2B15 File Offset: 0x000A0D15
		public BoundConditionalReceiver(SyntaxNode syntax, int id, TypeSymbol type, bool hasErrors)
			: base(BoundKind.ConditionalReceiver, syntax, type, hasErrors)
		{
			this.Id = id;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000A2B2D File Offset: 0x000A0D2D
		public BoundConditionalReceiver(SyntaxNode syntax, int id, TypeSymbol type)
			: base(BoundKind.ConditionalReceiver, syntax, type)
		{
			this.Id = id;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x000A2B43 File Offset: 0x000A0D43
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000A2B4B File Offset: 0x000A0D4B
		public int Id { get; }

		// Token: 0x06001F9B RID: 8091 RVA: 0x000A2B53 File Offset: 0x000A0D53
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConditionalReceiver(this);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000A2B5C File Offset: 0x000A0D5C
		public BoundConditionalReceiver Update(int id, TypeSymbol type)
		{
			if (id != this.Id || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundConditionalReceiver boundConditionalReceiver = new BoundConditionalReceiver(this.Syntax, id, type, base.HasErrors);
				boundConditionalReceiver.CopyAttributes(this);
				return boundConditionalReceiver;
			}
			return this;
		}
	}
}
