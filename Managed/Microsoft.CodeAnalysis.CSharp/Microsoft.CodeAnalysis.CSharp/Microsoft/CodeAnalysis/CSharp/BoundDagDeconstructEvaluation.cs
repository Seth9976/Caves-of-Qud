using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B5 RID: 437
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagDeconstructEvaluation : BoundDagEvaluation
	{
		// Token: 0x06001F66 RID: 8038 RVA: 0x000A25D9 File Offset: 0x000A07D9
		public BoundDagDeconstructEvaluation(SyntaxNode syntax, MethodSymbol deconstructMethod, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagDeconstructEvaluation, syntax, input, hasErrors || input.HasErrors())
		{
			this.DeconstructMethod = deconstructMethod;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000A25F9 File Offset: 0x000A07F9
		public MethodSymbol DeconstructMethod { get; }

		// Token: 0x06001F68 RID: 8040 RVA: 0x000A2601 File Offset: 0x000A0801
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagDeconstructEvaluation(this);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000A260A File Offset: 0x000A080A
		public BoundDagDeconstructEvaluation Update(MethodSymbol deconstructMethod, BoundDagTemp input)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(deconstructMethod, this.DeconstructMethod) || input != base.Input)
			{
				BoundDagDeconstructEvaluation boundDagDeconstructEvaluation = new BoundDagDeconstructEvaluation(this.Syntax, deconstructMethod, input, base.HasErrors);
				boundDagDeconstructEvaluation.CopyAttributes(this);
				return boundDagDeconstructEvaluation;
			}
			return this;
		}
	}
}
