using System;
using System.Collections.Generic;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x02000654 RID: 1620
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class AfterAddSkillEvent : IAddSkillEvent
	{
		// Token: 0x0600461F RID: 17951 RVA: 0x00165B11 File Offset: 0x00163D11
		public AfterAddSkillEvent()
		{
			this.ID = AfterAddSkillEvent.ID;
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x00165B24 File Offset: 0x00163D24
		public override int GetCascadeLevel()
		{
			return AfterAddSkillEvent.CascadeLevel;
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x00165B2B File Offset: 0x00163D2B
		public static int CountPool()
		{
			if (AfterAddSkillEvent.Pool != null)
			{
				return AfterAddSkillEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x00165B40 File Offset: 0x00163D40
		public static void ResetPool()
		{
			while (AfterAddSkillEvent.PoolCounter > 0)
			{
				AfterAddSkillEvent.Pool[--AfterAddSkillEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x00165B68 File Offset: 0x00163D68
		public static void ResetTo(ref AfterAddSkillEvent E)
		{
			MinEvent.ResetTo<AfterAddSkillEvent>(E, AfterAddSkillEvent.Pool, ref AfterAddSkillEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x00165B7E File Offset: 0x00163D7E
		public static AfterAddSkillEvent FromPool()
		{
			return MinEvent.FromPool<AfterAddSkillEvent>(ref AfterAddSkillEvent.Pool, ref AfterAddSkillEvent.PoolCounter, 8192);
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00165B94 File Offset: 0x00163D94
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x00165BA8 File Offset: 0x00163DA8
		public override void Reset()
		{
			base.Reset();
			this.Include = null;
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x00165BB8 File Offset: 0x00163DB8
		public static void Send(GameObject Actor, BaseSkill Skill, IBaseSkillEntry Entry, GameObject Source = null, string Context = null, List<BaseSkill> Include = null)
		{
			if (GameObject.Validate(Actor))
			{
				if (Actor.HasRegisteredEvent("AfterAddSkill"))
				{
					Event @event = Event.New("AfterAddSkill", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Skill", Skill);
					@event.SetParameter("Entry", Entry);
					@event.SetParameter("Source", Source);
					@event.SetParameter("Context", Context);
					@event.SetParameter("Include", Include);
					if (!Actor.FireEvent(@event))
					{
						return;
					}
				}
				if (Actor.WantEvent(AfterAddSkillEvent.ID, AfterAddSkillEvent.CascadeLevel))
				{
					AfterAddSkillEvent afterAddSkillEvent = AfterAddSkillEvent.FromPool();
					afterAddSkillEvent.Actor = Actor;
					afterAddSkillEvent.Source = Source;
					afterAddSkillEvent.Skill = Skill;
					afterAddSkillEvent.Entry = Entry;
					afterAddSkillEvent.Context = Context;
					afterAddSkillEvent.Include = Include;
					Actor.HandleEvent(afterAddSkillEvent);
					return;
				}
			}
		}

		// Token: 0x04002148 RID: 8520
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterAddSkillEvent), null, new MinEvent.EventPoolCount(AfterAddSkillEvent.CountPool), new MinEvent.EventPoolReset(AfterAddSkillEvent.ResetPool));

		// Token: 0x04002149 RID: 8521
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400214A RID: 8522
		private static List<AfterAddSkillEvent> Pool;

		// Token: 0x0400214B RID: 8523
		private static int PoolCounter;

		// Token: 0x0400214C RID: 8524
		public List<BaseSkill> Include;
	}
}
