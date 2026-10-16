using System;

namespace AiUnity.NLog.Core
{
	// Token: 0x0200020E RID: 526
	[Serializable]
	public class NLogConfigurationException : Exception
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x0006FF82 File Offset: 0x0006E182
		public NLogConfigurationException()
		{
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0006FF8A File Offset: 0x0006E18A
		public NLogConfigurationException(string message)
			: base(message)
		{
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0006FF93 File Offset: 0x0006E193
		public NLogConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
