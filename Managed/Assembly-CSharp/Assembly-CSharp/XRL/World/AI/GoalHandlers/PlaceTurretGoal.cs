using System;
using System.Collections.Generic;
using Genkit;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B9B RID: 2971
	[Serializable]
	public class PlaceTurretGoal : GoalHandler
	{
		// Token: 0x06007689 RID: 30345 RVA: 0x002C8DC5 File Offset: 0x002C6FC5
		public PlaceTurretGoal(Location2D target, string turretType)
		{
			this.target = target;
			this.turretType = turretType;
		}

		// Token: 0x0600768A RID: 30346 RVA: 0x002C8DDB File Offset: 0x002C6FDB
		public override void Create()
		{
			base.Think("I'm trying to place a turret!");
		}

		// Token: 0x0600768B RID: 30347 RVA: 0x002C8DE8 File Offset: 0x002C6FE8
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x0600768C RID: 30348 RVA: 0x002C8DEB File Offset: 0x002C6FEB
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x002C8DEE File Offset: 0x002C6FEE
		public override void Push(Brain pBrain)
		{
			base.Push(pBrain);
		}

		// Token: 0x0600768E RID: 30350 RVA: 0x002C8DF8 File Offset: 0x002C6FF8
		public override void TakeAction()
		{
			if (this.target == null)
			{
				base.Think("I don't have a target!");
				base.FailToParent();
				return;
			}
			if (string.IsNullOrEmpty(this.turretType))
			{
				base.Think("I don't have a turret type to place!");
				base.FailToParent();
				return;
			}
			if (base.ParentObject.CurrentCell.Location.SquareDistance(this.target) == 1)
			{
				base.Think("I'm going to place a turret!");
				GameObject gameObject = IntegratedWeaponHosts.GenerateTurret(GameObject.Create(this.turretType), base.ParentObject, false);
				base.ParentObject.CurrentZone.GetCell(this.target).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				gameObject.MakeActive(false);
				gameObject.FireEventOnBodyparts(Event.New("GenerateIntegratedHostInitialAmmo", "Host", gameObject));
				CommandReloadEvent.Execute(gameObject, true, false, 0);
				base.ParentObject.UseEnergy(1000, "Tinker Deploy Turret", null, null, false);
				base.FailToParent();
				base.ParentObject.Brain.DidXToY("place", gameObject, null, null, null, null, base.ParentObject, null, false, false, true, false, false, null, null, null, false, true, false, false, false, null);
				TurretTinker part = base.ParentObject.GetPart<TurretTinker>();
				if (part != null)
				{
					part.MaxTurretsPlaced--;
					if (part.MaxTurretsPlaced > 0)
					{
						int num = 0;
						using (List<Cell>.Enumerator enumerator = base.ParentObject.CurrentCell.GetLocalAdjacentCells(5, false).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.HasObjectWithTagOrProperty("TinkerTurret"))
								{
									num++;
								}
							}
						}
						if (num < 3)
						{
							Cell randomElement = base.ParentObject.CurrentCell.GetEmptyAdjacentCells(1, 3).GetRandomElement<Cell>(null);
							base.ParentObject.Brain.PushGoal(new PlaceTurretGoal(randomElement.Location, part.GetTurretWeaponBlueprintInstance()));
							return;
						}
					}
				}
			}
			else if (!base.MoveTowards(base.ParentObject.CurrentCell.ParentZone.GetCell(this.target), false, true))
			{
				base.Think("I can't get to my target!");
				base.FailToParent();
			}
		}

		// Token: 0x0400364D RID: 13901
		public Location2D target;

		// Token: 0x0400364E RID: 13902
		public string turretType;
	}
}
