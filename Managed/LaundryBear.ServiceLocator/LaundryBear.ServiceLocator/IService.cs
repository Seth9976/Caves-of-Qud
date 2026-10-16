using System;
using System.Collections;

namespace LaundryBear
{
	// Token: 0x02000003 RID: 3
	public interface IService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		string Name { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		ServiceLocator.ServiceInitializationStatus InitializationStatus { get; }

		// Token: 0x06000005 RID: 5
		IEnumerator Initialize(bool sync = false);
	}
}
