using System;

namespace Unity.Services.Core
{
	// Token: 0x0200000C RID: 12
	internal class UnityProjectNotLinkedException : ServicesInitializationException
	{
		// Token: 0x06000026 RID: 38 RVA: 0x0000229C File Offset: 0x0000049C
		public UnityProjectNotLinkedException()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000022A4 File Offset: 0x000004A4
		public UnityProjectNotLinkedException(string message)
			: base(message)
		{
		}
	}
}
