using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D50 RID: 3408
	[Serializable]
	public abstract class IShatterEffect : Effect
	{
		// Token: 0x060086AC RID: 34476 RVA: 0x0030685E File Offset: 0x00304A5E
		public override int GetEffectType()
		{
			return 100664320;
		}

		// Token: 0x060086AD RID: 34477
		public abstract int GetPenalty();

		// Token: 0x060086AE RID: 34478
		public abstract void IncrementPenalty();

		// Token: 0x060086AF RID: 34479
		public abstract GameObject GetOwner();

		// Token: 0x060086B0 RID: 34480
		public abstract void SetOwner(GameObject Owner);
	}
}
