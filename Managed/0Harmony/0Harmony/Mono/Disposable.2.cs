using System;

namespace Mono
{
	// Token: 0x020001A5 RID: 421
	internal struct Disposable<T> : IDisposable where T : class, IDisposable
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00017BD0 File Offset: 0x00015DD0
		public Disposable(T value, bool owned)
		{
			this.value = value;
			this.owned = owned;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00017BE0 File Offset: 0x00015DE0
		public void Dispose()
		{
			if (this.value != null && this.owned)
			{
				this.value.Dispose();
			}
		}

		// Token: 0x04000234 RID: 564
		internal readonly T value;

		// Token: 0x04000235 RID: 565
		private readonly bool owned;
	}
}
