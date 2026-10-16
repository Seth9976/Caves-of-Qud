using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000020 RID: 32
	public interface IPerformanceService : IService
	{
		// Token: 0x06000061 RID: 97
		void BeginCpuCritical();

		// Token: 0x06000062 RID: 98
		void EndCpuCritical();
	}
}
