using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E49 RID: 3657
	[Serializable]
	public class AcidImmunity : IPart
	{
		// Token: 0x0600942E RID: 37934 RVA: 0x003559EC File Offset: 0x00353BEC
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600942F RID: 37935 RVA: 0x003559EF File Offset: 0x00353BEF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID;
		}

		// Token: 0x06009430 RID: 37936 RVA: 0x00355A08 File Offset: 0x00353C08
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && E.Damage.IsAcidDamage())
			{
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009431 RID: 37937 RVA: 0x00355A63 File Offset: 0x00353C63
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
