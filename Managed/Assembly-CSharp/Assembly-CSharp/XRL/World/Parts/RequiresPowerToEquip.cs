using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020011DA RID: 4570
	[Serializable]
	public class RequiresPowerToEquip : IPoweredPart
	{
		// Token: 0x0600B632 RID: 46642 RVA: 0x00428378 File Offset: 0x00426578
		public RequiresPowerToEquip()
		{
			this.WorksOnEquipper = true;
			this.IsBootSensitive = false;
			this.ChargeUse = 0;
			this.ChargeMinimum = 1;
		}

		// Token: 0x0600B633 RID: 46643 RVA: 0x0042839C File Offset: 0x0042659C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID;
		}

		// Token: 0x0600B634 RID: 46644 RVA: 0x004283BA File Offset: 0x004265BA
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckEquip();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B635 RID: 46645 RVA: 0x004283C9 File Offset: 0x004265C9
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckEquip();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B636 RID: 46646 RVA: 0x004283D8 File Offset: 0x004265D8
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B637 RID: 46647 RVA: 0x004283DC File Offset: 0x004265DC
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			this.CheckEquip();
		}

		// Token: 0x0600B638 RID: 46648 RVA: 0x00428415 File Offset: 0x00426615
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B639 RID: 46649 RVA: 0x00428418 File Offset: 0x00426618
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginBeingEquipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B63A RID: 46650 RVA: 0x00428430 File Offset: 0x00426630
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginBeingEquipped" && base.IsDisabled(false, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null))
			{
				string text = this.ParentObject.The + this.ParentObject.DisplayNameOnly + this.ParentObject.GetVerb("do", true, false, false, true, false) + " not seem operational.";
				if (E.GetIntParameter("AutoEquipTry", 0) > 0)
				{
					E.SetParameter("FailureMessage", text);
				}
				else if (E.GetGameObjectParameter("Equipper").IsPlayer())
				{
					Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B63B RID: 46651 RVA: 0x004284FC File Offset: 0x004266FC
		public void CheckEquip()
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped != null && !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ParentObject.ForceUnequip(true);
				if (this.ParentObject.Equipped == null && equipped.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						equipped.Poss(this.ParentObject, true, null),
						this.ParentObject.GetVerb("stop", true, false, false, true, false),
						" operating; you unequip ",
						this.ParentObject.them,
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}
	}
}
