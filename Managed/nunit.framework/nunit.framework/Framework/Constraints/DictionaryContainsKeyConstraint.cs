using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000101 RID: 257
	public class DictionaryContainsKeyConstraint : CollectionContainsConstraint
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x00013906 File Offset: 0x00011B06
		public DictionaryContainsKeyConstraint(object expected)
			: base(expected)
		{
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001390F File Offset: 0x00011B0F
		public override string DisplayName
		{
			get
			{
				return "ContainsKey";
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00013916 File Offset: 0x00011B16
		public override string Description
		{
			get
			{
				return "dictionary containing key " + MsgUtils.FormatValue(base.Expected);
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00013930 File Offset: 0x00011B30
		protected override bool Matches(IEnumerable actual)
		{
			IDictionary dictionary = actual as IDictionary;
			if (dictionary == null)
			{
				throw new ArgumentException("The actual value must be an IDictionary", "actual");
			}
			return base.Matches(dictionary.Keys);
		}
	}
}
