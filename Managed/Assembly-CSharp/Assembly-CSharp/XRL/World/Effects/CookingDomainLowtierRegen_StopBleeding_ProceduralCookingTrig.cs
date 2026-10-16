using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CF5 RID: 3317
	[Serializable]
	public class CookingDomainLowtierRegen_StopBleeding_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600835E RID: 33630 RVA: 0x002FAB5A File Offset: 0x002F8D5A
		public override string GetDescription()
		{
			return "@they stop bleeding.";
		}

		// Token: 0x0600835F RID: 33631 RVA: 0x002FAB64 File Offset: 0x002F8D64
		public override void Apply(GameObject go)
		{
			int num = 10;
			while (go.HasEffect<Bleeding>())
			{
				go.RemoveEffect<Bleeding>();
				num--;
				if (num <= 0)
				{
					break;
				}
			}
		}
	}
}
