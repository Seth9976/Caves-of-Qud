using System;

namespace XRL.World.Parts
{
	// Token: 0x02001221 RID: 4641
	[Serializable]
	public class SecondaryAttackChance : IPart
	{
		// Token: 0x0600B849 RID: 47177 RVA: 0x00430D53 File Offset: 0x0042EF53
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600B84A RID: 47178 RVA: 0x00430D6C File Offset: 0x0042EF6C
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Intrinsic && !E.Primary && E.Weapon == this.ParentObject)
			{
				if (this.Additive)
				{
					E.Chance += this.Chance;
					E.Multiplier += this.Multiplier - 1.0;
				}
				else
				{
					if (E.Chance < this.Chance)
					{
						E.Chance = this.Chance;
					}
					E.Multiplier *= this.Multiplier;
				}
				if (this.Final)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004A28 RID: 18984
		public int Chance;

		// Token: 0x04004A29 RID: 18985
		public double Multiplier = 1.0;

		// Token: 0x04004A2A RID: 18986
		public bool Additive;

		// Token: 0x04004A2B RID: 18987
		public bool Final;
	}
}
