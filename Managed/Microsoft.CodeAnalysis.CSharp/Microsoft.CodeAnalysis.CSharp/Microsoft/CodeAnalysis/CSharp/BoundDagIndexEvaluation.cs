using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000083 RID: 131
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDagIndexEvaluation : BoundDagEvaluation
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x00051DF7 File Offset: 0x0004FFF7
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.Index;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00051E06 File Offset: 0x00050006
		public override bool Equals(BoundDagEvaluation obj)
		{
			return this == obj || (base.Equals(obj) && this.Index == ((BoundDagIndexEvaluation)obj).Index);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00051E2C File Offset: 0x0005002C
		public BoundDagIndexEvaluation(SyntaxNode syntax, PropertySymbol property, int index, BoundDagTemp input, bool hasErrors = false)
			: base(BoundKind.DagIndexEvaluation, syntax, input, hasErrors || input.HasErrors())
		{
			this.Property = property;
			this.Index = index;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00051E58 File Offset: 0x00050058
		public PropertySymbol Property { get; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00051E60 File Offset: 0x00050060
		public int Index { get; }

		// Token: 0x06000B25 RID: 2853 RVA: 0x00051E68 File Offset: 0x00050068
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDagIndexEvaluation(this);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00051E74 File Offset: 0x00050074
		public BoundDagIndexEvaluation Update(PropertySymbol property, int index, BoundDagTemp input)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(property, this.Property) || index != this.Index || input != base.Input)
			{
				BoundDagIndexEvaluation boundDagIndexEvaluation = new BoundDagIndexEvaluation(this.Syntax, property, index, input, base.HasErrors);
				boundDagIndexEvaluation.CopyAttributes(this);
				return boundDagIndexEvaluation;
			}
			return this;
		}
	}
}
