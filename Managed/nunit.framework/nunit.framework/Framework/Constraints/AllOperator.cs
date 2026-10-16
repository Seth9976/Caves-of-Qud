using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000122 RID: 290
	public class AllOperator : CollectionOperator
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x000163EB File Offset: 0x000145EB
		public override IConstraint ApplyPrefix(IConstraint constraint)
		{
			return new AllItemsConstraint(constraint);
		}
	}
}
