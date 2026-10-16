using System;

namespace System.Net
{
	// Token: 0x0200056F RID: 1391
	internal readonly struct SecurityStatusPal
	{
		// Token: 0x06002CFA RID: 11514 RVA: 0x0009A12E File Offset: 0x0009832E
		public SecurityStatusPal(SecurityStatusPalErrorCode errorCode, Exception exception = null)
		{
			this.ErrorCode = errorCode;
			this.Exception = exception;
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0009A140 File Offset: 0x00098340
		public override string ToString()
		{
			if (this.Exception != null)
			{
				return string.Format("{0}={1}, {2}={3}", new object[] { "ErrorCode", this.ErrorCode, "Exception", this.Exception });
			}
			return string.Format("{0}={1}", "ErrorCode", this.ErrorCode);
		}

		// Token: 0x04001864 RID: 6244
		public readonly SecurityStatusPalErrorCode ErrorCode;

		// Token: 0x04001865 RID: 6245
		public readonly Exception Exception;
	}
}
