using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ConsoleLib.Console;
using Genkit;
using Newtonsoft.Json;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Collections;
using XRL.Core;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI.Pathfinding;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.ZoneBuilders;
using XRL.World.ZoneBuilders.Utility;
using XRL.World.ZoneParts;

namespace XRL.World
{
	// Token: 0x0200098C RID: 2444
	[HasWishCommand]
	[Serializable]
	public class Zone
	{
		// Token: 0x06006734 RID: 26420 RVA: 0x00202158 File Offset: 0x00200358
		public void FloodAutoexplore(Rack<Cell> Cells, Rack<GameObject> Objects, int Course = 0)
		{
			Cells.Clear();
			Objects.Clear();
			GameObject player = The.Player;
			Cell cell = player.CurrentCell;
			if (cell == null || cell.ParentZone != this)
			{
				return;
			}
			if (Course == 0)
			{
				Course = Courses.Ordinal.GetRandomElement<int>(null);
			}
			Location2D[,] grid = Location2D.Grid;
			Cell[][] map = this.Map;
			bool[] exploredMap = this.ExploredMap;
			int[,] floodMap = this.FloodMap;
			int num = Zone.FloodValue + 1;
			int x = cell.X;
			int y = cell.Y;
			if (num == 2147483647)
			{
				num = int.MinValue;
			}
			Zone.FloodValue = num;
			floodMap[x, y] = num;
			int num2 = -1;
			int num3 = 0;
			int num4 = 2048;
			int num5 = 2049;
			Location2D[] floodArray = Zone.FloodArray;
			Location2D location2D = (floodArray[num4] = grid[x, y]);
			Location2D location2D2 = location2D;
			int num6 = Zone.CalculateNav(player, true, false, false, false, false, false);
			bool autoexploreChests = Options.AutoexploreChests;
			string setting = AutoAct.Setting;
			OngoingAction action = AutoAct.Action;
			bool drawFloodExplore = Options.DrawFloodExplore;
			if (drawFloodExplore)
			{
				if (Zone.FloodVisited == null)
				{
					Zone.FloodVisited = new Rack<Location2D>(2048);
				}
				Zone.FloodVisited.Clear();
			}
			Pivot[][] toExpandPivot = Courses.ToExpandPivot;
			Zone.Segment[] floodSegments = Zone.FloodSegments;
			for (;;)
			{
				if (num2 >= 0)
				{
					location2D = floodArray[num2--];
					Course = location2D.GetCourseFrom(location2D2);
					location2D2 = location2D;
					floodSegments[num3++] = new Zone.Segment(num4, num5);
					num4 = num5;
				}
				else if (num4 != num5)
				{
					location2D = floodArray[num4++];
				}
				else
				{
					if (num3 <= 0)
					{
						break;
					}
					Zone.Segment segment = floodSegments[--num3];
					num4 = segment.Head;
					num5 = segment.Tail;
					if (num4 == num5)
					{
						continue;
					}
					location2D = floodArray[num4++];
					Course = location2D.Ordinal;
				}
				int i = location2D.X;
				int j = location2D.Y;
				cell = map[i][j];
				if (drawFloodExplore)
				{
					Zone.FloodVisited.Add(location2D);
				}
				GameObject[] array = cell.Objects.GetArray();
				int k = 0;
				int count = cell.Objects.Count;
				while (k < count)
				{
					GameObject gameObject = array[k];
					if ((autoexploreChests && gameObject.ShouldAutoexploreAsChest()) || AutoexploreObjectEvent.Check(player, gameObject, setting, action))
					{
						Objects.Add(gameObject);
					}
					k++;
				}
				if (cell.NavigationWeight(player, num6) < 95)
				{
					int num7 = num5;
					foreach (Pivot pivot in toExpandPivot[Course])
					{
						int num8 = i - pivot.X;
						if (num8 < 80)
						{
							int num9 = j - pivot.Y;
							if (num9 < 25 && floodMap[num8, num9] != num)
							{
								floodMap[num8, num9] = num;
								if (exploredMap[num8 + num9 * 80])
								{
									floodArray[num5++] = grid[num8, num9];
								}
								else
								{
									floodArray[++num2] = grid[num8, num9];
									Cells.Add(map[num8][num9]);
								}
							}
						}
					}
					int m = num7;
					int num10 = num5 - 1;
					while (m < num10)
					{
						Location2D location2D3 = floodArray[m];
						floodArray[m] = floodArray[num10];
						floodArray[num10--] = location2D3;
						m++;
					}
				}
			}
			for (int j = 0; j < 25; j++)
			{
				for (int i = 0; i < 80; i++)
				{
					if (floodMap[i, j] != num)
					{
						cell = map[i][j];
						GameObject[] array3 = cell.Objects.GetArray();
						int n = 0;
						int count2 = cell.Objects.Count;
						while (n < count2)
						{
							GameObject gameObject2 = array3[n];
							if ((autoexploreChests && gameObject2.ShouldAutoexploreAsChest()) || AutoexploreObjectEvent.Check(player, gameObject2, setting, action))
							{
								Objects.Add(gameObject2);
							}
							n++;
						}
					}
				}
			}
			if (drawFloodExplore)
			{
				this.DrawFloodAutoexplore(Cells);
			}
		}

		// Token: 0x06006735 RID: 26421 RVA: 0x0020253C File Offset: 0x0020073C
		private void DrawFloodAutoexplore(Rack<Cell> Cells)
		{
			Cell[][] map = this.Map;
			Rack<Location2D> floodVisited = Zone.FloodVisited;
			ScreenBuffer screenBuffer = Popup._ScreenBuffer;
			Stopwatch frameTimer = XRLCore.FrameTimer;
			The.Core.RenderBaseToBuffer(screenBuffer);
			int i = 0;
			int count = floodVisited.Count;
			while (i < count)
			{
				float num = (float)i / ((float)count * 1f);
				Location2D location2D = floodVisited[i];
				ConsoleChar consoleChar = screenBuffer[location2D];
				Cell cell = map[location2D.X][location2D.Y];
				bool flag = Cells.Contains(cell);
				consoleChar.Foreground = (flag ? The.Color.White : The.Color.Gray);
				consoleChar.Background = Color.HSVToRGB(num, 1f, 1f);
				consoleChar.Detail = Color.HSVToRGB((num + 0.5f) % 1f, 1f, 1f);
				if (cell.GetNavigationWeightFor(The.Player, true, false, false, false, false) >= 95)
				{
					GameObject highestRenderLayerObject = cell.GetHighestRenderLayerObject();
					if (highestRenderLayerObject.IsValid() && highestRenderLayerObject.Render != null)
					{
						consoleChar.Tile = highestRenderLayerObject.Render.Tile;
					}
				}
				else if (flag)
				{
					consoleChar.Char = '.';
				}
				else
				{
					consoleChar.Char = ' ';
				}
				long num2 = frameTimer.ElapsedMilliseconds + 1L;
				do
				{
					screenBuffer.Draw();
				}
				while (frameTimer.ElapsedMilliseconds < num2);
				i++;
			}
			floodVisited.Clear();
			screenBuffer.Draw();
			Keyboard.getch();
		}

		// Token: 0x06006736 RID: 26422 RVA: 0x002026B8 File Offset: 0x002008B8
		public List<Cell> FastFloodNeighbors(int x1, int y1, int Radius)
		{
			List<Cell> list = new List<Cell>();
			this.FastFloodNeighborsRecurse(x1, y1, x1 - 1, y1, Radius, list);
			this.FastFloodNeighborsRecurse(x1, y1, x1 + 1, y1, Radius, list);
			this.FastFloodNeighborsRecurse(x1, y1, x1, y1 + 1, Radius, list);
			this.FastFloodNeighborsRecurse(x1, y1, x1, y1 - 1, Radius, list);
			return list;
		}

		// Token: 0x06006737 RID: 26423 RVA: 0x00202704 File Offset: 0x00200904
		public void FastFloodNeighborsRecurse(int xs, int ys, int x1, int y1, int Radius, List<Cell> Return)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return;
			}
			if (this.GetCell(x1, y1).IsOccluding())
			{
				return;
			}
			if (Return.CleanContains(this.GetCell(x1, y1)))
			{
				return;
			}
			Return.Add(this.GetCell(x1, y1));
			this.FastFloodNeighborsRecurse(xs, ys, x1 - 1, y1, Radius, Return);
			this.FastFloodNeighborsRecurse(xs, ys, x1 + 1, y1, Radius, Return);
			this.FastFloodNeighborsRecurse(xs, ys, x1, y1 + 1, Radius, Return);
			this.FastFloodNeighborsRecurse(xs, ys, x1, y1 - 1, Radius, Return);
		}

		// Token: 0x06006738 RID: 26424 RVA: 0x002027C8 File Offset: 0x002009C8
		public List<GameObject> FastFloodSearch(int x1, int y1, int Radius, string SearchPart = null, GameObject Looker = null, Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			this.FastFloodSearchRecurse(x1, y1, x1 - 1, y1, Radius, SearchPart, Filter, list, Looker);
			this.FastFloodSearchRecurse(x1, y1, x1 + 1, y1, Radius, SearchPart, Filter, list, Looker);
			this.FastFloodSearchRecurse(x1, y1, x1, y1 + 1, Radius, SearchPart, Filter, list, Looker);
			this.FastFloodSearchRecurse(x1, y1, x1, y1 - 1, Radius, SearchPart, Filter, list, Looker);
			this.FastFloodSearchRecurse(x1, y1, x1 - 1, y1 - 1, Radius, SearchPart, Filter, list, Looker);
			this.FastFloodSearchRecurse(x1, y1, x1 + 1, y1 + 1, Radius, SearchPart, Filter, list, Looker);
			this.FastFloodSearchRecurse(x1, y1, x1 - 1, y1 + 1, Radius, SearchPart, Filter, list, Looker);
			this.FastFloodSearchRecurse(x1, y1, x1 + 1, y1 - 1, Radius, SearchPart, Filter, list, Looker);
			return list;
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x002028A4 File Offset: 0x00200AA4
		private void FastFloodSearchRecurse(int xs, int ys, int x1, int y1, int Radius, string SearchPart, Predicate<GameObject> Filter, List<GameObject> Return, GameObject Looker)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return;
			}
			Cell cell = null;
			this.FloodMap[x1, y1] = Zone.FloodValue;
			if (cell == null)
			{
				cell = this.GetCell(x1, y1);
			}
			int i = 0;
			int count = cell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = cell.Objects[i];
				if ((SearchPart.IsNullOrEmpty() || gameObject.HasPart(SearchPart)) && (Filter == null || Filter(gameObject)))
				{
					Return.Add(gameObject);
				}
				i++;
			}
			this.FastFloodSearchRecurse(xs, ys, x1 - 1, y1, Radius, SearchPart, Filter, Return, Looker);
			this.FastFloodSearchRecurse(xs, ys, x1 + 1, y1, Radius, SearchPart, Filter, Return, Looker);
			this.FastFloodSearchRecurse(xs, ys, x1, y1 + 1, Radius, SearchPart, Filter, Return, Looker);
			this.FastFloodSearchRecurse(xs, ys, x1, y1 - 1, Radius, SearchPart, Filter, Return, Looker);
			this.FastFloodSearchRecurse(xs, ys, x1 - 1, y1 - 1, Radius, SearchPart, Filter, Return, Looker);
			this.FastFloodSearchRecurse(xs, ys, x1 + 1, y1 + 1, Radius, SearchPart, Filter, Return, Looker);
			this.FastFloodSearchRecurse(xs, ys, x1 - 1, y1 + 1, Radius, SearchPart, Filter, Return, Looker);
			this.FastFloodSearchRecurse(xs, ys, x1 + 1, y1 - 1, Radius, SearchPart, Filter, Return, Looker);
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x00202A34 File Offset: 0x00200C34
		public List<GameObject> FastFloodVisibility(int x1, int y1, int Radius, string SearchPart = null, GameObject Looker = null, Predicate<GameObject> ExtraVisibility = null, Predicate<GameObject> Filter = null)
		{
			Type type = ModManager.ResolveType("XRL.World.Parts." + SearchPart, false, false, true);
			if (type == null)
			{
				throw new TypeUnloadedException(SearchPart);
			}
			return this.FastFloodVisibility(x1, y1, Radius, type, Looker, ExtraVisibility, Filter);
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x00202A74 File Offset: 0x00200C74
		public List<GameObject> FastFloodVisibility(int x1, int y1, int Radius, Type SearchPart = null, GameObject Looker = null, Predicate<GameObject> ExtraVisibility = null, Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.FastFloodVisibility(x1, y1, Radius, list, SearchPart, Looker, ExtraVisibility, Filter);
			return list;
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x00202A9C File Offset: 0x00200C9C
		public void FastFloodVisibility(int x1, int y1, int Radius, List<GameObject> Return, Type SearchPart = null, GameObject Looker = null, Predicate<GameObject> ExtraVisibility = null, Predicate<GameObject> Filter = null)
		{
			Cell cell = this.GetCell(x1, y1);
			if (cell == null)
			{
				return;
			}
			int num = Zone.FloodValue + 1;
			if (num == 2147483647)
			{
				num = int.MinValue;
			}
			Zone.FloodValue = num;
			Location2D[] floodArray = Zone.FloodArray;
			int i = 0;
			floodArray[0] = cell.Location;
			Cell[][] map = this.Map;
			Location2D[,] grid = Location2D.Grid;
			int[,] floodMap = this.FloodMap;
			LightLevel[] lightMap = this.LightMap;
			int width = this.Width;
			int num2 = Math.Max(x1 - Radius, 0);
			int num3 = Math.Min(x1 + Radius, width - 1);
			int num4 = Math.Max(y1 - Radius, 0);
			int num5 = Math.Min(y1 + Radius, this.Height - 1);
			bool flag = Looker != null && (Looker.HasPart<DarkVision>() || Looker.HasPart<XRL.World.Parts.Mutation.NightVision>());
			while (i >= 0)
			{
				Location2D location2D = floodArray[i--];
				int x2 = location2D.X;
				int y2 = location2D.Y;
				LightLevel lightLevel = lightMap[x2 + y2 * width];
				if (lightLevel != LightLevel.Blackout && (flag || lightLevel > LightLevel.None))
				{
					cell = map[x2][y2];
					GameObject[] array = cell.Objects.GetArray();
					int j = 0;
					int count = cell.Objects.Count;
					while (j < count)
					{
						GameObject gameObject = array[j];
						if ((SearchPart == null || gameObject.HasPart(SearchPart)) && (Filter == null || Filter(gameObject)))
						{
							Return.Add(gameObject);
						}
						j++;
					}
					if (!cell.IsOccluding())
					{
						bool flag2 = y2 > num4;
						bool flag3 = y2 < num5;
						if (x2 > num2)
						{
							if (floodMap[x2 - 1, y2] != num)
							{
								floodArray[++i] = grid[x2 - 1, y2];
								floodMap[x2 - 1, y2] = num;
							}
							if (flag2 && floodMap[x2 - 1, y2 - 1] != num)
							{
								floodArray[++i] = grid[x2 - 1, y2 - 1];
								floodMap[x2 - 1, y2 - 1] = num;
							}
							if (flag3 && floodMap[x2 - 1, y2 + 1] != num)
							{
								floodArray[++i] = grid[x2 - 1, y2 + 1];
								floodMap[x2 - 1, y2 + 1] = num;
							}
						}
						if (x2 < num3)
						{
							if (floodMap[x2 + 1, y2] != num)
							{
								floodArray[++i] = grid[x2 + 1, y2];
								floodMap[x2 + 1, y2] = num;
							}
							if (flag2 && floodMap[x2 + 1, y2 - 1] != num)
							{
								floodArray[++i] = grid[x2 + 1, y2 - 1];
								floodMap[x2 + 1, y2 - 1] = num;
							}
							if (flag3 && floodMap[x2 + 1, y2 + 1] != num)
							{
								floodArray[++i] = grid[x2 + 1, y2 + 1];
								floodMap[x2 + 1, y2 + 1] = num;
							}
						}
						if (flag2 && floodMap[x2, y2 - 1] != num)
						{
							floodArray[++i] = grid[x2, y2 - 1];
							floodMap[x2, y2 - 1] = num;
						}
						if (flag3 && floodMap[x2, y2 + 1] != num)
						{
							floodArray[++i] = grid[x2, y2 + 1];
							floodMap[x2, y2 + 1] = num;
						}
					}
				}
			}
			if (Options.DrawFloodVis)
			{
				for (int k = 0; k < 25; k++)
				{
					for (int l = 0; l < 80; l++)
					{
						if (this.FloodMap[l, k] == num)
						{
							Popup._ScreenBuffer.Goto(l, k);
							Popup._ScreenBuffer.Write("*", true, false, false, null, -1);
						}
					}
				}
				Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
				Keyboard.getch();
			}
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x00202E6C File Offset: 0x0020106C
		public bool FastFloodVisibilityAny(int x1, int y1, int Radius, string SearchPart, GameObject Looker = null)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Looker != null && (Looker.HasPart<DarkVision>() || Looker.HasPart<XRL.World.Parts.Mutation.NightVision>()))
			{
				flag = true;
			}
			return this.FastFloodVisibilityAnyRecurse(x1, y1, x1 - 1, y1, Radius, SearchPart, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 + 1, y1, Radius, SearchPart, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1, y1 + 1, Radius, SearchPart, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1, y1 - 1, Radius, SearchPart, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 - 1, y1 - 1, Radius, SearchPart, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 + 1, y1 + 1, Radius, SearchPart, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 - 1, y1 + 1, Radius, SearchPart, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 + 1, y1 - 1, Radius, SearchPart, Looker, flag);
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x00202F5C File Offset: 0x0020115C
		public bool FastFloodVisibilityAnyRecurse(int xs, int ys, int x1, int y1, int Radius, string SearchPart, GameObject Looker, bool DarkVision)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return false;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return false;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return false;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return false;
			}
			LightLevel light = this.GetLight(x1, y1);
			if (light == LightLevel.Blackout)
			{
				return false;
			}
			if (!DarkVision && light <= LightLevel.None)
			{
				return false;
			}
			this.FloodMap[x1, y1] = Zone.FloodValue;
			return this.GetCell(x1, y1).HasObjectWithPart(SearchPart) || (!this.GetCell(x1, y1).IsOccluding() && (this.FastFloodVisibilityAnyRecurse(xs, ys, x1 - 1, y1, Radius, SearchPart, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 + 1, y1, Radius, SearchPart, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1, y1 + 1, Radius, SearchPart, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1, y1 - 1, Radius, SearchPart, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 - 1, y1 - 1, Radius, SearchPart, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 + 1, y1 + 1, Radius, SearchPart, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 - 1, y1 + 1, Radius, SearchPart, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 + 1, y1 - 1, Radius, SearchPart, Looker, DarkVision)));
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x002030D8 File Offset: 0x002012D8
		public bool FastFloodVisibilityAny(int x1, int y1, int Radius, Predicate<GameObject> Filter, GameObject Looker)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Looker != null && (Looker.HasPart<DarkVision>() || Looker.HasPart<XRL.World.Parts.Mutation.NightVision>()))
			{
				flag = true;
			}
			return this.FastFloodVisibilityAnyRecurse(x1, y1, x1 - 1, y1, Radius, Filter, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 + 1, y1, Radius, Filter, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1, y1 + 1, Radius, Filter, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1, y1 - 1, Radius, Filter, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 - 1, y1 - 1, Radius, Filter, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 + 1, y1 + 1, Radius, Filter, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 - 1, y1 + 1, Radius, Filter, Looker, flag) || this.FastFloodVisibilityAnyRecurse(x1, y1, x1 + 1, y1 - 1, Radius, Filter, Looker, flag);
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x002031C8 File Offset: 0x002013C8
		public bool FastFloodVisibilityAnyRecurse(int xs, int ys, int x1, int y1, int Radius, Predicate<GameObject> Filter, GameObject Looker, bool DarkVision)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return false;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return false;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return false;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return false;
			}
			LightLevel light = this.GetLight(x1, y1);
			if (light == LightLevel.Blackout)
			{
				return false;
			}
			if (!DarkVision && light <= LightLevel.None)
			{
				return false;
			}
			this.FloodMap[x1, y1] = Zone.FloodValue;
			return this.GetCell(x1, y1).HasObject(Filter) || (!this.GetCell(x1, y1).IsOccluding() && (this.FastFloodVisibilityAnyRecurse(xs, ys, x1 - 1, y1, Radius, Filter, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 + 1, y1, Radius, Filter, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1, y1 + 1, Radius, Filter, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1, y1 - 1, Radius, Filter, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 - 1, y1 - 1, Radius, Filter, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 + 1, y1 + 1, Radius, Filter, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 - 1, y1 + 1, Radius, Filter, Looker, DarkVision) || this.FastFloodVisibilityAnyRecurse(xs, ys, x1 + 1, y1 - 1, Radius, Filter, Looker, DarkVision)));
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x00203344 File Offset: 0x00201544
		public bool FastFloodVisibilityAnyBlueprint(int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool VisibleToPlayerOnly = false)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Looker != null && (Looker.HasPart<DarkVision>() || Looker.HasPart<XRL.World.Parts.Mutation.NightVision>()))
			{
				flag = true;
			}
			return this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1 - 1, y1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1 + 1, y1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1, y1 + 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1, y1 - 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1 - 1, y1 - 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1 + 1, y1 + 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1 - 1, y1 + 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(x1, y1, x1 + 1, y1 - 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly);
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x00203448 File Offset: 0x00201648
		public bool FastFloodVisibilityAnyBlueprintRecurse(int xs, int ys, int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool DarkVision, bool VisibleToPlayerOnly)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return false;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return false;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return false;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return false;
			}
			LightLevel light = this.GetLight(x1, y1);
			if (light == LightLevel.Blackout)
			{
				return false;
			}
			if (!DarkVision && light <= LightLevel.None)
			{
				return false;
			}
			this.FloodMap[x1, y1] = Zone.FloodValue;
			Cell cell = this.GetCell(x1, y1);
			return cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && ((VisibleToPlayerOnly ? cell.GetFirstObject(Blueprint) : cell.GetFirstVisibleObject(Blueprint)) != null || (!cell.IsOccluding() && (this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1 - 1, y1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1 + 1, y1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1, y1 + 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1, y1 - 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1 - 1, y1 - 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1 + 1, y1 + 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1 - 1, y1 + 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) || this.FastFloodVisibilityAnyBlueprintRecurse(xs, ys, x1 + 1, y1 - 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly))));
		}

		// Token: 0x06006743 RID: 26435 RVA: 0x002035F4 File Offset: 0x002017F4
		public bool FastFloodFindAnyBlueprint(int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool VisibleOnly = false, bool ExploredOnly = false)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			return this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1 - 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1 + 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1 - 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1 + 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1 - 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(x1, y1, x1 + 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly);
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x002036E8 File Offset: 0x002018E8
		public bool FastFloodFindAnyBlueprintRecurse(int xs, int ys, int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool VisibleOnly, bool ExploredOnly)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return false;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return false;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return false;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return false;
			}
			this.FloodMap[x1, y1] = Zone.FloodValue;
			Cell cell = this.GetCell(x1, y1);
			return cell != null && (!VisibleOnly || cell.IsVisible()) && (!ExploredOnly || cell.IsExploredFor(Looker)) && ((VisibleOnly ? cell.GetFirstVisibleObject(Blueprint) : cell.GetFirstObject(Blueprint)) != null || (!cell.IsOccluding() && (this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1 - 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1 + 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1 - 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1 + 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1 - 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) || this.FastFloodFindAnyBlueprintRecurse(xs, ys, x1 + 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly))));
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x00203888 File Offset: 0x00201A88
		public GameObject FastFloodVisibilityFirstBlueprint(int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool VisibleToPlayerOnly = false)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Looker != null && (Looker.HasPart<DarkVision>() || Looker.HasPart<XRL.World.Parts.Mutation.NightVision>()))
			{
				flag = true;
			}
			GameObject gameObject;
			if ((gameObject = this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1 - 1, y1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly)) == null && (gameObject = this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1 + 1, y1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly)) == null && (gameObject = this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1, y1 + 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly)) == null && (gameObject = this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1, y1 - 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly)) == null && (gameObject = this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1 - 1, y1 - 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly)) == null && (gameObject = this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1 + 1, y1 + 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly)) == null)
			{
				gameObject = this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1 - 1, y1 + 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly) ?? this.FastFloodVisibilityFirstBlueprintRecurse(x1, y1, x1 + 1, y1 - 1, Radius, Blueprint, Looker, flag, VisibleToPlayerOnly);
			}
			return gameObject;
		}

		// Token: 0x06006746 RID: 26438 RVA: 0x00203998 File Offset: 0x00201B98
		public GameObject FastFloodVisibilityFirstBlueprintRecurse(int xs, int ys, int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool DarkVision, bool VisibleToPlayerOnly)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return null;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return null;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return null;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return null;
			}
			LightLevel light = this.GetLight(x1, y1);
			if (light == LightLevel.Blackout)
			{
				return null;
			}
			if (!DarkVision && light <= LightLevel.None)
			{
				return null;
			}
			this.FloodMap[x1, y1] = Zone.FloodValue;
			Cell cell = this.GetCell(x1, y1);
			if (cell == null)
			{
				return null;
			}
			if (VisibleToPlayerOnly && !cell.IsVisible())
			{
				return null;
			}
			GameObject gameObject = (VisibleToPlayerOnly ? cell.GetFirstVisibleObject(Blueprint) : cell.GetFirstObject(Blueprint));
			if (gameObject != null)
			{
				return gameObject;
			}
			if (cell.IsOccluding())
			{
				return null;
			}
			GameObject gameObject2;
			if ((gameObject2 = this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1 - 1, y1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly)) == null && (gameObject2 = this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1 + 1, y1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly)) == null && (gameObject2 = this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1, y1 + 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly)) == null && (gameObject2 = this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1, y1 - 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly)) == null && (gameObject2 = this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1 - 1, y1 - 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly)) == null && (gameObject2 = this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1 + 1, y1 + 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly)) == null)
			{
				gameObject2 = this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1 - 1, y1 + 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly) ?? this.FastFloodVisibilityFirstBlueprintRecurse(xs, ys, x1 + 1, y1 - 1, Radius, Blueprint, Looker, DarkVision, VisibleToPlayerOnly);
			}
			return gameObject2;
		}

		// Token: 0x06006747 RID: 26439 RVA: 0x00203B50 File Offset: 0x00201D50
		public GameObject FastFloodFindFirstBlueprint(int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool VisibleOnly = false, bool ExploredOnly = false)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			GameObject gameObject;
			if ((gameObject = this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1 - 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject = this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1 + 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject = this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject = this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject = this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1 - 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject = this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1 + 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null)
			{
				gameObject = this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1 - 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) ?? this.FastFloodFindFirstBlueprintRecurse(x1, y1, x1 + 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly);
			}
			return gameObject;
		}

		// Token: 0x06006748 RID: 26440 RVA: 0x00203C50 File Offset: 0x00201E50
		public GameObject FastFloodFindFirstBlueprintRecurse(int xs, int ys, int x1, int y1, int Radius, string Blueprint, GameObject Looker, bool VisibleOnly, bool ExploredOnly)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return null;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return null;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return null;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return null;
			}
			this.FloodMap[x1, y1] = Zone.FloodValue;
			Cell cell = this.GetCell(x1, y1);
			if (cell == null)
			{
				return null;
			}
			if (VisibleOnly && !cell.IsVisible())
			{
				return null;
			}
			if (ExploredOnly && !cell.IsExploredFor(Looker))
			{
				return null;
			}
			GameObject gameObject = (VisibleOnly ? cell.GetFirstVisibleObject(Blueprint) : cell.GetFirstObject(Blueprint));
			if (gameObject != null)
			{
				return gameObject;
			}
			if (cell.IsOccluding())
			{
				return null;
			}
			GameObject gameObject2;
			if ((gameObject2 = this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1 - 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject2 = this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1 + 1, y1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject2 = this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject2 = this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject2 = this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1 - 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null && (gameObject2 = this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1 + 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly)) == null)
			{
				gameObject2 = this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1 - 1, y1 + 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly) ?? this.FastFloodFindFirstBlueprintRecurse(xs, ys, x1 + 1, y1 - 1, Radius, Blueprint, Looker, VisibleOnly, ExploredOnly);
			}
			return gameObject2;
		}

		// Token: 0x06006749 RID: 26441 RVA: 0x00203E00 File Offset: 0x00202000
		private void AddFloodNext(int xs, int ys, int x1, int y1, int Radius)
		{
			if (x1 < 0 || x1 > this.Width - 1 || y1 < 0 || y1 > this.Height - 1)
			{
				return;
			}
			if (this.FloodMap[x1, y1] == Zone.FloodValue)
			{
				return;
			}
			if (Math.Abs(xs - x1) > Radius)
			{
				return;
			}
			if (Math.Abs(ys - y1) > Radius)
			{
				return;
			}
			Cell cell = this.GetCell(x1, y1);
			if (cell == null)
			{
				return;
			}
			int num;
			if (Zone.FloodNextIndex.TryGetValue(cell, out num))
			{
				if (Radius > Zone.FloodNextRadii[num])
				{
					Zone.FloodNextRadii[num] = Radius;
				}
				return;
			}
			Zone.FloodNextIndex[cell] = num;
			Zone.FloodNext.Add(cell);
			Zone.FloodNextRadii.Add(Radius);
			this.FloodMap[x1, y1] = Zone.FloodValue;
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x00203ED0 File Offset: 0x002020D0
		public List<GameObject> FastFloodAudibility(int x1, int y1, int Radius, Predicate<GameObject> Filter, GameObject Hearer)
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Hearer != null)
			{
				HeightenedHearing part = Hearer.GetPart<HeightenedHearing>();
				if (part != null)
				{
					flag = true;
					Radius += part.Level;
				}
			}
			Zone.FloodNext.Clear();
			Zone.FloodNextIndex.Clear();
			Zone.FloodNextRadii.Clear();
			this.AddFloodNext(x1, y1, x1, y1, Radius);
			while (Zone.FloodNext.Count > 0)
			{
				Zone.FloodCurrent.Clear();
				Zone.FloodCurrent.AddRange(Zone.FloodNext);
				Zone.FloodCurrentRadii.Clear();
				Zone.FloodCurrentRadii.AddRange(Zone.FloodNextRadii);
				Zone.FloodNext.Clear();
				Zone.FloodNextIndex.Clear();
				Zone.FloodNextRadii.Clear();
				for (int i = 0; i < Zone.FloodCurrent.Count; i++)
				{
					this.FastFloodAudibilityProcess(x1, y1, Zone.FloodCurrent[i], Zone.FloodCurrentRadii[i], Filter, list, Hearer, flag);
				}
			}
			Zone.FloodCurrent.Clear();
			Zone.FloodCurrentRadii.Clear();
			if (Options.DrawFloodAud)
			{
				for (int j = 0; j < 25; j++)
				{
					for (int k = 0; k < 80; k++)
					{
						if (this.FloodMap[k, j] == Zone.FloodValue)
						{
							Popup._ScreenBuffer.Goto(k, j);
							Popup._ScreenBuffer.Write("*", true, false, false, null, -1);
						}
					}
				}
				Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
				Keyboard.getch();
			}
			return list;
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x00204074 File Offset: 0x00202274
		public void FastFloodAudibilityProcess(int xs, int ys, Cell C, int Radius, Predicate<GameObject> Filter, List<GameObject> Return, GameObject Hearer, bool HeightenedHearing)
		{
			foreach (GameObject gameObject in C.Objects)
			{
				if (Filter(gameObject))
				{
					Return.Add(gameObject);
				}
			}
			if (C.IsSolid())
			{
				if (HeightenedHearing)
				{
					Radius--;
				}
				else
				{
					Radius -= 10;
				}
				if (Radius <= 0)
				{
					return;
				}
			}
			this.AddFloodNext(xs, ys, C.X - 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y - 1, Radius);
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x002041BC File Offset: 0x002023BC
		public bool FastFloodAudibilityAny(int x1, int y1, int Radius, Predicate<GameObject> Filter, GameObject Hearer)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Hearer != null)
			{
				HeightenedHearing part = Hearer.GetPart<HeightenedHearing>();
				if (part != null)
				{
					flag = true;
					Radius += part.Level;
				}
			}
			Zone.FloodNext.Clear();
			Zone.FloodNextIndex.Clear();
			Zone.FloodNextRadii.Clear();
			this.AddFloodNext(x1, y1, x1, y1, Radius);
			try
			{
				while (Zone.FloodNext.Count > 0)
				{
					Zone.FloodCurrent.Clear();
					Zone.FloodCurrent.AddRange(Zone.FloodNext);
					Zone.FloodCurrentRadii.Clear();
					Zone.FloodCurrentRadii.AddRange(Zone.FloodNextRadii);
					Zone.FloodNext.Clear();
					Zone.FloodNextIndex.Clear();
					Zone.FloodNextRadii.Clear();
					for (int i = 0; i < Zone.FloodCurrent.Count; i++)
					{
						if (this.FastFloodAudibilityAnyProcess(x1, y1, Zone.FloodCurrent[i], Zone.FloodCurrentRadii[i], Filter, Hearer, flag))
						{
							return true;
						}
					}
				}
			}
			finally
			{
				Zone.FloodCurrent.Clear();
				Zone.FloodCurrentRadii.Clear();
			}
			return false;
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x002042FC File Offset: 0x002024FC
		public bool FastFloodAudibilityAnyProcess(int xs, int ys, Cell C, int Radius, Predicate<GameObject> Filter, GameObject Hearer, bool HeightenedHearing)
		{
			if (C.HasObject(Filter))
			{
				return true;
			}
			if (C.IsSolid())
			{
				if (HeightenedHearing)
				{
					Radius--;
				}
				else
				{
					Radius -= 10;
				}
				if (Radius <= 0)
				{
					return false;
				}
			}
			this.AddFloodNext(xs, ys, C.X - 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y - 1, Radius);
			return false;
		}

		// Token: 0x0600674E RID: 26446 RVA: 0x00204400 File Offset: 0x00202600
		public List<GameObject> FastFloodOlfaction(int x1, int y1, int Radius, Predicate<GameObject> Filter, GameObject Smeller)
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Smeller != null)
			{
				HeightenedSmell part = Smeller.GetPart<HeightenedSmell>();
				if (part != null)
				{
					flag = true;
					Radius += part.Level * 4;
				}
			}
			Zone.FloodNext.Clear();
			Zone.FloodNextIndex.Clear();
			Zone.FloodNextRadii.Clear();
			this.AddFloodNext(x1, y1, x1, y1, Radius);
			while (Zone.FloodNext.Count > 0)
			{
				Zone.FloodCurrent.Clear();
				Zone.FloodCurrent.AddRange(Zone.FloodNext);
				Zone.FloodCurrentRadii.Clear();
				Zone.FloodCurrentRadii.AddRange(Zone.FloodNextRadii);
				Zone.FloodNext.Clear();
				Zone.FloodNextIndex.Clear();
				Zone.FloodNextRadii.Clear();
				for (int i = 0; i < Zone.FloodCurrent.Count; i++)
				{
					this.FastFloodOlfactionProcess(x1, y1, Zone.FloodCurrent[i], Zone.FloodCurrentRadii[i], Filter, list, Smeller, flag);
				}
			}
			Zone.FloodCurrent.Clear();
			Zone.FloodCurrentRadii.Clear();
			if (Options.DrawFloodOlf)
			{
				for (int j = 0; j < 25; j++)
				{
					for (int k = 0; k < 80; k++)
					{
						if (this.FloodMap[k, j] == Zone.FloodValue)
						{
							Popup._ScreenBuffer.Goto(k, j);
							Popup._ScreenBuffer.Write("*", true, false, false, null, -1);
						}
					}
				}
				Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
				Keyboard.getch();
			}
			return list;
		}

		// Token: 0x0600674F RID: 26447 RVA: 0x002045A8 File Offset: 0x002027A8
		public void FastFloodOlfactionProcess(int xs, int ys, Cell C, int Radius, Predicate<GameObject> Filter, List<GameObject> Return, GameObject Smeller, bool HeightenedSmell)
		{
			foreach (GameObject gameObject in C.Objects)
			{
				if (Filter(gameObject))
				{
					Return.Add(gameObject);
				}
			}
			if (C.IsSolid())
			{
				if (!HeightenedSmell || !C.HasObjectWithPart("Door"))
				{
					return;
				}
				Radius = Radius / 2 - 1;
				if (Radius <= 0)
				{
					return;
				}
			}
			if (C.IsOccluding() || C.HasWadingDepthLiquid())
			{
				if (!HeightenedSmell)
				{
					Radius /= 2;
				}
				Radius--;
				if (Radius <= 0)
				{
					return;
				}
			}
			this.AddFloodNext(xs, ys, C.X - 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y - 1, Radius);
		}

		// Token: 0x06006750 RID: 26448 RVA: 0x0020471C File Offset: 0x0020291C
		public bool FastFloodOlfactionAny(int x1, int y1, int Radius, Predicate<GameObject> Filter, GameObject Smeller)
		{
			if (++Zone.FloodValue == 2147483647)
			{
				Zone.FloodValue = int.MinValue;
			}
			bool flag = false;
			if (Smeller != null)
			{
				HeightenedSmell part = Smeller.GetPart<HeightenedSmell>();
				if (part != null)
				{
					flag = true;
					Radius += part.Level * 4;
				}
			}
			Zone.FloodNext.Clear();
			Zone.FloodNextIndex.Clear();
			Zone.FloodNextRadii.Clear();
			this.AddFloodNext(x1, y1, x1, y1, Radius);
			try
			{
				while (Zone.FloodNext.Count > 0)
				{
					Zone.FloodCurrent.Clear();
					Zone.FloodCurrent.AddRange(Zone.FloodNext);
					Zone.FloodCurrentRadii.Clear();
					Zone.FloodCurrentRadii.AddRange(Zone.FloodNextRadii);
					Zone.FloodNext.Clear();
					Zone.FloodNextIndex.Clear();
					Zone.FloodNextRadii.Clear();
					for (int i = 0; i < Zone.FloodCurrent.Count; i++)
					{
						if (this.FastFloodOlfactionAnyProcess(x1, y1, Zone.FloodCurrent[i], Zone.FloodCurrentRadii[i], Filter, Smeller, flag))
						{
							return true;
						}
					}
				}
			}
			finally
			{
				Zone.FloodCurrent.Clear();
				Zone.FloodCurrentRadii.Clear();
			}
			return false;
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x0020485C File Offset: 0x00202A5C
		public bool FastFloodOlfactionAnyProcess(int xs, int ys, Cell C, int Radius, Predicate<GameObject> Filter, GameObject Smeller, bool HeightenedSmell)
		{
			if (C.HasObject(Filter))
			{
				return true;
			}
			if (C.IsSolid())
			{
				if (!HeightenedSmell || !C.HasObjectWithPart("Door"))
				{
					return false;
				}
				Radius = Radius / 2 - 1;
				if (Radius <= 0)
				{
					return false;
				}
			}
			if (C.IsOccluding() || C.HasWadingDepthLiquid())
			{
				if (!HeightenedSmell)
				{
					Radius /= 2;
				}
				Radius--;
				if (Radius <= 0)
				{
					return false;
				}
			}
			this.AddFloodNext(xs, ys, C.X - 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y - 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X - 1, C.Y + 1, Radius);
			this.AddFloodNext(xs, ys, C.X + 1, C.Y - 1, Radius);
			return false;
		}

		// Token: 0x06006752 RID: 26450 RVA: 0x00204990 File Offset: 0x00202B90
		private void InitFlood(string Method, int Radius)
		{
			if (Zone.InFloodMethod == null)
			{
				Zone.InFloodMethod = Method;
			}
			else
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"Call to ",
					Method,
					"() inside ",
					Zone.InFloodMethod,
					"(), global flood methods are mutually non-reentrant"
				}));
			}
			if (Zone.Flooded == null)
			{
				Zone.Flooded = new Dictionary<Cell, int>((Radius + 1) * (Radius + 1));
				return;
			}
			Zone.Flooded.Clear();
		}

		// Token: 0x06006753 RID: 26451 RVA: 0x00204A03 File Offset: 0x00202C03
		private void DoneFlood()
		{
			Zone.Flooded.Clear();
			Zone.InFloodMethod = null;
		}

		// Token: 0x06006754 RID: 26452 RVA: 0x00204A18 File Offset: 0x00202C18
		public List<GameObject> GlobalFloodObjects(int x1, int y1, int Radius, string SearchPart, GameObject Looker, List<GameObject> Return, bool CheckInWalls = false, bool ForFluid = false)
		{
			Return.Clear();
			if (Looker == null)
			{
				return Return;
			}
			Zone currentZone = Looker.CurrentZone;
			if (currentZone == null)
			{
				return Return;
			}
			Cell cell = currentZone.GetCell(x1, y1);
			if (cell == null)
			{
				return Return;
			}
			this.InitFlood("GlobalFloodObjects", Radius);
			this.GlobalFloodObjectsRecurse(cell, Radius, SearchPart, Return, ForFluid, CheckInWalls, true);
			this.DoneFlood();
			return Return;
		}

		// Token: 0x06006755 RID: 26453 RVA: 0x00204A78 File Offset: 0x00202C78
		public void GlobalFloodObjectsRecurse(Cell C, int Radius, string SearchPart, List<GameObject> Return, bool ForFluid, bool CheckInWalls, bool First = false)
		{
			if (C == null)
			{
				return;
			}
			if (Radius < 0)
			{
				return;
			}
			if (!First && C.IsSolid(ForFluid))
			{
				return;
			}
			if (!Zone.Flooded.ContainsKey(C))
			{
				Zone.Flooded.Add(C, 0);
				C.ForeachObjectWithPart(SearchPart, delegate(GameObject o)
				{
					Return.Add(o);
				});
			}
			if (Zone.Flooded[C] < Radius)
			{
				Zone.Flooded[C] = Radius;
				int num = Radius - 1;
				if (num > 0)
				{
					foreach (string text in Cell.DirectionListCardinalFirst)
					{
						this.GlobalFloodObjectsRecurse(C.GetCellFromDirectionGlobalIfBuilt(text), num, SearchPart, Return, ForFluid, CheckInWalls, false);
					}
					this.GlobalFloodObjectsRecurse(C.GetReversibleAccessUpCell(), num, SearchPart, Return, ForFluid, CheckInWalls, false);
					this.GlobalFloodObjectsRecurse(C.GetReversibleAccessDownCell(), num, SearchPart, Return, ForFluid, CheckInWalls, false);
				}
			}
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x00204B60 File Offset: 0x00202D60
		public GameObject GlobalFloodFirstObject(int x1, int y1, int Radius, string SearchPart, GameObject Looker, bool ForFluid = false, bool CheckInWalls = false)
		{
			if (Looker == null)
			{
				return null;
			}
			Zone currentZone = Looker.CurrentZone;
			if (currentZone == null)
			{
				return null;
			}
			Cell cell = currentZone.GetCell(x1, y1);
			if (cell == null)
			{
				return null;
			}
			this.InitFlood("GlobalFloodFirstObject", Radius);
			GameObject gameObject = this.GlobalFloodFirstObjectRecurse(cell, Radius, SearchPart, ForFluid, CheckInWalls, true);
			this.DoneFlood();
			return gameObject;
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x00204BB0 File Offset: 0x00202DB0
		public GameObject GlobalFloodFirstObjectRecurse(Cell C, int Radius, string SearchPart, bool ForFluid, bool CheckInWalls, bool First = false)
		{
			if (C == null)
			{
				return null;
			}
			if (Radius < 0)
			{
				return null;
			}
			if (!First && C.IsSolid(ForFluid))
			{
				return null;
			}
			if (!Zone.Flooded.ContainsKey(C))
			{
				GameObject firstObjectWithPart = C.GetFirstObjectWithPart(SearchPart);
				if (firstObjectWithPart != null)
				{
					return firstObjectWithPart;
				}
				Zone.Flooded.Add(C, 0);
			}
			if (Zone.Flooded[C] < Radius)
			{
				Zone.Flooded[C] = Radius;
				int num = Radius - 1;
				if (num >= 0)
				{
					GameObject gameObject;
					foreach (string text in Cell.DirectionListCardinalFirst)
					{
						gameObject = this.GlobalFloodFirstObjectRecurse(C.GetCellFromDirectionGlobalIfBuilt(text), num, SearchPart, ForFluid, CheckInWalls, false);
						if (gameObject != null)
						{
							return gameObject;
						}
					}
					gameObject = this.GlobalFloodFirstObjectRecurse(C.GetReversibleAccessUpCell(), num, SearchPart, ForFluid, CheckInWalls, false);
					if (gameObject != null)
					{
						return gameObject;
					}
					gameObject = this.GlobalFloodFirstObjectRecurse(C.GetReversibleAccessUpCell(), num, SearchPart, ForFluid, CheckInWalls, false);
					if (gameObject == null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x00204C8C File Offset: 0x00202E8C
		public GameObject GlobalFloodFirstObject(int x1, int y1, int Radius, string SearchPart, Predicate<GameObject> pFilter, GameObject Looker, bool ForFluid = false)
		{
			if (Looker == null)
			{
				return null;
			}
			Zone currentZone = Looker.CurrentZone;
			if (currentZone == null)
			{
				return null;
			}
			Cell cell = currentZone.GetCell(x1, y1);
			if (cell == null)
			{
				return null;
			}
			this.InitFlood("GlobalFloodFirstObject", Radius);
			GameObject gameObject = this.GlobalFloodFirstObjectRecurse(cell, Radius, SearchPart, pFilter, ForFluid, true, false);
			this.DoneFlood();
			return gameObject;
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x00204CDC File Offset: 0x00202EDC
		public GameObject GlobalFloodFirstObjectRecurse(Cell C, int Radius, string SearchPart, Predicate<GameObject> pFilter, bool ForFluid, bool CheckInWalls, bool First = false)
		{
			if (C == null)
			{
				return null;
			}
			if (Radius < 0)
			{
				return null;
			}
			bool flag = false;
			if (!First && C.IsSolid(ForFluid))
			{
				if (!CheckInWalls)
				{
					return null;
				}
				flag = true;
			}
			if (!Zone.Flooded.ContainsKey(C))
			{
				GameObject firstObjectWithPart = C.GetFirstObjectWithPart(SearchPart, pFilter);
				if (firstObjectWithPart != null)
				{
					return firstObjectWithPart;
				}
				Zone.Flooded.Add(C, 0);
			}
			if (flag)
			{
				return null;
			}
			if (Zone.Flooded[C] < Radius)
			{
				Zone.Flooded[C] = Radius;
				int num = Radius - 1;
				if (num >= 0)
				{
					GameObject gameObject;
					foreach (string text in Cell.DirectionListCardinalFirst)
					{
						gameObject = this.GlobalFloodFirstObjectRecurse(C.GetCellFromDirectionGlobalIfBuilt(text), num, SearchPart, pFilter, ForFluid, CheckInWalls, false);
						if (gameObject != null)
						{
							return gameObject;
						}
					}
					gameObject = this.GlobalFloodFirstObjectRecurse(C.GetReversibleAccessUpCell(), num, SearchPart, pFilter, ForFluid, CheckInWalls, false);
					if (gameObject != null)
					{
						return gameObject;
					}
					gameObject = this.GlobalFloodFirstObjectRecurse(C.GetReversibleAccessDownCell(), num, SearchPart, pFilter, ForFluid, CheckInWalls, false);
					if (gameObject == null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x00204DD8 File Offset: 0x00202FD8
		public bool GlobalFloodAny(int x1, int y1, int Radius, string SearchPart, GameObject Looker, bool ForFluid = false, bool CheckInWalls = false)
		{
			if (Looker == null)
			{
				return false;
			}
			Zone currentZone = Looker.CurrentZone;
			if (currentZone == null)
			{
				return false;
			}
			Cell cell = currentZone.GetCell(x1, y1);
			if (cell == null)
			{
				return false;
			}
			this.InitFlood("GlobalFloodAny", Radius);
			bool flag = this.GlobalFloodAnyRecurse(cell, Radius, SearchPart, ForFluid, CheckInWalls, true);
			this.DoneFlood();
			return flag;
		}

		// Token: 0x0600675B RID: 26459 RVA: 0x00204E28 File Offset: 0x00203028
		public bool GlobalFloodAnyRecurse(Cell C, int Radius, string SearchPart, bool ForFluid, bool CheckInWalls, bool First = false)
		{
			if (C == null)
			{
				return false;
			}
			if (Radius < 0)
			{
				return false;
			}
			bool flag = false;
			if (!First && C.IsSolid(ForFluid))
			{
				if (!CheckInWalls)
				{
					return false;
				}
				flag = true;
			}
			if (!Zone.Flooded.ContainsKey(C))
			{
				if (C.HasObjectWithPart(SearchPart))
				{
					return true;
				}
				Zone.Flooded.Add(C, 0);
			}
			if (flag)
			{
				return false;
			}
			if (Zone.Flooded[C] < Radius)
			{
				Zone.Flooded[C] = Radius;
				int num = Radius - 1;
				if (num >= 0)
				{
					foreach (string text in Cell.DirectionListCardinalFirst)
					{
						if (this.GlobalFloodAnyRecurse(C.GetCellFromDirectionGlobalIfBuilt(text), num, SearchPart, ForFluid, CheckInWalls, false))
						{
							return true;
						}
					}
					if (this.GlobalFloodAnyRecurse(C.GetReversibleAccessUpCell(), num, SearchPart, ForFluid, CheckInWalls, false))
					{
						return true;
					}
					if (this.GlobalFloodAnyRecurse(C.GetReversibleAccessDownCell(), num, SearchPart, ForFluid, CheckInWalls, false))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600675C RID: 26460 RVA: 0x00204F08 File Offset: 0x00203108
		public bool GlobalFloodAny(int x1, int y1, int Radius, string SearchPart, Predicate<GameObject> pFilter, GameObject Looker, bool ForFluid = false, bool CheckInWalls = false)
		{
			if (Looker == null)
			{
				return false;
			}
			Zone currentZone = Looker.CurrentZone;
			if (currentZone == null)
			{
				return false;
			}
			Cell cell = currentZone.GetCell(x1, y1);
			if (cell == null)
			{
				return false;
			}
			this.InitFlood("GlobalFloodAny", Radius);
			bool flag = this.GlobalFloodAnyRecurse(cell, Radius, SearchPart, pFilter, ForFluid, CheckInWalls, true);
			this.DoneFlood();
			return flag;
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x00204F5C File Offset: 0x0020315C
		public bool GlobalFloodAnyRecurse(Cell C, int Radius, string SearchPart, Predicate<GameObject> pFilter, bool ForFluid, bool CheckInWalls, bool First = false)
		{
			if (C == null)
			{
				return false;
			}
			if (Radius < 0)
			{
				return false;
			}
			bool flag = false;
			if (!First && C.IsSolid(ForFluid))
			{
				if (!CheckInWalls)
				{
					return false;
				}
				flag = true;
			}
			if (!Zone.Flooded.ContainsKey(C))
			{
				if (C.HasObjectWithPart(SearchPart, pFilter))
				{
					return true;
				}
				Zone.Flooded.Add(C, 0);
			}
			if (flag)
			{
				return false;
			}
			if (Zone.Flooded[C] < Radius)
			{
				Zone.Flooded[C] = Radius;
				int num = Radius - 1;
				if (num >= 0)
				{
					foreach (string text in Cell.DirectionListCardinalFirst)
					{
						if (this.GlobalFloodAnyRecurse(C.GetCellFromDirectionGlobalIfBuilt(text), num, SearchPart, pFilter, ForFluid, CheckInWalls, false))
						{
							return true;
						}
					}
					if (this.GlobalFloodAnyRecurse(C.GetReversibleAccessUpCell(), num, SearchPart, pFilter, ForFluid, CheckInWalls, false))
					{
						return true;
					}
					if (this.GlobalFloodAnyRecurse(C.GetReversibleAccessDownCell(), num, SearchPart, pFilter, ForFluid, CheckInWalls, false))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600675E RID: 26462 RVA: 0x00205042 File Offset: 0x00203242
		[WishCommand("unexplore", null)]
		private static void WishClearExplore()
		{
			The.ActiveZone.ClearExploredMap();
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600675F RID: 26463 RVA: 0x00205050 File Offset: 0x00203250
		[JsonIgnore]
		public LocationList area
		{
			get
			{
				if (Zone._locationList == null)
				{
					List<Location2D> list = new List<Location2D>();
					for (int i = 0; i < this.Height; i++)
					{
						for (int j = 0; j < this.Width; j++)
						{
							list.Add(Location2D.Get(j, i));
						}
					}
					Zone._locationList = new LocationList(list);
				}
				return Zone._locationList;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06006760 RID: 26464 RVA: 0x002050AC File Offset: 0x002032AC
		public Graveyard Graveyard
		{
			get
			{
				Graveyard graveyard;
				if ((graveyard = this._Graveyard) == null)
				{
					graveyard = (this._Graveyard = Graveyard.Get());
				}
				return graveyard;
			}
		}

		// Token: 0x06006761 RID: 26465 RVA: 0x002050D1 File Offset: 0x002032D1
		public virtual int GetTransitionIntervalTo(Zone Z)
		{
			if (Z != this)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x002050DA File Offset: 0x002032DA
		public virtual string GetCheckpointKey()
		{
			Cell cell = this.GetCell(0, 0);
			if (cell == null)
			{
				return null;
			}
			GameObject firstObject = cell.GetFirstObject("CheckpointWidget");
			if (firstObject == null)
			{
				return null;
			}
			return firstObject.GetStringProperty("CheckpointKey", null);
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x00205105 File Offset: 0x00203305
		public virtual bool IsCheckpoint()
		{
			Cell cell = this.GetCell(0, 0);
			return cell != null && cell.HasObject("CheckpointWidget");
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x00205120 File Offset: 0x00203320
		public void Release()
		{
			this.Stale = true;
			if (this._Graveyard != null)
			{
				Graveyard.Return(ref this._Graveyard);
			}
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					Cell cell = this.Map[i][j];
					for (int k = cell.Objects.Count - 1; k >= 0; k--)
					{
						cell.Objects[k].Pool();
					}
					cell.Objects.Clear();
				}
			}
			Array.Clear(Zone.FloodArray, 0, Zone.FloodArray.Length);
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x002051BA File Offset: 0x002033BA
		public void MarkActive()
		{
			this.LastActive = XRLCore.CurrentTurn;
		}

		// Token: 0x06006766 RID: 26470 RVA: 0x002051C7 File Offset: 0x002033C7
		public void MarkActive(Zone AlongWith)
		{
			this.LastActive = XRLCore.CurrentTurn;
			if (AlongWith != null && AlongWith != this)
			{
				AlongWith.MarkActive();
			}
		}

		// Token: 0x06006767 RID: 26471 RVA: 0x002051E4 File Offset: 0x002033E4
		public string GetDefaultWall()
		{
			string zoneProperty = this.GetZoneProperty("DefaultWall", "");
			if (zoneProperty != "")
			{
				return zoneProperty;
			}
			string text = "Shale";
			GameObject terrainObject = this.GetTerrainObject();
			if (terrainObject != null && this.Z <= 15)
			{
				text = terrainObject.GetTag("DefaultWall", "Shale");
			}
			if ((text == "Shale" || text == "Limestone") && this.Z > 49)
			{
				text = "Granite";
			}
			return text;
		}

		// Token: 0x06006768 RID: 26472 RVA: 0x00205267 File Offset: 0x00203467
		public void loadMap(string file)
		{
			new MapBuilder
			{
				ID = file
			}.BuildZone(this);
		}

		// Token: 0x06006769 RID: 26473 RVA: 0x0020527C File Offset: 0x0020347C
		public void SetActive()
		{
			The.ZoneManager.SetActiveZone(this.ZoneID);
		}

		// Token: 0x0600676A RID: 26474 RVA: 0x0020528F File Offset: 0x0020348F
		public void SetMusic(string Track)
		{
			this.GetCell(0, 0).RequireObject("ZoneMusic", null, null, null, null).SetStringProperty("Track", Track, false);
		}

		// Token: 0x0600676B RID: 26475 RVA: 0x002052B4 File Offset: 0x002034B4
		public void PlayMusic()
		{
			GameObject firstObject = this.GetCell(0, 0).GetFirstObject("ZoneMusic");
			XRL.World.Parts.Music music;
			if (firstObject != null && firstObject.TryGetPart<XRL.World.Parts.Music>(out music))
			{
				music.TryMusic();
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600676C RID: 26476 RVA: 0x002052E7 File Offset: 0x002034E7
		public int Level
		{
			get
			{
				return this.NewTier * 5;
			}
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x002052F4 File Offset: 0x002034F4
		public void AddSemanticTag(string newTag)
		{
			string text = this.GetZoneProperty("SemanticTags", "");
			if (text != "")
			{
				text += ",";
			}
			text += newTag;
			this.SetZoneProperty("SemanticTags", text);
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x0020533F File Offset: 0x0020353F
		public List<string> GetSemanticTags()
		{
			return this.GetZoneProperty("SemanticTags", "*Default").Split(',', StringSplitOptions.None).ToList<string>();
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x00205360 File Offset: 0x00203560
		public int NewTier
		{
			get
			{
				if (this._NewTier > 0)
				{
					return this._NewTier;
				}
				string text = The.ZoneManager.GetZoneProperty(this.ZoneID, "ZoneTierOverride", false, null) as string;
				if (!text.IsNullOrEmpty() && int.TryParse(text, out this._NewTier))
				{
					return this._NewTier;
				}
				ZoneBlueprint blueprint = this.GetBlueprint();
				if (blueprint != null && blueprint.Tier > 0)
				{
					return this._NewTier = blueprint.Tier;
				}
				GameObject terrainObject = this.GetTerrainObject();
				text = ((terrainObject != null) ? terrainObject.GetTag("RegionTier", null) : null) ?? "1";
				if (!int.TryParse(text, out this._NewTier))
				{
					this._NewTier = 1;
				}
				if (this.Z > 15)
				{
					this._NewTier = Math.Abs(this.Z - 16) / 5 + 2;
				}
				if (this._NewTier < 1)
				{
					this._NewTier = 1;
				}
				if (this._NewTier > 8)
				{
					this._NewTier = 8;
				}
				return this._NewTier;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06006770 RID: 26480 RVA: 0x00205459 File Offset: 0x00203659
		public Location2D ResolvedLocation
		{
			get
			{
				return Location2D.Get(this.ResolvedX, this.ResolvedY);
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06006771 RID: 26481 RVA: 0x0020546C File Offset: 0x0020366C
		public int ResolvedX
		{
			get
			{
				return this.wX * 3 + this.X;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06006772 RID: 26482 RVA: 0x0020547D File Offset: 0x0020367D
		public int ResolvedY
		{
			get
			{
				return this.wY * 3 + this.Y;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06006773 RID: 26483 RVA: 0x0020548E File Offset: 0x0020368E
		public int ResolvedStartX
		{
			get
			{
				return this.ResolvedX * 80;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06006774 RID: 26484 RVA: 0x00205499 File Offset: 0x00203699
		public int ResolvedStartY
		{
			get
			{
				return this.ResolvedY * 25;
			}
		}

		// Token: 0x06006775 RID: 26485 RVA: 0x002054A4 File Offset: 0x002036A4
		public string getZoneWorld()
		{
			if (this.ZoneID == null)
			{
				return null;
			}
			if (!this.ZoneID.Contains("."))
			{
				return this.ZoneID;
			}
			return this.ZoneID.Substring(0, this.ZoneID.IndexOf('.'));
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06006777 RID: 26487 RVA: 0x00205516 File Offset: 0x00203716
		// (set) Token: 0x06006776 RID: 26486 RVA: 0x002054E2 File Offset: 0x002036E2
		public virtual string ZoneID
		{
			get
			{
				return this._ZoneID;
			}
			set
			{
				this._ZoneID = value;
				XRL.World.ZoneID.Parse(value, out this.ZoneWorld, out this.wX, out this.wY, out this.X, out this.Y, out this.Z);
			}
		}

		// Token: 0x06006778 RID: 26488 RVA: 0x00205520 File Offset: 0x00203720
		public void SetInfluenceMapWalls(int[,] Ret)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.HasWall() || cell.HasObjectWithTag("EnsureVoidBlocker") || cell.HasObjectWithTag("InfluenceMapBlocker"))
					{
						Ret[j, i] = 1;
					}
					else
					{
						Ret[j, i] = 0;
					}
				}
			}
		}

		// Token: 0x06006779 RID: 26489 RVA: 0x00205590 File Offset: 0x00203790
		public void SetInfluenceMapWallsDoors(int[,] Ret)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.HasWall() || cell.HasObjectWithPart("Door"))
					{
						Ret[j, i] = 1;
					}
					else
					{
						Ret[j, i] = 0;
					}
				}
			}
		}

		// Token: 0x0600677A RID: 26490 RVA: 0x002055F4 File Offset: 0x002037F4
		public void UpdateSoundMap()
		{
			if (!Zone.SoundMapDirty)
			{
				return;
			}
			GameObject player = The.Player;
			if (player.CurrentCell != null)
			{
				for (int i = 0; i < this.Height; i++)
				{
					for (int j = 0; j < this.Width; j++)
					{
						Cell cell = this.Map[j][i];
						if (cell.HasWall() && cell.IsOccludingFor(player))
						{
							Zone.SoundWalls[j, i] = 1;
						}
						else
						{
							Zone.SoundWalls[j, i] = 0;
						}
					}
				}
				Zone.SoundMap.bDraw = false;
				Zone.SoundMap.ClearSeeds();
				Zone.SoundMap.Walls = Zone.SoundWalls;
				Zone.SoundMap.AddSeed(player.CurrentCell.Location, false);
				Zone.SoundMap.RecalculateCostOnly(false);
				Zone.SoundMapDirty = false;
			}
		}

		// Token: 0x0600677B RID: 26491 RVA: 0x002056BF File Offset: 0x002038BF
		public int defaultPathfinderWeightFunc(int x, int y, Cell cell)
		{
			if (cell.HasWall())
			{
				return 9999;
			}
			if (cell.IsSolid())
			{
				return 9999;
			}
			return 0;
		}

		// Token: 0x0600677C RID: 26492 RVA: 0x002056E0 File Offset: 0x002038E0
		public Pathfinder getPathfinder(Func<int, int, Cell, int> weightFunc = null)
		{
			if (weightFunc == null)
			{
				weightFunc = new Func<int, int, Cell, int>(this.defaultPathfinderWeightFunc);
			}
			Pathfinder pathfinder = Pathfinder.next(this.Width, this.Height);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					pathfinder.CurrentNavigationMap[j, i] = weightFunc(j, i, this.GetCell(j, i));
				}
			}
			return pathfinder;
		}

		// Token: 0x0600677D RID: 26493 RVA: 0x00205750 File Offset: 0x00203950
		public void SetInfluenceMapAutoexploreWeightsAndWalls(int[,] Weights, int[,] Walls, bool ExploredOnly = true)
		{
			int num = Zone.CalculateNav(The.Player, true, false, false, false, false, true);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (ExploredOnly && !this.GetExplored(j, i))
					{
						Weights[j, i] = 100;
						Walls[j, i] = 1;
					}
					else
					{
						int num2 = this.GetCell(j, i).NavigationWeight(The.Player, num);
						Weights[j, i] = num2;
						Walls[j, i] = ((num2 >= 95) ? 1 : 0);
					}
				}
			}
		}

		// Token: 0x0600677E RID: 26494 RVA: 0x002057E0 File Offset: 0x002039E0
		public void SetMouseMoveAutoexploreSeeds(InfluenceMap Map, Cell targetCell, out int NumSeeds, out int NumSeedsInBlackout)
		{
			NumSeeds = 0;
			NumSeedsInBlackout = 0;
			Zone.visited.Clear();
			Zone.horizon.Enqueue(targetCell.Location);
			Zone.<>c__DisplayClass156_0 CS$<>8__locals1;
			CS$<>8__locals1.zone = targetCell.ParentZone;
			while (Zone.horizon.Count > 0)
			{
				Location2D location2D = Zone.horizon.Dequeue();
				Zone.visited.Add(location2D);
				if (!CS$<>8__locals1.zone.GetCell(location2D).IsExplored())
				{
					int x = location2D.X;
					int y = location2D.Y;
					Map.AddSeed(x, y, false);
					NumSeeds++;
					if (this.GetLight(x, y) == LightLevel.Blackout)
					{
						NumSeedsInBlackout++;
					}
					Zone.<SetMouseMoveAutoexploreSeeds>g__Check|156_0(Location2D.Get(x - 1, y), ref CS$<>8__locals1);
					Zone.<SetMouseMoveAutoexploreSeeds>g__Check|156_0(Location2D.Get(x + 1, y), ref CS$<>8__locals1);
					Zone.<SetMouseMoveAutoexploreSeeds>g__Check|156_0(Location2D.Get(x, y - 1), ref CS$<>8__locals1);
					Zone.<SetMouseMoveAutoexploreSeeds>g__Check|156_0(Location2D.Get(x, y + 1), ref CS$<>8__locals1);
				}
			}
		}

		// Token: 0x0600677F RID: 26495 RVA: 0x002058CC File Offset: 0x00203ACC
		public void SetInfluenceAutoexploreSeeds(InfluenceMap Map, Rack<GameObject> Objects, ref Cell Unexplored, out int NumSeeds, out int NumSeedsInBlackout)
		{
			bool flag = false;
			bool autoexploreChests = Options.AutoexploreChests;
			if (Zone.LastActCount != AutoAct.Count)
			{
				Zone.LastActCount = AutoAct.Count;
				Objects.Clear();
				Unexplored = null;
				flag = true;
			}
			NumSeeds = 0;
			NumSeedsInBlackout = 0;
			Map.ClearSeeds();
			Cell playerCell = The.PlayerCell;
			int num = 9999999;
			bool flag2 = Unexplored != null && Unexplored.ParentZone == this && Zone.<SetInfluenceAutoexploreSeeds>g__ValidCell|158_0(Unexplored);
			if (!flag2)
			{
				Unexplored = null;
			}
			if (!Objects.IsNullOrEmpty<GameObject>())
			{
				GameObject[] array = Objects.GetArray();
				for (int i = Objects.Count - 1; i >= 0; i--)
				{
					GameObject gameObject = array[i];
					if (Zone.<SetInfluenceAutoexploreSeeds>g__ValidObject|158_1(gameObject, this, autoexploreChests))
					{
						Cell currentCell = gameObject.CurrentCell;
						Map.AddSeed(currentCell.X, currentCell.Y, false);
					}
					else
					{
						Objects.RemoveAt(i);
					}
				}
				if (Objects.Count == 0)
				{
					flag = true;
				}
			}
			if (!flag2 || flag)
			{
				for (int j = 0; j < this.Height; j++)
				{
					for (int k = 0; k < this.Width; k++)
					{
						Cell cell = this.GetCell(k, j);
						bool flag3 = false;
						if (flag)
						{
							bool flag4 = cell.IsSolidFor(The.Player, false);
							GameObject[] array2 = cell.Objects.GetArray();
							int l = 0;
							int count = cell.Objects.Count;
							while (l < count)
							{
								GameObject gameObject2 = array2[l];
								if ((!flag4 || gameObject2.ConsiderSolidFor(The.Player, false)) && Zone.<SetInfluenceAutoexploreSeeds>g__ValidObject|158_1(gameObject2, this, autoexploreChests))
								{
									Objects.Add(gameObject2);
									flag3 = true;
									break;
								}
								l++;
							}
						}
						if (flag3)
						{
							Map.AddSeed(k, j, false);
						}
						else if (!flag2 && Zone.<SetInfluenceAutoexploreSeeds>g__ValidCell|158_0(cell))
						{
							int num2 = playerCell.DistanceTo(k, j);
							if (num2 < num)
							{
								Unexplored = cell;
								num = num2;
							}
						}
					}
				}
			}
			if (Unexplored != null)
			{
				Map.AddSeed(Unexplored.Location, false);
			}
			NumSeeds = Map.Seeds.Count;
			foreach (Location2D location2D in Map.Seeds)
			{
				if (this.GetLight(location2D.X, location2D.Y) == LightLevel.Blackout)
				{
					NumSeedsInBlackout++;
				}
			}
		}

		// Token: 0x06006780 RID: 26496 RVA: 0x00205B20 File Offset: 0x00203D20
		public int SetInfluenceMapStairsUp(InfluenceMap Map)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					for (int k = 0; k < cell.Objects.Count; k++)
					{
						GameObject gameObject = cell.Objects[k];
						if (gameObject.HasPart<XRL.World.Parts.StairsUp>() && !gameObject.HasPropertyOrTag("NoAutowalk") && gameObject.CurrentCell.Explored)
						{
							Hidden part = gameObject.GetPart<Hidden>();
							if (part == null || part.Found)
							{
								HiddenRender part2 = gameObject.GetPart<HiddenRender>();
								if (part2 == null || part2.Found)
								{
									Map.AddSeed(j, i, false);
									num++;
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006781 RID: 26497 RVA: 0x00205BF4 File Offset: 0x00203DF4
		public int SetInfluenceMapStairsDown(InfluenceMap Map)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					for (int k = 0; k < cell.Objects.Count; k++)
					{
						GameObject gameObject = cell.Objects[k];
						if (gameObject.HasPart<XRL.World.Parts.StairsDown>() && !gameObject.HasPropertyOrTag("NoAutowalk") && gameObject.CurrentCell.Explored)
						{
							Hidden part = gameObject.GetPart<Hidden>();
							if (part == null || part.Found)
							{
								HiddenRender part2 = gameObject.GetPart<HiddenRender>();
								if (part2 == null || part2.Found)
								{
									Map.AddSeed(j, i, false);
									num++;
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06006782 RID: 26498 RVA: 0x00205CC8 File Offset: 0x00203EC8
		public static string XYToID(string World, int xp, int yp, int z)
		{
			int num = xp / 3;
			int num2 = yp / 3;
			int num3 = xp % 3;
			int num4 = yp % 3;
			return XRL.World.ZoneID.Assemble(World, num, num2, num3, num4, z);
		}

		// Token: 0x06006783 RID: 26499 RVA: 0x00205CF0 File Offset: 0x00203EF0
		public void FillRoundHollowBox(Box B, string Blueprint)
		{
			if (B.Width < 8 || B.Height < 5)
			{
				for (int i = B.x1 + 1; i <= B.x2 - 1; i++)
				{
					this.GetCell(i, B.y1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					this.GetCell(i, B.y2).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				for (int j = B.y1 + 1; j < B.y2; j++)
				{
					this.GetCell(B.x1, j).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					this.GetCell(B.x2, j).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				this.GetCell(B.x1 + 1, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.GetCell(B.x2 - 1, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.GetCell(B.x1 + 1, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.GetCell(B.x2 - 1, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				return;
			}
			for (int k = B.x1 + 3; k <= B.x2 - 3; k++)
			{
				this.GetCell(k, B.y1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.GetCell(k, B.y2).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			for (int l = B.y1 + 2; l <= B.y2 - 2; l++)
			{
				this.GetCell(B.x1, l).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.GetCell(B.x2, l).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			this.GetCell(B.x1 + 1, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x1 + 2, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x1 + 3, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 1, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 2, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 3, B.y1 + 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x1 + 1, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x1 + 2, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x1 + 3, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 1, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 2, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 3, B.y2 - 1).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x1 + 1, B.y1 + 2).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 1, B.y1 + 2).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x1 + 1, B.y2 - 2).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.GetCell(B.x2 - 1, B.y2 - 2).AddObject(GameObjectFactory.Factory.CreateObject(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x06006784 RID: 26500 RVA: 0x0020631C File Offset: 0x0020451C
		public void ClearWalkableBorders()
		{
			try
			{
				string zoneIDFromDirection = this.GetZoneIDFromDirection("U", 1);
				Zone zone;
				if (zoneIDFromDirection != null && The.ZoneManager.CachedZones.TryGetValue(zoneIDFromDirection, out zone))
				{
					for (int i = 0; i < zone.Height; i++)
					{
						for (int j = 0; j < zone.Width; j++)
						{
							Cell cell = zone.GetCell(j, i);
							if (cell.HasObjectWithBlueprint("OpenShaft"))
							{
								this.GetCell(j, i).ClearWalls();
							}
							if (cell.HasObjectWithBlueprint("StairsDown"))
							{
								this.GetCell(j, i).ClearWalls();
								this.GetCell(j, i).AddObject("StairsUp", null, null, null, null);
							}
						}
					}
				}
				string zoneIDFromDirection2 = this.GetZoneIDFromDirection("D", 1);
				Zone zone2;
				if (zoneIDFromDirection2 != null && The.ZoneManager.CachedZones.TryGetValue(zoneIDFromDirection2, out zone2))
				{
					for (int k = 0; k < zone2.Height; k++)
					{
						for (int l = 0; l < zone2.Width; l++)
						{
							if (zone2.GetCell(l, k).HasObjectWithBlueprint("StairsUp"))
							{
								this.GetCell(l, k).ClearWalls();
								this.GetCell(l, k).AddObject("StairsDown", null, null, null, null);
							}
						}
					}
				}
				string zoneIDFromDirection3 = this.GetZoneIDFromDirection("N", 1);
				Zone zone3;
				if (zoneIDFromDirection3 != null && The.ZoneManager.CachedZones.TryGetValue(zoneIDFromDirection3, out zone3))
				{
					for (int m = 0; m < zone3.Width; m++)
					{
						Cell cell2 = zone3.GetCell(m, zone3.Height - 1);
						if (cell2 != null && !cell2.IsSolid())
						{
							this.GetCell(m, 0).ClearWalls();
						}
					}
				}
				string zoneIDFromDirection4 = this.GetZoneIDFromDirection("S", 1);
				Zone zone4;
				if (zoneIDFromDirection4 != null && The.ZoneManager.CachedZones.TryGetValue(zoneIDFromDirection4, out zone4))
				{
					for (int n = 0; n < zone4.Width; n++)
					{
						Cell cell3 = zone4.GetCell(n, 0);
						if (cell3 != null && !cell3.IsSolid())
						{
							this.GetCell(n, this.Height - 1).ClearWalls();
						}
					}
				}
				string zoneIDFromDirection5 = this.GetZoneIDFromDirection("E", 1);
				Zone zone5;
				if (zoneIDFromDirection5 != null && The.ZoneManager.CachedZones.TryGetValue(zoneIDFromDirection5, out zone5))
				{
					for (int num = 0; num < zone5.Height; num++)
					{
						Cell cell4 = zone5.GetCell(0, num);
						if (cell4 != null && !cell4.IsSolid())
						{
							this.GetCell(this.Width - 1, num).ClearWalls();
						}
					}
				}
				string zoneIDFromDirection6 = this.GetZoneIDFromDirection("W", 1);
				Zone zone6;
				if (zoneIDFromDirection6 != null && The.ZoneManager.CachedZones.TryGetValue(zoneIDFromDirection6, out zone6))
				{
					for (int num2 = 0; num2 < zone6.Height; num2++)
					{
						Cell cell5 = zone6.GetCell(0, num2);
						if (cell5 != null && cell5.IsSolid())
						{
							this.GetCell(0, num2).ClearWalls();
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06006785 RID: 26501 RVA: 0x00206634 File Offset: 0x00204834
		public void FillBox(Box B, string Blueprint, bool clearFirst = false)
		{
			for (int i = B.y1; i <= B.y2; i++)
			{
				for (int j = B.x1; j <= B.x2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null)
					{
						if (clearFirst)
						{
							cell.Clear(null, false, false, null);
						}
						cell.AddObject(GameObject.Create(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x06006786 RID: 26502 RVA: 0x002066A4 File Offset: 0x002048A4
		public void ProcessHollowBox(Box B, Action<Cell> A)
		{
			for (int i = B.x1; i <= B.x2; i++)
			{
				A(this.GetCell(i, B.y1));
				A(this.GetCell(i, B.y2));
			}
			for (int j = B.y1 + 1; j < B.y2; j++)
			{
				A(this.GetCell(B.x1, j));
				A(this.GetCell(B.x2, j));
			}
		}

		// Token: 0x06006787 RID: 26503 RVA: 0x0020672C File Offset: 0x0020492C
		public void FillHollowBox(Box B, string Blueprint)
		{
			for (int i = B.x1; i <= B.x2; i++)
			{
				this.GetCell(i, B.y1).AddObject(GameObject.Create(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.GetCell(i, B.y2).AddObject(GameObject.Create(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			for (int j = B.y1 + 1; j < B.y2; j++)
			{
				this.GetCell(B.x1, j).AddObject(GameObject.Create(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.GetCell(B.x2, j).AddObject(GameObject.Create(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x06006788 RID: 26504 RVA: 0x00206808 File Offset: 0x00204A08
		public void Fill(string Blueprint)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.GetCell(j, i).AddObject(GameObject.Create(Blueprint), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x06006789 RID: 26505 RVA: 0x0020685C File Offset: 0x00204A5C
		public static void Save(SerializationWriter Writer, Zone Z)
		{
			Type type = Z.GetType();
			Writer.WriteTokenized(type);
			Writer.WriteTypeFields(Z, type, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			Z.Save(Writer);
		}

		// Token: 0x0600678A RID: 26506 RVA: 0x00206890 File Offset: 0x00204A90
		public virtual void Save(SerializationWriter Writer)
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.Map[i][j].Save(Writer);
				}
			}
			LightLevel[] lightMap = this.LightMap;
			int num = ((lightMap != null) ? lightMap.Length : 0);
			Writer.WriteOptimized(num);
			for (int k = 0; k < num; k++)
			{
				Writer.Write((byte)this.LightMap[k]);
			}
			Writer.Write(this.ExploredMap);
			Writer.Write(this.VisibilityMap);
			bool[,] reachableMap = this.ReachableMap;
			int num2 = ((reachableMap != null) ? reachableMap.GetLength(0) : 0);
			bool[,] reachableMap2 = this.ReachableMap;
			int num3 = ((reachableMap2 != null) ? reachableMap2.GetLength(1) : 0);
			Writer.WriteOptimized(num2);
			Writer.WriteOptimized(num3);
			for (int l = 0; l < num2; l++)
			{
				for (int m = 0; m < num3; m++)
				{
					Writer.Write(this.ReachableMap[l, m]);
				}
			}
			NavigationWeight[,] navigationMap = this.NavigationMap;
			num2 = ((navigationMap != null) ? navigationMap.GetLength(0) : 0);
			NavigationWeight[,] navigationMap2 = this.NavigationMap;
			num3 = ((navigationMap2 != null) ? navigationMap2.GetLength(1) : 0);
			Writer.WriteOptimized(num2);
			Writer.WriteOptimized(num3);
			for (int n = 0; n < num2; n++)
			{
				for (int num4 = 0; num4 < num3; num4++)
				{
					Writer.WriteOptimized(this.NavigationMap[n, num4].Weight);
				}
			}
			List<IZonePart> parts = this.Parts;
			num = ((parts != null) ? parts.Count : 0);
			Writer.WriteOptimized(num);
			for (int num5 = 0; num5 < num; num5++)
			{
				IZonePart.Save(this.Parts[num5], Writer);
			}
		}

		// Token: 0x0600678B RID: 26507 RVA: 0x00206A3C File Offset: 0x00204C3C
		public static Zone Load(SerializationReader Reader, string ZoneID = null)
		{
			Type type = Reader.ReadTokenizedType();
			Zone zone = Activator.CreateInstance(type, true) as Zone;
			if (!ZoneID.IsNullOrEmpty())
			{
				zone.ZoneID = ZoneID;
			}
			Reader.ReadTypeFields(zone, type, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			zone.Built = false;
			zone.Load(Reader);
			zone.Built = true;
			zone.BroadcastEvent(Event.New("ZoneLoaded", 0, 0, 0));
			return zone;
		}

		// Token: 0x0600678C RID: 26508 RVA: 0x00206AA8 File Offset: 0x00204CA8
		public virtual void Load(SerializationReader Reader)
		{
			this.FloodMap = new int[80, 25];
			this.Map = new Cell[this.Width][];
			this.MissileMap = new MissileMapType[this.Width][];
			for (int i = 0; i < this.Width; i++)
			{
				this.Map[i] = new Cell[this.Height];
				this.MissileMap[i] = new MissileMapType[this.Height];
			}
			for (int j = 0; j < this.Width; j++)
			{
				for (int k = 0; k < this.Height; k++)
				{
					this.Map[j][k] = Cell.Load(Reader, j, k, this);
				}
			}
			int num = Reader.ReadOptimizedInt32();
			this.LightMap = new LightLevel[num];
			for (int l = 0; l < num; l++)
			{
				this.LightMap[l] = (LightLevel)Reader.ReadByte();
			}
			this.ExploredMap = Reader.ReadBooleanArray();
			if (Reader.FileVersion < 407)
			{
				Reader.ReadBooleanArray();
			}
			this.VisibilityMap = Reader.ReadBooleanArray();
			int num2 = Reader.ReadOptimizedInt32();
			int num3 = Reader.ReadOptimizedInt32();
			this.ReachableMap = new bool[num2, num3];
			for (int m = 0; m < num2; m++)
			{
				for (int n = 0; n < num3; n++)
				{
					this.ReachableMap[m, n] = Reader.ReadBoolean();
				}
			}
			num2 = Reader.ReadOptimizedInt32();
			num3 = Reader.ReadOptimizedInt32();
			this.NavigationMap = new NavigationWeight[num2, num3];
			for (int num4 = 0; num4 < num2; num4++)
			{
				for (int num5 = 0; num5 < num3; num5++)
				{
					this.NavigationMap[num4, num5] = new NavigationWeight
					{
						Weight = Reader.ReadOptimizedInt32()
					};
				}
			}
			num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.Parts = new List<IZonePart>(num);
				for (int num6 = 0; num6 < num; num6++)
				{
					IZonePart zonePart = IZonePart.Load(this, Reader);
					if (zonePart != null)
					{
						this.Parts.Add(zonePart);
					}
				}
			}
		}

		// Token: 0x0600678D RID: 26509 RVA: 0x00206CA8 File Offset: 0x00204EA8
		public string GetRegion()
		{
			if (this.region == null)
			{
				this.region = ZoneManager.GetRegionForZone(this);
			}
			return this.region;
		}

		// Token: 0x0600678E RID: 26510 RVA: 0x00206CC4 File Offset: 0x00204EC4
		public virtual ZoneBlueprint GetBlueprint()
		{
			return The.ZoneManager.GetZoneBlueprint(new ZoneRequest(this));
		}

		// Token: 0x0600678F RID: 26511 RVA: 0x00206CD6 File Offset: 0x00204ED6
		public virtual WorldBlueprint ResolveWorldBlueprint()
		{
			return WorldFactory.Factory.getWorld(this.ResolveZoneWorld());
		}

		// Token: 0x06006790 RID: 26512 RVA: 0x00206CE8 File Offset: 0x00204EE8
		public virtual Cell GetWorldCell()
		{
			Zone zone = The.ZoneManager.GetZone(this.GetZoneWorld());
			if (zone == null)
			{
				return null;
			}
			return zone.GetCell(this.wX, this.wY);
		}

		// Token: 0x06006791 RID: 26513 RVA: 0x00206D11 File Offset: 0x00204F11
		public virtual GameObject GetWorldTerrainObject()
		{
			Cell worldCell = this.GetWorldCell();
			if (worldCell == null)
			{
				return null;
			}
			GameObject firstObjectWithPart = worldCell.GetFirstObjectWithPart("TerrainTravel");
			if (firstObjectWithPart == null)
			{
				return null;
			}
			TerrainTravel part = firstObjectWithPart.GetPart<TerrainTravel>();
			if (part == null)
			{
				return null;
			}
			return part.ParentObject;
		}

		// Token: 0x06006792 RID: 26514 RVA: 0x00206D3F File Offset: 0x00204F3F
		public virtual WorldBlueprint GetWorldBlueprint()
		{
			return WorldFactory.Factory.getWorld(this.ZoneWorld);
		}

		// Token: 0x06006793 RID: 26515 RVA: 0x00206D51 File Offset: 0x00204F51
		public virtual GameObject GetTerrainObject()
		{
			return ZoneManager.GetTerrainObjectForZone(this.wX, this.wY, this.ZoneWorld);
		}

		// Token: 0x06006794 RID: 26516 RVA: 0x00206D6C File Offset: 0x00204F6C
		public GameObject GetTerrainObjectFromDirection(string dir)
		{
			int num = this.wX;
			int num2 = this.wY;
			if (dir.Contains("n", CompareOptions.IgnoreCase))
			{
				num2--;
			}
			if (dir.Contains("s", CompareOptions.IgnoreCase))
			{
				num2++;
			}
			if (dir.Contains("e", CompareOptions.IgnoreCase))
			{
				num++;
			}
			if (dir.Contains("w", CompareOptions.IgnoreCase))
			{
				num--;
			}
			return ZoneManager.GetTerrainObjectForZone(num, num2, this.ZoneWorld);
		}

		// Token: 0x06006795 RID: 26517 RVA: 0x00206DDC File Offset: 0x00204FDC
		public string GetTerrainNameFromDirection(string dir)
		{
			int num = this.wX;
			int num2 = this.wY;
			if (dir.Contains("n", CompareOptions.IgnoreCase))
			{
				num2--;
			}
			if (dir.Contains("s", CompareOptions.IgnoreCase))
			{
				num2++;
			}
			if (dir.Contains("e", CompareOptions.IgnoreCase))
			{
				num++;
			}
			if (dir.Contains("w", CompareOptions.IgnoreCase))
			{
				num--;
			}
			GameObject terrainObjectForZone = ZoneManager.GetTerrainObjectForZone(num, num2, this.ZoneWorld);
			if (terrainObjectForZone == null)
			{
				return "";
			}
			return terrainObjectForZone.Blueprint;
		}

		// Token: 0x06006796 RID: 26518 RVA: 0x00206E5C File Offset: 0x0020505C
		public string GetTerrainDisplayName(bool Stripped = true)
		{
			if (this.Z > 10)
			{
				return "the deep underground";
			}
			GameObject terrainObject = this.GetTerrainObject();
			if (terrainObject == null)
			{
				return null;
			}
			return terrainObject.t(int.MaxValue, null, null, false, false, false, false, Stripped, true, true, false, null, false, true, false, null, true, null, false);
		}

		// Token: 0x06006797 RID: 26519 RVA: 0x00206EAC File Offset: 0x002050AC
		public string GetTerrainRegion()
		{
			string text = this.GetZoneProperty("Region", "");
			if (text.IsNullOrEmpty())
			{
				GameObject terrainObject = this.GetTerrainObject();
				if (GameObject.Validate(terrainObject))
				{
					text = terrainObject.GetTagOrStringProperty("Terrain", null);
				}
			}
			return text;
		}

		// Token: 0x06006798 RID: 26520 RVA: 0x00206EF0 File Offset: 0x002050F0
		public void ReplaceAll(string blueprint, string with)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					List<GameObject> objects = this.GetCell(j, i).GetObjects(blueprint);
					if (objects != null && objects.Count > 0)
					{
						foreach (GameObject gameObject in objects)
						{
							gameObject.Destroy(null, false, false, null);
						}
						this.GetCell(j, i).AddObject(with, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x06006799 RID: 26521 RVA: 0x00206F90 File Offset: 0x00205190
		public GameObject FindObjectByID(string id)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject gameObject = this.GetCell(j, i).FindObjectByID(id);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600679A RID: 26522 RVA: 0x00206FD4 File Offset: 0x002051D4
		public GameObject FindObjectByID(int ID)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject gameObject = this.GetCell(j, i).FindObjectByID(ID);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600679B RID: 26523 RVA: 0x00207018 File Offset: 0x00205218
		public int GetObjectCount(string Blueprint)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					num += this.GetCell(j, i).GetObjectCount(Blueprint);
				}
			}
			return num;
		}

		// Token: 0x0600679C RID: 26524 RVA: 0x0020705C File Offset: 0x0020525C
		public GameObject FindFirstObject(string Blueprint)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject firstObject = this.GetCell(j, i).GetFirstObject(Blueprint);
					if (firstObject != null)
					{
						return firstObject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600679D RID: 26525 RVA: 0x002070A0 File Offset: 0x002052A0
		public GameObject FindFirstObject(Predicate<GameObject> Filter)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject firstObject = this.GetCell(j, i).GetFirstObject(Filter);
					if (firstObject != null)
					{
						return firstObject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600679E RID: 26526 RVA: 0x002070E4 File Offset: 0x002052E4
		public void FindObjects(List<GameObject> Store, Predicate<GameObject> Filter)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.GetCell(j, i).FindObjects(Store, Filter);
				}
			}
		}

		// Token: 0x0600679F RID: 26527 RVA: 0x00207124 File Offset: 0x00205324
		public List<GameObject> FindObjects(Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.FindObjects(list, Filter);
			return list;
		}

		// Token: 0x060067A0 RID: 26528 RVA: 0x00207140 File Offset: 0x00205340
		public void FindObjects(List<GameObject> List, string Blueprint)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.GetCell(j, i).GetObjects(Blueprint, List);
				}
			}
		}

		// Token: 0x060067A1 RID: 26529 RVA: 0x00207180 File Offset: 0x00205380
		public List<GameObject> FindObjects(string Blueprint)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.FindObjects(list, Blueprint);
			return list;
		}

		// Token: 0x060067A2 RID: 26530 RVA: 0x0020719C File Offset: 0x0020539C
		public void FindObjectsWithTagOrProperty(List<GameObject> List, string Name)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.GetCell(j, i).GetObjectsWithTagOrProperty(List, Name);
				}
			}
		}

		// Token: 0x060067A3 RID: 26531 RVA: 0x002071DC File Offset: 0x002053DC
		public List<GameObject> FindObjectsWithTagOrProperty(string Name)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.FindObjectsWithTagOrProperty(list, Name);
			return list;
		}

		// Token: 0x060067A4 RID: 26532 RVA: 0x002071F8 File Offset: 0x002053F8
		public void FindObjectsWithPart(List<GameObject> List, string Name)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.GetCell(j, i).GetObjectsWithPart(Name, List);
				}
			}
		}

		// Token: 0x060067A5 RID: 26533 RVA: 0x00207238 File Offset: 0x00205438
		public List<GameObject> FindObjectsWithPart(string Name)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.FindObjectsWithPart(list, Name);
			return list;
		}

		// Token: 0x060067A6 RID: 26534 RVA: 0x00207254 File Offset: 0x00205454
		public bool HasObject(GameObject Object)
		{
			return Object.CurrentZone == this;
		}

		// Token: 0x060067A7 RID: 26535 RVA: 0x00207260 File Offset: 0x00205460
		public bool HasObjectWithTag(string Tag)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.GetCell(j, i).HasObjectWithTag(Tag))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067A8 RID: 26536 RVA: 0x002072A4 File Offset: 0x002054A4
		public bool HasObjectWithTagOrProperty(string Tag)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.GetCell(j, i).HasObjectWithTagOrProperty(Tag))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067A9 RID: 26537 RVA: 0x002072E8 File Offset: 0x002054E8
		public bool HasObject(Predicate<GameObject> Test)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.GetCell(j, i).HasObject(Test))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067AA RID: 26538 RVA: 0x0020732C File Offset: 0x0020552C
		public bool HasObject(string Blueprint)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.GetCell(j, i).HasObject(Blueprint))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067AB RID: 26539 RVA: 0x00207370 File Offset: 0x00205570
		public GameObject FindObject(Predicate<GameObject> Test)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject gameObject = this.GetCell(j, i).FindObject(Test);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x002073B4 File Offset: 0x002055B4
		public GameObject FindObject(string Blueprint)
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					GameObject gameObject = this.GetCell(i, j).FindObject(Blueprint);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x002073F8 File Offset: 0x002055F8
		public GameObject FindObjectExcludingSelf(string Blueprint, GameObject self)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject gameObject = this.GetCell(j, i).FindObjectExcept(Blueprint, self);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x060067AE RID: 26542 RVA: 0x00207440 File Offset: 0x00205640
		public GameObject FindClosestObject(GameObject Source, Predicate<GameObject> Predicate, bool ExploredOnly = false, bool VisibleOnly = true, bool IncludeSelf = true)
		{
			int num = 9999999;
			GameObject gameObject = null;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (!ExploredOnly || cell.Explored)
					{
						int num2 = cell.PathDistanceTo(Source.CurrentCell);
						if (num2 < num)
						{
							int k = 0;
							int count = cell.Objects.Count;
							while (k < count)
							{
								GameObject gameObject2 = cell.Objects[k];
								if (Predicate(gameObject2) && (IncludeSelf || gameObject2 != Source) && (!VisibleOnly || (gameObject2.Render != null && gameObject2.Render.Visible)) && !gameObject2.HasPropertyOrTag("NoAutowalk") && gameObject2.CurrentCell != null)
								{
									num = num2;
									gameObject = gameObject2;
								}
								k++;
							}
						}
					}
				}
			}
			return gameObject;
		}

		// Token: 0x060067AF RID: 26543 RVA: 0x0020752C File Offset: 0x0020572C
		public GameObject FindClosestObject(GameObject Source, string Blueprint, bool ExploredOnly = false, bool VisibleOnly = true, bool IncludeSelf = true)
		{
			int num = 9999999;
			GameObject gameObject = null;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (!ExploredOnly || cell.Explored)
					{
						int num2 = cell.PathDistanceTo(Source.CurrentCell);
						if (num2 < num)
						{
							int k = 0;
							int count = cell.Objects.Count;
							while (k < count)
							{
								GameObject gameObject2 = cell.Objects[k];
								if (!(gameObject2.Blueprint != Blueprint) && (IncludeSelf || gameObject2 != Source) && (!VisibleOnly || (gameObject2.Render != null && gameObject2.Render.Visible)) && !gameObject2.HasPropertyOrTag("NoAutowalk") && gameObject2.CurrentCell != null)
								{
									num = num2;
									gameObject = gameObject2;
								}
								k++;
							}
						}
					}
				}
			}
			return gameObject;
		}

		// Token: 0x060067B0 RID: 26544 RVA: 0x0020761C File Offset: 0x0020581C
		public GameObject FindClosestObjectWithPart(GameObject Source, string Part, bool ExploredOnly = false, bool IncludeSelf = true)
		{
			int num = 9999;
			GameObject gameObject = null;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					int k = 0;
					int count = cell.Objects.Count;
					while (k < count)
					{
						GameObject gameObject2 = cell.Objects[k];
						if (gameObject2.HasPart(Part) && (IncludeSelf || gameObject2 != Source) && !gameObject2.HasPropertyOrTag("NoAutowalk") && gameObject2.CurrentCell != null && gameObject2.CurrentCell.PathDistanceTo(Source.CurrentCell) < num && (!ExploredOnly || gameObject2.CurrentCell.Explored))
						{
							Hidden part = gameObject2.GetPart<Hidden>();
							if (part == null || part.Found)
							{
								HiddenRender part2 = gameObject2.GetPart<HiddenRender>();
								if (part2 == null || part2.Found)
								{
									num = gameObject2.CurrentCell.PathDistanceTo(Source.CurrentCell);
									gameObject = gameObject2;
								}
							}
						}
						k++;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x060067B1 RID: 26545 RVA: 0x00207734 File Offset: 0x00205934
		public GameObject FindClosestObjectWithTag(GameObject Source, string Tag, bool ExploredOnly = false)
		{
			int num = 9999;
			GameObject gameObject = null;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					int k = 0;
					int count = cell.Objects.Count;
					while (k < count)
					{
						GameObject gameObject2 = cell.Objects[k];
						if (gameObject2.HasTag(Tag) && !gameObject2.HasPropertyOrTag("NoAutowalk") && gameObject2.CurrentCell != null && gameObject2.CurrentCell.PathDistanceTo(Source.CurrentCell) < num && (!ExploredOnly || gameObject2.CurrentCell.Explored))
						{
							Hidden part = gameObject2.GetPart<Hidden>();
							if (part == null || part.Found)
							{
								HiddenRender part2 = gameObject2.GetPart<HiddenRender>();
								if (part2 == null || part2.Found)
								{
									num = gameObject2.CurrentCell.PathDistanceTo(Source.CurrentCell);
									gameObject = gameObject2;
								}
							}
						}
						k++;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x00207840 File Offset: 0x00205A40
		public List<GameObject> GetObjects()
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					num += this.Map[j][i].GetObjectCount();
				}
			}
			List<GameObject> list = new List<GameObject>(num);
			if (num > 0)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						list.AddRange(this.Map[l][k].Objects);
						if (list.Count >= num)
						{
							return list;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x002078DC File Offset: 0x00205ADC
		public List<GameObject> GetObjectsReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].GetObjects(list);
				}
			}
			return list;
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x00207924 File Offset: 0x00205B24
		public List<GameObject> GetObjects(string Blueprint)
		{
			List<Cell> cellsWithObject = this.GetCellsWithObject(Blueprint);
			int num = 0;
			foreach (Cell cell in cellsWithObject)
			{
				num += cell.GetObjectCount(Blueprint);
			}
			List<GameObject> list = new List<GameObject>(num);
			if (num > 0)
			{
				foreach (Cell cell2 in cellsWithObject)
				{
					list.AddRange(cell2.GetObjects(Blueprint));
					if (list.Count >= num)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x002079E0 File Offset: 0x00205BE0
		public List<GameObject> GetObjectsReadonly(string Blueprint)
		{
			List<GameObject> list = Event.NewGameObjectList();
			foreach (Cell cell in this.GetCells())
			{
				cell.GetObjects(Blueprint, list);
			}
			return list;
		}

		// Token: 0x060067B6 RID: 26550 RVA: 0x00207A3C File Offset: 0x00205C3C
		public IEnumerable<GameObject> YieldObjects()
		{
			int num;
			for (int y = 0; y < this.Height; y = num + 1)
			{
				for (int x = 0; x < this.Width; x = num + 1)
				{
					for (int i = 0; i < this.Map[x][y].Objects.Count; i = num + 1)
					{
						yield return this.Map[x][y].Objects[i];
						num = i;
					}
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x00207A4C File Offset: 0x00205C4C
		public Zone.ObjectEnumerator IterateObjects()
		{
			return new Zone.ObjectEnumerator(this);
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x00207A54 File Offset: 0x00205C54
		public int CountObjects(Predicate<GameObject> pFilter)
		{
			int num = 0;
			foreach (Cell cell in this.GetCells())
			{
				foreach (GameObject gameObject in cell.Objects)
				{
					if (gameObject != null && pFilter(gameObject))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x00207AEC File Offset: 0x00205CEC
		public int CountObjects(string Blueprint)
		{
			int num = 0;
			foreach (Cell cell in this.GetCells())
			{
				foreach (GameObject gameObject in cell.Objects)
				{
					if (gameObject != null && gameObject.Blueprint == Blueprint)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x00207B8C File Offset: 0x00205D8C
		public List<GameObject> GetObjects(Predicate<GameObject> Filter)
		{
			List<Cell> cellsWithObject = this.GetCellsWithObject(Filter);
			int num = 0;
			foreach (Cell cell in cellsWithObject)
			{
				num += cell.GetObjectCount(Filter);
			}
			List<GameObject> list = new List<GameObject>(num);
			if (num > 0)
			{
				foreach (Cell cell2 in cellsWithObject)
				{
					list.AddRange(cell2.GetObjects(Filter));
					if (list.Count >= num)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x00207C48 File Offset: 0x00205E48
		public List<GameObject> GetObjectsReadonly(Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			foreach (Cell cell in this.GetCells())
			{
				cell.GetObjects(Filter, list);
			}
			return list;
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x00207CA4 File Offset: 0x00205EA4
		public static Location2D zoneIDTo240x72Location(string zoneID)
		{
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			ReadOnlySpan<char> readOnlySpan3;
			ReadOnlySpan<char> readOnlySpan4;
			ReadOnlySpan<char> readOnlySpan5;
			zoneID.AsDelimitedSpans('.', out readOnlySpan, out readOnlySpan2, out readOnlySpan3, out readOnlySpan4, out readOnlySpan5);
			Location2D location2D;
			try
			{
				location2D = Location2D.Get(int.Parse(readOnlySpan2, NumberStyles.Integer, null) * 3 + int.Parse(readOnlySpan4, NumberStyles.Integer, null), int.Parse(readOnlySpan3, NumberStyles.Integer, null) * 3 + int.Parse(readOnlySpan5, NumberStyles.Integer, null));
			}
			catch (FormatException)
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"Error parsing spans for ZoneID '",
					zoneID,
					"': PX = ",
					readOnlySpan2.ToString(),
					", PY = ",
					readOnlySpan3.ToString(),
					", ZX = ",
					readOnlySpan4.ToString(),
					", ZY = ",
					readOnlySpan5.ToString()
				}));
				int num;
				int num2;
				int num3;
				int num4;
				XRL.World.ZoneID.Parse(zoneID, out num, out num2, out num3, out num4);
				location2D = Location2D.Get(num * 3 + num3, num2 * 3 + num4);
			}
			return location2D;
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x00207DAC File Offset: 0x00205FAC
		public List<GameObject> GetObjectsThatInheritFrom(string Blueprint)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					list.AddRange(this.Map[i][j].GetObjectsThatInheritFrom(Blueprint));
				}
			}
			return list;
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x00207DF8 File Offset: 0x00205FF8
		public List<GameObject> GetObjectsWithTagOrProperty(string Property, bool UseEventList = false)
		{
			List<GameObject> list;
			if (!UseEventList)
			{
				list = new List<GameObject>();
			}
			else
			{
				list = Event.NewGameObjectList();
			}
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					list.AddRange(this.Map[i][j].GetObjectsWithTagOrProperty(Property));
				}
			}
			return list;
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x00207E50 File Offset: 0x00206050
		public List<GameObject> GetObjectsWithProperty(string Property)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].GetObjectsWithProperty(Property, list);
				}
			}
			return list;
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x00207E98 File Offset: 0x00206098
		public List<GameObject> GetObjectsWithPart(string Part)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].GetObjectsWithPart(Part, list);
				}
			}
			return list;
		}

		// Token: 0x060067C1 RID: 26561 RVA: 0x00207EE0 File Offset: 0x002060E0
		public List<GameObject> GetObjectsWithPartReadonly(string Part)
		{
			List<GameObject> list = Event.NewGameObjectList();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].GetObjectsWithPart(Part, list);
				}
			}
			return list;
		}

		// Token: 0x060067C2 RID: 26562 RVA: 0x00207F28 File Offset: 0x00206128
		public GameObject GetFirstObjectWithPart(string Part)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject firstObjectWithPart = this.Map[j][i].GetFirstObjectWithPart(Part);
					if (firstObjectWithPart != null)
					{
						return firstObjectWithPart;
					}
				}
			}
			return null;
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x00207F70 File Offset: 0x00206170
		public GameObject GetFirstObjectWithPropertyOrTag(string Name)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject firstObjectWithPropertyOrTag = this.Map[j][i].GetFirstObjectWithPropertyOrTag(Name);
					if (firstObjectWithPropertyOrTag != null)
					{
						return firstObjectWithPropertyOrTag;
					}
				}
			}
			return null;
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x00207FB6 File Offset: 0x002061B6
		public IEnumerable<GameObject> LoopObjectsWithPart(string Part)
		{
			int num;
			for (int y = 0; y < this.Height; y = num + 1)
			{
				for (int x = 0; x < this.Width; x = num + 1)
				{
					foreach (GameObject gameObject in this.Map[x][y].LoopObjectsWithPart(Part))
					{
						yield return gameObject;
					}
					IEnumerator<GameObject> enumerator = null;
					num = x;
				}
				num = y;
			}
			yield break;
			yield break;
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x00207FD0 File Offset: 0x002061D0
		public void ForeachObjectWithPart(string Part, Action<GameObject> aProc)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].ForeachObjectWithPart(Part, aProc);
				}
			}
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x00208010 File Offset: 0x00206210
		public void ForeachObject(Action<GameObject> aProc)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].ForeachObject(aProc);
				}
			}
		}

		// Token: 0x060067C7 RID: 26567 RVA: 0x0020804F File Offset: 0x0020624F
		public bool HasBuilder(string builderName)
		{
			return The.ZoneManager.ZoneHasBuilder(this.ZoneID, builderName);
		}

		// Token: 0x060067C8 RID: 26568 RVA: 0x00208064 File Offset: 0x00206264
		public bool HasTryingToJoinPartyLeader()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasTryingToJoinPartyLeader())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067C9 RID: 26569 RVA: 0x002080A8 File Offset: 0x002062A8
		public bool HasTryingToJoinPartyLeaderForZoneUncaching()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasTryingToJoinPartyLeaderForZoneUncaching())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067CA RID: 26570 RVA: 0x002080EC File Offset: 0x002062EC
		public bool HasPlayerLed()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasPlayerLed())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067CB RID: 26571 RVA: 0x00208130 File Offset: 0x00206330
		public bool HasWasPlayer()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasWasPlayer())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067CC RID: 26572 RVA: 0x00208174 File Offset: 0x00206374
		public bool HasLeftBehindByPlayer()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasLeftBehindByPlayer())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060067CD RID: 26573 RVA: 0x002081B8 File Offset: 0x002063B8
		public void ForeachObjectWithTagOrProperty(string Name, Action<GameObject> aProc)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].ForeachObjectWithTagOrProperty(Name, aProc);
				}
			}
		}

		// Token: 0x060067CE RID: 26574 RVA: 0x002081F8 File Offset: 0x002063F8
		public GameObject GetObjectWithTag(string tag)
		{
			List<GameObject> objectsWithTag = this.GetObjectsWithTag(tag);
			if (objectsWithTag != null && objectsWithTag.Count > 0)
			{
				return objectsWithTag.GetRandomElement<GameObject>(null);
			}
			return null;
		}

		// Token: 0x060067CF RID: 26575 RVA: 0x00208224 File Offset: 0x00206424
		public List<GameObject> GetObjectsWithTag(string Tag)
		{
			List<GameObject> list = Event.NewGameObjectList();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					list.AddRange(this.Map[j][i].GetObjectsWithTag(Tag));
				}
			}
			return list;
		}

		// Token: 0x060067D0 RID: 26576 RVA: 0x00208270 File Offset: 0x00206470
		public GameObject GetAnyObjectWithTag(string Tag)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject objectWithTag = this.Map[j][i].GetObjectWithTag(Tag);
					if (objectWithTag != null)
					{
						return objectWithTag;
					}
				}
			}
			return null;
		}

		// Token: 0x060067D1 RID: 26577 RVA: 0x002082B8 File Offset: 0x002064B8
		public List<Point> LineFromAngle(int x0, int y0, int degrees)
		{
			float num = (float)degrees / 58f;
			float num2 = (float)Math.Sin((double)num);
			float num3 = (float)Math.Cos((double)num);
			List<Point> list = new List<Point>();
			char c = '2';
			float num4 = (float)x0;
			float num5 = (float)y0;
			int num6 = -1;
			int num7 = -1;
			for (int i = 0; i < 10000; i++)
			{
				int num8 = (int)Math.Round((double)num4, MidpointRounding.AwayFromZero);
				int num9 = (int)Math.Round((double)num5, MidpointRounding.AwayFromZero);
				if (num8 < 0 || num9 < 0 || num8 >= this.Width || num9 >= this.Height)
				{
					break;
				}
				if (num8 != num6 || num9 != num7)
				{
					list.Add(new Point(num8, num9, 0, c));
					if (num6 != -1 && num7 != -1)
					{
						if (num8 != num6 && num9 != num7)
						{
							if ((num8 > num6 && num9 > num7) || (num8 < num6 && num9 < num7))
							{
								c = '/';
							}
							else
							{
								c = '\\';
							}
						}
						else if (num8 != num6)
						{
							c = '-';
						}
						else
						{
							c = '|';
						}
					}
					num6 = num8;
					num7 = num9;
					num4 += num2;
					num5 += num3;
				}
			}
			list[0].DisplayChar = '@';
			list[list.Count - 1].DisplayChar = 'X';
			return list;
		}

		// Token: 0x060067D2 RID: 26578 RVA: 0x002083EC File Offset: 0x002065EC
		public static List<Point> Line(int x0, int y0, int x1, int y1, bool ReadOnly = false)
		{
			List<Point> list = (ReadOnly ? Zone.LineReturnCache : new List<Point>(Math.Abs(y1 - y0) + Math.Abs(x1 - x0) + 1));
			Zone.Line(x0, y0, x1, y1, list);
			return list;
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x00208428 File Offset: 0x00206628
		public static void Line(int x0, int y0, int x1, int y1, List<Point> Return)
		{
			bool flag = false;
			Return.Clear();
			if (x0 == x1 && y0 == y1)
			{
				Return.Add(new Point(x0, y0, 0, 'X'));
			}
			else
			{
				bool flag2 = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
				if (flag2)
				{
					int num = x0;
					x0 = y0;
					y0 = num;
					int num2 = x1;
					x1 = y1;
					y1 = num2;
				}
				if (x0 > x1)
				{
					flag = true;
					int num3 = x1;
					x1 = x0;
					x0 = num3;
					int num4 = y1;
					y1 = y0;
					y0 = num4;
				}
				int num5 = x1 - x0;
				double num6 = (double)Math.Abs(y1 - y0);
				char c = '2';
				double num7 = 0.0;
				double num8 = num6 / (double)num5;
				int num9 = y0;
				int num10;
				if (y0 < y1)
				{
					num10 = 1;
				}
				else
				{
					num10 = -1;
				}
				int num11 = 0;
				for (int i = x0; i <= x1; i++)
				{
					num11++;
					if (flag2)
					{
						Return.Add(new Point(num9, i, 0, c));
					}
					else
					{
						Return.Add(new Point(i, num9, 0, c));
					}
					num7 += num8;
					if (num7 >= 0.5)
					{
						num9 += num10;
						num7 -= 1.0;
						if (num10 < 0)
						{
							c = '/';
						}
						else
						{
							c = '\\';
						}
					}
					else if (flag2)
					{
						c = '|';
					}
					else
					{
						c = '-';
					}
				}
			}
			if (flag)
			{
				Return.Reverse();
			}
			Return[0].DisplayChar = '@';
			Return[Return.Count - 1].DisplayChar = 'X';
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060067D4 RID: 26580 RVA: 0x00208578 File Offset: 0x00206778
		// (set) Token: 0x060067D5 RID: 26581 RVA: 0x002085AC File Offset: 0x002067AC
		public string DisplayName
		{
			get
			{
				string text;
				if ((text = this._DisplayName) == null)
				{
					text = (this._DisplayName = The.ZoneManager.GetZoneDisplayName(this.ZoneID, false, false, true, true));
				}
				return text;
			}
			set
			{
				this._DisplayName = value;
				The.ZoneManager.SetZoneDisplayName(this.ZoneID, value, false);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060067D6 RID: 26582 RVA: 0x002085C7 File Offset: 0x002067C7
		// (set) Token: 0x060067D7 RID: 26583 RVA: 0x002085DA File Offset: 0x002067DA
		public string BaseDisplayName
		{
			get
			{
				return The.ZoneManager.GetZoneBaseDisplayName(this.ZoneID, true);
			}
			set
			{
				The.ZoneManager.SetZoneBaseDisplayName(this.ZoneID, value, true);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060067D8 RID: 26584 RVA: 0x002085EE File Offset: 0x002067EE
		public string ReferenceDisplayName
		{
			get
			{
				return The.ZoneManager.GetZoneReferenceDisplayName(this.ZoneID);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060067D9 RID: 26585 RVA: 0x00208600 File Offset: 0x00206800
		// (set) Token: 0x060067DA RID: 26586 RVA: 0x00208612 File Offset: 0x00206812
		public string NameContext
		{
			get
			{
				return The.ZoneManager.GetZoneNameContext(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneNameContext(this.ZoneID, value, true);
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060067DB RID: 26587 RVA: 0x00208626 File Offset: 0x00206826
		// (set) Token: 0x060067DC RID: 26588 RVA: 0x00208638 File Offset: 0x00206838
		public bool HasProperName
		{
			get
			{
				return The.ZoneManager.GetZoneHasProperName(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneHasProperName(this.ZoneID, new bool?(value));
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060067DD RID: 26589 RVA: 0x00208650 File Offset: 0x00206850
		// (set) Token: 0x060067DE RID: 26590 RVA: 0x00208662 File Offset: 0x00206862
		public string IndefiniteArticle
		{
			get
			{
				return The.ZoneManager.GetZoneIndefiniteArticle(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneIndefiniteArticle(this.ZoneID, value);
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060067DF RID: 26591 RVA: 0x00208675 File Offset: 0x00206875
		// (set) Token: 0x060067E0 RID: 26592 RVA: 0x00208687 File Offset: 0x00206887
		public string DefiniteArticle
		{
			get
			{
				return The.ZoneManager.GetZoneDefiniteArticle(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneDefiniteArticle(this.ZoneID, value);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060067E1 RID: 26593 RVA: 0x0020869A File Offset: 0x0020689A
		// (set) Token: 0x060067E2 RID: 26594 RVA: 0x002086AC File Offset: 0x002068AC
		public bool IncludeContextInZoneDisplay
		{
			get
			{
				return The.ZoneManager.GetZoneIncludeContextInZoneDisplay(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneIncludeContextInZoneDisplay(this.ZoneID, new bool?(value));
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060067E3 RID: 26595 RVA: 0x002086C4 File Offset: 0x002068C4
		// (set) Token: 0x060067E4 RID: 26596 RVA: 0x002086D6 File Offset: 0x002068D6
		public bool IncludeStratumInZoneDisplay
		{
			get
			{
				return The.ZoneManager.GetZoneIncludeStratumInZoneDisplay(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneIncludeStratumInZoneDisplay(this.ZoneID, new bool?(value));
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060067E5 RID: 26597 RVA: 0x002086EE File Offset: 0x002068EE
		// (set) Token: 0x060067E6 RID: 26598 RVA: 0x00208700 File Offset: 0x00206900
		public bool NamedByPlayer
		{
			get
			{
				return The.ZoneManager.GetZoneNamedByPlayer(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneNamedByPlayer(this.ZoneID, value);
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060067E7 RID: 26599 RVA: 0x00208713 File Offset: 0x00206913
		// (set) Token: 0x060067E8 RID: 26600 RVA: 0x00208725 File Offset: 0x00206925
		public string AmbientBed
		{
			get
			{
				return The.ZoneManager.GetZoneAmbientBed(this.ZoneID);
			}
			set
			{
				The.Game.SetStringGameState("ZoneAmbientBed_" + this.ZoneID, value);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060067E9 RID: 26601 RVA: 0x00208742 File Offset: 0x00206942
		public string AmbientSounds
		{
			get
			{
				return The.ZoneManager.GetZoneAmbientSounds(this.ZoneID);
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060067EA RID: 26602 RVA: 0x00208754 File Offset: 0x00206954
		// (set) Token: 0x060067EB RID: 26603 RVA: 0x00208766 File Offset: 0x00206966
		public int AmbientVolume
		{
			get
			{
				return The.ZoneManager.GetZoneAmbientVolume(this.ZoneID);
			}
			set
			{
				The.Game.SetIntGameState("ZoneAmbientVolume_" + this.ZoneID, value);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060067EC RID: 26604 RVA: 0x00208783 File Offset: 0x00206983
		// (set) Token: 0x060067ED RID: 26605 RVA: 0x00208795 File Offset: 0x00206995
		public bool HasWeather
		{
			get
			{
				return The.ZoneManager.GetZoneHasWeather(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneHasWeather(this.ZoneID, new bool?(value));
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060067EE RID: 26606 RVA: 0x002087AD File Offset: 0x002069AD
		// (set) Token: 0x060067EF RID: 26607 RVA: 0x002087BF File Offset: 0x002069BF
		public string WindSpeed
		{
			get
			{
				return The.ZoneManager.GetZoneWindSpeed(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneWindSpeed(this.ZoneID, value);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x060067F0 RID: 26608 RVA: 0x002087D2 File Offset: 0x002069D2
		// (set) Token: 0x060067F1 RID: 26609 RVA: 0x002087E4 File Offset: 0x002069E4
		public string WindDirections
		{
			get
			{
				return The.ZoneManager.GetZoneWindDirections(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneWindDirections(this.ZoneID, value);
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x002087F7 File Offset: 0x002069F7
		// (set) Token: 0x060067F3 RID: 26611 RVA: 0x00208809 File Offset: 0x00206A09
		public string WindDuration
		{
			get
			{
				return The.ZoneManager.GetZoneWindDuration(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneWindDuration(this.ZoneID, value);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x060067F4 RID: 26612 RVA: 0x0020881C File Offset: 0x00206A1C
		// (set) Token: 0x060067F5 RID: 26613 RVA: 0x0020882E File Offset: 0x00206A2E
		public int CurrentWindSpeed
		{
			get
			{
				return The.ZoneManager.GetZoneCurrentWindSpeed(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneCurrentWindSpeed(this.ZoneID, value);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x060067F6 RID: 26614 RVA: 0x00208841 File Offset: 0x00206A41
		// (set) Token: 0x060067F7 RID: 26615 RVA: 0x00208853 File Offset: 0x00206A53
		public string CurrentWindDirection
		{
			get
			{
				return The.ZoneManager.GetZoneCurrentWindDirection(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneCurrentWindDirection(this.ZoneID, value);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060067F8 RID: 26616 RVA: 0x00208866 File Offset: 0x00206A66
		// (set) Token: 0x060067F9 RID: 26617 RVA: 0x00208878 File Offset: 0x00206A78
		public long NextWindChange
		{
			get
			{
				return The.ZoneManager.GetZoneNextWindChange(this.ZoneID);
			}
			set
			{
				The.ZoneManager.SetZoneNextWindChange(this.ZoneID, value);
			}
		}

		// Token: 0x060067FA RID: 26618 RVA: 0x0020888B File Offset: 0x00206A8B
		public string GetDisplayName(bool WithIndefiniteArticle = false, bool WithDefiniteArticle = false, bool WithStratum = true, bool Mutate = true)
		{
			return The.ZoneManager.GetZoneDisplayName(this.ZoneID, WithIndefiniteArticle, WithDefiniteArticle, WithStratum, Mutate);
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x002088A2 File Offset: 0x00206AA2
		public bool IsOutside()
		{
			return !this.IsInside();
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x002088B0 File Offset: 0x00206AB0
		public bool IsInside()
		{
			string text;
			if (The.ZoneManager.TryGetZoneProperty<string>(this.ZoneID, "Inside", out text))
			{
				return text == "1";
			}
			return !this.IsWorldMap() && this.Z > 10;
		}

		// Token: 0x060067FD RID: 26621 RVA: 0x002088F8 File Offset: 0x00206AF8
		public void SetInside(bool? State)
		{
			if (State == null)
			{
				The.ZoneManager.RemoveZoneProperty(this.ZoneID, "Inside");
				return;
			}
			The.ZoneManager.SetZoneProperty(this.ZoneID, "Inside", State.Value ? "1" : "0");
		}

		// Token: 0x060067FE RID: 26622 RVA: 0x0020894F File Offset: 0x00206B4F
		public string SpecialUpMessage()
		{
			return this.GetZoneProperty("SpecialUpMessage", "");
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x00208961 File Offset: 0x00206B61
		public bool HasZoneProperty(string name)
		{
			return The.ZoneManager.GetZoneProperty(this.ZoneID, name, false, null) != null;
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x00208979 File Offset: 0x00206B79
		public void SetZoneProperty(string Name, string Value)
		{
			The.ZoneManager.SetZoneProperty(this.ZoneID, Name, Value);
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x0020898D File Offset: 0x00206B8D
		public string GetZoneProperty(string Name, string Default = "")
		{
			return The.ZoneManager.GetZoneProperty(this.ZoneID, Name, false, Default) as string;
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x002089A7 File Offset: 0x00206BA7
		public void RemoveZoneProperty(string Name)
		{
			The.ZoneManager.RemoveZoneProperty(this.ZoneID, Name);
		}

		// Token: 0x06006803 RID: 26627 RVA: 0x002089BB File Offset: 0x00206BBB
		public void ClearZoneConnectionCache()
		{
			this.ZoneConnectionCache.Clear();
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x002089C8 File Offset: 0x00206BC8
		public void WriteZoneConnectionCache()
		{
			foreach (CachedZoneConnection cachedZoneConnection in this.ZoneConnectionCache)
			{
				this.AddZoneConnection(cachedZoneConnection.TargetDirection, cachedZoneConnection.X, cachedZoneConnection.Y, cachedZoneConnection.Type, cachedZoneConnection.Object);
			}
			this.ZoneConnectionCache.Clear();
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x00208A44 File Offset: 0x00206C44
		public void CacheZoneConnection(string TargetZoneDirection, Location2D location, string Type, string ConnectionObject = null)
		{
			this.CacheZoneConnection(TargetZoneDirection, location.X, location.Y, Type, ConnectionObject);
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x00208A5C File Offset: 0x00206C5C
		public void CacheZoneConnection(string TargetZoneDirection, int X, int Y, string Type, string ConnectionObject)
		{
			this.ZoneConnectionCache.Add(new CachedZoneConnection(TargetZoneDirection, X, Y, Type, ConnectionObject));
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x00208A75 File Offset: 0x00206C75
		public void CacheZoneConnection(string TargetZoneDirection, Point P, string Type, string ConnectionObject)
		{
			this.ZoneConnectionCache.Add(new CachedZoneConnection(TargetZoneDirection, P.X, P.Y, Type, ConnectionObject));
		}

		// Token: 0x06006808 RID: 26632 RVA: 0x00208A97 File Offset: 0x00206C97
		public void AddZoneConnection(string TargetZoneDirection, int X, int Y, string Type, string ConnectionObject)
		{
			if (!this.Built)
			{
				this.CacheZoneConnection(TargetZoneDirection, X, Y, Type, ConnectionObject);
				return;
			}
			The.ZoneManager.AddZoneConnection(this.ZoneID, TargetZoneDirection, X, Y, Type, ConnectionObject);
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x00208AC8 File Offset: 0x00206CC8
		public void RemoveZoneConnection(string TargetZoneDirection, int X, int Y, string Type, string ConnectionObject)
		{
			if (!this.Built)
			{
				for (int i = this.ZoneConnectionCache.Count - 1; i >= 0; i--)
				{
					CachedZoneConnection cachedZoneConnection = this.ZoneConnectionCache[i];
					if (cachedZoneConnection.X == X && cachedZoneConnection.Y == Y && !(cachedZoneConnection.TargetDirection != TargetZoneDirection) && !(cachedZoneConnection.Type != Type) && !(cachedZoneConnection.Object != ConnectionObject))
					{
						this.ZoneConnectionCache.RemoveAt(i);
					}
				}
				return;
			}
			The.ZoneManager.RemoveZoneConnection(this.ZoneID, TargetZoneDirection, X, Y, Type, ConnectionObject);
		}

		// Token: 0x0600680A RID: 26634 RVA: 0x00208B64 File Offset: 0x00206D64
		public IEnumerable<ZoneConnection> EnumerateConnections()
		{
			foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(this.ZoneID))
			{
				yield return zoneConnection;
			}
			List<ZoneConnection>.Enumerator enumerator = default(List<ZoneConnection>.Enumerator);
			if (this.ZoneConnectionCache != null)
			{
				foreach (CachedZoneConnection cachedZoneConnection in this.ZoneConnectionCache)
				{
					if (cachedZoneConnection.TargetDirection == "-")
					{
						yield return cachedZoneConnection;
					}
				}
				List<CachedZoneConnection>.Enumerator enumerator2 = default(List<CachedZoneConnection>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x00208B74 File Offset: 0x00206D74
		public Zone()
		{
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x00208BD8 File Offset: 0x00206DD8
		public Zone(int _Width, int _Height)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.Map = new Cell[this.Width][];
			this.MissileMap = new MissileMapType[this.Width][];
			for (int i = 0; i < this.Width; i++)
			{
				this.Map[i] = new Cell[this.Height];
				this.MissileMap[i] = new MissileMapType[this.Height];
				for (int j = 0; j < this.Height; j++)
				{
					this.Map[i][j] = new Cell(this);
					this.Map[i][j].X = i;
					this.Map[i][j].Y = j;
					this.Map[i][j].Live = true;
					this.MissileMap[i][j] = MissileMapType.Empty;
				}
			}
			this.ExploredMap = new bool[this.Width * this.Height];
			this.VisibilityMap = new bool[this.Width * this.Height];
			this.ReachableMap = new bool[this.Width, this.Height];
			this.LightMap = new LightLevel[this.Width * this.Height];
			this.NavigationMap = new NavigationWeight[this.Width, this.Height];
			for (int k = 0; k < this.Height; k++)
			{
				for (int l = 0; l < this.Width; l++)
				{
					this.NavigationMap[l, k] = new NavigationWeight();
				}
			}
			this.ClearReachableMap(true);
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x00208DB4 File Offset: 0x00206FB4
		public void Clear(string BlueprintsToKeep = null)
		{
			this.ClearBox(new Box(0, 0, this.Width - 1, this.Height - 1), BlueprintsToKeep, null, null);
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x00208DD8 File Offset: 0x00206FD8
		public void Clear(IEnumerable<Location2D> cells, string blueprint = null)
		{
			foreach (Location2D location2D in cells)
			{
				Cell cell = this.GetCell(location2D);
				if (cell != null)
				{
					cell.Clear(blueprint, false, false, null);
				}
			}
		}

		// Token: 0x0600680F RID: 26639 RVA: 0x00208E30 File Offset: 0x00207030
		public void ClearBox(Box B, Action<Cell> after = null)
		{
			this.ClearBox(B, null, null, after);
		}

		// Token: 0x06006810 RID: 26640 RVA: 0x00208E3C File Offset: 0x0020703C
		public void ClearBox(Rect2D R)
		{
			this.ClearBox(new Box(R.x1, R.y1, R.x2, R.y2), null);
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x00208E62 File Offset: 0x00207062
		public void ClearBoxWith(Rect2D rect, string blueprint, Action<Cell> after = null)
		{
			this.ClearBox(new Box(rect.x1, rect.y1, rect.x2, rect.y2), null, blueprint, after);
		}

		// Token: 0x06006812 RID: 26642 RVA: 0x00208E8C File Offset: 0x0020708C
		public void ClearBox(Box B, string BlueprintsToKeep, string with = null, Action<Cell> after = null)
		{
			for (int i = B.y1; i <= B.y2; i++)
			{
				for (int j = B.x1; j <= B.x2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null)
					{
						if (!cell.HasObject(The.Player))
						{
							if (BlueprintsToKeep == null)
							{
								cell.Clear(null, false, false, null);
								if (with != null)
								{
									this.GetCell(j, i).AddObject(with, null, null, null, null);
								}
							}
							else
							{
								List<GameObject> list = Event.NewGameObjectList();
								foreach (GameObject gameObject in cell.Objects)
								{
									if ((BlueprintsToKeep == null || !BlueprintsToKeep.Contains(gameObject.Blueprint)) && gameObject.CanClear(false, false))
									{
										list.Add(gameObject);
									}
								}
								foreach (GameObject gameObject2 in list)
								{
									cell.Objects.Remove(gameObject2);
								}
								if (with != null)
								{
									cell.AddObject(with, null, null, null, null);
								}
							}
						}
						if (after != null)
						{
							after(cell);
						}
					}
				}
			}
		}

		// Token: 0x06006813 RID: 26643 RVA: 0x00208FEC File Offset: 0x002071EC
		public static bool ColorsVisible(LightLevel Light)
		{
			if (Light <= LightLevel.Light)
			{
				if (Light != LightLevel.Darkvision && Light != LightLevel.Light)
				{
					return false;
				}
			}
			else if (Light != LightLevel.Interpolight && Light != LightLevel.LitRadar && Light != LightLevel.Omniscient)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06006814 RID: 26644 RVA: 0x00209020 File Offset: 0x00207220
		public void ClearLightMap()
		{
			for (int i = 0; i < this.Width * this.Height; i++)
			{
				this.LightMap[i] = LightLevel.None;
			}
		}

		// Token: 0x06006815 RID: 26645 RVA: 0x0020904E File Offset: 0x0020724E
		public void ClearVisiblityMap()
		{
			Array.Clear(this.VisibilityMap, 0, this.Width * this.Height);
		}

		// Token: 0x06006816 RID: 26646 RVA: 0x00209069 File Offset: 0x00207269
		public void ClearExploredMap()
		{
			Array.Clear(this.ExploredMap, 0, this.Width * this.Height);
		}

		// Token: 0x06006817 RID: 26647 RVA: 0x00209084 File Offset: 0x00207284
		[Obsolete("Legacy amnesia behaviour, use ClearExploredMap")]
		public void ClearFakeUnexploredMap()
		{
		}

		// Token: 0x06006818 RID: 26648 RVA: 0x00209086 File Offset: 0x00207286
		public void SetReachable(int x, int y, bool reachable = true)
		{
			this.ReachableMap[x, y] = reachable;
		}

		// Token: 0x06006819 RID: 26649 RVA: 0x00209096 File Offset: 0x00207296
		public void SetLight(int x, int y, LightLevel v)
		{
			this.LightMap[x + y * this.Width] = v;
		}

		// Token: 0x0600681A RID: 26650 RVA: 0x002090AA File Offset: 0x002072AA
		public void SetVisibility(int x, int y, bool v)
		{
			this.VisibilityMap[x + y * this.Width] = v;
			if (v)
			{
				this.ExploredMap[x + y * this.Width] = true;
			}
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x002090D4 File Offset: 0x002072D4
		public List<Tuple<Cell, char>> GetLine(int x0, int y0, int x1, int y1, bool IncludeSolid = false, GameObject UseTargetability = null)
		{
			List<Tuple<Cell, char>> list = new List<Tuple<Cell, char>>();
			bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (flag)
			{
				int num = x0;
				x0 = y0;
				y0 = num;
				int num2 = x1;
				x1 = y1;
				y1 = num2;
			}
			int num3 = x1 - x0;
			int num4 = Math.Abs(y1 - y0);
			int num5 = Math.Abs(num3) / 2;
			int num6 = y0;
			int num7;
			if (y0 < y1)
			{
				num7 = 1;
			}
			else
			{
				num7 = -1;
			}
			if (x0 > x1)
			{
				for (int i = x0; i >= x1; i--)
				{
					char c;
					Cell cell;
					if (flag)
					{
						if (num7 == 0)
						{
							c = '|';
						}
						else if (num7 < 0)
						{
							c = '/';
						}
						else
						{
							c = '\\';
						}
						cell = this.Map[num6][i];
					}
					else
					{
						if (num7 == 0)
						{
							c = '-';
						}
						else if (num7 < 0)
						{
							c = '/';
						}
						else
						{
							c = '\\';
						}
						cell = this.Map[i][num6];
					}
					list.Add(new Tuple<Cell, char>(cell, c));
					if (i != x0 && i != x1)
					{
						if (cell.IsOccluding())
						{
							return list;
						}
						if (IncludeSolid)
						{
							if (UseTargetability == null)
							{
								if (cell.IsSolid(true))
								{
									return list;
								}
							}
							else if (cell.IsSolidFor(UseTargetability, false))
							{
								return list;
							}
						}
					}
					num5 -= num4;
					if (num5 < 0)
					{
						num6 += num7;
						num5 += Math.Abs(num3);
					}
				}
			}
			else
			{
				for (int j = x0; j <= x1; j++)
				{
					Cell cell;
					char c2;
					if (flag)
					{
						if (num7 == 0)
						{
							c2 = '|';
						}
						else if (num7 < 0)
						{
							c2 = '\\';
						}
						else
						{
							c2 = '/';
						}
						cell = this.Map[num6][j];
					}
					else
					{
						if (num7 == 0)
						{
							c2 = '-';
						}
						else if (num7 < 0)
						{
							c2 = '\\';
						}
						else
						{
							c2 = '/';
						}
						cell = this.Map[j][num6];
					}
					list.Add(new Tuple<Cell, char>(cell, c2));
					if (j != x0 && j != x1)
					{
						if (cell.IsOccluding())
						{
							return list;
						}
						if (IncludeSolid)
						{
							if (UseTargetability == null)
							{
								if (cell.IsSolid(true))
								{
									return list;
								}
							}
							else if (cell.IsSolidFor(UseTargetability, false))
							{
								return list;
							}
						}
					}
					num5 -= num4;
					if (num5 < 0)
					{
						num6 += num7;
						num5 += Math.Abs(num3);
					}
				}
			}
			return list;
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x002092E0 File Offset: 0x002074E0
		public bool CalculateLOS(int x0, int y0, int x1, int y1, bool IncludeSolid = false, bool BlackoutStops = false, GameObject UseTargetability = null, Predicate<Cell> OverrideBlocking = null, int PassBlocks = 0, bool BlockContinue = false, bool CheckOcclusion = true)
		{
			int num = y1 - y0;
			int num2 = (num ^ (num >> 31)) - (num >> 31);
			int num3 = x1 - x0;
			int num4 = (num3 ^ (num3 >> 31)) - (num3 >> 31);
			bool flag = num2 > num4;
			if (flag)
			{
				int num5 = x0;
				x0 = y0;
				y0 = num5;
				int num6 = x1;
				x1 = y1;
				y1 = num6;
			}
			int num7 = x1 - x0;
			num = y1 - y0;
			int num8 = (num ^ (num >> 31)) - (num >> 31);
			int num9 = ((num7 ^ (num7 >> 31)) - (num7 >> 31)) / 2;
			int num10 = y0;
			int num11;
			if (y0 < y1)
			{
				num11 = 1;
			}
			else
			{
				num11 = -1;
			}
			int num12 = 0;
			if (x0 > x1)
			{
				for (int i = x0; i >= x1; i--)
				{
					Cell cell;
					if (flag)
					{
						cell = this.Map[num10][i];
					}
					else
					{
						cell = this.Map[i][num10];
					}
					if (i != x0 && i != x1)
					{
						if (BlockContinue && num12 > 0)
						{
							if (++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (OverrideBlocking != null)
						{
							if (OverrideBlocking(cell) && ++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (CheckOcclusion && (cell.OccludeCache == 1 || (cell.OccludeCache != 0 && cell.IsOccluding())))
						{
							if (++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (BlackoutStops && cell.IsBlackedOut())
						{
							if (++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (IncludeSolid)
						{
							if (UseTargetability == null)
							{
								if (cell.IsSolid(true) && ++num12 > PassBlocks)
								{
									return false;
								}
							}
							else if (cell.IsSolidFor(UseTargetability, false) && ++num12 > PassBlocks)
							{
								return false;
							}
						}
					}
					num9 -= num8;
					if (num9 < 0)
					{
						num10 += num11;
						num9 += (num7 ^ (num7 >> 31)) - (num7 >> 31);
					}
				}
			}
			else
			{
				for (int j = x0; j <= x1; j++)
				{
					Cell cell;
					if (flag)
					{
						cell = this.Map[num10][j];
					}
					else
					{
						cell = this.Map[j][num10];
					}
					if (j != x0 && j != x1)
					{
						if (BlockContinue && num12 > 0)
						{
							if (++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (OverrideBlocking != null)
						{
							if (OverrideBlocking(cell) && ++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (cell.OccludeCache == 1 || (cell.OccludeCache != 0 && cell.IsOccluding()))
						{
							if (++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (BlackoutStops && cell.IsBlackedOut())
						{
							if (++num12 > PassBlocks)
							{
								return false;
							}
						}
						else if (IncludeSolid && cell.IsSolidForProjectile(null, UseTargetability, null, null, null, null, false) && ++num12 > PassBlocks)
						{
							return false;
						}
					}
					num9 -= num8;
					if (num9 < 0)
					{
						num10 += num11;
						num9 += (num7 ^ (num7 >> 31)) - (num7 >> 31);
					}
				}
			}
			return true;
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x002095BC File Offset: 0x002077BC
		public bool CalculateLOS(Cell C, int x1, int y1, bool IncludeSolid = false, bool BlackoutStops = false, GameObject UseTargetability = null, Predicate<Cell> OverrideBlocking = null, int PassBlocks = 0, bool BlockContinue = false, bool CheckOcclusion = true)
		{
			return C != null && this.CalculateLOS(C.X, C.Y, x1, y1, IncludeSolid, BlackoutStops, UseTargetability, OverrideBlocking, PassBlocks, BlockContinue, CheckOcclusion);
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x002095F0 File Offset: 0x002077F0
		public bool CalculateLOS(Cell C, Cell TC, bool IncludeSolid = false, bool BlackoutStops = false, GameObject UseTargetability = null, Predicate<Cell> OverrideBlocking = null, int PassBlocks = 0, bool BlockContinue = false)
		{
			return TC != null && this.CalculateLOS(C, TC.X, TC.Y, IncludeSolid, BlackoutStops, UseTargetability, OverrideBlocking, PassBlocks, BlockContinue, true);
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x00209624 File Offset: 0x00207824
		public bool CalculateLOS(GameObject GO, int x1, int y1, bool IncludeSolid = false, bool BlackoutStops = false, GameObject UseTargetability = null, Predicate<Cell> OverrideBlocking = null, int PassBlocks = 0, bool BlockContinue = false)
		{
			return this.CalculateLOS((GO != null) ? GO.CurrentCell : null, x1, y1, IncludeSolid, BlackoutStops, UseTargetability, OverrideBlocking, PassBlocks, BlockContinue, true);
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x00209654 File Offset: 0x00207854
		public bool HasUnobstructedLineTo(int x0, int y0, int x1, int y1, bool BlackoutStops = false, GameObject UseTargetability = null)
		{
			bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (flag)
			{
				int num = x0;
				x0 = y0;
				y0 = num;
				int num2 = x1;
				x1 = y1;
				y1 = num2;
			}
			int num3 = x1 - x0;
			int num4 = Math.Abs(y1 - y0);
			int num5 = Math.Abs(num3) / 2;
			int num6 = y0;
			int num7;
			if (y0 < y1)
			{
				num7 = 1;
			}
			else
			{
				num7 = -1;
			}
			if (x0 > x1)
			{
				for (int i = x0; i >= x1; i--)
				{
					Cell cell;
					if (flag)
					{
						cell = this.Map[num6][i];
					}
					else
					{
						cell = this.Map[i][num6];
					}
					if (i != x0 && i != x1)
					{
						if (BlackoutStops && cell.IsBlackedOut())
						{
							return false;
						}
						if (UseTargetability == null)
						{
							if (cell.IsSolid(true))
							{
								return false;
							}
						}
						else if (cell.IsSolidFor(UseTargetability, false))
						{
							return false;
						}
					}
					num5 -= num4;
					if (num5 < 0)
					{
						num6 += num7;
						num5 += Math.Abs(num3);
					}
				}
			}
			else
			{
				for (int j = x0; j <= x1; j++)
				{
					Cell cell;
					if (flag)
					{
						cell = this.Map[num6][j];
					}
					else
					{
						cell = this.Map[j][num6];
					}
					if (j != x0 && j != x1 && cell.IsSolid(true))
					{
						if (BlackoutStops && cell.IsBlackedOut())
						{
							return false;
						}
						if (UseTargetability == null)
						{
							if (cell.IsSolid(true))
							{
								return false;
							}
						}
						else if (cell.IsSolidFor(UseTargetability, false))
						{
							return false;
						}
					}
					num5 -= num4;
					if (num5 < 0)
					{
						num6 += num7;
						num5 += Math.Abs(num3);
					}
				}
			}
			return true;
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x002097CC File Offset: 0x002079CC
		public bool CalculateLOSFloat(int xstart, int ystart, int xend, int yend)
		{
			int num = (int)Math.Sqrt((double)((xstart - xend) * (xstart - xend) + (ystart - yend) * (ystart - yend)));
			float num2 = (float)xstart + 0.5f;
			float num3 = (float)ystart + 0.5f;
			float num4 = ((float)xend + 0.5f - num2) / (float)num;
			float num5 = ((float)yend + 0.5f - num3) / (float)num;
			for (int i = 0; i < num - 1; i++)
			{
				num2 += num4;
				num3 += num5;
				int num6 = (int)Math.Floor((double)num2);
				int num7 = (int)Math.Floor((double)num3);
				if (this.Map[num6][num7].IsOccluding())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x0020986C File Offset: 0x00207A6C
		public void LightAll(LightLevel Level = LightLevel.Light)
		{
			for (int i = 0; i < this.Width * this.Height; i++)
			{
				if (this.LightMap[i] < Level)
				{
					this.LightMap[i] = Level;
				}
			}
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x002098A8 File Offset: 0x00207AA8
		public void UnlightAll()
		{
			for (int i = 0; i < this.Width * this.Height; i++)
			{
				if (this.LightMap[i] < LightLevel.Interpolight)
				{
					this.LightMap[i] = LightLevel.None;
				}
			}
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x002098E8 File Offset: 0x00207AE8
		public void BlackoutAll()
		{
			for (int i = 0; i < this.Width * this.Height; i++)
			{
				if (this.LightMap[i] < LightLevel.Interpolight)
				{
					this.LightMap[i] = LightLevel.Blackout;
				}
			}
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x00209928 File Offset: 0x00207B28
		public void VisAll()
		{
			for (int i = 0; i < this.Width * this.Height; i++)
			{
				this.VisibilityMap[i] = true;
			}
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x00209956 File Offset: 0x00207B56
		public void RemoveLight(int x, int y, int r)
		{
			this.RemoveLight(x, y, r, LightLevel.None, LightLevel.Interpolight);
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x00209967 File Offset: 0x00207B67
		public void AddLight(int x, int y, int r)
		{
			this.AddLight(x, y, r, LightLevel.Light, false);
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x00209978 File Offset: 0x00207B78
		public void RemoveLight(int x, int y, int r, LightLevel Level, LightLevel RemoveUpTo = LightLevel.Interpolight)
		{
			if (this.GetLight(x, y) < RemoveUpTo)
			{
				this.SetLight(x, y, Level);
			}
			int num = x - r;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = y - r;
			if (num2 < 0)
			{
				num2 = 0;
			}
			r *= r;
			while (num < this.Width && num <= x + r)
			{
				int num3 = num2;
				while (num3 < this.Height && num3 <= y + r)
				{
					if ((num - x) * (num - x) + (num3 - y) * (num3 - y) <= r && this.GetLight(num, num3) < RemoveUpTo && this.CalculateLOS(x, y, num, num3, false, false, null, null, 0, false, true))
					{
						this.SetLight(num, num3, Level);
					}
					num3++;
				}
				num++;
			}
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x00209A1B File Offset: 0x00207C1B
		public static bool BlocksRadar(Cell C)
		{
			return C.BlocksRadar();
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x00209A24 File Offset: 0x00207C24
		public void MixLight(int x, int y, LightLevel Level, bool Force = false)
		{
			if (Force)
			{
				this.SetLight(x, y, Level);
				if (Level >= LightLevel.Interpolight)
				{
					this.SetVisibility(x, y, true);
				}
				return;
			}
			LightLevel light = this.GetLight(x, y);
			if (Level == LightLevel.Radar && light > LightLevel.None && light < LightLevel.Radar)
			{
				this.SetLight(x, y, LightLevel.LitRadar);
				this.SetVisibility(x, y, true);
				return;
			}
			if (light == LightLevel.Radar && Level > LightLevel.None && Level < LightLevel.Radar)
			{
				if (this.CalculateLOS(The.Player, x, y, false, false, null, null, 0, false))
				{
					this.SetLight(x, y, LightLevel.LitRadar);
					this.SetVisibility(x, y, true);
				}
				return;
			}
			if (Level > light)
			{
				this.SetLight(x, y, Level);
				if (Level >= LightLevel.Interpolight)
				{
					this.SetVisibility(x, y, true);
				}
				return;
			}
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x00209AE4 File Offset: 0x00207CE4
		public void MixLight(int x, int y, int xp, int yp, LightLevel Level, bool Force = false)
		{
			if (Force)
			{
				this.SetLight(xp, yp, Level);
				if (Level >= LightLevel.Interpolight)
				{
					this.SetVisibility(xp, yp, true);
				}
				return;
			}
			LightLevel light = this.GetLight(xp, yp);
			if (Level == LightLevel.Radar && light > LightLevel.None && light < LightLevel.Radar && this.CalculateLOS(x, y, xp, yp, true, false, null, new Predicate<Cell>(Zone.BlocksRadar), 0, false, true))
			{
				if (this.CalculateLOS(The.Player, xp, yp, false, false, null, null, 0, false))
				{
					this.SetLight(xp, yp, LightLevel.LitRadar);
				}
				else
				{
					this.SetLight(xp, yp, LightLevel.Radar);
				}
				this.SetVisibility(xp, yp, true);
				return;
			}
			if (light == LightLevel.Radar && Level > LightLevel.None && Level < LightLevel.Radar && this.CalculateLOS(The.Player, xp, yp, false, false, null, null, 0, false))
			{
				this.SetLight(xp, yp, LightLevel.LitRadar);
				this.SetVisibility(xp, yp, true);
				return;
			}
			if (Level > light)
			{
				if (Level == LightLevel.Radar)
				{
					if (this.CalculateLOS(x, y, xp, yp, true, false, null, new Predicate<Cell>(Zone.BlocksRadar), 0, false, true))
					{
						this.SetLight(xp, yp, Level);
						this.SetVisibility(xp, yp, true);
						return;
					}
				}
				else if (Level == LightLevel.Interpolight)
				{
					if (this.CalculateLOS(x, y, xp, yp, false, false, null, null, 2, true, true))
					{
						this.SetLight(xp, yp, Level);
						this.SetVisibility(xp, yp, true);
						return;
					}
				}
				else if (Level >= LightLevel.Omniscient || this.CalculateLOS(x, y, xp, yp, false, false, null, null, 0, false, true))
				{
					this.SetLight(xp, yp, Level);
					if (Level >= LightLevel.Radar)
					{
						this.SetVisibility(xp, yp, true);
					}
				}
				return;
			}
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x00209C90 File Offset: 0x00207E90
		public void AddLight(int x, int y, int r, LightLevel Level, bool Force = false)
		{
			this.MixLight(x, y, Level, Force);
			if (r <= 0)
			{
				return;
			}
			int num = Math.Max(x - r, 0);
			int num2 = Math.Max(y - r, 0);
			r *= r;
			while (num < this.Width && num <= x + r)
			{
				int num3 = num2;
				while (num3 < this.Height && num3 <= y + r)
				{
					if ((num - x) * (num - x) + (num3 - y) * (num3 - y) <= r && (x != num || y != num3))
					{
						this.MixLight(x, y, num, num3, Level, Force);
					}
					num3++;
				}
				num++;
			}
		}

		// Token: 0x0600682D RID: 26669 RVA: 0x00209D1C File Offset: 0x00207F1C
		public void AddLight(LightLevel Level, bool Force = false)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.MixLight(j, i, Level, Force);
				}
			}
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x00209D58 File Offset: 0x00207F58
		public void AddExplored(int x, int y, int r)
		{
			try
			{
				this.SetVisibility(x, y, true);
				int num = Math.Max(y - r, 0);
				int num2 = Math.Min(y + r, this.Height - 1);
				int num3 = r * r;
				int i = Math.Max(x - r, 0);
				int num4 = Math.Min(x + r, this.Width - 1);
				while (i <= num4)
				{
					for (int j = num; j <= num2; j++)
					{
						int num5 = (i - x) * (i - x) + (j - y) * (j - y);
						if (num5 <= num3 && !this.VisibilityMap[i + j * this.Width] && (num5 <= 1 || (num5 == 2 && Math.Abs(i - x) <= 1 && Math.Abs(j - y) <= 1) || (this.CalculateLOS(x, y, i, j, false, false, null, null, 0, false, true) && this.GetLight(i, j) > LightLevel.None)))
						{
							this.SetVisibility(i, j, true);
						}
					}
					i++;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Vis error", ex, "game_exception");
				throw ex;
			}
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x00209E6C File Offset: 0x0020806C
		public void AddVisibility(int x, int y, int r)
		{
			this.SetVisibility(x, y, true);
			int num = Math.Max(y - r, 0);
			int num2 = Math.Min(y + r, this.Height - 1);
			int num3 = r * r;
			int i = Math.Max(x - r, 0);
			int num4 = Math.Min(x + r, this.Width - 1);
			while (i <= num4)
			{
				for (int j = num; j <= num2; j++)
				{
					int num5 = (i - x) * (i - x) + (j - y) * (j - y);
					if (num5 <= num3 && !this.VisibilityMap[i + j * this.Width] && (num5 <= 1 || (num5 == 2 && Math.Abs(i - x) <= 1 && Math.Abs(j - y) <= 1) || (this.CalculateLOS(x, y, i, j, false, false, null, null, 0, false, true) && this.GetLight(i, j) > LightLevel.None)))
					{
						this.SetVisibility(i, j, true);
					}
				}
				i++;
			}
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x00209F58 File Offset: 0x00208158
		private void CalculateXYRadius(int x, int y, int Radius, out int x1, out int x2, out int y1, out int y2)
		{
			x1 = x - Radius;
			x2 = x + Radius;
			y1 = y - Radius;
			y2 = y + Radius;
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (x2 > 79)
			{
				x2 = 79;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (y2 > 24)
			{
				y2 = 24;
			}
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x00209FAC File Offset: 0x002081AC
		public List<GameObject> FastSquareSearchNoImpassible(int x, int y, int Radius, string SearchPart, GameObject Looker)
		{
			List<GameObject> Return = Event.NewGameObjectList();
			if (this.IsWorldMap())
			{
				return Return;
			}
			int num = 268435456;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateXYRadius(x, y, Radius, out num2, out num3, out num4, out num5);
			Action<GameObject> <>9__0;
			for (int i = num4; i <= num5; i++)
			{
				for (int j = num2; j <= num3; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.NavigationWeight(Looker, ref num) < 100)
					{
						Cell cell2 = cell;
						Action<GameObject> action;
						if ((action = <>9__0) == null)
						{
							action = (<>9__0 = delegate(GameObject o)
							{
								Return.Add(o);
							});
						}
						cell2.ForeachObjectWithPart(SearchPart, action);
					}
				}
			}
			return Return;
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x0020A060 File Offset: 0x00208260
		public List<GameObject> FastSquareSearch(int x, int y, int Radius, string SearchPart)
		{
			if (this.IsWorldMap())
			{
				return Event.NewGameObjectList();
			}
			List<GameObject> list = Event.NewGameObjectList();
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					List<GameObject> objectsViaEventList = this.GetCell(j, i).GetObjectsViaEventList();
					for (int k = 0; k < objectsViaEventList.Count; k++)
					{
						if (objectsViaEventList[k].HasPart(SearchPart))
						{
							list.Add(objectsViaEventList[k]);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x0020A0FC File Offset: 0x002082FC
		public List<GameObject> FastSquareSearch(int x, int y, int Radius, Predicate<GameObject> filter, bool CachedOkay = false)
		{
			if (this.IsWorldMap())
			{
				return Event.NewGameObjectList();
			}
			List<GameObject> list = (CachedOkay ? Event.NewGameObjectList() : null);
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					foreach (GameObject gameObject in this.GetCell(j, i).Objects)
					{
						if (filter(gameObject))
						{
							if (list == null)
							{
								list = new List<GameObject> { gameObject };
							}
							else
							{
								list.Add(gameObject);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x0020A1C8 File Offset: 0x002083C8
		public bool FastSquareSearchAny(int x, int y, int Radius, Predicate<GameObject> filter)
		{
			if (this.IsWorldMap())
			{
				return false;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					foreach (GameObject gameObject in this.GetCell(j, i).Objects)
					{
						if (filter(gameObject))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x0020A270 File Offset: 0x00208470
		public List<GameObject> FastCombatSquareVisibility(int x, int y, int Radius, GameObject Looker, Predicate<GameObject> Filter = null, bool VisibleToPlayerOnly = false, bool IncludeWalls = false, bool IncludeLooker = true, bool NoBrainOnly = true, GameObject skip = null)
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (this.IsWorldMap())
			{
				return list;
			}
			int num = 268435456;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateXYRadius(x, y, Radius, out num2, out num3, out num4, out num5);
			for (int i = num4; i <= num5; i++)
			{
				for (int j = num2; j <= num3; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && (VisibleToPlayerOnly ? cell.IsExploredFor(Looker) : this.ExploredMap[j + i * this.Width]))
					{
						int k = 0;
						int count = cell.Objects.Count;
						while (k < count)
						{
							if (cell.Objects[k].IsCombatObject() && (IncludeLooker || cell.Objects[k] != Looker) && cell.Objects[k] != skip && (Filter == null || Filter(cell.Objects[k])) && (IncludeWalls || cell.NavigationWeight(Looker, ref num) < 100) && (!VisibleToPlayerOnly || cell.Objects[k].IsVisible()))
							{
								list.Add(cell.Objects[k]);
							}
							k++;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006836 RID: 26678 RVA: 0x0020A3E0 File Offset: 0x002085E0
		public List<GameObject> FastSquareVisibility(int x, int y, int Radius, string SearchPart, GameObject Looker, bool VisibleToPlayerOnly = false, bool IncludeWalls = false, GameObject skip = null)
		{
			if (this.IsWorldMap())
			{
				return Event.NewGameObjectList();
			}
			List<GameObject> list = Event.NewGameObjectList();
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			int num5 = 268435456;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && cell.IsExploredFor(Looker) && (IncludeWalls || cell.NavigationWeight(Looker, ref num5) < 100))
					{
						int k = 0;
						int count = cell.Objects.Count;
						while (k < count)
						{
							if (cell.Objects[k] != skip && cell.Objects[k].HasPart(SearchPart) && (!VisibleToPlayerOnly || cell.Objects[k].IsVisible()))
							{
								list.Add(cell.Objects[k]);
							}
							k++;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x0020A500 File Offset: 0x00208700
		public List<GameObject> FastSquareVisibility(int x, int y, int Radius, GameObject Looker, Predicate<GameObject> Filter = null, bool VisibleToPlayerOnly = false, bool IncludeWalls = false)
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (this.IsWorldMap())
			{
				return list;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			int num5 = 268435456;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null)
					{
						if (VisibleToPlayerOnly)
						{
							if (!cell.IsVisible())
							{
								goto IL_00D4;
							}
							if (!cell.IsExploredFor(Looker))
							{
								goto IL_00D4;
							}
						}
						else if (!cell.IsExplored())
						{
							goto IL_00D4;
						}
						if (IncludeWalls || cell.NavigationWeight(Looker, ref num5) < 100)
						{
							int k = 0;
							int count = cell.Objects.Count;
							while (k < count)
							{
								GameObject gameObject = cell.Objects[k];
								if ((Filter == null || Filter(gameObject)) && (!VisibleToPlayerOnly || gameObject.IsVisible()))
								{
									list.Add(gameObject);
								}
								k++;
							}
						}
					}
					IL_00D4:;
				}
			}
			return list;
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x0020A600 File Offset: 0x00208800
		public GameObject FastSquareVisibilityFirst(int x, int y, int Radius, string SearchPart, GameObject Looker, bool VisibleToPlayerOnly = false, bool IncludeWalls = false, GameObject skip = null, int IgnoreFartherThan = 9999999, int IgnoreEasierThan = -2147483648)
		{
			if (this.IsWorldMap())
			{
				return null;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			int num5 = 268435456;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && (IgnoreFartherThan >= 9999999 || Looker == null || Looker.DistanceTo(cell) <= IgnoreFartherThan) && cell.IsExploredFor(Looker) && (IncludeWalls || cell.NavigationWeight(Looker, ref num5) < 100))
					{
						GameObject gameObject = ((IgnoreEasierThan > int.MinValue && Looker != null) ? cell.GetFirstObjectWithPartExcept(SearchPart, skip, IgnoreEasierThan, Looker) : cell.GetFirstObjectWithPartExcept(SearchPart, skip));
						if (gameObject != null && (!VisibleToPlayerOnly || gameObject.IsVisible()))
						{
							return gameObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x0020A6F4 File Offset: 0x002088F4
		public GameObject FastSquareVisibilityFirstBlueprint(int x, int y, int Radius, string blueprint, GameObject Looker, bool VisibleToPlayerOnly = false, bool IncludeWalls = false, GameObject skip = null)
		{
			if (this.IsWorldMap())
			{
				return null;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			int num5 = 268435456;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && cell.IsExploredFor(Looker) && (IncludeWalls || cell.NavigationWeight(Looker, ref num5) < 100))
					{
						GameObject gameObject = (VisibleToPlayerOnly ? cell.GetFirstVisibleObjectExcept(blueprint, skip) : cell.GetFirstObjectExcept(blueprint, skip));
						if (gameObject != null)
						{
							return gameObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x0020A7A4 File Offset: 0x002089A4
		public GameObject FastSquareVisibilityFirst(int x, int y, int Radius, string SearchPart, Predicate<GameObject> Filter, GameObject Looker, bool VisibleToPlayerOnly = false, bool IncludeWalls = false, GameObject skip = null, int IgnoreFartherThan = 9999999, int IgnoreEasierThan = -2147483648)
		{
			if (this.IsWorldMap())
			{
				return null;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			int num5 = 268435456;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && (IgnoreFartherThan >= 9999999 || Looker == null || Looker.DistanceTo(cell) <= IgnoreFartherThan) && cell.IsExploredFor(Looker) && (IncludeWalls || cell.NavigationWeight(Looker, ref num5) < 100))
					{
						GameObject gameObject = (VisibleToPlayerOnly ? ((IgnoreEasierThan > int.MinValue && Looker != null) ? cell.GetFirstVisibleObjectWithPartExcept(SearchPart, Filter, skip, IgnoreEasierThan, Looker) : cell.GetFirstVisibleObjectWithPartExcept(SearchPart, Filter, skip)) : ((IgnoreEasierThan > int.MinValue && Looker != null) ? cell.GetFirstObjectWithPartExcept(SearchPart, Filter, skip, IgnoreEasierThan, Looker) : cell.GetFirstObjectWithPartExcept(SearchPart, Filter, skip)));
						if (gameObject != null)
						{
							return gameObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x0020A8C8 File Offset: 0x00208AC8
		public bool FastSquareVisibilityAny(int x, int y, int Radius, string SearchPart, GameObject Looker, bool VisibleToPlayerOnly = false, bool IncludeWalls = false, GameObject skip = null)
		{
			if (this.IsWorldMap())
			{
				return false;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			int num5 = 268435456;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && cell.IsExploredFor(Looker) && (IncludeWalls || cell.NavigationWeight(Looker, ref num5) < 100) && (VisibleToPlayerOnly ? cell.HasVisibleObjectWithPartExcept(SearchPart, skip) : cell.HasObjectWithPartExcept(SearchPart, skip)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x0020A974 File Offset: 0x00208B74
		public bool FastSquareVisibilityAny(int x, int y, int Radius, string SearchPart, Predicate<GameObject> Filter, GameObject Looker, bool VisibleToPlayerOnly = false, bool IncludeWalls = false, GameObject skip = null)
		{
			if (this.IsWorldMap())
			{
				return false;
			}
			int num;
			int num2;
			int num3;
			int num4;
			this.CalculateXYRadius(x, y, Radius, out num, out num2, out num3, out num4);
			int num5 = 268435456;
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num; j <= num2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell != null && (!VisibleToPlayerOnly || cell.IsVisible()) && cell.IsExploredFor(Looker) && (IncludeWalls || cell.NavigationWeight(Looker, ref num5) < 100) && (VisibleToPlayerOnly ? cell.HasVisibleObjectWithPartExcept(SearchPart, Filter, skip) : cell.HasObjectWithPartExcept(SearchPart, Filter, skip)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x0020AA24 File Offset: 0x00208C24
		public void ClearReachableMap(bool bValue)
		{
			if (this.ReachableMap == null)
			{
				this.ReachableMap = new bool[this.Width, this.Height];
			}
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.ReachableMap[j, i] = bValue;
				}
			}
		}

		// Token: 0x0600683E RID: 26686 RVA: 0x0020AA80 File Offset: 0x00208C80
		public void ClearReachableMap()
		{
			this.ClearReachableMap(false);
		}

		// Token: 0x0600683F RID: 26687 RVA: 0x0020AA89 File Offset: 0x00208C89
		public int BuildReachableMap(int StartX, int StartY)
		{
			return this.BuildReachableMap(StartX, StartY, true);
		}

		// Token: 0x06006840 RID: 26688 RVA: 0x0020AA94 File Offset: 0x00208C94
		public int BuildReachableMap(int StartX, int StartY, bool bClearFirst)
		{
			if (bClearFirst)
			{
				this.ClearReachableMap();
			}
			if (this.ReachableMap == null)
			{
				this.ClearReachableMap();
			}
			return this.BuildReachableMapHelper(StartX, StartY, this.ReachableMap);
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x0020AABC File Offset: 0x00208CBC
		public bool BuildReachabilityFromEdges()
		{
			if (this.BuildReachableMap(this.Width / 2, this.Height - 1) >= 400)
			{
				return true;
			}
			if (this.BuildReachableMap(this.Width / 2, 0) >= 400)
			{
				return true;
			}
			if (this.BuildReachableMap(0, this.Height / 2) >= 400)
			{
				return true;
			}
			if (this.BuildReachableMap(this.Width - 1, this.Height / 2) >= 400)
			{
				return true;
			}
			for (int i = 0; i < this.Height; i++)
			{
				if (this.BuildReachableMap(0, i) >= 400)
				{
					return true;
				}
				if (this.BuildReachableMap(this.Width - 1, i) >= 400)
				{
					return true;
				}
			}
			for (int j = 0; j < this.Width; j++)
			{
				if (this.BuildReachableMap(j, this.Height - 1) >= 400)
				{
					return true;
				}
				if (this.BuildReachableMap(j, 0) >= 400)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006842 RID: 26690 RVA: 0x0020ABAC File Offset: 0x00208DAC
		public void BuildReachableMap()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (!cell.IsOccluding() && !cell.IsReachable())
					{
						this.BuildReachableMap(j, i, false);
					}
				}
			}
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x0020ABFE File Offset: 0x00208DFE
		public void RebuildReachableMap()
		{
			this.ClearReachableMap();
			this.BuildReachableMap();
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x0020AC0C File Offset: 0x00208E0C
		public bool IsActive()
		{
			XRLGame game = XRLCore.Core.Game;
			return ((game != null) ? game.ZoneManager.ActiveZone : null) == this;
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x0020AC2C File Offset: 0x00208E2C
		public bool IsReachable(int X, int Y)
		{
			return this.ReachableMap == null || this.ReachableMap[X, Y];
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x0020AC48 File Offset: 0x00208E48
		private int BuildReachableMapHelper(int StartX, int StartY, bool[,] Reachable)
		{
			Zone.VisitedList.Clear();
			Zone.CellQueue.Clear();
			int num = 0;
			if (StartX < 0)
			{
				return num;
			}
			if (StartY < 0)
			{
				return num;
			}
			if (StartX >= this.Width)
			{
				return num;
			}
			if (StartY >= this.Height)
			{
				return num;
			}
			if (this.GetCell(StartX, StartY).HasWall())
			{
				return num;
			}
			Zone.CellQueue.Enqueue(Location2D.Get(StartX, StartY));
			while (Zone.CellQueue.Count > 0)
			{
				Location2D location2D = Zone.CellQueue.Dequeue();
				if (!Zone.VisitedList.ContainsKey(location2D))
				{
					Zone.VisitedList.Add(location2D, true);
					if (!this.GetCell(location2D).HasWall())
					{
						Reachable[location2D.X, location2D.Y] = true;
						num++;
						if (location2D.X - 1 >= 0)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X - 1, location2D.Y));
						}
						if (location2D.X + 1 < this.Width)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X + 1, location2D.Y));
						}
						if (location2D.Y - 1 >= 0)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X, location2D.Y - 1));
						}
						if (location2D.Y + 1 < this.Height)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X, location2D.Y + 1));
						}
						if (location2D.X - 1 >= 0 && location2D.Y - 1 >= 0)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X - 1, location2D.Y - 1));
						}
						if (location2D.X + 1 < this.Width && location2D.Y - 1 >= 0)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X + 1, location2D.Y - 1));
						}
						if (location2D.X - 1 >= 0 && location2D.Y + 1 < this.Height)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X - 1, location2D.Y + 1));
						}
						if (location2D.X + 1 < this.Width && location2D.Y + 1 < this.Height)
						{
							Zone.CellQueue.Enqueue(Location2D.Get(location2D.X + 1, location2D.Y + 1));
						}
					}
				}
			}
			Zone.VisitedList.Clear();
			return num;
		}

		// Token: 0x06006847 RID: 26695 RVA: 0x0020AEB0 File Offset: 0x002090B0
		private void BuildReachableMapHelperFlood(int StartX, int StartY, bool[,] Reachable)
		{
			if (StartX < 0)
			{
				return;
			}
			if (StartY < 0)
			{
				return;
			}
			if (StartX >= this.Width)
			{
				return;
			}
			if (StartY >= this.Height)
			{
				return;
			}
			if (Reachable[StartX, StartY])
			{
				return;
			}
			if (!this.GetCell(StartX, StartY).IsEmpty())
			{
				return;
			}
			if (this.GetCell(StartX, StartY).Objects.Count > 0 && !this.GetCell(StartX, StartY).IsEmpty())
			{
				return;
			}
			Reachable[StartX, StartY] = true;
			this.BuildReachableMapHelper(StartX - 1, StartY - 1, Reachable);
			this.BuildReachableMapHelper(StartX, StartY - 1, Reachable);
			this.BuildReachableMapHelper(StartX + 1, StartY - 1, Reachable);
			this.BuildReachableMapHelper(StartX - 1, StartY, Reachable);
			this.BuildReachableMapHelper(StartX + 1, StartY, Reachable);
			this.BuildReachableMapHelper(StartX - 1, StartY + 1, Reachable);
			this.BuildReachableMapHelper(StartX, StartY + 1, Reachable);
			this.BuildReachableMapHelper(StartX + 1, StartY + 1, Reachable);
		}

		// Token: 0x06006848 RID: 26696 RVA: 0x0020AF8C File Offset: 0x0020918C
		public void DebugDrawSemantics()
		{
			if (Options.GetOption("OptionDrawZoneSemantics", "") == "Yes")
			{
				ScreenBuffer newBuffer = ScreenBuffer.GetScrapBuffer1(false);
				List<string> colorCodes = new List<string> { "G", "B", "Y", "W", "M", "K", "C", "R" };
				Dictionary<string, string> tagColors = new Dictionary<string, string>();
				Action<string> <>9__2;
				this.GetCells().ForEach(delegate(Cell c)
				{
					List<string> semanticTags = c.SemanticTags;
					if (semanticTags == null)
					{
						return;
					}
					Action<string> action;
					if ((action = <>9__2) == null)
					{
						action = (<>9__2 = delegate(string t)
						{
							if (t != null && !tagColors.ContainsKey(t))
							{
								tagColors.Add(t, "&" + colorCodes[tagColors.Count % colorCodes.Count]);
							}
						});
					}
					semanticTags.ForEach(action);
				});
				this.GetCells().ForEach(delegate(Cell c)
				{
					newBuffer.Goto(c.X, c.Y);
					List<string> semanticTags2 = this.GetCell(c.X, c.Y).SemanticTags;
					if (semanticTags2 != null && semanticTags2.Count > 0)
					{
						newBuffer.Write(tagColors[this.GetCell(c.X, c.Y).SemanticTags[0]] + "#", true, false, false, null, -1);
					}
				});
				newBuffer.Draw();
				Keyboard.getch();
			}
		}

		// Token: 0x06006849 RID: 26697 RVA: 0x0020B07E File Offset: 0x0020927E
		public void Render(ScreenBuffer Buf)
		{
			if (GameManager.bDraw == 12)
			{
				return;
			}
			Buf.Clear();
			Event.PinCurrentPool();
			this.Render(Buf, 0, 0, 79, 24);
			Event.ResetToPin();
		}

		// Token: 0x0600684A RID: 26698 RVA: 0x0020B0A8 File Offset: 0x002092A8
		public void Render(ScreenBuffer Buf, int X1, int Y1, int X2, int Y2)
		{
			if (The.Core.HostileWalkObjects == null)
			{
				The.Core.HostileWalkObjects = new List<GameObject>();
			}
			this.RenderedObjects = 0;
			bool bAlt = Keyboard.bAlt;
			bool disableFullscreenColorEffects = Options.DisableFullscreenColorEffects;
			Zone.WantsToPaint.Clear();
			if (Zone.SoundMapDirty)
			{
				this.UpdateSoundMap();
			}
			RenderEvent renderEvent = Zone.eRender;
			for (int i = Y1; i <= Y2; i++)
			{
				for (int j = X1; j <= X2; j++)
				{
					int num = j + i * this.Width;
					ConsoleChar consoleChar = Buf.Buffer[j, i];
					this.Map[j][i].Render(renderEvent, this.VisibilityMap[num], this.LightMap[num], this.ExploredMap[num], bAlt, disableFullscreenColorEffects, Zone.WantsToPaint);
					consoleChar.Set(renderEvent);
				}
			}
			if (Zone.SoundRenderers.Count != 0)
			{
				ISoundRenderer[] array = Zone.SoundRenderers.GetArray();
				RenderSoundEvent renderSoundEvent = Zone.eRenderSound;
				renderSoundEvent.Zone = this;
				renderSoundEvent.Buffer = Buf.Buffer;
				for (int k = Zone.SoundRenderers.Count - 1; k >= 0; k--)
				{
					renderSoundEvent.Unregister = false;
					array[k].Render(renderSoundEvent);
					renderSoundEvent.Char = null;
					if (renderSoundEvent.Unregister)
					{
						Zone.SoundRenderers.RemoveAt(k);
					}
				}
			}
			foreach (GameObject gameObject in Zone.WantsToPaint)
			{
				gameObject.Paint(Buf);
			}
			The.Core.RenderedObjects = this.RenderedObjects;
		}

		// Token: 0x0600684B RID: 26699 RVA: 0x0020B248 File Offset: 0x00209448
		public void RegisterSoundRenderer(ISoundRenderer Renderer)
		{
			if (Zone.SoundRenderers == null)
			{
				Zone.SoundRenderers = new Rack<ISoundRenderer> { Renderer };
				return;
			}
			if (!Zone.SoundRenderers.Contains(Renderer))
			{
				Zone.SoundRenderers.Add(Renderer);
			}
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x0020B27B File Offset: 0x0020947B
		public void UnregisterSoundRenderer(ISoundRenderer Renderer)
		{
			Rack<ISoundRenderer> soundRenderers = Zone.SoundRenderers;
			if (soundRenderers == null)
			{
				return;
			}
			soundRenderers.Remove(Renderer);
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x0020B290 File Offset: 0x00209490
		public void ForeachCell(Action<Cell> aProc)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					aProc(this.GetCell(j, i));
				}
			}
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x0020B2D0 File Offset: 0x002094D0
		public bool ForeachCell(Predicate<Cell> pProc)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (!pProc(this.GetCell(j, i)))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x0020B314 File Offset: 0x00209514
		public List<Cell> GetEmptyReachableCellsWithout(string[] Blueprint)
		{
			List<Cell> list = new List<Cell>();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.IsReachable() && !cell.HasObjectWithBlueprint(Blueprint))
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x0020B374 File Offset: 0x00209574
		public List<Cell> GetReachableCells()
		{
			List<Cell> list = new List<Cell>();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsReachable())
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x0020B3C4 File Offset: 0x002095C4
		public List<Cell> GetReachableCellsWithout(string[] Blueprint)
		{
			List<Cell> list = new List<Cell>();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsReachable() && !cell.HasObjectWithBlueprint(Blueprint))
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x0020B41C File Offset: 0x0020961C
		public List<Cell> GetEmptyReachableCells(Rect2D r)
		{
			List<Cell> list = new List<Cell>();
			for (int i = r.y1; i <= r.y2; i++)
			{
				for (int j = r.x1; j <= r.x2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.IsReachable())
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x0020B47C File Offset: 0x0020967C
		public Cell GetSpawnCell()
		{
			Cell cell = this.GetEmptyReachableCells().GetRandomElement<Cell>(null);
			if (cell == null)
			{
				cell = this.GetEmptyCells(null, null).GetRandomElement<Cell>(null);
			}
			if (cell == null)
			{
				cell = (from c in this.GetReachableCells()
					where !c.HasWall()
					select c).GetRandomElement<Cell>(null);
			}
			if (cell == null)
			{
				cell = this.GetCells().GetRandomElement<Cell>(null);
			}
			return cell;
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x0020B4F0 File Offset: 0x002096F0
		public List<Cell> GetEmptyReachableCells()
		{
			List<Cell> list = new List<Cell>(32);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.IsReachable())
					{
						list.Add(cell);
					}
				}
			}
			if (list.Count == 0)
			{
				return this.GetEmptyCells(null, null);
			}
			return list;
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x0020B55C File Offset: 0x0020975C
		public List<Cell> GetEmptyReachableCells(Predicate<Cell> Filter)
		{
			List<Cell> list = new List<Cell>(32);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.IsReachable() && Filter(cell))
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x0020B5C0 File Offset: 0x002097C0
		public List<Cell> GetEmptyReachableCells(Event E)
		{
			List<Cell> list = new List<Cell>(32);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.IsReachable() && cell.FireEvent(E))
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x0020B624 File Offset: 0x00209824
		public bool AnyEmptyReachableCells()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.IsReachable())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x0020B670 File Offset: 0x00209870
		public bool AnyEmptyReachableCells(Event E)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.IsReachable() && cell.FireEvent(E))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x0020B6C4 File Offset: 0x002098C4
		public List<Cell> GetEmptyCellsWithNoFurniture()
		{
			List<Cell> list = new List<Cell>(32);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && !cell.HasFurniture())
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x0020B71C File Offset: 0x0020991C
		public List<Cell> GetPassableCells(GameObject Object = null)
		{
			List<Cell> list = new List<Cell>(32);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsPassable(Object, true))
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x0020B770 File Offset: 0x00209970
		public List<Cell> GetEmptyCells(Predicate<Cell> filter = null, List<Cell> listToUse = null)
		{
			List<Cell> list = ((listToUse != null) ? listToUse : new List<Cell>(32));
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && (filter == null || filter(cell)))
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x0020B7D4 File Offset: 0x002099D4
		public List<Cell> GetEmptyCells(Rect2D R)
		{
			List<Cell> list = new List<Cell>();
			for (int i = R.y1; i <= R.y2; i++)
			{
				for (int j = R.x1; j <= R.x2; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty())
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x0020B82C File Offset: 0x00209A2C
		public int GetEmptyCellCount()
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.GetCell(j, i).IsEmpty())
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x0020B874 File Offset: 0x00209A74
		public List<Cell> GetEmptyCells(Event E)
		{
			List<Cell> list = new List<Cell>(32);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsEmpty() && cell.FireEvent(E))
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x0020B8D0 File Offset: 0x00209AD0
		public List<Cell> GetExploredCells()
		{
			List<Cell> list = new List<Cell>(32);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					if (cell.IsExplored())
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x0020B920 File Offset: 0x00209B20
		public Cell[] GetExploredCellsShuffled()
		{
			return Algorithms.RandomShuffle<Cell>(this.GetExploredCells(), Stat.Rand);
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x0020B932 File Offset: 0x00209B32
		[Obsolete("Legacy amnesia behaviour, use GetExploredCells")]
		public List<Cell> GetReallyExploredCells()
		{
			return this.GetExploredCells();
		}

		// Token: 0x06006862 RID: 26722 RVA: 0x0020B93A File Offset: 0x00209B3A
		[Obsolete("Legacy amnesia behaviour, use GetExploredCellsShuffled")]
		public Cell[] GetReallyExploredCellsShuffled()
		{
			return this.GetExploredCellsShuffled();
		}

		// Token: 0x06006863 RID: 26723 RVA: 0x0020B942 File Offset: 0x00209B42
		[Obsolete("Legacy amnesia behaviour, use AnyUnexplored")]
		public bool AnyReallyUnexplored()
		{
			return this.AnyUnexplored();
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x0020B94C File Offset: 0x00209B4C
		public bool AnyUnexplored()
		{
			Cell[][] map = this.Map;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (!map[j][i].IsExplored())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x0020B991 File Offset: 0x00209B91
		public Cell[] GetEmptyCellsShuffled()
		{
			return Algorithms.RandomShuffle<Cell>(this.GetEmptyCells(null, null), Stat.Rand);
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x0020B9A8 File Offset: 0x00209BA8
		public Cell GetCellWithEmptyBorder(int Size)
		{
			List<Cell> list = new List<Cell>();
			foreach (Cell cell in this.GetEmptyCellsShuffled())
			{
				if (cell.X > Size && cell.X < this.Width - Size && cell.Y > Size && cell.Y < this.Height - Size)
				{
					list.Clear();
					cell.GetAdjacentCells(Size, list, true, true);
					using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!enumerator.Current.IsEmpty())
							{
								goto IL_008C;
							}
						}
					}
					return cell;
				}
				IL_008C:;
			}
			return null;
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x0020BA60 File Offset: 0x00209C60
		public List<Cell> GetEmptyVisibleCells()
		{
			List<Cell> list = new List<Cell>();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.GetCell(j, i).IsEmpty() && this.GetCell(j, i).IsVisible())
					{
						list.Add(this.GetCell(j, i));
					}
				}
			}
			return list;
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x0020BAC2 File Offset: 0x00209CC2
		public IEnumerable<Cell> GetCells(Box b)
		{
			int num;
			for (int y = b.y1; y <= b.y2; y = num + 1)
			{
				for (int x = b.x1; x <= b.x2; x = num + 1)
				{
					yield return this.GetCell(x, y);
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x0020BADC File Offset: 0x00209CDC
		public List<Cell> GetCells()
		{
			if (this.CellList == null)
			{
				this.CellList = new List<Cell>(this.Width * this.Height);
				for (int i = 0; i < this.Height; i++)
				{
					for (int j = 0; j < this.Width; j++)
					{
						this.CellList.Add(this.Map[j][i]);
					}
				}
			}
			return this.CellList;
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x0020BB48 File Offset: 0x00209D48
		public List<Cell> GetCellsViaEventList(Predicate<Cell> pFilter)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (pFilter(this.Map[j][i]))
					{
						num++;
					}
				}
			}
			List<Cell> list = Event.NewCellList();
			if (num > 0)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						if (pFilter(this.Map[l][k]))
						{
							list.Add(this.Map[l][k]);
							if (list.Count >= num)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x0020BBF8 File Offset: 0x00209DF8
		public List<Cell> GetCells(Predicate<Cell> pFilter)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (pFilter(this.Map[j][i]))
					{
						num++;
					}
				}
			}
			List<Cell> list = new List<Cell>(num);
			if (num > 0)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						if (pFilter(this.Map[l][k]))
						{
							list.Add(this.Map[l][k]);
							if (list.Count >= num)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x0020BCA8 File Offset: 0x00209EA8
		public List<Cell> GetCellsWithObject(string Blueprint)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasObject(Blueprint))
					{
						num++;
					}
				}
			}
			List<Cell> list = new List<Cell>(num);
			if (num > 0)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						if (this.Map[l][k].HasObject(Blueprint))
						{
							list.Add(this.Map[l][k]);
							if (list.Count >= num)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600686D RID: 26733 RVA: 0x0020BD56 File Offset: 0x00209F56
		public IEnumerable<Cell> LoopCells()
		{
			int num;
			for (int y = 0; y < this.Height; y = num + 1)
			{
				for (int x = 0; x < this.Width; x = num + 1)
				{
					yield return this.Map[x][y];
					num = x;
				}
				num = y;
			}
			yield break;
		}

		// Token: 0x0600686E RID: 26734 RVA: 0x0020BD68 File Offset: 0x00209F68
		public List<Cell> GetCellsWithObject(Predicate<GameObject> pFilter)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasObject(pFilter))
					{
						num++;
					}
				}
			}
			List<Cell> list = new List<Cell>(num);
			if (num > 0)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						if (this.Map[l][k].HasObject(pFilter))
						{
							list.Add(this.Map[l][k]);
							if (list.Count >= num)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600686F RID: 26735 RVA: 0x0020BE18 File Offset: 0x0020A018
		public List<Cell> GetCellsWithTaggedObject(string What)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasObjectWithTag(What))
					{
						num++;
					}
				}
			}
			List<Cell> list = new List<Cell>(num);
			if (num > 0)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						if (this.Map[l][k].HasObjectWithTag(What))
						{
							list.Add(this.Map[l][k]);
							if (list.Count >= num)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x0020BEC8 File Offset: 0x0020A0C8
		public List<Cell> GetCellsWithTagOrPropertyObject(string What)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].HasObjectWithTagOrProperty(What))
					{
						num++;
					}
				}
			}
			List<Cell> list = new List<Cell>(num);
			if (num > 0)
			{
				for (int k = 0; k < this.Width; k++)
				{
					for (int l = 0; l < this.Height; l++)
					{
						if (this.Map[k][l].HasObjectWithTagOrProperty(What))
						{
							list.Add(this.Map[k][l]);
							if (list.Count >= num)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x0020BF78 File Offset: 0x0020A178
		public List<Cell> GetCellsWithFloor(string OkFloor)
		{
			int num = 0;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Map[j][i].PaintTile == OkFloor)
					{
						num++;
					}
				}
			}
			List<Cell> list = new List<Cell>(num);
			if (num > 0)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						if (this.Map[l][k].PaintTile == OkFloor)
						{
							list.Add(this.Map[l][k]);
							if (list.Count > num)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06006872 RID: 26738 RVA: 0x0020C030 File Offset: 0x0020A230
		public GameObject GetFirstObject()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject firstObject;
					if ((firstObject = this.Map[j][i].GetFirstObject()) != null)
					{
						return firstObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06006873 RID: 26739 RVA: 0x0020C078 File Offset: 0x0020A278
		public GameObject GetFirstObject(string Blueprint)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject firstObject;
					if ((firstObject = this.Map[j][i].GetFirstObject(Blueprint)) != null)
					{
						return firstObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x0020C0C0 File Offset: 0x0020A2C0
		public GameObject GetFirstObject(Predicate<GameObject> pFilter)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					GameObject firstObject;
					if ((firstObject = this.Map[j][i].GetFirstObject(pFilter)) != null)
					{
						return firstObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06006875 RID: 26741 RVA: 0x0020C108 File Offset: 0x0020A308
		public int[,] GetUnreachableGrid()
		{
			int[,] array = new int[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (!this.GetCell(i, j).IsReachable())
					{
						array[i, j] = 0;
					}
					else
					{
						array[i, j] = 1;
					}
				}
			}
			return array;
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x0020C170 File Offset: 0x0020A370
		public int[,] GetWallGrid()
		{
			int[,] array = new int[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (this.GetCell(i, j).HasWall())
					{
						array[i, j] = 0;
					}
					else
					{
						array[i, j] = 1;
					}
				}
			}
			return array;
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x0020C1D8 File Offset: 0x0020A3D8
		public int[,] GetUnreachableOrWallGrid()
		{
			int[,] array = new int[this.Width, this.Height];
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (this.GetCell(i, j).HasWall() || !this.GetCell(i, j).IsReachable())
					{
						array[i, j] = 0;
					}
					else
					{
						array[i, j] = 1;
					}
				}
			}
			return array;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06006878 RID: 26744 RVA: 0x0020C24C File Offset: 0x0020A44C
		public string DebugName
		{
			get
			{
				return this.ZoneID ?? "null-ID zone";
			}
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x0020C25D File Offset: 0x0020A45D
		public Cell GetCell(Point2D p)
		{
			return this.GetCell(p.x, p.y);
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x0020C271 File Offset: 0x0020A471
		public Cell GetCell(Location2D p)
		{
			return this.GetCell(p.X, p.Y);
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x0020C285 File Offset: 0x0020A485
		public Cell GetCell(Point p)
		{
			return this.GetCell(p.X, p.Y);
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x0020C299 File Offset: 0x0020A499
		public Cell GetCell(int x, int y)
		{
			if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
			{
				return null;
			}
			return this.Map[x][y];
		}

		// Token: 0x0600687D RID: 26749 RVA: 0x0020C2C4 File Offset: 0x0020A4C4
		public Cell GetCellChecked(int x, int y)
		{
			if (x < 0 || y < 0 || x >= this.Width || y >= this.Height)
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"cell at invalid coordinates ",
					x.ToString(),
					", ",
					y.ToString(),
					" requested from ",
					this.DebugName
				}));
				return null;
			}
			Cell cell = this.Map[x][y];
			if (cell == null)
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"no cell at coordinates ",
					x.ToString(),
					", ",
					y.ToString(),
					" in ",
					this.DebugName
				}));
			}
			return cell;
		}

		// Token: 0x0600687E RID: 26750 RVA: 0x0020C388 File Offset: 0x0020A588
		public Cell GetCellGlobal(int XD, int YD, bool LocalOnly = false, bool BuiltOnly = true)
		{
			if (XD >= 0 && YD >= 0 && XD < this.Width && YD < this.Height)
			{
				return this.Map[XD][YD];
			}
			if (LocalOnly)
			{
				return null;
			}
			int num = this.wX;
			int num2 = this.X;
			int num3 = this.wY;
			int num4 = this.Y;
			do
			{
				if (XD < 0)
				{
					if (num2 == 0)
					{
						num--;
						num2 += Definitions.Width;
					}
					num2--;
					XD += this.Width;
				}
				else if (XD >= this.Width)
				{
					if (num2 == Definitions.Width - 1)
					{
						num++;
						num2 -= Definitions.Width;
					}
					num2++;
					XD -= this.Width;
				}
				if (YD < 0)
				{
					if (num4 == 0)
					{
						num3--;
						num4 += Definitions.Height;
					}
					num4--;
					YD += this.Height;
				}
				else if (YD >= this.Height)
				{
					if (num4 == Definitions.Height - 1)
					{
						num3++;
						num4 -= Definitions.Height;
					}
					num4++;
					YD -= this.Height;
				}
			}
			while (XD < 0 || YD < 0 || XD >= this.Width || YD >= this.Height);
			if (num < 0)
			{
				return null;
			}
			if (num3 < 0)
			{
				return null;
			}
			if (num > 79)
			{
				return null;
			}
			if (num3 > 24)
			{
				return null;
			}
			string text = XRL.World.ZoneID.Assemble(this.ZoneWorld, num, num3, num2, num4, this.Z);
			if (BuiltOnly && !The.ZoneManager.IsZoneLive(text))
			{
				return null;
			}
			Zone zone = The.ZoneManager.GetZone(text);
			if (zone == null)
			{
				return null;
			}
			return zone.GetCell(XD, YD);
		}

		// Token: 0x0600687F RID: 26751 RVA: 0x0020C4FF File Offset: 0x0020A6FF
		public Cell GetRandomCell(int withinBorder = 0)
		{
			return this.GetCell(Stat.Random(withinBorder, this.Width - 1 - withinBorder), Stat.Random(withinBorder, this.Height - 1 - withinBorder));
		}

		// Token: 0x06006880 RID: 26752 RVA: 0x0020C527 File Offset: 0x0020A727
		public Cell GetCenterCell()
		{
			return this.GetCell(this.Width / 2, this.Height / 2);
		}

		// Token: 0x06006881 RID: 26753 RVA: 0x0020C540 File Offset: 0x0020A740
		public void UnexploreAll()
		{
			for (int i = 0; i <= this.ExploredMap.GetUpperBound(0); i++)
			{
				this.ExploredMap[i] = false;
			}
		}

		// Token: 0x06006882 RID: 26754 RVA: 0x0020C570 File Offset: 0x0020A770
		public void ExploreAll()
		{
			int i = 0;
			int upperBound = this.ExploredMap.GetUpperBound(0);
			while (i <= upperBound)
			{
				this.ExploredMap[i] = true;
				i++;
			}
		}

		// Token: 0x06006883 RID: 26755 RVA: 0x0020C5A0 File Offset: 0x0020A7A0
		public bool FireEvent(string ID)
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (!this.Map[i][j].FireEvent(ID))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006884 RID: 26756 RVA: 0x0020C5E4 File Offset: 0x0020A7E4
		public bool FireEvent(Event E)
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (!this.Map[i][j].FireEvent(E))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006885 RID: 26757 RVA: 0x0020C628 File Offset: 0x0020A828
		public bool BroadcastEvent(Event E)
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (!this.Map[i][j].BroadcastEvent(E))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006886 RID: 26758 RVA: 0x0020C66C File Offset: 0x0020A86C
		public bool BroadcastEvent(string Name)
		{
			return this.BroadcastEvent(Event.New(Name, 0, 0, 0));
		}

		// Token: 0x06006887 RID: 26759 RVA: 0x0020C680 File Offset: 0x0020A880
		public bool FireEventDirect(Event E)
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (!this.Map[i][j].FireEventDirect(E))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06006888 RID: 26760 RVA: 0x0020C6C4 File Offset: 0x0020A8C4
		public void CalculateMissileMap(GameObject Shooter)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell cell = this.GetCell(j, i);
					bool flag = false;
					bool flag2 = false;
					int k = 0;
					int count = cell.Objects.Count;
					while (k < count)
					{
						if (cell.Objects[k].IsCombatObject())
						{
							if (!cell.Objects[k].IsHostileTowards(Shooter) && !Shooter.IsHostileTowards(cell.Objects[k]))
							{
								flag2 = true;
								break;
							}
							flag = true;
						}
						k++;
					}
					if (flag2)
					{
						this.MissileMap[j][i] = MissileMapType.Friendly;
					}
					else if (flag)
					{
						this.MissileMap[j][i] = MissileMapType.Hostile;
					}
					else if (cell.IsSolidFor(Shooter, false))
					{
						this.MissileMap[j][i] = MissileMapType.Wall;
					}
					else
					{
						int num = 0;
						int l = 0;
						int count2 = cell.Objects.Count;
						while (l < count2)
						{
							num += GetMissileCoverPercentageEvent.GetFor(cell.Objects[l], Shooter, null, false, false);
							l++;
						}
						if (num > 0)
						{
							if (num >= 80)
							{
								this.MissileMap[j][i] = MissileMapType.VeryHeavyCover;
							}
							else if (num >= 50)
							{
								this.MissileMap[j][i] = MissileMapType.HeavyCover;
							}
							else if (num >= 30)
							{
								this.MissileMap[j][i] = MissileMapType.MediumCover;
							}
							else if (num >= 20)
							{
								this.MissileMap[j][i] = MissileMapType.LightCover;
							}
							else
							{
								this.MissileMap[j][i] = MissileMapType.VeryLightCover;
							}
						}
						else
						{
							this.MissileMap[j][i] = MissileMapType.Empty;
						}
					}
				}
			}
		}

		// Token: 0x06006889 RID: 26761 RVA: 0x0020C858 File Offset: 0x0020AA58
		public static int CalculateNav(GameObject Looker, bool Autoexploring = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false, bool AllowDigging = true)
		{
			int num = 0;
			if (GameObject.Validate(ref Looker))
			{
				if (!Looker.HasStat("Intelligence") || Looker.Stat("Intelligence", 0) > 6)
				{
					num |= 1;
				}
				if (AllowDigging && Looker.PathAsBurrower)
				{
					num |= 2;
				}
				if (Autoexploring)
				{
					num |= 4;
				}
				if (Looker.IsFlying || Looker.HasTagOrProperty("PathAsIfFlying"))
				{
					num |= 8;
				}
				else
				{
					Brain brain = Looker.Brain;
					if (brain != null && brain.WallWalker)
					{
						num |= 16;
					}
				}
				if (Looker.HasPropertyOrTag("IgnoresWalls"))
				{
					num |= 32;
				}
				int num2 = 50;
				GetSwimmingPerformanceEvent.GetFor(Looker, ref num2);
				if (num2 < 50)
				{
					num |= 64;
				}
				if (Looker.Slimewalking)
				{
					num |= 128;
				}
				if (Looker.LimitToAquatic())
				{
					num |= 256;
				}
				if (Looker.Polypwalking)
				{
					num |= 512;
				}
				if (Looker.Strutwalking)
				{
					num |= 1024;
				}
				if (Juggernaut)
				{
					num |= 2048;
				}
				if (IgnoreCreatures)
				{
					num |= 8192;
				}
				if (!Looker.Respires)
				{
					num |= 32768;
				}
				if (IgnoreGases)
				{
					num |= 16384;
				}
				if (Looker.HasPropertyOrTag("FilthAffinity"))
				{
					num |= 65536;
				}
				if (Looker.HasEffect<Phased>())
				{
					num |= 131072;
				}
				if (Looker.HasEffect<Omniphase>())
				{
					num |= 262144;
				}
				if (Looker.HasTag("Nullphase") || Looker.GetIntProperty("Nullphase", 0) >= 1)
				{
					num |= 524288;
				}
				if (FlexPhase)
				{
					num |= 1048576;
				}
			}
			return num;
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x0020C9E0 File Offset: 0x0020ABE0
		public static int CalculateNav(bool Smart, bool Burrower, bool Autoexploring, bool Flying, bool WallWalker, bool IgnoresWalls, bool Swimming, bool Slimewalking, bool Aquatic, bool Polypwalking, bool Strutwalking, bool Juggernaut, bool Reefer, bool IgnoreCreatures, bool IgnoreGases, bool Unbreathing, bool FilthAffinity, bool OutOfPhase, bool Omniphase, bool Nullphase, bool FlexPhase)
		{
			int num = 0;
			if (Smart)
			{
				num |= 1;
			}
			if (Burrower)
			{
				num |= 2;
			}
			if (Autoexploring)
			{
				num |= 4;
			}
			if (Flying)
			{
				num |= 8;
			}
			if (WallWalker)
			{
				num |= 16;
			}
			if (IgnoresWalls)
			{
				num |= 32;
			}
			if (Swimming)
			{
				num |= 64;
			}
			if (Slimewalking)
			{
				num |= 128;
			}
			if (Aquatic)
			{
				num |= 256;
			}
			if (Polypwalking)
			{
				num |= 512;
			}
			if (Strutwalking)
			{
				num |= 1024;
			}
			if (Juggernaut)
			{
				num |= 2048;
			}
			if (Reefer)
			{
				num |= 4096;
			}
			if (IgnoreCreatures)
			{
				num |= 8192;
			}
			if (IgnoreGases)
			{
				num |= 16384;
			}
			if (Unbreathing)
			{
				num |= 32768;
			}
			if (FilthAffinity)
			{
				num |= 65536;
			}
			if (OutOfPhase)
			{
				num |= 131072;
			}
			if (Omniphase)
			{
				num |= 262144;
			}
			if (Nullphase)
			{
				num |= 524288;
			}
			if (FlexPhase)
			{
				num |= 1048576;
			}
			return num;
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x0020CAD0 File Offset: 0x0020ACD0
		public static void UnpackNav(int Nav, out bool Smart, out bool Burrower, out bool Autoexploring, out bool Flying, out bool WallWalker, out bool IgnoresWalls, out bool Swimming, out bool Slimewalking, out bool Aquatic, out bool Polypwalking, out bool Strutwalking, out bool Juggernaut, out bool Reefer, out bool IgnoreCreatures, out bool IgnoreGases, out bool Unbreathing, out bool FilthAffinity, out bool OutOfPhase, out bool Omniphase, out bool Nullphase, out bool FlexPhase)
		{
			Smart = (Nav & 1) != 0;
			Burrower = (Nav & 2) != 0;
			Autoexploring = (Nav & 4) != 0;
			Flying = (Nav & 8) != 0;
			WallWalker = (Nav & 16) != 0;
			IgnoresWalls = (Nav & 32) != 0;
			Swimming = (Nav & 64) != 0;
			Slimewalking = (Nav & 128) != 0;
			Aquatic = (Nav & 256) != 0;
			Polypwalking = (Nav & 512) != 0;
			Strutwalking = (Nav & 1024) != 0;
			Juggernaut = (Nav & 2048) != 0;
			Reefer = (Nav & 4096) != 0;
			IgnoreCreatures = (Nav & 8192) != 0;
			IgnoreGases = (Nav & 16384) != 0;
			Unbreathing = (Nav & 32768) != 0;
			FilthAffinity = (Nav & 65536) != 0;
			OutOfPhase = (Nav & 131072) != 0;
			Omniphase = (Nav & 262144) != 0;
			Nullphase = (Nav & 524288) != 0;
			FlexPhase = (Nav & 1048576) != 0;
		}

		// Token: 0x0600688C RID: 26764 RVA: 0x0020CBD4 File Offset: 0x0020ADD4
		public void CalculateNavigationMap(GameObject Looker, bool AddNoise = false, bool ExploredOnly = false, bool Autoexploring = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false)
		{
			NoiseMap noiseMap = null;
			if (AddNoise)
			{
				noiseMap = new NoiseMap(80, 25, 10, 3, 3, 4, 80, 80, 6, 3, -3, 1, new List<NoiseMapNode>());
			}
			int num = Zone.CalculateNav(Looker, Autoexploring, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, true);
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (ExploredOnly && !this.GetExplored(j, i))
					{
						this.NavigationMap[j, i].Weight = 100;
					}
					else
					{
						this.NavigationMap[j, i].Weight = this.Map[j][i].NavigationWeight(Looker, num);
						if (AddNoise && noiseMap.Noise[j, i] > 1)
						{
							this.NavigationMap[j, i].Weight = 98;
						}
					}
				}
			}
			if (Options.DrawNavigationWeightMaps)
			{
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				for (int k = 0; k < scrapBuffer.Width; k++)
				{
					for (int l = 0; l < scrapBuffer.Height; l++)
					{
						string text = Math.Min(this.NavigationMap[k, l].Weight / 6, 15).ToString("X");
						scrapBuffer.Goto(k, l);
						string text2 = "&g";
						if (this.NavigationMap[k, l].Weight == 0)
						{
							text2 = "&G";
						}
						if (this.NavigationMap[k, l].Weight >= 50)
						{
							text2 = "&W";
						}
						if (this.NavigationMap[k, l].Weight >= 98)
						{
							text2 = "&r";
							text = "E";
						}
						if (this.NavigationMap[k, l].Weight >= 100)
						{
							text2 = "&K";
							text = "-";
						}
						scrapBuffer.Write(text2 + text[0].ToString(), true, false, false, null, -1);
					}
				}
				XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
				Keyboard.getch();
			}
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0020CDF5 File Offset: 0x0020AFF5
		public bool IsWorldMap()
		{
			if (this.ZoneID == null)
			{
				return false;
			}
			if (this._isWorldMapCache == null)
			{
				this._isWorldMapCache = new bool?(this.ZoneID.IndexOf('.') == -1);
			}
			return this._isWorldMapCache.GetValueOrDefault();
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x0020CE34 File Offset: 0x0020B034
		public string GetZoneIDFromDirection(string Direction, int n = 1)
		{
			int zoneX = this.GetZoneX();
			int zoneY = this.GetZoneY();
			int zoneZ = this.GetZoneZ();
			int zonewX = this.GetZonewX();
			int zonewY = this.GetZonewY();
			if (Direction == ".")
			{
				return this.ZoneID;
			}
			Directions.ApplyDirectionGlobal(Direction, ref zoneX, ref zoneY, ref zoneZ, ref zonewX, ref zonewY, n);
			if (zonewX < 0)
			{
				return null;
			}
			if (zonewX > 79)
			{
				return null;
			}
			if (zonewY < 0)
			{
				return null;
			}
			if (zonewY > 24)
			{
				return null;
			}
			return XRL.World.ZoneID.Assemble(this.GetZoneWorld(), zonewX, zonewY, zoneX, zoneY, zoneZ);
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0020CEB8 File Offset: 0x0020B0B8
		public Zone GetZoneAtLevel(int Level)
		{
			int zoneX = this.GetZoneX();
			int zoneY = this.GetZoneY();
			int zonewX = this.GetZonewX();
			int zonewY = this.GetZonewY();
			return The.ZoneManager.GetZone(this.GetZoneWorld(), zonewX, zonewY, zoneX, zoneY, Level);
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x0020CEF8 File Offset: 0x0020B0F8
		public Zone GetZoneFromDirection(string Direction)
		{
			int zoneX = this.GetZoneX();
			int zoneY = this.GetZoneY();
			int zoneZ = this.GetZoneZ();
			int zonewX = this.GetZonewX();
			int zonewY = this.GetZonewY();
			if (Direction == ".")
			{
				return this;
			}
			Directions.ApplyDirectionGlobal(Direction, ref zoneX, ref zoneY, ref zoneZ, ref zonewX, ref zonewY, 1);
			return The.ZoneManager.GetZone(this.GetZoneWorld(), zonewX, zonewY, zoneX, zoneY, zoneZ);
		}

		// Token: 0x06006891 RID: 26769 RVA: 0x0020CF60 File Offset: 0x0020B160
		public Zone GetZoneFromDirection(string Direction, int n)
		{
			int zoneX = this.GetZoneX();
			int zoneY = this.GetZoneY();
			int zoneZ = this.GetZoneZ();
			int zonewX = this.GetZonewX();
			int zonewY = this.GetZonewY();
			if (Direction == ".")
			{
				return this;
			}
			Directions.ApplyDirectionGlobal(Direction, ref zoneX, ref zoneY, ref zoneZ, ref zonewX, ref zonewY, n);
			return The.ZoneManager.GetZone(this.GetZoneWorld(), zonewX, zonewY, zoneX, zoneY, zoneZ);
		}

		// Token: 0x06006892 RID: 26770 RVA: 0x0020CFC7 File Offset: 0x0020B1C7
		public virtual string ResolveZoneWorld()
		{
			return this.ZoneWorld;
		}

		// Token: 0x06006893 RID: 26771 RVA: 0x0020CFCF File Offset: 0x0020B1CF
		public string GetZoneWorld()
		{
			return this.ZoneWorld;
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x0020CFD7 File Offset: 0x0020B1D7
		public int GetZonewX()
		{
			return this.wX;
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x0020CFDF File Offset: 0x0020B1DF
		public int GetZonewY()
		{
			return this.wY;
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x0020CFE7 File Offset: 0x0020B1E7
		public int GetZoneX()
		{
			return this.X;
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x0020CFEF File Offset: 0x0020B1EF
		public int GetZoneY()
		{
			return this.Y;
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x0020CFF7 File Offset: 0x0020B1F7
		public int GetZoneZ()
		{
			return this.Z;
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x0020CFFF File Offset: 0x0020B1FF
		public bool GetVisibility(int x, int y)
		{
			return this.VisibilityMap[x + y * this.Width];
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x0020D012 File Offset: 0x0020B212
		public LightLevel GetLight(int x, int y)
		{
			return this.LightMap[x + y * this.Width];
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x0020D025 File Offset: 0x0020B225
		public bool GetExplored(int x, int y)
		{
			return this.ExploredMap[x + y * this.Width];
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x0020D038 File Offset: 0x0020B238
		public void SetExplored(int x, int y, bool state)
		{
			this.ExploredMap[x + y * this.Width] = state;
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x0020D04C File Offset: 0x0020B24C
		[Obsolete("Legacy amnesia behaviour, use GetExplored")]
		public bool GetReallyExplored(int x, int y)
		{
			return this.ExploredMap[x + y * this.Width];
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x0020D05F File Offset: 0x0020B25F
		[Obsolete("Legacy amnesia behaviour, use SetExplored")]
		public void SetFakeUnexplored(int x, int y, bool state)
		{
		}

		// Token: 0x0600689F RID: 26783 RVA: 0x0020D061 File Offset: 0x0020B261
		[Obsolete("This is dangerous to use because the queued events will be overwritten. We'll probably remove or refactor this. -BB 7-16-22")]
		public void QueueEvent(Event E)
		{
			if (this.QueuedEvents == null)
			{
				this.QueuedEvents = new List<IEvent>();
			}
			this.QueuedEvents.Add(E);
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x0020D082 File Offset: 0x0020B282
		private void _QueueEvent(Event E)
		{
			if (this.QueuedEvents == null)
			{
				this.QueuedEvents = new List<IEvent>();
			}
			this.QueuedEvents.Add(E);
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x0020D0A3 File Offset: 0x0020B2A3
		private void QueueEvent(MinEvent E)
		{
			if (this.QueuedEvents == null)
			{
				this.QueuedEvents = new List<IEvent>();
			}
			this.QueuedEvents.Add(E);
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x0020D0C4 File Offset: 0x0020B2C4
		public void QueueEvent(string ID)
		{
			this._QueueEvent(new Event(ID));
		}

		// Token: 0x060068A3 RID: 26787 RVA: 0x0020D0D4 File Offset: 0x0020B2D4
		private void QueueEventOnce(Event E)
		{
			if (this.QueuedEvents != null)
			{
				foreach (IEvent @event in this.QueuedEvents)
				{
					Event event2 = @event as Event;
					if (event2 != null && event2.ID == E.ID)
					{
						return;
					}
				}
			}
			this._QueueEvent(E);
		}

		// Token: 0x060068A4 RID: 26788 RVA: 0x0020D14C File Offset: 0x0020B34C
		public void QueueEventOnce(string ID)
		{
			if (this.QueuedEvents != null)
			{
				foreach (IEvent @event in this.QueuedEvents)
				{
					Event event2 = @event as Event;
					if (event2 != null && event2.ID == ID)
					{
						return;
					}
				}
			}
			this.QueueEvent(ID);
		}

		// Token: 0x060068A5 RID: 26789 RVA: 0x0020D1C0 File Offset: 0x0020B3C0
		private void QueueEventOnce(MinEvent E)
		{
			if (this.QueuedEvents != null)
			{
				foreach (IEvent @event in this.QueuedEvents)
				{
					MinEvent minEvent = @event as MinEvent;
					if (minEvent != null && minEvent.ID == E.ID)
					{
						return;
					}
				}
			}
			this.QueueEvent(E);
		}

		// Token: 0x060068A6 RID: 26790 RVA: 0x0020D234 File Offset: 0x0020B434
		public void CheckEventQueue()
		{
			if (this.QueuedEvents != null && this.QueuedEvents.Count > 0)
			{
				int num = 0;
				while (this.QueuedEvents.Count > 0)
				{
					if (++num >= 100)
					{
						MetricsManager.LogError("cyclic event queue on " + this.QueuedEvents[0].GetType().Name + ", clearing");
						this.QueuedEvents.Clear();
						return;
					}
					if (this.QueuedEventsToFire == null)
					{
						this.QueuedEventsToFire = new List<IEvent>(this.QueuedEvents.Count);
					}
					this.QueuedEventsToFire.Clear();
					this.QueuedEventsToFire.AddRange(this.QueuedEvents);
					this.QueuedEvents.Clear();
					foreach (IEvent @event in this.QueuedEventsToFire)
					{
						try
						{
							Event event2 = @event as Event;
							if (event2 != null)
							{
								this.FireEvent(event2);
							}
							else
							{
								MinEvent minEvent = @event as MinEvent;
								if (minEvent != null && this.WantEvent(minEvent.GetID(), minEvent.GetCascadeLevel()))
								{
									SynchronizeExistenceEvent synchronizeExistenceEvent = minEvent as SynchronizeExistenceEvent;
									if (synchronizeExistenceEvent != null)
									{
										this.HandleEvent(synchronizeExistenceEvent);
									}
									else
									{
										this.HandleEvent(minEvent);
									}
								}
							}
						}
						catch (Exception ex)
						{
							XRLCore.LogError(ex.ToString(), "Queued event error");
						}
					}
				}
			}
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x0020D3B0 File Offset: 0x0020B5B0
		public bool IsAdjacentTo(Zone Z)
		{
			return Math.Abs(Z.wX - this.wX) <= 1 && Math.Abs(Z.wY - this.wY) <= 1;
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x0020D3E1 File Offset: 0x0020B5E1
		public static int GetSuspendabilityTurns()
		{
			if (!Options.CacheEarly)
			{
				return 40;
			}
			return 5;
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x0020D3F0 File Offset: 0x0020B5F0
		public Suspendability GetSuspendability(int Turns)
		{
			long currentTurn = XRLCore.CurrentTurn;
			if (this.LastCached == -1L)
			{
				MetricsManager.LogError("Cache time uninitialized for '" + this.ZoneID + "', initializing now.");
				this.LastCached = currentTurn;
				return Suspendability.TooRecentlyActive;
			}
			if (!this.Built)
			{
				return Suspendability.TooRecentlyActive;
			}
			if (The.ZoneManager.ActiveZone == this)
			{
				return Suspendability.Active;
			}
			if (The.ZoneManager.PinnedZones != null)
			{
				if (The.ZoneManager.PinnedZones.Count > 3)
				{
					MetricsManager.LogException("ZonePins", new Exception("ZonePins"), "game_exception");
					The.ZoneManager.PinnedZones.Clear();
				}
				if (The.ZoneManager.PinnedZones.Contains(this.ZoneID))
				{
					return Suspendability.Pinned;
				}
			}
			if (currentTurn - this.LastActive <= (long)Turns && !this.IsWorldMap())
			{
				return Suspendability.TooRecentlyActive;
			}
			Suspendability suspendability = Suspendability.Suspendable;
			try
			{
				suspendability = GetZoneSuspendabilityEvent.GetFor(this, Suspendability.Suspendable);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error getting suspendability of " + this.ZoneID, ex, "game_exception");
			}
			return suspendability;
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x0020D4FC File Offset: 0x0020B6FC
		public static int GetFreezabilityTurns()
		{
			return 0;
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x0020D500 File Offset: 0x0020B700
		public Freezability GetFreezability(int Turns)
		{
			long currentTurn = XRLCore.CurrentTurn;
			if (this.LastCached == -1L)
			{
				MetricsManager.LogError("Cache time uninitialized for '" + this.ZoneID + "', initializing now.");
				this.LastCached = currentTurn;
			}
			if (!this.Built || currentTurn - this.LastActive <= (long)Turns || currentTurn == this.LastCached)
			{
				return Freezability.TooRecentlyActive;
			}
			if (this.IsWorldMap())
			{
				if (this.ZoneWorld == The.ActiveZone.ZoneWorld)
				{
					this.MarkActive();
					return Freezability.IsWorldMap;
				}
				if (currentTurn - this.LastActive <= 40L)
				{
					return Freezability.TooRecentlyActive;
				}
			}
			Freezability freezability = Freezability.Freezable;
			try
			{
				freezability = GetZoneFreezabilityEvent.GetFor(this, Freezability.Freezable);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error getting freezability of " + this.ZoneID, ex, "game_exception");
			}
			return freezability;
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x0020D5D0 File Offset: 0x0020B7D0
		public Freezability GetFreezability()
		{
			return this.GetFreezability(Zone.GetFreezabilityTurns());
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x0020D5DD File Offset: 0x0020B7DD
		public bool GetFreezable(int Turns)
		{
			return this.GetFreezability(Turns) == Freezability.Freezable;
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x0020D5E9 File Offset: 0x0020B7E9
		public bool GetFreezable()
		{
			return this.GetFreezable(Zone.GetFreezabilityTurns());
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x0020D5F8 File Offset: 0x0020B7F8
		public virtual bool WantEvent(int ID, int cascade)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].WantEvent(ID, cascade))
					{
						return true;
					}
					i++;
				}
			}
			if (!MinEvent.CascadeTo(cascade, 32))
			{
				for (int j = 0; j < this.Height; j++)
				{
					for (int k = 0; k < this.Width; k++)
					{
						if (this.Map[k][j].WantEvent(ID, cascade))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060068B0 RID: 26800 RVA: 0x0020D680 File Offset: 0x0020B880
		public static MethodInfo getHandleEventMethod(Type HandlingType, Type HandledType)
		{
			Dictionary<Type, MethodInfo> dictionary;
			if (!Zone.HandleEventLookup.TryGetValue(HandlingType, out dictionary))
			{
				dictionary = new Dictionary<Type, MethodInfo>();
				Zone.HandleEventLookup.Add(HandlingType, dictionary);
			}
			MethodInfo methodInfo = null;
			if (!dictionary.TryGetValue(HandledType, out methodInfo))
			{
				Zone.HandleEventMethodParameterList[0] = HandledType;
				methodInfo = HandlingType.GetMethod("HandleEvent", BindingFlags.Instance | BindingFlags.Public, null, Zone.HandleEventMethodParameterList, null);
				if (methodInfo != null && methodInfo.GetParameters()[0].ParameterType.Name == "MinEvent")
				{
					methodInfo = null;
				}
				dictionary.Add(HandledType, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x0020D70C File Offset: 0x0020B90C
		public static bool callHandleEventMethod(object obj, MethodInfo method, MinEvent E)
		{
			if (method == null)
			{
				return true;
			}
			bool flag = false;
			object[] array;
			if (!Zone.HandleEventMethodArgumentList1InUse)
			{
				array = Zone.HandleEventMethodArgumentList1;
				Zone.HandleEventMethodArgumentList1InUse = true;
			}
			else if (!Zone.HandleEventMethodArgumentList2InUse)
			{
				array = Zone.HandleEventMethodArgumentList2;
				Zone.HandleEventMethodArgumentList2InUse = true;
			}
			else if (!Zone.HandleEventMethodArgumentList3InUse)
			{
				array = Zone.HandleEventMethodArgumentList3;
				Zone.HandleEventMethodArgumentList3InUse = true;
			}
			else
			{
				array = new object[1];
				flag = true;
			}
			array[0] = E;
			bool flag2;
			try
			{
				flag2 = (bool)method.Invoke(obj, array);
			}
			catch (ArgumentException ex)
			{
				global::UnityEngine.Debug.LogError(string.Concat(new string[]
				{
					"failed to call event handler ",
					obj.GetType().FullName,
					"::",
					method.Name,
					"(",
					method.GetParameters()[0].ParameterType.Name,
					") with parameter ",
					E.GetType().FullName
				}));
				throw ex;
			}
			finally
			{
				array[0] = null;
				if (!flag)
				{
					if (array == Zone.HandleEventMethodArgumentList1)
					{
						Zone.HandleEventMethodArgumentList1InUse = false;
					}
					else if (array == Zone.HandleEventMethodArgumentList2)
					{
						Zone.HandleEventMethodArgumentList2InUse = false;
					}
					else if (array == Zone.HandleEventMethodArgumentList3)
					{
						Zone.HandleEventMethodArgumentList3InUse = false;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x0020D848 File Offset: 0x0020BA48
		public virtual bool HandleEvent(MinEvent E)
		{
			if (this.Parts != null)
			{
				int id = E.GetID();
				int cascadeLevel = E.GetCascadeLevel();
				int i = 0;
				int num = this.Parts.Count;
				while (i < num)
				{
					IZonePart zonePart = this.Parts[i];
					if (zonePart.WantEvent(id, cascadeLevel))
					{
						if (!E.Dispatch(zonePart))
						{
							return false;
						}
						if (!zonePart.HandleEvent(E))
						{
							return false;
						}
						if (num != this.Parts.Count)
						{
							num = this.Parts.Count;
							if (i < num && this.Parts[i] != zonePart)
							{
								i--;
							}
						}
					}
					i++;
				}
			}
			if (!E.CascadeTo(32))
			{
				for (int j = 0; j < this.Height; j++)
				{
					for (int k = 0; k < this.Width; k++)
					{
						if (!this.Map[k][j].HandleEvent(E))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x0020D934 File Offset: 0x0020BB34
		public bool HandleEvent<T>(T E, IEvent ParentEvent) where T : MinEvent
		{
			bool flag = this.HandleEvent(E);
			if (ParentEvent != null)
			{
				ParentEvent.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x060068B4 RID: 26804 RVA: 0x0020D954 File Offset: 0x0020BB54
		public void GetObjectsThatWantEvent(int ID, int Cascade, ICollection<GameObject> Result)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell.ObjectRack objects = this.Map[j][i].Objects;
					GameObject[] array = objects.GetArray();
					int k = 0;
					int count = objects.Count;
					while (k < count)
					{
						if (array[k].WantEvent(ID, Cascade))
						{
							Result.Add(array[k]);
						}
						k++;
					}
				}
			}
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x0020D9C4 File Offset: 0x0020BBC4
		public List<GameObject> GetObjectsThatWantEvent(int ID, int Cascade)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetObjectsThatWantEvent(ID, Cascade, list);
			return list;
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x0020D9E4 File Offset: 0x0020BBE4
		public bool AnyObjectsWantEvent(int ID, int cascade)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell.ObjectRack objects = this.Map[j][i].Objects;
					int k = 0;
					int count = objects.Count;
					while (k < count)
					{
						if (objects[k].WantEvent(ID, cascade))
						{
							return true;
						}
						k++;
					}
				}
			}
			return false;
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x0020DA4C File Offset: 0x0020BC4C
		public void GetWantEventHandlers(int ID, int Cascade, Rack<IEventHandler> Result)
		{
			List<IZonePart> parts = this.Parts;
			if (parts == null)
			{
				return;
			}
			int i = 0;
			int count = parts.Count;
			while (i < count)
			{
				IZonePart zonePart = parts[i];
				if (zonePart.WantEvent(ID, Cascade))
				{
					Result.Add(zonePart);
				}
				i++;
			}
		}

		// Token: 0x060068B8 RID: 26808 RVA: 0x0020DA90 File Offset: 0x0020BC90
		public void GetContentWantEventHandlers(int ID, int Cascade, ICollection<IEventHandler> Result)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					Cell.ObjectRack objects = this.Map[j][i].Objects;
					GameObject[] array = objects.GetArray();
					int k = 0;
					int count = objects.Count;
					while (k < count)
					{
						if (array[k].WantEvent(ID, Cascade))
						{
							Result.Add(array[k]);
						}
						k++;
					}
				}
			}
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x0020DAFE File Offset: 0x0020BCFE
		public void Thawed(long FrozenTicks = 0L)
		{
			ZoneThawedEvent.Send(this, FrozenTicks);
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x0020DB08 File Offset: 0x0020BD08
		public virtual void Activated()
		{
			this.MarkActive();
			try
			{
				ZoneActivatedEvent.Send(this);
				Zone.ePrimePowerSystems.Zone = this;
				this.HandleEvent(Zone.ePrimePowerSystems);
				Zone.ePrimePowerSystems.Reset();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("ZoneActivation", ex, "game_exception");
			}
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x0020DB68 File Offset: 0x0020BD68
		public virtual void Deactivated()
		{
			ZoneDeactivatedEvent.Send(this);
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x0020DB70 File Offset: 0x0020BD70
		public void WantSynchronizeExistence()
		{
			this.QueueEventOnce(Zone.eSynchronizeExistence);
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x0020DB7D File Offset: 0x0020BD7D
		public void CheckWeather(long TurnNumber)
		{
			if (this.HasWeather && TurnNumber > this.NextWindChange)
			{
				this.WindChange(TurnNumber);
			}
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x0020DB97 File Offset: 0x0020BD97
		public void CheckWeather()
		{
			this.CheckWeather(The.Game.TimeTicks);
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x0020DBAC File Offset: 0x0020BDAC
		public void WindChange(long TurnNumber)
		{
			if (string.IsNullOrEmpty(this.WindDirections))
			{
				return;
			}
			int currentWindSpeed = this.CurrentWindSpeed;
			string currentWindDirection = this.CurrentWindDirection;
			string windSpeedDescription = Zone.GetWindSpeedDescription(currentWindSpeed);
			int num = (this.CurrentWindSpeed = this.WindSpeed.RollCached());
			string text = (this.CurrentWindDirection = this.WindDirections.CachedCommaExpansion().GetRandomElement<string>(null));
			this.NextWindChange = TurnNumber + (long)this.WindDuration.RollCached();
			if (this.IsActive() && The.Player.HasSkill("Survival"))
			{
				string windSpeedDescription2 = Zone.GetWindSpeedDescription(num);
				if (windSpeedDescription == windSpeedDescription2 && !currentWindDirection.IsNullOrEmpty())
				{
					if (currentWindDirection != this.CurrentWindDirection)
					{
						if (The.Player.HasSkill("Survival_Trailblazer"))
						{
							MessageQueue.AddPlayerMessage(string.Concat(new string[]
							{
								"The wind changes direction from the ",
								Directions.GetExpandedDirection(currentWindDirection),
								" to the ",
								Directions.GetExpandedDirection(text),
								"."
							}), null, true);
							return;
						}
						MessageQueue.AddPlayerMessage("The wind changes direction.", null, true);
						return;
					}
				}
				else if (The.Player.HasSkill("Survival_Trailblazer"))
				{
					if (windSpeedDescription2 == null)
					{
						MessageQueue.AddPlayerMessage("The wind becomes still.", null, true);
						return;
					}
					if (windSpeedDescription == null)
					{
						MessageQueue.AddPlayerMessage(string.Concat(new string[]
						{
							"The wind begins blowing at ",
							windSpeedDescription2,
							" from the ",
							Directions.GetExpandedDirection(text),
							"."
						}), null, true);
						return;
					}
					if (num > currentWindSpeed)
					{
						MessageQueue.AddPlayerMessage(string.Concat(new string[]
						{
							"The wind intensifies to ",
							windSpeedDescription2,
							", blowing from the ",
							Directions.GetExpandedDirection(text),
							"."
						}), null, true);
						return;
					}
					if (num < currentWindSpeed)
					{
						MessageQueue.AddPlayerMessage(string.Concat(new string[]
						{
							"The wind calms to ",
							windSpeedDescription2,
							", blowing from the ",
							Directions.GetExpandedDirection(text),
							"."
						}), null, true);
						return;
					}
				}
				else
				{
					if (windSpeedDescription2 == null)
					{
						MessageQueue.AddPlayerMessage("The wind becomes still.", null, true);
						return;
					}
					if (windSpeedDescription == null)
					{
						MessageQueue.AddPlayerMessage("The wind begins blowing at " + windSpeedDescription2 + ".", null, true);
						return;
					}
					if (num > currentWindSpeed)
					{
						MessageQueue.AddPlayerMessage("The wind intensifies to " + windSpeedDescription2 + ".", null, true);
						return;
					}
					if (num < currentWindSpeed)
					{
						MessageQueue.AddPlayerMessage("The wind calms to " + windSpeedDescription2 + ".", null, true);
					}
				}
			}
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x0020DE12 File Offset: 0x0020C012
		public void WindChange()
		{
			this.CheckWeather(The.Game.TimeTicks);
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x0020DE24 File Offset: 0x0020C024
		public static string GetWindSpeedDescription(int kph)
		{
			if (kph <= 0)
			{
				return null;
			}
			if (kph <= 10)
			{
				return "a very gentle breeze";
			}
			if (kph <= 20)
			{
				return "a gentle breeze";
			}
			if (kph <= 30)
			{
				return "a moderate breeze";
			}
			if (kph <= 40)
			{
				return "a fresh breeze";
			}
			if (kph <= 50)
			{
				return "a strong breeze";
			}
			if (kph <= 60)
			{
				return "near gale intensity";
			}
			if (kph <= 70)
			{
				return "gale intensity";
			}
			if (kph <= 90)
			{
				return "strong gale intensity";
			}
			if (kph <= 100)
			{
				return "storm intensity";
			}
			if (kph <= 120)
			{
				return "violent storm intensity";
			}
			return "hurricane intensity";
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x0020DEAC File Offset: 0x0020C0AC
		public void FlushNavigationCaches()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Map[j][i].FlushNavigationCache();
				}
			}
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x0020DEEC File Offset: 0x0020C0EC
		public string GetDirectionFromZone(Zone From)
		{
			if (From == null || From == this)
			{
				return ".";
			}
			if (this.Z != From.Z)
			{
				if (this.Z < From.Z)
				{
					return "U";
				}
				if (this.Z > From.Z)
				{
					return "D";
				}
				return ".";
			}
			else if (this.ResolvedX == From.ResolvedX)
			{
				if (this.ResolvedY == From.ResolvedY)
				{
					return ".";
				}
				if (this.ResolvedY > From.ResolvedY)
				{
					return "S";
				}
				return "N";
			}
			else if (this.ResolvedX > From.ResolvedX)
			{
				if (this.ResolvedY == From.ResolvedY)
				{
					return "E";
				}
				if (this.ResolvedY > From.ResolvedY)
				{
					return "SE";
				}
				return "NE";
			}
			else
			{
				if (this.ResolvedY == From.ResolvedY)
				{
					return "W";
				}
				if (this.ResolvedY > From.ResolvedY)
				{
					return "SW";
				}
				return "NW";
			}
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x0020DFE7 File Offset: 0x0020C1E7
		public void Constrain(ref int x, ref int y)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (y < 0)
			{
				y = 0;
			}
			if (x >= this.Width)
			{
				x = this.Width - 1;
			}
			if (y >= this.Height)
			{
				y = this.Height - 1;
			}
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x0020E024 File Offset: 0x0020C224
		public void Constrain(ref int x1, ref int y1, ref int x2, ref int y2)
		{
			if (x1 < 0)
			{
				x1 = 0;
			}
			if (x2 < 0)
			{
				x2 = 0;
			}
			if (y1 < 0)
			{
				y1 = 0;
			}
			if (y2 < 0)
			{
				y2 = 0;
			}
			if (x1 >= this.Width)
			{
				x1 = this.Width - 1;
			}
			if (x2 >= this.Width)
			{
				x2 = this.Width - 1;
			}
			if (y1 >= this.Height)
			{
				y1 = this.Height - 1;
			}
			if (y2 >= this.Height)
			{
				y2 = this.Height - 1;
			}
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x0020E0A8 File Offset: 0x0020C2A8
		public Cell GetPullDownLocation(GameObject who)
		{
			Cell cell;
			GameObject gameObject;
			GetTransitiveLocationEvent.GetFor(this, "PullDown,WorldMap", who, who, who.CurrentCell, out cell, out gameObject);
			if (cell != null)
			{
				return cell;
			}
			try
			{
				string text = this.GetZoneProperty("PullDownLocation", null) ?? this.GetZoneProperty("pulldownLocation", "");
				if (!text.IsNullOrEmpty() && text.IndexOf(',') != -1)
				{
					ReadOnlySpan<char> readOnlySpan;
					ReadOnlySpan<char> readOnlySpan2;
					text.AsDelimitedSpans(',', out readOnlySpan, out readOnlySpan2);
					Cell cell2 = this.GetCell(int.Parse(readOnlySpan, NumberStyles.Integer, null), int.Parse(readOnlySpan2, NumberStyles.Integer, null));
					if (cell2 != null && cell2.IsPassable(who, true))
					{
						return cell2;
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("exception processing pulldown location", ex);
			}
			for (int i = this.Height - 1; i >= 0; i--)
			{
				for (int j = this.Width / 2; j < this.Width - 1; j++)
				{
					Cell cell2 = this.GetCell(j, i);
					if (cell2 != null && cell2.IsReachable() && cell2.IsPassable(who, true) && !cell2.HasSwimmingDepthLiquid() && !cell2.HasSpawnBlocker())
					{
						return cell2;
					}
					cell2 = this.GetCell(this.Width - 1 - j, i);
					if (cell2 != null && cell2.IsReachable() && cell2.IsPassable(who, true) && !cell2.HasSwimmingDepthLiquid() && !cell2.HasSpawnBlocker())
					{
						return cell2;
					}
				}
			}
			for (int k = this.Height - 1; k >= 0; k--)
			{
				for (int l = this.Width / 2; l < this.Width - 1; l++)
				{
					Cell cell2 = this.GetCell(l, k);
					if (cell2 != null && cell2.IsReachable() && cell2.IsPassable(who, true))
					{
						return cell2;
					}
					cell2 = this.GetCell(this.Width - 1 - l, k);
					if (cell2 != null && cell2.IsReachable() && cell2.IsPassable(who, true))
					{
						return cell2;
					}
				}
			}
			for (int m = this.Height - 1; m >= 0; m--)
			{
				for (int n = this.Width / 2; n < this.Width - 1; n++)
				{
					Cell cell2 = this.GetCell(n, m);
					if (cell2 != null && cell2.IsPassable(who, true))
					{
						return cell2;
					}
					cell2 = this.GetCell(this.Width - 1 - n, m);
					if (cell2 != null && cell2.IsPassable(who, true))
					{
						return cell2;
					}
				}
			}
			return this.GetCell(this.Width / 2, this.Height - 1);
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x0020E318 File Offset: 0x0020C518
		public float GetCoverAt(int X, int Y)
		{
			if (X >= 0 && Y >= 0 && X < this.Width && Y <= this.Height)
			{
				MissileMapType missileMapType = this.MissileMap[X][Y];
				if (missileMapType == MissileMapType.Wall)
				{
					return 1f;
				}
				if (missileMapType == MissileMapType.VeryHeavyCover)
				{
					return 0.8f;
				}
				if (missileMapType == MissileMapType.HeavyCover)
				{
					return 0.5f;
				}
				if (missileMapType == MissileMapType.MediumCover)
				{
					return 0.3f;
				}
				if (missileMapType == MissileMapType.LightCover)
				{
					return 0.1f;
				}
				if (missileMapType == MissileMapType.VeryLightCover)
				{
					return 0.05f;
				}
			}
			return 0f;
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x0020E38C File Offset: 0x0020C58C
		public T GetPart<T>() where T : IZonePart
		{
			if (this.Parts != null)
			{
				string name = typeof(T).Name;
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Name == name)
					{
						return this.Parts[i] as T;
					}
					i++;
				}
			}
			return default(T);
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x0020E404 File Offset: 0x0020C604
		public IZonePart GetPart(string Name)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Name == Name)
					{
						return this.Parts[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x0020E458 File Offset: 0x0020C658
		public IZonePart GetPartExcept(string Name, IZonePart Skip)
		{
			if (this.Parts != null)
			{
				if (Skip == null)
				{
					return this.GetPart(Name);
				}
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Name == Name && this.Parts[i] != Skip)
					{
						return this.Parts[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x0020E4C8 File Offset: 0x0020C6C8
		public bool TryGetPart<T>(out T Part) where T : IZonePart
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].GetType() == typeof(T))
					{
						Part = this.Parts[i] as T;
						return true;
					}
					i++;
				}
			}
			Part = default(T);
			return false;
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x0020E538 File Offset: 0x0020C738
		protected void AddPartInternals(IZonePart Part, bool Initial = false, bool Creation = false)
		{
			this.FlushWantTurnTickCache();
			if (Part == null)
			{
				return;
			}
			if (this.Parts == null)
			{
				this.Parts = new List<IZonePart>();
			}
			this.Parts.Add(Part);
			Part.ParentZone = this;
			if (Initial)
			{
				Part.Initialize();
			}
			Part.Attach();
			if (!Creation)
			{
				Part.AddedAfterCreation();
			}
		}

		// Token: 0x060068CD RID: 26829 RVA: 0x0020E58D File Offset: 0x0020C78D
		public IZonePart AddPart(IZonePart Part, bool Creation = false)
		{
			this.AddPartInternals(Part, true, Creation);
			return Part;
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x0020E599 File Offset: 0x0020C799
		public T AddPart<T>(T P, bool Creation = false) where T : IZonePart
		{
			return this.AddPart(P, Creation) as T;
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x0020E5B2 File Offset: 0x0020C7B2
		public T AddPart<T>(bool Creation = false) where T : IZonePart, new()
		{
			return this.AddPart(new T(), Creation) as T;
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x0020E5D0 File Offset: 0x0020C7D0
		public T RequirePart<T>(bool Creation = false) where T : IZonePart, new()
		{
			T part = this.GetPart<T>();
			if (part != null)
			{
				return part;
			}
			return this.AddPart<T>(Creation);
		}

		// Token: 0x060068D1 RID: 26833 RVA: 0x0020E5F5 File Offset: 0x0020C7F5
		public IEnumerable<IZonePart> LoopParts()
		{
			if (this.Parts == null)
			{
				yield break;
			}
			foreach (IZonePart zonePart in this.Parts)
			{
				yield return zonePart;
			}
			List<IZonePart>.Enumerator enumerator = default(List<IZonePart>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060068D2 RID: 26834 RVA: 0x0020E608 File Offset: 0x0020C808
		public void ForeachPart(Action<IZonePart> Proc)
		{
			if (Proc != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					Proc(this.Parts[i]);
					i++;
				}
			}
		}

		// Token: 0x060068D3 RID: 26835 RVA: 0x0020E644 File Offset: 0x0020C844
		public bool ForeachPart(Predicate<IZonePart> Proc)
		{
			if (Proc != null)
			{
				foreach (IZonePart zonePart in this.Parts)
				{
					if (!Proc(zonePart))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060068D4 RID: 26836 RVA: 0x0020E6A4 File Offset: 0x0020C8A4
		public void ForeachPartDescendedFrom<T>(Action<T> Proc) where T : IZonePart
		{
			if (this.Parts != null)
			{
				foreach (IZonePart zonePart in this.Parts)
				{
					T t = zonePart as T;
					if (t != null)
					{
						Proc(t);
					}
				}
			}
		}

		// Token: 0x060068D5 RID: 26837 RVA: 0x0020E714 File Offset: 0x0020C914
		public bool ForeachPartDescendedFrom<T>(Predicate<T> Proc) where T : IPart
		{
			if (this.Parts != null)
			{
				foreach (IZonePart zonePart in this.Parts)
				{
					T t = zonePart as T;
					if (t != null && !Proc(t))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060068D6 RID: 26838 RVA: 0x0020E78C File Offset: 0x0020C98C
		public bool RemovePart(Type Part)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].GetType() == Part && this.RemovePart(this.Parts[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060068D7 RID: 26839 RVA: 0x0020E7EC File Offset: 0x0020C9EC
		public bool RemovePart(string Part)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Name == Part && this.RemovePart(this.Parts[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x0020E84C File Offset: 0x0020CA4C
		public bool RemovePart<T>() where T : IZonePart
		{
			IZonePart zonePart = this.GetPart<T>();
			return zonePart != null && this.RemovePart(zonePart);
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x0020E871 File Offset: 0x0020CA71
		public bool RemovePart(IZonePart Part)
		{
			this.FlushWantTurnTickCache();
			if (Part == null)
			{
				return false;
			}
			if (this.Parts == null)
			{
				return false;
			}
			if (!this.Parts.Remove(Part))
			{
				return false;
			}
			Part.Remove();
			Part.ParentZone = null;
			return true;
		}

		// Token: 0x060068DA RID: 26842 RVA: 0x0020E8A6 File Offset: 0x0020CAA6
		public static bool Validate(Zone Zone)
		{
			return Zone != null && !Zone.Stale;
		}

		// Token: 0x060068DB RID: 26843 RVA: 0x0020E8B6 File Offset: 0x0020CAB6
		public static bool Validate(ref Zone Zone)
		{
			if (Zone != null)
			{
				if (!Zone.Stale)
				{
					return true;
				}
				Zone = null;
			}
			return false;
		}

		// Token: 0x060068DC RID: 26844 RVA: 0x0020E8CD File Offset: 0x0020CACD
		public void FlushWantTurnTickCache()
		{
			this.WantTurnTickCache = 0;
		}

		// Token: 0x060068DD RID: 26845 RVA: 0x0020E8D8 File Offset: 0x0020CAD8
		public bool WantTurnTick()
		{
			if (this.Parts != null)
			{
				if ((this.WantTurnTickCache & 1) != 0)
				{
					return true;
				}
				if ((this.WantTurnTickCache & 2) != 0)
				{
					return false;
				}
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].WantTurnTick())
					{
						this.WantTurnTickCache |= 1;
						return true;
					}
					i++;
				}
				this.WantTurnTickCache |= 2;
			}
			return false;
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x0020E954 File Offset: 0x0020CB54
		public void TurnTick(long TimeTick, int Amount)
		{
			if (this.Parts == null)
			{
				return;
			}
			int i = 0;
			int num = this.Parts.Count;
			while (i < num)
			{
				IZonePart zonePart = this.Parts[i];
				zonePart.TurnTick(TimeTick, Amount);
				if (num != this.Parts.Count)
				{
					num = this.Parts.Count;
					if (i < num && this.Parts[i] != zonePart)
					{
						i--;
					}
				}
				i++;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060068DF RID: 26847 RVA: 0x0020E9C8 File Offset: 0x0020CBC8
		// (set) Token: 0x060068E0 RID: 26848 RVA: 0x0020E9D0 File Offset: 0x0020CBD0
		[Obsolete("Use Suspended")]
		public bool bSuspended
		{
			get
			{
				return this.Suspended;
			}
			set
			{
				this.Suspended = value;
			}
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x0020E9D9 File Offset: 0x0020CBD9
		[Obsolete("Use FindObjectById")]
		public GameObject findObjectById(string id)
		{
			return this.FindObjectByID(id);
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060068E2 RID: 26850 RVA: 0x0020E9E2 File Offset: 0x0020CBE2
		[Obsolete("Use ResolvedLocation")]
		public Location2D resolvedLocation
		{
			get
			{
				return Location2D.Get(this.ResolvedX, this.ResolvedY);
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060068E3 RID: 26851 RVA: 0x0020E9F5 File Offset: 0x0020CBF5
		[Obsolete("Use ResolvedX")]
		public int resolvedX
		{
			get
			{
				return this.wX * 3 + this.X;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060068E4 RID: 26852 RVA: 0x0020EA06 File Offset: 0x0020CC06
		[Obsolete("Use ResolvedY")]
		public int resolvedY
		{
			get
			{
				return this.wY * 3 + this.Y;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060068E5 RID: 26853 RVA: 0x0020EA17 File Offset: 0x0020CC17
		[Obsolete("Use ResolvedStartX")]
		public int resolvedStartX
		{
			get
			{
				return this.ResolvedX * 80;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060068E6 RID: 26854 RVA: 0x0020EA22 File Offset: 0x0020CC22
		[Obsolete("Use ResolvedStartY")]
		public int resolvedStartY
		{
			get
			{
				return this.ResolvedY * 25;
			}
		}

		// Token: 0x060068E8 RID: 26856 RVA: 0x0020EB80 File Offset: 0x0020CD80
		[CompilerGenerated]
		internal static void <SetMouseMoveAutoexploreSeeds>g__Check|156_0(Location2D l, ref Zone.<>c__DisplayClass156_0 A_1)
		{
			if (l == null)
			{
				return;
			}
			if (Zone.visited.Contains(l))
			{
				return;
			}
			if (l.X < 0)
			{
				return;
			}
			if (l.Y < 0)
			{
				return;
			}
			if (l.X >= A_1.zone.Width)
			{
				return;
			}
			if (l.Y >= A_1.zone.Height)
			{
				return;
			}
			if (A_1.zone.GetCell(l).IsExplored())
			{
				return;
			}
			Zone.visited.Add(l);
			Zone.horizon.Enqueue(l);
		}

		// Token: 0x060068E9 RID: 26857 RVA: 0x0020EC0C File Offset: 0x0020CE0C
		[CompilerGenerated]
		internal static bool <SetInfluenceAutoexploreSeeds>g__ValidCell|158_0(Cell Cell)
		{
			return !Cell.IsExplored() && (!Cell.HasWall() || Cell.HasAdjacentLocalNonwallCell());
		}

		// Token: 0x060068EA RID: 26858 RVA: 0x0020EC28 File Offset: 0x0020CE28
		[CompilerGenerated]
		internal static bool <SetInfluenceAutoexploreSeeds>g__ValidObject|158_1(GameObject Object, Zone Zone, bool ExploreChests)
		{
			return Object.IsValid() && Object.CurrentZone == Zone && ((ExploreChests && Object.ShouldAutoexploreAsChest()) || AutoexploreObjectEvent.Check(The.Player, Object, AutoAct.Setting, AutoAct.Action));
		}

		// Token: 0x04002FB9 RID: 12217
		private const int FLOOD_WIDTH = 80;

		// Token: 0x04002FBA RID: 12218
		private const int FLOOD_HEIGHT = 25;

		// Token: 0x04002FBB RID: 12219
		private const int ARR_LEN = 2048;

		// Token: 0x04002FBC RID: 12220
		[NonSerialized]
		private static int FloodValue = 0;

		// Token: 0x04002FBD RID: 12221
		[NonSerialized]
		public int[,] FloodMap = new int[80, 25];

		// Token: 0x04002FBE RID: 12222
		[NonSerialized]
		private static Location2D[] FloodArray = new Location2D[4096];

		// Token: 0x04002FBF RID: 12223
		[NonSerialized]
		private static Zone.Segment[] FloodSegments = new Zone.Segment[2048];

		// Token: 0x04002FC0 RID: 12224
		[NonSerialized]
		private static Rack<Location2D> FloodVisited;

		// Token: 0x04002FC1 RID: 12225
		[NonSerialized]
		private static Dictionary<Cell, int> FloodNextIndex = new Dictionary<Cell, int>(64);

		// Token: 0x04002FC2 RID: 12226
		[NonSerialized]
		private static List<Cell> FloodNext = new List<Cell>(64);

		// Token: 0x04002FC3 RID: 12227
		[NonSerialized]
		private static List<int> FloodNextRadii = new List<int>(64);

		// Token: 0x04002FC4 RID: 12228
		[NonSerialized]
		private static List<Cell> FloodCurrent = new List<Cell>(64);

		// Token: 0x04002FC5 RID: 12229
		[NonSerialized]
		private static List<int> FloodCurrentRadii = new List<int>(64);

		// Token: 0x04002FC6 RID: 12230
		[NonSerialized]
		private static Dictionary<Cell, int> Flooded = null;

		// Token: 0x04002FC7 RID: 12231
		[NonSerialized]
		private static string InFloodMethod = null;

		// Token: 0x04002FC8 RID: 12232
		public const int NAV_SMART = 1;

		// Token: 0x04002FC9 RID: 12233
		public const int NAV_BURROWER = 2;

		// Token: 0x04002FCA RID: 12234
		public const int NAV_AUTOEXPLORING = 4;

		// Token: 0x04002FCB RID: 12235
		public const int NAV_FLYING = 8;

		// Token: 0x04002FCC RID: 12236
		public const int NAV_WALL_WALKER = 16;

		// Token: 0x04002FCD RID: 12237
		public const int NAV_IGNORES_WALLS = 32;

		// Token: 0x04002FCE RID: 12238
		public const int NAV_SWIMMING = 64;

		// Token: 0x04002FCF RID: 12239
		public const int NAV_SLIMEWALKING = 128;

		// Token: 0x04002FD0 RID: 12240
		public const int NAV_AQUATIC = 256;

		// Token: 0x04002FD1 RID: 12241
		public const int NAV_POLYPWALKING = 512;

		// Token: 0x04002FD2 RID: 12242
		public const int NAV_STRUTWALKING = 1024;

		// Token: 0x04002FD3 RID: 12243
		public const int NAV_JUGGERNAUT = 2048;

		// Token: 0x04002FD4 RID: 12244
		public const int NAV_REEFER = 4096;

		// Token: 0x04002FD5 RID: 12245
		public const int NAV_IGNORE_CREATURES = 8192;

		// Token: 0x04002FD6 RID: 12246
		public const int NAV_IGNORE_GASES = 16384;

		// Token: 0x04002FD7 RID: 12247
		public const int NAV_UNBREATHING = 32768;

		// Token: 0x04002FD8 RID: 12248
		public const int NAV_FILTH_AFFINITY = 65536;

		// Token: 0x04002FD9 RID: 12249
		public const int NAV_OUT_OF_PHASE = 131072;

		// Token: 0x04002FDA RID: 12250
		public const int NAV_OMNIPHASE = 262144;

		// Token: 0x04002FDB RID: 12251
		public const int NAV_NULLPHASE = 524288;

		// Token: 0x04002FDC RID: 12252
		public const int NAV_FLEX_PHASE = 1048576;

		// Token: 0x04002FDD RID: 12253
		public const int NAV_LAZY_INIT = 268435456;

		// Token: 0x04002FDE RID: 12254
		public const int NAV_REEFWALKING = 1536;

		// Token: 0x04002FDF RID: 12255
		public const int REEFER_NAV_WEIGHT = 25;

		// Token: 0x04002FE0 RID: 12256
		public const int NAV_WEIGHT_DETER = 999;

		// Token: 0x04002FE1 RID: 12257
		public const int NAV_WEIGHT_DENY = 9999;

		// Token: 0x04002FE2 RID: 12258
		public const int RANGE_UNLIMITED = 9999;

		// Token: 0x04002FE3 RID: 12259
		public bool Suspended;

		/// <summary>This is a stale reference to a released/frozen zone.</summary>
		// Token: 0x04002FE4 RID: 12260
		[NonSerialized]
		public bool Stale;

		// Token: 0x04002FE5 RID: 12261
		public long LastActive;

		/// <summary>When this zone last entered into the cache, either newly built or thawed.</summary>
		// Token: 0x04002FE6 RID: 12262
		public long LastCached = -1L;

		// Token: 0x04002FE7 RID: 12263
		public long LastPlayerPresence = -1L;

		// Token: 0x04002FE8 RID: 12264
		public string GroundLiquid = "salt-1000";

		// Token: 0x04002FE9 RID: 12265
		[NonSerialized]
		public List<IZonePart> Parts;

		// Token: 0x04002FEA RID: 12266
		[NonSerialized]
		private static LocationList _locationList;

		// Token: 0x04002FEB RID: 12267
		private Graveyard _Graveyard;

		// Token: 0x04002FEC RID: 12268
		[NonSerialized]
		private int _NewTier;

		// Token: 0x04002FED RID: 12269
		public string _ZoneID;

		// Token: 0x04002FEE RID: 12270
		public string ZoneWorld = "";

		// Token: 0x04002FEF RID: 12271
		public int BaseTemperature = 25;

		// Token: 0x04002FF0 RID: 12272
		public int Tier = 1;

		// Token: 0x04002FF1 RID: 12273
		public int BuildTries;

		// Token: 0x04002FF2 RID: 12274
		public int X;

		// Token: 0x04002FF3 RID: 12275
		public int Y;

		// Token: 0x04002FF4 RID: 12276
		public int Z;

		// Token: 0x04002FF5 RID: 12277
		public int wX;

		// Token: 0x04002FF6 RID: 12278
		public int wY;

		// Token: 0x04002FF7 RID: 12279
		public bool Built;

		// Token: 0x04002FF8 RID: 12280
		public int Width;

		// Token: 0x04002FF9 RID: 12281
		public int Height;

		// Token: 0x04002FFA RID: 12282
		[NonSerialized]
		public static int[,] SoundWalls = new int[80, 25];

		// Token: 0x04002FFB RID: 12283
		[NonSerialized]
		public static InfluenceMap SoundMap = new InfluenceMap(80, 25);

		// Token: 0x04002FFC RID: 12284
		[NonSerialized]
		public static Rack<ISoundRenderer> SoundRenderers = new Rack<ISoundRenderer>();

		// Token: 0x04002FFD RID: 12285
		public static bool SoundMapDirty = true;

		// Token: 0x04002FFE RID: 12286
		private static HashSet<Location2D> visited = new HashSet<Location2D>();

		// Token: 0x04002FFF RID: 12287
		private static Queue<Location2D> horizon = new Queue<Location2D>();

		// Token: 0x04003000 RID: 12288
		private static int LastActCount;

		// Token: 0x04003001 RID: 12289
		[NonSerialized]
		public LightLevel[] LightMap;

		// Token: 0x04003002 RID: 12290
		[NonSerialized]
		public bool[] ExploredMap;

		// Token: 0x04003003 RID: 12291
		[NonSerialized]
		public bool[] VisibilityMap;

		// Token: 0x04003004 RID: 12292
		[NonSerialized]
		public bool[,] ReachableMap;

		// Token: 0x04003005 RID: 12293
		[NonSerialized]
		public NavigationWeight[,] NavigationMap;

		// Token: 0x04003006 RID: 12294
		[NonSerialized]
		public List<CachedZoneConnection> ZoneConnectionCache = new List<CachedZoneConnection>();

		// Token: 0x04003007 RID: 12295
		[NonSerialized]
		public Cell[][] Map;

		// Token: 0x04003008 RID: 12296
		[NonSerialized]
		public MissileMapType[][] MissileMap;

		// Token: 0x04003009 RID: 12297
		private string region;

		// Token: 0x0400300A RID: 12298
		private static readonly List<Point> LineReturnCache = new List<Point>(16);

		// Token: 0x0400300B RID: 12299
		private string _DisplayName;

		// Token: 0x0400300C RID: 12300
		[NonSerialized]
		private static CleanQueue<Location2D> CellQueue = new CleanQueue<Location2D>();

		// Token: 0x0400300D RID: 12301
		[NonSerialized]
		private static Dictionary<Location2D, bool> VisitedList = new Dictionary<Location2D, bool>(2000);

		// Token: 0x0400300E RID: 12302
		[NonSerialized]
		public int RenderedObjects;

		// Token: 0x0400300F RID: 12303
		[NonSerialized]
		private static RenderEvent eRender = new RenderEvent();

		// Token: 0x04003010 RID: 12304
		[NonSerialized]
		private static RenderSoundEvent eRenderSound = new RenderSoundEvent();

		// Token: 0x04003011 RID: 12305
		[NonSerialized]
		private static List<GameObject> WantsToPaint = new List<GameObject>();

		// Token: 0x04003012 RID: 12306
		[NonSerialized]
		private List<Cell> CellList;

		// Token: 0x04003013 RID: 12307
		[NonSerialized]
		private bool? _isWorldMapCache;

		// Token: 0x04003014 RID: 12308
		[NonSerialized]
		public List<IEvent> QueuedEvents;

		// Token: 0x04003015 RID: 12309
		[NonSerialized]
		public List<IEvent> QueuedEventsToFire;

		// Token: 0x04003016 RID: 12310
		[NonSerialized]
		private static Type[] HandleEventMethodParameterList = new Type[1];

		// Token: 0x04003017 RID: 12311
		[NonSerialized]
		private static object[] HandleEventMethodArgumentList1 = new object[1];

		// Token: 0x04003018 RID: 12312
		[NonSerialized]
		private static bool HandleEventMethodArgumentList1InUse;

		// Token: 0x04003019 RID: 12313
		[NonSerialized]
		private static object[] HandleEventMethodArgumentList2 = new object[1];

		// Token: 0x0400301A RID: 12314
		[NonSerialized]
		private static bool HandleEventMethodArgumentList2InUse;

		// Token: 0x0400301B RID: 12315
		[NonSerialized]
		private static object[] HandleEventMethodArgumentList3 = new object[1];

		// Token: 0x0400301C RID: 12316
		[NonSerialized]
		private static bool HandleEventMethodArgumentList3InUse;

		// Token: 0x0400301D RID: 12317
		[NonSerialized]
		private static Dictionary<Type, Dictionary<Type, MethodInfo>> HandleEventLookup = new Dictionary<Type, Dictionary<Type, MethodInfo>>();

		// Token: 0x0400301E RID: 12318
		[NonSerialized]
		private static PrimePowerSystemsEvent ePrimePowerSystems = new PrimePowerSystemsEvent();

		// Token: 0x0400301F RID: 12319
		[NonSerialized]
		private static SynchronizeExistenceEvent eSynchronizeExistence = new SynchronizeExistenceEvent();

		// Token: 0x04003020 RID: 12320
		[NonSerialized]
		private static List<IZonePart> TurnTickParts = new List<IZonePart>();

		// Token: 0x04003021 RID: 12321
		[NonSerialized]
		private static bool TurnTickPartsInUse;

		// Token: 0x04003022 RID: 12322
		private byte WantTurnTickCache;

		// Token: 0x02001A97 RID: 6807
		private readonly struct Segment
		{
			// Token: 0x0600E849 RID: 59465 RVA: 0x00538124 File Offset: 0x00536324
			public Segment(int Index)
			{
				this.Head = Index;
				this.Tail = Index;
			}

			// Token: 0x0600E84A RID: 59466 RVA: 0x00538134 File Offset: 0x00536334
			public Segment(int Head, int Tail)
			{
				this.Head = Head;
				this.Tail = Tail;
			}

			// Token: 0x040069E2 RID: 27106
			public readonly int Head;

			// Token: 0x040069E3 RID: 27107
			public readonly int Tail;
		}

		// Token: 0x02001A98 RID: 6808
		public ref struct ObjectEnumerator
		{
			// Token: 0x17000EDD RID: 3805
			// (get) Token: 0x0600E84B RID: 59467 RVA: 0x00538144 File Offset: 0x00536344
			public GameObject Current
			{
				get
				{
					return this.Object;
				}
			}

			// Token: 0x0600E84C RID: 59468 RVA: 0x0053814C File Offset: 0x0053634C
			public ObjectEnumerator(Zone Zone)
			{
				this.Zone = Zone;
				this.Object = null;
				this.X = (this.Y = 0);
				this.Objects = Zone.Map[this.X][this.Y].Objects;
				this.Z = this.Objects.Count - 1;
			}

			// Token: 0x0600E84D RID: 59469 RVA: 0x005381A9 File Offset: 0x005363A9
			public Zone.ObjectEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600E84E RID: 59470 RVA: 0x005381B4 File Offset: 0x005363B4
			public bool MoveNext()
			{
				int num;
				while (this.Z < 0)
				{
					num = this.X + 1;
					this.X = num;
					if (num >= this.Zone.Width)
					{
						num = this.Y + 1;
						this.Y = num;
						if (num >= this.Zone.Height)
						{
							this.Object = null;
							return false;
						}
						this.X = 0;
					}
					this.Objects = this.Zone.Map[this.X][this.Y].Objects;
					this.Z = this.Objects.Count - 1;
				}
				Rack<GameObject> objects = this.Objects;
				num = this.Z;
				this.Z = num - 1;
				this.Object = objects[num];
				return true;
			}

			// Token: 0x040069E4 RID: 27108
			private Zone Zone;

			// Token: 0x040069E5 RID: 27109
			private Rack<GameObject> Objects;

			// Token: 0x040069E6 RID: 27110
			private GameObject Object;

			// Token: 0x040069E7 RID: 27111
			private int X;

			// Token: 0x040069E8 RID: 27112
			private int Y;

			// Token: 0x040069E9 RID: 27113
			private int Z;
		}
	}
}
