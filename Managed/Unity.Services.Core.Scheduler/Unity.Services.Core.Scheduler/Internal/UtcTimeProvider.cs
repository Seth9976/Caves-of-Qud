using System;

namespace Unity.Services.Core.Scheduler.Internal
{
	// Token: 0x02000009 RID: 9
	internal class UtcTimeProvider : ITimeProvider
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002A45 File Offset: 0x00000C45
		public DateTime Now
		{
			get
			{
				return DateTime.UtcNow;
			}
		}
	}
}
