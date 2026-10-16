using System;

namespace XRL.World
{
	// Token: 0x02000761 RID: 1889
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetMatterPhaseEvent : PooledEvent<GetMatterPhaseEvent>
	{
		// Token: 0x06004DDF RID: 19935 RVA: 0x0017E32E File Offset: 0x0017C52E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x0017E337 File Offset: 0x0017C537
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.MatterPhase = 0;
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0017E34D File Offset: 0x0017C54D
		public static GetMatterPhaseEvent FromPool(GameObject Object, int Base)
		{
			GetMatterPhaseEvent getMatterPhaseEvent = PooledEvent<GetMatterPhaseEvent>.FromPool();
			getMatterPhaseEvent.Object = Object;
			getMatterPhaseEvent.MatterPhase = Base;
			return getMatterPhaseEvent;
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0017E362 File Offset: 0x0017C562
		public void MinMatterPhase(int MatterPhase)
		{
			if (this.MatterPhase < MatterPhase)
			{
				this.MatterPhase = MatterPhase;
			}
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x0017E374 File Offset: 0x0017C574
		public static int GetFor(GameObject Object, int Base = 1)
		{
			if (Object != null)
			{
				GetMatterPhaseEvent getMatterPhaseEvent = GetMatterPhaseEvent.FromPool(Object, Base);
				Object.HandleEvent(getMatterPhaseEvent);
				int matterPhase = getMatterPhaseEvent.MatterPhase;
				PooledEvent<GetMatterPhaseEvent>.ResetTo(ref getMatterPhaseEvent);
				return matterPhase;
			}
			return Base;
		}

		// Token: 0x040025DD RID: 9693
		public GameObject Object;

		// Token: 0x040025DE RID: 9694
		public int MatterPhase;
	}
}
