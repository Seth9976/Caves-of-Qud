using System;

namespace XRL.World
{
	// Token: 0x020006CA RID: 1738
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanReceiveTelepathyEvent : PooledEvent<CanReceiveTelepathyEvent>
	{
		// Token: 0x06004985 RID: 18821 RVA: 0x00171EB5 File Offset: 0x001700B5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x00171EBE File Offset: 0x001700BE
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x00171ED4 File Offset: 0x001700D4
		public static CanReceiveTelepathyEvent FromPool(GameObject Object, GameObject Actor)
		{
			CanReceiveTelepathyEvent canReceiveTelepathyEvent = PooledEvent<CanReceiveTelepathyEvent>.FromPool();
			canReceiveTelepathyEvent.Object = Object;
			canReceiveTelepathyEvent.Actor = Actor;
			return canReceiveTelepathyEvent;
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x00171EE9 File Offset: 0x001700E9
		public static bool Check(GameObject Object, GameObject Actor)
		{
			return !Object.WantEvent(PooledEvent<CanReceiveTelepathyEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(CanReceiveTelepathyEvent.FromPool(Object, Actor));
		}

		// Token: 0x04002355 RID: 9045
		public GameObject Object;

		// Token: 0x04002356 RID: 9046
		public GameObject Actor;
	}
}
