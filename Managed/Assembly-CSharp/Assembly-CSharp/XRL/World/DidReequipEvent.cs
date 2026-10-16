using System;

namespace XRL.World
{
	// Token: 0x020006FA RID: 1786
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class DidReequipEvent : PooledEvent<DidReequipEvent>
	{
		// Token: 0x06004AB9 RID: 19129 RVA: 0x00175103 File Offset: 0x00173303
		public override int GetCascadeLevel()
		{
			return DidReequipEvent.CascadeLevel;
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x0017510A File Offset: 0x0017330A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00175113 File Offset: 0x00173313
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00175124 File Offset: 0x00173324
		public static void Send(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("DidReequip"))
			{
				Event @event = Event.New("DidReequip", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<DidReequipEvent>.ID, DidReequipEvent.CascadeLevel))
			{
				DidReequipEvent didReequipEvent = PooledEvent<DidReequipEvent>.FromPool();
				didReequipEvent.Object = Object;
				flag = Object.HandleEvent(didReequipEvent);
			}
		}

		// Token: 0x0400241A RID: 9242
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400241B RID: 9243
		public GameObject Object;
	}
}
