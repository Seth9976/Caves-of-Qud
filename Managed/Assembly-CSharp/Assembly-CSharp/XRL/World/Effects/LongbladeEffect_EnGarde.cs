using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D5C RID: 3420
	[Serializable]
	public class LongbladeEffect_EnGarde : Effect, ITierInitialized
	{
		// Token: 0x0600874D RID: 34637 RVA: 0x003096EC File Offset: 0x003078EC
		public LongbladeEffect_EnGarde()
		{
			this.DisplayName = "{{G|En garde!}}";
			this.Duration = 9999;
		}

		// Token: 0x0600874E RID: 34638 RVA: 0x0030970A File Offset: 0x0030790A
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(10, 20);
		}

		// Token: 0x0600874F RID: 34639 RVA: 0x0030971B File Offset: 0x0030791B
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008750 RID: 34640 RVA: 0x0030971E File Offset: 0x0030791E
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x06008751 RID: 34641 RVA: 0x00309725 File Offset: 0x00307925
		public override string GetDetails()
		{
			return "Lunge and Swipe have no cooldown.";
		}

		// Token: 0x06008752 RID: 34642 RVA: 0x0030972C File Offset: 0x0030792C
		public override bool Apply(GameObject Object)
		{
			Object.RemoveEffect<LongbladeEffect_EnGarde>();
			return true;
		}

		// Token: 0x06008753 RID: 34643 RVA: 0x00309736 File Offset: 0x00307936
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && XRLCore.CurrentFrame % 20 > 10)
			{
				E.RenderString = "!";
				E.ColorString = "&G";
				E.DetailColor = "W";
			}
			return true;
		}
	}
}
