using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CC9 RID: 3273
	[Serializable]
	public class CookingDomainMedicinal_DiseaseImmunity_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008266 RID: 33382 RVA: 0x002F7A58 File Offset: 0x002F5C58
		public override string GetDescription()
		{
			return "@they can't acquire new diseases for 6 hours.";
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x002F7A5F File Offset: 0x002F5C5F
		public override string GetNotification()
		{
			return "@they become impervious to disease.";
		}

		// Token: 0x06008268 RID: 33384 RVA: 0x002F7A66 File Offset: 0x002F5C66
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainMedicinal_DiseaseImmunity_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainMedicinal_DiseaseImmunity_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
