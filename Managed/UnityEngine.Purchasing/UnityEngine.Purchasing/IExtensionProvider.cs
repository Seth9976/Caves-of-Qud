using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000018 RID: 24
	public interface IExtensionProvider
	{
		// Token: 0x06000059 RID: 89
		T GetExtension<T>() where T : IStoreExtension;
	}
}
