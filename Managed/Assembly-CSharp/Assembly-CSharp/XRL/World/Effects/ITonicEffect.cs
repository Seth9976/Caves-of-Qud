using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D51 RID: 3409
	[Serializable]
	public abstract class ITonicEffect : Effect
	{
		// Token: 0x060086B2 RID: 34482 RVA: 0x0030686D File Offset: 0x00304A6D
		public override int GetEffectType()
		{
			return 4;
		}

		// Token: 0x060086B3 RID: 34483 RVA: 0x00306870 File Offset: 0x00304A70
		public override bool IsTonic()
		{
			return true;
		}

		// Token: 0x060086B4 RID: 34484
		public abstract void ApplyAllergy(GameObject target);
	}
}
