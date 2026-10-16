using System;

namespace XRL.World
{
	// Token: 0x020007B4 RID: 1972
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class HasFlammableEquipmentOrInventoryEvent : PooledEvent<HasFlammableEquipmentOrInventoryEvent>
	{
		// Token: 0x0600501E RID: 20510 RVA: 0x00185FC8 File Offset: 0x001841C8
		public override int GetCascadeLevel()
		{
			return HasFlammableEquipmentOrInventoryEvent.CascadeLevel;
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x00185FCF File Offset: 0x001841CF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x00185FD8 File Offset: 0x001841D8
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Temperature = 0;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x00185FEE File Offset: 0x001841EE
		public static HasFlammableEquipmentOrInventoryEvent FromPool(GameObject Object, int Temperature)
		{
			HasFlammableEquipmentOrInventoryEvent hasFlammableEquipmentOrInventoryEvent = PooledEvent<HasFlammableEquipmentOrInventoryEvent>.FromPool();
			hasFlammableEquipmentOrInventoryEvent.Object = Object;
			hasFlammableEquipmentOrInventoryEvent.Temperature = Temperature;
			return hasFlammableEquipmentOrInventoryEvent;
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00186004 File Offset: 0x00184204
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
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<HasFlammableEquipmentOrInventoryEvent>.ID, HasFlammableEquipmentOrInventoryEvent.CascadeLevel))
			{
				flag = Object.HandleEvent(HasFlammableEquipmentOrInventoryEvent.FromPool(Object, Temperature));
			}
			return !flag;
		}

		// Token: 0x040027A8 RID: 10152
		public new static readonly int CascadeLevel = 3;

		// Token: 0x040027A9 RID: 10153
		public GameObject Object;

		// Token: 0x040027AA RID: 10154
		public int Temperature;
	}
}
