using System;

namespace XRL.World
{
	// Token: 0x020006F6 RID: 1782
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class DefendMeleeHitEvent : PooledEvent<DefendMeleeHitEvent>
	{
		// Token: 0x06004A9B RID: 19099 RVA: 0x001749E3 File Offset: 0x00172BE3
		public override int GetCascadeLevel()
		{
			return DefendMeleeHitEvent.CascadeLevel;
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x001749EA File Offset: 0x00172BEA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x001749F3 File Offset: 0x00172BF3
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Weapon = null;
			this.Damage = null;
			this.Result = 0;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x00174A1E File Offset: 0x00172C1E
		public static DefendMeleeHitEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Weapon, Damage Damage, int Result)
		{
			DefendMeleeHitEvent defendMeleeHitEvent = PooledEvent<DefendMeleeHitEvent>.FromPool();
			defendMeleeHitEvent.Attacker = Attacker;
			defendMeleeHitEvent.Defender = Defender;
			defendMeleeHitEvent.Weapon = Weapon;
			defendMeleeHitEvent.Damage = Damage;
			defendMeleeHitEvent.Result = Result;
			return defendMeleeHitEvent;
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x00174A4C File Offset: 0x00172C4C
		public static void Send(GameObject Attacker, GameObject Defender, GameObject Weapon, Damage Damage, int Result)
		{
			if (Defender.WantEvent(PooledEvent<DefendMeleeHitEvent>.ID, DefendMeleeHitEvent.CascadeLevel) && !Defender.HandleEvent(DefendMeleeHitEvent.FromPool(Attacker, Defender, Weapon, Damage, Result)))
			{
				return;
			}
			if (Defender.HasRegisteredEvent("DefendMeleeHit"))
			{
				Event @event = Event.New("DefendMeleeHit", 0, 0, 0);
				@event.SetParameter("Attacker", Attacker);
				@event.SetParameter("Defender", Defender);
				@event.SetParameter("Weapon", Weapon);
				@event.SetParameter("Damage", Damage);
				@event.SetParameter("Result", Result);
				Defender.FireEvent(@event);
			}
		}

		// Token: 0x040023F9 RID: 9209
		public new static readonly int CascadeLevel = 3;

		// Token: 0x040023FA RID: 9210
		public GameObject Attacker;

		// Token: 0x040023FB RID: 9211
		public GameObject Defender;

		// Token: 0x040023FC RID: 9212
		public GameObject Weapon;

		// Token: 0x040023FD RID: 9213
		public Damage Damage;

		// Token: 0x040023FE RID: 9214
		public int Result;
	}
}
