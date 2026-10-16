using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C58 RID: 3160
	[Serializable]
	public class CookingDomainArtifact_UnitPsychometry : ProceduralCookingEffectUnitMutation<Psychometry>
	{
		// Token: 0x060080BC RID: 32956 RVA: 0x002F5037 File Offset: 0x002F3237
		public CookingDomainArtifact_UnitPsychometry()
		{
			this.AddedTier = "1-2";
			this.BonusTier = "3-4";
		}
	}
}
