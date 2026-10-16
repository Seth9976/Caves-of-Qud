using System;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C69 RID: 3177
	public class BasicCookingEffect_ToHit : BasicCookingEffect
	{
		// Token: 0x0600810E RID: 33038 RVA: 0x002F5971 File Offset: 0x002F3B71
		public BasicCookingEffect_ToHit()
		{
		}

		// Token: 0x0600810F RID: 33039 RVA: 0x002F5979 File Offset: 0x002F3B79
		public BasicCookingEffect_ToHit(string tastyMessage)
			: this()
		{
			this.wellFedMessage = tastyMessage;
		}

		// Token: 0x06008110 RID: 33040 RVA: 0x002F5988 File Offset: 0x002F3B88
		public override string GetDetails()
		{
			return "+1 to hit";
		}

		// Token: 0x06008111 RID: 33041 RVA: 0x002F598F File Offset: 0x002F3B8F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x06008112 RID: 33042 RVA: 0x002F59A5 File Offset: 0x002F3BA5
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == base.Object && E.Checking == "Actor")
			{
				E.Modifier++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008113 RID: 33043 RVA: 0x002F59DC File Offset: 0x002F3BDC
		public override void ApplyEffect(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show(this.wellFedMessage + "\n\n{{W|+1 to hit for the rest of the day}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			base.ApplyEffect(Object);
		}
	}
}
