using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D8E RID: 3470
	[Serializable]
	public class Spectacles : Effect, ITierInitialized
	{
		// Token: 0x06008A25 RID: 35365 RVA: 0x00314D4E File Offset: 0x00312F4E
		public Spectacles()
		{
			this.DisplayName = "corrected vision";
			this.Duration = 9999;
		}

		// Token: 0x06008A26 RID: 35366 RVA: 0x00314D6C File Offset: 0x00312F6C
		public void Initialize(int Tier)
		{
			this.Duration = 9999;
		}

		// Token: 0x06008A27 RID: 35367 RVA: 0x00314D79 File Offset: 0x00312F79
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008A28 RID: 35368 RVA: 0x00314D7C File Offset: 0x00312F7C
		public override int GetEffectType()
		{
			return 16908288;
		}

		// Token: 0x06008A29 RID: 35369 RVA: 0x00314D83 File Offset: 0x00312F83
		public override string GetDescription()
		{
			if (base.Object.GetEffect<Spectacles>() != this)
			{
				return null;
			}
			return base.GetDescription();
		}

		// Token: 0x06008A2A RID: 35370 RVA: 0x00314D9B File Offset: 0x00312F9B
		public override string GetDetails()
		{
			return "Can see things at normal distances.";
		}

		// Token: 0x06008A2B RID: 35371 RVA: 0x00314DA4 File Offset: 0x00312FA4
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_genericBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return base.Apply(Object);
		}
	}
}
