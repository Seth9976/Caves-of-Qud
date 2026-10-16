using System;

namespace Mono.Btls
{
	// Token: 0x020000DD RID: 221
	internal class MonoBtlsException : Exception
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000DC12 File Offset: 0x0000BE12
		public MonoBtlsException()
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000DC1A File Offset: 0x0000BE1A
		public MonoBtlsException(MonoBtlsSslError error)
			: base(error.ToString())
		{
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000DC2F File Offset: 0x0000BE2F
		public MonoBtlsException(string message)
			: base(message)
		{
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000DC38 File Offset: 0x0000BE38
		public MonoBtlsException(string format, params object[] args)
			: base(string.Format(format, args))
		{
		}
	}
}
