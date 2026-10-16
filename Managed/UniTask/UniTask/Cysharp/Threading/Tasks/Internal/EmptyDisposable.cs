using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200011C RID: 284
	internal class EmptyDisposable : IDisposable
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0000F36D File Offset: 0x0000D56D
		private EmptyDisposable()
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000F375 File Offset: 0x0000D575
		public void Dispose()
		{
		}

		// Token: 0x0400014A RID: 330
		public static EmptyDisposable Instance = new EmptyDisposable();
	}
}
