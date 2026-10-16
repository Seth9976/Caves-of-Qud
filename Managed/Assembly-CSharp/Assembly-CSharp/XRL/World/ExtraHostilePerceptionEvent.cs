using System;

namespace XRL.World
{
	// Token: 0x02000711 RID: 1809
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class ExtraHostilePerceptionEvent : PooledEvent<ExtraHostilePerceptionEvent>
	{
		// Token: 0x06004B6C RID: 19308 RVA: 0x00176D97 File Offset: 0x00174F97
		public override int GetCascadeLevel()
		{
			return ExtraHostilePerceptionEvent.CascadeLevel;
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00176D9E File Offset: 0x00174F9E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x00176DA7 File Offset: 0x00174FA7
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Hostile = null;
			this.PerceiveVerb = null;
			this.TreatAsVisible = false;
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00176DCC File Offset: 0x00174FCC
		public static bool Check(GameObject Actor, out GameObject Hostile, out string PerceiveVerb, out bool TreatAsVisible)
		{
			Hostile = null;
			PerceiveVerb = "perceive";
			TreatAsVisible = false;
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("ExtraHostilePerception"))
			{
				Event @event = Event.New("ExtraHostilePerception", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Hostile", Hostile);
				@event.SetParameter("PerceiveVerb", PerceiveVerb);
				@event.SetFlag("TreatAsVisible", TreatAsVisible);
				flag = Actor.FireEvent(@event);
				Hostile = @event.GetGameObjectParameter("Hostile");
				PerceiveVerb = @event.GetStringParameter("PerceiveVerb", null);
				TreatAsVisible = @event.HasFlag("TreatAsVisible");
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<ExtraHostilePerceptionEvent>.ID, ExtraHostilePerceptionEvent.CascadeLevel))
			{
				ExtraHostilePerceptionEvent extraHostilePerceptionEvent = PooledEvent<ExtraHostilePerceptionEvent>.FromPool();
				extraHostilePerceptionEvent.Actor = Actor;
				extraHostilePerceptionEvent.Hostile = Hostile;
				extraHostilePerceptionEvent.PerceiveVerb = PerceiveVerb;
				extraHostilePerceptionEvent.TreatAsVisible = TreatAsVisible;
				flag = Actor.HandleEvent(extraHostilePerceptionEvent);
				Hostile = extraHostilePerceptionEvent.Hostile;
				PerceiveVerb = extraHostilePerceptionEvent.PerceiveVerb;
				TreatAsVisible = extraHostilePerceptionEvent.TreatAsVisible;
			}
			return Hostile != null;
		}

		// Token: 0x04002465 RID: 9317
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002466 RID: 9318
		public GameObject Actor;

		// Token: 0x04002467 RID: 9319
		public GameObject Hostile;

		// Token: 0x04002468 RID: 9320
		public string PerceiveVerb;

		// Token: 0x04002469 RID: 9321
		public bool TreatAsVisible;
	}
}
