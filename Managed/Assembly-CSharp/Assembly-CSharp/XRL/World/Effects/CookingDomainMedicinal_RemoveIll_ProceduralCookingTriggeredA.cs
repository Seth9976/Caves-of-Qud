using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CC7 RID: 3271
	[Serializable]
	public class CookingDomainMedicinal_RemoveIll_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600825E RID: 33374 RVA: 0x002F7A07 File Offset: 0x002F5C07
		public override string GetDescription()
		{
			return "@they are cured of the Ill status effect.";
		}

		// Token: 0x0600825F RID: 33375 RVA: 0x002F7A0E File Offset: 0x002F5C0E
		public override string GetNotification()
		{
			return "@they feel better.";
		}

		// Token: 0x06008260 RID: 33376 RVA: 0x002F7A15 File Offset: 0x002F5C15
		public override void Apply(GameObject go)
		{
			go.RemoveEffect<Ill>();
		}
	}
}
