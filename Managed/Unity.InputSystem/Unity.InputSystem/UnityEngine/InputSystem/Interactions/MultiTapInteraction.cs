using System;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000126 RID: 294
	public class MultiTapInteraction : IInputInteraction<float>, IInputInteraction
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x000502D6 File Offset: 0x0004E4D6
		private float tapTimeOrDefault
		{
			get
			{
				if ((double)this.tapTime <= 0.0)
				{
					return InputSystem.settings.defaultTapTime;
				}
				return this.tapTime;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x000502FB File Offset: 0x0004E4FB
		internal float tapDelayOrDefault
		{
			get
			{
				if ((double)this.tapDelay <= 0.0)
				{
					return InputSystem.settings.multiTapDelayTime;
				}
				return this.tapDelay;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x00050320 File Offset: 0x0004E520
		private float pressPointOrDefault
		{
			get
			{
				if (this.pressPoint <= 0f)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x0005033B File Offset: 0x0004E53B
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0005034C File Offset: 0x0004E54C
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.Canceled();
				return;
			}
			switch (this.m_CurrentTapPhase)
			{
			case MultiTapInteraction.TapPhase.None:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextRelease;
					this.m_CurrentTapStartTime = context.time;
					context.Started();
					float tapTimeOrDefault = this.tapTimeOrDefault;
					float tapDelayOrDefault = this.tapDelayOrDefault;
					context.SetTimeout(tapTimeOrDefault);
					context.SetTotalTimeoutCompletionTime(tapTimeOrDefault * (float)this.tapCount + (float)(this.tapCount - 1) * tapDelayOrDefault);
					return;
				}
				break;
			case MultiTapInteraction.TapPhase.WaitingForNextRelease:
				if (!context.ControlIsActuated(this.releasePointOrDefault))
				{
					if (context.time - this.m_CurrentTapStartTime > (double)this.tapTimeOrDefault)
					{
						context.Canceled();
						return;
					}
					this.m_CurrentTapCount++;
					if (this.m_CurrentTapCount >= this.tapCount)
					{
						context.Performed();
						return;
					}
					this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextPress;
					this.m_LastTapReleaseTime = context.time;
					context.SetTimeout(this.tapDelayOrDefault);
					return;
				}
				break;
			case MultiTapInteraction.TapPhase.WaitingForNextPress:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					if (context.time - this.m_LastTapReleaseTime <= (double)this.tapDelayOrDefault)
					{
						this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.WaitingForNextRelease;
						this.m_CurrentTapStartTime = context.time;
						context.SetTimeout(this.tapTimeOrDefault);
						return;
					}
					context.Canceled();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00050498 File Offset: 0x0004E698
		public void Reset()
		{
			this.m_CurrentTapPhase = MultiTapInteraction.TapPhase.None;
			this.m_CurrentTapCount = 0;
			this.m_CurrentTapStartTime = 0.0;
			this.m_LastTapReleaseTime = 0.0;
		}

		// Token: 0x040006F2 RID: 1778
		[Tooltip("The maximum time (in seconds) allowed to elapse between pressing and releasing a control for it to register as a tap.")]
		public float tapTime;

		// Token: 0x040006F3 RID: 1779
		[Tooltip("The maximum delay (in seconds) allowed between each tap. If this time is exceeded, the multi-tap is canceled.")]
		public float tapDelay;

		// Token: 0x040006F4 RID: 1780
		[Tooltip("How many taps need to be performed in succession. Two means double-tap, three means triple-tap, and so on.")]
		public int tapCount = 2;

		// Token: 0x040006F5 RID: 1781
		public float pressPoint;

		// Token: 0x040006F6 RID: 1782
		private MultiTapInteraction.TapPhase m_CurrentTapPhase;

		// Token: 0x040006F7 RID: 1783
		private int m_CurrentTapCount;

		// Token: 0x040006F8 RID: 1784
		private double m_CurrentTapStartTime;

		// Token: 0x040006F9 RID: 1785
		private double m_LastTapReleaseTime;

		// Token: 0x0200023F RID: 575
		private enum TapPhase
		{
			// Token: 0x04000C6A RID: 3178
			None,
			// Token: 0x04000C6B RID: 3179
			WaitingForNextRelease,
			// Token: 0x04000C6C RID: 3180
			WaitingForNextPress
		}
	}
}
