using System;

namespace XRL.World
{
	// Token: 0x020007E9 RID: 2025
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class InteriorZoneBuiltEvent : PooledEvent<InteriorZoneBuiltEvent>
	{
		// Token: 0x06005161 RID: 20833 RVA: 0x0018962B File Offset: 0x0018782B
		public override int GetCascadeLevel()
		{
			return InteriorZoneBuiltEvent.CascadeLevel;
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x00189632 File Offset: 0x00187832
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x0018963B File Offset: 0x0018783B
		public override void Reset()
		{
			base.Reset();
			this.Zone = null;
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x0018964C File Offset: 0x0018784C
		public static void Send(GameObject Object, InteriorZone Zone)
		{
			if (!GameObject.Validate(Object))
			{
				return;
			}
			if (Object.HasRegisteredEvent("InteriorZoneBuilt"))
			{
				Event @event = Event.New("InteriorZoneBuilt", "Zone", Zone);
				if (!Object.FireEvent(@event))
				{
					return;
				}
			}
			if (Object.WantEvent(PooledEvent<InteriorZoneBuiltEvent>.ID, InteriorZoneBuiltEvent.CascadeLevel))
			{
				InteriorZoneBuiltEvent interiorZoneBuiltEvent = PooledEvent<InteriorZoneBuiltEvent>.FromPool();
				interiorZoneBuiltEvent.Zone = Zone;
				Object.HandleEvent(interiorZoneBuiltEvent);
				return;
			}
		}

		// Token: 0x040028BF RID: 10431
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040028C0 RID: 10432
		public InteriorZone Zone;
	}
}
