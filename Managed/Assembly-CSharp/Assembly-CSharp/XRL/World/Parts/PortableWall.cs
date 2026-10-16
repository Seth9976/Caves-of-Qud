using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200107F RID: 4223
	[Serializable]
	public class PortableWall : IPart
	{
		// Token: 0x0600A80D RID: 43021 RVA: 0x003D7254 File Offset: 0x003D5454
		public override bool SameAs(IPart p)
		{
			PortableWall portableWall = p as PortableWall;
			return !(portableWall.Blueprint != this.Blueprint) && portableWall.Size == this.Size && !(portableWall.Message != this.Message) && !(portableWall.ExamineFailureMessage != this.ExamineFailureMessage) && !(portableWall.DeployingWhat != this.DeployingWhat) && base.SameAs(p);
		}

		// Token: 0x0600A80E RID: 43022 RVA: 0x003D72D3 File Offset: 0x003D54D3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A80F RID: 43023 RVA: 0x003D7304 File Offset: 0x003D5504
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "activate", "ActivatePortableWall", null, 'a', false, 10, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A810 RID: 43024 RVA: 0x003D733C File Offset: 0x003D553C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivatePortableWall" && E.Actor != null)
			{
				if (E.Actor.OnWorldMap())
				{
					E.Actor.ShowFailure("You cannot do that on the world map.");
					return base.HandleEvent(E);
				}
				List<Cell> list = null;
				this.ParentObject.SplitFromStack();
				if (E.Actor.IsPlayer())
				{
					list = base.PickFieldAdjacent(this.Size, E.Actor, this.DeployingWhat, false, false);
				}
				else
				{
					List<GameObject> list2 = E.Actor.CurrentCell.ParentZone.FastSquareVisibility(E.Actor.CurrentCell.X, E.Actor.CurrentCell.Y, this.Size, "ForceWallTarget", E.Actor, false, false, null);
					if (list2.Count > 0)
					{
						list = new List<Cell>(list2.Count);
						foreach (GameObject gameObject in list2)
						{
							list.Add(gameObject.CurrentCell);
							if (list.Count >= this.Size)
							{
								break;
							}
						}
					}
				}
				if (list != null && list.Count > 0 && !list[0].OnWorldMap())
				{
					list[0].PlayWorldSound(this.Sound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					this.DeployToCells(list, E.Actor);
					if (E.Actor != null && E.Actor.IsPlayer())
					{
						Popup.Show(GameText.VariableReplace(this.Message, E.Actor, this.ParentObject, true), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					this.ParentObject.Destroy(null, false, false, null);
				}
				else
				{
					this.ParentObject.CheckStack();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A811 RID: 43025 RVA: 0x003D7544 File Offset: 0x003D5744
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A812 RID: 43026 RVA: 0x003D755A File Offset: 0x003D575A
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A813 RID: 43027 RVA: 0x003D7570 File Offset: 0x003D5770
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100())
			{
				Cell startCell = this.ParentObject.CurrentCell ?? E.Actor.CurrentCell;
				Cell startCell2 = startCell;
				Cell cell = ((startCell2 != null) ? startCell2.GetRandomLocalCardinalAdjacentCell() : null);
				if (cell != null)
				{
					List<Cell> cells = Event.NewCellList();
					cells.Add(cell);
					Predicate<Cell> <>9__0;
					while (cells.Count < this.Size)
					{
						Cell cell2 = cells.Last<Cell>();
						Predicate<Cell> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = (Cell c) => !cells.Contains(c) && c != startCell);
						}
						cell = cell2.GetRandomLocalCardinalAdjacentCell(predicate);
						if (cell == null)
						{
							break;
						}
						cells.Add(cell);
					}
					GameObject gameObject = this.DeployToCells(cells, E.Actor);
					if (gameObject != null)
					{
						if (E.Actor != null && E.Actor.IsPlayer())
						{
							Popup.Show(GameText.VariableReplace(this.ExamineFailureMessage, this.ParentObject, gameObject, true), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						E.IdentifyIfDestroyed = true;
						this.ParentObject.Destroy(null, false, false, null);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600A814 RID: 43028 RVA: 0x003D76B8 File Offset: 0x003D58B8
		private GameObject DeployToCells(List<Cell> Cells, GameObject Actor)
		{
			GameObject gameObject = null;
			TextConsole textConsole = Look._TextConsole;
			The.Core.RenderBase(true, false);
			textConsole.WaitFrame();
			foreach (Cell cell in Cells)
			{
				string directionFromCell = Actor.CurrentCell.GetDirectionFromCell(cell, false);
				foreach (GameObject gameObject2 in cell.GetObjectsWithPart("Physics"))
				{
					if (gameObject2.Physics.Solid)
					{
						gameObject2.Physics.Push(directionFromCell, 7500, 4, false, true, null);
					}
				}
				foreach (GameObject gameObject3 in cell.GetObjectsWithPart("Combat"))
				{
					if (gameObject3.Physics != null)
					{
						gameObject3.Physics.Push(directionFromCell, 7500, 4, false, true, null);
					}
				}
				gameObject = cell.AddObject(this.Blueprint, null, null, null, null);
				The.Core.RenderBase(true, false);
				textConsole.WaitFrame();
			}
			return gameObject;
		}

		// Token: 0x0400455C RID: 17756
		public string Blueprint = "Foamcrete";

		// Token: 0x0400455D RID: 17757
		public int Size = 9;

		// Token: 0x0400455E RID: 17758
		public string Message = "You open the box and expose the compressed foamcrete to air. It starts to expand.";

		// Token: 0x0400455F RID: 17759
		public string ExamineFailureMessage = "=subject.T= suddenly =verb:erupt= in =object.name=!";

		// Token: 0x04004560 RID: 17760
		public string DeployingWhat = "Wall";

		// Token: 0x04004561 RID: 17761
		public string Sound = "Sounds/Interact/sfx_interact_portableWall_deploy";
	}
}
