using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001F7 RID: 503
	internal sealed class ExclusiveReference
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x000364FC File Offset: 0x000346FC
		public RegexRunner Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) != 0)
			{
				return null;
			}
			RegexRunner @ref = this._ref;
			if (@ref == null)
			{
				this._locked = 0;
				return null;
			}
			this._obj = @ref;
			return @ref;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00036538 File Offset: 0x00034738
		public void Release(RegexRunner obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._obj == obj)
			{
				this._obj = null;
				this._locked = 0;
				return;
			}
			if (this._obj == null && Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				if (this._ref == null)
				{
					this._ref = obj;
				}
				this._locked = 0;
				return;
			}
		}

		// Token: 0x04000822 RID: 2082
		private RegexRunner _ref;

		// Token: 0x04000823 RID: 2083
		private RegexRunner _obj;

		// Token: 0x04000824 RID: 2084
		private volatile int _locked;
	}
}
