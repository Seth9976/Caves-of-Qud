using System;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x02000797 RID: 1943
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetThrownWeaponPerformanceEvent : PooledEvent<GetThrownWeaponPerformanceEvent>
	{
		// Token: 0x06004F58 RID: 20312 RVA: 0x001835A1 File Offset: 0x001817A1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x001835AC File Offset: 0x001817AC
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Attacker = null;
			this.Defender = null;
			this.BaseDamage = null;
			this.DamageDieModifier = 0;
			this.DamageModifier = 0;
			this.Penetration = 0;
			this.PenetrationBonus = 0;
			this.PenetrationModifier = 0;
			this.Vorpal = false;
			this.Prospective = false;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x0018360C File Offset: 0x0018180C
		public static void GetFor(GameObject Object, ref string Damage, ref int Penetration, ref int PenetrationBonus, ref int PenetrationModifier, ref bool Vorpal, bool Prospective = false, GameObject Attacker = null, GameObject Defender = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetThrownWeaponPerformance"))
			{
				Event @event = Event.New("GetThrownWeaponPerformance", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Attacker", Attacker);
				@event.SetParameter("Defender", Defender);
				@event.SetParameter("Damage", Damage);
				@event.SetParameter("Penetration", Penetration);
				@event.SetParameter("PenetrationBonus", PenetrationBonus);
				@event.SetParameter("PenetrationModifier", PenetrationModifier);
				@event.SetFlag("Vorpal", Vorpal);
				@event.SetFlag("Prospective", Prospective);
				flag = Object.FireEvent(@event);
				Damage = @event.GetStringParameter("Damage", null);
				Penetration = @event.GetIntParameter("Penetration", 0);
				PenetrationBonus = @event.GetIntParameter("PenetrationBonus", 0);
				PenetrationModifier = @event.GetIntParameter("PenetrationModifier", 0);
				Vorpal = @event.HasFlag("Vorpal");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetThrownWeaponPerformanceEvent>.ID, MinEvent.CascadeLevel))
			{
				GetThrownWeaponPerformanceEvent getThrownWeaponPerformanceEvent = PooledEvent<GetThrownWeaponPerformanceEvent>.FromPool();
				getThrownWeaponPerformanceEvent.Object = Object;
				getThrownWeaponPerformanceEvent.Attacker = Attacker;
				getThrownWeaponPerformanceEvent.Defender = Defender;
				getThrownWeaponPerformanceEvent.BaseDamage = Damage;
				getThrownWeaponPerformanceEvent.Penetration = Penetration;
				getThrownWeaponPerformanceEvent.PenetrationBonus = PenetrationBonus;
				getThrownWeaponPerformanceEvent.PenetrationModifier = PenetrationModifier;
				getThrownWeaponPerformanceEvent.Vorpal = Vorpal;
				getThrownWeaponPerformanceEvent.Prospective = Prospective;
				flag = Object.HandleEvent(getThrownWeaponPerformanceEvent);
				Damage = getThrownWeaponPerformanceEvent.BaseDamage;
				Penetration = getThrownWeaponPerformanceEvent.Penetration;
				PenetrationBonus = getThrownWeaponPerformanceEvent.PenetrationBonus;
				PenetrationModifier = getThrownWeaponPerformanceEvent.PenetrationModifier;
				Vorpal = getThrownWeaponPerformanceEvent.Vorpal;
				if (getThrownWeaponPerformanceEvent.DamageDieModifier != 0)
				{
					Damage = DieRoll.AdjustDieSize(Damage, getThrownWeaponPerformanceEvent.DamageDieModifier);
				}
				if (getThrownWeaponPerformanceEvent.DamageModifier != 0)
				{
					Damage = DieRoll.AdjustResult(Damage, getThrownWeaponPerformanceEvent.DamageModifier);
				}
			}
		}

		// Token: 0x04002706 RID: 9990
		public GameObject Object;

		// Token: 0x04002707 RID: 9991
		public GameObject Attacker;

		// Token: 0x04002708 RID: 9992
		public GameObject Defender;

		// Token: 0x04002709 RID: 9993
		public string BaseDamage;

		// Token: 0x0400270A RID: 9994
		public int DamageDieModifier;

		// Token: 0x0400270B RID: 9995
		public int DamageModifier;

		// Token: 0x0400270C RID: 9996
		public int Penetration;

		// Token: 0x0400270D RID: 9997
		public int PenetrationBonus;

		// Token: 0x0400270E RID: 9998
		public int PenetrationModifier;

		// Token: 0x0400270F RID: 9999
		public bool Vorpal;

		// Token: 0x04002710 RID: 10000
		public bool Prospective;
	}
}
