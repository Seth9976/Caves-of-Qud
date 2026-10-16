using System;
using System.Collections.Generic;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.World
{
	// Token: 0x02000687 RID: 1671
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeAddSkillEvent : IAddSkillEvent
	{
		// Token: 0x06004795 RID: 18325 RVA: 0x00169D52 File Offset: 0x00167F52
		public BeforeAddSkillEvent()
		{
			this.ID = BeforeAddSkillEvent.ID;
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x00169D70 File Offset: 0x00167F70
		public override int GetCascadeLevel()
		{
			return BeforeAddSkillEvent.CascadeLevel;
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00169D77 File Offset: 0x00167F77
		public static int CountPool()
		{
			if (BeforeAddSkillEvent.Pool != null)
			{
				return BeforeAddSkillEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00169D8C File Offset: 0x00167F8C
		public static void ResetPool()
		{
			while (BeforeAddSkillEvent.PoolCounter > 0)
			{
				BeforeAddSkillEvent.Pool[--BeforeAddSkillEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00169DB4 File Offset: 0x00167FB4
		public static void ResetTo(ref BeforeAddSkillEvent E)
		{
			MinEvent.ResetTo<BeforeAddSkillEvent>(E, BeforeAddSkillEvent.Pool, ref BeforeAddSkillEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00169DCA File Offset: 0x00167FCA
		public static BeforeAddSkillEvent FromPool()
		{
			return MinEvent.FromPool<BeforeAddSkillEvent>(ref BeforeAddSkillEvent.Pool, ref BeforeAddSkillEvent.PoolCounter, 8192);
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x00169DE0 File Offset: 0x00167FE0
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x00169DF4 File Offset: 0x00167FF4
		public override void Reset()
		{
			base.Reset();
			this.Include.Clear();
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x00169E07 File Offset: 0x00168007
		public static BeforeAddSkillEvent FromPool(GameObject Actor, BaseSkill Skill)
		{
			BeforeAddSkillEvent beforeAddSkillEvent = BeforeAddSkillEvent.FromPool();
			beforeAddSkillEvent.Actor = Actor;
			beforeAddSkillEvent.Skill = Skill;
			return beforeAddSkillEvent;
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x00169E1C File Offset: 0x0016801C
		public static BeforeAddSkillEvent FromSkill(BaseSkill Skill, GameObject Source = null, string Context = null)
		{
			BeforeAddSkillEvent beforeAddSkillEvent = BeforeAddSkillEvent.FromPool();
			beforeAddSkillEvent.Actor = Skill.ParentObject;
			beforeAddSkillEvent.Source = Source;
			beforeAddSkillEvent.Skill = Skill;
			beforeAddSkillEvent.Context = Context;
			if (SkillFactory.Factory.TryGetFirstEntry(Skill.Name, out beforeAddSkillEvent.Entry))
			{
				SkillEntry skillEntry = beforeAddSkillEvent.Entry as SkillEntry;
				if (skillEntry != null)
				{
					using (Dictionary<string, PowerEntry>.ValueCollection.Enumerator enumerator = skillEntry.Powers.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PowerEntry powerEntry = enumerator.Current;
							if (powerEntry.Cost == 0 && !beforeAddSkillEvent.Actor.HasSkill(powerEntry.Class))
							{
								beforeAddSkillEvent.Include.Add(powerEntry);
							}
						}
						return beforeAddSkillEvent;
					}
				}
				PowerEntry powerEntry2 = beforeAddSkillEvent.Entry as PowerEntry;
				if (powerEntry2 != null)
				{
					skillEntry = powerEntry2.ParentSkill;
					if (skillEntry != null && skillEntry.Cost == 0 && !beforeAddSkillEvent.Actor.HasSkill(skillEntry.Class))
					{
						beforeAddSkillEvent.Include.Add(skillEntry);
					}
				}
			}
			return beforeAddSkillEvent;
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x00169F2C File Offset: 0x0016812C
		public static void Send(BeforeAddSkillEvent E)
		{
			if (!E.Skill.BeforeAddSkill(E))
			{
				return;
			}
			if (E.Actor.HasRegisteredEvent("BeforeAddSkill"))
			{
				Event @event = Event.New("BeforeAddSkill", 0, 0, 0);
				@event.SetParameter("Actor", E.Actor);
				@event.SetParameter("Skill", E.Skill);
				@event.SetParameter("Entry", E.Entry);
				@event.SetParameter("Source", E.Source);
				@event.SetParameter("Context", E.Context);
				@event.SetParameter("Include", E.Include);
				if (!E.Actor.FireEvent(@event))
				{
					return;
				}
			}
			E.Actor.HandleEvent(E);
		}

		// Token: 0x04002222 RID: 8738
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeAddSkillEvent), null, new MinEvent.EventPoolCount(BeforeAddSkillEvent.CountPool), new MinEvent.EventPoolReset(BeforeAddSkillEvent.ResetPool));

		// Token: 0x04002223 RID: 8739
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002224 RID: 8740
		private static List<BeforeAddSkillEvent> Pool;

		// Token: 0x04002225 RID: 8741
		private static int PoolCounter;

		// Token: 0x04002226 RID: 8742
		public List<IBaseSkillEntry> Include = new List<IBaseSkillEntry>();
	}
}
