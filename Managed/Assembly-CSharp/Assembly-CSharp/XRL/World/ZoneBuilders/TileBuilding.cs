using System;
using System.Collections.Generic;
using System.IO;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AFB RID: 2811
	[HasModSensitiveStaticCache]
	public class TileBuilding
	{
		// Token: 0x0600707B RID: 28795 RVA: 0x00280C6B File Offset: 0x0027EE6B
		[PreGameCacheInit]
		public static void CheckInit()
		{
			if (TileBuilding.Tiles == null)
			{
				Loading.LoadTask("Loading BuildingTiles.txt", new Action(TileBuilding.LoadTiles), true);
			}
		}

		// Token: 0x0600707C RID: 28796 RVA: 0x00280C8C File Offset: 0x0027EE8C
		private static void LoadTiles()
		{
			TileBuilding.Tiles = new TileManager();
			try
			{
				List<string> TileFiles = new List<string>();
				TileFiles.Add(DataManager.FilePath("BuildingTiles.txt"));
				TileFiles.AddRange(Directory.GetFiles(DataManager.FilePath("."), "BuildingTiles_*.txt", SearchOption.AllDirectories));
				ModManager.ForEachFile("BuildingTiles.txt", delegate(string path)
				{
					TileFiles.Add(path);
				}, false, false);
				foreach (string text in TileFiles)
				{
					using (StreamReader streamReader = new StreamReader(text))
					{
						string text2 = "";
						bool flag = true;
						bool flag2 = true;
						bool flag3 = false;
						bool flag4 = true;
						bool flag5 = true;
						bool flag6 = false;
						while (text2 != null)
						{
							for (;;)
							{
								text2 = streamReader.ReadLine();
								while (text2 != null && text2.Length <= 0)
								{
									text2 = streamReader.ReadLine();
								}
								if (text2 == null || text2 == "end")
								{
									goto IL_0239;
								}
								if (text2.Length > 0 && text2[0] == '[')
								{
									TileBuilding.Tiles.Markers.Add(text2.Trim(new char[] { '[', ']' }), TileBuilding.Tiles.Tiles.Count);
									flag4 = true;
									flag5 = true;
									flag6 = false;
								}
								else if (text2 == "*X")
								{
									flag6 = true;
								}
								else if (text2 == "X")
								{
									flag3 = true;
								}
								else if (text2 == "*NF")
								{
									flag5 = false;
								}
								else if (text2 == "*NM")
								{
									flag4 = false;
								}
								else if (text2 == "NF")
								{
									flag2 = false;
								}
								else
								{
									if (!(text2 == "NM"))
									{
										break;
									}
									flag = false;
								}
							}
							List<string> list = new List<string>();
							list.Add(text2);
							for (int i = 0; i < TileBuilding.Tiles.TileSize - 1; i++)
							{
								list.Add(streamReader.ReadLine());
							}
							if (!flag3 && !flag6)
							{
								TileBuilding.Tiles.AddTile(list, flag && flag4, flag2 && flag5);
							}
							flag = true;
							flag2 = true;
							flag3 = false;
							foreach (string text3 in list)
							{
							}
						}
						IL_0239:;
					}
				}
			}
			catch (Exception ex)
			{
				XRLCore.LogError(ex);
			}
		}

		// Token: 0x0600707D RID: 28797 RVA: 0x00280F68 File Offset: 0x0027F168
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).AddObject("Dirty", null, null, null, null);
			this.CustomChar = new Dictionary<char, string>();
			if (this.CustomCharMap != null)
			{
				foreach (string text in this.CustomCharMap.Split(',', StringSplitOptions.None))
				{
					if (text != "")
					{
						string[] array2 = text.Split('@', StringSplitOptions.None);
						this.CustomChar.Add(array2[0][0], array2[1]);
					}
				}
			}
			TileType[,] array3 = new TileType[this.Wide * (TileBuilding.Tiles.TileSize - 1) + 1, this.High * (TileBuilding.Tiles.TileSize - 1) + 1];
			char[,] array4 = new char[this.Wide * (TileBuilding.Tiles.TileSize - 1) + 1, this.High * (TileBuilding.Tiles.TileSize - 1) + 1];
			bool[,] array5 = new bool[this.Wide * (TileBuilding.Tiles.TileSize - 1) + 1, this.High * (TileBuilding.Tiles.TileSize - 1) + 1];
			for (int j = 0; j <= array4.GetUpperBound(0); j++)
			{
				for (int k = 0; k <= array4.GetUpperBound(1); k++)
				{
					array4[j, k] = '.';
					array5[j, k] = false;
				}
			}
			Z.ClearBox(new Box(this.XCorner, this.YCorner, this.XCorner + this.Wide * (TileBuilding.Tiles.TileSize - 1) + 1, this.YCorner + this.High * (TileBuilding.Tiles.TileSize - 1)), null);
			int[,] array6 = new int[this.Wide, this.High];
			for (int l = 0; l < this.Wide; l++)
			{
				for (int m = 0; m < this.High; m++)
				{
					array6[l, m] = -1;
				}
			}
			List<Point> list = new List<Point>();
			for (int n = 0; n < this.Wide; n++)
			{
				for (int num = 0; num < this.High; num++)
				{
					list.Add(new Point(n, num));
				}
			}
			for (int num2 = 0; num2 < this.NumberOfVaults; num2++)
			{
				int num3 = (TileBuilding.Tiles.Markers[this.VaultEndMarker] - TileBuilding.Tiles.Markers[this.VaultStartMarker]) / (this.VaultWidth * this.VaultHeight);
				if (Stat.Random(1, 100) <= this.ChancePerVault)
				{
					int num4 = 0;
					IL_03EB:
					while (num4 < 10)
					{
						int num5 = Stat.Random(0, this.Wide - this.VaultWidth - 1);
						int num6 = Stat.Random(0, this.High - this.VaultHeight - 1);
						for (int num7 = 0; num7 < this.VaultWidth; num7++)
						{
							for (int num8 = 0; num8 < this.VaultHeight; num8++)
							{
								if (array6[num5 + num7, num6 + num8] != -1)
								{
									num4++;
									goto IL_03EB;
								}
							}
						}
						int num9 = Stat.Random(0, num3 - 1);
						int num10 = TileBuilding.Tiles.Markers[this.VaultStartMarker] + num9 * (this.VaultWidth * this.VaultHeight);
						for (int num11 = 0; num11 < this.VaultHeight; num11++)
						{
							for (int num12 = 0; num12 < this.VaultWidth; num12++)
							{
								array6[num5 + num12, num6 + num11] = num10;
								num10++;
							}
						}
						for (int num13 = num5 * (TileBuilding.Tiles.TileSize - 1); num13 < (num5 + this.VaultWidth) * (TileBuilding.Tiles.TileSize - 1); num13++)
						{
							for (int num14 = num6 * (TileBuilding.Tiles.TileSize - 1); num14 < (num6 + this.VaultHeight) * (TileBuilding.Tiles.TileSize - 1); num14++)
							{
								array5[num13, num14] = true;
							}
						}
						break;
					}
				}
			}
			for (int num15 = 0; num15 < this.NumberOfVaults2; num15++)
			{
				int num16 = (TileBuilding.Tiles.Markers[this.VaultEndMarker2] - TileBuilding.Tiles.Markers[this.VaultStartMarker2]) / (this.VaultWidth2 * this.VaultHeight2);
				if (Stat.Random(1, 100) <= this.ChancePerVault2)
				{
					int num17 = 0;
					IL_05C2:
					while (num17 < 10)
					{
						int num18 = Stat.Random(0, this.Wide - this.VaultWidth2 - 1);
						int num19 = Stat.Random(0, this.High - this.VaultHeight2 - 1);
						for (int num20 = 0; num20 < this.VaultWidth2; num20++)
						{
							for (int num21 = 0; num21 < this.VaultHeight2; num21++)
							{
								if (array6[num18 + num20, num19 + num21] != -1)
								{
									num17++;
									goto IL_05C2;
								}
							}
						}
						int num22 = Stat.Random(0, num16 - 1);
						int num23 = TileBuilding.Tiles.Markers[this.VaultStartMarker2] + num22 * (this.VaultWidth2 * this.VaultHeight2);
						for (int num24 = 0; num24 < this.VaultHeight2; num24++)
						{
							for (int num25 = 0; num25 < this.VaultWidth2; num25++)
							{
								array6[num18 + num25, num19 + num24] = num23;
								num23++;
							}
						}
						for (int num26 = num18 * (TileBuilding.Tiles.TileSize - 1); num26 <= (num18 + this.VaultWidth2) * (TileBuilding.Tiles.TileSize - 1); num26++)
						{
							for (int num27 = num19 * (TileBuilding.Tiles.TileSize - 1); num27 <= (num19 + this.VaultHeight2) * (TileBuilding.Tiles.TileSize - 1); num27++)
							{
								array5[num26, num27] = true;
							}
						}
						break;
					}
				}
			}
			new List<Point>(Algorithms.RandomShuffle<Point>(list, Stat.Rand));
			foreach (Point point in list)
			{
				int x = point.X;
				int y = point.Y;
				if (array6[x, y] == -1)
				{
					int num28 = 500;
					while (num28 > 0)
					{
						int num29 = Stat.Random(TileBuilding.Tiles.Markers[this.TileStartMarker], TileBuilding.Tiles.Markers[this.TileEndMarker] - 1);
						array6[x, y] = num29;
						if (x == 0)
						{
							for (int num30 = 0; num30 < TileBuilding.Tiles.TileSize; num30++)
							{
								if (TileBuilding.Tiles.Tiles[num29].Tile[0, num30] != TileType.Wall)
								{
									goto IL_09E9;
								}
							}
							goto IL_0740;
						}
						if (x > 0 && array6[x - 1, y] != -1)
						{
							for (int num31 = 0; num31 < TileBuilding.Tiles.TileSize; num31++)
							{
								if (!BuildingTile.Matches(TileBuilding.Tiles.Tiles[array6[x - 1, y]].Tile[TileBuilding.Tiles.TileSize - 1, num31], TileBuilding.Tiles.Tiles[num29].Tile[0, num31]))
								{
									goto IL_09E9;
								}
							}
							goto IL_0740;
						}
						goto IL_0740;
						IL_09E9:
						num28--;
						continue;
						IL_0740:
						if (x >= this.Wide - 1)
						{
							for (int num32 = 0; num32 < TileBuilding.Tiles.TileSize; num32++)
							{
								if (TileBuilding.Tiles.Tiles[num29].Tile[TileBuilding.Tiles.TileSize - 1, num32] != TileType.Wall)
								{
									goto IL_09E9;
								}
							}
						}
						else if (x < this.Wide - 1 && array6[x + 1, y] != -1)
						{
							for (int num33 = 0; num33 < TileBuilding.Tiles.TileSize; num33++)
							{
								if (!BuildingTile.Matches(TileBuilding.Tiles.Tiles[array6[x + 1, y]].Tile[0, num33], TileBuilding.Tiles.Tiles[num29].Tile[TileBuilding.Tiles.TileSize - 1, num33]))
								{
									goto IL_09E9;
								}
							}
						}
						if (y == 0)
						{
							for (int num34 = 0; num34 < TileBuilding.Tiles.TileSize; num34++)
							{
								if (TileBuilding.Tiles.Tiles[num29].Tile[num34, 0] != TileType.Wall)
								{
									goto IL_09E9;
								}
							}
						}
						else if (y > 0 && array6[x, y - 1] != -1)
						{
							for (int num35 = 0; num35 < TileBuilding.Tiles.TileSize; num35++)
							{
								if (!BuildingTile.Matches(TileBuilding.Tiles.Tiles[array6[x, y - 1]].Tile[num35, TileBuilding.Tiles.TileSize - 1], TileBuilding.Tiles.Tiles[num29].Tile[num35, 0]))
								{
									goto IL_09E9;
								}
							}
						}
						if (y >= this.High - 1)
						{
							for (int num36 = 0; num36 < TileBuilding.Tiles.TileSize; num36++)
							{
								if (TileBuilding.Tiles.Tiles[num29].Tile[num36, TileBuilding.Tiles.TileSize - 1] != TileType.Wall)
								{
									goto IL_09E9;
								}
							}
							break;
						}
						if (y < this.High - 1 && array6[x, y + 1] != -1)
						{
							for (int num37 = 0; num37 < TileBuilding.Tiles.TileSize; num37++)
							{
								if (!BuildingTile.Matches(TileBuilding.Tiles.Tiles[array6[x, y + 1]].Tile[num37, 0], TileBuilding.Tiles.Tiles[num29].Tile[num37, TileBuilding.Tiles.TileSize - 1]))
								{
									goto IL_09E9;
								}
							}
							break;
						}
						break;
					}
					if (num28 <= 0)
					{
						if (x > 0 && array6[x - 1, y] != -1)
						{
							TileBuilding.Tiles.Tiles[array6[x - 1, y]].Trace();
						}
						if (x < this.Wide - 1 && array6[x + 1, y] != -1)
						{
							TileBuilding.Tiles.Tiles[array6[x + 1, y]].Trace();
						}
						if (y > 0 && array6[x, y - 1] != -1)
						{
							TileBuilding.Tiles.Tiles[array6[x, y - 1]].Trace();
						}
						if (y < this.High - 1 && array6[x, y + 1] != -1)
						{
							TileBuilding.Tiles.Tiles[array6[x, y + 1]].Trace();
						}
					}
				}
			}
			for (int num38 = 0; num38 < this.Wide; num38++)
			{
				for (int num39 = 0; num39 < this.High; num39++)
				{
					int num40 = TileBuilding.Tiles.TileSize - 1;
					int num41 = TileBuilding.Tiles.TileSize - 1;
					if (num38 == this.Wide - 1)
					{
						num40 = TileBuilding.Tiles.TileSize;
					}
					if (num39 == this.High - 1)
					{
						num41 = TileBuilding.Tiles.TileSize;
					}
					for (int num42 = 0; num42 < num40; num42++)
					{
						for (int num43 = 0; num43 < num41; num43++)
						{
							List<TileType> list2 = new List<TileType>();
							list2.Add(TileBuilding.Tiles.Tiles[array6[num38, num39]].Tile[num42, num43]);
							int num44 = num38 * (TileBuilding.Tiles.TileSize - 1) + num42;
							int num45 = num39 * (TileBuilding.Tiles.TileSize - 1) + num43;
							if (TileBuilding.Tiles.Tiles[array6[num38, num39]].CustomTile[num42, num43] != '?')
							{
								array4[num44, num45] = TileBuilding.Tiles.Tiles[array6[num38, num39]].CustomTile[num42, num43];
							}
							if (num44 >= 0 && num44 < Z.Width && num45 > 0 && num45 < Z.Height)
							{
								if (num42 == 0 && num38 > 0)
								{
									list2.Add(TileBuilding.Tiles.Tiles[array6[num38 - 1, num39]].Tile[TileBuilding.Tiles.TileSize - 1, num43]);
									if (TileBuilding.Tiles.Tiles[array6[num38 - 1, num39]].CustomTile[TileBuilding.Tiles.TileSize - 1, num43] != '?')
									{
										array4[num44, num45] = TileBuilding.Tiles.Tiles[array6[num38 - 1, num39]].CustomTile[TileBuilding.Tiles.TileSize - 1, num43];
									}
								}
								if (num43 == 0 && num39 > 0)
								{
									list2.Add(TileBuilding.Tiles.Tiles[array6[num38, num39 - 1]].Tile[num42, TileBuilding.Tiles.TileSize - 1]);
									if (TileBuilding.Tiles.Tiles[array6[num38, num39 - 1]].CustomTile[num42, TileBuilding.Tiles.TileSize - 1] != '?')
									{
										array4[num44, num45] = TileBuilding.Tiles.Tiles[array6[num38, num39 - 1]].CustomTile[num42, TileBuilding.Tiles.TileSize - 1];
									}
								}
								if (num42 == 0 && num43 == 0 && num38 > 0 && num39 > 0)
								{
									list2.Add(TileBuilding.Tiles.Tiles[array6[num38 - 1, num39 - 1]].Tile[TileBuilding.Tiles.TileSize - 1, TileBuilding.Tiles.TileSize - 1]);
									if (TileBuilding.Tiles.Tiles[array6[num38 - 1, num39 - 1]].CustomTile[TileBuilding.Tiles.TileSize - 1, TileBuilding.Tiles.TileSize - 1] != '?')
									{
										array4[num44, num45] = TileBuilding.Tiles.Tiles[array6[num38 - 1, num39 - 1]].CustomTile[TileBuilding.Tiles.TileSize - 1, TileBuilding.Tiles.TileSize - 1];
									}
								}
								if (list2.CleanContains(TileType.OpenConnect))
								{
									Z.GetCell(num44 + this.XCorner, num45 + this.YCorner).AddObject("ConnectWidget", null, null, null, null);
								}
								array3[num44, num45] = TileType.Open;
								if (list2.CleanContains(TileType.Door) && !list2.CleanContains(TileType.Wall))
								{
									array3[num44, num45] = TileType.Door;
								}
								else if (list2.CleanContains(TileType.Wall))
								{
									array3[num44, num45] = TileType.Wall;
								}
								if (list2.CleanContains(TileType.Garbage))
								{
									array3[num44, num45] = TileType.Garbage;
								}
								if (list2.CleanContains(TileType.LittleChest))
								{
									array3[num44, num45] = TileType.LittleChest;
								}
								if (list2.CleanContains(TileType.BigChest))
								{
									array3[num44, num45] = TileType.BigChest;
								}
								if (list2.CleanContains(TileType.Creature1))
								{
									array3[num44, num45] = TileType.Creature1;
								}
								if (list2.CleanContains(TileType.Liquid))
								{
									array3[num44, num45] = TileType.Liquid;
								}
							}
						}
					}
				}
			}
			bool flag = true;
			while (flag)
			{
				flag = false;
				for (int num46 = 0; num46 < array3.GetUpperBound(0) + 1; num46++)
				{
					for (int num47 = 0; num47 < array3.GetUpperBound(1) + 1; num47++)
					{
						if (num46 == 0 || num46 == array3.GetUpperBound(0) || num47 == -1 || num47 == array3.GetUpperBound(1))
						{
							array3[num46, num47] = TileType.Wall;
						}
						else if (array3[num46, num47] == TileType.Door)
						{
							int num48 = 0;
							if (array3[num46 - 1, num47] == TileType.Door)
							{
								num48++;
							}
							if (array3[num46 + 1, num47] == TileType.Door)
							{
								num48++;
							}
							if (array3[num46, num47 + 1] == TileType.Door)
							{
								num48++;
							}
							if (array3[num46, num47 + 1] == TileType.Door)
							{
								num48++;
							}
							int num49 = 0;
							if (array3[num46 - 1, num47] == TileType.Open)
							{
								num49++;
							}
							if (array3[num46 + 1, num47] == TileType.Open)
							{
								num49++;
							}
							if (array3[num46, num47 + 1] == TileType.Open)
							{
								num49++;
							}
							if (array3[num46, num47 + 1] == TileType.Open)
							{
								num49++;
							}
							if (num49 != 2)
							{
								flag = true;
								array3[num46, num47] = TileType.Wall;
							}
						}
					}
				}
			}
			for (int num50 = 0; num50 < array3.GetUpperBound(0) + 1; num50++)
			{
				for (int num51 = 0; num51 < array3.GetUpperBound(1) + 1; num51++)
				{
					if (this.CustomChar.ContainsKey(array4[num50, num51]))
					{
						if (this.CustomChar[array4[num50, num51]] == "(clear)")
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).ClearWalls();
						}
						else
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject(this.CustomChar[array4[num50, num51]], null, null, null, null);
						}
					}
					else
					{
						if (array3[num50, num51] == TileType.Wall)
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject(this.WallMaterial, null, null, null, null);
						}
						if (array3[num50, num51] == TileType.Door)
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("Door", null, null, null, null);
						}
						if (array3[num50, num51] == TileType.LittleChest)
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("Locker2", null, null, null, null);
						}
						if (array3[num50, num51] == TileType.BigChest)
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("MedLocker", null, null, null, null);
						}
						if (array3[num50, num51] == TileType.Creature1)
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("Boosterbot", null, null, null, null);
						}
						if (array3[num50, num51] == TileType.Liquid)
						{
							if (num50 == 0 || num50 >= array3.GetUpperBound(0) || num51 == 0 || num51 >= array3.GetUpperBound(1))
							{
								Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("CyanMarbleWalkway", null, null, null, null);
							}
							else
							{
								int num52 = 0;
								for (int num53 = -1; num53 <= 1; num53++)
								{
									for (int num54 = -1; num54 <= 1; num54++)
									{
										if (array3[num50 + num53, num51 + num54] == TileType.Liquid)
										{
											num52++;
										}
									}
								}
								if (num52 == 9)
								{
									Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("ConvalessencePuddle", null, null, null, null);
								}
								else
								{
									Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("CyanMarbleWalkway", null, null, null, null);
								}
							}
						}
						if (array3[num50, num51] == TileType.Garbage)
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddTableObject("MedScrap");
						}
						if (array5[num50, num51])
						{
							Z.GetCell(num50 + this.XCorner, num51 + this.YCorner).AddObject("SpawnBlocker", null, null, null, null);
						}
					}
				}
			}
			if (this.Shell)
			{
				Z.FillHollowBox(new Box(this.XCorner - 1, this.YCorner, Math.Min(Z.Width - 1, this.XCorner + this.Wide * (TileBuilding.Tiles.TileSize - 1) + 1), Math.Min(Z.Height - 1, this.YCorner + this.High * (TileBuilding.Tiles.TileSize - 1) + 1)), this.WallMaterial);
				Z.FillHollowBox(new Box(this.XCorner - 1, this.YCorner - 1, Math.Min(Z.Width - 1, this.XCorner + this.Wide * (TileBuilding.Tiles.TileSize - 1) + 1), Math.Min(Z.Height - 1, this.YCorner + this.High * (TileBuilding.Tiles.TileSize - 1) + 1)), this.ShellMaterial);
			}
			foreach (CachedZoneConnection cachedZoneConnection in Z.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-")
				{
					Z.GetCell(cachedZoneConnection.X, cachedZoneConnection.Y).Clear(null, false, false, null);
				}
			}
			new ForceConnections().BuildZone(Z);
			new Connecter().BuildZone(Z);
			return true;
		}

		// Token: 0x040033F5 RID: 13301
		[ModSensitiveStaticCache(false)]
		private static TileManager Tiles;

		// Token: 0x040033F6 RID: 13302
		public string WallMaterial = "Fulcrete";

		// Token: 0x040033F7 RID: 13303
		public string ShellMaterial = "Fulcrete";

		// Token: 0x040033F8 RID: 13304
		public bool Shell;

		// Token: 0x040033F9 RID: 13305
		public int Wide = 13;

		// Token: 0x040033FA RID: 13306
		public int High = 4;

		// Token: 0x040033FB RID: 13307
		public int XCorner;

		// Token: 0x040033FC RID: 13308
		public int YCorner;

		// Token: 0x040033FD RID: 13309
		public string TileStartMarker;

		// Token: 0x040033FE RID: 13310
		public string TileEndMarker;

		// Token: 0x040033FF RID: 13311
		public int NumberOfVaults;

		// Token: 0x04003400 RID: 13312
		public int ChancePerVault;

		// Token: 0x04003401 RID: 13313
		public int VaultWidth;

		// Token: 0x04003402 RID: 13314
		public int VaultHeight;

		// Token: 0x04003403 RID: 13315
		public string VaultStartMarker;

		// Token: 0x04003404 RID: 13316
		public string VaultEndMarker;

		// Token: 0x04003405 RID: 13317
		public int NumberOfVaults2;

		// Token: 0x04003406 RID: 13318
		public int ChancePerVault2;

		// Token: 0x04003407 RID: 13319
		public int VaultWidth2;

		// Token: 0x04003408 RID: 13320
		public int VaultHeight2;

		// Token: 0x04003409 RID: 13321
		public string VaultStartMarker2;

		// Token: 0x0400340A RID: 13322
		public string VaultEndMarker2;

		// Token: 0x0400340B RID: 13323
		public string CustomCharMap;

		// Token: 0x0400340C RID: 13324
		public Dictionary<char, string> CustomChar;
	}
}
