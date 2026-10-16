using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;

namespace UnityEngine.InputSystem.Interactions
{
	// Token: 0x02000127 RID: 295
	[DisplayName("Press")]
	public class PressInteraction : IInputInteraction
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x000504D5 File Offset: 0x0004E6D5
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

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x000504F0 File Offset: 0x0004E6F0
		private float releasePointOrDefault
		{
			get
			{
				return this.pressPointOrDefault * ButtonControl.s_GlobalDefaultButtonReleaseThreshold;
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00050500 File Offset: 0x0004E700
		public void Process(ref InputInteractionContext context)
		{
			float num = context.ComputeMagnitude();
			switch (this.behavior)
			{
			case PressBehavior.PressOnly:
				if (this.m_WaitingForRelease)
				{
					if (num <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						if (Mathf.Approximately(0f, num))
						{
							context.Canceled();
							return;
						}
						context.Started();
						return;
					}
				}
				else
				{
					if (num >= this.pressPointOrDefault)
					{
						this.m_WaitingForRelease = true;
						context.PerformedAndStayPerformed();
						return;
					}
					if (num > 0f && !context.isStarted)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, num) && context.isStarted)
					{
						context.Canceled();
						return;
					}
				}
				break;
			case PressBehavior.ReleaseOnly:
				if (this.m_WaitingForRelease)
				{
					if (num <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						context.Performed();
						context.Canceled();
						return;
					}
				}
				else if (num >= this.pressPointOrDefault)
				{
					this.m_WaitingForRelease = true;
					if (!context.isStarted)
					{
						context.Started();
						return;
					}
				}
				else
				{
					bool isStarted = context.isStarted;
					if (num > 0f && !isStarted)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, num) && isStarted)
					{
						context.Canceled();
						return;
					}
				}
				break;
			case PressBehavior.PressAndRelease:
				if (this.m_WaitingForRelease)
				{
					if (num <= this.releasePointOrDefault)
					{
						this.m_WaitingForRelease = false;
						context.Performed();
						if (Mathf.Approximately(0f, num))
						{
							context.Canceled();
							return;
						}
					}
				}
				else
				{
					if (num >= this.pressPointOrDefault)
					{
						this.m_WaitingForRelease = true;
						context.PerformedAndStayPerformed();
						return;
					}
					bool isStarted2 = context.isStarted;
					if (num > 0f && !isStarted2)
					{
						context.Started();
						return;
					}
					if (Mathf.Approximately(0f, num) && isStarted2)
					{
						context.Canceled();
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public void Reset()
		{
			this.m_WaitingForRelease = false;
		}

		// Token: 0x040006FA RID: 1786
		[Tooltip("The amount of actuation a control requires before being considered pressed. If not set, default to 'Default Press Point' in the global input settings.")]
		public float pressPoint;

		// Token: 0x040006FB RID: 1787
		[Tooltip("Determines how button presses trigger the action. By default (PressOnly), the action is performed on press. With ReleaseOnly, the action is performed on release. With PressAndRelease, the action is performed on press and release.")]
		public PressBehavior behavior;

		// Token: 0x040006FC RID: 1788
		private bool m_WaitingForRelease;
	}
}
