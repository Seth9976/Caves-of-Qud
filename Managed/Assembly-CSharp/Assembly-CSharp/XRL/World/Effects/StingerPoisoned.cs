using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D93 RID: 3475
	[Serializable]
	public class StingerPoisoned : Poisoned
	{
		// Token: 0x06008A70 RID: 35440 RVA: 0x00315AFE File Offset: 0x00313CFE
		public StingerPoisoned()
		{
		}

		// Token: 0x06008A71 RID: 35441 RVA: 0x00315B06 File Offset: 0x00313D06
		public StingerPoisoned(int Duration, string DamageIncrement, int Level, GameObject Owner = null)
			: base(Duration, DamageIncrement, Level, Owner, false, false)
		{
		}

		// Token: 0x06008A72 RID: 35442 RVA: 0x00315B18 File Offset: 0x00313D18
		public override bool Apply(GameObject Object)
		{
			StingerPoisoned stingerPoisoned;
			if (Object.TryGetEffect<StingerPoisoned>(out stingerPoisoned))
			{
				stingerPoisoned.Duration = (stingerPoisoned.Duration + this.Duration) / 2;
				stingerPoisoned.DamageIncrement = (Convert.ToInt32(stingerPoisoned.DamageIncrement.Split('d', StringSplitOptions.None)[0]) + Convert.ToInt32(this.DamageIncrement.Split('d', StringSplitOptions.None)[0])).ToString() + "d2";
				stingerPoisoned.Level = (stingerPoisoned.Level + this.Level) / 2;
				return false;
			}
			if (Object.FireEvent("ApplyPoison") && ApplyEffectEvent.Check(Object, "Poison", this, null))
			{
				base.DidX("have", "been poisoned", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
				return true;
			}
			return false;
		}
	}
}
