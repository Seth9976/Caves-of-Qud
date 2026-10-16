using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D4A RID: 3402
	[Serializable]
	public class HulkHoney_Tonic_Allergy : Effect
	{
		// Token: 0x06008672 RID: 34418 RVA: 0x00305C34 File Offset: 0x00303E34
		public HulkHoney_Tonic_Allergy()
		{
			this.DisplayName = "{{R|enraged}}";
		}

		// Token: 0x06008673 RID: 34419 RVA: 0x00305C47 File Offset: 0x00303E47
		public HulkHoney_Tonic_Allergy(int duration)
			: this()
		{
			this.Duration = duration;
		}

		// Token: 0x06008674 RID: 34420 RVA: 0x00305C56 File Offset: 0x00303E56
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06008675 RID: 34421 RVA: 0x00305C5D File Offset: 0x00303E5D
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008676 RID: 34422 RVA: 0x00305C60 File Offset: 0x00303E60
		public override string GetDescription()
		{
			return "{{R|enraged}}";
		}

		// Token: 0x06008677 RID: 34423 RVA: 0x00305C67 File Offset: 0x00303E67
		public override string GetDetails()
		{
			return "ARRRRRRHGHH";
		}

		// Token: 0x06008678 RID: 34424 RVA: 0x00305C70 File Offset: 0x00303E70
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_enraged", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			HulkHoney_Tonic_Allergy effect = Object.GetEffect<HulkHoney_Tonic_Allergy>();
			if (effect != null)
			{
				if (effect.Duration < this.Duration)
				{
					effect.Duration = this.Duration;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06008679 RID: 34425 RVA: 0x00305CD1 File Offset: 0x00303ED1
		public override void Remove(GameObject Object)
		{
		}
	}
}
