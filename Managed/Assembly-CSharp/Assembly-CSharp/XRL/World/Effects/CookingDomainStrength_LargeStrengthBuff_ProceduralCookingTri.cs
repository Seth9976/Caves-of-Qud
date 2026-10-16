using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D0C RID: 3340
	[Serializable]
	public class CookingDomainStrength_LargeStrengthBuff_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x060083CF RID: 33743 RVA: 0x002FB8F0 File Offset: 0x002F9AF0
		public override string GetDescription()
		{
			return "@they gain@s +8 Strength for 50 turns.";
		}

		// Token: 0x060083D0 RID: 33744 RVA: 0x002FB8F7 File Offset: 0x002F9AF7
		public override string GetNotification()
		{
			return "@their muscles bulge.";
		}

		// Token: 0x060083D1 RID: 33745 RVA: 0x002FB8FE File Offset: 0x002F9AFE
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<CookingDomainStrength_LargeStrengthBuff_ProceduralCookingTriggeredActionEffect>())
			{
				go.ApplyEffect(new CookingDomainStrength_LargeStrengthBuff_ProceduralCookingTriggeredActionEffect(), null);
			}
		}
	}
}
