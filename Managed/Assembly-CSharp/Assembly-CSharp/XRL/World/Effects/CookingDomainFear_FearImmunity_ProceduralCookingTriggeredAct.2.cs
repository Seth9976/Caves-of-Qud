using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CA3 RID: 3235
	[Serializable]
	public class CookingDomainFear_FearImmunity_ProceduralCookingTriggeredActionEffect : BasicTriggeredCookingEffect
	{
		// Token: 0x060081CC RID: 33228 RVA: 0x002F6C61 File Offset: 0x002F4E61
		public CookingDomainFear_FearImmunity_ProceduralCookingTriggeredActionEffect()
		{
			this.Duration = 300;
		}

		// Token: 0x060081CD RID: 33229 RVA: 0x002F6C74 File Offset: 0x002F4E74
		public override string GetDetails()
		{
			return "Immune to fear.";
		}

		// Token: 0x060081CE RID: 33230 RVA: 0x002F6C7B File Offset: 0x002F4E7B
		public override bool Apply(GameObject Object)
		{
			Object.RegisterEffectEvent(this, "ApplyFear");
			return base.Apply(Object);
		}

		// Token: 0x060081CF RID: 33231 RVA: 0x002F6C90 File Offset: 0x002F4E90
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
			Object.UnregisterEffectEvent(this, "ApplyFear");
		}

		// Token: 0x060081D0 RID: 33232 RVA: 0x002F6CA5 File Offset: 0x002F4EA5
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "ApplyFear") && base.FireEvent(E);
		}
	}
}
