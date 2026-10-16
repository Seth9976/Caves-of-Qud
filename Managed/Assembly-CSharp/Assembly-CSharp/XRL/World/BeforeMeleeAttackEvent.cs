using System;

namespace XRL.World
{
	// Token: 0x02000695 RID: 1685
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeMeleeAttackEvent : PooledEvent<BeforeMeleeAttackEvent>
	{
		// Token: 0x06004801 RID: 18433 RVA: 0x0016B1B3 File Offset: 0x001693B3
		public override int GetCascadeLevel()
		{
			return BeforeMeleeAttackEvent.CascadeLevel;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x0016B1BA File Offset: 0x001693BA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0016B1C3 File Offset: 0x001693C3
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Weapon = null;
			this.Skill = null;
			this.Stat = null;
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0016B1F0 File Offset: 0x001693F0
		public static void Send(GameObject Actor, GameObject Target, GameObject Weapon, string Skill, string Stat)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("BeforeMeleeAttack"))
			{
				Event @event = Event.New("BeforeMeleeAttack", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("Weapon", Weapon);
				@event.SetParameter("Skill", Skill);
				@event.SetParameter("Stat", Stat);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<BeforeMeleeAttackEvent>.ID, BeforeMeleeAttackEvent.CascadeLevel))
			{
				BeforeMeleeAttackEvent beforeMeleeAttackEvent = PooledEvent<BeforeMeleeAttackEvent>.FromPool();
				beforeMeleeAttackEvent.Actor = Actor;
				beforeMeleeAttackEvent.Target = Target;
				beforeMeleeAttackEvent.Weapon = Weapon;
				beforeMeleeAttackEvent.Skill = Skill;
				beforeMeleeAttackEvent.Stat = Stat;
				flag = Actor.HandleEvent(beforeMeleeAttackEvent);
			}
		}

		// Token: 0x0400225D RID: 8797
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400225E RID: 8798
		public GameObject Actor;

		// Token: 0x0400225F RID: 8799
		public GameObject Target;

		// Token: 0x04002260 RID: 8800
		public GameObject Weapon;

		// Token: 0x04002261 RID: 8801
		public string Skill;

		// Token: 0x04002262 RID: 8802
		public string Stat;
	}
}
