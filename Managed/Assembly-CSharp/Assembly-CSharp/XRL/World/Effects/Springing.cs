using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D91 RID: 3473
	[Serializable]
	public class Springing : Effect, ITierInitialized
	{
		// Token: 0x06008A4D RID: 35405 RVA: 0x00315537 File Offset: 0x00313737
		public Springing()
		{
			this.DisplayName = "springing";
			this.Duration = 9999;
		}

		// Token: 0x06008A4E RID: 35406 RVA: 0x00315555 File Offset: 0x00313755
		public Springing(GameObject Source)
			: this()
		{
			this.Source = Source;
		}

		// Token: 0x06008A4F RID: 35407 RVA: 0x00315564 File Offset: 0x00313764
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 300);
		}

		// Token: 0x06008A50 RID: 35408 RVA: 0x00315578 File Offset: 0x00313778
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008A51 RID: 35409 RVA: 0x0031557B File Offset: 0x0031377B
		public override int GetEffectType()
		{
			return 16777344;
		}

		// Token: 0x06008A52 RID: 35410 RVA: 0x00315582 File Offset: 0x00313782
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008A53 RID: 35411 RVA: 0x00315585 File Offset: 0x00313785
		public override string GetDescription()
		{
			return "springing";
		}

		// Token: 0x06008A54 RID: 35412 RVA: 0x0031558C File Offset: 0x0031378C
		public override string GetDetails()
		{
			return "Sprints or power skates at thrice move speed.";
		}

		// Token: 0x06008A55 RID: 35413 RVA: 0x00315594 File Offset: 0x00313794
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_movementBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (this.Source == null)
			{
				Springing effect2 = Object.GetEffect<Springing>((Springing effect) => effect.Source == null);
				if (effect2 != null)
				{
					effect2.Duration = Math.Max(effect2.Duration, this.Duration);
					return false;
				}
			}
			return base.Apply(Object);
		}

		// Token: 0x04003A9A RID: 15002
		public GameObject Source;
	}
}
