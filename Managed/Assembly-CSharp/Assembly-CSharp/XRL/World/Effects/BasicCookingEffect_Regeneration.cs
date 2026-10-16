using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C67 RID: 3175
	[Serializable]
	public class BasicCookingEffect_Regeneration : BasicCookingEffect
	{
		// Token: 0x06008102 RID: 33026 RVA: 0x002F5841 File Offset: 0x002F3A41
		public BasicCookingEffect_Regeneration()
		{
		}

		// Token: 0x06008103 RID: 33027 RVA: 0x002F5849 File Offset: 0x002F3A49
		public BasicCookingEffect_Regeneration(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x06008104 RID: 33028 RVA: 0x002F5858 File Offset: 0x002F3A58
		public override string GetDetails()
		{
			return "+10% to natural healing rate";
		}

		// Token: 0x06008105 RID: 33029 RVA: 0x002F585F File Offset: 0x002F3A5F
		public override void ApplyEffect(GameObject Object)
		{
			Object.RegisterEffectEvent(this, "Regenerating2");
			if (Object.IsPlayer())
			{
				Popup.Show(this.wellFedMessage + "\n\n{{W|+10% to natural healing rate for the rest of the day}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x06008106 RID: 33030 RVA: 0x002F5895 File Offset: 0x002F3A95
		public override void RemoveEffect(GameObject Object)
		{
			Object.UnregisterEffectEvent(this, "Regenerating2");
		}

		// Token: 0x06008107 RID: 33031 RVA: 0x002F58A4 File Offset: 0x002F3AA4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Regenerating2")
			{
				E.SetParameter("Amount", (int)Math.Round((double)E.GetIntParameter("Amount", 0) * 1.1, MidpointRounding.AwayFromZero));
			}
			return base.FireEvent(E);
		}
	}
}
