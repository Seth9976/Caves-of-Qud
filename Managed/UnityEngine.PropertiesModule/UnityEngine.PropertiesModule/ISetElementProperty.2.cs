using System;

namespace Unity.Properties
{
	// Token: 0x02000021 RID: 33
	public interface ISetElementProperty<out TKey> : ISetElementProperty, ICollectionElementProperty
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000073 RID: 115
		TKey Key { get; }
	}
}
