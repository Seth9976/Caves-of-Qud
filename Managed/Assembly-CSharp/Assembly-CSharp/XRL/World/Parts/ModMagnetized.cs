using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200114E RID: 4430
	[Serializable]
	public class ModMagnetized : IModification
	{
		// Token: 0x0600AFE4 RID: 45028 RVA: 0x0040D085 File Offset: 0x0040B285
		public ModMagnetized()
		{
		}

		// Token: 0x0600AFE5 RID: 45029 RVA: 0x0040D08D File Offset: 0x0040B28D
		public ModMagnetized(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AFE6 RID: 45030 RVA: 0x0040D096 File Offset: 0x0040B296
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "MagLev";
			this.IsEMPSensitive = true;
			base.IsTechScannable = true;
		}

		// Token: 0x0600AFE7 RID: 45031 RVA: 0x0040D0B8 File Offset: 0x0040B2B8
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<Metal>() && Object.WeightEach <= 100 && !Object.HasPart<ModGesticulating>() && !Object.HasPart<IntegratedPowerSystems>() && Object.CanBeUnequipped(null, null, false, false);
		}

		// Token: 0x0600AFE8 RID: 45032 RVA: 0x0040D0F4 File Offset: 0x0040B2F4
		public override void ApplyModification(GameObject Object)
		{
			if (Object.Equipped != null)
			{
				EquipmentAPI.UnequipObject(this.ParentObject);
			}
			Armor armor = Object.GetPart<Armor>();
			Shield part = Object.GetPart<Shield>();
			MissileWeapon part2 = Object.GetPart<MissileWeapon>();
			if (armor == null && part == null && part2 == null)
			{
				armor = Object.AddPart<Armor>(true, false);
				MeleeWeapon part3 = Object.GetPart<MeleeWeapon>();
				if (part3 != null)
				{
					part3.Slot = "Floating Nearby";
				}
			}
			if (armor != null)
			{
				armor.WornOn = "Floating Nearby";
			}
			if (part != null)
			{
				part.WornOn = "Floating Nearby";
			}
			if (part2 != null)
			{
				part2.SlotType = "Floating Nearby";
			}
			Object.UsesTwoSlots = false;
			if (!string.IsNullOrEmpty(Object.UsesSlots))
			{
				Object.UsesSlots = "";
			}
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600AFE9 RID: 45033 RVA: 0x0040D1A3 File Offset: 0x0040B3A3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeMagneticallyManipulatedEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AFEA RID: 45034 RVA: 0x0040D1D9 File Offset: 0x0040B3D9
		public override bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			E.Allow = true;
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFEB RID: 45035 RVA: 0x0040D1E9 File Offset: 0x0040B3E9
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("magnetized", -5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFEC RID: 45036 RVA: 0x0040D214 File Offset: 0x0040B414
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModMagnetized.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFED RID: 45037 RVA: 0x0040D234 File Offset: 0x0040B434
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckFloating();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFEE RID: 45038 RVA: 0x0040D243 File Offset: 0x0040B443
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckFloating();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFEF RID: 45039 RVA: 0x0040D252 File Offset: 0x0040B452
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AFF0 RID: 45040 RVA: 0x0040D258 File Offset: 0x0040B458
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			this.CheckFloating();
		}

		// Token: 0x0600AFF1 RID: 45041 RVA: 0x0040D291 File Offset: 0x0040B491
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AFF2 RID: 45042 RVA: 0x0040D294 File Offset: 0x0040B494
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginBeingEquipped");
			Registrar.Register("EncumbranceChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AFF3 RID: 45043 RVA: 0x0040D2B4 File Offset: 0x0040B4B4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginBeingEquipped")
			{
				if (base.IsDisabled(false, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null))
				{
					string text = this.ParentObject.The + this.ParentObject.ShortDisplayName + this.ParentObject.GetVerb("are", true, false, false, true, false) + " not able to float right now.";
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
			}
			else if (E.ID == "EncumbranceChanged")
			{
				this.CheckFloating();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AFF4 RID: 45044 RVA: 0x0040D399 File Offset: 0x0040B599
		public static string GetDescription(int Tier)
		{
			return "Magnetized: This item floats around you.";
		}

		// Token: 0x0600AFF5 RID: 45045 RVA: 0x0040D3A0 File Offset: 0x0040B5A0
		public void CheckFloating()
		{
			GameObject equipped = this.ParentObject.Equipped;
			if (equipped != null && (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) || this.ParentObject.GetCarriedWeight() > 25))
			{
				if (equipped.OnWorldMap())
				{
					this.ParentObject.ForceUnequip(true);
					if (this.ParentObject.Equipped == null && equipped.IsPlayer())
					{
						Popup.Show(this.ParentObject.Does("fall", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to the ground; you pick " + this.ParentObject.them + " up.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
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

		// Token: 0x04004808 RID: 18440
		public const int MAX_CARRIED_WEIGHT = 25;
	}
}
