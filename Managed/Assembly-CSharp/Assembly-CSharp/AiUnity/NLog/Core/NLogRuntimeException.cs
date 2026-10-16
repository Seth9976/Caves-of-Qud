using System;

namespace AiUnity.NLog.Core
{
	// Token: 0x0200020F RID: 527
	[Serializable]
	public class NLogRuntimeException : Exception
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x0006FF9D File Offset: 0x0006E19D
		public NLogRuntimeException()
		{
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006FFA5 File Offset: 0x0006E1A5
		public NLogRuntimeException(string message)
			: base(message)
		{
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0006FFAE File Offset: 0x0006E1AE
		public NLogRuntimeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
