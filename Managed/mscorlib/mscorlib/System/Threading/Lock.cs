using System;

namespace System.Threading
{
	// Token: 0x020002BA RID: 698
	public class Lock
	{
		// Token: 0x06001E77 RID: 7799 RVA: 0x00070D80 File Offset: 0x0006EF80
		public void Acquire()
		{
			Monitor.Enter(this._lock);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00070D8D File Offset: 0x0006EF8D
		public void Release()
		{
			Monitor.Exit(this._lock);
		}

		// Token: 0x04001AB8 RID: 6840
		private object _lock = new object();
	}
}
