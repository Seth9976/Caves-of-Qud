using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020002C7 RID: 711
	public sealed class WaitWhile : CustomYieldInstruction
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x00043814 File Offset: 0x00041A14
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_MaxExecutionTime == -1.0;
				bool flag2;
				if (flag)
				{
					flag2 = this.m_Predicate();
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
						flag2 = this.m_Predicate();
					}
				}
				return flag2;
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00043877 File Offset: 0x00041A77
		public WaitWhile(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00043898 File Offset: 0x00041A98
		public WaitWhile(Func<bool> predicate, TimeSpan timeout, Action onTimeout, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
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

		// Token: 0x0600227C RID: 8828 RVA: 0x00043904 File Offset: 0x00041B04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double GetTime()
		{
			return (this.m_TimeoutMode == WaitTimeoutMode.InGameTime) ? Time.timeAsDouble : Time.realtimeSinceStartupAsDouble;
		}

		// Token: 0x04000A12 RID: 2578
		private readonly Func<bool> m_Predicate;

		// Token: 0x04000A13 RID: 2579
		private readonly Action m_TimeoutCallback;

		// Token: 0x04000A14 RID: 2580
		private readonly WaitTimeoutMode m_TimeoutMode;

		// Token: 0x04000A15 RID: 2581
		private readonly double m_MaxExecutionTime = -1.0;
	}
}
