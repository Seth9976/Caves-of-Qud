using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000126 RID: 294
	public abstract class CollectionOperator : PrefixOperator
	{
		// Token: 0x0600092E RID: 2350 RVA: 0x00016503 File Offset: 0x00014703
		protected CollectionOperator()
		{
			this.left_precedence = 1;
			this.right_precedence = 10;
		}
	}
}
