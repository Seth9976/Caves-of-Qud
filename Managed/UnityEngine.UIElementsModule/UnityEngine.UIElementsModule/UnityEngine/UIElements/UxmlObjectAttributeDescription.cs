using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000560 RID: 1376
	internal class UxmlObjectAttributeDescription<T> where T : new()
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x00096F27 File Offset: 0x00095127
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x00096F2F File Offset: 0x0009512F
		public T defaultValue { get; set; }

		// Token: 0x060029D1 RID: 10705 RVA: 0x00096F38 File Offset: 0x00095138
		public virtual T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
			List<T> list = ((visualTreeAsset != null) ? visualTreeAsset.GetUxmlObjects<T>(bag, cc) : null);
			bool flag = list != null;
			if (flag)
			{
				using (List<T>.Enumerator enumerator = list.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
			}
			return this.defaultValue;
		}
	}
}
