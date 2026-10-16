using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C6F RID: 3183
	[Serializable]
	public class BasicTriggeredCookingStatEffect : BasicTriggeredCookingEffect
	{
		// Token: 0x0600812D RID: 33069 RVA: 0x002F5EC7 File Offset: 0x002F40C7
		public override string GetDetails()
		{
			return this.amount.Signed(false) + " " + Statistic.GetStatDisplayName(this.stat);
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x002F5EEA File Offset: 0x002F40EA
		public BasicTriggeredCookingStatEffect(string stat, string amount, int duration)
		{
			this.Duration = duration;
			this.DisplayName = null;
			this.stat = stat;
			this.amount = amount.RollCached();
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x002F5F13 File Offset: 0x002F4113
		public override void ApplyEffect(GameObject Object)
		{
			base.StatShifter.SetStatShift(this.stat, this.amount, false);
			base.ApplyEffect(Object);
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x002F5F35 File Offset: 0x002F4135
		public override void RemoveEffect(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			this.amount = 0;
			base.RemoveEffect(Object);
		}

		// Token: 0x04003917 RID: 14615
		public string stat;

		// Token: 0x04003918 RID: 14616
		public int amount;
	}
}
