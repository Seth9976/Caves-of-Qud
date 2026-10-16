using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C6B RID: 3179
	[Serializable]
	public class BasicCookingEffect_Hitpoints : BasicCookingEffect
	{
		// Token: 0x06008119 RID: 33049 RVA: 0x002F5B61 File Offset: 0x002F3D61
		public BasicCookingEffect_Hitpoints()
		{
		}

		// Token: 0x0600811A RID: 33050 RVA: 0x002F5B69 File Offset: 0x002F3D69
		public BasicCookingEffect_Hitpoints(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x0600811B RID: 33051 RVA: 0x002F5B78 File Offset: 0x002F3D78
		public override string GetDetails()
		{
			if (this.amount == 0)
			{
				return "+10% hit points";
			}
			return this.amount.Signed(false) + " hit points";
		}

		// Token: 0x0600811C RID: 33052 RVA: 0x002F5BA0 File Offset: 0x002F3DA0
		public override void ApplyEffect(GameObject Object)
		{
			this.amount = (int)Math.Max(Math.Round((double)Object.BaseStat("Hitpoints", 0) * 0.1, MidpointRounding.AwayFromZero), 1.0);
			base.StatShifter.SetStatShift("Hitpoints", this.amount, false);
			if (Object.IsPlayer())
			{
				Popup.Show(string.Concat(new string[]
				{
					this.wellFedMessage,
					"\n\n{{W|+",
					this.amount.ToString(),
					" hit ",
					(this.amount == 1) ? "point" : "points",
					" for the rest of the day}}"
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600811D RID: 33053 RVA: 0x002F5C61 File Offset: 0x002F3E61
		public override void RemoveEffect(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			this.amount = 0;
		}

		// Token: 0x0400390F RID: 14607
		public string stat;

		// Token: 0x04003910 RID: 14608
		public int amount;
	}
}
