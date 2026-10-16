using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D1E RID: 3358
	[Serializable]
	public class Dashing : Effect, ITierInitialized
	{
		// Token: 0x06008433 RID: 33843 RVA: 0x002FC4E5 File Offset: 0x002FA6E5
		public Dashing()
		{
			this.DisplayName = "{{W|dashing}}";
		}

		// Token: 0x06008434 RID: 33844 RVA: 0x002FC4F8 File Offset: 0x002FA6F8
		public Dashing(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008435 RID: 33845 RVA: 0x002FC507 File Offset: 0x002FA707
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(10, 20);
		}

		// Token: 0x06008436 RID: 33846 RVA: 0x002FC518 File Offset: 0x002FA718
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008437 RID: 33847 RVA: 0x002FC51B File Offset: 0x002FA71B
		public override int GetEffectType()
		{
			return 128;
		}

		// Token: 0x06008438 RID: 33848 RVA: 0x002FC522 File Offset: 0x002FA722
		public override string GetDetails()
		{
			return "Dashes to the nearest obstacle when moving in a direction.";
		}

		// Token: 0x06008439 RID: 33849 RVA: 0x002FC52C File Offset: 0x002FA72C
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_ability_mutation_flamingRay_attack", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x0600843A RID: 33850 RVA: 0x002FC567 File Offset: 0x002FA767
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x0600843B RID: 33851 RVA: 0x002FC56C File Offset: 0x002FA76C
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 15 && num < 25)
			{
				E.RenderString = "\u001a";
				int num2 = Stat.Random(0, 2);
				if (num2 == 0)
				{
					E.ColorString = "&b";
				}
				if (num2 == 1)
				{
					E.ColorString = "&B";
				}
				if (num2 == 2)
				{
					E.ColorString = "&Y";
				}
			}
			return true;
		}
	}
}
