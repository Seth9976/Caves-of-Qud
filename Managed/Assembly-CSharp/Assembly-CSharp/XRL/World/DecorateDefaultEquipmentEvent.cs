using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006F5 RID: 1781
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class DecorateDefaultEquipmentEvent : PooledEvent<DecorateDefaultEquipmentEvent>
	{
		// Token: 0x06004A95 RID: 19093 RVA: 0x0017491A File Offset: 0x00172B1A
		public override int GetCascadeLevel()
		{
			return DecorateDefaultEquipmentEvent.CascadeLevel;
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x00174921 File Offset: 0x00172B21
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x0017492A File Offset: 0x00172B2A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Body = null;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00174940 File Offset: 0x00172B40
		public static void Send(GameObject Object, Body Body)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("DecorateDefaultEquipment"))
			{
				Event @event = Event.New("DecorateDefaultEquipment", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Body", Body);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<DecorateDefaultEquipmentEvent>.ID, DecorateDefaultEquipmentEvent.CascadeLevel))
			{
				DecorateDefaultEquipmentEvent decorateDefaultEquipmentEvent = PooledEvent<DecorateDefaultEquipmentEvent>.FromPool();
				decorateDefaultEquipmentEvent.Object = Object;
				decorateDefaultEquipmentEvent.Body = Body;
				flag = Object.HandleEvent(decorateDefaultEquipmentEvent);
			}
		}

		// Token: 0x040023F6 RID: 9206
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040023F7 RID: 9207
		public GameObject Object;

		// Token: 0x040023F8 RID: 9208
		public Body Body;
	}
}
