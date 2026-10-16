using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CD4 RID: 3284
	[Serializable]
	public class CookingDomainPhase_UnitDoublePhaseDuration : ProceduralCookingEffectUnit
	{
		// Token: 0x06008299 RID: 33433 RVA: 0x002F7EE1 File Offset: 0x002F60E1
		public override string GetDescription()
		{
			return "Phase effects last twice as long.";
		}

		// Token: 0x0600829A RID: 33434 RVA: 0x002F7EE8 File Offset: 0x002F60E8
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "ApplyEffect");
		}

		// Token: 0x0600829B RID: 33435 RVA: 0x002F7EF6 File Offset: 0x002F60F6
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "ApplyEffect");
		}

		// Token: 0x0600829C RID: 33436 RVA: 0x002F7F04 File Offset: 0x002F6104
		public override void FireEvent(Event E)
		{
			if (E.ID == "ApplyEffect")
			{
				Effect effect = E.GetParameter("Effect") as Effect;
				if (effect != null && effect.GetType().Name == "Phased")
				{
					effect.Duration *= 2;
				}
			}
		}

		// Token: 0x0400393B RID: 14651
		public GameObject Object;
	}
}
