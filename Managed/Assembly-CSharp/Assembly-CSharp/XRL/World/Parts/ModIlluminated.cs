using System;

namespace XRL.World.Parts
{
	// Token: 0x02001147 RID: 4423
	[Serializable]
	public class ModIlluminated : IModification
	{
		// Token: 0x0600AF89 RID: 44937 RVA: 0x0040B5B9 File Offset: 0x004097B9
		public ModIlluminated()
		{
		}

		// Token: 0x0600AF8A RID: 44938 RVA: 0x0040B5C1 File Offset: 0x004097C1
		public ModIlluminated(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF8B RID: 44939 RVA: 0x0040B5CA File Offset: 0x004097CA
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AF8C RID: 44940 RVA: 0x0040B5D3 File Offset: 0x004097D3
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasTag("Book");
		}

		// Token: 0x0600AF8D RID: 44941 RVA: 0x0040B5E0 File Offset: 0x004097E0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AF8E RID: 44942 RVA: 0x0040B5FE File Offset: 0x004097FE
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood())
			{
				E.AddAdjective("[{{illuminated|illuminated}}]", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF8F RID: 44943 RVA: 0x0040B61B File Offset: 0x0040981B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModIlluminated.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF90 RID: 44944 RVA: 0x0040B63B File Offset: 0x0040983B
		public static string GetDescription(int Tier)
		{
			return "Illuminated: This book is adorned with decorative marginalia and illustrations. It's worth 30% more.";
		}
	}
}
