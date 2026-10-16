using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001244 RID: 4676
	[Serializable]
	public class StairsDown : IPart
	{
		// Token: 0x0600B9D7 RID: 47575 RVA: 0x0043DA4C File Offset: 0x0043BC4C
		public override bool SameAs(IPart p)
		{
			StairsDown stairsDown = p as StairsDown;
			return stairsDown.Connected == this.Connected && stairsDown.PullDown == this.PullDown && !(stairsDown.PullMessage != this.PullMessage) && base.SameAs(p);
		}

		// Token: 0x0600B9D8 RID: 47576 RVA: 0x0043DA9C File Offset: 0x0043BC9C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == PooledEvent<CheckAttackableEvent>.ID || ID == CommandSmartUseEvent.ID || ID == EnteredCellEvent.ID || (ID == GetInventoryActionsEvent.ID && !this.PullDown) || (ID == GetNavigationWeightEvent.ID && this.PullDown) || (ID == GetAdjacentNavigationWeightEvent.ID && this.PullDown) || ID == GravitationEvent.ID || ID == PooledEvent<IdleQueryEvent>.ID || (ID == PooledEvent<InterruptAutowalkEvent>.ID && this.PullDown) || (ID == InventoryActionEvent.ID && !this.PullDown) || (ID == ObjectEnteredCellEvent.ID && this.PullDown) || (ID == ObjectEnteringCellEvent.ID && this.PullDown) || ID == PooledEvent<SubjectToGravityEvent>.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600B9D9 RID: 47577 RVA: 0x0043DB77 File Offset: 0x0043BD77
		public override bool HandleEvent(SubjectToGravityEvent E)
		{
			E.SubjectToGravity = false;
			return false;
		}

		// Token: 0x0600B9DA RID: 47578 RVA: 0x0043DB84 File Offset: 0x0043BD84
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.PullDown && CommandBindingManager.GetCommandFromKey(Keys.RButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift) == "CmdMoveD")
			{
				E.AddAction("Descend", "descend", "Descend", null, 'd', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9DB RID: 47579 RVA: 0x0043DBDC File Offset: 0x0043BDDC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Descend" && !this.PullDown && E.Actor.IsPlayer() && CommandBindingManager.GetCommandFromKey(Keys.RButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Shift) == "CmdMoveD")
			{
				Popup.ShowFail("Use " + ControlManager.getCommandInputFormatted("CmdMoveD", true, false) + " to descend.", true, true, true);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9DC RID: 47580 RVA: 0x0043DC56 File Offset: 0x0043BE56
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (this.PullDown)
			{
				E.MinWeight(99);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9DD RID: 47581 RVA: 0x0043DC6F File Offset: 0x0043BE6F
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (this.PullDown)
			{
				E.MinWeight(4);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9DE RID: 47582 RVA: 0x0043DC87 File Offset: 0x0043BE87
		public override bool HandleEvent(CheckAttackableEvent E)
		{
			return false;
		}

		// Token: 0x0600B9DF RID: 47583 RVA: 0x0043DC8A File Offset: 0x0043BE8A
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			if (this.PullDown && The.Player.CanFall)
			{
				E.IndicateObject = this.ParentObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9E0 RID: 47584 RVA: 0x0043DCB8 File Offset: 0x0043BEB8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (E.Cell.ParentZone.ZoneWorld == "Tzimtzlum")
			{
				E.Cell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				this.ParentObject.Obliterate(null, false, null);
				E.Cell.AddObject("Space-Time Rift", null, null, null, null);
				return false;
			}
			int i = 0;
			int count = E.Cell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = E.Cell.Objects[i];
				if (gameObject != this.ParentObject && gameObject.HasPart<StairsDown>())
				{
					E.Cell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					this.ParentObject.Obliterate(null, false, null);
					return false;
				}
				i++;
			}
			if (this.Connected)
			{
				E.Cell.ParentZone.AddZoneConnection("d", E.Cell.X, E.Cell.Y, "StairsUp", this.ConnectionObject);
			}
			else if (this.ConnectLanding)
			{
				E.Cell.ParentZone.AddZoneConnection("d", E.Cell.X, E.Cell.Y, this.PullDown ? "PullDownEnd" : "DownEnd", null);
			}
			if (this.PullDown && !E.IgnoreGravity && (E.Cell.Objects.Count > 1 || !E.Cell.Objects.Contains(this.ParentObject)))
			{
				List<GameObject> list = Event.NewGameObjectList();
				list.AddRange(E.Cell.Objects);
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					if (list[j] != this.ParentObject)
					{
						this.CheckPullDown(list[j]);
					}
					j++;
				}
			}
			Cell cell = E.Cell;
			if (cell != null)
			{
				cell.ClearWalls();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9E1 RID: 47585 RVA: 0x0043DEC8 File Offset: 0x0043C0C8
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			if (this.PullDown)
			{
				Cell currentCell = base.currentCell;
				if (currentCell != null)
				{
					List<GameObject> list = Event.NewGameObjectList();
					list.AddRange(currentCell.Objects);
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						this.CheckPullDown(list[i]);
						i++;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9E2 RID: 47586 RVA: 0x0043DF24 File Offset: 0x0043C124
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return (!this.PullDown || E.Forced || E.System || !this.IsValidForPullDown(E.Object) || !E.Object.IsPlayer() || E.Object.IsConfused || ((this.IsLongFall() ? this.JumpPrompt : null) ?? E.Cell.ParentZone.GetZoneProperty("PullDownPrompt", "")).IsNullOrEmpty() || Popup.WarnYesNoCancel(this.JumpPrompt, "Sounds/UI/ui_notification_warning", true, DialogResult.Cancel) == DialogResult.Yes) && base.HandleEvent(E);
		}

		// Token: 0x0600B9E3 RID: 47587 RVA: 0x0043DFC7 File Offset: 0x0043C1C7
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (this.PullDown && !E.IgnoreGravity)
			{
				this.CheckPullDown(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9E4 RID: 47588 RVA: 0x0043DFED File Offset: 0x0043C1ED
		public override bool HandleEvent(GravitationEvent E)
		{
			if (this.PullDown)
			{
				this.CheckPullDown(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9E5 RID: 47589 RVA: 0x0043E00B File Offset: 0x0043C20B
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return E.Actor.CurrentCell != this.ParentObject.CurrentCell && base.HandleEvent(E);
		}

		// Token: 0x0600B9E6 RID: 47590 RVA: 0x0043E030 File Offset: 0x0043C230
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.CurrentCell == this.ParentObject.CurrentCell)
			{
				if (E.Actor.IsPlayer())
				{
					Keyboard.PushMouseEvent("Command:CmdMoveD", null);
				}
				else
				{
					for (int i = 0; i < this.Levels; i++)
					{
						E.Actor.Move("D", false, this.Levels > 1, false, false, false, true, null, null, false, null, null, null, false, false, null, null, 0);
					}
				}
			}
			return false;
		}

		// Token: 0x0600B9E7 RID: 47591 RVA: 0x0043E0BC File Offset: 0x0043C2BC
		public override bool HandleEvent(IdleQueryEvent E)
		{
			if (this.ParentObject.HasTagOrProperty("IdleStairs") && E.Actor.HasPart<Brain>() && Stat.Random(1, 2000) == 2000)
			{
				GameObject who = E.Actor;
				who.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
				{
					if (who.CurrentCell == this.ParentObject.CurrentCell)
					{
						for (int i = 0; i < this.Levels; i++)
						{
							who.Move("D", false, this.Levels > 1, false, false, false, true, null, null, false, null, null, null, false, false, null, null, 0);
						}
					}
					h.FailToParent();
				}, null, null));
				who.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9E8 RID: 47592 RVA: 0x0043E166 File Offset: 0x0043C366
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B9E9 RID: 47593 RVA: 0x0043E169 File Offset: 0x0043C369
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ClimbDown");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B9EA RID: 47594 RVA: 0x0043E17E File Offset: 0x0043C37E
		public bool IsValidForPullDown(GameObject Object)
		{
			return GameObject.Validate(ref Object) && Object != this.ParentObject && Object.CanFall;
		}

		// Token: 0x0600B9EB RID: 47595 RVA: 0x0043E19C File Offset: 0x0043C39C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ClimbDown")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("GO");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					string tag = this.ParentObject.GetTag("KeyObject", null);
					if (!string.IsNullOrEmpty(tag) && !gameObjectParameter.IsCarryingObject(tag))
					{
						base.DidX("are", "locked, and you don't have the key", null, null, null, null, null, false, false, null, null, false, false, false, true, false, null);
						return false;
					}
				}
				if (!this.PullDown || gameObjectParameter.IsFlying)
				{
					bool flag = false;
					for (int i = 0; i < this.Levels; i++)
					{
						flag |= gameObjectParameter.Move("D", false, this.Levels > 1, false, false, false, true, null, null, false, null, null, null, false, false, null, null, 0);
					}
					if (flag)
					{
						base.PlayWorldSound(this.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					}
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B9EC RID: 47596 RVA: 0x0043E2B0 File Offset: 0x0043C4B0
		public bool CheckPullDown(GameObject Object)
		{
			try
			{
				if (!this.PullDown)
				{
					return false;
				}
				if (!GameObject.Validate(this.ParentObject))
				{
					return false;
				}
				if (!this.IsValidForPullDown(Object))
				{
					return false;
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				int num;
				Cell cell = this.GetPullDownCell(currentCell, out num, int.MaxValue);
				if (cell == null)
				{
					return false;
				}
				if (!BeforePullDownEvent.Check(this.ParentObject, Object, ref cell))
				{
					return false;
				}
				if (!GameObject.Validate(this.ParentObject))
				{
					return false;
				}
				if (cell == null)
				{
					return false;
				}
				if (Object.IsPlayer())
				{
					ZoneManager.ZoneTransitionCount -= num;
				}
				if (!cell.IsPassable(Object, true))
				{
					Cell closestPassableCellFor = cell.getClosestPassableCellFor(Object);
					if (closestPassableCellFor != null && closestPassableCellFor.RealDistanceTo(cell, true) <= 2.0)
					{
						cell = closestPassableCellFor;
					}
				}
				if (num > 1)
				{
					base.PlayWorldSound("sfx_characterTrigger_shaft_fall", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				else
				{
					base.PlayWorldSound("fly_generic_fall", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				string text = (this.ParentObject.HasPropertyOrTag("FallPreposition") ? this.ParentObject.GetPropertyOrTag("FallPreposition", null) : "down");
				bool flag = this.ParentObject.HasPropertyOrTag("FallUseDefiniteArticle") && this.ParentObject.GetPropertyOrTag("FallUseDefiniteArticle", null) == "true";
				if (Object.IsPlayerLed() && !Object.IsTrifling)
				{
					Popup.Show(string.Concat(new string[]
					{
						"Your companion, ",
						Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, true, false, null, false, false, false, false, null, false, null, false, true),
						",",
						Object.GetVerb("have", true, false, false, true, false),
						" fallen ",
						text,
						" ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" ",
						The.Player.DescribeDirectionToward(this.ParentObject, false, false),
						"!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					string text2 = "fall";
					string text3 = text;
					GameObject parentObject = this.ParentObject;
					string text4 = null;
					string text5 = null;
					string text6 = null;
					string text7 = null;
					GameObject gameObject = null;
					bool flag2 = !flag;
					IComponent<GameObject>.XDidYToZ(Object, text2, text3, parentObject, text4, text5, text6, text7, gameObject, Object, false, false, flag2, false, false, null, null, null, false, false, false, false, false, null);
				}
				Object.SystemMoveTo(cell, new int?(0), true, true, false, false);
				if (!Object.IsPlayer())
				{
					IComponent<GameObject>.XDidY(Object, "fall", "down from above", null, null, null, null, Object, false, true, null, null, false, false, false, false, false, null);
				}
				cell = Object.CurrentCell ?? cell;
				List<GameObject> list = null;
				if (Object.GetMatterPhase() <= 2)
				{
					int phase = Object.GetPhase();
					int i = 0;
					int count = cell.Objects.Count;
					while (i < count)
					{
						GameObject gameObject2 = cell.Objects[i];
						if (gameObject2 != null && gameObject2 != Object && gameObject2.IsCombat && gameObject2.GetMatterPhase() <= 2 && gameObject2.PhaseMatches(phase))
						{
							if (list == null)
							{
								list = Event.NewGameObjectList();
							}
							list.Add(gameObject2);
						}
						i++;
					}
				}
				if (list != null)
				{
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						list[j].TakeDamage(Stat.Random(1, 4), string.Concat(new string[]
						{
							"from ",
							Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" falling on ",
							list[j].them,
							"."
						}), "Crushing", Object.An(int.MaxValue, null, null, false, true, false, false, false, true, true, false, false, true, false, null, false, null, false) + " fell on " + list[j].them + ".", Object.An(int.MaxValue, null, null, false, true, false, false, false, true, true, false, false, true, false, null, false, null, false) + " fell on " + list[j].them + ".", Object, null, null, null, null, true, false, false, false, false, false, false, false, false, 5, null);
						if (num <= 1 && j == count2 - 1)
						{
							Object.TakeDamage(Stat.Random(1, 4), string.Concat(new string[]
							{
								"from ",
								Object.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								" falling on ",
								list[j].them,
								"."
							}), "Crushing", Object.An(int.MaxValue, null, null, false, true, false, false, false, true, true, false, false, true, false, null, false, null, false) + " fell on " + list[j].them + ".", Object.An(int.MaxValue, null, null, false, true, false, false, false, true, true, false, false, true, false, null, false, null, false) + " fell on " + list[j].them + ".", Object, null, null, null, null, true, false, false, false, false, false, false, false, false, 5, null);
							if (Object.IsPlayer() && Object.hitpoints <= 0)
							{
								Achievement.DIE_BY_FALLING.Unlock();
							}
						}
						j++;
					}
				}
				if (num > 1)
				{
					StairsDown.InflictFallDamage(Object, num, this.GenericFall ? null : ("You fell down " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "."), this.GenericFall ? null : (Object.It + " @@fell down " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "."));
				}
				FellDownEvent.Send(Object, cell, currentCell, num);
				if (Object.IsValid())
				{
					GameObject partyLeader = Object.PartyLeader;
					if (partyLeader != null && !Object.InSameZone(partyLeader) && !Object.HasEffect<Incommunicado>())
					{
						Object.ApplyEffect(new Incommunicado(), null);
					}
				}
				if (Object != null && Object.IsPlayer())
				{
					The.ZoneManager.SetActiveZone(cell.ParentZone);
					The.ZoneManager.ProcessGoToPartyLeader();
					if (num > 1)
					{
						IComponent<GameObject>.AddPlayerMessage("You fall downward!", null, true);
					}
					else if (!this.PullMessage.IsNullOrEmpty())
					{
						IComponent<GameObject>.AddPlayerMessage(this.PullMessage, null, true);
					}
				}
				bool? flag3;
				if (Object == null)
				{
					flag3 = null;
				}
				else
				{
					Cell currentCell2 = Object.CurrentCell;
					flag3 = ((currentCell2 != null) ? new bool?(currentCell2.IsSolid()) : null);
				}
				bool? flag4 = flag3;
				if (flag4.GetValueOrDefault())
				{
					Cell cell2;
					if (Object == null)
					{
						cell2 = null;
					}
					else
					{
						Cell currentCell3 = Object.CurrentCell;
						cell2 = ((currentCell3 != null) ? currentCell3.GetFirstPassableConnectedAdjacentCell(80, true, null, null, true) : null);
					}
					Cell cell3 = cell2;
					if (cell3 != null)
					{
						Object.SystemMoveTo(cell3, null, false, true, false, false);
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("StairsDown::CheckPulldown", ex, "game_exception");
			}
			return true;
		}

		// Token: 0x0600B9ED RID: 47597 RVA: 0x0043EA70 File Offset: 0x0043CC70
		public static void InflictFallDamage(GameObject Object, int Distance, string DeathReason = null, string ThirdPersonDeathReason = null)
		{
			if (Distance <= 1)
			{
				return;
			}
			if (DeathReason == null)
			{
				DeathReason = "You fell from a great height.";
			}
			if (ThirdPersonDeathReason == null)
			{
				ThirdPersonDeathReason = Object.It + " @@fell from a great height.";
			}
			Object.TakeDamage(Stat.Roll(Distance.ToString() + "d20+" + (100 + Distance * 25).ToString(), null), "from " + Object.its + " fall.", "Crushing Falling", DeathReason, ThirdPersonDeathReason, Object, null, null, null, null, true, false, false, false, false, false, false, false, false, 5, null);
			if (Object.IsPlayer() && Object.hitpoints <= 0)
			{
				Achievement.DIE_BY_FALLING.Unlock();
			}
		}

		// Token: 0x0600B9EE RID: 47598 RVA: 0x0043EB18 File Offset: 0x0043CD18
		public bool IsLongFall()
		{
			int num;
			this.GetPullDownCell(out num, 2);
			return num > 1;
		}

		// Token: 0x0600B9EF RID: 47599 RVA: 0x0043EB34 File Offset: 0x0043CD34
		public Cell GetPullDownCell(Cell CC, out int Distance, int MaxDistance = 2147483647)
		{
			Distance = 0;
			if (CC == null || CC.ParentZone == null || !CC.ParentZone.Built || CC.HasObjectWithTagOrProperty("SuspendedPlatform"))
			{
				return null;
			}
			Cell cell = CC.GetCellFromDirection("D", false);
			bool flag = cell.HasObjectWithPropertyOrTag("SuspendedPlatform");
			if (cell != null && !flag)
			{
				Distance++;
				if (Distance >= MaxDistance)
				{
					return null;
				}
				for (int i = 1; i < this.Levels; i++)
				{
					Cell cellFromDirection = cell.GetCellFromDirection("D", false);
					if (cellFromDirection == null)
					{
						break;
					}
					cell = cellFromDirection;
					Distance++;
					if (Distance >= MaxDistance)
					{
						return null;
					}
					if (cell.HasObjectWithPropertyOrTag("SuspendedPlatform"))
					{
						break;
					}
				}
			}
			if (cell != null && !flag)
			{
				GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("StairsDown");
				if (firstObjectWithPart != null)
				{
					StairsDown part = firstObjectWithPart.GetPart<StairsDown>();
					if (part != null && part.PullDown)
					{
						int num;
						Cell pullDownCell = part.GetPullDownCell(out num, MaxDistance - Distance);
						if (pullDownCell != null)
						{
							cell = pullDownCell;
						}
						Distance += num;
						if (Distance >= MaxDistance)
						{
							return null;
						}
					}
				}
			}
			return cell;
		}

		// Token: 0x0600B9F0 RID: 47600 RVA: 0x0043EC26 File Offset: 0x0043CE26
		public Cell GetPullDownCell(out int Distance, int MaxDistance = 2147483647)
		{
			return this.GetPullDownCell(base.currentCell, out Distance, MaxDistance);
		}

		// Token: 0x0600B9F1 RID: 47601 RVA: 0x0043EC38 File Offset: 0x0043CE38
		public Cell GetPullDownCell(int MaxDistance = 2147483647)
		{
			int num;
			return this.GetPullDownCell(out num, MaxDistance);
		}

		// Token: 0x04004AB2 RID: 19122
		public bool Connected = true;

		// Token: 0x04004AB3 RID: 19123
		public string ConnectionObject = "StairsUp";

		// Token: 0x04004AB4 RID: 19124
		public bool PullDown;

		// Token: 0x04004AB5 RID: 19125
		public bool GenericFall;

		// Token: 0x04004AB6 RID: 19126
		public bool ConnectLanding = true;

		// Token: 0x04004AB7 RID: 19127
		public string PullMessage = "You fall down a deep shaft!";

		// Token: 0x04004AB8 RID: 19128
		public string JumpPrompt = "It looks like an awfully long fall. Are you sure you want to jump into the shaft?";

		// Token: 0x04004AB9 RID: 19129
		public string Sound = "Sounds/Interact/sfx_interact_stairs_descend";

		// Token: 0x04004ABA RID: 19130
		public int Levels = 1;
	}
}
