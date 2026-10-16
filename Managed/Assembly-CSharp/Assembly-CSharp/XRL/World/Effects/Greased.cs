using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D41 RID: 3393
	[Serializable]
	public class Greased : Effect, ITierInitialized
	{
		// Token: 0x060085F8 RID: 34296 RVA: 0x003043DB File Offset: 0x003025DB
		public Greased()
		{
			this.DisplayName = "greased";
			this.Duration = 9999;
		}

		// Token: 0x060085F9 RID: 34297 RVA: 0x003043F9 File Offset: 0x003025F9
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(200, 1000);
		}

		// Token: 0x060085FA RID: 34298 RVA: 0x00304410 File Offset: 0x00302610
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060085FB RID: 34299 RVA: 0x00304413 File Offset: 0x00302613
		public override int GetEffectType()
		{
			return 16777248;
		}

		// Token: 0x060085FC RID: 34300 RVA: 0x0030441A File Offset: 0x0030261A
		public override string GetDetails()
		{
			return "Can walk over webs without getting stuck.";
		}

		// Token: 0x060085FD RID: 34301 RVA: 0x00304424 File Offset: 0x00302624
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_movementBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return base.Apply(Object);
		}
	}
}
