using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x02000048 RID: 72
	public class Contains
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000642B File Offset: 0x0000462B
		public static CollectionContainsConstraint Item(object expected)
		{
			return new CollectionContainsConstraint(expected);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006433 File Offset: 0x00004633
		public static DictionaryContainsKeyConstraint Key(object expected)
		{
			return new DictionaryContainsKeyConstraint(expected);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000643B File Offset: 0x0000463B
		public static DictionaryContainsValueConstraint Value(object expected)
		{
			return new DictionaryContainsValueConstraint(expected);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006443 File Offset: 0x00004643
		public static SubstringConstraint Substring(string expected)
		{
			return new SubstringConstraint(expected);
		}
	}
}
