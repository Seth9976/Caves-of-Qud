using System;

namespace XRL.World
{
	// Token: 0x02000699 RID: 1689
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class BeforePilotChangeEvent : PooledEvent<BeforePilotChangeEvent>
	{
		// Token: 0x06004821 RID: 18465 RVA: 0x0016B6F1 File Offset: 0x001698F1
		public override int GetCascadeLevel()
		{
			return BeforePilotChangeEvent.CascadeLevel;
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x0016B6F8 File Offset: 0x001698F8
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x0016B701 File Offset: 0x00169901
		public override void Reset()
		{
			base.Reset();
			this.Vehicle = null;
			this.NewPilot = null;
			this.OldPilot = null;
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x0016B720 File Offset: 0x00169920
		public static bool Check(GameObject Vehicle, GameObject NewPilot, GameObject OldPilot)
		{
			BeforePilotChangeEvent beforePilotChangeEvent = PooledEvent<BeforePilotChangeEvent>.FromPool();
			beforePilotChangeEvent.Vehicle = Vehicle;
			beforePilotChangeEvent.NewPilot = NewPilot;
			beforePilotChangeEvent.OldPilot = OldPilot;
			bool flag;
			try
			{
				if (!Vehicle.HandleEvent(beforePilotChangeEvent))
				{
					flag = false;
				}
				else if (NewPilot != null && !NewPilot.HandleEvent(beforePilotChangeEvent))
				{
					flag = false;
				}
				else if (OldPilot != null && !OldPilot.HandleEvent(beforePilotChangeEvent))
				{
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			finally
			{
				PooledEvent<BeforePilotChangeEvent>.ResetTo(ref beforePilotChangeEvent);
			}
			return flag;
		}

		// Token: 0x0400226C RID: 8812
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400226D RID: 8813
		public GameObject Vehicle;

		// Token: 0x0400226E RID: 8814
		public GameObject NewPilot;

		// Token: 0x0400226F RID: 8815
		public GameObject OldPilot;
	}
}
