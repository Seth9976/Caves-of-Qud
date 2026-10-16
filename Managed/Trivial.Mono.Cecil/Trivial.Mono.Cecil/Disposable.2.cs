using System;

namespace Trivial.Mono
{
	// Token: 0x02000005 RID: 5
	internal struct Disposable<T> : IDisposable where T : class, IDisposable
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		public Disposable(T value, bool owned)
		{
			this.value = value;
			this.owned = owned;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002080 File Offset: 0x00000280
		public void Dispose()
		{
			if (this.value != null && this.owned)
			{
				this.value.Dispose();
			}
		}

		// Token: 0x04000003 RID: 3
		internal readonly T value;

		// Token: 0x04000004 RID: 4
		private readonly bool owned;
	}
}
