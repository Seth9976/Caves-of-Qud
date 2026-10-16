using System;

namespace XRL.World
{
	// Token: 0x020006C9 RID: 1737
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanReceiveEmpathyEvent : PooledEvent<CanReceiveEmpathyEvent>
	{
		// Token: 0x06004980 RID: 18816 RVA: 0x00171E53 File Offset: 0x00170053
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x00171E5C File Offset: 0x0017005C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x00171E72 File Offset: 0x00170072
		public static CanReceiveEmpathyEvent FromPool(GameObject Object, GameObject Actor)
		{
			CanReceiveEmpathyEvent canReceiveEmpathyEvent = PooledEvent<CanReceiveEmpathyEvent>.FromPool();
			canReceiveEmpathyEvent.Object = Object;
			canReceiveEmpathyEvent.Actor = Actor;
			return canReceiveEmpathyEvent;
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x00171E87 File Offset: 0x00170087
		public static bool Check(GameObject Object, GameObject Actor)
		{
			return !Object.WantEvent(PooledEvent<CanReceiveEmpathyEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(CanReceiveEmpathyEvent.FromPool(Object, Actor));
		}

		// Token: 0x04002353 RID: 9043
		public GameObject Object;

		// Token: 0x04002354 RID: 9044
		public GameObject Actor;
	}
}
