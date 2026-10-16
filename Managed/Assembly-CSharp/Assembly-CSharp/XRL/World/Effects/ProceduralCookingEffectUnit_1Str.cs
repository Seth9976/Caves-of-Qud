using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C61 RID: 3169
	[Serializable]
	public class ProceduralCookingEffectUnit_1Str : ProceduralCookingEffectUnit
	{
		// Token: 0x060080DE RID: 32990 RVA: 0x002F5516 File Offset: 0x002F3716
		public override string GetDescription()
		{
			return "+1 STR";
		}

		// Token: 0x060080DF RID: 32991 RVA: 0x002F551D File Offset: 0x002F371D
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.GetStat("Strength").Bonus++;
		}

		// Token: 0x060080E0 RID: 32992 RVA: 0x002F5537 File Offset: 0x002F3737
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.GetStat("Strength").Bonus--;
		}
	}
}
