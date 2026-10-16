using System;

namespace Unity.Services.Core
{
	// Token: 0x0200000A RID: 10
	public class ServicesInitializationException : Exception
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002281 File Offset: 0x00000481
		public ServicesInitializationException()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002289 File Offset: 0x00000489
		public ServicesInitializationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002292 File Offset: 0x00000492
		public ServicesInitializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
