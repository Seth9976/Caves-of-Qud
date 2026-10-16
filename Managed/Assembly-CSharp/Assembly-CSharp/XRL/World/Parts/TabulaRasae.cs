using System;
using System.Collections.Generic;
using XRL.Messages;

namespace XRL.World.Parts
{
	// Token: 0x02000E92 RID: 3730
	[Serializable]
	public class TabulaRasae : IPart
	{
		// Token: 0x06009629 RID: 38441 RVA: 0x003663E5 File Offset: 0x003645E5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x0600962A RID: 38442 RVA: 0x00366404 File Offset: 0x00364604
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && E.Damage.HasAnyAttribute(this.GetDamageImmunities()))
			{
				if (E.Actor != null && E.Actor.IsPlayer())
				{
					MessageQueue.AddPlayerMessage("Your attack does not affect " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
				}
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600962B RID: 38443 RVA: 0x003664C0 File Offset: 0x003646C0
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == this.ParentObject && this.ParentObject.hitpoints <= 0)
			{
				List<string> damageImmunities = this.GetDamageImmunities();
				if (E.Damage != null)
				{
					foreach (string text in E.Damage.Attributes)
					{
						if (!damageImmunities.Contains(text))
						{
							damageImmunities.Add(text);
							MessageQueue.AddPlayerMessage("The Tabula Rasae adapt to " + text.ToLower() + " damage.", null, true);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600962C RID: 38444 RVA: 0x00366570 File Offset: 0x00364770
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600962D RID: 38445 RVA: 0x00366573 File Offset: 0x00364773
		public List<string> GetDamageImmunities()
		{
			if (!The.Game.HasGameState("TabulaRasaeDamageImmunities"))
			{
				The.Game.SetObjectGameState("TabulaRasaeDamageImmunities", new List<string>());
			}
			return The.Game.GetObjectGameState("TabulaRasaeDamageImmunities") as List<string>;
		}

		// Token: 0x04003E2E RID: 15918
		public static readonly string[] EXEMPT = new string[] { "AffectGas", "Unavoidable", "NonPenetrating" };
	}
}
