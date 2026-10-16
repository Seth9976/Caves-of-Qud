using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CDE RID: 3294
	[Serializable]
	public class CookingDomainPlant_UnitPlantReputationLowTier : ProceduralCookingEffectUnit
	{
		// Token: 0x060082C7 RID: 33479 RVA: 0x002F841C File Offset: 0x002F661C
		public override string GetDescription()
		{
			return "+100 reputation with flowers, roots, succulents, trees, vines, and the Consortium of Phyta";
		}

		// Token: 0x060082C8 RID: 33480 RVA: 0x002F8424 File Offset: 0x002F6624
		public override void Apply(GameObject Object, Effect parent)
		{
			if (Object.IsPlayer())
			{
				The.Game.PlayerReputation.Modify("Flowers", 100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Roots", 100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Succulents", 100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Trees", 100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Vines", 100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Consortium", 100, "Cooking", null, null, true, true);
				this.Applied = true;
			}
		}

		// Token: 0x060082C9 RID: 33481 RVA: 0x002F8504 File Offset: 0x002F6704
		public override void Remove(GameObject Object, Effect parent)
		{
			if (this.Applied)
			{
				The.Game.PlayerReputation.Modify("Flowers", -100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Roots", -100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Succulents", -100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Trees", -100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Vines", -100, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Consortium", -100, "Cooking", null, null, true, true);
			}
		}

		// Token: 0x04003941 RID: 14657
		public int Tier;

		// Token: 0x04003942 RID: 14658
		private bool Applied;
	}
}
