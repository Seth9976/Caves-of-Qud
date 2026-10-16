using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CDF RID: 3295
	[Serializable]
	public class CookingDomainPlant_UnitPlantReputationHighTier : ProceduralCookingEffectUnit
	{
		// Token: 0x060082CB RID: 33483 RVA: 0x002F85E4 File Offset: 0x002F67E4
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.Applied = false;
		}

		// Token: 0x060082CC RID: 33484 RVA: 0x002F85F4 File Offset: 0x002F67F4
		public override string GetDescription()
		{
			return "+200 reputation with flowers, roots, succulents, trees, vines, and the Consortium of Phyta";
		}

		// Token: 0x060082CD RID: 33485 RVA: 0x002F85FC File Offset: 0x002F67FC
		public override void Apply(GameObject Object, Effect parent)
		{
			if (Object.IsPlayer())
			{
				The.Game.PlayerReputation.Modify("Flowers", 200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Roots", 200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Succulents", 200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Trees", 200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Vines", 200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Consortium", 200, "Cooking", null, null, true, true);
				this.Applied = true;
			}
		}

		// Token: 0x060082CE RID: 33486 RVA: 0x002F86F0 File Offset: 0x002F68F0
		public override void Remove(GameObject Object, Effect parent)
		{
			if (this.Applied)
			{
				The.Game.PlayerReputation.Modify("Flowers", -200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Roots", -200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Succulents", -200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Trees", -200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Vines", -200, "Cooking", null, null, true, true);
				The.Game.PlayerReputation.Modify("Consortium", -200, "Cooking", null, null, true, true);
			}
		}

		// Token: 0x04003943 RID: 14659
		public int Tier;

		// Token: 0x04003944 RID: 14660
		private bool Applied;
	}
}
