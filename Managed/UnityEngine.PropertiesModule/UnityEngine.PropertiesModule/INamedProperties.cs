using System;

namespace Unity.Properties
{
	// Token: 0x02000040 RID: 64
	public interface INamedProperties<TContainer>
	{
		// Token: 0x0600012F RID: 303
		bool TryGetProperty(ref TContainer container, string name, out IProperty<TContainer> property);
	}
}
