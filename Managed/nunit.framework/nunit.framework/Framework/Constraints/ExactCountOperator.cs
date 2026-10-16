using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200014E RID: 334
	public class ExactCountOperator : CollectionOperator
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00017BA0 File Offset: 0x00015DA0
		public ExactCountOperator(int expectedCount)
		{
			this.expectedCount = expectedCount;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00017BAF File Offset: 0x00015DAF
		public override IConstraint ApplyPrefix(IConstraint constraint)
		{
			return new ExactCountConstraint(this.expectedCount, constraint);
		}

		// Token: 0x0400020F RID: 527
		private int expectedCount;
	}
}
