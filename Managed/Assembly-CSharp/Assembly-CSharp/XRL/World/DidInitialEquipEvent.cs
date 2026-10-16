using System;

namespace XRL.World
{
	// Token: 0x020006F9 RID: 1785
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class DidInitialEquipEvent : PooledEvent<DidInitialEquipEvent>
	{
		// Token: 0x06004AB3 RID: 19123 RVA: 0x00175052 File Offset: 0x00173252
		public override int GetCascadeLevel()
		{
			return DidInitialEquipEvent.CascadeLevel;
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00175059 File Offset: 0x00173259
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x00175062 File Offset: 0x00173262
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00175074 File Offset: 0x00173274
		public static void Send(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("DidInitialEquip"))
			{
				Event @event = Event.New("DidInitialEquip", 0, 0, 0);
				@event.SetParameter("Object", Object);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<DidInitialEquipEvent>.ID, DidInitialEquipEvent.CascadeLevel))
			{
				DidInitialEquipEvent didInitialEquipEvent = PooledEvent<DidInitialEquipEvent>.FromPool();
				didInitialEquipEvent.Object = Object;
				flag = Object.HandleEvent(didInitialEquipEvent);
			}
		}

		// Token: 0x04002418 RID: 9240
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002419 RID: 9241
		public GameObject Object;
	}
}
