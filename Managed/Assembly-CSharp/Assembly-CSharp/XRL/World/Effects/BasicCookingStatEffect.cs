using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C70 RID: 3184
	[Serializable]
	public class BasicCookingStatEffect : BasicCookingEffect
	{
		// Token: 0x06008131 RID: 33073 RVA: 0x002F5F50 File Offset: 0x002F4150
		public BasicCookingStatEffect(string stat, string amount, int duration)
		{
			this.Duration = duration;
			this.DisplayName = null;
			this.stat = stat;
			this.amount = Stat.Roll(amount, null);
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x002F5F7A File Offset: 0x002F417A
		public override void ApplyEffect(GameObject Object)
		{
			Object.Statistics[this.stat].Bonus += this.amount;
			base.ApplyEffect(Object);
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x002F5FA6 File Offset: 0x002F41A6
		public override void RemoveEffect(GameObject Object)
		{
			Object.Statistics[this.stat].Bonus -= this.amount;
			this.amount = 0;
			base.RemoveEffect(Object);
		}

		// Token: 0x04003919 RID: 14617
		public string stat;

		// Token: 0x0400391A RID: 14618
		public int amount;
	}
}
