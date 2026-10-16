using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CCE RID: 3278
	[Serializable]
	public class CookingDomainMedicinal_DiseaseResistUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x0600827E RID: 33406 RVA: 0x002F7CD4 File Offset: 0x002F5ED4
		public override void Init(GameObject target)
		{
		}

		// Token: 0x0600827F RID: 33407 RVA: 0x002F7CD6 File Offset: 0x002F5ED6
		public override string GetDescription()
		{
			return "+3 to saves vs. disease";
		}

		// Token: 0x06008280 RID: 33408 RVA: 0x002F7CDD File Offset: 0x002F5EDD
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "ModifyDefendingSave");
		}

		// Token: 0x06008281 RID: 33409 RVA: 0x002F7CEB File Offset: 0x002F5EEB
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "ModifyDefendingSave");
		}

		// Token: 0x06008282 RID: 33410 RVA: 0x002F7CFC File Offset: 0x002F5EFC
		public override void FireEvent(Event E)
		{
			if (E.ID == "ModifyDefendingSave" && E.GetStringParameter("Vs", null).Contains("Disease"))
			{
				E.SetParameter("Roll", E.GetIntParameter("Roll", 0) + 3);
			}
		}
	}
}
