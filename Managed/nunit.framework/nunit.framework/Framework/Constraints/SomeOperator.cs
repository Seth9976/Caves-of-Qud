using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200012E RID: 302
	public class SomeOperator : CollectionOperator
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00016632 File Offset: 0x00014832
		public override IConstraint ApplyPrefix(IConstraint constraint)
		{
			return new SomeItemsConstraint(constraint);
		}
	}
}
