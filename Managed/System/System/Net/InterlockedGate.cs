using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x020005E2 RID: 1506
	internal struct InterlockedGate
	{
		// Token: 0x06003050 RID: 12368 RVA: 0x000A67F0 File Offset: 0x000A49F0
		internal void Reset()
		{
			this.m_State = 0;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000A67FC File Offset: 0x000A49FC
		internal bool Trigger(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 2, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000A682C File Offset: 0x000A4A2C
		internal bool StartTriggering(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 1, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000A685C File Offset: 0x000A4A5C
		internal void FinishTriggering()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 2, 1) != 1)
			{
				throw new InternalException();
			}
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000A6874 File Offset: 0x000A4A74
		internal bool StartSignaling(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 3, 2);
			if (exclusive && (num == 3 || num == 4))
			{
				throw new InternalException();
			}
			return num == 2;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000A68A4 File Offset: 0x000A4AA4
		internal void FinishSignaling()
		{
			if (Interlocked.CompareExchange(ref this.m_State, 4, 3) != 3)
			{
				throw new InternalException();
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000A68BC File Offset: 0x000A4ABC
		internal bool Complete()
		{
			return Interlocked.CompareExchange(ref this.m_State, 5, 4) == 4;
		}

		// Token: 0x04001B29 RID: 6953
		private int m_State;

		// Token: 0x04001B2A RID: 6954
		internal const int Open = 0;

		// Token: 0x04001B2B RID: 6955
		internal const int Triggering = 1;

		// Token: 0x04001B2C RID: 6956
		internal const int Triggered = 2;

		// Token: 0x04001B2D RID: 6957
		internal const int Signaling = 3;

		// Token: 0x04001B2E RID: 6958
		internal const int Signaled = 4;

		// Token: 0x04001B2F RID: 6959
		internal const int Completed = 5;
	}
}
