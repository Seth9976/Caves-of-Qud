using System;

namespace Unity.Properties
{
	// Token: 0x02000023 RID: 35
	public interface IDictionaryElementProperty<out TKey> : IDictionaryElementProperty, ICollectionElementProperty
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000075 RID: 117
		TKey Key { get; }
	}
}
