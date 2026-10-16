using System;

namespace XRL.World
{
	// Token: 0x02000753 RID: 1875
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetHostilityRecognitionLimitsEvent : PooledEvent<GetHostilityRecognitionLimitsEvent>
	{
		// Token: 0x06004D77 RID: 19831 RVA: 0x0017D235 File Offset: 0x0017B435
		public override int GetCascadeLevel()
		{
			return GetHostilityRecognitionLimitsEvent.CascadeLevel;
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x0017D23C File Offset: 0x0017B43C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0017D245 File Offset: 0x0017B445
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Subject = null;
			this.IgnoreEasierThan = 0;
			this.IgnoreFartherThan = 0;
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0017D269 File Offset: 0x0017B469
		public void MinDifficulty(int Value)
		{
			if (this.IgnoreEasierThan < Value)
			{
				this.IgnoreEasierThan = Value;
			}
		}

		// Token: 0x06004D7B RID: 19835 RVA: 0x0017D27B File Offset: 0x0017B47B
		public void MaxDistance(int Value)
		{
			if (this.IgnoreFartherThan > Value)
			{
				this.IgnoreFartherThan = Value;
			}
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x0017D290 File Offset: 0x0017B490
		public static bool GetFor(GameObject Actor, GameObject Subject, ref int IgnoreEasierThan, ref int IgnoreFartherThan)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Subject) && Subject.HasRegisteredEvent("GetHostilityRecognitionLimits"))
			{
				Event @event = Event.New("GetHostilityRecognitionLimits", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("IgnoreEasierThan", IgnoreEasierThan);
				@event.SetParameter("IgnoreFartherThan", IgnoreFartherThan);
				flag = Subject.FireEvent(@event);
				IgnoreEasierThan = @event.GetIntParameter("IgnoreEasierThan", 0);
				IgnoreFartherThan = @event.GetIntParameter("IgnoreFartherThan", 0);
			}
			if (flag && GameObject.Validate(ref Subject) && Subject.WantEvent(PooledEvent<GetHostilityRecognitionLimitsEvent>.ID, GetHostilityRecognitionLimitsEvent.CascadeLevel))
			{
				GetHostilityRecognitionLimitsEvent getHostilityRecognitionLimitsEvent = PooledEvent<GetHostilityRecognitionLimitsEvent>.FromPool();
				getHostilityRecognitionLimitsEvent.Actor = Actor;
				getHostilityRecognitionLimitsEvent.Subject = Subject;
				getHostilityRecognitionLimitsEvent.IgnoreEasierThan = IgnoreEasierThan;
				getHostilityRecognitionLimitsEvent.IgnoreFartherThan = IgnoreFartherThan;
				flag = Subject.HandleEvent(getHostilityRecognitionLimitsEvent);
				IgnoreEasierThan = getHostilityRecognitionLimitsEvent.IgnoreEasierThan;
				IgnoreFartherThan = getHostilityRecognitionLimitsEvent.IgnoreFartherThan;
			}
			return flag;
		}

		// Token: 0x0400259B RID: 9627
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400259C RID: 9628
		public GameObject Actor;

		// Token: 0x0400259D RID: 9629
		public GameObject Subject;

		// Token: 0x0400259E RID: 9630
		public int IgnoreEasierThan;

		// Token: 0x0400259F RID: 9631
		public int IgnoreFartherThan;
	}
}
