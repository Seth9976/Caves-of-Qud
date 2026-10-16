using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000038 RID: 56
	public class CircularDependencyException : ServicesInitializationException
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00002B64 File Offset: 0x00000D64
		public CircularDependencyException()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002B6C File Offset: 0x00000D6C
		public CircularDependencyException(string message)
			: base(message)
		{
		}
	}
}
