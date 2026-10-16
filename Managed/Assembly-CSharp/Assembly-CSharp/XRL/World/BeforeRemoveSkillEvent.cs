using System;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x0200069D RID: 1693
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeRemoveSkillEvent : PooledEvent<BeforeRemoveSkillEvent>
	{
		// Token: 0x06004845 RID: 18501 RVA: 0x0016BDA8 File Offset: 0x00169FA8
		public override int GetCascadeLevel()
		{
			return BeforeRemoveSkillEvent.CascadeLevel;
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x0016BDAF File Offset: 0x00169FAF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x0016BDB8 File Offset: 0x00169FB8
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Skill = null;
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0016BDCE File Offset: 0x00169FCE
		public static BeforeRemoveSkillEvent FromPool(GameObject Actor, BaseSkill Skill)
		{
			BeforeRemoveSkillEvent beforeRemoveSkillEvent = PooledEvent<BeforeRemoveSkillEvent>.FromPool();
			beforeRemoveSkillEvent.Actor = Actor;
			beforeRemoveSkillEvent.Skill = Skill;
			return beforeRemoveSkillEvent;
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0016BDE4 File Offset: 0x00169FE4
		public static void Send(GameObject Actor, BaseSkill Skill)
		{
			if (GameObject.Validate(Actor))
			{
				if (Actor.HasRegisteredEvent("BeforeRemoveSkill"))
				{
					Event @event = Event.New("BeforeRemoveSkill", "Actor", Actor, "Skill", Skill);
					if (!Actor.FireEvent(@event))
					{
						return;
					}
				}
				if (Actor.WantEvent(PooledEvent<BeforeRemoveSkillEvent>.ID, BeforeRemoveSkillEvent.CascadeLevel))
				{
					BeforeRemoveSkillEvent beforeRemoveSkillEvent = BeforeRemoveSkillEvent.FromPool(Actor, Skill);
					Actor.HandleEvent(beforeRemoveSkillEvent);
					return;
				}
			}
		}

		// Token: 0x04002287 RID: 8839
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002288 RID: 8840
		public GameObject Actor;

		// Token: 0x04002289 RID: 8841
		public BaseSkill Skill;
	}
}
