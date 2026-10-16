using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000038 RID: 56
	public class HashSetPropertyBag<TElement> : SetPropertyBagBase<HashSet<TElement>, TElement>
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005275 File Offset: 0x00003475
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000533D File Offset: 0x0000353D
		protected override HashSet<TElement> Instantiate()
		{
			return new HashSet<TElement>();
		}
	}
}
