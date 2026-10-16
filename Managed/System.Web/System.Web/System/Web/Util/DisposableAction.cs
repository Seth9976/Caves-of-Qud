using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x02000112 RID: 274
	internal sealed class DisposableAction : IDisposable
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x000255D4 File Offset: 0x000237D4
		public DisposableAction(Action disposeAction)
		{
			this._disposeAction = disposeAction;
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x000255E4 File Offset: 0x000237E4
		public void Dispose()
		{
			Action action = Interlocked.Exchange<Action>(ref this._disposeAction, null);
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x040011AE RID: 4526
		public static readonly DisposableAction Empty = new DisposableAction(null);

		// Token: 0x040011AF RID: 4527
		private Action _disposeAction;
	}
}
