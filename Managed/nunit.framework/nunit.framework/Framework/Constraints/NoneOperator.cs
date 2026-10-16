using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000128 RID: 296
	public class NoneOperator : CollectionOperator
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x0001654C File Offset: 0x0001474C
		public override IConstraint ApplyPrefix(IConstraint constraint)
		{
			return new NoItemConstraint(constraint);
		}
	}
}
