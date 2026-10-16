using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CB9 RID: 3257
	[Serializable]
	public class CookingDomainFungus_FungusResistUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x0600821D RID: 33309 RVA: 0x002F7398 File Offset: 0x002F5598
		public override void Init(GameObject target)
		{
		}

		// Token: 0x0600821E RID: 33310 RVA: 0x002F739A File Offset: 0x002F559A
		public override string GetDescription()
		{
			return "75% chance that itchy skin doesn't develop into a fungal infection";
		}

		// Token: 0x0600821F RID: 33311 RVA: 0x002F73A1 File Offset: 0x002F55A1
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "BeforeApplyFungalInfection");
		}

		// Token: 0x06008220 RID: 33312 RVA: 0x002F73AF File Offset: 0x002F55AF
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "BeforeApplyFungalInfection");
		}

		// Token: 0x06008221 RID: 33313 RVA: 0x002F73BD File Offset: 0x002F55BD
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyFungalInfection")
			{
				if (75.in100())
				{
					E.AddParameter("Cancelled", true);
				}
				return;
			}
		}
	}
}
