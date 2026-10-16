using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CFB RID: 3323
	[Serializable]
	public class CookingDomainRegenLowtier_BleedResistUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x06008376 RID: 33654 RVA: 0x002FADB6 File Offset: 0x002F8FB6
		public override string GetDescription()
		{
			return this.Tier.Signed(false) + " to saves vs. bleeding";
		}

		// Token: 0x06008377 RID: 33655 RVA: 0x002FADCE File Offset: 0x002F8FCE
		public override string GetTemplatedDescription()
		{
			return "+8-12 to saves vs. bleeding";
		}

		// Token: 0x06008378 RID: 33656 RVA: 0x002FADD5 File Offset: 0x002F8FD5
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(8, 12);
		}

		// Token: 0x06008379 RID: 33657 RVA: 0x002FADE5 File Offset: 0x002F8FE5
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "ModifyDefendingSave");
		}

		// Token: 0x0600837A RID: 33658 RVA: 0x002FADF3 File Offset: 0x002F8FF3
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "ModifyDefendingSave");
		}

		// Token: 0x0600837B RID: 33659 RVA: 0x002FAE04 File Offset: 0x002F9004
		public override void FireEvent(Event E)
		{
			if (E.ID == "ModifyDefendingSave" && E.GetStringParameter("Vs", null).Contains("Bleeding"))
			{
				E.SetParameter("Roll", E.GetIntParameter("Roll", 0) + this.Tier);
			}
		}

		// Token: 0x04003966 RID: 14694
		public int Tier;
	}
}
