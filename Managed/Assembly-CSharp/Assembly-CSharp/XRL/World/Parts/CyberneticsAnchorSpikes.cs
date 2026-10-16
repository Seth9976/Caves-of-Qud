using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F16 RID: 3862
	[Serializable]
	public class CyberneticsAnchorSpikes : IPart
	{
		// Token: 0x060099E3 RID: 39395 RVA: 0x0037E346 File Offset: 0x0037C546
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060099E4 RID: 39396 RVA: 0x0037E349 File Offset: 0x0037C549
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == ImplantedEvent.ID || ID == PooledEvent<IsRootedInPlaceEvent>.ID || ID == ModifyDefendingSaveEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x060099E5 RID: 39397 RVA: 0x0037E388 File Offset: 0x0037C588
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Anchor Spikes", CyberneticsAnchorSpikes.COMMAND_NAME, "Cybernetics", null, "\u00b8", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x060099E6 RID: 39398 RVA: 0x0037E3D5 File Offset: 0x0037C5D5
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x060099E7 RID: 39399 RVA: 0x0037E3F0 File Offset: 0x0037C5F0
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsAnchorSpikes.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				this.ParentObject.Implantee.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099E8 RID: 39400 RVA: 0x0037E45D File Offset: 0x0037C65D
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return (E.Object != this.ParentObject.Implantee || !this.ParentObject.Implantee.IsActivatedAbilityToggledOn(this.ActivatedAbilityID)) && base.HandleEvent(E);
		}

		// Token: 0x060099E9 RID: 39401 RVA: 0x0037E494 File Offset: 0x0037C694
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Move", E) && this.ParentObject.Implantee.IsActivatedAbilityToggledOn(this.ActivatedAbilityID))
			{
				E.Roll += E.Difficulty;
				E.IgnoreNatural1 = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099EA RID: 39402 RVA: 0x0037E4E7 File Offset: 0x0037C6E7
		public override bool HandleEvent(IsRootedInPlaceEvent E)
		{
			return (E.Object != this.ParentObject.Implantee || !this.ParentObject.Implantee.IsActivatedAbilityToggledOn(this.ActivatedAbilityID)) && base.HandleEvent(E);
		}

		// Token: 0x060099EB RID: 39403 RVA: 0x0037E51D File Offset: 0x0037C71D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004027 RID: 16423
		public static readonly string COMMAND_NAME = "CommandToggleAnchorSpikes";

		// Token: 0x04004028 RID: 16424
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
