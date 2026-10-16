using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C6E RID: 3182
	[Serializable]
	public class BasicCookingEffect_Quickness : BasicCookingEffect
	{
		// Token: 0x06008128 RID: 33064 RVA: 0x002F5DE6 File Offset: 0x002F3FE6
		public BasicCookingEffect_Quickness()
		{
		}

		// Token: 0x06008129 RID: 33065 RVA: 0x002F5DEE File Offset: 0x002F3FEE
		public BasicCookingEffect_Quickness(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x0600812A RID: 33066 RVA: 0x002F5DFD File Offset: 0x002F3FFD
		public override string GetDetails()
		{
			if (this.amount == 0)
			{
				return "+3% Quickness";
			}
			return this.amount.Signed(false) + " Quickness";
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x002F5E24 File Offset: 0x002F4024
		public override void ApplyEffect(GameObject Object)
		{
			this.amount = (int)Math.Max(Math.Round((double)Object.BaseStat("Speed", 0) * 0.03, MidpointRounding.AwayFromZero), 1.0);
			base.StatShifter.SetStatShift("Speed", this.amount, false);
			if (Object.IsPlayer())
			{
				Popup.Show(this.wellFedMessage + "\n\n{{W|+" + this.amount.ToString() + " quickness for the rest of the day}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x002F5EB3 File Offset: 0x002F40B3
		public override void RemoveEffect(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			this.amount = 0;
		}

		// Token: 0x04003915 RID: 14613
		public string stat;

		// Token: 0x04003916 RID: 14614
		public int amount;
	}
}
