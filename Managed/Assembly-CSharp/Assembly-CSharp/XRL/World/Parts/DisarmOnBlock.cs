using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200101B RID: 4123
	[Serializable]
	public class DisarmOnBlock : IPart
	{
		// Token: 0x0600A437 RID: 42039 RVA: 0x003C0D43 File Offset: 0x003BEF43
		public DisarmOnBlock()
		{
		}

		// Token: 0x0600A438 RID: 42040 RVA: 0x003C0D5B File Offset: 0x003BEF5B
		public DisarmOnBlock(int Chance)
			: this()
		{
			this.Chance = Chance;
		}

		// Token: 0x0600A439 RID: 42041 RVA: 0x003C0D6A File Offset: 0x003BEF6A
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<AfterShieldBlockEvent>.ID;
		}

		// Token: 0x0600A43A RID: 42042 RVA: 0x003C0D74 File Offset: 0x003BEF74
		public override bool HandleEvent(AfterShieldBlockEvent E)
		{
			if (E.Shield == this.ParentObject || E.Defender == this.ParentObject)
			{
				GameObject defender = E.Defender;
				GameObject parentObject = this.ParentObject;
				string text = "Part DisarmOnBlock Activation";
				GameObject attacker = E.Attacker;
				if (GetSpecialEffectChanceEvent.GetFor(defender, parentObject, text, this.Chance, attacker, null, true, false).in100() && GameObject.Validate(E.Weapon))
				{
					Disarming.Disarm(E.Attacker, E.Defender, this.SaveTarget, this.SaveStat.Coalesce("Strength"), this.DisarmerStat.Coalesce("Agility"), E.Weapon, this.ParentObject);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004413 RID: 17427
		public string SaveStat;

		// Token: 0x04004414 RID: 17428
		public string DisarmerStat;

		// Token: 0x04004415 RID: 17429
		public int SaveTarget = 40;

		// Token: 0x04004416 RID: 17430
		public int Chance = 100;
	}
}
