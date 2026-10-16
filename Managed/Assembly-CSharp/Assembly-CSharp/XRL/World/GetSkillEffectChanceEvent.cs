using System;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x0200078A RID: 1930
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetSkillEffectChanceEvent : PooledEvent<GetSkillEffectChanceEvent>
	{
		// Token: 0x06004F02 RID: 20226 RVA: 0x001822A9 File Offset: 0x001804A9
		public override int GetCascadeLevel()
		{
			return GetSkillEffectChanceEvent.CascadeLevel;
		}

		// Token: 0x06004F03 RID: 20227 RVA: 0x001822B0 File Offset: 0x001804B0
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x001822B9 File Offset: 0x001804B9
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Skill = null;
			this.BaseChance = 0;
			this.Chance = 0;
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x001822E4 File Offset: 0x001804E4
		public static int GetFor(GameObject Actor, GameObject Object, BaseSkill Skill, int Chance = 0, bool ConstrainToPercentage = true, bool ConstrainToPermillage = false)
		{
			int num = Chance;
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetSpecialEffectChance");
				bool flag3 = GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetSpecialEffectChance");
				if (flag2 || flag3)
				{
					Event @event = Event.New("GetSpecialEffectChance", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Skill", Skill);
					@event.SetParameter("BaseChance", num);
					@event.SetParameter("Chance", Chance);
					if (flag && flag2)
					{
						flag = Actor.FireEvent(@event);
					}
					if (flag && flag3)
					{
						flag = Object.FireEvent(@event);
					}
					Chance = @event.GetIntParameter("Chance", 0);
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetSkillEffectChanceEvent>.ID, GetSkillEffectChanceEvent.CascadeLevel);
				bool flag5 = GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetSkillEffectChanceEvent>.ID, GetSkillEffectChanceEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					GetSkillEffectChanceEvent getSkillEffectChanceEvent = PooledEvent<GetSkillEffectChanceEvent>.FromPool();
					getSkillEffectChanceEvent.Actor = Actor;
					getSkillEffectChanceEvent.Object = Object;
					getSkillEffectChanceEvent.Skill = Skill;
					getSkillEffectChanceEvent.BaseChance = num;
					getSkillEffectChanceEvent.Chance = Chance;
					if (flag && flag4)
					{
						flag = Actor.HandleEvent(getSkillEffectChanceEvent);
					}
					if (flag && flag5)
					{
						flag = Object.HandleEvent(getSkillEffectChanceEvent);
					}
					Chance = getSkillEffectChanceEvent.Chance;
				}
			}
			if (ConstrainToPercentage)
			{
				if (Chance > 100)
				{
					Chance = 100;
				}
				else if (Chance < 0)
				{
					Chance = 0;
				}
			}
			else if (ConstrainToPermillage)
			{
				if (Chance > 1000)
				{
					Chance = 1000;
				}
				else if (Chance < 0)
				{
					Chance = 0;
				}
			}
			return Chance;
		}

		// Token: 0x040026BA RID: 9914
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040026BB RID: 9915
		public GameObject Actor;

		// Token: 0x040026BC RID: 9916
		public GameObject Object;

		// Token: 0x040026BD RID: 9917
		public BaseSkill Skill;

		// Token: 0x040026BE RID: 9918
		public int BaseChance;

		// Token: 0x040026BF RID: 9919
		public int Chance;
	}
}
