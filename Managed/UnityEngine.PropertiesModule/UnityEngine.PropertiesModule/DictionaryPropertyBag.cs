using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x02000037 RID: 55
	public class DictionaryPropertyBag<TKey, TValue> : KeyValueCollectionPropertyBag<Dictionary<TKey, TValue>, TKey, TValue>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005275 File Offset: 0x00003475
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000532D File Offset: 0x0000352D
		protected override Dictionary<TKey, TValue> Instantiate()
		{
			return new Dictionary<TKey, TValue>();
		}
	}
}
