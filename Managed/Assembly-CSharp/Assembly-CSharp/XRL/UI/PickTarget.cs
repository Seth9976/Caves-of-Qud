using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Genkit;
using Qud.UI;
using UnityEngine;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x020014A9 RID: 5289
	[UIView("PickTarget", true, false, false, "Targeting", "PickTargetFrame", false, 1, false)]
	public class PickTarget : IWantsTextConsoleInit
	{
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600CFA5 RID: 53157 RVA: 0x004C3D70 File Offset: 0x004C1F70
		// (set) Token: 0x0600CFA6 RID: 53158 RVA: 0x004C3D77 File Offset: 0x004C1F77
		public static bool bLocked
		{
			get
			{
				return Options.PickTargetLocked;
			}
			set
			{
				Options.PickTargetLocked = value;
			}
		}

		// Token: 0x0600CFA7 RID: 53159 RVA: 0x004C3D7F File Offset: 0x004C1F7F
		public void Init(TextConsole TextConsole_, ScreenBuffer ScreenBuffer_)
		{
			PickTarget._TextConsole = TextConsole_;
			PickTarget._ScreenBuffer = ScreenBuffer_;
		}

		// Token: 0x0600CFA8 RID: 53160 RVA: 0x004C3D90 File Offset: 0x004C1F90
		public static XRL.World.GameObject GetCombatObjectAt(int X, int Y, Zone Z)
		{
			if (X < 0)
			{
				return null;
			}
			if (X > Z.Width - 1)
			{
				return null;
			}
			if (Y < 0)
			{
				return null;
			}
			if (Y > Z.Height - 1)
			{
				return null;
			}
			Cell cell = Z.GetCell(X, Y);
			if (!cell.IsExplored() || !cell.IsLit())
			{
				return null;
			}
			return cell.GetFirstObjectWithPart("Combat");
		}

		// Token: 0x0600CFA9 RID: 53161 RVA: 0x004C3DE9 File Offset: 0x004C1FE9
		public static void GetObjectListCone(int StartX, int StartY, List<XRL.World.GameObject> ObjectList, string Direction, Predicate<XRL.World.GameObject> ObjectTest = null, Predicate<XRL.World.GameObject> ExtraVisibility = null)
		{
			Look.GetObjectListCone(StartX, StartY, ObjectList, Direction, ObjectTest, ExtraVisibility);
		}

		// Token: 0x0600CFAA RID: 53162 RVA: 0x004C3DF8 File Offset: 0x004C1FF8
		public static List<Cell> ShowFieldPicker(int Range, int Size, int StartX, int StartY, string What = "Wall", bool StartAdjacent = false, bool ReturnNullForAbort = false, bool AllowDiagonals = false, bool AllowDiagonalStart = true, bool RequireVisibility = false)
		{
			PickTargetWindow.currentMode = PickTargetWindow.TargetMode.PickField;
			GameManager.Instance.PushGameView("PickTarget", true);
			PickTarget.OldBuffer.Copy(TextConsole.CurrentBuffer);
			PickTarget.Buffer.Copy(TextConsole.CurrentBuffer);
			List<Cell> list = new List<Cell>();
			XRL.World.Parts.Physics physics = The.Player.Physics;
			int num = Range;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (physics != null)
			{
				Cell currentCell = physics.CurrentCell;
				int num2 = StartX;
				int num3 = StartY;
				int num4 = num2;
				int num5 = num3;
				while (!flag3)
				{
					XRL.World.Event.ResetPool(false);
					XRLCore.Core.RenderMapToBuffer(PickTarget.Buffer);
					XRLCore.ParticleManager.Frame();
					XRLCore.ParticleManager.Render(PickTarget.Buffer);
					string text = string.Concat(new string[]
					{
						(flag || !StartAdjacent || num2 != StartX || num3 != StartY) ? string.Concat(new string[]
						{
							"{{W|space}} {{W|5}}-",
							flag ? "End " : "Start",
							" ",
							What,
							" {{C|",
							num.ToString(),
							"}}"
						}) : (num.ToString() ?? ""),
						" ",
						(num == 1) ? "square" : "squares",
						" left  {{W|Escape}}-",
						flag ? "Clear" : "Cancel"
					});
					if (num2 != num4 || num3 != num5)
					{
						num4 = num2;
						num5 = num3;
					}
					if (Options.ModernUI)
					{
						PickTargetWindow.currentText = text;
					}
					else
					{
						if (num2 < 40)
						{
							PickTarget.Buffer.Goto(79 - ConsoleLib.Console.ColorUtility.LengthExceptFormatting(text), 0);
						}
						else
						{
							PickTarget.Buffer.Goto(1, 0);
						}
						PickTarget.Buffer.Write(text, true, false, false, null, -1);
					}
					foreach (Cell cell in list)
					{
						PickTarget.Buffer.Goto(cell.X, cell.Y);
						if (Options.UseImposters)
						{
							PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerSelected", 0, null);
						}
						else
						{
							PickTarget.Buffer.Write("{{G|#}}", true, false, false, null, -1);
						}
					}
					PickTarget.Buffer.Goto(num2, num3);
					if (Options.UseImposters)
					{
						PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerBright", 0, null);
						PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
					}
					else if (XRLCore.CurrentFrame % 32 < 16)
					{
						PickTarget.Buffer.Write("{{W|X}}", true, false, false, null, -1);
					}
					if (!flag5)
					{
						PickTarget.Buffer.focusPosition = new Point2D(num2, num3);
					}
					PickTarget._TextConsole.DrawBuffer(PickTarget.Buffer, null, false);
					if (Keyboard.kbhit())
					{
						Keys keys = Keyboard.getvk(true, false, true);
						if (keys == Keys.Escape)
						{
							if (flag)
							{
								flag = false;
								list = new List<Cell>();
								num = Range;
								if (StartAdjacent)
								{
									num2 = StartX;
									num3 = StartY;
								}
							}
							else
							{
								flag3 = true;
								if (ReturnNullForAbort)
								{
									flag4 = true;
								}
							}
						}
						if (keys == Keys.U)
						{
							PickTarget.bLocked = false;
						}
						if (keys == Keys.L)
						{
							PickTarget.bLocked = true;
						}
						int num6 = num2;
						int num7 = num3;
						if (keys == Keys.MouseEvent)
						{
							Keyboard.MouseEvent currentMouseEvent = Keyboard.CurrentMouseEvent;
							if (currentMouseEvent.Event == "PointerOver")
							{
								flag5 = true;
								if (flag2)
								{
									flag2 = false;
								}
								else
								{
									num2 = currentMouseEvent.x;
									num3 = currentMouseEvent.y;
								}
							}
							else if (currentMouseEvent.Event == "RightClick")
							{
								flag3 = true;
							}
						}
						if (keys >= Keys.NumPad1 && keys <= Keys.NumPad9)
						{
							flag5 = false;
						}
						if (keys == Keys.NumPad1)
						{
							num2--;
							num3++;
						}
						if (keys == Keys.NumPad2)
						{
							num3++;
						}
						if (keys == Keys.NumPad3)
						{
							num2++;
							num3++;
						}
						if (keys == Keys.NumPad4)
						{
							num2--;
						}
						if (keys == Keys.NumPad6)
						{
							num2++;
						}
						if (keys == Keys.NumPad7)
						{
							num2--;
							num3--;
						}
						if (keys == Keys.NumPad8)
						{
							num3--;
						}
						if (keys == Keys.NumPad9)
						{
							num2++;
							num3--;
						}
						if (num2 < 0)
						{
							num2 = 0;
						}
						if (num2 >= currentCell.ParentZone.Width)
						{
							num2 = currentCell.ParentZone.Width - 1;
						}
						if (num3 < 0)
						{
							num3 = 0;
						}
						if (num3 >= currentCell.ParentZone.Height)
						{
							num3 = currentCell.ParentZone.Height - 1;
						}
						if (flag)
						{
							if ((!AllowDiagonals && num2 != num6 && num3 != num7) || Math.Abs(num6 - num2) > 1 || Math.Abs(num7 - num3) > 1)
							{
								num2 = num6;
								num3 = num7;
							}
						}
						else if (StartAdjacent)
						{
							if (num2 > StartX + 1)
							{
								num2 = StartX + 1;
							}
							else if (num2 < StartX - 1)
							{
								num2 = StartX - 1;
							}
							if (num3 > StartY + 1)
							{
								num3 = StartY + 1;
							}
							else if (num3 < StartY - 1)
							{
								num3 = StartY - 1;
							}
							if (!AllowDiagonalStart && num2 != StartX && num3 != StartY)
							{
								if (num6 == StartX)
								{
									num2 = num6;
								}
								else if (num7 == StartY)
								{
									num3 = num7;
								}
								else
								{
									num2 = num6;
									num3 = num7;
								}
							}
						}
						if (RequireVisibility && (num2 != num6 || num3 != num7) && !currentCell.ParentZone.GetCell(num2, num3).IsVisible())
						{
							num2 = num6;
							num3 = num7;
						}
						if (keys == Keys.NumPad5 || keys == Keys.F || keys == Keys.Space || keys == Keys.Enter || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftClick") || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdFire") || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTargetSelf"))
						{
							if (!flag)
							{
								if (!StartAdjacent || num2 != StartX || num3 != StartY)
								{
									Cell cell2 = currentCell.ParentZone.GetCell(num2, num3);
									list.Add(cell2);
									num--;
									flag = true;
								}
							}
							else
							{
								flag3 = true;
							}
						}
						if ((num2 != num6 || num3 != num7) && flag)
						{
							Cell cell3 = currentCell.ParentZone.GetCell(num2, num3);
							if (list.CleanContains(cell3))
							{
								if (list.Count > 1 && cell3 == list[list.Count - 2])
								{
									list.Remove(currentCell.ParentZone.GetCell(num6, num7));
									num++;
								}
								else
								{
									num2 = num6;
									num3 = num7;
								}
							}
							else if (num <= 0)
							{
								num2 = num6;
								num3 = num7;
							}
							else
							{
								list.Add(currentCell.ParentZone.GetCell(num2, num3));
								num--;
							}
						}
					}
				}
			}
			GameManager.Instance.PopGameView(true);
			PickTarget._TextConsole.DrawBuffer(PickTarget.OldBuffer, null, false);
			if (ReturnNullForAbort && flag4 && list.Count == 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x0600CFAB RID: 53163 RVA: 0x004C44BC File Offset: 0x004C26BC
		public static Cell ShowPicker(PickTarget.PickStyle Style = PickTarget.PickStyle.Burst, int Radius = 0, int Range = 9999, int StartX = 40, int StartY = 12, bool Locked = true, AllowVis VisLevel = AllowVis.OnlyVisible, Predicate<XRL.World.GameObject> ExtraVisibility = null, Predicate<XRL.World.GameObject> ObjectTest = null, XRL.World.GameObject UsePassability = null, Point2D? Origin = null, string Label = null, bool EnforceRange = false, bool UseTarget = true)
		{
			PickTargetWindow.currentMode = PickTargetWindow.TargetMode.PickCells;
			GameManager.Instance.PushGameView("PickTarget", true);
			bool flag = Locked;
			PickTarget.OldBuffer.Copy(TextConsole.CurrentBuffer);
			PickTarget.Buffer.Copy(TextConsole.CurrentBuffer);
			XRL.World.GameObject player = The.Player;
			Cell cell = ((player != null) ? player.GetCurrentCell() : null);
			Cell cell2 = null;
			if (cell != null)
			{
				bool flag2 = false;
				Point2D point2D = Origin ?? cell.Pos2D;
				if (UseTarget && Sidebar.CurrentTarget != null && !The.Player.IsConfused && (ObjectTest == null || ObjectTest(Sidebar.CurrentTarget)))
				{
					Cell currentCell = Sidebar.CurrentTarget.CurrentCell;
					if (currentCell != null && The.Player.InSameZone(currentCell))
					{
						StartX = currentCell.X;
						StartY = currentCell.Y;
					}
				}
				int num = StartX;
				int num2 = StartY;
				bool flag3 = true;
				List<Location2D> list = new List<Location2D>();
				List<Point> list2 = new List<Point>(8);
				bool flag4 = true;
				int num3 = num;
				int num4 = num2;
				while (!flag2)
				{
					XRL.World.Event.ResetStringbuilderPool();
					XRL.World.Event.ResetGameObjectListPool();
					XRLCore.Core.RenderMapToBuffer(PickTarget.Buffer);
					XRLCore.ParticleManager.Frame();
					XRLCore.ParticleManager.Render(PickTarget.Buffer);
					Zone.Line(point2D.x, point2D.y, num, num2, list2);
					if (num != num3 || num2 != num4)
					{
						num3 = num;
						num4 = num2;
					}
					if (Style == PickTarget.PickStyle.EmptyCell)
					{
						if (list2.Count == 0)
						{
							PickTarget.Buffer.Goto(num, num2);
							if (Options.UseImposters)
							{
								PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerBright", 0, null);
								PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
							}
							else
							{
								PickTarget.Buffer.Write("{{W|X}}", true, false, false, null, -1);
							}
						}
						else
						{
							PickTarget.Buffer.Goto(num, num2);
							int num5 = Math.Max(Math.Abs(point2D.x - num), Math.Abs(point2D.y - num2));
							Cell cell3 = cell.ParentZone.GetCell(num, num2);
							if (num5 < list2.Count - 1 || XRLCore.CurrentFrame % 32 < 16 || Options.UseImposters)
							{
								if (num5 > Range)
								{
									if (Options.UseImposters)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerInvalid", 0, null);
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
									}
									else
									{
										PickTarget.Buffer.Write("&K" + list2[num5].DisplayChar.ToString(), true, false, false, null, -1);
									}
								}
								else if (((!cell3.IsVisible() || !cell3.IsLit()) && (ExtraVisibility == null || !cell3.HasObject(ExtraVisibility))) || The.Player.IsConfused)
								{
									if (Options.UseImposters)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerBright", 0, null);
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
									}
									else
									{
										PickTarget.Buffer.Write("&Y" + list2[num5].DisplayChar.ToString(), true, false, false, null, -1);
									}
								}
								else if (((cell3.IsVisible() && cell3.IsLit()) || (ExtraVisibility != null && cell3.HasObject(ExtraVisibility))) && (cell3.IsEmpty() || (UsePassability != null && cell3.IsPassable(UsePassability, true))))
								{
									if (Options.UseImposters)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerSelected", 0, null);
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
									}
									else
									{
										PickTarget.Buffer.Write("X", true, false, false, null, -1);
									}
								}
								else if (Options.UseImposters)
								{
									PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerError", 0, null);
									PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
								}
								else
								{
									PickTarget.Buffer.Write("X", true, false, false, null, -1);
								}
							}
						}
					}
					else if (Style == PickTarget.PickStyle.Cone)
					{
						XRL.Rules.Geometry.GetCone(Location2D.Get(point2D.x, point2D.y), Location2D.Get(num, num2), Range, Radius, list);
						for (int i = 0; i < list.Count; i++)
						{
							PickTarget.Buffer.Goto(list[i].X, list[i].Y);
							Cell cell4 = cell.ParentZone.GetCell(list[i].X, list[i].Y);
							if (i < list.Count)
							{
								if (list[i].Distance(Location2D.Get(StartX, StartY)) > Range)
								{
									if (Options.UseImposters)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerInvalid", 0, null);
									}
									else
									{
										PickTarget.Buffer.Write("&KX", true, false, false, null, -1);
									}
								}
								else if ((!cell4.IsVisible() || !cell4.IsLit()) && (ExtraVisibility == null || !cell4.HasObject(ExtraVisibility)))
								{
									if (Options.UseImposters)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerInvalid", 0, null);
									}
									else
									{
										PickTarget.Buffer.Write("&KX", true, false, false, null, -1);
									}
								}
								else if (((cell4.IsVisible() && cell4.IsLit()) || (ExtraVisibility != null && cell4.HasObject(ExtraVisibility))) && cell4.HasVisibleCombatObject(false) && !The.Player.IsConfused)
								{
									if (Options.UseImposters)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerError", 0, null);
									}
									else
									{
										ConsoleChar currentChar = PickTarget.Buffer.CurrentChar;
										currentChar.Background = (currentChar.Detail = The.Color.Red);
										currentChar.Char = currentChar.BackupChar;
										currentChar._Tile = null;
									}
								}
								else if (Options.UseImposters)
								{
									PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerDim", 0, null);
								}
								else
								{
									string text = "&yX";
									PickTarget.Buffer.Write(text, true, false, false, null, -1);
								}
							}
						}
						if (Options.UseImposters)
						{
							PickTarget.Buffer.Goto(num, num2);
							PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerBright", 0, null);
							PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
						}
						else if (XRLCore.CurrentFrame % 32 < 16)
						{
							PickTarget.Buffer.Goto(num, num2);
							PickTarget.Buffer.Write("{{W|X}}", true, false, false, null, -1);
						}
					}
					else if (Style == PickTarget.PickStyle.Line)
					{
						if (list2.Count == 0)
						{
							PickTarget.Buffer.Goto(num, num2);
							PickTarget.Buffer.Write("{{W|X}}", true, false, false, null, -1);
						}
						else
						{
							bool isConfused = The.Player.IsConfused;
							int j = ((Origin == null) ? 1 : 0);
							int count = list2.Count;
							while (j < count)
							{
								PickTarget.Buffer.Goto(list2[j].X, list2[j].Y);
								Cell cell5 = cell.ParentZone.GetCell(list2[j].X, list2[j].Y);
								Color color = The.Color.Gray;
								Color color2 = The.Color.Black;
								bool flag5 = !isConfused && cell5.HasVisibleCombatObject(false);
								string text2;
								if (flag5)
								{
									if (XRLCore.CurrentFrameLong < 500)
									{
										text2 = "&R";
										color = The.Color.Red;
										color2 = The.Color.DarkRed;
									}
									else
									{
										text2 = "&r";
										color = The.Color.DarkRed;
										color2 = The.Color.Red;
									}
								}
								else if (j > Radius)
								{
									text2 = "&K";
									color = The.Color.Black;
									color2 = The.Color.Gray;
								}
								else if ((!cell5.IsVisible() || !cell5.IsLit()) && (ExtraVisibility == null || !cell5.HasObject(ExtraVisibility)))
								{
									text2 = "&K";
									color = The.Color.Black;
									color2 = The.Color.Gray;
								}
								else
								{
									text2 = "&W";
								}
								if (flag5 || j == count - 1)
								{
									ConsoleChar currentChar2 = PickTarget.Buffer.CurrentChar;
									if (currentChar2._Tile != null)
									{
										currentChar2._TileForeground = (currentChar2._Foreground = color);
										currentChar2._Detail = color2;
									}
									else
									{
										currentChar2.Background = color;
										currentChar2.Char = currentChar2.BackupChar;
									}
								}
								else
								{
									PickTarget.Buffer.Write(text2 + list2[j].DisplayChar.ToString(), true, false, false, null, -1);
								}
								j++;
							}
							PickTarget.Buffer.Buffer[list2.Last<Point>().X, list2.Last<Point>().Y].imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
						}
					}
					else if (Style == PickTarget.PickStyle.Circle)
					{
						int num6 = num - Radius;
						int num7 = num + Radius;
						int num8 = num2 - Radius;
						int num9 = num2 + Radius;
						cell.ParentZone.Constrain(ref num6, ref num8, ref num7, ref num9);
						for (int k = num8; k <= num9; k++)
						{
							for (int l = num6; l <= num7; l++)
							{
								if (Math.Sqrt((double)((l - num) * (l - num) + (k - num2) * (k - num2))) <= (double)Radius)
								{
									PickTarget.Buffer.Goto(l, k);
									if (l == num && k == num2)
									{
										if (Options.UseImposters)
										{
											PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerBright", 0, null);
											PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
										}
										else
										{
											PickTarget.Buffer.Write("{{W|X}}", true, false, false, null, -1);
										}
									}
									else if (Options.UseImposters)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerDim", 0, null);
									}
									else
									{
										PickTarget.Buffer.Write("{{w|X}}", true, false, false, null, -1);
									}
								}
							}
						}
					}
					else if (Style == PickTarget.PickStyle.Burst)
					{
						int num10 = num - Radius;
						int num11 = num + Radius;
						int num12 = num2 - Radius;
						int num13 = num2 + Radius;
						cell.ParentZone.Constrain(ref num10, ref num12, ref num11, ref num13);
						bool flag6 = false;
						string text3 = "&WX";
						string text4 = "&wX";
						if (point2D.ManhattanDistance(new Point2D(num, num2)) > Range)
						{
							flag6 = true;
							text3 = "&KX";
							text4 = "&KX";
						}
						for (int m = num12; m <= num13; m++)
						{
							for (int n = num10; n <= num11; n++)
							{
								PickTarget.Buffer.Goto(n, m);
								if (Options.UseImposters)
								{
									if (n == num && m == num2)
									{
										if (flag6)
										{
											PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerInvalid", 0, null);
										}
										else
										{
											PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerBright", 0, null);
										}
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
									}
									else if (flag6)
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerInvalid", 0, null);
									}
									else
									{
										PickTarget.Buffer.CurrentChar.imposterExtra.Add("Prefabs/Imposters/TargetPickerDim", 0, null);
									}
								}
								else if (n == num && m == num2)
								{
									PickTarget.Buffer.Write(text3, true, false, false, null, -1);
								}
								else
								{
									PickTarget.Buffer.Write(text4, true, false, false, null, -1);
								}
							}
						}
					}
					if (Options.ModernUI)
					{
						PickTargetWindow.currentText = string.Concat(new string[]
						{
							Label,
							" | {{W|",
							ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false),
							"}}-select",
							CapabilityManager.AllowKeyboardHotkeys ? (" | unlock (" + ControlManager.getCommandInputFormatted("CmdLockUnlock", true, false) + "))") : ""
						});
					}
					else
					{
						if (num < 40)
						{
							if (Label.IsNullOrEmpty())
							{
								PickTarget.Buffer.Goto(54, 0);
							}
							else
							{
								PickTarget.Buffer.Goto(52 - ConsoleLib.Console.ColorUtility.LengthExceptFormatting(Label), 0);
							}
						}
						else
						{
							PickTarget.Buffer.Goto(1, 0);
						}
						if (!Label.IsNullOrEmpty())
						{
							PickTarget.Buffer.Write(Label, true, false, false, null, -1);
							PickTarget.Buffer.Write("  ", true, false, false, null, -1);
						}
						if (flag)
						{
							PickTarget.Buffer.Write("{{W|" + ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false) + "}}-select" + (CapabilityManager.AllowKeyboardHotkeys ? (" | unlock (" + ControlManager.getCommandInputFormatted("CmdLockUnlock", true, false) + "))") : ""), true, false, false, null, -1);
						}
						else
						{
							PickTarget.Buffer.Write("{{W|" + ControlManager.getCommandInputDescription("Accept", true, false, ControlManager.InputDeviceType.Unknown, false) + "}}-select" + (CapabilityManager.AllowKeyboardHotkeys ? (" | lock (" + ControlManager.getCommandInputFormatted("CmdLockUnlock", true, false) + ")") : ""), true, false, false, null, -1);
						}
					}
					if (!flag4)
					{
						PickTarget.Buffer.focusPosition = new Point2D(num, num2);
					}
					PickTarget._TextConsole.DrawBuffer(PickTarget.Buffer, null, false);
					if (Keyboard.kbhit())
					{
						Keys keys = Keyboard.getvk(true, false, true);
						if (keys == Keys.MouseEvent)
						{
							if (Keyboard.CurrentMouseEvent.Event == "PointerOver" && !flag3)
							{
								flag4 = true;
								num = Keyboard.CurrentMouseEvent.x;
								num2 = Keyboard.CurrentMouseEvent.y;
							}
							if (Keyboard.CurrentMouseEvent.Event == "RightClick")
							{
								flag2 = true;
							}
							if (Keyboard.CurrentMouseEvent.Event == "PointerOver")
							{
								flag3 = false;
							}
						}
						if (keys == Keys.NumPad5 || keys == Keys.Escape || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTargetSelf"))
						{
							flag2 = true;
						}
						if (keys == Keys.F1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdLockUnlock"))
						{
							flag = !flag;
						}
						if (keys >= Keys.NumPad1 && keys <= Keys.NumPad9)
						{
							flag4 = false;
						}
						if (flag && !The.Player.IsConfused)
						{
							List<XRL.World.GameObject> list3 = new List<XRL.World.GameObject>();
							if (keys == Keys.NumPad1)
							{
								PickTarget.GetObjectListCone(num - 1, num2 + 1, list3, "sw", ObjectTest, ExtraVisibility);
							}
							if (keys == Keys.NumPad2)
							{
								PickTarget.GetObjectListCone(num, num2 + 1, list3, "s", ObjectTest, ExtraVisibility);
							}
							if (keys == Keys.NumPad3)
							{
								PickTarget.GetObjectListCone(num + 1, num2 + 1, list3, "se", ObjectTest, ExtraVisibility);
							}
							if (keys == Keys.NumPad4)
							{
								PickTarget.GetObjectListCone(num - 1, num2, list3, "w", ObjectTest, ExtraVisibility);
							}
							if (keys == Keys.NumPad6)
							{
								PickTarget.GetObjectListCone(num + 1, num2, list3, "e", ObjectTest, ExtraVisibility);
							}
							if (keys == Keys.NumPad7)
							{
								PickTarget.GetObjectListCone(num - 1, num2 - 1, list3, "nw", ObjectTest, ExtraVisibility);
							}
							if (keys == Keys.NumPad8)
							{
								PickTarget.GetObjectListCone(num, num2 - 1, list3, "n", ObjectTest, ExtraVisibility);
							}
							if (keys == Keys.NumPad9)
							{
								PickTarget.GetObjectListCone(num + 1, num2 - 1, list3, "ne", ObjectTest, ExtraVisibility);
							}
							bool flag7 = true;
							if (list3.Count > 0)
							{
								Cell currentCell2 = list3[0].CurrentCell;
								if (Math.Abs(currentCell2.X - point2D.x) <= Range && Math.Abs(currentCell2.Y - point2D.y) <= Range)
								{
									num = currentCell2.X;
									num2 = currentCell2.Y;
									flag7 = false;
								}
							}
							if (flag7)
							{
								if (keys == Keys.NumPad1)
								{
									num--;
									num2++;
								}
								if (keys == Keys.NumPad2)
								{
									num2++;
								}
								if (keys == Keys.NumPad3)
								{
									num++;
									num2++;
								}
								if (keys == Keys.NumPad4)
								{
									num--;
								}
								if (keys == Keys.NumPad6)
								{
									num++;
								}
								if (keys == Keys.NumPad7)
								{
									num--;
									num2--;
								}
								if (keys == Keys.NumPad8)
								{
									num2--;
								}
								if (keys == Keys.NumPad9)
								{
									num++;
									num2--;
								}
							}
						}
						else
						{
							if (keys == Keys.NumPad1)
							{
								num--;
								num2++;
							}
							if (keys == Keys.NumPad2)
							{
								num2++;
							}
							if (keys == Keys.NumPad3)
							{
								num++;
								num2++;
							}
							if (keys == Keys.NumPad4)
							{
								num--;
							}
							if (keys == Keys.NumPad6)
							{
								num++;
							}
							if (keys == Keys.NumPad7)
							{
								num--;
								num2--;
							}
							if (keys == Keys.NumPad8)
							{
								num2--;
							}
							if (keys == Keys.NumPad9)
							{
								num++;
								num2--;
							}
						}
						if (EnforceRange)
						{
							if (num < StartX - Range)
							{
								num = StartX - Range;
							}
							else if (num > StartX + Range)
							{
								num = StartX + Range;
							}
							if (num2 < StartY - Range)
							{
								num2 = StartY - Range;
							}
							else if (num2 > StartY + Range)
							{
								num2 = StartY + Range;
							}
						}
						if (keys == Keys.F || keys == Keys.Space || keys == Keys.Enter || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdFire") || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftClick"))
						{
							if (point2D.ManhattanDistance(new Point2D(num, num2)) > Range && Style == PickTarget.PickStyle.Burst)
							{
								Popup.ShowFail("You must select a location within " + Range.ToString() + " tiles!", true, true, true);
							}
							else if (VisLevel == AllowVis.OnlyVisible && !cell.ParentZone.GetCell(num, num2).IsVisible() && (ExtraVisibility == null || !cell.ParentZone.GetCell(num, num2).HasObject(ExtraVisibility)))
							{
								Popup.ShowFail("You may only select a visible square!", true, true, true);
							}
							else if (VisLevel == AllowVis.OnlyExplored && !cell.ParentZone.GetCell(num, num2).IsExplored())
							{
								Popup.ShowFail("You may only select an explored square!", true, true, true);
							}
							else
							{
								flag2 = true;
								cell2 = cell.ParentZone.GetCell(num, num2);
							}
						}
						cell.ParentZone.Constrain(ref num, ref num2);
					}
				}
			}
			GameManager.Instance.PopGameView(true);
			PickTarget._TextConsole.DrawBuffer(PickTarget.OldBuffer, null, false);
			return cell2;
		}

		// Token: 0x040050D4 RID: 20692
		public static TextConsole _TextConsole;

		// Token: 0x040050D5 RID: 20693
		public static ScreenBuffer _ScreenBuffer;

		// Token: 0x040050D6 RID: 20694
		public static ScreenBuffer OldBuffer = ScreenBuffer.create(80, 25);

		// Token: 0x040050D7 RID: 20695
		public static ScreenBuffer Buffer = ScreenBuffer.create(80, 25);

		// Token: 0x02001DBE RID: 7614
		public enum PickStyle
		{
			// Token: 0x040073EE RID: 29678
			Cone,
			// Token: 0x040073EF RID: 29679
			Line,
			// Token: 0x040073F0 RID: 29680
			Burst,
			// Token: 0x040073F1 RID: 29681
			Circle,
			// Token: 0x040073F2 RID: 29682
			EmptyCell
		}
	}
}
