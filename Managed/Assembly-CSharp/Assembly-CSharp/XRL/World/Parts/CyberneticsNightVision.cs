using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F37 RID: 3895
	[Serializable]
	public class CyberneticsNightVision : IPart
	{
		// Token: 0x06009B51 RID: 39761 RVA: 0x00385AF8 File Offset: 0x00383CF8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B52 RID: 39762 RVA: 0x00385AFB File Offset: 0x00383CFB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009B53 RID: 39763 RVA: 0x00385B2C File Offset: 0x00383D2C
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && implantee.IsPlayer())
			{
				ActivatedAbilityEntry activatedAbility = implantee.GetActivatedAbility(this.ActivatedAbilityID);
				if (activatedAbility != null && activatedAbility.IsUsable && activatedAbility.ToggleState && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed())
				{
					Cell currentCell = implantee.CurrentCell;
					if (currentCell != null)
					{
						Zone parentZone = currentCell.ParentZone;
						if (parentZone != null)
						{
							parentZone.AddLight(currentCell.X, currentCell.Y, this.Radius, LightLevel.Darkvision, false);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B54 RID: 39764 RVA: 0x00385BC0 File Offset: 0x00383DC0
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Night Vision", CyberneticsNightVision.COMMAND_NAME, "Cybernetics", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B55 RID: 39765 RVA: 0x00385C0D File Offset: 0x00383E0D
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009B56 RID: 39766 RVA: 0x00385C28 File Offset: 0x00383E28
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsNightVision.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				this.ParentObject.Implantee.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B57 RID: 39767 RVA: 0x00385C95 File Offset: 0x00383E95
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004073 RID: 16499
		public static readonly string COMMAND_NAME = "CommandToggleCyberNightVision";

		// Token: 0x04004074 RID: 16500
		public int Radius = 40;

		// Token: 0x04004075 RID: 16501
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
