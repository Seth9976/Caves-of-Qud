using System;

namespace XRL.World.Parts
{
	// Token: 0x0200102C RID: 4140
	[Serializable]
	public class FlareCompensation : IPoweredPart
	{
		// Token: 0x0600A4D9 RID: 42201 RVA: 0x003C466C File Offset: 0x003C286C
		public FlareCompensation()
		{
			this.ChargeUse = 0;
			this.IsEMPSensitive = false;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A4DA RID: 42202 RVA: 0x003C4698 File Offset: 0x003C2898
		public override bool SameAs(IPart Part)
		{
			FlareCompensation flareCompensation = Part as FlareCompensation;
			return flareCompensation.Level == this.Level && flareCompensation.LevelHardened == this.LevelHardened && flareCompensation.ShowInShortDescription == this.ShowInShortDescription && flareCompensation.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(Part);
		}

		// Token: 0x0600A4DB RID: 42203 RVA: 0x003C46F3 File Offset: 0x003C28F3
		public override bool WantTurnTick()
		{
			return this.ChargeUse > 0;
		}

		// Token: 0x0600A4DC RID: 42204 RVA: 0x003C4700 File Offset: 0x003C2900
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
		}

		// Token: 0x0600A4DD RID: 42205 RVA: 0x003C4733 File Offset: 0x003C2933
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription) || ID == ImplantedEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600A4DE RID: 42206 RVA: 0x003C4774 File Offset: 0x003C2974
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				E.Postfix.AppendRules("Offers protection against visual flash effects.", base.GetEventSensitiveAddStatusSummary(E));
				if (this.ComputePowerFactor > 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
				}
				else if (this.ComputePowerFactor < 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice decreases this item's effectiveness.");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4DF RID: 42207 RVA: 0x003C47E6 File Offset: 0x003C29E6
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "ModifyFlashbang");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4E0 RID: 42208 RVA: 0x003C4800 File Offset: 0x003C2A00
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "ModifyFlashbang");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4E1 RID: 42209 RVA: 0x003C481A File Offset: 0x003C2A1A
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterPartEvent(this, "ModifyFlashbang");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4E2 RID: 42210 RVA: 0x003C4834 File Offset: 0x003C2A34
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterPartEvent(this, "ModifyFlashbang");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4E3 RID: 42211 RVA: 0x003C484E File Offset: 0x003C2A4E
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A4E4 RID: 42212 RVA: 0x003C4851 File Offset: 0x003C2A51
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ModifyFlashbang");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A4E5 RID: 42213 RVA: 0x003C4868 File Offset: 0x003C2A68
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ModifyFlashbang")
			{
				int num = GetAvailableComputePowerEvent.AdjustUp(this, this.LevelHardened, this.ComputePowerFactor);
				int num2 = GetAvailableComputePowerEvent.AdjustUp(this, this.Level, this.ComputePowerFactor);
				if (num != 0 && base.IsReady(false, true, false, false, false, false, true, false, false, false, 1, null, false, 0L, null))
				{
					E.SetParameter("Amount", Math.Max(E.GetIntParameter("Amount", 0) - num, 0));
				}
				if (num2 != 0 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					E.SetParameter("Amount", Math.Max(E.GetIntParameter("Amount", 0) - num2, 0));
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400443E RID: 17470
		public int Level = 6;

		// Token: 0x0400443F RID: 17471
		public int LevelHardened;

		// Token: 0x04004440 RID: 17472
		public bool ShowInShortDescription = true;

		// Token: 0x04004441 RID: 17473
		public float ComputePowerFactor;
	}
}
