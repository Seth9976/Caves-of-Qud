using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B6 RID: 438
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagTypeEvaluation : BoundDagEvaluation
	{
		// Token: 0x06001F6A RID: 8042 RVA: 0x000A2644 File Offset: 0x000A0844
		public BoundDagTypeEvaluation(SyntaxNode syntax, TypeSymbol type, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagTypeEvaluation, syntax, input, hasErrors || input.HasErrors())
		{
			this.Type = type;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x000A2664 File Offset: 0x000A0864
		public TypeSymbol Type { get; }

		// Token: 0x06001F6C RID: 8044 RVA: 0x000A266C File Offset: 0x000A086C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagTypeEvaluation(this);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000A2675 File Offset: 0x000A0875
		public BoundDagTypeEvaluation Update(TypeSymbol type, BoundDagTemp input)
		{
			if (!TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything) || input != base.Input)
			{
				BoundDagTypeEvaluation boundDagTypeEvaluation = new BoundDagTypeEvaluation(this.Syntax, type, input, base.HasErrors);
				boundDagTypeEvaluation.CopyAttributes(this);
				return boundDagTypeEvaluation;
			}
			return this;
		}
	}
}
