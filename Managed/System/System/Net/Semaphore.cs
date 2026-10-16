using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200063D RID: 1597
	internal sealed class Semaphore : WaitHandle
	{
		// Token: 0x06003249 RID: 12873 RVA: 0x000ADD94 File Offset: 0x000ABF94
		internal Semaphore(int initialCount, int maxCount)
		{
			lock (this)
			{
				int num;
				this.Handle = Semaphore.CreateSemaphore_internal(initialCount, maxCount, null, out num);
			}
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000ADDE0 File Offset: 0x000ABFE0
		internal bool ReleaseSemaphore()
		{
			int num;
			return Semaphore.ReleaseSemaphore_internal(this.Handle, 1, out num);
		}
	}
}
