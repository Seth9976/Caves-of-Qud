using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000741 RID: 1857
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 17)]
	public class GetDifficultyEvaluationEvent : MinEvent
	{
		// Token: 0x06004CE5 RID: 19685 RVA: 0x0017B35A File Offset: 0x0017955A
		public GetDifficultyEvaluationEvent()
		{
			this.ID = GetDifficultyEvaluationEvent.ID;
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0017B383 File Offset: 0x00179583
		public override int GetCascadeLevel()
		{
			return GetDifficultyEvaluationEvent.CascadeLevel;
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0017B38A File Offset: 0x0017958A
		public static int CountPool()
		{
			if (GetDifficultyEvaluationEvent.Pool != null)
			{
				return GetDifficultyEvaluationEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0017B39F File Offset: 0x0017959F
		public static void ResetPool()
		{
			while (GetDifficultyEvaluationEvent.PoolCounter > 0)
			{
				GetDifficultyEvaluationEvent.Pool[--GetDifficultyEvaluationEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0017B3C7 File Offset: 0x001795C7
		public static void ResetTo(ref GetDifficultyEvaluationEvent E)
		{
			MinEvent.ResetTo<GetDifficultyEvaluationEvent>(E, GetDifficultyEvaluationEvent.Pool, ref GetDifficultyEvaluationEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0017B3DD File Offset: 0x001795DD
		public static GetDifficultyEvaluationEvent FromPool()
		{
			return MinEvent.FromPool<GetDifficultyEvaluationEvent>(ref GetDifficultyEvaluationEvent.Pool, ref GetDifficultyEvaluationEvent.PoolCounter, 8192);
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0017B3F3 File Offset: 0x001795F3
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0017B3FC File Offset: 0x001795FC
		public override void Reset()
		{
			base.Reset();
			this.Subject = null;
			this.Actor = null;
			this.SubjectRole = null;
			this.ActorRole = null;
			this.SubjectLevel = 0;
			this.ActorLevel = 0;
			this.SubjectEffectiveLevel = 0;
			this.ActorEffectiveLevel = 0;
			this.BaseRating = 0;
			this.Rating = 0;
			this.MinRating = int.MinValue;
			this.MaxRating = int.MaxValue;
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0017B46B File Offset: 0x0017966B
		public void MinimumRating(int Value)
		{
			if (this.MinRating < Value)
			{
				this.MinRating = Value;
			}
			if (this.Rating < Value)
			{
				this.Rating = Value;
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0017B48D File Offset: 0x0017968D
		public void MaximumRating(int Value)
		{
			if (this.MaxRating > Value)
			{
				this.MaxRating = Value;
			}
			if (this.Rating > Value)
			{
				this.Rating = Value;
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0017B4B0 File Offset: 0x001796B0
		public static int GetFor(GameObject Subject, GameObject Actor, string SubjectRole, string ActorRole, int SubjectLevel, int ActorLevel, int SubjectEffectiveLevel, int ActorEffectiveLevel, int BaseRating, int MinRating = -2147483648, int MaxRating = 2147483647)
		{
			int num = BaseRating;
			bool flag = true;
			if (flag && GameObject.Validate(ref Subject) && Subject.HasRegisteredEvent("GetDifficultyEvaluation"))
			{
				Event @event = Event.New("GetDifficultyEvaluation", 0, 0, 0);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("SubjectRole", SubjectRole);
				@event.SetParameter("ActorRole", ActorRole);
				@event.SetParameter("SubjectLevel", SubjectLevel);
				@event.SetParameter("ActorLevel", ActorLevel);
				@event.SetParameter("SubjectEffectiveLevel", SubjectEffectiveLevel);
				@event.SetParameter("ActorEffectiveLevel", ActorEffectiveLevel);
				@event.SetParameter("BaseRating", BaseRating);
				@event.SetParameter("Rating", num);
				@event.SetParameter("MinRating", MinRating);
				@event.SetParameter("MaxRating", MaxRating);
				flag = Subject.FireEvent(@event);
				num = @event.GetIntParameter("Rating", 0);
				MinRating = @event.GetIntParameter("MinRating", 0);
				MaxRating = @event.GetIntParameter("MaxRating", 0);
			}
			if (flag && GameObject.Validate(ref Subject) && Subject.WantEvent(GetDifficultyEvaluationEvent.ID, GetDifficultyEvaluationEvent.CascadeLevel))
			{
				GetDifficultyEvaluationEvent getDifficultyEvaluationEvent = GetDifficultyEvaluationEvent.FromPool();
				getDifficultyEvaluationEvent.Subject = Subject;
				getDifficultyEvaluationEvent.Actor = Actor;
				getDifficultyEvaluationEvent.SubjectRole = SubjectRole;
				getDifficultyEvaluationEvent.ActorRole = ActorRole;
				getDifficultyEvaluationEvent.SubjectLevel = SubjectLevel;
				getDifficultyEvaluationEvent.ActorLevel = ActorLevel;
				getDifficultyEvaluationEvent.SubjectEffectiveLevel = SubjectEffectiveLevel;
				getDifficultyEvaluationEvent.ActorEffectiveLevel = ActorEffectiveLevel;
				getDifficultyEvaluationEvent.BaseRating = BaseRating;
				getDifficultyEvaluationEvent.Rating = num;
				getDifficultyEvaluationEvent.MinRating = MinRating;
				getDifficultyEvaluationEvent.MaxRating = MaxRating;
				flag = Subject.HandleEvent(getDifficultyEvaluationEvent);
				num = getDifficultyEvaluationEvent.Rating;
				MinRating = getDifficultyEvaluationEvent.MinRating;
				MaxRating = getDifficultyEvaluationEvent.MaxRating;
			}
			return Math.Max(Math.Min(num, MaxRating), MinRating);
		}

		// Token: 0x04002531 RID: 9521
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetDifficultyEvaluationEvent), null, new MinEvent.EventPoolCount(GetDifficultyEvaluationEvent.CountPool), new MinEvent.EventPoolReset(GetDifficultyEvaluationEvent.ResetPool));

		// Token: 0x04002532 RID: 9522
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002533 RID: 9523
		private static List<GetDifficultyEvaluationEvent> Pool;

		// Token: 0x04002534 RID: 9524
		private static int PoolCounter;

		// Token: 0x04002535 RID: 9525
		public GameObject Subject;

		// Token: 0x04002536 RID: 9526
		public GameObject Actor;

		// Token: 0x04002537 RID: 9527
		public string SubjectRole;

		// Token: 0x04002538 RID: 9528
		public string ActorRole;

		// Token: 0x04002539 RID: 9529
		public int SubjectLevel;

		// Token: 0x0400253A RID: 9530
		public int ActorLevel;

		// Token: 0x0400253B RID: 9531
		public int SubjectEffectiveLevel;

		// Token: 0x0400253C RID: 9532
		public int ActorEffectiveLevel;

		// Token: 0x0400253D RID: 9533
		public int BaseRating;

		// Token: 0x0400253E RID: 9534
		public int Rating;

		// Token: 0x0400253F RID: 9535
		public int MinRating = int.MinValue;

		// Token: 0x04002540 RID: 9536
		public int MaxRating = int.MaxValue;
	}
}
