using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C6C RID: 3180
	[Serializable]
	public class BasicCookingEffect_MA : BasicCookingEffect
	{
		// Token: 0x0600811E RID: 33054 RVA: 0x002F5C75 File Offset: 0x002F3E75
		public BasicCookingEffect_MA()
		{
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x002F5C7D File Offset: 0x002F3E7D
		public BasicCookingEffect_MA(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x06008120 RID: 33056 RVA: 0x002F5C8C File Offset: 0x002F3E8C
		public override string GetDetails()
		{
			return "+1 MA";
		}

		// Token: 0x06008121 RID: 33057 RVA: 0x002F5C94 File Offset: 0x002F3E94
		public override void ApplyEffect(GameObject Object)
		{
			this.amount = 1;
			base.StatShifter.SetStatShift("MA", this.amount, false);
			if (Object.IsPlayer())
			{
				Popup.Show(this.wellFedMessage + "\n\n{{W|+1 MA for the rest of the day}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x06008122 RID: 33058 RVA: 0x002F5CE8 File Offset: 0x002F3EE8
		public override void RemoveEffect(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			this.amount = 0;
		}

		// Token: 0x04003911 RID: 14609
		public string stat;

		// Token: 0x04003912 RID: 14610
		public int amount;
	}
}
