using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000129 RID: 297
	public class NotOperator : PrefixOperator
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x00016554 File Offset: 0x00014754
		public NotOperator()
		{
			this.left_precedence = (this.right_precedence = 1);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00016577 File Offset: 0x00014777
		public override IConstraint ApplyPrefix(IConstraint constraint)
		{
			return new NotConstraint(constraint);
		}
	}
}
