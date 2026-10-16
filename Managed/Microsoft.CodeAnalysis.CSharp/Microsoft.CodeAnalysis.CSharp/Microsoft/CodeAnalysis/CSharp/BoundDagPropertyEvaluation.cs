using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B8 RID: 440
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagPropertyEvaluation : BoundDagEvaluation
	{
		// Token: 0x06001F72 RID: 8050 RVA: 0x000A2716 File Offset: 0x000A0916
		public BoundDagPropertyEvaluation(SyntaxNode syntax, PropertySymbol property, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagPropertyEvaluation, syntax, input, hasErrors || input.HasErrors())
		{
			this.Property = property;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x000A2739 File Offset: 0x000A0939
		public PropertySymbol Property { get; }

		// Token: 0x06001F74 RID: 8052 RVA: 0x000A2741 File Offset: 0x000A0941
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagPropertyEvaluation(this);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000A274A File Offset: 0x000A094A
		public BoundDagPropertyEvaluation Update(PropertySymbol property, BoundDagTemp input)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(property, this.Property) || input != base.Input)
			{
				BoundDagPropertyEvaluation boundDagPropertyEvaluation = new BoundDagPropertyEvaluation(this.Syntax, property, input, base.HasErrors);
				boundDagPropertyEvaluation.CopyAttributes(this);
				return boundDagPropertyEvaluation;
			}
			return this;
		}
	}
}
