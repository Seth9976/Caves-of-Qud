using System;

namespace XRL.World.Parts
{
	// Token: 0x0200116B RID: 4459
	[Serializable]
	public class ModSlender : IModification
	{
		// Token: 0x0600B162 RID: 45410 RVA: 0x00411908 File Offset: 0x0040FB08
		public ModSlender()
		{
		}

		// Token: 0x0600B163 RID: 45411 RVA: 0x00411910 File Offset: 0x0040FB10
		public ModSlender(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B164 RID: 45412 RVA: 0x00411919 File Offset: 0x0040FB19
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B165 RID: 45413 RVA: 0x00411922 File Offset: 0x0040FB22
		public override bool ModificationApplicable(GameObject Object)
		{
			return true;
		}

		// Token: 0x0600B166 RID: 45414 RVA: 0x00411925 File Offset: 0x0040FB25
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseComplexityIfComplex(1, null);
		}

		// Token: 0x0600B167 RID: 45415 RVA: 0x0041192F File Offset: 0x0040FB2F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B168 RID: 45416 RVA: 0x00411932 File Offset: 0x0040FB32
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustWeightEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B169 RID: 45417 RVA: 0x00411958 File Offset: 0x0040FB58
		public override bool HandleEvent(AdjustWeightEvent E)
		{
			E.AdjustWeight(0.5);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B16A RID: 45418 RVA: 0x00411970 File Offset: 0x0040FB70
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("slender", -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B16B RID: 45419 RVA: 0x0041199B File Offset: 0x0040FB9B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Slender: This item weighs less than normal.");
			return base.HandleEvent(E);
		}
	}
}
