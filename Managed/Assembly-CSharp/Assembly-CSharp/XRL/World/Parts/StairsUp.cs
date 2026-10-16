using System;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02001245 RID: 4677
	[Serializable]
	public class StairsUp : IPart
	{
		// Token: 0x0600B9F3 RID: 47603 RVA: 0x0043ECA4 File Offset: 0x0043CEA4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == PooledEvent<CheckAttackableEvent>.ID || ID == CommandSmartUseEvent.ID || ID == EnteredCellEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<SubjectToGravityEvent>.ID;
		}

		// Token: 0x0600B9F4 RID: 47604 RVA: 0x0043ECFD File Offset: 0x0043CEFD
		public override bool HandleEvent(SubjectToGravityEvent E)
		{
			E.SubjectToGravity = false;
			return false;
		}

		// Token: 0x0600B9F5 RID: 47605 RVA: 0x0043ED08 File Offset: 0x0043CF08
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (CommandBindingManager.GetCommandFromKey(Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift) == "CmdMoveU")
			{
				E.AddAction("Ascend", "ascend", "Ascend", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9F6 RID: 47606 RVA: 0x0043ED58 File Offset: 0x0043CF58
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Ascend" && E.Actor.IsPlayer() && CommandBindingManager.GetCommandFromKey(Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift) == "CmdMoveU")
			{
				Popup.ShowFail("Use " + ControlManager.getCommandInputFormatted("CmdMoveU", true, false) + " to ascend.", true, true, true);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9F7 RID: 47607 RVA: 0x0043EDCA File Offset: 0x0043CFCA
		public override bool HandleEvent(CheckAttackableEvent E)
		{
			return false;
		}

		// Token: 0x0600B9F8 RID: 47608 RVA: 0x0043EDCD File Offset: 0x0043CFCD
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return E.Actor.CurrentCell != this.ParentObject.CurrentCell && base.HandleEvent(E);
		}

		// Token: 0x0600B9F9 RID: 47609 RVA: 0x0043EDF0 File Offset: 0x0043CFF0
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.CurrentCell == this.ParentObject.CurrentCell)
			{
				if (E.Actor.IsPlayer())
				{
					Keyboard.PushMouseEvent("Command:CmdMoveU", null);
				}
				else
				{
					E.Actor.Move("U", false, false, false, false, false, true, null, null, false, null, null, null, false, false, null, null, 0);
				}
			}
			return false;
		}

		// Token: 0x0600B9FA RID: 47610 RVA: 0x0043EE64 File Offset: 0x0043D064
		public override bool HandleEvent(IdleQueryEvent E)
		{
			if (this.ParentObject.HasTagOrProperty("IdleStairs") && E.Actor.HasPart<Brain>() && Stat.Random(1, 2000) == 2000)
			{
				GameObject who = E.Actor;
				who.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
				{
					if (who.GetCurrentCell() == this.ParentObject.GetCurrentCell())
					{
						who.Move("U", false, false, false, false, false, true, null, null, false, null, null, null, false, false, null, null, 0);
					}
					h.FailToParent();
				}, null, null));
				who.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9FB RID: 47611 RVA: 0x0043EF10 File Offset: 0x0043D110
		public override bool HandleEvent(EnteredCellEvent E)
		{
			bool flag = false;
			foreach (GameObject gameObject in E.Cell.Objects)
			{
				if (gameObject != this.ParentObject && gameObject.HasPart<StairsUp>())
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				E.Cell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				this.ParentObject.Obliterate(null, false, null);
				return false;
			}
			if (this.Connected)
			{
				E.Cell.ParentZone.AddZoneConnection("u", E.Cell.X, E.Cell.Y, "StairsDown", this.ConnectionObject);
			}
			else
			{
				E.Cell.ParentZone.AddZoneConnection("u", E.Cell.X, E.Cell.Y, "UpEnd", null);
			}
			if (E != null)
			{
				Cell cell = E.Cell;
				if (cell != null)
				{
					cell.ClearWalls();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9FC RID: 47612 RVA: 0x0043F038 File Offset: 0x0043D238
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B9FD RID: 47613 RVA: 0x0043F03B File Offset: 0x0043D23B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ClimbUp");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B9FE RID: 47614 RVA: 0x0043F050 File Offset: 0x0043D250
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "ClimbUp"))
			{
				return base.FireEvent(E);
			}
			GameObject gameObjectParameter = E.GetGameObjectParameter("GO");
			if (gameObjectParameter != null && gameObjectParameter.IsPlayer() && this.ParentObject.HasTag("KeyObject") && !gameObjectParameter.IsCarryingObject(this.ParentObject.GetTag("KeyObject", null)))
			{
				Popup.Show("You don't have the proper key standin message", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			if (gameObjectParameter.Move("U", false, false, false, false, false, true, null, null, false, null, null, null, false, false, null, null, 0))
			{
				base.PlayWorldSound(this.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			}
			return false;
		}

		// Token: 0x04004ABB RID: 19131
		public bool Connected = true;

		// Token: 0x04004ABC RID: 19132
		public string ConnectionObject = "StairsDown";

		// Token: 0x04004ABD RID: 19133
		public string Sound = "Sounds/Interact/sfx_interact_stairs_ascend";
	}
}
