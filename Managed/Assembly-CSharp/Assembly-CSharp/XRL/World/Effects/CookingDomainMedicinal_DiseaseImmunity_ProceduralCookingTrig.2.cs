using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CCA RID: 3274
	[Serializable]
	public class CookingDomainMedicinal_DiseaseImmunity_ProceduralCookingTriggeredActionEffect : BasicTriggeredCookingEffect
	{
		// Token: 0x0600826A RID: 33386 RVA: 0x002F7A85 File Offset: 0x002F5C85
		public CookingDomainMedicinal_DiseaseImmunity_ProceduralCookingTriggeredActionEffect()
		{
			this.Duration = 300;
		}

		// Token: 0x0600826B RID: 33387 RVA: 0x002F7A98 File Offset: 0x002F5C98
		public override string GetDetails()
		{
			return "Immune to disease onset.";
		}

		// Token: 0x0600826C RID: 33388 RVA: 0x002F7A9F File Offset: 0x002F5C9F
		public override bool Apply(GameObject Object)
		{
			Object.RegisterEffectEvent(this, "ApplyDiseaseOnset");
			return base.Apply(Object);
		}

		// Token: 0x0600826D RID: 33389 RVA: 0x002F7AB4 File Offset: 0x002F5CB4
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
			Object.UnregisterEffectEvent(this, "ApplyDiseaseOnset");
		}

		// Token: 0x0600826E RID: 33390 RVA: 0x002F7AC9 File Offset: 0x002F5CC9
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "ApplyDiseaseOnset") && base.FireEvent(E);
		}
	}
}
