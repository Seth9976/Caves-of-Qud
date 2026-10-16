using System;

namespace XRL.World
{
	// Token: 0x020006BE RID: 1726
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CanBeTradedEvent : PooledEvent<CanBeTradedEvent>
	{
		// Token: 0x0600493A RID: 18746 RVA: 0x001710DE File Offset: 0x0016F2DE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x001710E7 File Offset: 0x0016F2E7
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Holder = null;
			this.OtherParty = null;
			this.CostMultiple = 0f;
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0017110F File Offset: 0x0016F30F
		public static CanBeTradedEvent FromPool(GameObject Object, GameObject Holder, GameObject OtherParty, float CostMultiple = 1f)
		{
			CanBeTradedEvent canBeTradedEvent = PooledEvent<CanBeTradedEvent>.FromPool();
			canBeTradedEvent.Object = Object;
			canBeTradedEvent.Holder = Holder;
			canBeTradedEvent.OtherParty = OtherParty;
			canBeTradedEvent.CostMultiple = CostMultiple;
			return canBeTradedEvent;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x00171132 File Offset: 0x0016F332
		public static bool Check(GameObject Object, GameObject Holder, GameObject OtherParty, float CostMultiple = 1f)
		{
			return !GameObject.Validate(ref Object) || !Object.WantEvent(PooledEvent<CanBeTradedEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(CanBeTradedEvent.FromPool(Object, Holder, OtherParty, CostMultiple));
		}

		// Token: 0x0400231E RID: 8990
		public GameObject Object;

		// Token: 0x0400231F RID: 8991
		public GameObject Holder;

		// Token: 0x04002320 RID: 8992
		public GameObject OtherParty;

		// Token: 0x04002321 RID: 8993
		public float CostMultiple;
	}
}
