using System;

namespace Unity.Services.Core
{
	// Token: 0x0200000D RID: 13
	public class RequestFailedException : Exception
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000022AD File Offset: 0x000004AD
		public int ErrorCode { get; }

		// Token: 0x06000029 RID: 41 RVA: 0x000022B5 File Offset: 0x000004B5
		public RequestFailedException(int errorCode, string message)
			: this(errorCode, message, null)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000022C0 File Offset: 0x000004C0
		public RequestFailedException(int errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}
	}
}
