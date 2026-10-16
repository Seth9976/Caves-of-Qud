using System;

namespace XRL.World
{
	// Token: 0x0200073C RID: 1852
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetCriticalThresholdEvent : PooledEvent<GetCriticalThresholdEvent>
	{
		// Token: 0x06004CA7 RID: 19623 RVA: 0x0017A57D File Offset: 0x0017877D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0017A586 File Offset: 0x00178786
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Weapon = null;
			this.Projectile = null;
			this.Skill = null;
			this.Threshold = 20;
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0017A5B9 File Offset: 0x001787B9
		public static GetCriticalThresholdEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Weapon, GameObject Projectile, string Skill, int Threshold)
		{
			GetCriticalThresholdEvent getCriticalThresholdEvent = PooledEvent<GetCriticalThresholdEvent>.FromPool();
			getCriticalThresholdEvent.Attacker = Attacker;
			getCriticalThresholdEvent.Defender = Defender;
			getCriticalThresholdEvent.Weapon = Weapon;
			getCriticalThresholdEvent.Projectile = Projectile;
			getCriticalThresholdEvent.Skill = Skill;
			getCriticalThresholdEvent.Threshold = Threshold;
			return getCriticalThresholdEvent;
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0017A5EC File Offset: 0x001787EC
		public static int GetFor(GameObject Attacker, GameObject Defender, GameObject Weapon, GameObject Projectile = null, string Skill = null, int Threshold = 20)
		{
			bool flag = Attacker != null && Attacker.HasRegisteredEvent("GetCriticalThreshold");
			bool flag2 = Defender != null && Defender.HasRegisteredEvent("GetCriticalThreshold");
			bool flag3 = Weapon != null && Weapon.HasRegisteredEvent("GetCriticalThreshold");
			bool flag4 = Projectile != null && Projectile.HasRegisteredEvent("GetCriticalThreshold");
			if (flag || flag2 || flag3 || flag4)
			{
				Event @event = Event.New("GetCriticalThreshold", 0, 0, 0);
				@event.SetParameter("Attacker", Attacker);
				@event.SetParameter("Defender", Defender);
				@event.SetParameter("Weapon", Weapon);
				@event.SetParameter("Projectile", Projectile);
				@event.SetParameter("Skill", Skill);
				@event.SetParameter("Threshold", Threshold);
				if (flag)
				{
					Attacker.FireEvent(@event);
				}
				if (flag2)
				{
					Defender.FireEvent(@event);
				}
				if (flag3)
				{
					Weapon.FireEvent(@event);
				}
				if (flag4)
				{
					Projectile.FireEvent(@event);
				}
				Threshold = @event.GetIntParameter("Threshold", 0);
			}
			bool flag5 = Attacker != null && Attacker.WantEvent(PooledEvent<GetCriticalThresholdEvent>.ID, MinEvent.CascadeLevel);
			bool flag6 = Defender != null && Defender.WantEvent(PooledEvent<GetCriticalThresholdEvent>.ID, MinEvent.CascadeLevel);
			bool flag7 = Weapon != null && Weapon.WantEvent(PooledEvent<GetCriticalThresholdEvent>.ID, MinEvent.CascadeLevel);
			bool flag8 = Projectile != null && Projectile.WantEvent(PooledEvent<GetCriticalThresholdEvent>.ID, MinEvent.CascadeLevel);
			if (flag5 || flag6 || flag7 || flag8)
			{
				GetCriticalThresholdEvent getCriticalThresholdEvent = GetCriticalThresholdEvent.FromPool(Attacker, Defender, Weapon, Projectile, Skill, Threshold);
				if (flag5)
				{
					Attacker.HandleEvent(getCriticalThresholdEvent);
				}
				if (flag6)
				{
					Defender.HandleEvent(getCriticalThresholdEvent);
				}
				if (flag7)
				{
					Weapon.HandleEvent(getCriticalThresholdEvent);
				}
				if (flag8)
				{
					Projectile.HandleEvent(getCriticalThresholdEvent);
				}
				Threshold = getCriticalThresholdEvent.Threshold;
			}
			return Threshold;
		}

		// Token: 0x0400251E RID: 9502
		public const int BASE_THRESHOLD = 20;

		// Token: 0x0400251F RID: 9503
		public GameObject Attacker;

		// Token: 0x04002520 RID: 9504
		public GameObject Defender;

		// Token: 0x04002521 RID: 9505
		public GameObject Weapon;

		// Token: 0x04002522 RID: 9506
		public GameObject Projectile;

		// Token: 0x04002523 RID: 9507
		public string Skill;

		// Token: 0x04002524 RID: 9508
		public int Threshold = 20;
	}
}
