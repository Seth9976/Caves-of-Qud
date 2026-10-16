using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200103A RID: 4154
	[Serializable]
	public class GasMask : IPart
	{
		// Token: 0x0600A566 RID: 42342 RVA: 0x003C80A6 File Offset: 0x003C62A6
		public override bool SameAs(IPart p)
		{
			return (p as GasMask).Power == this.Power && base.SameAs(p);
		}

		// Token: 0x0600A567 RID: 42343 RVA: 0x003C80C4 File Offset: 0x003C62C4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetRespiratoryAgentPerformanceEvent>.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600A568 RID: 42344 RVA: 0x003C80EA File Offset: 0x003C62EA
		public override bool HandleEvent(GetRespiratoryAgentPerformanceEvent E)
		{
			if (!E.WillAllowSave && E.Object == this.ParentObject.Equipped)
			{
				E.LinearAdjustment -= this.Power * 5;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A569 RID: 42345 RVA: 0x003C8123 File Offset: 0x003C6323
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Inhaled Gas", E.Vs))
			{
				E.Roll += this.Power;
				E.IgnoreNatural1 = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A56A RID: 42346 RVA: 0x003C8158 File Offset: 0x003C6358
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Damage != null && E.Damage.HasAttribute("Gas") && E.Object == this.ParentObject.Equipped)
			{
				E.Damage.Amount = E.Damage.Amount * (100 - this.Power) / 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400445B RID: 17499
		public int Power = 10;
	}
}
