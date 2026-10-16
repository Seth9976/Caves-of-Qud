using System;

namespace XRL.World.Parts
{
	// Token: 0x020011EB RID: 4587
	[Serializable]
	public class Flame : IPart
	{
		// Token: 0x0600B6B2 RID: 46770 RVA: 0x0042A0E4 File Offset: 0x004282E4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<RespiresEvent>.ID;
		}

		// Token: 0x0600B6B3 RID: 46771 RVA: 0x0042A10A File Offset: 0x0042830A
		public override bool HandleEvent(RespiresEvent E)
		{
			return false;
		}

		// Token: 0x0600B6B4 RID: 46772 RVA: 0x0042A10D File Offset: 0x0042830D
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(4);
			return false;
		}

		// Token: 0x0600B6B5 RID: 46773 RVA: 0x0042A118 File Offset: 0x00428318
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && !this.ParentObject.HasTag("Creature"))
			{
				if (E.Damage.IsHeatDamage() || E.Damage.IsAcidDamage() || E.Damage.HasAttribute("Poison"))
				{
					NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
					E.Damage.Amount = 0;
				}
				else if (E.Damage.IsColdDamage())
				{
					E.Damage.Amount /= 2;
				}
			}
			return base.HandleEvent(E);
		}
	}
}
