using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C6D RID: 3181
	[Serializable]
	public class BasicCookingEffect_MS : BasicCookingEffect
	{
		// Token: 0x06008123 RID: 33059 RVA: 0x002F5CFC File Offset: 0x002F3EFC
		public BasicCookingEffect_MS()
		{
		}

		// Token: 0x06008124 RID: 33060 RVA: 0x002F5D04 File Offset: 0x002F3F04
		public BasicCookingEffect_MS(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x06008125 RID: 33061 RVA: 0x002F5D13 File Offset: 0x002F3F13
		public override string GetDetails()
		{
			if (this.amount == 0)
			{
				return "+6% Move Speed";
			}
			return this.amount.Signed(false) + " Move Speed";
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x002F5D3C File Offset: 0x002F3F3C
		public override void ApplyEffect(GameObject Object)
		{
			this.amount = (int)Math.Max(Math.Round((double)(200 - Object.BaseStat("MoveSpeed", 0)) * 0.06, MidpointRounding.AwayFromZero), 1.0);
			base.StatShifter.SetStatShift("MoveSpeed", -this.amount, false);
			if (Object.IsPlayer())
			{
				Popup.Show(this.wellFedMessage + "\n\n{{W|" + this.amount.ToString() + " move speed for the rest of the day}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x002F5DD2 File Offset: 0x002F3FD2
		public override void RemoveEffect(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			this.amount = 0;
		}

		// Token: 0x04003913 RID: 14611
		public string stat;

		// Token: 0x04003914 RID: 14612
		public int amount;
	}
}
