using System;
using UnityEngine.Purchasing.Extension;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200003E RID: 62
	public interface IAndroidStoreSelection : IStoreConfiguration
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000147 RID: 327
		AppStore appStore { get; }
	}
}
