using System;
using System.Collections;

namespace UnityEngine
{
	// Token: 0x0200028F RID: 655
	public abstract class CustomYieldInstruction : IEnumerator
	{
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600208C RID: 8332
		public abstract bool keepWaiting { get; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x0003ECD0 File Offset: 0x0003CED0
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0003ECE4 File Offset: 0x0003CEE4
		public bool MoveNext()
		{
			return this.keepWaiting;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00002925 File Offset: 0x00000B25
		public virtual void Reset()
		{
		}
	}
}
