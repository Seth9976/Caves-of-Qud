using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002C RID: 44
	public struct InputInteractionContext
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000E954 File Offset: 0x0000CB54
		public InputAction action
		{
			get
			{
				return this.m_State.GetActionOrNull(ref this.m_TriggerState);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000E967 File Offset: 0x0000CB67
		public InputControl control
		{
			get
			{
				return this.m_State.GetControl(ref this.m_TriggerState);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000E97A File Offset: 0x0000CB7A
		public InputActionPhase phase
		{
			get
			{
				return this.m_TriggerState.phase;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000E987 File Offset: 0x0000CB87
		public double time
		{
			get
			{
				return this.m_TriggerState.time;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000E994 File Offset: 0x0000CB94
		public double startTime
		{
			get
			{
				return this.m_TriggerState.startTime;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000E9A1 File Offset: 0x0000CBA1
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000E9AE File Offset: 0x0000CBAE
		public bool timerHasExpired
		{
			get
			{
				return (this.m_Flags & InputInteractionContext.Flags.TimerHasExpired) > (InputInteractionContext.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputInteractionContext.Flags.TimerHasExpired;
					return;
				}
				this.m_Flags &= ~InputInteractionContext.Flags.TimerHasExpired;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
		public bool isWaiting
		{
			get
			{
				return this.phase == InputActionPhase.Waiting;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public bool isStarted
		{
			get
			{
				return this.phase == InputActionPhase.Started;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000E9E7 File Offset: 0x0000CBE7
		public float ComputeMagnitude()
		{
			return this.m_TriggerState.magnitude;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public bool ControlIsActuated(float threshold = 0f)
		{
			return InputActionState.IsActuated(ref this.m_TriggerState, threshold);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000EA02 File Offset: 0x0000CC02
		public void Started()
		{
			this.m_TriggerState.startTime = this.time;
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Started, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000EA2A File Offset: 0x0000CC2A
		public void Performed()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public void PerformedAndStayStarted()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Started, InputActionPhase.Waiting, true);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000EA96 File Offset: 0x0000CC96
		public void PerformedAndStayPerformed()
		{
			if (this.m_TriggerState.phase == InputActionPhase.Waiting)
			{
				this.m_TriggerState.startTime = this.time;
			}
			this.m_State.ChangePhaseOfInteraction(InputActionPhase.Performed, ref this.m_TriggerState, InputActionPhase.Performed, InputActionPhase.Waiting, true);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000EACC File Offset: 0x0000CCCC
		public void Canceled()
		{
			if (this.m_TriggerState.phase != InputActionPhase.Canceled)
			{
				this.m_State.ChangePhaseOfInteraction(InputActionPhase.Canceled, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000EAF1 File Offset: 0x0000CCF1
		public void Waiting()
		{
			if (this.m_TriggerState.phase != InputActionPhase.Waiting)
			{
				this.m_State.ChangePhaseOfInteraction(InputActionPhase.Waiting, ref this.m_TriggerState, InputActionPhase.Waiting, InputActionPhase.Waiting, true);
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000EB16 File Offset: 0x0000CD16
		public void SetTimeout(float seconds)
		{
			this.m_State.StartTimeout(seconds, ref this.m_TriggerState);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000EB2A File Offset: 0x0000CD2A
		public void SetTotalTimeoutCompletionTime(float seconds)
		{
			if (seconds <= 0f)
			{
				throw new ArgumentException("Seconds must be a positive value", "seconds");
			}
			this.m_State.SetTotalTimeoutCompletionTime(seconds, ref this.m_TriggerState);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000EB56 File Offset: 0x0000CD56
		public TValue ReadValue<TValue>() where TValue : struct
		{
			return this.m_State.ReadValue<TValue>(this.m_TriggerState.bindingIndex, this.m_TriggerState.controlIndex, false);
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000EB7A File Offset: 0x0000CD7A
		internal int mapIndex
		{
			get
			{
				return this.m_TriggerState.mapIndex;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000EB87 File Offset: 0x0000CD87
		internal int controlIndex
		{
			get
			{
				return this.m_TriggerState.controlIndex;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000EB94 File Offset: 0x0000CD94
		internal int bindingIndex
		{
			get
			{
				return this.m_TriggerState.bindingIndex;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000EBA1 File Offset: 0x0000CDA1
		internal int interactionIndex
		{
			get
			{
				return this.m_TriggerState.interactionIndex;
			}
		}

		// Token: 0x04000102 RID: 258
		internal InputActionState m_State;

		// Token: 0x04000103 RID: 259
		internal InputInteractionContext.Flags m_Flags;

		// Token: 0x04000104 RID: 260
		internal InputActionState.TriggerState m_TriggerState;

		// Token: 0x02000189 RID: 393
		[Flags]
		internal enum Flags
		{
			// Token: 0x040008A5 RID: 2213
			TimerHasExpired = 2
		}
	}
}
