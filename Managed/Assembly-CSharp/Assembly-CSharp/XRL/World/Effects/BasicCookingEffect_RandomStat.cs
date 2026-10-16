using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C6A RID: 3178
	[Serializable]
	public class BasicCookingEffect_RandomStat : BasicCookingEffect
	{
		// Token: 0x06008114 RID: 33044 RVA: 0x002F5A0D File Offset: 0x002F3C0D
		public BasicCookingEffect_RandomStat()
		{
			this.DisplayName = "{{W|well fed}}";
		}

		// Token: 0x06008115 RID: 33045 RVA: 0x002F5A20 File Offset: 0x002F3C20
		public BasicCookingEffect_RandomStat(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x06008116 RID: 33046 RVA: 0x002F5A2F File Offset: 0x002F3C2F
		public override string GetDetails()
		{
			return this.amount.Signed(false) + " " + this.stat;
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x002F5A50 File Offset: 0x002F3C50
		public override void ApplyEffect(GameObject Object)
		{
			this.stat = new List<string> { "Strength", "Intelligence", "Willpower", "Agility", "Toughness", "Ego" }.GetRandomElement<string>(null);
			this.amount = 1;
			Object.Statistics[this.stat].Bonus += this.amount;
			if (Object.IsPlayer())
			{
				Popup.Show(string.Concat(new string[]
				{
					this.wellFedMessage,
					"\n\n{{W|",
					this.amount.ToString(),
					" ",
					this.stat,
					" for the rest of the day}}"
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x06008118 RID: 33048 RVA: 0x002F5B35 File Offset: 0x002F3D35
		public override void RemoveEffect(GameObject Object)
		{
			Object.Statistics[this.stat].Bonus -= this.amount;
			this.amount = 0;
		}

		// Token: 0x0400390D RID: 14605
		public string stat;

		// Token: 0x0400390E RID: 14606
		public int amount;
	}
}
