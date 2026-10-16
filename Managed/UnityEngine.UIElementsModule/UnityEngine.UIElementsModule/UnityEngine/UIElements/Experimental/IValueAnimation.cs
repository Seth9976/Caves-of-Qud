using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200071E RID: 1822
	public interface IValueAnimation
	{
		// Token: 0x060034CF RID: 13519
		void Start();

		// Token: 0x060034D0 RID: 13520
		void Stop();

		// Token: 0x060034D1 RID: 13521
		void Recycle();

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060034D2 RID: 13522
		bool isRunning { get; }

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060034D3 RID: 13523
		// (set) Token: 0x060034D4 RID: 13524
		int durationMs { get; set; }
	}
}
