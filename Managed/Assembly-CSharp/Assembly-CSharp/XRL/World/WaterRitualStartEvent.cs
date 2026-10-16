using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200087B RID: 2171
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class WaterRitualStartEvent : PooledEvent<WaterRitualStartEvent>
	{
		// Token: 0x0600562E RID: 22062 RVA: 0x0019A7E5 File Offset: 0x001989E5
		public override int GetCascadeLevel()
		{
			return WaterRitualStartEvent.CascadeLevel;
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x0019A7EC File Offset: 0x001989EC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x0019A7F5 File Offset: 0x001989F5
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.SpeakingWith = null;
			this.Record = null;
			this.Initial = false;
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x0019A81C File Offset: 0x00198A1C
		public static void Send(GameObject Actor, GameObject SpeakingWith, WaterRitualRecord Record, bool Initial)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("WaterRitualStart"))
			{
				Event @event = Event.New("WaterRitualStart", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("SpeakingWith", SpeakingWith);
				@event.SetParameter("Record", Record);
				@event.SetFlag("Initial", Initial);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<WaterRitualStartEvent>.ID, WaterRitualStartEvent.CascadeLevel))
			{
				WaterRitualStartEvent waterRitualStartEvent = PooledEvent<WaterRitualStartEvent>.FromPool();
				waterRitualStartEvent.Actor = Actor;
				waterRitualStartEvent.SpeakingWith = SpeakingWith;
				waterRitualStartEvent.Record = Record;
				waterRitualStartEvent.Initial = Initial;
				flag = Actor.HandleEvent(waterRitualStartEvent);
			}
		}

		// Token: 0x04002B69 RID: 11113
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002B6A RID: 11114
		public GameObject Actor;

		// Token: 0x04002B6B RID: 11115
		public GameObject SpeakingWith;

		// Token: 0x04002B6C RID: 11116
		public WaterRitualRecord Record;

		// Token: 0x04002B6D RID: 11117
		public bool Initial;
	}
}
