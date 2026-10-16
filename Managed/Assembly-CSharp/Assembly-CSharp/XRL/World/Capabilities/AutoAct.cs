using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Collections;
using XRL.Messages;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI.Pathfinding;
using XRL.World.Parts;

namespace XRL.World.Capabilities
{
	// Token: 0x020009DE RID: 2526
	[HasWishCommand]
	public static class AutoAct
	{
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06006BB6 RID: 27574 RVA: 0x0022BE8A File Offset: 0x0022A08A
		// (set) Token: 0x06006BB7 RID: 27575 RVA: 0x0022BE98 File Offset: 0x0022A098
		public static string Setting
		{
			get
			{
				return The.Core.PlayerWalking;
			}
			set
			{
				if (value != "" && The.Core.PlayerWalking == "")
				{
					AutoAct.Count++;
				}
				The.Core.PlayerWalking = value;
				AutoAct.Digging = 0;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06006BB8 RID: 27576 RVA: 0x0022BEE5 File Offset: 0x0022A0E5
		// (set) Token: 0x06006BB9 RID: 27577 RVA: 0x0022BEEC File Offset: 0x0022A0EC
		public static OngoingAction Action
		{
			get
			{
				return AutoAct._Action;
			}
			set
			{
				if (value != null && AutoAct.IsActive(false))
				{
					AutoAct.ResumeAction = AutoAct._Action;
					AutoAct.ResumeSetting = AutoAct.Setting;
				}
				AutoAct._Action = value;
				if (AutoAct._Action != null)
				{
					AutoAct.Setting = "o";
				}
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06006BBA RID: 27578 RVA: 0x0022BF24 File Offset: 0x0022A124
		// (set) Token: 0x06006BBB RID: 27579 RVA: 0x0022BF2B File Offset: 0x0022A12B
		public static OngoingAction ResumeAction
		{
			get
			{
				return AutoAct._ResumeAction;
			}
			set
			{
				AutoAct._ResumeAction = value;
				if (AutoAct._ResumeAction != null)
				{
					AutoAct.ResumeSetting = "o";
				}
			}
		}

		// Token: 0x06006BBC RID: 27580 RVA: 0x0022BF44 File Offset: 0x0022A144
		public static bool IsActive(string What)
		{
			return What != null && What != "" && What != "ReopenMissileUI";
		}

		// Token: 0x06006BBD RID: 27581 RVA: 0x0022BF63 File Offset: 0x0022A163
		public static bool IsActive(bool IgnoreAutoget = false)
		{
			return (!IgnoreAutoget || !(AutoAct.Setting == "g")) && AutoAct.IsActive(AutoAct.Setting);
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x0022BF85 File Offset: 0x0022A185
		public static bool IsInterruptable(string What)
		{
			return AutoAct.IsActive(What);
		}

		// Token: 0x06006BBF RID: 27583 RVA: 0x0022BF8D File Offset: 0x0022A18D
		public static bool IsInterruptable()
		{
			return AutoAct.IsInterruptable(AutoAct.Setting);
		}

		// Token: 0x06006BC0 RID: 27584 RVA: 0x0022BF9C File Offset: 0x0022A19C
		public static bool ShouldHostilesInterrupt(string What, OngoingAction Action = null, bool logSpot = false, bool popSpot = false, bool CheckingPrior = true)
		{
			if (!AutoAct.IsActive(What))
			{
				return false;
			}
			if (What == "g")
			{
				if (Options.AutogetIfHostiles)
				{
					return false;
				}
			}
			else if (What == "o" && Action != null && !Action.ShouldHostilesInterrupt())
			{
				return false;
			}
			return The.Player.ArePerceptibleHostilesNearby(logSpot, popSpot, null, Action, What, AutoAct.IsResting(What, null) ? int.MinValue : Options.AutoexploreIgnoreEasyEnemies, Options.AutoexploreIgnoreDistantEnemies, AutoAct.IsCombat(What, null), CheckingPrior);
		}

		// Token: 0x06006BC1 RID: 27585 RVA: 0x0022C01B File Offset: 0x0022A21B
		public static bool ShouldHostilesInterrupt(bool logSpot = false, bool popSpot = false, bool CheckingPrior = true)
		{
			return AutoAct.ShouldHostilesInterrupt(AutoAct.Setting, AutoAct.Action, logSpot, popSpot, CheckingPrior);
		}

		// Token: 0x06006BC2 RID: 27586 RVA: 0x0022C02F File Offset: 0x0022A22F
		public static bool CheckHostileInterrupt(bool logSpot)
		{
			if (AutoAct.ShouldHostilesInterrupt(logSpot, false, false))
			{
				AutoAct.Interrupt(null, null, null, false);
				return true;
			}
			return false;
		}

		// Token: 0x06006BC3 RID: 27587 RVA: 0x0022C047 File Offset: 0x0022A247
		public static bool CheckHostileInterrupt()
		{
			return AutoAct.CheckHostileInterrupt(!AutoAct.IsOnlyGathering() || Sidebar.AnyAutogotItems());
		}

		// Token: 0x06006BC4 RID: 27588 RVA: 0x0022C05D File Offset: 0x0022A25D
		public static bool ShouldHostilesPreventAutoget()
		{
			return AutoAct.ShouldHostilesInterrupt("g", null, false, false, true);
		}

		// Token: 0x06006BC5 RID: 27589 RVA: 0x0022C070 File Offset: 0x0022A270
		public static bool IsMovement(string What, OngoingAction Action = null)
		{
			if (!AutoAct.IsActive(What))
			{
				return false;
			}
			char c = What[0];
			if (c <= 'g')
			{
				if (c == '.')
				{
					return false;
				}
				if (c == 'g')
				{
					return false;
				}
			}
			else
			{
				if (c == 'o')
				{
					return Action != null && Action.IsMovement();
				}
				if (c == 'r' || c == 'z')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006BC6 RID: 27590 RVA: 0x0022C0C4 File Offset: 0x0022A2C4
		public static bool IsMovement()
		{
			return AutoAct.IsMovement(AutoAct.Setting, AutoAct.Action);
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x0022C0D5 File Offset: 0x0022A2D5
		public static bool IsAnyMovement()
		{
			return AutoAct.IsMovement(AutoAct.Setting, AutoAct.Action) || AutoAct.IsMovement(AutoAct.ResumeSetting, AutoAct.ResumeAction);
		}

		// Token: 0x06006BC8 RID: 27592 RVA: 0x0022C0F9 File Offset: 0x0022A2F9
		public static bool IsCombat(string What, OngoingAction Action = null)
		{
			return What == "ReopenMissileUI" || What == "a" || (What == "o" && Action != null && Action.IsCombat());
		}

		// Token: 0x06006BC9 RID: 27593 RVA: 0x0022C133 File Offset: 0x0022A333
		public static bool IsCombat()
		{
			return AutoAct.IsCombat(AutoAct.Setting, AutoAct.Action);
		}

		// Token: 0x06006BCA RID: 27594 RVA: 0x0022C144 File Offset: 0x0022A344
		public static bool IsAnyCombat()
		{
			return AutoAct.IsCombat(AutoAct.Setting, AutoAct.Action) || AutoAct.IsCombat(AutoAct.ResumeSetting, AutoAct.ResumeAction);
		}

		// Token: 0x06006BCB RID: 27595 RVA: 0x0022C168 File Offset: 0x0022A368
		public static bool IsExploration(string What, OngoingAction Action = null)
		{
			return What == "?" || (What == "o" && Action != null && Action.IsExploration());
		}

		// Token: 0x06006BCC RID: 27596 RVA: 0x0022C193 File Offset: 0x0022A393
		public static bool IsExploration()
		{
			return AutoAct.IsExploration(AutoAct.Setting, AutoAct.Action);
		}

		// Token: 0x06006BCD RID: 27597 RVA: 0x0022C1A4 File Offset: 0x0022A3A4
		public static bool IsAnyExploration()
		{
			return AutoAct.IsExploration(AutoAct.Setting, AutoAct.Action) || AutoAct.IsExploration(AutoAct.ResumeSetting, AutoAct.ResumeAction);
		}

		// Token: 0x06006BCE RID: 27598 RVA: 0x0022C1C8 File Offset: 0x0022A3C8
		public static bool IsGathering(string What, OngoingAction Action = null)
		{
			return What == "g" || (What == "o" && Action != null && Action.IsGathering());
		}

		// Token: 0x06006BCF RID: 27599 RVA: 0x0022C1F3 File Offset: 0x0022A3F3
		public static bool IsGathering()
		{
			return AutoAct.IsGathering(AutoAct.Setting, AutoAct.Action);
		}

		// Token: 0x06006BD0 RID: 27600 RVA: 0x0022C204 File Offset: 0x0022A404
		public static bool IsAnyGathering()
		{
			return AutoAct.IsGathering(AutoAct.Setting, AutoAct.Action) || AutoAct.IsGathering(AutoAct.ResumeSetting, AutoAct.ResumeAction);
		}

		// Token: 0x06006BD1 RID: 27601 RVA: 0x0022C228 File Offset: 0x0022A428
		public static bool IsOnlyGathering()
		{
			return AutoAct.IsGathering(AutoAct.Setting, AutoAct.Action) && (AutoAct.ResumeSetting.IsNullOrEmpty() || AutoAct.IsGathering(AutoAct.ResumeSetting, AutoAct.ResumeAction));
		}

		// Token: 0x06006BD2 RID: 27602 RVA: 0x0022C25C File Offset: 0x0022A45C
		public static bool IsResting(string What, OngoingAction Action = null)
		{
			if (!AutoAct.IsActive(What))
			{
				return false;
			}
			char c = What[0];
			if (c <= 'o')
			{
				if (c != '.')
				{
					if (c != 'o')
					{
						return false;
					}
					return Action != null && Action.IsResting();
				}
			}
			else if (c != 'r' && c != 'z')
			{
				return false;
			}
			return true;
		}

		// Token: 0x06006BD3 RID: 27603 RVA: 0x0022C2A5 File Offset: 0x0022A4A5
		public static bool IsResting()
		{
			return AutoAct.IsResting(AutoAct.Setting, AutoAct.Action);
		}

		// Token: 0x06006BD4 RID: 27604 RVA: 0x0022C2B6 File Offset: 0x0022A4B6
		public static bool IsAnyResting()
		{
			return AutoAct.IsResting(AutoAct.Setting, AutoAct.Action) || AutoAct.IsResting(AutoAct.ResumeSetting, AutoAct.ResumeAction);
		}

		// Token: 0x06006BD5 RID: 27605 RVA: 0x0022C2DC File Offset: 0x0022A4DC
		public static bool IsRateLimited(string What, OngoingAction Action = null)
		{
			if (What == "o")
			{
				return Action != null && Action.IsRateLimited();
			}
			return AutoAct.IsGathering(What, Action) || AutoAct.IsCombat(What, Action) || (AutoAct.IsMovement(What, Action) && AutoAct.Digging <= 0);
		}

		// Token: 0x06006BD6 RID: 27606 RVA: 0x0022C32C File Offset: 0x0022A52C
		public static bool IsRateLimited()
		{
			return AutoAct.IsRateLimited(AutoAct.Setting, AutoAct.Action);
		}

		// Token: 0x06006BD7 RID: 27607 RVA: 0x0022C33D File Offset: 0x0022A53D
		public static bool IsAnyRateLimited()
		{
			return AutoAct.IsRateLimited(AutoAct.Setting, AutoAct.Action) || AutoAct.IsRateLimited(AutoAct.ResumeSetting, AutoAct.ResumeAction);
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x0022C364 File Offset: 0x0022A564
		public static string GetDescription(string What, OngoingAction action)
		{
			if (!AutoAct.IsActive(What))
			{
				return "acting";
			}
			char c = What[0];
			if (c <= 'd')
			{
				if (c <= '?')
				{
					if (c == '.')
					{
						return "waiting";
					}
					if (c == '?')
					{
						return "exploring";
					}
				}
				else
				{
					if (c == 'a')
					{
						return "attacking";
					}
					if (c == 'd')
					{
						if (AutoAct.Digging > 0)
						{
							return "digging";
						}
					}
				}
			}
			else if (c <= 'o')
			{
				if (c == 'g')
				{
					return "gathering";
				}
				if (c == 'o')
				{
					return ((action != null) ? action.GetDescription() : null) ?? "acting";
				}
			}
			else if (c == 'r' || c == 'z')
			{
				return "resting";
			}
			return "moving";
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x0022C40D File Offset: 0x0022A60D
		public static string GetDescription()
		{
			if (!AutoAct.ResumeSetting.IsNullOrEmpty())
			{
				return AutoAct.GetDescription(AutoAct.ResumeSetting, AutoAct.ResumeAction);
			}
			return AutoAct.GetDescription(AutoAct.Setting, AutoAct.Action);
		}

		// Token: 0x06006BDA RID: 27610 RVA: 0x0022C43A File Offset: 0x0022A63A
		public static void ClearAutoMoveStop()
		{
			GameObject.AutomoveInterruptTurn = -2147483648L;
		}

		// Token: 0x06006BDB RID: 27611 RVA: 0x0022C448 File Offset: 0x0022A648
		public static void Interrupt(string Because = null, Cell IndicateCell = null, GameObject IndicateObject = null, bool IsThreat = false)
		{
			GameObject.AutomoveInterruptBecause = Because;
			GameObject.AutomoveInterruptTurn = The.Game.Turns;
			if (AutoAct.IsActive(false))
			{
				if (Because == null && AutoAct.Action != null)
				{
					Because = AutoAct.Action.GetInterruptBecause();
				}
				if (!Because.IsNullOrEmpty() && (!AutoAct.IsOnlyGathering() || Sidebar.AnyAutogotItems()))
				{
					MessageQueue.AddPlayerMessage(Event.NewStringBuilder(null).Append("{{").Append(IsThreat ? 'r' : 'y')
						.Append("|You stop ")
						.Append(AutoAct.GetDescription())
						.Append(" because ")
						.Append(Because)
						.Append(".}}")
						.ToString(), null, true);
				}
				if (IndicateObject != null && (!AutoAct.IsOnlyGathering() || Sidebar.AnyAutogotItems()))
				{
					IndicateObject.Indicate(IsThreat);
				}
				if (IndicateCell != null && (!AutoAct.IsOnlyGathering() || Sidebar.AnyAutogotItems()))
				{
					IndicateCell.Indicate(IsThreat);
				}
			}
			OngoingAction resumeAction = AutoAct.ResumeAction;
			if (resumeAction != null)
			{
				resumeAction.Interrupt();
			}
			OngoingAction resumeAction2 = AutoAct.ResumeAction;
			if (resumeAction2 != null)
			{
				resumeAction2.End();
			}
			OngoingAction action = AutoAct.Action;
			if (action != null)
			{
				action.Interrupt();
			}
			OngoingAction action2 = AutoAct.Action;
			if (action2 != null)
			{
				action2.End();
			}
			AutoAct.Setting = "";
			AutoAct.Action = null;
			AutoAct.ResumeSetting = "";
			AutoAct.ResumeAction = null;
			AutoAct.Attacking = false;
			The.Core.PlayerAvoid.Clear();
		}

		// Token: 0x06006BDC RID: 27612 RVA: 0x0022C5A0 File Offset: 0x0022A7A0
		public static void Interrupt(GameObject BecauseOf, bool ShowIndicator = true, bool IsThreat = false)
		{
			if (BecauseOf != null && AutoAct.IsActive(false))
			{
				if (!AutoAct.IsOnlyGathering() || Sidebar.AnyAutogotItems())
				{
					MessageQueue.AddPlayerMessage(The.Player.GenerateSpotMessage(BecauseOf, null, null, "see", false, null, true), IsThreat ? 'r' : 'y', true);
				}
				if (ShowIndicator)
				{
					BecauseOf.Indicate(IsThreat);
				}
			}
			OngoingAction resumeAction = AutoAct.ResumeAction;
			if (resumeAction != null)
			{
				resumeAction.Interrupt();
			}
			OngoingAction resumeAction2 = AutoAct.ResumeAction;
			if (resumeAction2 != null)
			{
				resumeAction2.End();
			}
			OngoingAction action = AutoAct.Action;
			if (action != null)
			{
				action.Interrupt();
			}
			OngoingAction action2 = AutoAct.Action;
			if (action2 != null)
			{
				action2.End();
			}
			AutoAct.Setting = "";
			AutoAct.Action = null;
			AutoAct.ResumeSetting = "";
			AutoAct.ResumeAction = null;
			AutoAct.Attacking = false;
			The.Core.PlayerAvoid.Clear();
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x0022C669 File Offset: 0x0022A869
		public static void Resume()
		{
			AutoAct.Setting = AutoAct.ResumeSetting;
			AutoAct.Action = AutoAct.ResumeAction;
			AutoAct.ResumeSetting = "";
			AutoAct.ResumeAction = null;
		}

		// Token: 0x06006BDE RID: 27614 RVA: 0x0022C690 File Offset: 0x0022A890
		public static bool TryFindEdgeStep(char Direction, out string Step)
		{
			Step = ".";
			Cell playerCell = The.PlayerCell;
			FindPath findPath = AutoAct.TargetPath;
			int num = Zone.CalculateNav(The.Player, false, false, false, false, false, true);
			if (findPath.Found && findPath.Steps[0].ParentZone == playerCell.ParentZone && AutoAct.EdgeLastAct == AutoAct.Count)
			{
				int count = findPath.Steps.Count;
				int num2 = findPath.Steps.IndexOf(playerCell);
				if (num2 == count - 1)
				{
					return true;
				}
				if (num2 != -1)
				{
					Cell cell = findPath.Steps[num2 + 1];
					int num3 = findPath.Weights[num2 + 1];
					if (cell.NavigationWeight(The.Player, num) == num3)
					{
						Step = findPath.Directions[num2];
						return true;
					}
				}
			}
			AutoAct.EdgeLastAct = AutoAct.Count;
			int num4 = playerCell.X;
			int num5 = playerCell.Y;
			int num6 = 0;
			int num7 = 0;
			FindPath findPath2 = AutoAct.TryPath;
			if (Direction == 'N')
			{
				num5 = 0;
				num7 = 1;
			}
			else if (Direction == 'S')
			{
				num5 = 24;
				num7 = -1;
			}
			else if (Direction == 'E')
			{
				num4 = 79;
				num6 = -1;
			}
			else
			{
				if (Direction != 'W')
				{
					AutoAct.Interrupt(null, null, null, false);
					MetricsManager.LogError("invalid screen edge " + Direction.ToString());
					return false;
				}
				num4 = 0;
				num6 = 1;
			}
			Zone parentZone = playerCell.ParentZone;
			int num8 = int.MaxValue;
			bool flag = num7 != 0;
			Cell cell2 = null;
			while (cell2 == null)
			{
				int i = 0;
				int num9 = parentZone.Width * 2;
				while (i < num9)
				{
					int num10 = i / 2;
					if (flag)
					{
						if (num4 + num10 >= parentZone.Width && num4 - num10 < 0)
						{
							break;
						}
					}
					else if (num5 + num10 >= parentZone.Height && num5 - num10 < 0)
					{
						break;
					}
					if (i % 2 == 1)
					{
						num10 = -num10;
					}
					int num11 = (flag ? (num4 + num10) : num4);
					int num12 = (flag ? num5 : (num5 + num10));
					Cell cell3 = parentZone.GetCell(num11, num12);
					if (cell3 != null)
					{
						if (cell3 == playerCell)
						{
							return true;
						}
						if (cell3.NavigationWeight(The.Player, num) <= 95)
						{
							findPath2.PerformPathfind(playerCell.ParentZone, playerCell.X, playerCell.Y, num11, num12, 0, The.Player, false, 95, false, false, false, false, false, true);
							if (findPath2.Usable && findPath2.Steps.Count < num8)
							{
								cell2 = cell3;
								num8 = findPath2.Steps.Count;
								FindPath findPath3 = findPath;
								findPath = findPath2;
								findPath2 = findPath3;
							}
						}
					}
					i++;
				}
				if (cell2 == null)
				{
					if (num4 != -1)
					{
						num4 += num6;
						if ((num6 < 0 && num4 <= playerCell.X) || (num6 > 0 && num4 >= playerCell.X))
						{
							break;
						}
					}
					if (num5 != -1)
					{
						num5 += num7;
						if ((num7 < 0 && num5 <= playerCell.Y) || (num7 > 0 && num5 >= playerCell.Y))
						{
							break;
						}
					}
				}
			}
			if (cell2 != null)
			{
				AutoAct.TargetPath = findPath;
				AutoAct.TryPath = findPath2;
				Step = findPath.Directions[0];
				return true;
			}
			return false;
		}

		// Token: 0x06006BDF RID: 27615 RVA: 0x0022C97C File Offset: 0x0022AB7C
		public static bool TryFindPathStep(Cell Target, out string Step)
		{
			Step = ".";
			Cell playerCell = The.PlayerCell;
			FindPath targetPath = AutoAct.TargetPath;
			if (targetPath.Found && targetPath.Steps[0].ParentZone == playerCell.ParentZone)
			{
				int num = targetPath.Steps.IndexOf(playerCell);
				if (num != -1 && num < targetPath.Steps.LastIndexOf(Target))
				{
					Cell cell = targetPath.Steps[num + 1];
					int num2 = targetPath.Weights[num + 1];
					int num3 = Zone.CalculateNav(The.Player, false, false, false, false, false, true);
					if (cell.NavigationWeight(The.Player, num3) == num2)
					{
						Step = targetPath.Directions[num];
						return true;
					}
				}
			}
			targetPath.PerformPathfind(playerCell.ParentZone, playerCell.X, playerCell.Y, Target.X, Target.Y, 0, The.Player, false, 95, false, false, false, false, false, true);
			if (targetPath.Found)
			{
				Step = targetPath.Directions[0];
				return true;
			}
			return false;
		}

		// Token: 0x06006BE0 RID: 27616 RVA: 0x0022CA7A File Offset: 0x0022AC7A
		public static void FindAutoexploreStep(out string Step, out bool Blackout)
		{
			AutoAct.FindAutoexploreStep(false, out Step, out Blackout);
		}

		// Token: 0x06006BE1 RID: 27617 RVA: 0x0022CA84 File Offset: 0x0022AC84
		public static void FindAutoexploreStep(bool Force, out string Step, out bool Blackout)
		{
			Blackout = false;
			Step = ".";
			Cell playerCell = The.PlayerCell;
			Zone zone = ((playerCell != null) ? playerCell.ParentZone : null);
			if (zone == null)
			{
				return;
			}
			if (Force || AutoAct.AutoexploreZone != zone)
			{
				AutoAct.AutoexploreZone = zone;
				AutoAct.AutoexploreLastAct = AutoAct.Count;
				AutoAct.AutoexploreObjects.Clear();
				AutoAct.AutoexploreCells.Clear();
				AutoAct.AutoexplorePath.Clear();
				zone.FloodAutoexplore(AutoAct.AutoexploreCells, AutoAct.AutoexploreObjects, AutoAct.AutoexploreLastCourse);
				AutoAct.AutoexploreCells.Reverse();
				AutoAct.AutoexploreObjects.Reverse();
			}
			Cell autoexploreLastTarget = AutoAct.AutoexploreLastTarget;
			if (autoexploreLastTarget != null && autoexploreLastTarget.ParentZone == zone && !autoexploreLastTarget.IsBlackedOut() && AutoAct.TryGetAutoexploreStepToLast(autoexploreLastTarget, out Step))
			{
				return;
			}
			AutoAct.AutoexploreLastTarget = null;
			if (AutoAct.AutoexploreLastAct != AutoAct.Count)
			{
				AutoAct.FindAutoexploreStep(true, out Step, out Blackout);
				return;
			}
			if (!AutoAct.AutoexploreObjects.IsNullOrEmpty<GameObject>())
			{
				GameObject[] array = AutoAct.AutoexploreObjects.GetArray();
				for (int i = AutoAct.AutoexploreObjects.Count - 1; i >= 0; i--)
				{
					GameObject gameObject = array[i];
					if (!AutoAct.IsAutoexploreObject(gameObject))
					{
						AutoAct.AutoexploreObjects.RemoveAt(i);
					}
					else
					{
						Cell currentCell = gameObject.CurrentCell;
						if (currentCell.IsBlackedOut())
						{
							Blackout = true;
						}
						else if (currentCell.IsExplored() && AutoAct.TryGetAutoexploreStepToCell(currentCell, true, out Step))
						{
							return;
						}
					}
				}
			}
			if (!AutoAct.AutoexploreCells.IsNullOrEmpty<Cell>())
			{
				Cell[] array2 = AutoAct.AutoexploreCells.GetArray();
				for (int j = AutoAct.AutoexploreCells.Count - 1; j >= 0; j--)
				{
					Cell cell = array2[j];
					if (!AutoAct.IsAutoexploreCell(cell))
					{
						AutoAct.AutoexploreCells.RemoveAt(j);
					}
					else if (cell.IsBlackedOut())
					{
						Blackout = true;
					}
					else if (AutoAct.TryGetAutoexploreStepToCell(cell, false, out Step))
					{
						return;
					}
				}
			}
			if (!Force)
			{
				AutoAct.FindAutoexploreStep(true, out Step, out Blackout);
			}
		}

		// Token: 0x06006BE2 RID: 27618 RVA: 0x0022CC3D File Offset: 0x0022AE3D
		private static bool TryGetAutoexploreStepToLast(Cell Cell, out string Step)
		{
			if (AutoAct.IsAutoexploreCell(Cell))
			{
				return AutoAct.TryGetAutoexploreStepToCell(Cell, false, out Step);
			}
			if (Cell.HasObject(new Predicate<GameObject>(AutoAct.IsAutoexploreObject)))
			{
				return AutoAct.TryGetAutoexploreStepToCell(Cell, true, out Step);
			}
			Step = ".";
			return false;
		}

		// Token: 0x06006BE3 RID: 27619 RVA: 0x0022CC78 File Offset: 0x0022AE78
		private static bool TryGetAutoexploreStepToCell(Cell Cell, bool AllowDigging, out string Step)
		{
			Step = ".";
			AutoAct.AutoexploreLastTarget = Cell;
			Cell playerCell = The.PlayerCell;
			if (Cell == playerCell)
			{
				return false;
			}
			FindPath autoexplorePath = AutoAct.AutoexplorePath;
			if (autoexplorePath.Found)
			{
				int num = autoexplorePath.Steps.IndexOf(playerCell);
				if (num != -1 && num < autoexplorePath.Steps.LastIndexOf(Cell))
				{
					Cell cell = autoexplorePath.Steps[num + 1];
					int num2 = autoexplorePath.Weights[num + 1];
					int num3 = Zone.CalculateNav(The.Player, true, false, false, false, false, AllowDigging);
					if (cell.NavigationWeight(The.Player, num3) == num2)
					{
						Step = autoexplorePath.Directions[num];
						return true;
					}
				}
			}
			autoexplorePath.PerformPathfind(playerCell.ParentZone, playerCell.X, playerCell.Y, Cell.ParentZone, Cell.X, Cell.Y, false, The.Player, true, false, false, null, 95, false, true, false, false, false, false, AllowDigging);
			if (autoexplorePath.Found)
			{
				Step = autoexplorePath.Directions[0];
				AutoAct.AutoexploreLastCourse = playerCell.GetGeneralCourseFrom(Cell);
				return true;
			}
			return false;
		}

		// Token: 0x06006BE4 RID: 27620 RVA: 0x0022CD7C File Offset: 0x0022AF7C
		public static bool IsAutoexploreObject(GameObject Object)
		{
			return Object.IsValid() && Object.CurrentZone == AutoAct.AutoexploreZone && (!Object.CurrentCell.IsSolidFor(The.Player, false) || Object.CanInteractInCellWithSolid(The.Player)) && ((Options.AutoexploreChests && Object.ShouldAutoexploreAsChest()) || AutoexploreObjectEvent.Check(The.Player, Object, AutoAct.Setting, AutoAct.Action));
		}

		// Token: 0x06006BE5 RID: 27621 RVA: 0x0022CDE8 File Offset: 0x0022AFE8
		public static bool IsAutoexploreCell(Cell Cell)
		{
			return Cell.ParentZone == AutoAct.AutoexploreZone && !Cell.IsExplored();
		}

		// Token: 0x06006BE6 RID: 27622 RVA: 0x0022CE04 File Offset: 0x0022B004
		public static bool TryToMove(GameObject Actor, Cell FromCell, ref GameObject LastDoor, Cell ToCell = null, string Direction = null, bool AllowDigging = true, bool OpenDoors = true, bool Peaceful = true, bool PostMoveHostileCheck = true, bool PostMoveSidebarCheck = true)
		{
			GameObject gameObject = LastDoor;
			LastDoor = null;
			if (FromCell == null)
			{
				AutoAct.Interrupt("your spacetime context is incoherent", null, null, false);
				return false;
			}
			if (ToCell == null && !Direction.IsNullOrEmpty())
			{
				ToCell = FromCell.GetCellFromDirection(Direction, true);
			}
			if (ToCell == null)
			{
				AutoAct.Interrupt("you can go no further", null, null, false);
				return false;
			}
			if (!ToCell.IsAdjacentTo(FromCell, true))
			{
				AutoAct.Interrupt("something is wrong with the spacetime connectivity of your environment", null, null, false);
				return false;
			}
			if (Direction.IsNullOrEmpty())
			{
				Direction = FromCell.GetDirectionFromCell(ToCell, false);
			}
			if (Direction.IsNullOrEmpty() || Direction == "." || Direction == "?")
			{
				AutoAct.Interrupt("the direction you seem to be trying to go makes no sense", null, null, false);
				return false;
			}
			if (OpenDoors)
			{
				int i = 0;
				int count = ToCell.Objects.Count;
				while (i < count)
				{
					GameObject gameObject2 = ToCell.Objects[i];
					Door door;
					if (gameObject2.TryGetPart<Door>(out door) && !door.Open && gameObject2.PhaseMatches(Actor))
					{
						if (gameObject2 == gameObject)
						{
							AutoAct.Interrupt("you cannot keep " + gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " open", null, gameObject2, false);
							return false;
						}
						LastDoor = gameObject2;
						Event @event = Event.New("Open", 0, 0, 0);
						@event.SetParameter("Actor", Actor);
						@event.SetParameter("Opener", Actor);
						@event.SetFlag("UsePopupsForFailures", Actor.IsPlayer());
						@event.SetFlag("FromMove", true);
						if (door.FireEvent(@event))
						{
							AutoAct.Digging = 0;
							goto IL_0368;
						}
						if (!gameObject2.IsCreature)
						{
							AutoAct.Interrupt("you cannot open " + gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false), null, gameObject2, false);
							return false;
						}
					}
					i++;
				}
			}
			if (AllowDigging && ToCell.IsSolidFor(Actor, false) && Actor.PathAsBurrower)
			{
				if (AutoAct.Digging > 1000)
				{
					AutoAct.Interrupt("you cannot seem to make any progress", ToCell, null, false);
					return false;
				}
				int num = 0;
				int j = 0;
				int count2 = ToCell.Objects.Count;
				while (j < count2)
				{
					num += ToCell.Objects[j].hitpoints;
					j++;
				}
				if (!Actor.AttackDirection(Direction, true))
				{
					AutoAct.Interrupt(null, ToCell, null, true);
					return false;
				}
				int num2 = 0;
				int k = 0;
				int count3 = ToCell.Objects.Count;
				while (k < count3)
				{
					num2 += ToCell.Objects[k].hitpoints;
					k++;
				}
				if (num2 < num)
				{
					AutoAct.Digging = 1;
				}
				else
				{
					AutoAct.Digging++;
				}
			}
			else
			{
				GameObject gameObject3;
				if (!Actor.Move(Direction, out gameObject3, false, false, false, false, true, true, null, null, false, null, null, null, Peaceful, false, null, null, 0))
				{
					if (gameObject3 != null)
					{
						AutoAct.Interrupt(gameObject3.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " in the way", null, gameObject3, false);
					}
					else
					{
						AutoAct.Interrupt(null, ToCell, null, false);
					}
					return false;
				}
				if (Actor.CurrentCell == FromCell)
				{
					AutoAct.Interrupt("you tried to move but stayed in the same place", FromCell, null, false);
					return false;
				}
				AutoAct.Digging = 0;
			}
			IL_0368:
			if (PostMoveHostileCheck)
			{
				AutoAct.CheckHostileInterrupt();
			}
			if (PostMoveSidebarCheck && Actor.IsPlayer())
			{
				Cell currentCell = Actor.CurrentCell;
				if (currentCell != null && currentCell != FromCell)
				{
					if (currentCell.X > 42 && Sidebar.State == "right")
					{
						Sidebar.SetSidebarState("left");
					}
					else if (currentCell.X < 38 && Sidebar.State == "left")
					{
						Sidebar.SetSidebarState("right");
					}
				}
			}
			return true;
		}

		// Token: 0x06006BE7 RID: 27623 RVA: 0x0022D1F0 File Offset: 0x0022B3F0
		public static bool TryToMove(GameObject Actor, Cell FromCell, Cell ToCell = null, string Direction = null, bool AllowDigging = true, bool OpenDoors = true, bool Peaceful = true, bool PostMoveHostileCheck = true, bool PostMoveSidebarCheck = true)
		{
			GameObject gameObject = null;
			return AutoAct.TryToMove(Actor, FromCell, ref gameObject, ToCell, Direction, AllowDigging, OpenDoors, Peaceful, PostMoveHostileCheck, PostMoveSidebarCheck);
		}

		// Token: 0x06006BE8 RID: 27624 RVA: 0x0022D214 File Offset: 0x0022B414
		public static string AutoexploreActionProperty(string Command)
		{
			return "AutoexploreAction" + (Command.IsNullOrEmpty() ? "_" : Command);
		}

		// Token: 0x06006BE9 RID: 27625 RVA: 0x0022D230 File Offset: 0x0022B430
		public static int GetAutoexploreActionProperty(GameObject Object, string Command)
		{
			return Object.GetIntProperty(AutoAct.AutoexploreActionProperty(Command), 0);
		}

		// Token: 0x06006BEA RID: 27626 RVA: 0x0022D23F File Offset: 0x0022B43F
		public static void SetAutoexploreActionProperty(GameObject Object, string Command, int Value)
		{
			Object.SetIntProperty(AutoAct.AutoexploreActionProperty(Command), Value, false);
		}

		// Token: 0x06006BEB RID: 27627 RVA: 0x0022D250 File Offset: 0x0022B450
		public static string AutoexploreSuppressionProperty()
		{
			return "AutoexploreSuppression";
		}

		// Token: 0x06006BEC RID: 27628 RVA: 0x0022D257 File Offset: 0x0022B457
		public static bool AutoexploreSuppressed(GameObject Object)
		{
			return Object.GetIntProperty(AutoAct.AutoexploreSuppressionProperty(), 0) > 0;
		}

		// Token: 0x06006BED RID: 27629 RVA: 0x0022D268 File Offset: 0x0022B468
		public static void SetAutoexploreSuppression(GameObject Object, bool Flag)
		{
			Object.SetIntProperty(AutoAct.AutoexploreSuppressionProperty(), Flag ? 1 : 0, true);
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x0022D280 File Offset: 0x0022B480
		public static void ResetZoneAutoexploreAction(string Command)
		{
			Zone parentZone = The.PlayerCell.ParentZone;
			string prop = AutoAct.AutoexploreActionProperty(Command);
			parentZone.ForeachObjectWithTagOrProperty(prop, delegate(GameObject obj)
			{
				AutoAct.SetAutoexploreActionProperty(obj, prop, 0);
			});
			AutoAct.ResetZoneAutoexploreSupression();
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x0022D2C8 File Offset: 0x0022B4C8
		public static void ResetZoneAutoexploreSupression()
		{
			Zone parentZone = The.PlayerCell.ParentZone;
			string prop = AutoAct.AutoexploreSuppressionProperty();
			parentZone.ForeachObjectWithTagOrProperty(prop, delegate(GameObject obj)
			{
				AutoAct.SetAutoexploreActionProperty(obj, prop, 0);
			});
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x0022D307 File Offset: 0x0022B507
		[WishCommand("resetauto", null)]
		public static bool ResetAutoexploreProperties()
		{
			The.PlayerCell.ParentZone.ForeachObject(delegate(GameObject obj)
			{
				AutoAct.PropsToRemove.Clear();
				foreach (string text in obj.IntProperty.Keys)
				{
					if (text.StartsWith("Autoexplore"))
					{
						AutoAct.PropsToRemove.Add(text);
					}
				}
				if (AutoAct.PropsToRemove.Count > 0)
				{
					string text2 = AutoAct.PropsToRemove.Aggregate(null, delegate(string a, string b)
					{
						if (a != null)
						{
							return a + ", " + b;
						}
						return b;
					});
					MessageQueue.AddPlayerMessage("Resetting " + text2 + " on " + obj.DebugName, null, true);
					obj.IntProperty.RemoveAll<string, int>(AutoAct.PropsToRemove);
					AutoAct.PropsToRemove.Clear();
				}
			});
			return true;
		}

		// Token: 0x0400313D RID: 12605
		public const string OngoingActionSetting = "o";

		// Token: 0x0400313E RID: 12606
		public static string ResumeSetting = "";

		// Token: 0x0400313F RID: 12607
		public static int Digging = 0;

		// Token: 0x04003140 RID: 12608
		public static int Count;

		// Token: 0x04003141 RID: 12609
		public static OngoingAction _Action = null;

		// Token: 0x04003142 RID: 12610
		public static OngoingAction _ResumeAction = null;

		// Token: 0x04003143 RID: 12611
		public static bool Attacking;

		// Token: 0x04003144 RID: 12612
		private static FindPath TargetPath = new FindPath();

		// Token: 0x04003145 RID: 12613
		private static FindPath TryPath = new FindPath();

		// Token: 0x04003146 RID: 12614
		private static int EdgeLastAct;

		// Token: 0x04003147 RID: 12615
		private static Rack<GameObject> AutoexploreObjects = new Rack<GameObject>();

		// Token: 0x04003148 RID: 12616
		private static Rack<Cell> AutoexploreCells = new Rack<Cell>();

		// Token: 0x04003149 RID: 12617
		private static Zone AutoexploreZone;

		// Token: 0x0400314A RID: 12618
		private static Cell AutoexploreLastTarget;

		// Token: 0x0400314B RID: 12619
		private static FindPath AutoexplorePath = new FindPath();

		// Token: 0x0400314C RID: 12620
		private static int AutoexploreLastAct;

		// Token: 0x0400314D RID: 12621
		private static int AutoexploreLastCourse;

		// Token: 0x0400314E RID: 12622
		private static List<string> PropsToRemove = new List<string>();
	}
}
