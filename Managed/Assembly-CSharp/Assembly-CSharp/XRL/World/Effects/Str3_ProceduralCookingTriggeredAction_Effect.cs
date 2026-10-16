using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C5C RID: 3164
	[Serializable]
	public class Str3_ProceduralCookingTriggeredAction_Effect : BasicTriggeredCookingEffect
	{
		// Token: 0x060080CA RID: 32970 RVA: 0x002F5274 File Offset: 0x002F3474
		public Str3_ProceduralCookingTriggeredAction_Effect()
		{
			this.Duration = 1;
			this.DisplayName = null;
		}

		// Token: 0x060080CB RID: 32971 RVA: 0x002F529C File Offset: 0x002F349C
		public override void ApplyEffect(GameObject Object)
		{
			this.DisplayName = string.Concat(new string[]
			{
				"&Wwell fed (+",
				this.amount.ToString(),
				" ",
				this.stat,
				")&y"
			});
			Object.Statistics[this.stat].Bonus += this.amount;
			base.ApplyEffect(Object);
		}

		// Token: 0x060080CC RID: 32972 RVA: 0x002F5313 File Offset: 0x002F3513
		public override void RemoveEffect(GameObject Object)
		{
			Object.Statistics[this.stat].Bonus -= this.amount;
			this.amount = 0;
			base.RemoveEffect(Object);
		}

		// Token: 0x04003907 RID: 14599
		public string stat = "Strength";

		// Token: 0x04003908 RID: 14600
		public int amount = 3;
	}
}
