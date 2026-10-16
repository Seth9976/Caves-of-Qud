using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006A0 RID: 1696
	[GameEvent(Cascade = 17)]
	public class BeforeShieldBlockEvent : PooledEvent<BeforeShieldBlockEvent>
	{
		// Token: 0x06004856 RID: 18518 RVA: 0x0016C065 File Offset: 0x0016A265
		public override int GetCascadeLevel()
		{
			return BeforeShieldBlockEvent.CascadeLevel;
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x0016C06C File Offset: 0x0016A26C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0016C075 File Offset: 0x0016A275
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Weapon = null;
			this.Shield = null;
			this.ShieldPart = null;
			this.Chance = 0;
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x0016C0A8 File Offset: 0x0016A2A8
		public static bool Check(GameObject Attacker, GameObject Defender, GameObject Weapon, GameObject Shield, Shield ShieldPart, ref int Chance)
		{
			BeforeShieldBlockEvent beforeShieldBlockEvent = PooledEvent<BeforeShieldBlockEvent>.FromPool();
			beforeShieldBlockEvent.Attacker = Attacker;
			beforeShieldBlockEvent.Defender = Defender;
			beforeShieldBlockEvent.Weapon = Weapon;
			beforeShieldBlockEvent.Shield = Shield;
			beforeShieldBlockEvent.ShieldPart = ShieldPart;
			beforeShieldBlockEvent.Chance = Chance;
			bool flag = Attacker.HandleEvent(beforeShieldBlockEvent) && Defender.HandleEvent(beforeShieldBlockEvent);
			Chance = beforeShieldBlockEvent.Chance;
			PooledEvent<BeforeShieldBlockEvent>.ResetTo(ref beforeShieldBlockEvent);
			return flag;
		}

		// Token: 0x04002294 RID: 8852
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002295 RID: 8853
		public GameObject Attacker;

		// Token: 0x04002296 RID: 8854
		public GameObject Defender;

		// Token: 0x04002297 RID: 8855
		public GameObject Weapon;

		// Token: 0x04002298 RID: 8856
		public GameObject Shield;

		// Token: 0x04002299 RID: 8857
		public Shield ShieldPart;

		// Token: 0x0400229A RID: 8858
		public int Chance;
	}
}
