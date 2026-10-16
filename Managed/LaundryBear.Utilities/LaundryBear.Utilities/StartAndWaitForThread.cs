using System;
using System.Threading;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000015 RID: 21
	public class StartAndWaitForThread : CustomYieldInstruction
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000317B File Offset: 0x0000137B
		public StartAndWaitForThread(Thread thread)
		{
			this.m_thread = thread;
			this.m_thread.Start();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003195 File Offset: 0x00001395
		public override bool keepWaiting
		{
			get
			{
				return this.m_thread.IsAlive;
			}
		}

		// Token: 0x04000031 RID: 49
		private Thread m_thread;
	}
}
