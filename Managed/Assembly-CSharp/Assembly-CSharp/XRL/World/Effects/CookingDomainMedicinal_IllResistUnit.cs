using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CCD RID: 3277
	[Serializable]
	public class CookingDomainMedicinal_IllResistUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x06008278 RID: 33400 RVA: 0x002F7C25 File Offset: 0x002F5E25
		public override void Init(GameObject target)
		{
		}

		// Token: 0x06008279 RID: 33401 RVA: 0x002F7C27 File Offset: 0x002F5E27
		public override string GetDescription()
		{
			return "@thisCreature only get@s Ill for 1/10th the usual length of time.";
		}

		// Token: 0x0600827A RID: 33402 RVA: 0x002F7C2E File Offset: 0x002F5E2E
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "EndTurn");
		}

		// Token: 0x0600827B RID: 33403 RVA: 0x002F7C3C File Offset: 0x002F5E3C
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "EndTurn");
		}

		// Token: 0x0600827C RID: 33404 RVA: 0x002F7C4C File Offset: 0x002F5E4C
		public override void FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				try
				{
					Ill ill;
					if (this.parent != null && this.parent.Object != null && this.parent.Object.TryGetEffect<Ill>(out ill))
					{
						ill.Duration -= 9;
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("CookingDomainMedicinal_IllResistUnit::EndTurn", ex, "game_exception");
				}
				return;
			}
		}
	}
}
