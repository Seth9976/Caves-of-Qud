using System;
using NUnit.Framework.Constraints;

namespace NUnit.Framework
{
	// Token: 0x0200004A RID: 74
	public static class Does
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00006529 File Offset: 0x00004729
		public static ConstraintExpression Not
		{
			get
			{
				return new ConstraintExpression().Not;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00006535 File Offset: 0x00004735
		public static FileOrDirectoryExistsConstraint Exist
		{
			get
			{
				return new FileOrDirectoryExistsConstraint();
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000642B File Offset: 0x0000462B
		public static CollectionContainsConstraint Contain(object expected)
		{
			return new CollectionContainsConstraint(expected);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000653C File Offset: 0x0000473C
		public static ContainsConstraint Contain(string expected)
		{
			return new ContainsConstraint(expected);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006544 File Offset: 0x00004744
		public static DictionaryContainsKeyConstraint ContainKey(object expected)
		{
			return Contains.Key(expected);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000654C File Offset: 0x0000474C
		public static DictionaryContainsValueConstraint ContainValue(object expected)
		{
			return Contains.Value(expected);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006554 File Offset: 0x00004754
		public static StartsWithConstraint StartWith(string expected)
		{
			return new StartsWithConstraint(expected);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000655C File Offset: 0x0000475C
		public static EndsWithConstraint EndWith(string expected)
		{
			return new EndsWithConstraint(expected);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006564 File Offset: 0x00004764
		public static RegexConstraint Match(string pattern)
		{
			return new RegexConstraint(pattern);
		}
	}
}
