using System;
using System.Threading.Tasks;

namespace Unity.Services.Core
{
	// Token: 0x02000007 RID: 7
	public interface IUnityServices
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600000D RID: 13
		// (remove) Token: 0x0600000E RID: 14
		event Action Initialized;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600000F RID: 15
		// (remove) Token: 0x06000010 RID: 16
		event Action<Exception> InitializeFailed;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17
		ServicesInitializationState State { get; }

		// Token: 0x06000012 RID: 18
		Task InitializeAsync(InitializationOptions options = null);

		// Token: 0x06000013 RID: 19 RVA: 0x00002188 File Offset: 0x00000388
		string GetIdentifier()
		{
			return null;
		}

		// Token: 0x06000014 RID: 20
		T GetService<T>();
	}
}
