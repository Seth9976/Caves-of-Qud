using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000159 RID: 345
	internal struct RegionAnalysisContext
	{
		// Token: 0x06001D7A RID: 7546 RVA: 0x0009F2AC File Offset: 0x0009D4AC
		public RegionAnalysisContext(CSharpCompilation compilation, Symbol member, BoundNode boundNode, BoundNode firstInRegion, BoundNode lastInRegion)
		{
			this.Compilation = compilation;
			this.Member = member;
			this.BoundNode = boundNode;
			this.FirstInRegion = firstInRegion;
			this.LastInRegion = lastInRegion;
			this.Failed = boundNode == null || firstInRegion == null || lastInRegion == null || firstInRegion.Syntax.SpanStart > lastInRegion.Syntax.Span.End;
			if (!this.Failed && firstInRegion == lastInRegion)
			{
				BoundKind kind = firstInRegion.Kind;
				if (kind == BoundKind.TypeExpression || kind == BoundKind.NamespaceExpression)
				{
					this.Failed = true;
				}
			}
		}

		// Token: 0x04000E5E RID: 3678
		public readonly CSharpCompilation Compilation;

		// Token: 0x04000E5F RID: 3679
		public readonly Symbol Member;

		// Token: 0x04000E60 RID: 3680
		public readonly BoundNode BoundNode;

		// Token: 0x04000E61 RID: 3681
		public readonly BoundNode FirstInRegion;

		// Token: 0x04000E62 RID: 3682
		public readonly BoundNode LastInRegion;

		// Token: 0x04000E63 RID: 3683
		public readonly bool Failed;
	}
}
