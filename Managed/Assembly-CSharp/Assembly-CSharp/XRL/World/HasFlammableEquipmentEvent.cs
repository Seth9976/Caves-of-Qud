using System;

namespace XRL.World
{
	// Token: 0x020007B3 RID: 1971
	[GameEvent(Cascade = 1, Cache = MinEvent.Cache.Pool)]
	public class HasFlammableEquipmentEvent : PooledEvent<HasFlammableEquipmentEvent>
	{
		// Token: 0x06005017 RID: 20503 RVA: 0x00185EF2 File Offset: 0x001840F2
		public override int GetCascadeLevel()
		{
			return HasFlammableEquipmentEvent.CascadeLevel;
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x00185EF9 File Offset: 0x001840F9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x00185F02 File Offset: 0x00184102
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Temperature = 0;
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x00185F18 File Offset: 0x00184118
		public static HasFlammableEquipmentEvent FromPool(GameObject Object, int Temperature)
		{
			HasFlammableEquipmentEvent hasFlammableEquipmentEvent = PooledEvent<HasFlammableEquipmentEvent>.FromPool();
			hasFlammableEquipmentEvent.Object = Object;
			hasFlammableEquipmentEvent.Temperature = Temperature;
			return hasFlammableEquipmentEvent;
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x00185F30 File Offset: 0x00184130
		public static bool Check(GameObject Object, int Temperature)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("HasFlammableEquipment"))
			{
				Event @event = Event.New("HasFlammableEquipment", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Temperature", Temperature);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<HasFlammableEquipmentEvent>.ID, HasFlammableEquipmentEvent.CascadeLevel))
			{
				flag = Object.HandleEvent(HasFlammableEquipmentEvent.FromPool(Object, Temperature));
			}
			return !flag;
		}

		// Token: 0x040027A5 RID: 10149
		public new static readonly int CascadeLevel = 1;

		// Token: 0x040027A6 RID: 10150
		public GameObject Object;

		// Token: 0x040027A7 RID: 10151
		public int Temperature;
	}
}
