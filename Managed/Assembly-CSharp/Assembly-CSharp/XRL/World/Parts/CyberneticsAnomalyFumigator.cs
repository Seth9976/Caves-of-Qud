using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F17 RID: 3863
	[Serializable]
	public class CyberneticsAnomalyFumigator : IPart
	{
		// Token: 0x060099EE RID: 39406 RVA: 0x0037E53F File Offset: 0x0037C73F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060099EF RID: 39407 RVA: 0x0037E542 File Offset: 0x0037C742
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x060099F0 RID: 39408 RVA: 0x0037E570 File Offset: 0x0037C770
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Anomaly Fumigator", CyberneticsAnomalyFumigator.COMMAND_NAME, "Cybernetics", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x060099F1 RID: 39409 RVA: 0x0037E5BD File Offset: 0x0037C7BD
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x060099F2 RID: 39410 RVA: 0x0037E5D8 File Offset: 0x0037C7D8
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsAnomalyFumigator.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				this.ParentObject.Implantee.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099F3 RID: 39411 RVA: 0x0037E645 File Offset: 0x0037C845
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x060099F4 RID: 39412 RVA: 0x0037E65F File Offset: 0x0037C85F
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060099F5 RID: 39413 RVA: 0x0037E664 File Offset: 0x0037C864
		public override void TurnTick(long TimeTick, int Amount)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && !implantee.OnWorldMap() && implantee.IsActivatedAbilityToggledOn(this.ActivatedAbilityID))
			{
				Cell currentCell = implantee.CurrentCell;
				if (currentCell != null)
				{
					List<Cell> localEmptyAdjacentCells = currentCell.GetLocalEmptyAdjacentCells();
					int num = Stat.Random(0, 2);
					while (num > 0 && localEmptyAdjacentCells.Count > 0)
					{
						GameObject gameObject = GameObject.Create("NormalityGas");
						Gas part = gameObject.GetPart<Gas>();
						int num2 = GetAvailableComputePowerEvent.AdjustUp(implantee, Stat.Random(10, 120));
						part.Density = num2;
						Cell randomElement = localEmptyAdjacentCells.GetRandomElement<Cell>(null);
						randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						localEmptyAdjacentCells.Remove(randomElement);
						GameObject parentObject = this.ParentObject;
						if (parentObject != null)
						{
							parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_gasMutation_passiveRelease", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
						num--;
					}
				}
			}
		}

		// Token: 0x060099F6 RID: 39414 RVA: 0x0037E75F File Offset: 0x0037C95F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004029 RID: 16425
		public static readonly string COMMAND_NAME = "CommandToggleAnomalyFumigator";

		// Token: 0x0400402A RID: 16426
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
