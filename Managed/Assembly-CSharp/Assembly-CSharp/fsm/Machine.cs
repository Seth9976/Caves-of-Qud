using System;
using UnityEngine;

namespace fsm
{
	// Token: 0x0200034F RID: 847
	public class Machine : State
	{
		// Token: 0x06001F81 RID: 8065 RVA: 0x00099342 File Offset: 0x00097542
		public Machine()
			: base("fsm_state_machine", null)
		{
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0009936F File Offset: 0x0009756F
		public void Restart()
		{
			this.Stop();
			this.Start();
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00099380 File Offset: 0x00097580
		public void Start()
		{
			if (this.machineState == Machine.MachineState.Running || this.machineState == Machine.MachineState.Paused)
			{
				return;
			}
			this.machineState = Machine.MachineState.Running;
			if (this.onStart != null)
			{
				this.onStart();
			}
			if (this.mode != State.Mode.Exclusive)
			{
				for (int i = 0; i < this.children.Count; i++)
				{
					base.EnterStates(this.children[i], this.startState);
				}
				return;
			}
			if (base.initState != null)
			{
				base.EnterStates(base.initState, this.startState);
				return;
			}
			Debug.LogError("FSM error: can't find initial state in " + this.name);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00099420 File Offset: 0x00097620
		public void Stop()
		{
			if (this.machineState == Machine.MachineState.Stopped)
			{
				return;
			}
			if (this.isUpdating)
			{
				this.machineState = Machine.MachineState.Stopping;
				return;
			}
			this.ProcessStop();
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00099442 File Offset: 0x00097642
		public void Pause()
		{
			this.machineState = Machine.MachineState.Paused;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0009944B File Offset: 0x0009764B
		public void Resume()
		{
			this.machineState = Machine.MachineState.Running;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00099454 File Offset: 0x00097654
		protected void ProcessStop()
		{
			base.ClearCurrentStatesRecursively();
			if (this.onStop != null)
			{
				this.onStop();
			}
			this.machineState = Machine.MachineState.Stopped;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00099478 File Offset: 0x00097678
		public void Tick()
		{
			if (this.machineState == Machine.MachineState.Paused || this.machineState == Machine.MachineState.Stopped)
			{
				return;
			}
			this.isUpdating = true;
			if (this.machineState != Machine.MachineState.Stopping)
			{
				bool flag = false;
				base.CheckConditions();
				base.OnAction();
				base.UpdateTransitions();
				if (flag)
				{
					this.Stop();
				}
			}
			this.isUpdating = false;
			if (this.machineState == Machine.MachineState.Stopping)
			{
				this.ProcessStop();
			}
		}

		// Token: 0x04001032 RID: 4146
		public bool showDebugInfo = true;

		// Token: 0x04001033 RID: 4147
		public bool logDebugInfo;

		// Token: 0x04001034 RID: 4148
		public Action onStart;

		// Token: 0x04001035 RID: 4149
		public Action onStop;

		// Token: 0x04001036 RID: 4150
		protected Machine.MachineState machineState = Machine.MachineState.Stopped;

		// Token: 0x04001037 RID: 4151
		protected State startState = new State("fsm_start", null);

		// Token: 0x04001038 RID: 4152
		protected bool isUpdating;

		// Token: 0x0200175C RID: 5980
		public enum MachineState
		{
			// Token: 0x04005E40 RID: 24128
			Running,
			// Token: 0x04005E41 RID: 24129
			Paused,
			// Token: 0x04005E42 RID: 24130
			Stopping,
			// Token: 0x04005E43 RID: 24131
			Stopped
		}
	}
}
