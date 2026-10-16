using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200152A RID: 5418
	public struct ReadLockSlim : IDisposable
	{
		// Token: 0x0600D2D3 RID: 53971 RVA: 0x004E3BDF File Offset: 0x004E1DDF
		public ReadLockSlim(ReaderWriterLockSlim Lock)
		{
			this.Lock = Lock;
		}

		// Token: 0x0600D2D4 RID: 53972 RVA: 0x004E3BE8 File Offset: 0x004E1DE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			this.Lock.ExitReadLock();
		}

		// Token: 0x040053AB RID: 21419
		private ReaderWriterLockSlim Lock;
	}
}
