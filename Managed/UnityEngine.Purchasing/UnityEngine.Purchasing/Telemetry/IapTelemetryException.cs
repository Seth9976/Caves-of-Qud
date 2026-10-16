using System;

namespace UnityEngine.Purchasing.Telemetry
{
	// Token: 0x02000038 RID: 56
	internal class IapTelemetryException : Exception
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00003C40 File Offset: 0x00001E40
		public IapTelemetryException()
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003C48 File Offset: 0x00001E48
		public IapTelemetryException(string message)
			: base(message)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003C51 File Offset: 0x00001E51
		public IapTelemetryException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
