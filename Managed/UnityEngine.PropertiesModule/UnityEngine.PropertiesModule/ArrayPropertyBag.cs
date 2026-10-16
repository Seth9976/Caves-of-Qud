using System;

namespace Unity.Properties
{
	// Token: 0x02000035 RID: 53
	public sealed class ArrayPropertyBag<TElement> : IndexedCollectionPropertyBag<TElement[], TElement>
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005275 File Offset: 0x00003475
		protected override InstantiationKind InstantiationKind
		{
			get
			{
				return InstantiationKind.PropertyBagOverride;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005278 File Offset: 0x00003478
		protected override TElement[] InstantiateWithCount(int count)
		{
			return new TElement[count];
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005280 File Offset: 0x00003480
		protected override TElement[] Instantiate()
		{
			return Array.Empty<TElement>();
		}
	}
}
