using System;

namespace XRL.World
{
	// Token: 0x02000822 RID: 2082
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class PartSupportEvent : PooledEvent<PartSupportEvent>
	{
		// Token: 0x060052EA RID: 21226 RVA: 0x0018E328 File Offset: 0x0018C528
		public override int GetCascadeLevel()
		{
			return PartSupportEvent.CascadeLevel;
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0018E32F File Offset: 0x0018C52F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0018E338 File Offset: 0x0018C538
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Type = null;
			this.Part = null;
			this.Skip = null;
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0018E35C File Offset: 0x0018C55C
		public static PartSupportEvent FromPool(GameObject Actor, string Type, IPart Part, IPart Skip = null)
		{
			PartSupportEvent partSupportEvent = PooledEvent<PartSupportEvent>.FromPool();
			partSupportEvent.Actor = Actor;
			partSupportEvent.Type = Type;
			partSupportEvent.Part = Part;
			partSupportEvent.Skip = Skip;
			return partSupportEvent;
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0018E380 File Offset: 0x0018C580
		public static bool Check(GameObject Actor, string Type, IPart Part, IPart Skip = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("PartSupport"))
			{
				Event @event = Event.New("PartSupport", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Part", Part);
				@event.SetParameter("Skip", Skip);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<PartSupportEvent>.ID, PartSupportEvent.CascadeLevel))
			{
				flag = Actor.HandleEvent(PartSupportEvent.FromPool(Actor, Type, Part, Skip));
			}
			return !flag;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0018E424 File Offset: 0x0018C624
		public static bool Check(NeedPartSupportEvent PE, IPart Part)
		{
			return PartSupportEvent.Check(PE.Actor, PE.Type, Part, PE.Skip);
		}

		// Token: 0x040029B3 RID: 10675
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040029B4 RID: 10676
		public GameObject Actor;

		// Token: 0x040029B5 RID: 10677
		public string Type;

		// Token: 0x040029B6 RID: 10678
		public IPart Part;

		// Token: 0x040029B7 RID: 10679
		public IPart Skip;
	}
}
