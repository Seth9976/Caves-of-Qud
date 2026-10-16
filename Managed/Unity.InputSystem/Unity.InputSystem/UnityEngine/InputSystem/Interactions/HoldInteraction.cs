using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000125 RID: 293
	[DisplayName("Hold")]
	public class HoldInteraction : IInputInteraction
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x000501CF File Offset: 0x0004E3CF
		private float durationOrDefault
		{
			get
			{
				if ((double)this.duration <= 0.0)
				{
					return InputSystem.settings.defaultHoldTime;
				}
				return this.duration;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x000501F4 File Offset: 0x0004E3F4
		private float pressPointOrDefault
		{
			get
			{
				if ((double)this.pressPoint <= 0.0)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00050214 File Offset: 0x0004E414
		public void Process(ref InputInteractionContext context)
		{
			if (context.timerHasExpired)
			{
				context.PerformedAndStayPerformed();
				return;
			}
			switch (context.phase)
			{
			case InputActionPhase.Waiting:
				if (context.ControlIsActuated(this.pressPointOrDefault))
				{
					this.m_TimePressed = context.time;
					context.Started();
					context.SetTimeout(this.durationOrDefault);
					return;
				}
				break;
			case InputActionPhase.Started:
				if (context.time - this.m_TimePressed >= (double)this.durationOrDefault)
				{
					context.PerformedAndStayPerformed();
				}
				if (!context.ControlIsActuated(0f))
				{
					context.Canceled();
					return;
				}
				break;
			case InputActionPhase.Performed:
				if (!context.ControlIsActuated(this.pressPointOrDefault))
				{
					context.Canceled();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000502BD File Offset: 0x0004E4BD
		public void Reset()
		{
			this.m_TimePressed = 0.0;
		}

		// Token: 0x040006EF RID: 1775
		public float duration;

		// Token: 0x040006F0 RID: 1776
		public float pressPoint;

		// Token: 0x040006F1 RID: 1777
		private double m_TimePressed;
	}
}
