using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001DD RID: 477
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNegatedPattern : BoundPattern
	{
		// Token: 0x0600203B RID: 8251 RVA: 0x000A3E89 File Offset: 0x000A2089
		public BoundNegatedPattern(SyntaxNode syntax, BoundPattern negated, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.NegatedPattern, syntax, inputType, narrowedType, hasErrors || negated.HasErrors())
		{
			this.Negated = negated;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x000A3EAE File Offset: 0x000A20AE
		public BoundPattern Negated { get; }

		// Token: 0x0600203D RID: 8253 RVA: 0x000A3EB6 File Offset: 0x000A20B6
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNegatedPattern(this);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000A3EC0 File Offset: 0x000A20C0
		public BoundNegatedPattern Update(BoundPattern negated, TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (negated != this.Negated || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundNegatedPattern boundNegatedPattern = new BoundNegatedPattern(this.Syntax, negated, inputType, narrowedType, base.HasErrors);
				boundNegatedPattern.CopyAttributes(this);
				return boundNegatedPattern;
			}
			return this;
		}
	}
}
