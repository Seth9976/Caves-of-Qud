using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000162 RID: 354
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPropertyEqualsValue : BoundEqualsValue
	{
		// Token: 0x06001DA0 RID: 7584 RVA: 0x0009F988 File Offset: 0x0009DB88
		public BoundPropertyEqualsValue(SyntaxNode syntax, PropertySymbol property, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression value, bool hasErrors = false)
			: base(BoundKind.PropertyEqualsValue, syntax, locals, value, hasErrors || value.HasErrors())
		{
			this.Property = property;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x0009F9AA File Offset: 0x0009DBAA
		public PropertySymbol Property { get; }

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0009F9B2 File Offset: 0x0009DBB2
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPropertyEqualsValue(this);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0009F9BC File Offset: 0x0009DBBC
		public BoundPropertyEqualsValue Update(PropertySymbol property, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression value)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(property, this.Property) || locals != base.Locals || value != base.Value)
			{
				BoundPropertyEqualsValue boundPropertyEqualsValue = new BoundPropertyEqualsValue(this.Syntax, property, locals, value, base.HasErrors);
				boundPropertyEqualsValue.CopyAttributes(this);
				return boundPropertyEqualsValue;
			}
			return this;
		}
	}
}
