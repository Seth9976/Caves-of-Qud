using System;

namespace XRL.World.Parts
{
	// Token: 0x02001139 RID: 4409
	[Serializable]
	public class ModFeathered : IModification
	{
		// Token: 0x0600AEC6 RID: 44742 RVA: 0x00408E2E File Offset: 0x0040702E
		public ModFeathered()
		{
		}

		// Token: 0x0600AEC7 RID: 44743 RVA: 0x00408E41 File Offset: 0x00407041
		public ModFeathered(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AEC8 RID: 44744 RVA: 0x00408E55 File Offset: 0x00407055
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AEC9 RID: 44745 RVA: 0x00408E5E File Offset: 0x0040705E
		public override void ApplyModification(GameObject Object)
		{
			AddsRep.AddModifier(Object, "Birds:" + this.Amount.ToString() + ":hidden");
		}

		// Token: 0x0600AECA RID: 44746 RVA: 0x00408E81 File Offset: 0x00407081
		public override bool SameAs(IPart p)
		{
			return (p as ModFeathered).Amount == this.Amount && base.SameAs(p);
		}

		// Token: 0x0600AECB RID: 44747 RVA: 0x00408E9F File Offset: 0x0040709F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AECC RID: 44748 RVA: 0x00408EC5 File Offset: 0x004070C5
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageIncrease += 50;
			return base.HandleEvent(E);
		}

		// Token: 0x0600AECD RID: 44749 RVA: 0x00408EDD File Offset: 0x004070DD
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{feathered|feathered}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AECE RID: 44750 RVA: 0x00408F07 File Offset: 0x00407107
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Feathered: This item grants the wearer " + this.Amount.Signed(false) + " reputation with birds.");
			return base.HandleEvent(E);
		}

		// Token: 0x040047FC RID: 18428
		public int Amount = 250;
	}
}
