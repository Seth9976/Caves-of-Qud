using System;

namespace XRL.World
{
	// Token: 0x02000821 RID: 2081
	[GameEvent(Cascade = 0)]
	public class PartRemovedEvent : PooledEvent<PartRemovedEvent>
	{
		// Token: 0x060052E5 RID: 21221 RVA: 0x0018E2A4 File Offset: 0x0018C4A4
		public override int GetCascadeLevel()
		{
			return PartRemovedEvent.CascadeLevel;
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x0018E2AB File Offset: 0x0018C4AB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x0018E2B4 File Offset: 0x0018C4B4
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Part = null;
			this.Type = null;
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0018E2D4 File Offset: 0x0018C4D4
		public static void Send(GameObject Actor, IPart Part, Type Type)
		{
			using (PartRemovedEvent partRemovedEvent = PooledEvent<PartRemovedEvent>.FromPool())
			{
				partRemovedEvent.Actor = Actor;
				partRemovedEvent.Part = Part;
				partRemovedEvent.Type = Type;
				Actor.HandleEvent(partRemovedEvent);
			}
		}

		// Token: 0x040029AF RID: 10671
		public new static readonly int CascadeLevel;

		// Token: 0x040029B0 RID: 10672
		public GameObject Actor;

		// Token: 0x040029B1 RID: 10673
		public IPart Part;

		// Token: 0x040029B2 RID: 10674
		public Type Type;
	}
}
