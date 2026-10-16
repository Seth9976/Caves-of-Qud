using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020002C6 RID: 710
	public sealed class WaitUntil : CustomYieldInstruction
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x00043704 File Offset: 0x00041904
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_MaxExecutionTime == -1.0;
				bool flag2;
				if (flag)
				{
					flag2 = !this.m_Predicate();
				}
				else
				{
					bool flag3 = this.GetTime() > this.m_MaxExecutionTime;
					if (flag3)
					{
						this.m_TimeoutCallback();
						flag2 = false;
					}
					else
					{
						flag2 = !this.m_Predicate();
					}
				}
				return flag2;
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0004376D File Offset: 0x0004196D
		public WaitUntil(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x00043790 File Offset: 0x00041990
		public WaitUntil(Func<bool> predicate, TimeSpan timeout, Action onTimeout, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			: this(predicate)
		{
			bool flag = timeout <= TimeSpan.Zero;
			if (flag)
			{
				throw new ArgumentException("Timeout must be greater than zero", "timeout");
			}
			if (onTimeout == null)
			{
				throw new ArgumentNullException("onTimeout", "Timeout callback must be specified");
			}
			this.m_TimeoutCallback = onTimeout;
			this.m_TimeoutMode = timeoutMode;
			this.m_MaxExecutionTime = this.GetTime() + timeout.TotalSeconds;
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000437FC File Offset: 0x000419FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double GetTime()
		{
			return (this.m_TimeoutMode == WaitTimeoutMode.InGameTime) ? Time.timeAsDouble : Time.realtimeSinceStartupAsDouble;
		}

		// Token: 0x04000A0E RID: 2574
		private readonly Func<bool> m_Predicate;

		// Token: 0x04000A0F RID: 2575
		private readonly Action m_TimeoutCallback;

		// Token: 0x04000A10 RID: 2576
		private readonly WaitTimeoutMode m_TimeoutMode;

		// Token: 0x04000A11 RID: 2577
		private readonly double m_MaxExecutionTime = -1.0;
	}
}
