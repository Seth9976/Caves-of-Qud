using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EA7 RID: 3751
	[Serializable]
	public class ElectricImmunity : IPart
	{
		// Token: 0x0600969E RID: 38558 RVA: 0x00369256 File Offset: 0x00367456
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID;
		}

		// Token: 0x0600969F RID: 38559 RVA: 0x0036926C File Offset: 0x0036746C
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && E.Damage.IsElectricDamage())
			{
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060096A0 RID: 38560 RVA: 0x003692C7 File Offset: 0x003674C7
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
