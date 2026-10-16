using System;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000292 RID: 658
	public class LoggingConfigurationReloadedEventArgs : EventArgs
	{
		// Token: 0x0600175C RID: 5980 RVA: 0x0007AD6A File Offset: 0x00078F6A
		internal LoggingConfigurationReloadedEventArgs(bool succeeded, Exception exception)
		{
			this.Succeeded = succeeded;
			this.Exception = exception;
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x0007AD80 File Offset: 0x00078F80
		// (set) Token: 0x0600175E RID: 5982 RVA: 0x0007AD88 File Offset: 0x00078F88
		public bool Succeeded { get; private set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0007AD91 File Offset: 0x00078F91
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x0007AD99 File Offset: 0x00078F99
		public Exception Exception { get; private set; }
	}
}
