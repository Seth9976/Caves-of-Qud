using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000102 RID: 258
	public class DictionaryContainsValueConstraint : CollectionContainsConstraint
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x00013906 File Offset: 0x00011B06
		public DictionaryContainsValueConstraint(object expected)
			: base(expected)
		{
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00013963 File Offset: 0x00011B63
		public override string DisplayName
		{
			get
			{
				return "ContainsValue";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001396A File Offset: 0x00011B6A
		public override string Description
		{
			get
			{
				return "dictionary containing value " + MsgUtils.FormatValue(base.Expected);
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00013984 File Offset: 0x00011B84
		protected override bool Matches(IEnumerable actual)
		{
			IDictionary dictionary = actual as IDictionary;
			if (dictionary == null)
			{
				throw new ArgumentException("The actual value must be an IDictionary", "actual");
			}
			return base.Matches(dictionary.Values);
		}
	}
}
