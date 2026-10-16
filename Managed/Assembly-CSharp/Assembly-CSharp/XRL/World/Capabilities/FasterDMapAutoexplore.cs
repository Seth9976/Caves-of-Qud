using System;
using System.Runtime.CompilerServices;
using Genkit;
using XRL.Collections;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI.Pathfinding;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E7 RID: 2535
	[HasWishCommand]
	public static class FasterDMapAutoexplore
	{
		// Token: 0x06006C20 RID: 27680 RVA: 0x0022F1B4 File Offset: 0x0022D3B4
		private static Rack<Location2D>[] InitRings()
		{
			Rack<Location2D>[] array = new Rack<Location2D>[101];
			for (int i = 0; i < 101; i++)
			{
				array[i] = new Rack<Location2D>(64);
			}
			return array;
		}

		// Token: 0x06006C21 RID: 27681 RVA: 0x0022F1E4 File Offset: 0x0022D3E4
		public static bool TryGetNextTarget(Zone Zone, Cell From, GameObject Looker, int Nav, int[,] AutoexploreObjectMarks, int AutoexploreObjectMarkerValue, int Course, bool GoalAlwaysCostsOne, FindPath Path, out int Cost, out bool Blackout)
		{
			FasterDMapAutoexplore.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.AutoexploreObjectMarks = AutoexploreObjectMarks;
			CS$<>8__locals1.AutoexploreObjectMarkerValue = AutoexploreObjectMarkerValue;
			Cost = int.MaxValue;
			Blackout = false;
			if (From == null || From.ParentZone != Zone)
			{
				return false;
			}
			if (FasterDMapAutoexplore.SearchRing == null)
			{
				FasterDMapAutoexplore.SearchRing = FasterDMapAutoexplore.InitRings();
			}
			for (int i = 0; i < 101; i++)
			{
				FasterDMapAutoexplore.SearchRing[i].Clear();
			}
			long num = (FasterDMapAutoexplore.CurrentPass += 1L);
			if (FasterDMapAutoexplore.CurrentPass >= 9223372036854775806L)
			{
				FasterDMapAutoexplore.CurrentPass = 0L;
			}
			long[,] passMap = FasterDMapAutoexplore.PassMap;
			int[,] searchCost = FasterDMapAutoexplore.SearchCost;
			Location2D[,] searchCellParent = FasterDMapAutoexplore.SearchCellParent;
			Cell[][] map = Zone.Map;
			CS$<>8__locals1.explored = Zone.ExploredMap;
			CS$<>8__locals1.width = Zone.Width;
			Location2D location = From.Location;
			passMap[location.X, location.Y] = num;
			searchCost[location.X, location.Y] = 0;
			searchCellParent[location.X, location.Y] = null;
			FasterDMapAutoexplore.NavWeightCache[location.X, location.Y] = From.NavigationWeight(Looker, Nav);
			FasterDMapAutoexplore.SearchRing[0].Add(location);
			Location2D location2D = null;
			int num2 = int.MaxValue;
			int num3 = 0;
			int j = 0;
			while (j < 101)
			{
				Rack<Location2D> rack = FasterDMapAutoexplore.SearchRing[num3 % 101];
				if (rack.Count == 0)
				{
					j++;
				}
				else
				{
					j = 0;
					Location2D[] array = rack.GetArray();
					int k = 0;
					int count = rack.Count;
					while (k < count)
					{
						Location2D location2D2 = array[k];
						if (location2D2 != location && FasterDMapAutoexplore.<TryGetNextTarget>g__IsGoal|10_0(location2D2.X, location2D2.Y, ref CS$<>8__locals1))
						{
							int num4 = FasterDMapAutoexplore.<TryGetNextTarget>g__TieScore|10_1(location2D2);
							if (num4 < num2)
							{
								num2 = num4;
								location2D = location2D2;
							}
						}
						k++;
					}
					if (location2D != null)
					{
						Cost = num3;
						break;
					}
					int l = 0;
					int count2 = rack.Count;
					while (l < count2)
					{
						Location2D location2D3 = array[l];
						int m = 0;
						int num5 = Location2D.Directions.Length;
						while (m < num5)
						{
							Location2D location2D4 = location2D3.FromDirection(Location2D.Directions[m]);
							if (!(location2D4 == null) && location2D4.X < Zone.Width && location2D4.Y < Zone.Height)
							{
								int x = location2D4.X;
								int y = location2D4.Y;
								byte b = ((location2D3 == location) ? ((byte)m) : FasterDMapAutoexplore.SearchFirstStep[location2D3.X, location2D3.Y]);
								if (passMap[x, y] == num)
								{
									int num6 = FasterDMapAutoexplore.NavWeightCache[x, y];
									int num7 = ((num6 >= 95 || (GoalAlwaysCostsOne && FasterDMapAutoexplore.<TryGetNextTarget>g__IsGoal|10_0(x, y, ref CS$<>8__locals1))) ? 1 : Math.Max(num6, 1));
									if (searchCost[x, y] != 2147483647 && searchCost[x, y] == num3 + num7 && b < FasterDMapAutoexplore.SearchFirstStep[x, y])
									{
										FasterDMapAutoexplore.SearchFirstStep[x, y] = b;
										searchCellParent[x, y] = location2D3;
									}
								}
								else
								{
									passMap[x, y] = num;
									FasterDMapAutoexplore.SearchFirstStep[x, y] = b;
									Cell cell = map[x][y];
									int num8 = (AutoexploreGuard.IsBlocked(x, y) ? 101 : cell.NavigationWeight(Looker, Nav));
									FasterDMapAutoexplore.NavWeightCache[x, y] = num8;
									if (num8 >= 95)
									{
										searchCost[x, y] = int.MaxValue;
										if (!CS$<>8__locals1.explored[x + y * CS$<>8__locals1.width] && cell.IsBlackedOut())
										{
											Blackout = true;
										}
										else if (FasterDMapAutoexplore.<TryGetNextTarget>g__IsGoal|10_0(x, y, ref CS$<>8__locals1))
										{
											int num9 = num3 + 1;
											searchCost[x, y] = num9;
											searchCellParent[x, y] = location2D3;
											FasterDMapAutoexplore.SearchRing[num9 % 101].Add(location2D4);
										}
									}
									else
									{
										int num10 = Math.Max(num8, 1);
										if (GoalAlwaysCostsOne && FasterDMapAutoexplore.<TryGetNextTarget>g__IsGoal|10_0(x, y, ref CS$<>8__locals1))
										{
											num10 = 1;
										}
										int num11 = num3 + num10;
										searchCost[x, y] = num11;
										searchCellParent[x, y] = location2D3;
										FasterDMapAutoexplore.SearchRing[num11 % 101].Add(location2D4);
									}
								}
							}
							m++;
						}
						l++;
					}
					rack.Clear();
				}
				num3++;
			}
			if (location2D == null)
			{
				return false;
			}
			Path.Clear();
			Location2D location2D5 = location2D;
			while (location2D5 != null)
			{
				Path.Steps.Add(map[location2D5.X][location2D5.Y]);
				location2D5 = searchCellParent[location2D5.X, location2D5.Y];
			}
			Path.Steps.Reverse();
			int n = 0;
			int count3 = Path.Steps.Count;
			while (n < count3)
			{
				Cell cell2 = Path.Steps[n];
				Path.Weights.Add(FasterDMapAutoexplore.NavWeightCache[cell2.X, cell2.Y]);
				if (n + 1 < count3)
				{
					Path.Directions.Add(cell2.GetDirectionFromCell(Path.Steps[n + 1], false));
				}
				n++;
			}
			Path.Found = Path.Steps.Count > 1;
			return Path.Found;
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06006C22 RID: 27682 RVA: 0x0022F75F File Offset: 0x0022D95F
		public static bool DebugDraw
		{
			get
			{
				return Options.DrawFloodExplore;
			}
		}

		// Token: 0x06006C23 RID: 27683 RVA: 0x0022F766 File Offset: 0x0022D966
		public static void FindAutoexploreStep(out string Step, out bool Blackout)
		{
			FasterDMapAutoexplore.FindAutoexploreStep(false, out Step, out Blackout);
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x0022F770 File Offset: 0x0022D970
		private static void MarkAutoexploreObjects(Zone zone, ref bool Blackout)
		{
			FasterDMapAutoexplore.AutoexploreObjectMarkerValue++;
			GameObject[] array = FasterDMapAutoexplore.AutoexploreObjects.GetArray();
			for (int i = FasterDMapAutoexplore.AutoexploreObjects.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = array[i];
				if (!FasterDMapAutoexplore.IsAutoexploreObject(gameObject, zone))
				{
					FasterDMapAutoexplore.AutoexploreObjects.RemoveAt(i);
				}
				else
				{
					Cell currentCell = gameObject.CurrentCell;
					if (currentCell.IsBlackedOut())
					{
						Blackout = true;
					}
					else if (currentCell.IsExplored() && !AutoexploreGuard.IsDeadCell(currentCell.X, currentCell.Y))
					{
						FasterDMapAutoexplore.AutoexploreObjectMarks[currentCell.X, currentCell.Y] = FasterDMapAutoexplore.AutoexploreObjectMarkerValue;
					}
				}
			}
		}

		// Token: 0x06006C25 RID: 27685 RVA: 0x0022F810 File Offset: 0x0022DA10
		private static bool TryDigToNearestObject(Cell Current, out string Step)
		{
			Step = ".";
			Cell cell = null;
			int num = int.MaxValue;
			GameObject[] array = FasterDMapAutoexplore.AutoexploreObjects.GetArray();
			int i = 0;
			int count = FasterDMapAutoexplore.AutoexploreObjects.Count;
			while (i < count)
			{
				Cell currentCell = array[i].CurrentCell;
				if (currentCell != null && FasterDMapAutoexplore.AutoexploreObjectMarks[currentCell.X, currentCell.Y] == FasterDMapAutoexplore.AutoexploreObjectMarkerValue)
				{
					int num2 = Current.DistanceTo(currentCell.X, currentCell.Y);
					if (num2 < num)
					{
						num = num2;
						cell = currentCell;
					}
				}
				i++;
			}
			if (cell == null)
			{
				return false;
			}
			FasterDMapAutoexplore.AutoexplorePath.PerformPathfind(Current.ParentZone, Current.X, Current.Y, cell.ParentZone, cell.X, cell.Y, false, The.Player, true, false, false, null, 95, false, true, false, false, false, false, true);
			if (!FasterDMapAutoexplore.AutoexplorePath.Found)
			{
				AutoexploreGuard.MarkCellAsDead(cell);
				return false;
			}
			FasterDMapAutoexplore.AutoexploreLastDigging = true;
			FasterDMapAutoexplore.AutoexploreLastTarget = cell;
			FasterDMapAutoexplore.AutoexploreLastCourse = Current.GetGeneralCourseFrom(cell);
			Step = FasterDMapAutoexplore.AutoexplorePath.Directions[0];
			return true;
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x0022F924 File Offset: 0x0022DB24
		private static bool TryFollowCurrentPath(Cell Current, out string StepDir, out int StepIndex)
		{
			StepDir = ".";
			StepIndex = -1;
			FindPath autoexplorePath = FasterDMapAutoexplore.AutoexplorePath;
			if (!autoexplorePath.Found)
			{
				return false;
			}
			Cell autoexploreLastTarget = FasterDMapAutoexplore.AutoexploreLastTarget;
			if (autoexploreLastTarget == null || !FasterDMapAutoexplore.IsAutoexploreCell(autoexploreLastTarget))
			{
				return false;
			}
			int num = autoexplorePath.Steps.IndexOf(Current);
			if (num < 0 || num + 1 >= autoexplorePath.Steps.Count)
			{
				return false;
			}
			Cell cell = autoexplorePath.Steps[num + 1];
			if (AutoexploreGuard.IsBlocked(cell.X, cell.Y))
			{
				return false;
			}
			int num2 = Zone.CalculateNav(The.Player, true, false, false, false, false, FasterDMapAutoexplore.AutoexploreLastDigging);
			if (cell.NavigationWeight(The.Player, num2) != autoexplorePath.Weights[num + 1])
			{
				return false;
			}
			StepDir = autoexplorePath.Directions[num];
			StepIndex = num;
			return true;
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x0022F9EC File Offset: 0x0022DBEC
		public static bool IsAutoexploreCell(Cell C)
		{
			if (C == null || C.ParentZone != FasterDMapAutoexplore.AutoexploreZone)
			{
				return false;
			}
			if (AutoexploreGuard.IsDeadCell(C.X, C.Y))
			{
				return false;
			}
			if (C.IsExplored())
			{
				return C.HasObject((GameObject o) => FasterDMapAutoexplore.IsAutoexploreObject(o, FasterDMapAutoexplore.AutoexploreZone));
			}
			return true;
		}

		// Token: 0x06006C28 RID: 27688 RVA: 0x0022FA50 File Offset: 0x0022DC50
		public static bool IsAutoexploreObject(GameObject Object, Zone Zone)
		{
			if (Zone == null || !Object.IsValid())
			{
				return false;
			}
			Cell currentCell = Object.CurrentCell;
			return currentCell != null && currentCell.ParentZone == Zone && (!currentCell.IsSolidFor(The.Player, false) || Object.CanInteractInCellWithSolid(The.Player)) && ((Options.AutoexploreChests && Object.ShouldAutoexploreAsChest()) || AutoexploreObjectEvent.Check(The.Player, Object, AutoAct.Setting, AutoAct.Action));
		}

		// Token: 0x06006C29 RID: 27689 RVA: 0x0022FAC4 File Offset: 0x0022DCC4
		public static void FindAutoexploreStep(bool Force, out string StepDir, out bool Blackout)
		{
			Blackout = false;
			StepDir = ".";
			Cell playerCell = The.PlayerCell;
			Zone zone = ((playerCell != null) ? playerCell.ParentZone : null);
			if (zone == null)
			{
				return;
			}
			bool flag = FasterDMapAutoexplore.AutoexploreZone != zone || FasterDMapAutoexplore.AutoexploreLastAct != AutoAct.Count;
			if (Force || flag)
			{
				FasterDMapAutoexplore.AutoexploreObjectMarkerValue++;
				FasterDMapAutoexplore.AutoexploreZone = zone;
				FasterDMapAutoexplore.AutoexploreLastAct = AutoAct.Count;
				FasterDMapAutoexplore.AutoexploreObjects.Clear();
				FasterDMapAutoexplore.AutoexploreCells.Clear();
				FasterDMapAutoexplore.AutoexplorePath.Clear();
				FasterDMapAutoexplore.AutoexploreLastTarget = null;
				FasterDMapAutoexplore.AutoexploreLastDigging = false;
				FasterDMapAutoexplore.AutoexploreAdjacentTurns = 0;
				FasterDMapAutoexplore.AutoexploreAdjacentTarget = null;
				FasterDMapAutoexplore.AutoexploreAdjacentTarget = null;
				zone.FloodAutoexplore(FasterDMapAutoexplore.AutoexploreCells, FasterDMapAutoexplore.AutoexploreObjects, FasterDMapAutoexplore.AutoexploreLastCourse);
				if (flag)
				{
					AutoexploreGuard.Reset(zone);
				}
			}
			int lastProgress = AutoexploreGuard.LastProgress;
			string text = AutoexploreGuard.DoSanityCheck(playerCell, zone);
			if (text != null)
			{
				AutoAct.Interrupt(text, null, null, false);
				return;
			}
			if (lastProgress != AutoexploreGuard.LastProgress)
			{
				AutoexploreGuard.StepsSinceLastPlan = int.MaxValue;
			}
			string text2;
			int num;
			bool flag2 = FasterDMapAutoexplore.TryFollowCurrentPath(playerCell, out text2, out num);
			if (flag2 && AutoexploreGuard.StepsSinceLastPlan < 2147483647)
			{
				AutoexploreGuard.StepsSinceLastPlan++;
				StepDir = text2;
				AutoexploreGuard.RecordStepHistory(playerCell, StepDir);
				return;
			}
			Cell autoexploreLastTarget = FasterDMapAutoexplore.AutoexploreLastTarget;
			if (autoexploreLastTarget != null && playerCell.DistanceTo(autoexploreLastTarget.X, autoexploreLastTarget.Y) <= 1 && FasterDMapAutoexplore.IsAutoexploreCell(autoexploreLastTarget))
			{
				if (autoexploreLastTarget != FasterDMapAutoexplore.AutoexploreAdjacentTarget)
				{
					FasterDMapAutoexplore.AutoexploreAdjacentTarget = autoexploreLastTarget;
					FasterDMapAutoexplore.AutoexploreAdjacentTurns = 0;
				}
				if (++FasterDMapAutoexplore.AutoexploreAdjacentTurns >= 2)
				{
					AutoexploreGuard.MarkCellAsDead(autoexploreLastTarget);
				}
			}
			else
			{
				FasterDMapAutoexplore.AutoexploreAdjacentTarget = null;
				FasterDMapAutoexplore.AutoexploreAdjacentTurns = 0;
			}
			FasterDMapAutoexplore.MarkAutoexploreObjects(zone, ref Blackout);
			int num2 = Zone.CalculateNav(The.Player, true, false, false, false, false, false);
			int num3;
			bool flag4;
			bool flag3 = FasterDMapAutoexplore.TryGetNextTarget(zone, playerCell, The.Player, num2, FasterDMapAutoexplore.AutoexploreObjectMarks, FasterDMapAutoexplore.AutoexploreObjectMarkerValue, FasterDMapAutoexplore.AutoexploreLastCourse, FasterDMapAutoexplore.GoalAlwaysCostsOne, FasterDMapAutoexplore.NewAutoexplorePath, out num3, out flag4);
			Blackout = Blackout || flag4;
			if (flag3)
			{
				Cell cell = FasterDMapAutoexplore.NewAutoexplorePath.Steps[FasterDMapAutoexplore.NewAutoexplorePath.Steps.Count - 1];
				if (flag2 && cell != FasterDMapAutoexplore.AutoexploreLastTarget && !AutoexploreGuard.ShouldSwitchTarget(num3, AutoexploreGuard.CostRemaining(FasterDMapAutoexplore.AutoexplorePath, num, FasterDMapAutoexplore.GoalAlwaysCostsOne)))
				{
					AutoexploreGuard.StepsSinceLastPlan = 1;
					StepDir = text2;
					AutoexploreGuard.RecordStepHistory(playerCell, StepDir);
					return;
				}
				FindPath autoexplorePath = FasterDMapAutoexplore.AutoexplorePath;
				FasterDMapAutoexplore.AutoexplorePath = FasterDMapAutoexplore.NewAutoexplorePath;
				FasterDMapAutoexplore.NewAutoexplorePath = autoexplorePath;
				FasterDMapAutoexplore.AutoexploreLastTarget = cell;
				FasterDMapAutoexplore.AutoexploreLastCourse = playerCell.GetGeneralCourseFrom(cell);
				FasterDMapAutoexplore.AutoexploreLastDigging = false;
				AutoexploreGuard.StepsSinceLastPlan = 1;
				StepDir = FasterDMapAutoexplore.AutoexplorePath.Directions[0];
				AutoexploreGuard.RecordStepHistory(playerCell, StepDir);
				return;
			}
			else
			{
				if (FasterDMapAutoexplore.TryDigToNearestObject(playerCell, out StepDir))
				{
					AutoexploreGuard.StepsSinceLastPlan = 1;
					AutoexploreGuard.RecordStepHistory(playerCell, StepDir);
					return;
				}
				FasterDMapAutoexplore.AutoexplorePath.Clear();
				FasterDMapAutoexplore.AutoexploreLastTarget = null;
				FasterDMapAutoexplore.AutoexploreLastDigging = false;
				if (!Force)
				{
					FasterDMapAutoexplore.FindAutoexploreStep(true, out StepDir, out Blackout);
				}
				return;
			}
		}

		// Token: 0x06006C2B RID: 27691 RVA: 0x0022FE1E File Offset: 0x0022E01E
		[CompilerGenerated]
		internal static bool <TryGetNextTarget>g__IsGoal|10_0(int x, int y, ref FasterDMapAutoexplore.<>c__DisplayClass10_0 A_2)
		{
			return !AutoexploreGuard.IsDeadCell(x, y) && (!A_2.explored[x + y * A_2.width] || A_2.AutoexploreObjectMarks[x, y] == A_2.AutoexploreObjectMarkerValue);
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x0022FE55 File Offset: 0x0022E055
		[CompilerGenerated]
		internal static int <TryGetNextTarget>g__TieScore|10_1(Location2D goal)
		{
			return (int)FasterDMapAutoexplore.SearchFirstStep[goal.X, goal.Y];
		}

		// Token: 0x04003182 RID: 12674
		private const int MAX_COST = 100;

		// Token: 0x04003183 RID: 12675
		private const int MAX_RINGS = 101;

		// Token: 0x04003184 RID: 12676
		[NonSerialized]
		public static long CurrentPass = 0L;

		// Token: 0x04003185 RID: 12677
		[NonSerialized]
		private static readonly long[,] PassMap = new long[80, 25];

		// Token: 0x04003186 RID: 12678
		[NonSerialized]
		private static readonly int[,] SearchCost = new int[80, 25];

		// Token: 0x04003187 RID: 12679
		[NonSerialized]
		private static readonly int[,] NavWeightCache = new int[80, 25];

		// Token: 0x04003188 RID: 12680
		[NonSerialized]
		private static readonly byte[,] SearchFirstStep = new byte[80, 25];

		// Token: 0x04003189 RID: 12681
		[NonSerialized]
		private static readonly Location2D[,] SearchCellParent = new Location2D[80, 25];

		// Token: 0x0400318A RID: 12682
		[NonSerialized]
		private static Rack<Location2D>[] SearchRing;

		// Token: 0x0400318B RID: 12683
		private const int REPLAN_STEPS = 2147483647;

		// Token: 0x0400318C RID: 12684
		public static bool GoalAlwaysCostsOne = true;

		// Token: 0x0400318D RID: 12685
		private static readonly Rack<GameObject> AutoexploreObjects = new Rack<GameObject>();

		// Token: 0x0400318E RID: 12686
		private static readonly Rack<Cell> AutoexploreCells = new Rack<Cell>();

		// Token: 0x0400318F RID: 12687
		private static readonly int[,] AutoexploreObjectMarks = new int[80, 25];

		// Token: 0x04003190 RID: 12688
		private static int AutoexploreObjectMarkerValue;

		// Token: 0x04003191 RID: 12689
		private static Zone AutoexploreZone;

		// Token: 0x04003192 RID: 12690
		private static bool AutoexploreLastDigging;

		// Token: 0x04003193 RID: 12691
		private static Cell AutoexploreLastTarget;

		// Token: 0x04003194 RID: 12692
		private static int AutoexploreLastAct;

		// Token: 0x04003195 RID: 12693
		private static int AutoexploreLastCourse;

		// Token: 0x04003196 RID: 12694
		private static int AutoexploreAdjacentTurns;

		// Token: 0x04003197 RID: 12695
		private static Cell AutoexploreAdjacentTarget;

		// Token: 0x04003198 RID: 12696
		private static FindPath AutoexplorePath = new FindPath();

		// Token: 0x04003199 RID: 12697
		private static FindPath NewAutoexplorePath = new FindPath();
	}
}
