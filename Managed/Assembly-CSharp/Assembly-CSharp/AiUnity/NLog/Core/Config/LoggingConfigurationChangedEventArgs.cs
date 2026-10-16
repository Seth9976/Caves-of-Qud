using System;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000291 RID: 657
	public class LoggingConfigurationChangedEventArgs : EventArgs
	{
		// Token: 0x06001757 RID: 5975 RVA: 0x0007AD32 File Offset: 0x00078F32
		internal LoggingConfigurationChangedEventArgs(LoggingConfiguration oldConfiguration, LoggingConfiguration newConfiguration)
		{
			this.OldConfiguration = oldConfiguration;
			this.NewConfiguration = newConfiguration;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0007AD48 File Offset: 0x00078F48
		// (set) Token: 0x06001759 RID: 5977 RVA: 0x0007AD50 File Offset: 0x00078F50
		public LoggingConfiguration OldConfiguration { get; private set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0007AD59 File Offset: 0x00078F59
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x0007AD61 File Offset: 0x00078F61
		public LoggingConfiguration NewConfiguration { get; private set; }
	}
}
