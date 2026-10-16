using System;
using XRL.Messages;

namespace XRL.World.Effects
{
	// Token: 0x02000CD1 RID: 3281
	[Serializable]
	public class NoPhase_ProceduralCookingTriggeredAction_Effect : BasicTriggeredCookingEffect
	{
		// Token: 0x0600828B RID: 33419 RVA: 0x002F7DC1 File Offset: 0x002F5FC1
		public override string GetDetails()
		{
			return "Can't be phased.";
		}

		// Token: 0x0600828C RID: 33420 RVA: 0x002F7DC8 File Offset: 0x002F5FC8
		public NoPhase_ProceduralCookingTriggeredAction_Effect()
		{
			this.Duration = 100;
			this.DisplayName = null;
		}

		// Token: 0x0600828D RID: 33421 RVA: 0x002F7DDF File Offset: 0x002F5FDF
		public override void ApplyEffect(GameObject Object)
		{
			this.Duration = 100;
			Object.RegisterEffectEvent(this, "ApplyPhased");
			base.ApplyEffect(Object);
		}

		// Token: 0x0600828E RID: 33422 RVA: 0x002F7DFC File Offset: 0x002F5FFC
		public override void RemoveEffect(GameObject Object)
		{
			Object.UnregisterEffectEvent(this, "ApplyPhased");
			base.RemoveEffect(Object);
		}

		// Token: 0x0600828F RID: 33423 RVA: 0x002F7E11 File Offset: 0x002F6011
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyPhased")
			{
				if (base.Object != null && base.Object.IsPlayer())
				{
					MessageQueue.AddPlayerMessage("Your phase remains stable.", null, true);
				}
				return false;
			}
			return base.FireEvent(E);
		}
	}
}
