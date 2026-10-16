using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CCF RID: 3279
	[Serializable]
	public class CookingDomainPhase_Phase_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x06008283 RID: 33411 RVA: 0x002F7D4D File Offset: 0x002F5F4D
		public override string GetDescription()
		{
			return "@they phase out for 20 turns.";
		}

		// Token: 0x06008284 RID: 33412 RVA: 0x002F7D54 File Offset: 0x002F5F54
		public override string GetNotification()
		{
			return "@they phase out.";
		}

		// Token: 0x06008285 RID: 33413 RVA: 0x002F7D5B File Offset: 0x002F5F5B
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer() && !go.HasEffect<Phased>() && IComponent<GameObject>.CheckRealityDistortionAccessibility(go) && !go.OnWorldMap())
			{
				go.ApplyEffect(new Phased(20), null);
			}
		}
	}
}
