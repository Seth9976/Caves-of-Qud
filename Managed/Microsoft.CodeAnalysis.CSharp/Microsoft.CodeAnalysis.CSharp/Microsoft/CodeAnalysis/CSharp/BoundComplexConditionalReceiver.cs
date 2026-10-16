using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C0 RID: 448
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundComplexConditionalReceiver : BoundExpression
	{
		// Token: 0x06001F9D RID: 8093 RVA: 0x000A2B92 File Offset: 0x000A0D92
		public BoundComplexConditionalReceiver(SyntaxNode syntax, BoundExpression valueTypeReceiver, BoundExpression referenceTypeReceiver, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ComplexConditionalReceiver, syntax, type, hasErrors || valueTypeReceiver.HasErrors() || referenceTypeReceiver.HasErrors())
		{
			this.ValueTypeReceiver = valueTypeReceiver;
			this.ReferenceTypeReceiver = referenceTypeReceiver;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x000A2BC5 File Offset: 0x000A0DC5
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x000A2BCD File Offset: 0x000A0DCD
		public BoundExpression ValueTypeReceiver { get; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x000A2BD5 File Offset: 0x000A0DD5
		public BoundExpression ReferenceTypeReceiver { get; }

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000A2BDD File Offset: 0x000A0DDD
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitComplexConditionalReceiver(this);
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000A2BE6 File Offset: 0x000A0DE6
		public BoundComplexConditionalReceiver Update(BoundExpression valueTypeReceiver, BoundExpression referenceTypeReceiver, TypeSymbol type)
		{
			if (valueTypeReceiver != this.ValueTypeReceiver || referenceTypeReceiver != this.ReferenceTypeReceiver || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundComplexConditionalReceiver boundComplexConditionalReceiver = new BoundComplexConditionalReceiver(this.Syntax, valueTypeReceiver, referenceTypeReceiver, type, base.HasErrors);
				boundComplexConditionalReceiver.CopyAttributes(this);
				return boundComplexConditionalReceiver;
			}
			return this;
		}
	}
}
