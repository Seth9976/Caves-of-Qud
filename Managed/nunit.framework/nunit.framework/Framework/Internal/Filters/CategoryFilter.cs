using System;
using System.Collections;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public class CategoryFilter : ValueMatchFilter
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x0000D425 File Offset: 0x0000B625
		public CategoryFilter(string name)
			: base(name)
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000D714 File Offset: 0x0000B914
		public override bool Match(ITest test)
		{
			IList list = test.Properties["Category"];
			if (list != null)
			{
				foreach (object obj in list)
				{
					string text = (string)obj;
					if (base.Match(text))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000D788 File Offset: 0x0000B988
		protected override string ElementName
		{
			get
			{
				return "cat";
			}
		}
	}
}
