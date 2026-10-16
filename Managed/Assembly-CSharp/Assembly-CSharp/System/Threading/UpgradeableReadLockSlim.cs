using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200152B RID: 5419
	public struct UpgradeableReadLockSlim : IDisposable
	{
		// Token: 0x0600D2D5 RID: 53973 RVA: 0x004E3BF5 File Offset: 0x004E1DF5
		public UpgradeableReadLockSlim(ReaderWriterLockSlim Lock)
		{
			this.Lock = Lock;
		}

		// Token: 0x0600D2D6 RID: 53974 RVA: 0x004E3BFE File Offset: 0x004E1DFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			this.Lock.ExitUpgradeableReadLock();
		}

		// Token: 0x040053AC RID: 21420
		private ReaderWriterLockSlim Lock;
	}
}
