using System;
using fsm;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class FSMBase : MonoBehaviour
{
	// Token: 0x060008F8 RID: 2296 RVA: 0x00039A3F File Offset: 0x00037C3F
	public virtual void Init()
	{
		if (this.stateMachine == null)
		{
			this.stateMachine = new Machine();
		}
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00039A54 File Offset: 0x00037C54
	public void StartFSM()
	{
		if (this.stateMachine != null)
		{
			this.stateMachine.Start();
		}
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00039A69 File Offset: 0x00037C69
	public void RestartFSM()
	{
		if (this.stateMachine != null)
		{
			this.stateMachine.Restart();
		}
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00039A7E File Offset: 0x00037C7E
	public virtual void Tick()
	{
		if (this.stateMachine != null)
		{
			this.stateMachine.Tick();
		}
	}

	// Token: 0x0400061B RID: 1563
	[NonSerialized]
	public Machine stateMachine;
}
