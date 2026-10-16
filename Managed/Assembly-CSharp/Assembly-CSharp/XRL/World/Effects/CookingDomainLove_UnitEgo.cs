using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CC5 RID: 3269
	[Serializable]
	public class CookingDomainLove_UnitEgo : ProceduralCookingEffectUnit
	{
		// Token: 0x06008257 RID: 33367 RVA: 0x002F7987 File Offset: 0x002F5B87
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Ego";
		}

		// Token: 0x06008258 RID: 33368 RVA: 0x002F799F File Offset: 0x002F5B9F
		public override string GetTemplatedDescription()
		{
			return "+4 Ego";
		}

		// Token: 0x06008259 RID: 33369 RVA: 0x002F79A6 File Offset: 0x002F5BA6
		public override void Init(GameObject target)
		{
			this.Bonus = 4;
		}

		// Token: 0x0600825A RID: 33370 RVA: 0x002F79AF File Offset: 0x002F5BAF
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["Ego"].Bonus += this.Bonus;
		}

		// Token: 0x0600825B RID: 33371 RVA: 0x002F79D3 File Offset: 0x002F5BD3
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["Ego"].Bonus -= this.Bonus;
		}

		// Token: 0x0400393A RID: 14650
		public int Bonus;
	}
}
