using System;
using Qud.API;

namespace XRL.World.Effects
{
	// Token: 0x02000D06 RID: 3334
	[Serializable]
	public class CookingDomainSecrets_RevealSecrets : ProceduralCookingEffectUnit
	{
		// Token: 0x060083B0 RID: 33712 RVA: 0x002FB33B File Offset: 0x002F953B
		public override string GetDescription()
		{
			return "Reveals a secret to @thisCreature.";
		}

		// Token: 0x060083B1 RID: 33713 RVA: 0x002FB344 File Offset: 0x002F9544
		public override void Apply(GameObject Object, Effect parent)
		{
			int num = 1;
			for (int i = 0; i < num; i++)
			{
				if (Object.IsPlayer())
				{
					JournalAPI.RevealRandomSecret();
				}
			}
		}

		// Token: 0x060083B2 RID: 33714 RVA: 0x002FB36C File Offset: 0x002F956C
		public override void Remove(GameObject Object, Effect parent)
		{
		}
	}
}
