using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C62 RID: 3170
	[Serializable]
	public class ProceduralCookingEffectUnit_10Regeneration : ProceduralCookingEffectUnit
	{
		// Token: 0x060080E2 RID: 32994 RVA: 0x002F5559 File Offset: 0x002F3759
		public override string GetDescription()
		{
			return "+10% to natural healing rate";
		}

		// Token: 0x060080E3 RID: 32995 RVA: 0x002F5560 File Offset: 0x002F3760
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "Regenerating2");
		}

		// Token: 0x060080E4 RID: 32996 RVA: 0x002F556E File Offset: 0x002F376E
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "Regenerating2");
		}

		// Token: 0x060080E5 RID: 32997 RVA: 0x002F557C File Offset: 0x002F377C
		public override void FireEvent(Event E)
		{
			if (E.ID == "Regenerating2")
			{
				int num = (int)Math.Ceiling((double)((float)E.GetIntParameter("Amount", 0) * 0.9f));
				E.SetParameter("Amount", num);
				return;
			}
		}
	}
}
