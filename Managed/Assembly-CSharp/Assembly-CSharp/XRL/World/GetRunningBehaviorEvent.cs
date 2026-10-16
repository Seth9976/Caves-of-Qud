using System;

namespace XRL.World
{
	// Token: 0x02000785 RID: 1925
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetRunningBehaviorEvent : PooledEvent<GetRunningBehaviorEvent>
	{
		// Token: 0x06004EDE RID: 20190 RVA: 0x00181B9A File Offset: 0x0017FD9A
		public override int GetCascadeLevel()
		{
			return GetRunningBehaviorEvent.CascadeLevel;
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x00181BA1 File Offset: 0x0017FDA1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x00181BAC File Offset: 0x0017FDAC
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.AbilityName = null;
			this.Verb = null;
			this.EffectDisplayName = null;
			this.EffectMessageName = null;
			this.EffectDuration = 0;
			this.SpringingEffective = false;
			this.Stats = null;
			this.Priority = 0;
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x00181C00 File Offset: 0x0017FE00
		public static void Retrieve(GameObject Actor, out string AbilityName, out string Verb, out string EffectDisplayName, out string EffectMessageName, out int EffectDuration, out bool SpringingEffective, Templates.StatCollector Stats = null)
		{
			AbilityName = null;
			Verb = null;
			EffectDisplayName = null;
			EffectMessageName = null;
			EffectDuration = 0;
			SpringingEffective = false;
			bool flag = true;
			int num = 0;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetRunningBehavior"))
			{
				Event @event = Event.New("GetRunningBehavior", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("AbilityName", AbilityName);
				@event.SetParameter("Verb", Verb);
				@event.SetParameter("EffectDisplayName", EffectDisplayName);
				@event.SetParameter("EffectMessageName", EffectMessageName);
				@event.SetParameter("EffectDuration", EffectDuration);
				@event.SetParameter("Stats", Stats);
				@event.SetFlag("SpringingEffective", SpringingEffective);
				@event.SetParameter("Priority", num);
				flag = Actor.FireEvent(@event);
				AbilityName = @event.GetStringParameter("AbilityName", null);
				Verb = @event.GetStringParameter("Verb", null);
				EffectDisplayName = @event.GetStringParameter("EffectDisplayName", null);
				EffectMessageName = @event.GetStringParameter("EffectMessageName", null);
				EffectDuration = @event.GetIntParameter("EffectDuration", 0);
				SpringingEffective = @event.HasFlag("SpringingEffective");
				num = @event.GetIntParameter("Priority", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetRunningBehaviorEvent>.ID, GetRunningBehaviorEvent.CascadeLevel))
			{
				GetRunningBehaviorEvent getRunningBehaviorEvent = PooledEvent<GetRunningBehaviorEvent>.FromPool();
				getRunningBehaviorEvent.Actor = Actor;
				getRunningBehaviorEvent.AbilityName = AbilityName;
				getRunningBehaviorEvent.Verb = Verb;
				getRunningBehaviorEvent.EffectDisplayName = EffectDisplayName;
				getRunningBehaviorEvent.EffectMessageName = EffectMessageName;
				getRunningBehaviorEvent.EffectDuration = EffectDuration;
				getRunningBehaviorEvent.SpringingEffective = SpringingEffective;
				getRunningBehaviorEvent.Stats = Stats;
				getRunningBehaviorEvent.Priority = num;
				flag = Actor.HandleEvent(getRunningBehaviorEvent);
				AbilityName = getRunningBehaviorEvent.AbilityName;
				Verb = getRunningBehaviorEvent.Verb;
				EffectDisplayName = getRunningBehaviorEvent.EffectDisplayName;
				EffectMessageName = getRunningBehaviorEvent.EffectMessageName;
				EffectDuration = getRunningBehaviorEvent.EffectDuration;
				SpringingEffective = getRunningBehaviorEvent.SpringingEffective;
				num = getRunningBehaviorEvent.Priority;
			}
		}

		// Token: 0x04002699 RID: 9881
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400269A RID: 9882
		public GameObject Actor;

		// Token: 0x0400269B RID: 9883
		public string AbilityName;

		// Token: 0x0400269C RID: 9884
		public string Verb;

		// Token: 0x0400269D RID: 9885
		public string EffectDisplayName;

		// Token: 0x0400269E RID: 9886
		public string EffectMessageName;

		// Token: 0x0400269F RID: 9887
		public int EffectDuration;

		// Token: 0x040026A0 RID: 9888
		public bool SpringingEffective;

		// Token: 0x040026A1 RID: 9889
		public Templates.StatCollector Stats;

		// Token: 0x040026A2 RID: 9890
		public int Priority;
	}
}
