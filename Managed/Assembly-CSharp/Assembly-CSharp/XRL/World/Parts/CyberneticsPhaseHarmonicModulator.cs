using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F3F RID: 3903
	[Serializable]
	public class CyberneticsPhaseHarmonicModulator : IPart
	{
		// Token: 0x06009BB1 RID: 39857 RVA: 0x003879E1 File Offset: 0x00385BE1
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009BB2 RID: 39858 RVA: 0x003879E4 File Offset: 0x00385BE4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009BB3 RID: 39859 RVA: 0x003879E7 File Offset: 0x00385BE7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009BB4 RID: 39860 RVA: 0x00387A10 File Offset: 0x00385C10
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Phase Harmonic Modulator", CyberneticsPhaseHarmonicModulator.COMMAND_NAME, "Cybernetics", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			E.Implantee.ForceApplyEffect(new Omniphase(base.Name), null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BB5 RID: 39861 RVA: 0x00387A75 File Offset: 0x00385C75
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveEffect(typeof(Omniphase), new Predicate<Effect>(this.OurEffect));
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009BB6 RID: 39862 RVA: 0x00387AB4 File Offset: 0x00385CB4
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsPhaseHarmonicModulator.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				E.Actor.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
				if (E.Actor.IsActivatedAbilityToggledOn(this.ActivatedAbilityID))
				{
					if (!E.Actor.HasEffect(typeof(Omniphase), new Predicate<Effect>(this.OurEffect)))
					{
						E.Actor.ForceApplyEffect(new Omniphase(base.Name), null);
					}
				}
				else
				{
					E.Actor.RemoveEffect(typeof(Omniphase), new Predicate<Effect>(this.OurEffect));
				}
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009BB7 RID: 39863 RVA: 0x00387B94 File Offset: 0x00385D94
		private bool OurEffect(Effect FX)
		{
			Omniphase omniphase = FX as Omniphase;
			return omniphase != null && omniphase.SourceKey == base.Name;
		}

		// Token: 0x04004086 RID: 16518
		public static readonly string COMMAND_NAME = "CommandTogglePhaseHarmonicModulator";

		// Token: 0x04004087 RID: 16519
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
