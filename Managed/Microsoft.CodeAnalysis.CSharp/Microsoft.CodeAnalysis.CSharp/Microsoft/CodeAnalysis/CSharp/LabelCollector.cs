using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000200 RID: 512
	internal abstract class LabelCollector : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
	{
		// Token: 0x06002396 RID: 9110 RVA: 0x000B98BF File Offset: 0x000B7ABF
		public override BoundNode VisitLabelStatement(BoundLabelStatement node)
		{
			this.CollectLabel(node.Label);
			return base.VisitLabelStatement(node);
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000B98D4 File Offset: 0x000B7AD4
		private void CollectLabel(LabelSymbol label)
		{
			if (label != null)
			{
				HashSet<LabelSymbol> hashSet = this.currentLabels;
				if (hashSet == null)
				{
					hashSet = (this.currentLabels = new HashSet<LabelSymbol>());
				}
				hashSet.Add(label);
			}
		}

		// Token: 0x040010BF RID: 4287
		protected HashSet<LabelSymbol> currentLabels;
	}
}
