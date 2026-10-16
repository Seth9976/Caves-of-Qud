using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200014B RID: 331
	public class UniqueItemsConstraint : CollectionItemsEqualConstraint
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00017985 File Offset: 0x00015B85
		public override string Description
		{
			get
			{
				return "all items unique";
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001798C File Offset: 0x00015B8C
		protected override bool Matches(IEnumerable actual)
		{
			List<object> list = new List<object>();
			foreach (object obj in actual)
			{
				foreach (object obj2 in list)
				{
					if (base.ItemsEqual(obj, obj2))
					{
						return false;
					}
				}
				list.Add(obj);
			}
			return true;
		}
	}
}
