using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000129 RID: 297
	[DisplayName("Long Tap")]
	public class SlowTapInteraction : IInputInteraction
	{
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x000506B5 File Offset: 0x0004E8B5
		private float durationOrDefault
		{
			get
			{
				if (this.duration <= 0f)
				{
					return InputSystem.settings.defaultSlowTapTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x000506D5 File Offset: 0x0004E8D5
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

		// Token: 0x060010BD RID: 4285 RVA: 0x000506F0 File Offset: 0x0004E8F0
		public void Process(ref InputInteractionContext context)
		{
			if (context.isWaiting && context.ControlIsActuated(this.pressPointOrDefault))
			{
				this.m_SlowTapStartTime = context.time;
				context.Started();
				return;
			}
			if (context.isStarted && !context.ControlIsActuated(this.pressPointOrDefault))
			{
				if (context.time - this.m_SlowTapStartTime >= (double)this.durationOrDefault)
				{
					context.Performed();
					return;
				}
				context.Canceled();
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0005075F File Offset: 0x0004E95F
		public void Reset()
		{
			this.m_SlowTapStartTime = 0.0;
		}

		// Token: 0x04000701 RID: 1793
		public float duration;

		// Token: 0x04000702 RID: 1794
		public float pressPoint;

		// Token: 0x04000703 RID: 1795
		private double m_SlowTapStartTime;
	}
}
