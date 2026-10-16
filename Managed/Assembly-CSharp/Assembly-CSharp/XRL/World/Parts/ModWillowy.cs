using System;

namespace XRL.World.Parts
{
	// Token: 0x02001178 RID: 4472
	[Serializable]
	public class ModWillowy : IModification
	{
		// Token: 0x0600B1F3 RID: 45555 RVA: 0x00412FF5 File Offset: 0x004111F5
		public ModWillowy()
		{
		}

		// Token: 0x0600B1F4 RID: 45556 RVA: 0x00412FFD File Offset: 0x004111FD
		public ModWillowy(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1F5 RID: 45557 RVA: 0x00413006 File Offset: 0x00411206
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B1F6 RID: 45558 RVA: 0x0041300F File Offset: 0x0041120F
		public override bool ModificationApplicable(GameObject Object)
		{
			return true;
		}

		// Token: 0x0600B1F7 RID: 45559 RVA: 0x00413012 File Offset: 0x00411212
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseComplexityIfComplex(1, null);
		}

		// Token: 0x0600B1F8 RID: 45560 RVA: 0x0041301C File Offset: 0x0041121C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustWeightEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B1F9 RID: 45561 RVA: 0x00413042 File Offset: 0x00411242
		public override bool HandleEvent(AdjustWeightEvent E)
		{
			E.AdjustWeight(0.25);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1FA RID: 45562 RVA: 0x0041305A File Offset: 0x0041125A
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("willowy", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1FB RID: 45563 RVA: 0x00413084 File Offset: 0x00411284
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Willowy: This item weighs much less than normal.");
			return base.HandleEvent(E);
		}
	}
}
