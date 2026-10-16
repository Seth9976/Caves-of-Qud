using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000670 RID: 1648
	[GameEvent(Cascade = 17)]
	public class AfterShieldBlockEvent : PooledEvent<AfterShieldBlockEvent>
	{
		// Token: 0x060046E6 RID: 18150 RVA: 0x00167F3F File Offset: 0x0016613F
		public override int GetCascadeLevel()
		{
			return AfterShieldBlockEvent.CascadeLevel;
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x00167F46 File Offset: 0x00166146
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x00167F4F File Offset: 0x0016614F
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Weapon = null;
			this.Shield = null;
			this.ShieldPart = null;
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x00167F7C File Offset: 0x0016617C
		public static void Send(GameObject Attacker, GameObject Defender, GameObject Weapon, GameObject Shield, Shield ShieldPart)
		{
			AfterShieldBlockEvent afterShieldBlockEvent = PooledEvent<AfterShieldBlockEvent>.FromPool();
			afterShieldBlockEvent.Attacker = Attacker;
			afterShieldBlockEvent.Defender = Defender;
			afterShieldBlockEvent.Weapon = Weapon;
			afterShieldBlockEvent.Shield = Shield;
			afterShieldBlockEvent.ShieldPart = ShieldPart;
			Attacker.HandleEvent(afterShieldBlockEvent);
			Defender.HandleEvent(afterShieldBlockEvent);
			PooledEvent<AfterShieldBlockEvent>.ResetTo(ref afterShieldBlockEvent);
		}

		// Token: 0x040021C5 RID: 8645
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040021C6 RID: 8646
		public GameObject Attacker;

		// Token: 0x040021C7 RID: 8647
		public GameObject Defender;

		// Token: 0x040021C8 RID: 8648
		public GameObject Weapon;

		// Token: 0x040021C9 RID: 8649
		public GameObject Shield;

		// Token: 0x040021CA RID: 8650
		public Shield ShieldPart;
	}
}
