using System;

namespace System.Buffers
{
	// Token: 0x02000AE3 RID: 2787
	public abstract class MemoryPool<T> : IDisposable
	{
		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x0600631C RID: 25372 RVA: 0x0014B6BD File Offset: 0x001498BD
		public static MemoryPool<T> Shared
		{
			get
			{
				return MemoryPool<T>.s_shared;
			}
		}

		// Token: 0x0600631D RID: 25373
		public abstract IMemoryOwner<T> Rent(int minBufferSize = -1);

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x0600631E RID: 25374
		public abstract int MaxBufferSize { get; }

		// Token: 0x06006320 RID: 25376 RVA: 0x0014B6C4 File Offset: 0x001498C4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006321 RID: 25377
		protected abstract void Dispose(bool disposing);

		// Token: 0x04003A5B RID: 14939
		private static readonly MemoryPool<T> s_shared = new ArrayMemoryPool<T>();
	}
}
