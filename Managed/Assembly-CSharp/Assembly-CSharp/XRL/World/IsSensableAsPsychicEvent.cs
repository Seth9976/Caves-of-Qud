using System;

namespace XRL.World
{
	// Token: 0x020007F4 RID: 2036
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class IsSensableAsPsychicEvent : PooledEvent<IsSensableAsPsychicEvent>
	{
		// Token: 0x060051A2 RID: 20898 RVA: 0x0018A312 File Offset: 0x00188512
		public override int GetCascadeLevel()
		{
			return IsSensableAsPsychicEvent.CascadeLevel;
		}

		// Token: 0x060051A3 RID: 20899 RVA: 0x0018A319 File Offset: 0x00188519
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051A4 RID: 20900 RVA: 0x0018A322 File Offset: 0x00188522
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Sensable = false;
		}

		// Token: 0x060051A5 RID: 20901 RVA: 0x0018A338 File Offset: 0x00188538
		public static bool Check(GameObject Object)
		{
			bool flag = false;
			bool flag2 = true;
			if (flag2 && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("IsSensableAsPsychic"))
			{
				Event @event = Event.New("IsSensableAsPsychic", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetFlag("Sensable", flag);
				flag2 = Object.FireEvent(@event);
				flag = @event.HasFlag("Sensable");
			}
			if (flag2 && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<IsSensableAsPsychicEvent>.ID, IsSensableAsPsychicEvent.CascadeLevel))
			{
				IsSensableAsPsychicEvent isSensableAsPsychicEvent = PooledEvent<IsSensableAsPsychicEvent>.FromPool();
				isSensableAsPsychicEvent.Object = Object;
				isSensableAsPsychicEvent.Sensable = flag;
				flag2 = Object.HandleEvent(isSensableAsPsychicEvent);
				flag = isSensableAsPsychicEvent.Sensable;
			}
			return flag;
		}

		// Token: 0x040028F0 RID: 10480
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040028F1 RID: 10481
		public GameObject Object;

		// Token: 0x040028F2 RID: 10482
		public bool Sensable;
	}
}
