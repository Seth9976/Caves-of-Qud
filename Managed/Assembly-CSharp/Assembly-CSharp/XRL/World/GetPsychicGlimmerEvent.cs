using System;

namespace XRL.World
{
	// Token: 0x0200077C RID: 1916
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetPsychicGlimmerEvent : PooledEvent<GetPsychicGlimmerEvent>
	{
		// Token: 0x06004EA8 RID: 20136 RVA: 0x0018120A File Offset: 0x0017F40A
		public override int GetCascadeLevel()
		{
			return GetPsychicGlimmerEvent.CascadeLevel;
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00181211 File Offset: 0x0017F411
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x0018121A File Offset: 0x0017F41A
		public override void Reset()
		{
			base.Reset();
			this.Subject = null;
			this.Base = 0;
			this.Level = 0;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00181238 File Offset: 0x0017F438
		public static int GetFor(GameObject Subject, int Base = 0)
		{
			int num = Base;
			bool flag = true;
			if (flag && Subject != null && Subject.HasRegisteredEvent("GetPsychicGlimmer"))
			{
				Event @event = Event.New("GetPsychicGlimmer", 0, 0, 0);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("Base", Base);
				@event.SetParameter("Level", num);
				flag = Subject.FireEvent(@event);
				num = @event.GetIntParameter("Level", 0);
			}
			if (flag && Subject != null && Subject.WantEvent(PooledEvent<GetPsychicGlimmerEvent>.ID, GetPsychicGlimmerEvent.CascadeLevel))
			{
				GetPsychicGlimmerEvent getPsychicGlimmerEvent = PooledEvent<GetPsychicGlimmerEvent>.FromPool();
				getPsychicGlimmerEvent.Subject = Subject;
				getPsychicGlimmerEvent.Base = Base;
				getPsychicGlimmerEvent.Level = num;
				flag = Subject.HandleEvent(getPsychicGlimmerEvent);
				num = getPsychicGlimmerEvent.Level;
			}
			return Math.Max(num, 0);
		}

		// Token: 0x0400266F RID: 9839
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002670 RID: 9840
		public GameObject Subject;

		// Token: 0x04002671 RID: 9841
		public int Base;

		// Token: 0x04002672 RID: 9842
		public int Level;
	}
}
