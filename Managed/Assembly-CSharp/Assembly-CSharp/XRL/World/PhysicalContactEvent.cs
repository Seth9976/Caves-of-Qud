using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000824 RID: 2084
	[GameEvent(Cascade = 17)]
	public class PhysicalContactEvent : PooledEvent<PhysicalContactEvent>
	{
		// Token: 0x060052F8 RID: 21240 RVA: 0x0018E503 File Offset: 0x0018C703
		public override int GetCascadeLevel()
		{
			return PhysicalContactEvent.CascadeLevel;
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0018E50A File Offset: 0x0018C70A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0018E513 File Offset: 0x0018C713
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.ActorBodyPart = null;
			this.ObjectBodyPart = null;
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0018E538 File Offset: 0x0018C738
		public static void Send(GameObject Actor, GameObject Object, BodyPart ActorBodyPart = null, BodyPart ObjectBodyPart = null)
		{
			PhysicalContactEvent physicalContactEvent = PooledEvent<PhysicalContactEvent>.FromPool();
			physicalContactEvent.Actor = Actor;
			physicalContactEvent.Object = Object;
			physicalContactEvent.ActorBodyPart = ActorBodyPart;
			physicalContactEvent.ObjectBodyPart = ObjectBodyPart;
			Actor.HandleEvent(physicalContactEvent);
			Object.HandleEvent(physicalContactEvent);
		}

		// Token: 0x040029BA RID: 10682
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040029BB RID: 10683
		public GameObject Actor;

		// Token: 0x040029BC RID: 10684
		public GameObject Object;

		// Token: 0x040029BD RID: 10685
		public BodyPart ActorBodyPart;

		// Token: 0x040029BE RID: 10686
		public BodyPart ObjectBodyPart;
	}
}
