using System;

namespace Unity.Properties
{
	// Token: 0x02000041 RID: 65
	public interface IKeyedProperties<TContainer, TKey>
	{
		// Token: 0x06000130 RID: 304
		bool TryGetProperty(ref TContainer container, TKey key, out IProperty<TContainer> property);
	}
}
