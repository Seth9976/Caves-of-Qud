using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D08 RID: 3336
	[Serializable]
	public class CookingDomainSelfHarm_UnitSelfGlotrot : ProceduralCookingEffectUnit
	{
		// Token: 0x060083B8 RID: 33720 RVA: 0x002FB3D1 File Offset: 0x002F95D1
		public override string GetDescription()
		{
			return "Causes @their tongue to rot away.";
		}

		// Token: 0x060083B9 RID: 33721 RVA: 0x002FB3D8 File Offset: 0x002F95D8
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.ApplyEffect(new GlotrotOnset(), null);
		}

		// Token: 0x060083BA RID: 33722 RVA: 0x002FB3E7 File Offset: 0x002F95E7
		public override void Remove(GameObject Object, Effect parent)
		{
		}

		// Token: 0x04003969 RID: 14697
		public GameObject Object;
	}
}
