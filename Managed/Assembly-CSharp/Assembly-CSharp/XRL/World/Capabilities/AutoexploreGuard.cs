using System;
using Genkit;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E6 RID: 2534
	public static class AutoexploreGuard
	{
		// Token: 0x06006C12 RID: 27666 RVA: 0x0022EE54 File Offset: 0x0022D054
		public static void Reset(Zone Z)
		{
			AutoexploreGuard.CurrentSessionId++;
			if (AutoexploreGuard.CurrentSessionId >= 2147483646)
			{
				AutoexploreGuard.CurrentSessionId = 0;
			}
			AutoexploreGuard.HistoryHead = (AutoexploreGuard.HistoryCount = 0);
			AutoexploreGuard.LastCell = null;
			AutoexploreGuard.LastStep = ".";
			AutoexploreGuard.StepsWithoutMoving = 0;
			AutoexploreGuard.LastProgress = AutoexploreGuard.Progress(Z);
			AutoexploreGuard.StepsWithoutProgress = 0;
			AutoexploreGuard.StepsSinceLastPlan = 0;
		}

		// Token: 0x06006C13 RID: 27667 RVA: 0x0022EEB8 File Offset: 0x0022D0B8
		public static int Progress(Zone Z)
		{
			int num = 0;
			bool[] exploredMap = Z.ExploredMap;
			int i = 0;
			int num2 = exploredMap.Length;
			while (i < num2)
			{
				if (exploredMap[i])
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06006C14 RID: 27668 RVA: 0x0022EEE8 File Offset: 0x0022D0E8
		public static int NoMoveStrikes
		{
			get
			{
				return AutoexploreGuard.StepsWithoutMoving;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06006C15 RID: 27669 RVA: 0x0022EEEF File Offset: 0x0022D0EF
		public static int StepsWithoutProgressCount
		{
			get
			{
				return AutoexploreGuard.StepsWithoutProgress;
			}
		}

		// Token: 0x06006C16 RID: 27670 RVA: 0x0022EEF6 File Offset: 0x0022D0F6
		public static bool IsDeadCell(int X, int Y)
		{
			return AutoexploreGuard.DeadGoal[X, Y] == AutoexploreGuard.CurrentSessionId;
		}

		// Token: 0x06006C17 RID: 27671 RVA: 0x0022EF0B File Offset: 0x0022D10B
		public static bool IsBlocked(int X, int Y)
		{
			return AutoexploreGuard.Blocked[X, Y] == AutoexploreGuard.CurrentSessionId;
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x0022EF20 File Offset: 0x0022D120
		public static void MarkCellAsDead(Cell C)
		{
			AutoexploreGuard.DeadGoal[C.X, C.Y] = AutoexploreGuard.CurrentSessionId;
		}

		// Token: 0x06006C19 RID: 27673 RVA: 0x0022EF3D File Offset: 0x0022D13D
		public static void MarkCellAsBlocked(Cell C)
		{
			AutoexploreGuard.Blocked[C.X, C.Y] = AutoexploreGuard.CurrentSessionId;
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x0022EF5C File Offset: 0x0022D15C
		public static string DoSanityCheck(Cell Current, Zone zone)
		{
			if (Current == null || Current.ParentZone != zone)
			{
				return null;
			}
			int num = AutoexploreGuard.Progress(zone);
			if (num != AutoexploreGuard.LastProgress)
			{
				AutoexploreGuard.LastProgress = num;
				AutoexploreGuard.StepsWithoutProgress = 0;
			}
			else
			{
				AutoexploreGuard.StepsWithoutProgress++;
			}
			bool flag = AutoexploreGuard.LastCell != Current;
			if (!flag && AutoexploreGuard.LastStep != "." && AutoAct.Digging == 0)
			{
				AutoexploreGuard.StepsWithoutMoving++;
				if (AutoexploreGuard.StepsWithoutMoving >= 6)
				{
					return "you can't move";
				}
				if (AutoexploreGuard.StepsWithoutMoving >= 2)
				{
					Cell cellFromDirection = Current.GetCellFromDirection(AutoexploreGuard.LastStep, true);
					if (cellFromDirection != null)
					{
						AutoexploreGuard.MarkCellAsBlocked(cellFromDirection);
						AutoexploreGuard.MarkCellAsDead(cellFromDirection);
					}
				}
			}
			else
			{
				AutoexploreGuard.StepsWithoutMoving = 0;
			}
			if (flag)
			{
				int num2 = 0;
				bool flag2 = true;
				for (int i = 0; i < AutoexploreGuard.HistoryCount; i++)
				{
					int num3 = (AutoexploreGuard.HistoryHead - 1 - i + 16) % 16;
					if (AutoexploreGuard.History[num3].X == Current.X && AutoexploreGuard.History[num3].Y == Current.Y)
					{
						num2++;
						if (AutoexploreGuard.HistoryProgress[num3] != num)
						{
							flag2 = false;
						}
					}
				}
				if (num2 >= 2 && flag2)
				{
					return "you are going in circles";
				}
			}
			if (AutoexploreGuard.StepsWithoutProgress > 160 && AutoAct.Digging == 0)
			{
				return "you aren't making progress exploring the area";
			}
			if (flag)
			{
				AutoexploreGuard.History[AutoexploreGuard.HistoryHead] = Current.Location;
				AutoexploreGuard.HistoryProgress[AutoexploreGuard.HistoryHead] = num;
				AutoexploreGuard.HistoryHead = (AutoexploreGuard.HistoryHead + 1) % 16;
				if (AutoexploreGuard.HistoryCount < 16)
				{
					AutoexploreGuard.HistoryCount++;
				}
			}
			return null;
		}

		// Token: 0x06006C1B RID: 27675 RVA: 0x0022F0E7 File Offset: 0x0022D2E7
		public static void RecordStepHistory(Cell Current, string Step)
		{
			AutoexploreGuard.LastCell = Current;
			AutoexploreGuard.LastStep = Step ?? ".";
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x0022F0FE File Offset: 0x0022D2FE
		public static bool ShouldSwitchTarget(int NewCost, int RemainingCost)
		{
			return RemainingCost - NewCost >= 3;
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x0022F109 File Offset: 0x0022D309
		public static bool IsOpposite(int From, int To)
		{
			return From != 0 && To != 0 && (From & To) == 0 && (Courses.Opposite[From] & To) != 0;
		}

		// Token: 0x06006C1E RID: 27678 RVA: 0x0022F128 File Offset: 0x0022D328
		public static int CostRemaining(FindPath Path, int From, bool GoalAlwaysCostsOne = false)
		{
			int num = 0;
			int num2 = Path.Steps.Count - 1;
			for (int i = From + 1; i <= num2; i++)
			{
				num += ((GoalAlwaysCostsOne && i == num2) ? 1 : Math.Max(Path.Weights[i], 1));
			}
			return num;
		}

		// Token: 0x0400316F RID: 12655
		public const int MAX_HISTORY_DEPTH = 16;

		// Token: 0x04003170 RID: 12656
		public const int MAX_CYCLE_VISITS = 2;

		// Token: 0x04003171 RID: 12657
		public const int MAX_STEPS_WITHOUT_PROGRESS = 160;

		// Token: 0x04003172 RID: 12658
		public const int MAX_FAILED_MOVES_BEFORE_MARKING_CELL_BLOCKED = 2;

		// Token: 0x04003173 RID: 12659
		public const int MAX_FAILED_MOVES_BEFORE_STOPPING_AUTOEXPLORE = 6;

		// Token: 0x04003174 RID: 12660
		public const int COST_MARGIN_BEFORE_SWITCHING_TARGETS = 3;

		// Token: 0x04003175 RID: 12661
		private static int CurrentSessionId;

		// Token: 0x04003176 RID: 12662
		private static readonly int[,] DeadGoal = new int[80, 25];

		// Token: 0x04003177 RID: 12663
		private static readonly int[,] Blocked = new int[80, 25];

		// Token: 0x04003178 RID: 12664
		private static readonly Location2D[] History = new Location2D[16];

		// Token: 0x04003179 RID: 12665
		private static readonly int[] HistoryProgress = new int[16];

		// Token: 0x0400317A RID: 12666
		private static int HistoryHead;

		// Token: 0x0400317B RID: 12667
		private static int HistoryCount;

		// Token: 0x0400317C RID: 12668
		private static Cell LastCell;

		// Token: 0x0400317D RID: 12669
		private static string LastStep = ".";

		// Token: 0x0400317E RID: 12670
		private static int StepsWithoutMoving;

		// Token: 0x0400317F RID: 12671
		public static int LastProgress;

		// Token: 0x04003180 RID: 12672
		private static int StepsWithoutProgress;

		// Token: 0x04003181 RID: 12673
		public static int StepsSinceLastPlan;
	}
}
