using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CD0 RID: 3280
	[Serializable]
	public class CookingDomainPhase_NoPhase_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008287 RID: 33415 RVA: 0x002F7D94 File Offset: 0x002F5F94
		public override string GetDescription()
		{
			return "@they can't be phased for 2 hours.";
		}

		// Token: 0x06008288 RID: 33416 RVA: 0x002F7D9B File Offset: 0x002F5F9B
		public override string GetNotification()
		{
			return "@they become phase-anchored.";
		}

		// Token: 0x06008289 RID: 33417 RVA: 0x002F7DA2 File Offset: 0x002F5FA2
		public override void Apply(GameObject go)
		{
			if (!go.HasEffect<NoPhase_ProceduralCookingTriggeredAction_Effect>())
			{
				go.ApplyEffect(new NoPhase_ProceduralCookingTriggeredAction_Effect(), null);
			}
		}
	}
}
