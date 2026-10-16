using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001080 RID: 4224
	[Serializable]
	public class PoweredFloating : IPoweredPart
	{
		// Token: 0x0600A816 RID: 43030 RVA: 0x003D7896 File Offset: 0x003D5A96
		public PoweredFloating()
		{
			this.WorksOnEquipper = true;
			this.IsBootSensitive = false;
			this.NameForStatus = "HoverSystems";
		}

		// Token: 0x0600A817 RID: 43031 RVA: 0x003D78BE File Offset: 0x003D5ABE
		public override bool SameAs(IPart p)
		{
			return (p as PoweredFloating).FallIntoInventory == this.FallIntoInventory && base.SameAs(p);
		}

		// Token: 0x0600A818 RID: 43032 RVA: 0x003D78DC File Offset: 0x003D5ADC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID;
		}

		// Token: 0x0600A819 RID: 43033 RVA: 0x003D78FA File Offset: 0x003D5AFA
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckFloating();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A81A RID: 43034 RVA: 0x003D7909 File Offset: 0x003D5B09
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckFloating();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A81B RID: 43035 RVA: 0x003D7918 File Offset: 0x003D5B18
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A81C RID: 43036 RVA: 0x003D791C File Offset: 0x003D5B1C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
				this.CheckFloating();
			}
		}

		// Token: 0x0600A81D RID: 43037 RVA: 0x003D795D File Offset: 0x003D5B5D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A81E RID: 43038 RVA: 0x003D7960 File Offset: 0x003D5B60
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginBeingEquipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A81F RID: 43039 RVA: 0x003D7978 File Offset: 0x003D5B78
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginBeingEquipped" && base.IsDisabled(false, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null))
			{
				string text = this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not seem to be able to float at this time.";
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

		// Token: 0x0600A820 RID: 43040 RVA: 0x003D7A48 File Offset: 0x003D5C48
		public void CheckFloating()
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped != null && !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.FallIntoInventory || equipped.OnWorldMap())
				{
					this.ParentObject.ForceUnequip(true);
					if (this.ParentObject.Equipped == null && equipped.IsPlayer())
					{
						if (this.FallIntoInventory)
						{
							Popup.Show(this.ParentObject.Does("cease", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " floating near you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							return;
						}
						Popup.Show(this.ParentObject.Does("fall", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to the ground; you scoop " + this.ParentObject.them + " up.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return;
					}
				}
				else if (equipped.CurrentCell != null)
				{
					this.ParentObject.ForceUnequip(true);
					if (this.ParentObject.Equipped == null)
					{
						if (equipped.IsPlayer())
						{
							Popup.Show(this.ParentObject.Does("fall", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to the ground.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						InventoryActionEvent.Check(equipped, equipped, this.ParentObject, "CommandDropObject", false, false, false, true, true, 0, 0, 0, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x04004562 RID: 17762
		public bool FallIntoInventory = true;
	}
}
