using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000161 RID: 353
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFieldEqualsValue : BoundEqualsValue
	{
		// Token: 0x06001D9C RID: 7580 RVA: 0x0009F900 File Offset: 0x0009DB00
		public BoundFieldEqualsValue(SyntaxNode syntax, FieldSymbol field, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression value, bool hasErrors = false)
			: base(BoundKind.FieldEqualsValue, syntax, locals, value, hasErrors || value.HasErrors())
		{
			this.Field = field;
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0009F922 File Offset: 0x0009DB22
		public FieldSymbol Field { get; }

		// Token: 0x06001D9E RID: 7582 RVA: 0x0009F92A File Offset: 0x0009DB2A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFieldEqualsValue(this);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0009F934 File Offset: 0x0009DB34
		public BoundFieldEqualsValue Update(FieldSymbol field, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression value)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(field, this.Field) || locals != base.Locals || value != base.Value)
			{
				BoundFieldEqualsValue boundFieldEqualsValue = new BoundFieldEqualsValue(this.Syntax, field, locals, value, base.HasErrors);
				boundFieldEqualsValue.CopyAttributes(this);
				return boundFieldEqualsValue;
			}
			return this;
		}
	}
}
