using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200004F RID: 79
	public class ListPropertyBag<TElement> : IndexedCollectionPropertyBag<List<TElement>, TElement>
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005275 File Offset: 0x00003475
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005B27 File Offset: 0x00003D27
		protected override List<TElement> InstantiateWithCount(int count)
		{
			return new List<TElement>(count);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005B2F File Offset: 0x00003D2F
		protected override List<TElement> Instantiate()
		{
			return new List<TElement>();
		}
	}
}
