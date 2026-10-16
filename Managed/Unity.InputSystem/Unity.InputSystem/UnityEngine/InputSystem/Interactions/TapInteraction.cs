using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x0200012A RID: 298
	[DisplayName("Tap")]
	public class TapInteraction : IInputInteraction
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00050778 File Offset: 0x0004E978
		private float durationOrDefault
		{
			get
			{
				if ((double)this.duration <= 0.0)
				{
					return InputSystem.settings.defaultTapTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0005079D File Offset: 0x0004E99D
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

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x000507B8 File Offset: 0x0004E9B8
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000507C8 File Offset: 0x0004E9C8
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.Canceled();
				this.canceledFromTimerExpired = true;
				return;
			}
			if (context.isWaiting && context.ControlIsActuated(this.pressPointOrDefault) && !this.canceledFromTimerExpired)
			{
				this.m_TapStartTime = context.time;
				context.Started();
				context.SetTimeout(this.durationOrDefault + 1E-05f);
				return;
			}
			if (context.isStarted && !context.ControlIsActuated(this.releasePointOrDefault))
			{
				if (context.time - this.m_TapStartTime <= (double)this.durationOrDefault)
				{
					context.Performed();
				}
				else
				{
					context.Canceled();
				}
			}
			if (!context.ControlIsActuated(this.releasePointOrDefault))
			{
				this.canceledFromTimerExpired = false;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0005087D File Offset: 0x0004EA7D
		public void Reset()
		{
			this.m_TapStartTime = 0.0;
		}

		// Token: 0x04000704 RID: 1796
		public float duration;

		// Token: 0x04000705 RID: 1797
		public float pressPoint;

		// Token: 0x04000706 RID: 1798
		private double m_TapStartTime;

		// Token: 0x04000707 RID: 1799
		private bool canceledFromTimerExpired;
	}
}
