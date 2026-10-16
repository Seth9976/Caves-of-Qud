using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200152C RID: 5420
	public struct WriteLockSlim : IDisposable
	{
		// Token: 0x0600D2D7 RID: 53975 RVA: 0x004E3C0B File Offset: 0x004E1E0B
		public WriteLockSlim(ReaderWriterLockSlim Lock)
		{
			this.Lock = Lock;
		}

		// Token: 0x0600D2D8 RID: 53976 RVA: 0x004E3C14 File Offset: 0x004E1E14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			this.Lock.ExitWriteLock();
		}

		// Token: 0x040053AD RID: 21421
		private ReaderWriterLockSlim Lock;
	}
}
