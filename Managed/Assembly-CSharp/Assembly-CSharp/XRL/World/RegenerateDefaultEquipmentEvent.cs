using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000840 RID: 2112
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class RegenerateDefaultEquipmentEvent : PooledEvent<RegenerateDefaultEquipmentEvent>
	{
		// Token: 0x060053E6 RID: 21478 RVA: 0x001909EF File Offset: 0x0018EBEF
		public override int GetCascadeLevel()
		{
			return RegenerateDefaultEquipmentEvent.CascadeLevel;
		}

		// Token: 0x060053E7 RID: 21479 RVA: 0x001909F6 File Offset: 0x0018EBF6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060053E8 RID: 21480 RVA: 0x001909FF File Offset: 0x0018EBFF
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Body = null;
		}

		// Token: 0x060053E9 RID: 21481 RVA: 0x00190A18 File Offset: 0x0018EC18
		public static void Send(GameObject Object, Body Body)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("RegenerateDefaultEquipment"))
			{
				Event @event = Event.New("RegenerateDefaultEquipment", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Body", Body);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<RegenerateDefaultEquipmentEvent>.ID, RegenerateDefaultEquipmentEvent.CascadeLevel))
			{
				RegenerateDefaultEquipmentEvent regenerateDefaultEquipmentEvent = PooledEvent<RegenerateDefaultEquipmentEvent>.FromPool();
				regenerateDefaultEquipmentEvent.Object = Object;
				regenerateDefaultEquipmentEvent.Body = Body;
				flag = Object.HandleEvent(regenerateDefaultEquipmentEvent);
			}
		}

		// Token: 0x04002A2A RID: 10794
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002A2B RID: 10795
		public GameObject Object;

		// Token: 0x04002A2C RID: 10796
		public Body Body;
	}
}
