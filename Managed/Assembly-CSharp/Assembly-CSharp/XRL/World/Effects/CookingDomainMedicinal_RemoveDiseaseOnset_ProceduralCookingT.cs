using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CC8 RID: 3272
	[Serializable]
	public class CookingDomainMedicinal_RemoveDiseaseOnset_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008262 RID: 33378 RVA: 0x002F7A26 File Offset: 0x002F5C26
		public override string GetDescription()
		{
			return "@they are cured of an incubating disease with a 25% chance.";
		}

		// Token: 0x06008263 RID: 33379 RVA: 0x002F7A2D File Offset: 0x002F5C2D
		public override string GetNotification()
		{
			return "@they feel like you might be fighting off any ailments you have.";
		}

		// Token: 0x06008264 RID: 33380 RVA: 0x002F7A34 File Offset: 0x002F5C34
		public override void Apply(GameObject go)
		{
			if (Stat.Random(1, 100) <= 25)
			{
				go.RemoveEffect<GlotrotOnset>();
				go.RemoveEffect<IronshankOnset>();
			}
		}
	}
}
