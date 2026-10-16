using System;

namespace XRL.World
{
	// Token: 0x020006E5 RID: 1765
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckUsesChargeWhileEquippedEvent : PooledEvent<CheckUsesChargeWhileEquippedEvent>
	{
		// Token: 0x06004A26 RID: 18982 RVA: 0x0017398B File Offset: 0x00171B8B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x00173994 File Offset: 0x00171B94
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x001739A3 File Offset: 0x00171BA3
		public static CheckUsesChargeWhileEquippedEvent FromPool(GameObject Object)
		{
			CheckUsesChargeWhileEquippedEvent checkUsesChargeWhileEquippedEvent = PooledEvent<CheckUsesChargeWhileEquippedEvent>.FromPool();
			checkUsesChargeWhileEquippedEvent.Object = Object;
			return checkUsesChargeWhileEquippedEvent;
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x001739B1 File Offset: 0x00171BB1
		public static bool Check(GameObject Object)
		{
			return Object.WantEvent(PooledEvent<CheckUsesChargeWhileEquippedEvent>.ID, MinEvent.CascadeLevel) && !Object.HandleEvent(CheckUsesChargeWhileEquippedEvent.FromPool(Object));
		}

		// Token: 0x040023AB RID: 9131
		public GameObject Object;
	}
}
