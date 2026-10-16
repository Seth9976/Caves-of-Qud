using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C68 RID: 3176
	public class BasicCookingEffect_XP : BasicCookingEffect
	{
		// Token: 0x06008108 RID: 33032 RVA: 0x002F58F4 File Offset: 0x002F3AF4
		public BasicCookingEffect_XP()
		{
		}

		// Token: 0x06008109 RID: 33033 RVA: 0x002F58FC File Offset: 0x002F3AFC
		public BasicCookingEffect_XP(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x0600810A RID: 33034 RVA: 0x002F590B File Offset: 0x002F3B0B
		public override string GetDetails()
		{
			return "+5% XP gained";
		}

		// Token: 0x0600810B RID: 33035 RVA: 0x002F5912 File Offset: 0x002F3B12
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AwardingXPEvent.ID;
		}

		// Token: 0x0600810C RID: 33036 RVA: 0x002F5928 File Offset: 0x002F3B28
		public override bool HandleEvent(AwardingXPEvent E)
		{
			E.Amount += E.Amount / 20;
			return base.HandleEvent(E);
		}

		// Token: 0x0600810D RID: 33037 RVA: 0x002F5947 File Offset: 0x002F3B47
		public override void ApplyEffect(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show(this.wellFedMessage + "\n\n{{W|+5% XP gained for the rest of the day}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}
	}
}
