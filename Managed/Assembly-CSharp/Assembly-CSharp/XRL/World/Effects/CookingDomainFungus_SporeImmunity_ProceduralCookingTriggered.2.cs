using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CB5 RID: 3253
	[Serializable]
	public class CookingDomainFungus_SporeImmunity_ProceduralCookingTriggeredActionEffect : BasicTriggeredCookingEffect
	{
		// Token: 0x0600820A RID: 33290 RVA: 0x002F71DA File Offset: 0x002F53DA
		public CookingDomainFungus_SporeImmunity_ProceduralCookingTriggeredActionEffect()
		{
			this.Duration = 300;
		}

		// Token: 0x0600820B RID: 33291 RVA: 0x002F71ED File Offset: 0x002F53ED
		public override string GetDetails()
		{
			return "Immune to fungal spores.";
		}

		// Token: 0x0600820C RID: 33292 RVA: 0x002F71F4 File Offset: 0x002F53F4
		public override bool Apply(GameObject Object)
		{
			Object.RegisterEffectEvent(this, "ApplySpores");
			return base.Apply(Object);
		}

		// Token: 0x0600820D RID: 33293 RVA: 0x002F7209 File Offset: 0x002F5409
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
			Object.UnregisterEffectEvent(this, "ApplySpores");
		}

		// Token: 0x0600820E RID: 33294 RVA: 0x002F721E File Offset: 0x002F541E
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "ApplySpores") && base.FireEvent(E);
		}
	}
}
