using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020011E0 RID: 4576
	[Serializable]
	public class Windup : IPoweredPart
	{
		// Token: 0x0600B667 RID: 46695 RVA: 0x00428F6C File Offset: 0x0042716C
		public Windup()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B668 RID: 46696 RVA: 0x00428FC8 File Offset: 0x004271C8
		public override bool SameAs(IPart p)
		{
			Windup windup = p as Windup;
			return windup.ChargeRate == this.ChargeRate && !(windup.ActionName != this.ActionName) && !(windup.ActionLabel != this.ActionLabel) && !(windup.ActionVerb != this.ActionVerb) && !(windup.ActionKey != this.ActionKey) && base.SameAs(p);
		}

		// Token: 0x0600B669 RID: 46697 RVA: 0x00429047 File Offset: 0x00427247
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetPassiveAbilityListEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600B66A RID: 46698 RVA: 0x0042906D File Offset: 0x0042726D
		public override bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			if (Stat.Random(1, this.LastWindDidAnything ? 3 : 100) == 1)
			{
				E.Add("WindUp", 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B66B RID: 46699 RVA: 0x004290A4 File Offset: 0x004272A4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.WorksFor(IComponent<GameObject>.ThePlayer))
			{
				E.AddAction(this.ActionName, this.ActionLabel, "WindUp", null, this.ActionKey[0], false, 0, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B66C RID: 46700 RVA: 0x004290F4 File Offset: 0x004272F4
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "WindUp")
			{
				if (base.IsDisabled(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show(string.Concat(new string[]
						{
							"You try to ",
							this.ActionVerb,
							" ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							", but ",
							this.ParentObject.itis,
							" unresponsive."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (E.Actor.IsVisible())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							E.Actor.Does("try", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" to ",
							this.ActionVerb,
							" ",
							this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							", but ",
							this.ParentObject.itis,
							" unresponsive."
						}), null, true);
					}
				}
				else
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show(string.Concat(new string[]
						{
							"You ",
							this.ActionVerb,
							" ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (IComponent<GameObject>.Visible(E.Actor))
					{
						IComponent<GameObject>.AddPlayerMessage(E.Actor.Does(this.ActionVerb, int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", null, true);
					}
					this.LastWindDidAnything = false;
					if (this.ParentObject.ChargeAvailable(this.ChargeRate, 0L, 1, false, null) > 0)
					{
						this.LastWindDidAnything = true;
						base.ConsumeCharge(null, null);
					}
					E.Actor.UseEnergy(1000);
					E.RequestInterfaceExit();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B66D RID: 46701 RVA: 0x004293E6 File Offset: 0x004275E6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004994 RID: 18836
		public int ChargeRate = 1;

		// Token: 0x04004995 RID: 18837
		public string ActionName = "Wind";

		// Token: 0x04004996 RID: 18838
		public string ActionLabel = "&Ww&yind";

		// Token: 0x04004997 RID: 18839
		public string ActionVerb = "wind";

		// Token: 0x04004998 RID: 18840
		public string ActionKey = "w";

		// Token: 0x04004999 RID: 18841
		public bool LastWindDidAnything;
	}
}
