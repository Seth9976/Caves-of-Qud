using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000019 RID: 25
	public interface IActionScheduler : IServiceComponent
	{
		// Token: 0x06000031 RID: 49
		long ScheduleAction(Action action, double delaySeconds = 0.0);

		// Token: 0x06000032 RID: 50
		void CancelAction(long actionId);
	}
}
