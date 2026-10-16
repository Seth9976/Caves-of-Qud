using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CDA RID: 3290
	[Serializable]
	public class CookingDomainPlant_BurgeoningHighTier_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060082B9 RID: 33465 RVA: 0x002F82E1 File Offset: 0x002F64E1
		public override void Init(GameObject target)
		{
			this.Tier = 10;
			base.Init(target);
		}

		// Token: 0x060082BA RID: 33466 RVA: 0x002F82F2 File Offset: 0x002F64F2
		public override string GetDescription()
		{
			return "@they cause@s plants to spontaneously grow in a random nearby area.";
		}

		// Token: 0x060082BB RID: 33467 RVA: 0x002F82F9 File Offset: 0x002F64F9
		public override string GetNotification()
		{
			return "Plants burgeon around @them!";
		}

		// Token: 0x060082BC RID: 33468 RVA: 0x002F8300 File Offset: 0x002F6500
		public override void Apply(GameObject go)
		{
			UnwelcomeGermination.Germinate(go, this.Tier, 8, true, null);
		}

		// Token: 0x04003940 RID: 14656
		public int Tier;
	}
}
