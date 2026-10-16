using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000163 RID: 355
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundParameterEqualsValue : BoundEqualsValue
	{
		// Token: 0x06001DA4 RID: 7588 RVA: 0x0009FA10 File Offset: 0x0009DC10
		public BoundParameterEqualsValue(SyntaxNode syntax, ParameterSymbol parameter, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression value, bool hasErrors = false)
			: base(BoundKind.ParameterEqualsValue, syntax, locals, value, hasErrors || value.HasErrors())
		{
			this.Parameter = parameter;
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0009FA32 File Offset: 0x0009DC32
		public ParameterSymbol Parameter { get; }

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0009FA3A File Offset: 0x0009DC3A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitParameterEqualsValue(this);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0009FA44 File Offset: 0x0009DC44
		public BoundParameterEqualsValue Update(ParameterSymbol parameter, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression value)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(parameter, this.Parameter) || locals != base.Locals || value != base.Value)
			{
				BoundParameterEqualsValue boundParameterEqualsValue = new BoundParameterEqualsValue(this.Syntax, parameter, locals, value, base.HasErrors);
				boundParameterEqualsValue.CopyAttributes(this);
				return boundParameterEqualsValue;
			}
			return this;
		}
	}
}
