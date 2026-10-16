using System;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x0200066E RID: 1646
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class AfterRemoveSkillEvent : PooledEvent<AfterRemoveSkillEvent>
	{
		// Token: 0x060046D9 RID: 18137 RVA: 0x00167DE2 File Offset: 0x00165FE2
		public override int GetCascadeLevel()
		{
			return AfterRemoveSkillEvent.CascadeLevel;
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x00167DE9 File Offset: 0x00165FE9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00167DF2 File Offset: 0x00165FF2
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Skill = null;
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x00167E08 File Offset: 0x00166008
		public static AfterRemoveSkillEvent FromPool(GameObject Actor, BaseSkill Skill)
		{
			AfterRemoveSkillEvent afterRemoveSkillEvent = PooledEvent<AfterRemoveSkillEvent>.FromPool();
			afterRemoveSkillEvent.Actor = Actor;
			afterRemoveSkillEvent.Skill = Skill;
			return afterRemoveSkillEvent;
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x00167E20 File Offset: 0x00166020
		public static void Send(GameObject Actor, BaseSkill Skill)
		{
			if (GameObject.Validate(Actor))
			{
				if (Actor.HasRegisteredEvent("AfterRemoveSkill"))
				{
					Event @event = Event.New("AfterRemoveSkill", "Actor", Actor, "Skill", Skill);
					if (!Actor.FireEvent(@event))
					{
						return;
					}
				}
				if (Actor.WantEvent(PooledEvent<AfterRemoveSkillEvent>.ID, AfterRemoveSkillEvent.CascadeLevel))
				{
					AfterRemoveSkillEvent afterRemoveSkillEvent = AfterRemoveSkillEvent.FromPool(Actor, Skill);
					Actor.HandleEvent(afterRemoveSkillEvent);
					return;
				}
			}
		}

		// Token: 0x040021BB RID: 8635
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040021BC RID: 8636
		public GameObject Actor;

		// Token: 0x040021BD RID: 8637
		public BaseSkill Skill;
	}
}
