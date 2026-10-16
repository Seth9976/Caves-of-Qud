using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using Genkit;
using Qud.UI;
using UnityEngine;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x0200111D RID: 4381
	[UIView("FireMissileWeapon", true, false, false, "Targeting", "PickTargetFrame", false, 1, false)]
	[Serializable]
	public class MissileWeapon : IPart
	{
		// Token: 0x0600AD56 RID: 44374 RVA: 0x003FA8BC File Offset: 0x003F8ABC
		public static List<Pair> ListOfVisitedSquares(int x1, int y1, int x2, int y2)
		{
			List<Pair> list = new List<Pair>(Math.Abs(x2 - x1) + Math.Abs(y2 - y1));
			int num = y1;
			int num2 = x1;
			int num3 = x2 - x1;
			int num4 = y2 - y1;
			list.Add(new Pair(x1, y1));
			int num5;
			if (num4 < 0)
			{
				num5 = -1;
				num4 = -num4;
			}
			else
			{
				num5 = 1;
			}
			int num6;
			if (num3 < 0)
			{
				num6 = -1;
				num3 = -num3;
			}
			else
			{
				num6 = 1;
			}
			int num7 = 2 * num4;
			int num8 = 2 * num3;
			if (num8 >= num7)
			{
				int num10;
				int num9 = (num10 = num3);
				for (int i = 0; i < num3; i++)
				{
					num2 += num6;
					num9 += num7;
					if (num9 > num8)
					{
						num += num5;
						num9 -= num8;
						if (num9 + num10 < num8)
						{
							list.Add(new Pair(num2, num - num5));
						}
						else if (num9 + num10 > num8)
						{
							list.Add(new Pair(num2 - num6, num));
						}
					}
					list.Add(new Pair(num2, num));
					num10 = num9;
				}
			}
			else
			{
				int num10;
				int num9 = (num10 = num4);
				for (int i = 0; i < num4; i++)
				{
					num += num5;
					num9 += num8;
					if (num9 > num7)
					{
						num2 += num6;
						num9 -= num7;
						if (num9 + num10 < num7)
						{
							list.Add(new Pair(num2 - num6, num));
						}
						else if (num9 + num10 > num7)
						{
							list.Add(new Pair(num2, num - num5));
						}
					}
					list.Add(new Pair(num2, num));
					num10 = num9;
				}
			}
			return list;
		}

		// Token: 0x0600AD57 RID: 44375 RVA: 0x003FAA2C File Offset: 0x003F8C2C
		public static void CalculateMissilePath(MissilePath Path, Zone Z, int X0, int Y0, int X1, int Y1, bool IncludeStart = false, bool IncludeCover = false, bool MapCalculated = false, GameObject Actor = null)
		{
			Path.Reset();
			Path.X0 = (float)(X0 * 3);
			Path.Y0 = (float)(Y0 * 3);
			Path.X1 = (float)(X1 * 3 + 1);
			Path.Y1 = (float)(Y1 * 3 + 1);
			Path.Angle = (float)Math.Atan2((double)(X1 - X0), (double)(Y1 - Y0));
			if (IncludeCover && Path.Cover == null)
			{
				Path.Cover = new List<float>();
			}
			bool flag = false;
			if (!MapCalculated && IncludeCover)
			{
				Z.CalculateMissileMap(Actor ?? The.Player);
			}
			if (IncludeStart)
			{
				Path.Path.Add(Z.GetCell(X0, Y0));
				if (IncludeCover)
				{
					Path.Cover.Add(Z.GetCoverAt(X0, Y0));
				}
			}
			if (X0 == X1 && Y0 == Y1)
			{
				Path.Path.Add(Z.GetCell(X0, Y0));
				if (IncludeCover)
				{
					Path.Cover.Add(Z.GetCoverAt(X0, Y0));
				}
			}
			else
			{
				bool flag2 = Math.Abs(Y1 - Y0) > Math.Abs(X1 - X0);
				if (flag2)
				{
					int num = X0;
					X0 = Y0;
					Y0 = num;
					int num2 = X1;
					X1 = Y1;
					Y1 = num2;
				}
				if (X0 > X1)
				{
					flag = true;
					int num3 = X1;
					X1 = X0;
					X0 = num3;
					int num4 = Y1;
					Y1 = Y0;
					Y0 = num4;
				}
				double num5 = (double)(X1 - X0);
				double num6 = (double)Math.Abs(Y1 - Y0);
				double num7 = 0.0;
				double num8 = num6 / num5;
				int num9 = Y0;
				int num10;
				if (Y0 < Y1)
				{
					num10 = 1;
				}
				else
				{
					num10 = -1;
				}
				int num11 = 0;
				for (int i = X0; i <= X1; i++)
				{
					num11++;
					Cell cell = (flag2 ? Z.GetCell(num9, i) : Z.GetCell(i, num9));
					Path.Path.Add(cell);
					if (IncludeCover)
					{
						float cover = cell.GetCover();
						Path.Cover.Add(cover);
					}
					num7 += num8;
					if (num7 >= 0.5)
					{
						num9 += num10;
						num7 -= 1.0;
					}
				}
			}
			if (flag)
			{
				Path.Path.Reverse();
				List<float> cover2 = Path.Cover;
				if (cover2 != null)
				{
					cover2.Reverse();
				}
			}
			if (IncludeCover)
			{
				float num12 = 0f;
				int j = 0;
				int count = Path.Cover.Count;
				while (j < count)
				{
					num12 += Path.Cover[j];
					Path.Cover[j] = num12;
					j++;
				}
			}
		}

		// Token: 0x0600AD58 RID: 44376 RVA: 0x003FAC80 File Offset: 0x003F8E80
		public static MissilePath CalculateMissilePath(Zone Z, int X0, int Y0, int X1, int Y1, bool IncludeStart = false, bool IncludeCover = true, bool MapCalculated = false, GameObject Actor = null)
		{
			MissilePath missilePath = new MissilePath();
			MissileWeapon.CalculateMissilePath(missilePath, Z, X0, Y0, X1, Y1, IncludeStart, IncludeCover, MapCalculated, Actor);
			return missilePath;
		}

		// Token: 0x0600AD59 RID: 44377 RVA: 0x003FACA6 File Offset: 0x003F8EA6
		public static void GetObjectListCone(int StartX, int StartY, List<GameObject> ObjectList, string Direction)
		{
			Look.GetObjectListCone(StartX, StartY, ObjectList, Direction, null, null);
		}

		// Token: 0x0600AD5A RID: 44378 RVA: 0x003FACB4 File Offset: 0x003F8EB4
		public static string GetRoundCooldown(int nCooldown)
		{
			int num = Math.Max((int)Math.Ceiling((double)nCooldown / 10.0), 1);
			if (num == 1)
			{
				return "({{C|1}} turn)";
			}
			return "({{C|" + num.ToString() + "}} turns)";
		}

		// Token: 0x0600AD5B RID: 44379 RVA: 0x003FACFC File Offset: 0x003F8EFC
		public static MissilePath ShowPicker(int StartX, int StartY, bool Snap, AllowVis VisLevel, int Range, bool BowOrRifle, GameObject Projectile, ref FireType FireType, int MidRange = -1)
		{
			PickTargetWindow.currentMode = PickTargetWindow.TargetMode.PickCells;
			GameManager.Instance.PushGameView("FireMissileWeapon", true);
			GameObject gameObject = null;
			if (Snap && !The.Player.IsConfused)
			{
				gameObject = Sidebar.CurrentTarget ?? The.Player.GetNearestVisibleObject(true, "Combat", 80, true, false, null);
			}
			TextConsole textConsole = Popup._TextConsole;
			TextConsole.LoadScrapBuffers();
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			bool flag = false;
			bool flag2 = true;
			if (gameObject != null)
			{
				Cell currentCell = gameObject.CurrentCell;
				if (currentCell != null && The.Player.InSameZone(currentCell))
				{
					StartX = currentCell.X;
					StartY = currentCell.Y;
				}
			}
			Cell currentCell2 = The.Player.CurrentCell;
			if (currentCell2 != null)
			{
				MissileWeapon.PlayerMissilePath.Reset();
				currentCell2.ParentZone.CalculateMissileMap(The.Player);
				int num = StartX;
				int num2 = StartY;
				bool flag3 = true;
				while (!flag)
				{
					Event.ResetStringbuilderPool();
					Event.ResetGameObjectListPool();
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					bool flag11 = false;
					The.Core.RenderMapToBuffer(scrapBuffer);
					List<Point> list = Zone.Line(currentCell2.X, currentCell2.Y, num, num2, false);
					MissileWeapon.CalculateMissilePath(MissileWeapon.PlayerMissilePath, currentCell2.ParentZone, currentCell2.X, currentCell2.Y, num, num2, false, true, true, The.Player);
					scrapBuffer.Goto(0, 2);
					Cell cell = currentCell2.ParentZone.GetCell(num, num2);
					if (!flag3)
					{
						scrapBuffer.focusPosition = cell.Pos2D;
					}
					if (list.Count == 0)
					{
						scrapBuffer.Goto(num, num2);
						scrapBuffer.Buffer[num, num2].imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
					}
					else
					{
						bool isConfused = The.Player.IsConfused;
						int i = 1;
						int count = list.Count;
						while (i < count)
						{
							scrapBuffer.Goto(list[i].X, list[i].Y);
							Cell cell2 = currentCell2.ParentZone.GetCell(list[i].X, list[i].Y);
							Color color = The.Color.Gray;
							Color color2 = The.Color.Black;
							bool flag12 = !isConfused && cell2.HasVisibleCombatObject(false);
							string text;
							if (flag12)
							{
								if (XRLCore.CurrentFrameLong < 500)
								{
									text = "&R";
									color = The.Color.Red;
									color2 = The.Color.DarkRed;
								}
								else
								{
									text = "&r";
									color = The.Color.DarkRed;
									color2 = The.Color.Red;
								}
							}
							else if (i > Range)
							{
								text = "&K";
								color = The.Color.Black;
								color2 = The.Color.Gray;
							}
							else if (MidRange >= 0 && i > MidRange)
							{
								text = "&W";
								color = The.Color.Yellow;
								color2 = The.Color.Brown;
							}
							else if (!cell2.IsVisible() || !cell2.IsLit())
							{
								text = "&K";
								color = The.Color.Black;
								color2 = The.Color.Gray;
							}
							else
							{
								float num3 = (isConfused ? 0f : ((MissileWeapon.PlayerMissilePath.Cover == null) ? 0f : ((i >= MissileWeapon.PlayerMissilePath.Cover.Count) ? MissileWeapon.PlayerMissilePath.Cover.Last<float>() : MissileWeapon.PlayerMissilePath.Cover[i])));
								if (num3 >= 1f)
								{
									text = "&R";
									color = The.Color.Red;
									color2 = The.Color.DarkRed;
								}
								else if ((double)num3 >= 0.8)
								{
									text = "&r";
									color = The.Color.DarkRed;
									color2 = The.Color.Red;
								}
								else if ((double)num3 >= 0.5)
								{
									text = "&w";
									color = The.Color.Brown;
									color2 = The.Color.Yellow;
								}
								else if ((double)num3 >= 0.2)
								{
									text = "&g";
									color = The.Color.DarkGreen;
									color2 = The.Color.Green;
								}
								else
								{
									text = "&G";
									color = The.Color.Green;
									color2 = The.Color.DarkGreen;
								}
							}
							if (flag12 || i == count - 1)
							{
								ConsoleChar currentChar = scrapBuffer.CurrentChar;
								if (currentChar._Tile != null)
								{
									currentChar._TileForeground = (currentChar._Foreground = color);
									currentChar._Detail = color2;
								}
								else
								{
									currentChar.Background = color;
									currentChar.Char = currentChar.BackupChar;
								}
							}
							else
							{
								scrapBuffer.Write(text + list[i].DisplayChar.ToString(), true, false, false, null, -1);
							}
							i++;
						}
						scrapBuffer.Buffer[list.Last<Point>().X, list.Last<Point>().Y].imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
					}
					int num4;
					if (num < 40)
					{
						num4 = 43;
					}
					else
					{
						num4 = 1;
					}
					scrapBuffer.Goto(num4, 0);
					string text2;
					if (MissileWeapon.LockActive)
					{
						text2 = string.Concat(new string[]
						{
							"{{W|",
							ControlManager.getCommandInputFormatted("Accept", Options.ModernUI, false),
							"}}-select | unlock (",
							ControlManager.getCommandInputFormatted("CmdLockUnlock", Options.ModernUI, false),
							")"
						});
					}
					else
					{
						text2 = string.Concat(new string[]
						{
							"{{W|",
							ControlManager.getCommandInputFormatted("Accept", Options.ModernUI, false),
							"}}-select | lock (",
							ControlManager.getCommandInputFormatted("CmdLockUnlock", Options.ModernUI, false),
							")"
						});
					}
					if (The.Player.IsConfused)
					{
						BowOrRifle = false;
					}
					bool flag13 = false;
					GameObject combatTarget = cell.GetCombatTarget(The.Player, true, false, false, 5, Projectile, null, null, null, null, false, false, null);
					int num5 = 1;
					MissileWeapon.MenuOptions.Clear();
					if (BowOrRifle && The.Player.HasSkill("Rifle_DrawABead"))
					{
						Rifle_DrawABead part = The.Player.GetPart<Rifle_DrawABead>();
						RifleMark rifleMark = ((combatTarget != null) ? combatTarget.GetEffect<RifleMark>() : null);
						scrapBuffer.Goto(num4, num5);
						num5++;
						if (rifleMark != null && rifleMark.Marker.IsPlayer())
						{
							if (Options.ModernUI)
							{
								text2 = "{{G|marked target}} " + text2;
							}
							else
							{
								scrapBuffer.Write("{{G|marked target}}", true, false, false, null, -1);
							}
							flag13 = true;
						}
						else if (part != null)
						{
							flag4 = true;
							if (Options.ModernUI)
							{
								text2 = text2 + " " + ControlManager.getCommandInputFormatted("CmdMarkTarget", Options.ModernUI, false) + " - mark target";
							}
							else
							{
								scrapBuffer.Write(" " + ControlManager.getCommandInputFormatted("CmdMarkTarget", Options.ModernUI, false) + " - mark target", true, false, false, null, -1);
							}
							MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("mark target", "MarkTarget"));
						}
					}
					if (BowOrRifle && The.Player.HasSkill("Rifle_DrawABead") && combatTarget != null)
					{
						int num6 = 0;
						if (The.Player.HasSkill("Rifle_SuppressiveFire"))
						{
							scrapBuffer.Goto(num4, num5);
							num5++;
							if (The.Player.HasSkill("Rifle_FlatteningFire"))
							{
								flag6 = Rifle_FlatteningFire.MeetsCriteria(combatTarget);
							}
							if (!flag13)
							{
								if (flag6)
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{K|" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Flattening Fire (not marked)}}", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{K||" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Flattening Fire (not marked)}}";
									}
								}
								else if (!Options.ModernUI)
								{
									scrapBuffer.Write("{{K|" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Suppressive Fire (not marked)}}", true, false, false, null, -1);
								}
								else
								{
									text2 = text2 + " {{K|" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Suppressive Fire (not marked)}}";
								}
							}
							else if (num6 <= 0)
							{
								flag5 = true;
								if (flag6)
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - {{W|Flattening Fire}}", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{W|" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - {{W|Flattening Fire}}";
									}
									MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("Flattening Fire", "SupressiveFire"));
								}
								else
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Suppressive Fire", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + "{{W|" + ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Suppressive Fire";
									}
									MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("Suppressive Fire", "SupressiveFire"));
								}
							}
							else if (!Options.ModernUI)
							{
								scrapBuffer.Write(string.Concat(new string[]
								{
									"{{K|",
									ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Suppressive Fire ",
									MissileWeapon.GetRoundCooldown(num6),
									"}}"
								}), true, false, false, null, -1);
							}
							else
							{
								text2 = string.Concat(new string[]
								{
									text2,
									" {{K|",
									ControlManager.getCommandInputDescription("CmdAltFire1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Suppressive Fire ",
									MissileWeapon.GetRoundCooldown(num6),
									"}}"
								});
							}
						}
						if (The.Player.HasSkill("Rifle_WoundingFire"))
						{
							scrapBuffer.Goto(num4, num5);
							num5++;
							if (The.Player.HasSkill("Rifle_DisorientingFire"))
							{
								flag8 = Rifle_DisorientingFire.MeetsCriteria(combatTarget);
							}
							if (!flag13)
							{
								if (flag8)
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{K|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Disorienting Fire (not marked)}}", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{K|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Disorienting Fire (not marked)}}";
									}
								}
								else if (!Options.ModernUI)
								{
									scrapBuffer.Write("{{K|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Wounding Fire (not marked)}}", true, false, false, null, -1);
								}
								else
								{
									text2 = text2 + " {{K|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Wounding Fire (not marked)}}";
								}
							}
							else if (num6 <= 0)
							{
								flag7 = true;
								if (flag8)
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - {{W|Disorienting Fire}}", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{W|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - {{W|Disorienting Fire}}";
									}
									MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("Disorienting Fire", "WoundingFire"));
								}
								else
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Wounding Fire", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{W|" + ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Wounding Fire";
									}
									MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("Wounding Fire", "WoundingFire"));
								}
							}
							else if (!Options.ModernUI)
							{
								scrapBuffer.Write(string.Concat(new string[]
								{
									"{{K|",
									ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Wounding Fire ",
									MissileWeapon.GetRoundCooldown(num6),
									"}}"
								}), true, false, false, null, -1);
							}
							else
							{
								text2 = string.Concat(new string[]
								{
									text2,
									" {{K|",
									ControlManager.getCommandInputDescription("CmdAltFire2", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Wounding Fire ",
									MissileWeapon.GetRoundCooldown(num6),
									"}}"
								});
							}
						}
						if (The.Player.HasSkill("Rifle_SureFire"))
						{
							scrapBuffer.Goto(num4, num5);
							num5++;
							if (The.Player.HasSkill("Rifle_BeaconFire"))
							{
								flag10 = Rifle_BeaconFire.MeetsCriteria(combatTarget);
							}
							if (!flag13)
							{
								if (flag10)
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{K|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Beacon Fire (not marked)}}", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{K|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Beacon Fire (not marked)}}";
									}
								}
								else if (!Options.ModernUI)
								{
									scrapBuffer.Write("{{K|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Sure Fire (not marked)}}", true, false, false, null, -1);
								}
								else
								{
									text2 = text2 + " {{K|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Sure Fire (not marked)}}";
								}
							}
							else if (num6 <= 0)
							{
								flag9 = true;
								if (The.Player.HasSkill("Rifle_BeaconFire"))
								{
									flag10 = Rifle_BeaconFire.MeetsCriteria(combatTarget);
								}
								if (flag10)
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - {{W|Beacon Fire}}", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{W|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - {{W|Beacon Fire}}";
									}
									MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("Beacon Fire", "SureFire"));
								}
								else
								{
									if (!Options.ModernUI)
									{
										scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Sure Fire", true, false, false, null, -1);
									}
									else
									{
										text2 = text2 + " {{W|" + ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Sure Fire";
									}
									MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("Sure Fire", "SureFire"));
								}
							}
							else if (!Options.ModernUI)
							{
								scrapBuffer.Write(string.Concat(new string[]
								{
									"{{K|",
									ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Sure Fire ",
									MissileWeapon.GetRoundCooldown(num6),
									"}}"
								}), true, false, false, null, -1);
							}
							else
							{
								text2 = string.Concat(new string[]
								{
									text2,
									" {{K|",
									ControlManager.getCommandInputDescription("CmdAltFire3", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Sure Fire ",
									MissileWeapon.GetRoundCooldown(num6),
									"}}"
								});
							}
						}
						if (The.Player.HasSkill("Rifle_OneShot"))
						{
							scrapBuffer.Goto(num4, num5);
							num5++;
							Rifle_OneShot part2 = The.Player.GetPart<Rifle_OneShot>();
							if (!flag13)
							{
								if (!Options.ModernUI)
								{
									scrapBuffer.Write("{{K|" + ControlManager.getCommandInputDescription("CmdAltFire4", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Ultra Fire (not marked)}}", true, false, false, null, -1);
								}
								else
								{
									text2 = text2 + " {{K|" + ControlManager.getCommandInputDescription("CmdAltFire4", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " - Ultra Fire (not marked)}}";
								}
							}
							else if (part2.Cooldown <= 0)
							{
								flag11 = true;
								if (!Options.ModernUI)
								{
									scrapBuffer.Write("{{W|" + ControlManager.getCommandInputDescription("CmdAltFire4", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Ultra Fire", true, false, false, null, -1);
								}
								else
								{
									text2 = text2 + " {{W|" + ControlManager.getCommandInputDescription("CmdAltFire4", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} - Ultra Fire";
								}
								MissileWeapon.MenuOptions.Add(new ValueTuple<string, string>("Ultra Fire", "UltraFire"));
							}
							else if (!Options.ModernUI)
							{
								scrapBuffer.Write(string.Concat(new string[]
								{
									"{{K|",
									ControlManager.getCommandInputDescription("CmdAltFire4", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Ultra Fire ",
									MissileWeapon.GetRoundCooldown(part2.Cooldown),
									"}}"
								}), true, false, false, null, -1);
							}
							else
							{
								text2 = string.Concat(new string[]
								{
									text2,
									" {{K|",
									ControlManager.getCommandInputDescription("CmdAltFire4", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false),
									" - Ultra Fire ",
									MissileWeapon.GetRoundCooldown(part2.Cooldown),
									"}}"
								});
							}
						}
					}
					if (MissileWeapon.MenuOptions.Count > 0)
					{
						scrapBuffer.Goto(num4, num5);
						if (Options.ModernUI)
						{
							text2 = text2 + " [{{W|" + ControlManager.getCommandInputDescription("CmdMissileWeaponMenu", true, false, ControlManager.InputDeviceType.Unknown, false) + "}}] Menu";
						}
						else
						{
							scrapBuffer.Write("[{{W|" + ControlManager.getCommandInputDescription("CmdMissileWeaponMenu", true, false, ControlManager.InputDeviceType.Unknown, false) + "}}] Menu", true, false, false, null, -1);
						}
					}
					if (Options.ModernUI)
					{
						PickTargetWindow.currentText = (text2.IsNullOrEmpty() ? "" : (text2 + " | ")) + "Fire Missile Weapon";
					}
					else if (!text2.IsNullOrEmpty())
					{
						scrapBuffer.WriteAt(Math.Max(80 - ConsoleLib.Console.ColorUtility.LengthExceptFormatting(text2), 0), 0, text2, true);
					}
					if (!flag3)
					{
						scrapBuffer.focusPosition = new Point2D(num, num2);
					}
					textConsole.DrawBuffer(scrapBuffer, null, false);
					if (Keyboard.kbhit())
					{
						Keys keys = Keyboard.getvk(true, false, true);
						string text3 = null;
						if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdMissileWeaponMenu")
						{
							int num7 = Popup.PickOption("Select Fire Mode", null, "", "Sounds/UI/ui_notification", MissileWeapon.MenuOptions.Select<ValueTuple<string, string>, string>((ValueTuple<string, string> m) => m.Item1).ToArray<string>(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
							if (num7 >= 0)
							{
								text3 = MissileWeapon.MenuOptions[num7].Item2;
							}
							else
							{
								keys = Keys.None;
							}
						}
						MissileWeapon.MenuOptions.Clear();
						if (text3 == "MarkTarget")
						{
							keys = Keys.M;
						}
						if (text3 == "SupressiveFire")
						{
							keys = Keys.D1;
						}
						if (text3 == "WoundingFire")
						{
							keys = Keys.D2;
						}
						if (text3 == "SureFire")
						{
							keys = Keys.D3;
						}
						if (text3 == "UltraFire")
						{
							keys = Keys.D4;
						}
						if (keys == Keys.MouseEvent)
						{
							if (Keyboard.CurrentMouseEvent.Event == "PointerOver" && !flag2)
							{
								num = Keyboard.CurrentMouseEvent.x;
								num2 = Keyboard.CurrentMouseEvent.y;
								flag3 = true;
							}
							if (Keyboard.CurrentMouseEvent.Event == "PointerOver")
							{
								flag2 = false;
							}
						}
						if (keys >= Keys.NumPad1 && keys <= Keys.NumPad9)
						{
							flag3 = false;
						}
						if (keys == Keys.NumPad5 || keys == Keys.Escape || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick"))
						{
							GameManager.Instance.PopGameView(false);
							return null;
						}
						if (keys == Keys.U || keys == Keys.L || keys == Keys.F1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdLockUnlock"))
						{
							MissileWeapon.LockActive = !MissileWeapon.LockActive;
						}
						if (MissileWeapon.LockActive)
						{
							MissileWeapon.LockObjectList.Clear();
							if (!The.Player.IsConfused)
							{
								if (keys == Keys.NumPad1)
								{
									MissileWeapon.GetObjectListCone(num - 1, num2 + 1, MissileWeapon.LockObjectList, "sw");
								}
								if (keys == Keys.NumPad2)
								{
									MissileWeapon.GetObjectListCone(num, num2 + 1, MissileWeapon.LockObjectList, "s");
								}
								if (keys == Keys.NumPad3)
								{
									MissileWeapon.GetObjectListCone(num + 1, num2 + 1, MissileWeapon.LockObjectList, "se");
								}
								if (keys == Keys.NumPad4)
								{
									MissileWeapon.GetObjectListCone(num - 1, num2, MissileWeapon.LockObjectList, "w");
								}
								if (keys == Keys.NumPad6)
								{
									MissileWeapon.GetObjectListCone(num + 1, num2, MissileWeapon.LockObjectList, "e");
								}
								if (keys == Keys.NumPad7)
								{
									MissileWeapon.GetObjectListCone(num - 1, num2 - 1, MissileWeapon.LockObjectList, "nw");
								}
								if (keys == Keys.NumPad8)
								{
									MissileWeapon.GetObjectListCone(num, num2 - 1, MissileWeapon.LockObjectList, "n");
								}
								if (keys == Keys.NumPad9)
								{
									MissileWeapon.GetObjectListCone(num + 1, num2 - 1, MissileWeapon.LockObjectList, "ne");
								}
							}
							if (MissileWeapon.LockObjectList.Count > 0)
							{
								Cell currentCell3 = MissileWeapon.LockObjectList[0].CurrentCell;
								MissileWeapon.LockObjectList.Clear();
								if (Math.Abs(currentCell3.X - currentCell2.X) <= Range && Math.Abs(currentCell3.Y - currentCell2.Y) <= Range)
								{
									num = currentCell3.X;
									num2 = currentCell3.Y;
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
						if ((keys == Keys.Oemplus || keys == Keys.M || keys == Keys.Add || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdMarkTarget")) && flag4)
						{
							FireType = FireType.Mark;
							GameManager.Instance.PopGameView(false);
							return MissileWeapon.PlayerMissilePath;
						}
						if (flag5 && (keys == Keys.Oem1 || keys == Keys.D1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdAltFire1")))
						{
							if (VisLevel == AllowVis.OnlyVisible && !currentCell2.ParentZone.GetCell(num, num2).IsVisible())
							{
								Popup.ShowFail("You may only select a visible square!", true, true, true);
							}
							else
							{
								if (VisLevel != AllowVis.OnlyExplored || currentCell2.ParentZone.GetCell(num, num2).IsExplored())
								{
									The.Player.GetPart<Rifle_DrawABead>().ClearMark();
									FireType = FireType.SuppressingFire;
									if (flag6)
									{
										FireType = FireType.FlatteningFire;
									}
									GameManager.Instance.PopGameView(false);
									return MissileWeapon.PlayerMissilePath;
								}
								Popup.ShowFail("You may only select an explored square!", true, true, true);
							}
						}
						if (flag7 && (keys == Keys.OemQuestion || keys == Keys.D2 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdAltFire2")))
						{
							if (VisLevel == AllowVis.OnlyVisible && !currentCell2.ParentZone.GetCell(num, num2).IsVisible())
							{
								Popup.ShowFail("You may only select a visible square!", true, true, true);
							}
							else
							{
								if (VisLevel != AllowVis.OnlyExplored || currentCell2.ParentZone.GetCell(num, num2).IsExplored())
								{
									The.Player.GetPart<Rifle_DrawABead>().ClearMark();
									FireType = FireType.WoundingFire;
									if (flag8)
									{
										FireType = FireType.DisorientingFire;
									}
									GameManager.Instance.PopGameView(false);
									return MissileWeapon.PlayerMissilePath;
								}
								Popup.ShowFail("You may only select an explored square!", true, true, true);
							}
						}
						if (flag9 && (keys == Keys.Oemtilde || keys == Keys.D3 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdAltFire3")))
						{
							if (VisLevel == AllowVis.OnlyVisible && !currentCell2.ParentZone.GetCell(num, num2).IsVisible())
							{
								Popup.ShowFail("You may only select a visible square!", true, true, true);
							}
							else
							{
								if (VisLevel != AllowVis.OnlyExplored || currentCell2.ParentZone.GetCell(num, num2).IsExplored())
								{
									The.Player.GetPart<Rifle_DrawABead>().ClearMark();
									FireType = FireType.SureFire;
									if (flag10)
									{
										FireType = FireType.BeaconFire;
									}
									GameManager.Instance.PopGameView(false);
									return MissileWeapon.PlayerMissilePath;
								}
								Popup.ShowFail("You may only select an explored square!", true, true, true);
							}
						}
						if (flag11 && (keys == Keys.Oem4 || keys == Keys.D4 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdAltFire4")))
						{
							if (VisLevel == AllowVis.OnlyVisible && !currentCell2.ParentZone.GetCell(num, num2).IsVisible())
							{
								Popup.ShowFail("You may only select a visible square!", true, true, true);
							}
							else
							{
								if (VisLevel != AllowVis.OnlyExplored || currentCell2.ParentZone.GetCell(num, num2).IsExplored())
								{
									Rifle_OneShot part3 = The.Player.GetPart<Rifle_OneShot>();
									Event @event = Event.New("BeforeCooldownActivatedAbility", "AbilityEntry", null, "Turns", 1010, "Tags", "Agility");
									if (The.Player.FireEvent(@event) && @event.GetIntParameter("Turns", 0) != 0 && !The.Core.cool)
									{
										int num8 = 0;
										if (The.Player.HasStat("Willpower"))
										{
											num8 = Math.Min(80, (The.Player.Stat("Willpower", 0) - 16) * 5);
										}
										part3.Cooldown = 1000 * (100 - num8) / 100 + 10;
									}
									FireType = FireType.OneShot;
									The.Player.GetPart<Rifle_DrawABead>().ClearMark();
									GameManager.Instance.PopGameView(false);
									return MissileWeapon.PlayerMissilePath;
								}
								Popup.ShowFail("You may only select an explored square!", true, true, true);
							}
						}
						if (keys == Keys.F || keys == Keys.Space || keys == Keys.Enter || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdFire") || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "LeftClick"))
						{
							if (VisLevel == AllowVis.OnlyVisible && !currentCell2.ParentZone.GetCell(num, num2).IsVisible())
							{
								Popup.ShowFail("You may only select a visible square!", true, true, true);
							}
							else
							{
								if (VisLevel != AllowVis.OnlyExplored || currentCell2.ParentZone.GetCell(num, num2).IsExplored())
								{
									GameManager.Instance.PopGameView(false);
									return MissileWeapon.PlayerMissilePath;
								}
								Popup.ShowFail("You may only select an explored square!", true, true, true);
							}
						}
						if (num < 0)
						{
							num = 0;
						}
						if (num >= currentCell2.ParentZone.Width)
						{
							num = currentCell2.ParentZone.Width - 1;
						}
						if (num2 < 0)
						{
							num2 = 0;
						}
						if (num2 >= currentCell2.ParentZone.Height)
						{
							num2 = currentCell2.ParentZone.Height - 1;
						}
					}
				}
			}
			GameManager.Instance.PopGameView(false);
			return null;
		}

		// Token: 0x0600AD5C RID: 44380 RVA: 0x003FC844 File Offset: 0x003FAA44
		public void CheckHeavyWeaponMovementPenalty(GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = this.ParentObject.Equipped ?? this.ParentObject.Implantee;
				if (Subject == null)
				{
					return;
				}
			}
			Hampered effect = Subject.GetEffect<Hampered>();
			if (effect != null)
			{
				effect.CheckApplicable(true);
				return;
			}
			if (Hampered.Applicable(Subject))
			{
				Subject.ForceApplyEffect(new Hampered(), null);
			}
		}

		// Token: 0x0600AD5D RID: 44381 RVA: 0x003FC89C File Offset: 0x003FAA9C
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && CheckLoadAmmoEvent.Check(this.ParentObject, E.Actor, true))
			{
				Cell cell;
				if ((cell = this.ParentObject.CurrentCell) == null)
				{
					GameObject actor = E.Actor;
					cell = ((actor != null) ? actor.CurrentCell : null);
				}
				Cell cell2 = cell;
				Cell cell3 = ((cell2 != null) ? cell2.GetRandomLocalAdjacentCell(4, false) : null);
				if (cell3 != null)
				{
					Event @event = Event.New("CommandFireMissile", 0, 0, 0);
					@event.SetParameter("Actor", E.Actor);
					@event.SetParameter("StartCell", cell2);
					@event.SetParameter("TargetCell", cell3);
					@event.SetFlag("IncludeStart", E.Actor.CurrentCell == cell2 && Chance.in100());
					@event.SetFlag("ShowEmitMessage", true);
					@event.SetFlag("ActivePartsIgnoreSubject", true);
					@event.SetFlag("UsePopups", true);
					if (this.FireEvent(@event))
					{
						E.Identify = true;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600AD5E RID: 44382 RVA: 0x003FC9B8 File Offset: 0x003FABB8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterAddSkillEvent.ID || ID == PooledEvent<AfterRemoveSkillEvent>.ID || ID == EquippedEvent.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == PooledEvent<GenericQueryEvent>.ID || (ID == AdjustTotalWeightEvent.ID && this.Skill == "HeavyWeapons") || (ID == SingletonEvent<GetEnergyCostEvent>.ID && this.Skill == "HeavyWeapons") || ID == GetShortDescriptionEvent.ID || ID == QueryEquippableListEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600AD5F RID: 44383 RVA: 0x003FCA4D File Offset: 0x003FAC4D
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600AD60 RID: 44384 RVA: 0x003FCA63 File Offset: 0x003FAC63
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600AD61 RID: 44385 RVA: 0x003FCA79 File Offset: 0x003FAC79
		public override bool HandleEvent(AfterAddSkillEvent E)
		{
			if (E.Skill.Name == "HeavyWeapons_Tank")
			{
				this.CheckHeavyWeaponMovementPenalty(null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD62 RID: 44386 RVA: 0x003FCAA0 File Offset: 0x003FACA0
		public override bool HandleEvent(AfterRemoveSkillEvent E)
		{
			if (E.Skill.Name == "HeavyWeapons_Tank")
			{
				this.CheckHeavyWeaponMovementPenalty(null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD63 RID: 44387 RVA: 0x003FCAC7 File Offset: 0x003FACC7
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckHeavyWeaponMovementPenalty(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD64 RID: 44388 RVA: 0x003FCADC File Offset: 0x003FACDC
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.CheckHeavyWeaponMovementPenalty(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD65 RID: 44389 RVA: 0x003FCAF1 File Offset: 0x003FACF1
		public override bool HandleEvent(GenericQueryEvent E)
		{
			if (E.Query == "PhaseHarmonicEligible" && ModPhaseHarmonic.IsProjectileCompatible(GetProjectileBlueprintEvent.GetFor(this.ParentObject)))
			{
				E.Result = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD66 RID: 44390 RVA: 0x003FCB28 File Offset: 0x003FAD28
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.Item == this.ParentObject && !E.List.Contains(E.Item) && this.ValidSlotType(E.SlotType, false))
			{
				if (!E.RequirePossible || E.SlotType == "Floating Nearby")
				{
					E.List.Add(E.Item);
				}
				else
				{
					string usesSlots = E.Item.UsesSlots;
					if (!usesSlots.IsNullOrEmpty() && (E.SlotType != "Thrown Weapon" || usesSlots.Contains("Thrown Weapon")) && (E.SlotType != "Hand" || usesSlots.Contains("Hand")))
					{
						if (E.Actor.IsGiganticCreature)
						{
							if (E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
							{
								E.List.Add(E.Item);
							}
						}
						else if (E.SlotType == "Hand" || E.SlotType == "Missile Weapon" || !E.Item.IsGiganticEquipment || !E.Item.IsNatural())
						{
							E.List.Add(E.Item);
						}
					}
					else if (!E.Actor.IsGiganticCreature || E.Item.IsGiganticEquipment || E.Item.HasPropertyOrTag("GiganticEquippable") || E.Item.IsNatural())
					{
						int slotsRequiredFor = E.Item.GetSlotsRequiredFor(E.Actor, this.SlotType, false);
						if (slotsRequiredFor > 0 && slotsRequiredFor <= E.Actor.GetBodyPartCount(E.SlotType))
						{
							E.List.Add(E.Item);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD67 RID: 44391 RVA: 0x003FCD21 File Offset: 0x003FAF21
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Append(this.GetDetailedStats());
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD68 RID: 44392 RVA: 0x003FCD3C File Offset: 0x003FAF3C
		public override bool HandleEvent(AdjustTotalWeightEvent E)
		{
			if (this.Skill == "HeavyWeapons")
			{
				GameObject gameObject = this.ParentObject.Equipped ?? this.ParentObject.InInventory;
				if (gameObject != null && gameObject.HasSkill("HeavyWeapons_StrappingShoulders"))
				{
					E.AdjustWeight(0.5);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD69 RID: 44393 RVA: 0x003FCD9C File Offset: 0x003FAF9C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AD6A RID: 44394 RVA: 0x003FCD9F File Offset: 0x003FAF9F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandFireMissile");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD6B RID: 44395 RVA: 0x003FCDB4 File Offset: 0x003FAFB4
		public string GetDetailedStats()
		{
			if (The.Player == null)
			{
				return "";
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("{{rules|");
			if (this.Skill == "Pistol")
			{
				stringBuilder.Append("\nWeapon Class: Pistol");
			}
			else if (this.Skill == "Rifle")
			{
				stringBuilder.Append("\nWeapon Class: Bows && Rifles");
			}
			else if (this.Skill == "HeavyWeapons")
			{
				stringBuilder.Append("\nWeapon Class: Heavy Weapon");
			}
			else if (!this.Skill.IsNullOrEmpty())
			{
				stringBuilder.Append("\nWeapon Class: ").Append(this.Skill);
			}
			if (this.WeaponAccuracy <= 0)
			{
				stringBuilder.Append("\nAccuracy: Very High");
			}
			else if (this.WeaponAccuracy < 5)
			{
				stringBuilder.Append("\nAccuracy: High");
			}
			else if (this.WeaponAccuracy < 10)
			{
				stringBuilder.Append("\nAccuracy: Medium");
			}
			else if (this.WeaponAccuracy < 25)
			{
				stringBuilder.Append("\nAccuracy: Low");
			}
			else
			{
				stringBuilder.Append("\nAccuracy: Very Low");
			}
			if (this.AmmoPerAction > 1)
			{
				stringBuilder.Append("\nMultiple ammo used per shot: " + this.AmmoPerAction.ToString());
			}
			if (this.ShotsPerAction > 1 && this.ShowShotsPerAction)
			{
				stringBuilder.Append("\nMultiple projectiles per shot: " + this.ShotsPerAction.ToString());
			}
			if (this.NoWildfire)
			{
				stringBuilder.Append("\nSpray fire: This item can be fired while adjacent to multiple enemies without risk of the shot going wild.");
			}
			if (this.Skill == "HeavyWeapons")
			{
				stringBuilder.Append("\n-25 move speed");
			}
			if (!this.ProjectilePenetrationStat.IsNullOrEmpty())
			{
				stringBuilder.Append("\nProjectiles fired with this weapon receive bonus penetration based on the wielder's ").Append(this.ProjectilePenetrationStat).Append('.');
			}
			stringBuilder.Append("}}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600AD6C RID: 44396 RVA: 0x003FCF90 File Offset: 0x003FB190
		private static int toCoord(float pos)
		{
			return (int)Math.Floor((double)(pos / 3f));
		}

		// Token: 0x0600AD6D RID: 44397 RVA: 0x003FCFA0 File Offset: 0x003FB1A0
		public static List<Point> CalculateBulletTrajectory(out bool PlayerInvolved, out bool CameNearPlayer, out Cell NearPlayerCell, MissilePath Path, GameObject Projectile = null, GameObject Weapon = null, GameObject Owner = null, Zone Zone = null, string AimVariance = null, int FlatVariance = 0, int WeaponVariance = 0, bool IntendedPathOnly = false)
		{
			PlayerInvolved = false;
			CameNearPlayer = false;
			NearPlayerCell = null;
			double num = Math.Atan2((double)Path.X1 - (double)Path.X0, (double)Path.Y1 - (double)Path.Y0).normalizeRadians();
			List<Pair> list = new List<Pair>(32);
			int num2 = (int)(num * 57.32484076433121);
			Path.Angle = (float)num2;
			int num3 = WeaponVariance + FlatVariance + (AimVariance.IsNullOrEmpty() ? 0 : AimVariance.RollCached());
			if (Weapon != null && Weapon.HasRegisteredEvent("ModifyMissileWeaponAngle"))
			{
				Event @event = Event.New("ModifyMissileWeaponAngle", "Attacker", Owner, "Projectile", Projectile, "Angle", num, "Mod", num3);
				Weapon.FireEvent(@event);
				num = (double)@event.GetParameter("Angle");
				num3 = @event.GetIntParameter("Mod", 0);
			}
			num += (double)num3 * 0.0174532925;
			double num4 = Math.Sin(num);
			double num5 = Math.Cos(num);
			double num6 = (double)Path.X0;
			double num7 = (double)Path.Y0;
			while (Math.Floor(num6) >= 0.0 && Math.Floor(num6) <= 237.0 && Math.Floor(num7) >= 0.0 && Math.Floor(num7) <= 72.0)
			{
				num6 += num4;
				num7 += num5;
			}
			list.AddRange(MissileWeapon.ListOfVisitedSquares((int)Path.X0, (int)Path.Y0, (int)num6, (int)num7));
			if (Zone != null && Projectile != null && !IntendedPathOnly)
			{
				Cell cell = null;
				int i = 0;
				int num8 = list.Count;
				while (i < num8)
				{
					int num9 = MissileWeapon.toCoord((float)list[i].x);
					int num10 = MissileWeapon.toCoord((float)list[i].y);
					Cell cell2 = Zone.GetCell(num9, num10);
					if (cell2 != null && cell2 != cell)
					{
						cell = cell2;
						if (i != 0 && ((cell2.HasObjectWithRegisteredEvent("RefractLight") && Projectile.HasTagOrProperty("Light")) || cell2.HasObjectWithRegisteredEvent("ReflectProjectile")))
						{
							bool flag = true;
							GameObject gameObject = null;
							string text = null;
							int num11 = -1;
							string text2 = null;
							if (cell2.HasObjectWithRegisteredEvent("RefractLight") && Projectile.HasTagOrProperty("Light"))
							{
								Event event2 = Event.New("RefractLight", 0, 0, 0);
								event2.SetParameter("Projectile", Projectile);
								event2.SetParameter("Attacker", Owner);
								event2.SetParameter("Cell", cell2);
								event2.SetParameter("Angle", Path.Angle);
								event2.SetParameter("Direction", Stat.Random(0, 359));
								event2.SetParameter("Verb", null);
								event2.SetParameter("Sound", "sfx_light_refract");
								event2.SetParameter("By", null);
								flag = cell2.FireEvent(event2);
								if (!flag)
								{
									gameObject = event2.GetGameObjectParameter("By");
									text = event2.GetParameter<string>("Sound");
									text2 = event2.GetStringParameter("Verb", null) ?? "refract";
									num11 = event2.GetIntParameter("Direction", 0).normalizeDegrees();
								}
							}
							if (flag && cell2.HasObjectWithRegisteredEvent("ReflectProjectile"))
							{
								Event event3 = Event.New("ReflectProjectile", 0, 0, 0);
								event3.SetParameter("Projectile", Projectile);
								event3.SetParameter("Attacker", Owner);
								event3.SetParameter("Cell", cell2);
								event3.SetParameter("Angle", Path.Angle);
								event3.SetParameter("Direction", Stat.Random(0, 359));
								event3.SetParameter("Verb", null);
								event3.SetParameter("Sound", "sfx_light_refract");
								event3.SetParameter("By", null);
								flag = cell2.FireEvent(event3);
								if (!flag)
								{
									gameObject = event3.GetGameObjectParameter("By");
									text = event3.GetStringParameter("Sound", null);
									text2 = event3.GetStringParameter("Verb", null) ?? "reflect";
									num11 = event3.GetIntParameter("Direction", 0).normalizeDegrees();
								}
							}
							if (!flag && GameObject.Validate(ref gameObject))
							{
								if (gameObject.IsPlayer())
								{
									PlayerInvolved = true;
								}
								else
								{
									GameObject objectContext = gameObject.GetObjectContext();
									if (objectContext != null && objectContext.IsPlayer())
									{
										PlayerInvolved = true;
									}
								}
								if (gameObject != null)
								{
									Physics physics = gameObject.Physics;
									if (physics != null)
									{
										physics.PlayWorldSound(text, 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
									}
								}
								IComponent<GameObject>.XDidYToZ(gameObject, text2, Projectile, null, "!", null, null, gameObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
								float num12 = (float)list[i].x;
								float num13 = (float)list[i].y;
								float num14 = num12;
								float num15 = num13;
								float num16 = (float)Math.Sin((double)((float)num11 * 0.017453292f));
								float num17 = (float)Math.Cos((double)((float)num11 * 0.017453292f));
								list.RemoveRange(i, list.Count - i);
								num8 = list.Count;
								Cell cell3 = cell2;
								do
								{
									num14 += num16;
									num15 += num17;
									Cell cell4 = Zone.GetCell(MissileWeapon.toCoord(num14), MissileWeapon.toCoord(num15));
									if (cell4 == null)
									{
										break;
									}
									if (cell4 != cell2)
									{
										list.Add(new Pair((int)num14, (int)num15));
										if (cell4 != cell3)
										{
											if (cell4.GetCombatTarget(Owner, true, false, false, 0, Projectile, null, null, null, null, false, false, null) != null || cell4.HasSolidObjectForMissile(Owner, Projectile, null, false, false))
											{
												break;
											}
											cell3 = cell4;
										}
									}
								}
								while (num14 > 0f && num14 < 237f && num15 > 0f && num15 < 72f);
							}
						}
					}
					i++;
				}
			}
			List<Point> list2 = new List<Point>(list.Count / 2);
			int num18 = int.MinValue;
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				Pair pair = list[j];
				int num19 = MissileWeapon.toCoord((float)pair.x) + MissileWeapon.toCoord((float)pair.y) * 1000;
				if (num19 != num18)
				{
					list2.Add(new Point(MissileWeapon.toCoord((float)pair.x), MissileWeapon.toCoord((float)pair.y)));
					num18 = num19;
				}
				j++;
			}
			if (The.Player != null && Zone != null && list2.Count > 0)
			{
				Cell currentCell = The.Player.GetCurrentCell();
				if (currentCell != null)
				{
					Cell cell5 = Zone.GetCell(list2[0]);
					if (cell5 != null && cell5.PathDistanceTo(currentCell) >= 2)
					{
						bool flag2 = false;
						int k = 1;
						int count2 = list2.Count;
						while (k < count2)
						{
							Cell cell6 = Zone.GetCell(list2[k]);
							if (cell6 == currentCell)
							{
								break;
							}
							if (flag2)
							{
								if (cell6.PathDistanceTo(currentCell) >= 2)
								{
									CameNearPlayer = true;
									break;
								}
							}
							else if (cell6.PathDistanceTo(currentCell) <= 1)
							{
								flag2 = true;
								NearPlayerCell = cell6;
							}
							k++;
						}
					}
				}
			}
			return list2;
		}

		// Token: 0x0600AD6E RID: 44398 RVA: 0x003FD6D8 File Offset: 0x003FB8D8
		public static List<Point> CalculateBulletTrajectory(MissilePath Path, GameObject projectile = null, GameObject weapon = null, GameObject owner = null, Zone zone = null, string AimVariance = null, int FlatVariance = 0, int WeaponVariance = 0, bool IntendedPathOnly = false)
		{
			bool flag;
			bool flag2;
			Cell cell;
			return MissileWeapon.CalculateBulletTrajectory(out flag, out flag2, out cell, Path, projectile, weapon, owner, zone, AimVariance, FlatVariance, WeaponVariance, IntendedPathOnly);
		}

		// Token: 0x0600AD6F RID: 44399 RVA: 0x003FD700 File Offset: 0x003FB900
		public string GetSlotType(bool Cybernetic = false)
		{
			string text;
			if (Cybernetic)
			{
				CyberneticsBaseItem part = this.ParentObject.GetPart<CyberneticsBaseItem>();
				text = ((part != null) ? part.Slots : null);
			}
			else
			{
				text = this.ParentObject.UsesSlots;
			}
			if (text.IsNullOrEmpty())
			{
				text = this.SlotType;
			}
			if (text.IndexOf(',') != -1)
			{
				return text.CachedCommaExpansion()[0];
			}
			return text;
		}

		// Token: 0x0600AD70 RID: 44400 RVA: 0x003FD760 File Offset: 0x003FB960
		public bool ValidSlotType(string Type, bool Cybernetic = false)
		{
			string text;
			if (Cybernetic)
			{
				CyberneticsBaseItem part = this.ParentObject.GetPart<CyberneticsBaseItem>();
				text = ((part != null) ? part.Slots : null);
			}
			else
			{
				text = this.ParentObject.UsesSlots;
			}
			if (text.IsNullOrEmpty())
			{
				text = this.SlotType;
			}
			if (text.IndexOf(',') != -1)
			{
				List<string> list = text.CachedCommaExpansion();
				return list.Contains(Type) || list.Contains("*");
			}
			return text == Type || text == "*";
		}

		// Token: 0x0600AD71 RID: 44401 RVA: 0x003FD7E4 File Offset: 0x003FB9E4
		public static void SetupProjectile(GameObject Projectile, GameObject Attacker, GameObject Launcher = null, Projectile pProjectile = null)
		{
			Projectile.SetIntProperty("Primed", 1, false);
			if (pProjectile != null)
			{
				pProjectile.Launcher = Launcher;
			}
			if (Attacker.HasEffect<Phased>() && !Projectile.HasTagOrProperty("IndependentPhaseProjectile") && Projectile.FireEvent("CanApplyPhased") && Projectile.ForceApplyEffect(new Phased(9999), null))
			{
				Projectile.ModIntProperty("ProjectilePhaseAdded", 1, false);
			}
			if (Attacker.HasEffect<Omniphase>() && !Projectile.HasTagOrProperty("IndependentOmniphaseProjectile") && Projectile.FireEvent("CanApplyOmniphase") && Projectile.ForceApplyEffect(new Omniphase(9999), null))
			{
				Projectile.ModIntProperty("ProjectileOmniphaseAdded", 1, false);
			}
			if (Launcher != null && Launcher.HasRegisteredEvent("ProjectileSetup"))
			{
				Launcher.FireEvent(Event.New("ProjectileSetup", "Attacker", Attacker, "Launcher", Launcher, "Projectile", Projectile));
			}
		}

		// Token: 0x0600AD72 RID: 44402 RVA: 0x003FD8C4 File Offset: 0x003FBAC4
		public static void CleanupProjectile(GameObject Projectile)
		{
			if (!GameObject.Validate(ref Projectile))
			{
				return;
			}
			if (Projectile.Physics.IsReal)
			{
				if (Projectile.GetIntProperty("ProjectilePhaseAdded", 0) > 0)
				{
					Projectile.RemoveEffect<Phased>();
					Projectile.ModIntProperty("ProjectilePhaseAdded", -1, true);
				}
				if (Projectile.GetIntProperty("ProjectileOmniphaseAdded", 0) > 0)
				{
					Projectile.RemoveEffect<Omniphase>();
					Projectile.ModIntProperty("ProjectileOmniphaseAdded", -1, true);
					return;
				}
			}
			else
			{
				Projectile.Obliterate(null, false, null);
			}
		}

		// Token: 0x0600AD73 RID: 44403 RVA: 0x003FD93C File Offset: 0x003FBB3C
		private void MissileHit(GameObject Attacker, GameObject Defender, GameObject Owner, GameObject Projectile, Projectile pProjectile, GameObject AimedAt, GameObject ApparentTarget, MissilePath MPath, Cell ImpactCell, FireType FireType, int AimLevel, int NaturalHitResult, int HitResult, bool PathInvolvesPlayer, GameObject MessageAsFrom, bool UsePopups, ref bool Done, ref bool PenetrateCreatures, ref bool PenetrateWalls, bool TargetWasInitiallyUnset, bool ShowUninvolved)
		{
			try
			{
				bool flag = false;
				if (DefenderMissileHitEvent.Check(this.ParentObject, Attacker, Defender, Owner, Projectile, pProjectile, AimedAt, ApparentTarget, MPath, FireType, AimLevel, NaturalHitResult, HitResult, PathInvolvesPlayer, MessageAsFrom, ref Done, ref PenetrateCreatures, ref PenetrateWalls))
				{
					bool flag2 = Defender != ApparentTarget;
					string text = null;
					if (MessageAsFrom != null && MessageAsFrom != Owner)
					{
						if (MessageAsFrom.IsPlayer())
						{
							text = "You";
						}
						else if (MessageAsFrom.HasProperName)
						{
							text = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(MessageAsFrom.ShortDisplayName);
						}
						else if (MessageAsFrom.Equipped == Owner)
						{
							text = Owner.Poss(MessageAsFrom, true, null);
						}
						else if (MessageAsFrom.Equipped != null)
						{
							text = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(Grammar.MakePossessive(MessageAsFrom.Equipped.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + " " + MessageAsFrom.ShortDisplayName);
						}
						else
						{
							text = ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(MessageAsFrom.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
						}
					}
					if (Defender == AimedAt && (FireType == FireType.BeaconFire || (FireType == FireType.OneShot && Owner.HasSkill("Rifle_BeaconFire") && Rifle_BeaconFire.MeetsCriteria(Defender))))
					{
						if (Owner.IsPlayer())
						{
							if (text != null)
							{
								base.EmitMessage(string.Concat(new string[]
								{
									text,
									MessageAsFrom.GetVerb("hit", true, false, false, true, false),
									" ",
									(Defender == MessageAsFrom) ? MessageAsFrom.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
									" in a vital area."
								}), ' ', false, UsePopups, false, null, null);
							}
							else
							{
								base.EmitMessage("You hit " + ((Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + " in a vital area.", ' ', false, UsePopups, false, null, null);
							}
						}
						Defender.BloodsplatterCone(true, MPath.Angle, 45);
						flag = true;
					}
					if (!flag)
					{
						int num = GetCriticalThresholdEvent.GetFor(Attacker, Defender, this.ParentObject, Projectile, this.Skill, 20);
						int @for = GetSpecialEffectChanceEvent.GetFor(Attacker, this.ParentObject, "Missile Critical", 5, Defender, Projectile, true, false);
						if (@for != 5)
						{
							num -= (@for - 5) / 5;
						}
						if (NaturalHitResult >= num)
						{
							flag = true;
						}
					}
					int num2 = pProjectile.BasePenetration;
					int num3 = pProjectile.BasePenetration + pProjectile.StrengthPenetration;
					if (flag)
					{
						BaseSkill genericSkill = Skills.GetGenericSkill(this.Skill, Attacker);
						if (genericSkill != null)
						{
							int weaponCriticalModifier = genericSkill.GetWeaponCriticalModifier(Attacker, Defender, this.ParentObject);
							if (weaponCriticalModifier != 0)
							{
								num2 += weaponCriticalModifier;
								num3 += weaponCriticalModifier;
							}
						}
					}
					if (!this.ProjectilePenetrationStat.IsNullOrEmpty() && Attacker != null)
					{
						num2 += Attacker.StatMod(this.ProjectilePenetrationStat, 0);
					}
					Event @event = Event.New("WeaponMissileWeaponHit", 0, 0, 0);
					@event.SetParameter("Attacker", Attacker);
					@event.SetParameter("Defender", Defender);
					@event.SetParameter("Weapon", this.ParentObject);
					@event.SetParameter("Penetrations", num2);
					@event.SetParameter("PenetrationCap", num3);
					@event.SetParameter("MessageAsFrom", MessageAsFrom);
					@event.SetFlag("Critical", flag);
					this.ParentObject.FireEvent(@event);
					num2 = @event.GetIntParameter("Penetrations", 0);
					num3 = @event.GetIntParameter("PenetrationCap", 0);
					flag = @event.HasFlag("Critical");
					@event.ID = "AttackerMissileWeaponHit";
					if (Attacker != null)
					{
						Attacker.FireEvent(@event);
					}
					@event.ID = "DefenderMissileWeaponHit";
					if (Defender != null)
					{
						Defender.FireEvent(@event);
					}
					if (flag)
					{
						@event.ID = "MissileAttackerCriticalHit";
						Attacker.FireEvent(@event);
					}
					bool flag3 = Defender.HasTag("Creature");
					string blueprint = Defender.Blueprint;
					WeaponUsageTracking.TrackMissileWeaponHit(Owner, this.ParentObject, Projectile, flag3, blueprint, flag2);
					GetMissileWeaponPerformanceEvent for2 = GetMissileWeaponPerformanceEvent.GetFor(Owner, this.ParentObject, Projectile, new int?(num2), new int?(num3), pProjectile.BaseDamage, null, null, new bool?(pProjectile.PenetrateCreatures), new bool?(pProjectile.PenetrateWalls), new bool?(pProjectile.Quiet), null, null, true);
					if (for2.PenetrateCreatures)
					{
						PenetrateCreatures = true;
					}
					if (for2.PenetrateWalls)
					{
						PenetrateWalls = true;
					}
					Damage damage = new Damage(0);
					damage.AddAttributes(for2.Attributes);
					int num4;
					if (for2.Attributes.Contains("Mental"))
					{
						if (Defender.Brain == null && (Defender.IsCreature ? for2.PenetrateCreatures : PenetrateWalls))
						{
							return;
						}
						num4 = Stats.GetCombatMA(Defender);
					}
					else
					{
						num4 = Stats.GetCombatAV(Defender);
					}
					int num5 = 0;
					if (for2.Attributes.Contains("NonPenetrating"))
					{
						num5 = 1;
					}
					else if (for2.Attributes.Contains("Vorpal"))
					{
						num5 = Stat.RollDamagePenetrations(0, 0, 0) + for2.PenetrationBonus;
					}
					else
					{
						num5 = for2.RollDamagePenetrations(num4);
					}
					string text2 = null;
					MissilePenetrateEvent.Process(this.ParentObject, Attacker, Defender, Owner, Projectile, pProjectile, AimedAt, ApparentTarget, MPath, FireType, AimLevel, NaturalHitResult, PathInvolvesPlayer, MessageAsFrom, ref num5, ref text2);
					if (this.Skill == "Pistol" && Owner.HasSkill("Pistol_DisarmingShot") && Owner.StatMod("Agility", 0).in100())
					{
						Disarming.Disarm(Defender, Attacker, 100, "Strength", "Agility", null, this.ParentObject);
					}
					if (num5 == 0)
					{
						Defender.ParticleBlip("&K\a", 10, 0L, false);
						if (Owner.IsPlayer())
						{
							if (text != null)
							{
								base.EmitMessage(string.Concat(new string[]
								{
									text,
									MessageAsFrom.GetVerb("fail", true, false, false, true, false),
									" to penetrate ",
									Defender.poss("armor", true),
									" with ",
									MessageAsFrom.its_(Projectile, false),
									text2,
									"!"
								}), 'r', false, UsePopups, false, null, null);
							}
							else
							{
								base.EmitMessage(string.Concat(new string[]
								{
									Owner.Poss(Projectile, true, null),
									Projectile.GetVerb("fail", true, false, false, true, false),
									" to penetrate ",
									Defender.poss("armor", true),
									text2,
									"!"
								}), 'r', false, UsePopups, false, null, null);
							}
						}
						else if (Defender.IsPlayer())
						{
							if (text != null)
							{
								base.EmitMessage(string.Concat(new string[]
								{
									text,
									MessageAsFrom.GetVerb("fail", true, false, false, true, false),
									" to penetrate your armor with ",
									MessageAsFrom.its_(Projectile, false),
									text2,
									"!"
								}), 'g', false, UsePopups, false, null, null);
							}
							else
							{
								base.EmitMessage(string.Concat(new string[]
								{
									Owner.Poss(Projectile, true, null),
									Projectile.GetVerb("fail", true, false, false, true, false),
									" to penetrate your armor",
									text2,
									"!"
								}), 'g', false, UsePopups, false, null, null);
							}
						}
						Done = true;
						if (Projectile.IsValid() && ImpactCell != null)
						{
							ImpactCell.AddObject(Projectile, false, false, true, true, false, true, true, null, "Missile Transit", null, null, null, null, null, null);
						}
						Event event2 = Event.New("ProjectileHit", 0, 0, 0);
						event2.SetParameter("Attacker", Attacker);
						event2.SetParameter("Defender", Defender);
						event2.SetParameter("Skill", this.Skill);
						event2.SetParameter("Damage", damage);
						event2.SetParameter("AimLevel", AimLevel);
						event2.SetParameter("Owner", Attacker);
						event2.SetParameter("Launcher", this.ParentObject);
						event2.SetParameter("Projectile", Projectile);
						event2.SetParameter("Path", MPath);
						event2.SetParameter("Penetrations", 0);
						event2.SetParameter("ApparentTarget", ApparentTarget);
						event2.SetParameter("AimedAt", AimedAt);
						event2.SetParameter("ImpactCell", ImpactCell);
						event2.SetFlag("Critical", flag);
						Projectile.FireEvent(event2);
						event2.ID = "DefenderProjectileHit";
						Defender.FireEvent(event2);
						event2.ID = "LauncherProjectileHit";
						this.ParentObject.FireEvent(event2);
					}
					else
					{
						if (Defender == AimedAt && Defender.IsCombatObject())
						{
							if (FireType == FireType.SuppressingFire || FireType == FireType.FlatteningFire || (FireType == FireType.OneShot && Attacker.HasSkill("Rifle_SuppressiveFire")))
							{
								if (Defender.ApplyEffect(new Suppressed(Stat.Random(3, 5)), null))
								{
									if (text != null)
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, Grammar.MakePossessive(text) + " suppressive fire locks " + Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in place.", ' ', false, false, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(Attacker, Attacker.Poss("suppressive fire locks ", true) + Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in place.", ' ', false, false, false, null, null);
									}
								}
								if (Attacker.HasSkill("Rifle_FlatteningFire") && Rifle_FlatteningFire.MeetsCriteria(Defender))
								{
									if (Defender.ApplyEffect(new Prone(), null))
									{
										if (text != null)
										{
											IComponent<GameObject>.EmitMessage(MessageAsFrom, Grammar.MakePossessive(text) + " flattening fire drops " + Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to the ground!", ' ', false, false, false, null, null);
										}
										else
										{
											IComponent<GameObject>.EmitMessage(Attacker, Attacker.Poss("flattening fire drops ", true) + Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to the ground!", ' ', false, false, false, null, null);
										}
									}
									Disarming.Disarm(Defender, Attacker, 100, "Strength", "Agility", null, this.ParentObject);
								}
							}
							if (FireType == FireType.WoundingFire || FireType == FireType.DisorientingFire || (FireType == FireType.OneShot && Attacker.HasSkill("Rifle_WoundingFire")))
							{
								string text3 = (Attacker.IsPlayer() ? "You" : Attacker.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
								if (Defender.ApplyEffect(new Bleeding(num5.ToString(), 20 + for2.BaseDamage.RollMaxCached(), Attacker, false, false, false, false), null))
								{
									if (text != null)
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
										{
											text,
											MessageAsFrom.GetVerb("wound", true, false, false, true, false),
											" ",
											Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											"."
										}), ' ', false, false, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(Attacker, string.Concat(new string[]
										{
											text3,
											Attacker.GetVerb("wound", true, false, false, true, false),
											" ",
											Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											"."
										}), ' ', false, false, false, null, null);
									}
									Defender.BloodsplatterCone(true, MPath.Angle, 45);
								}
								if (Attacker.HasSkill("Rifle_DisorientingFire") && Rifle_DisorientingFire.MeetsCriteria(Defender) && Defender.ApplyEffect(new Disoriented(Stat.Random(5, 7), 4), null))
								{
									if (text != null)
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
										{
											text,
											MessageAsFrom.GetVerb("disorient", true, false, false, true, false),
											" ",
											Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											"."
										}), ' ', false, false, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(Attacker, string.Concat(new string[]
										{
											text3,
											Attacker.GetVerb("disorient", true, false, false, true, false),
											" ",
											Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											"."
										}), ' ', false, false, false, null, null);
									}
								}
							}
						}
						if (Options.ShowMonsterHPHearts)
						{
							Defender.ParticleBlip(Stat.GetResultColor(num5) + "\u0003", 10, 0L, false);
						}
						bool flag4 = for2.BaseDamage != "0";
						if (for2.Attributes.Contains("Mental") && Defender.Brain == null)
						{
							flag4 = false;
							if (Attacker.IsPlayer())
							{
								text2 = ", but your mental attack has no effect" + (text2.IsNullOrEmpty() ? "" : text2);
							}
						}
						string text4 = (flag ? "critically" : null);
						string text5 = (flag ? " critically" : "");
						if (flag4)
						{
							DieRoll possiblyCachedDamageRoll = for2.GetPossiblyCachedDamageRoll();
							int num6 = 0;
							for (int i = 0; i < num5; i++)
							{
								num6 += possiblyCachedDamageRoll.Resolve();
							}
							damage.Amount = num6;
							if (flag2)
							{
								damage.AddAttribute("Accidental");
							}
							int num7 = Projectile.GetPhase();
							if (damage.Amount > 0 && flag)
							{
								Defender.ParticleText("*critical hit*", IComponent<GameObject>.ConsequentialColorChar(null, Defender), false, 1.5f, -8f);
								Defender.PlayWorldSound("Sounds/Damage/sfx_damage_critical", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
							}
							if (damage.Amount > 0)
							{
								if (num5 < 2)
								{
									if (Defender != null)
									{
										Defender.PlayWorldSound("Sounds/Damage/sfx_damage_penetrate_low", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
									}
								}
								else if (num5 < 4)
								{
									if (Defender != null)
									{
										Defender.PlayWorldSound("Sounds/Damage/sfx_damage_penetrate_med", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
									}
								}
								else if (Defender != null)
								{
									Defender.PlayWorldSound("Sounds/Damage/sfx_damage_penetrate_high", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
								}
								Event event3 = Event.New("DealingMissileDamage", 0, 0, 0);
								event3.SetParameter("Attacker", Attacker);
								event3.SetParameter("Defender", Defender);
								event3.SetParameter("Skill", this.Skill);
								event3.SetParameter("Damage", damage);
								event3.SetParameter("AimLevel", AimLevel);
								event3.SetParameter("Phase", num7);
								event3.SetFlag("Critical", flag);
								if (!Attacker.FireEvent(event3))
								{
									damage.Amount = 0;
								}
								if (event3.HasFlag("RecheckPhase"))
								{
									num7 = Projectile.GetPhase();
								}
							}
							if (damage.Amount > 0)
							{
								Event event4 = Event.New("WeaponDealingMissileDamage", 0, 0, 0);
								event4.SetParameter("Attacker", Attacker);
								event4.SetParameter("Defender", Defender);
								event4.SetParameter("Skill", this.Skill);
								event4.SetParameter("Damage", damage);
								event4.SetParameter("AimLevel", AimLevel);
								event4.SetParameter("Phase", num7);
								event4.SetFlag("Critical", flag);
								if (!this.ParentObject.FireEvent(event4))
								{
									damage.Amount = 0;
								}
								if (event4.HasFlag("RecheckPhase"))
								{
									num7 = Projectile.GetPhase();
								}
							}
							bool flag5 = false;
							if (damage.Amount > 0)
							{
								Defender.WillCheckHP(new bool?(true));
								flag5 = true;
								Event event5 = Event.New("TakeDamage", 0, 0, 0);
								event5.SetParameter("Damage", damage);
								event5.SetParameter("Owner", Attacker);
								event5.SetParameter("Attacker", Attacker);
								event5.SetParameter("Weapon", this.ParentObject);
								event5.SetParameter("Projectile", Projectile);
								event5.SetParameter("Phase", num7);
								event5.SetParameter("OutcomeMessageFragment", text2);
								event5.SetFlag("WillUseOutcomeMessageFragment", true);
								if (this.ParentObject.HasTagOrProperty("NoMissileSetTarget"))
								{
									event5.SetFlag("NoSetTarget", true);
								}
								if (!Defender.FireEvent(event5))
								{
									damage.Amount = 0;
								}
								text2 = event5.GetStringParameter("OutcomeMessageFragment", null);
							}
							WeaponUsageTracking.TrackMissileWeaponDamage(Owner, this.ParentObject, Projectile, flag3, blueprint, flag2, damage);
							if (damage.Amount > 0)
							{
								if (Options.ShowMonsterHPHearts)
								{
									Defender.ParticleBlip(Defender.GetHPColor() + "\u0003", 10, 0L, false);
								}
							}
							else if (flag5)
							{
								Defender.WillCheckHP(new bool?(false));
								flag5 = false;
							}
							if (Owner.IsPlayer())
							{
								if (text2 != null)
								{
									if (Defender.IsVisible())
									{
										if (text != null)
										{
											if (MessageAsFrom.IsVisible())
											{
												IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
												{
													text,
													text5,
													MessageAsFrom.GetVerb("hit", true, false, false, true, false),
													" ",
													(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
													" with ",
													Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
													text2,
													"."
												}), ' ', false, UsePopups, false, null, null);
											}
											else
											{
												IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
												{
													"Something hits ",
													(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
													" with ",
													Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
													text2,
													"."
												}), ' ', false, UsePopups, false, null, null);
											}
										}
										else
										{
											IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
											{
												"You",
												text5,
												" hit ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												" with ",
												Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
									}
								}
								else if (damage.Amount > 0 || !damage.SuppressionMessageDone)
								{
									if (text != null)
									{
										if (Defender.IsVisible())
										{
											if (MessageAsFrom.IsVisible())
											{
												IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
												{
													text,
													text5,
													MessageAsFrom.GetVerb("hit", true, false, false, true, false),
													" ",
													(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
													" (x",
													num5.ToString(),
													") with ",
													Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
													" for ",
													damage.Amount.ToString(),
													" damage!"
												}), Stat.GetResultColorChar(num5), false, UsePopups, false, null, null);
											}
											else
											{
												IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
												{
													"Something hits ",
													(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
													" (x",
													num5.ToString(),
													") with ",
													Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
													" for ",
													damage.Amount.ToString(),
													" damage!"
												}), Stat.GetResultColorChar(num5), false, UsePopups, false, null, null);
											}
										}
										else if (Defender.IsAudible(The.Player, 80))
										{
											IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
											{
												text,
												MessageAsFrom.GetVerb("hit", true, false, false, true, false),
												" something ",
												Owner.DescribeDirectionToward(Defender, false, false),
												"!"
											}), ' ', false, UsePopups, false, null, null);
										}
									}
									else if (Defender.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
										{
											"You",
											text5,
											" hit ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" (x",
											num5.ToString(),
											") with ",
											Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" for ",
											damage.Amount.ToString(),
											" damage!"
										}), Stat.GetResultColorChar(num5), false, UsePopups, false, null, null);
									}
									else if (Defender.IsAudible(The.Player, 80))
									{
										IComponent<GameObject>.EmitMessage(Owner, "You hit something " + Owner.DescribeDirectionToward(Defender, false, false) + "!", ' ', false, UsePopups, false, null, null);
									}
								}
							}
							else if (Defender.IsPlayer())
							{
								if (text2 != null)
								{
									if (text != null)
									{
										if (MessageAsFrom.IsVisible())
										{
											IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
											{
												text,
												text5,
												MessageAsFrom.GetVerb("hit", true, false, false, true, false),
												" you with ",
												Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
										else
										{
											IComponent<GameObject>.EmitMessage(Owner, Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false) + " you" + text2 + ".", ' ', false, UsePopups, false, null, null);
										}
									}
									else if (Owner.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
										{
											Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											" you with ",
											Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											text2,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" you ",
											Defender.DescribeDirectionFrom(Owner, false),
											text2,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}
								}
								else if (text != null)
								{
									if (MessageAsFrom.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
										{
											text,
											text5,
											MessageAsFrom.GetVerb("hit", true, false, false, true, false),
											" you (x",
											num5.ToString(),
											") with ",
											Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" for ",
											damage.Amount.ToString(),
											" damage!"
										}), 'r', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" you (x",
											num5.ToString(),
											") ",
											Defender.DescribeDirectionFrom(Owner, false),
											" for ",
											damage.Amount.ToString(),
											" damage!"
										}), 'r', false, UsePopups, false, null, null);
									}
								}
								else if (Owner.IsVisible())
								{
									IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
									{
										Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
										" you (x",
										num5.ToString(),
										") with ",
										Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
										" for ",
										damage.Amount.ToString(),
										" damage!"
									}), 'r', false, UsePopups, false, null, null);
								}
								else
								{
									IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
									{
										Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
										" you (x",
										num5.ToString(),
										") ",
										Defender.DescribeDirectionFrom(Owner, false),
										" for ",
										damage.Amount.ToString(),
										" damage!"
									}), 'r', false, UsePopups, false, null, null);
								}
							}
							else if ((PathInvolvesPlayer || ShowUninvolved) && Defender.IsVisible())
							{
								if (text2 != null)
								{
									if (text != null)
									{
										if (MessageAsFrom.IsVisible())
										{
											IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
											{
												text,
												text5,
												MessageAsFrom.GetVerb("hit", true, false, false, true, false),
												" ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												" with ",
												Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
										else
										{
											IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
											{
												Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
												" ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												" ",
												The.Player.DescribeDirectionFrom(Owner, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
									}
									else if (Owner.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
										{
											Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" with ",
											Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											text2,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" ",
											The.Player.DescribeDirectionFrom(Owner, false),
											text2,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}
								}
								else if (text != null)
								{
									if (MessageAsFrom.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
										{
											text,
											text5,
											MessageAsFrom.GetVerb("hit", true, false, false, true, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" (x",
											num5.ToString(),
											") with ",
											Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" for ",
											damage.Amount.ToString(),
											" damage!"
										}), ' ', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" (x",
											num5.ToString(),
											") ",
											The.Player.DescribeDirectionFrom(Owner, false),
											" for ",
											damage.Amount.ToString(),
											" damage!"
										}), ' ', false, UsePopups, false, null, null);
									}
								}
								else if (Owner.IsVisible())
								{
									IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
									{
										Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
										" ",
										(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
										" (x",
										num5.ToString(),
										") with ",
										Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
										" for ",
										damage.Amount.ToString(),
										" damage!"
									}), ' ', false, UsePopups, false, null, null);
								}
								else
								{
									IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
									{
										Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
										" ",
										(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
										" (x",
										num5.ToString(),
										") ",
										The.Player.DescribeDirectionFrom(Owner, false),
										" for ",
										damage.Amount.ToString(),
										" damage!"
									}), ' ', false, UsePopups, false, null, null);
								}
							}
							if (flag5)
							{
								Defender.CheckHP(null, null, null, true);
							}
						}
						else if (!for2.Quiet)
						{
							if (Owner.IsPlayer())
							{
								if (text2 != null)
								{
									if (Defender.IsVisible())
									{
										if (text != null)
										{
											if (MessageAsFrom.IsVisible())
											{
												IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
												{
													text,
													text5,
													MessageAsFrom.GetVerb("hit", true, false, false, true, false),
													" ",
													(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
													" with ",
													Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
													text2,
													"."
												}), ' ', false, UsePopups, false, null, null);
											}
											else
											{
												IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
												{
													Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
													" ",
													(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
													" ",
													Defender.DescribeDirectionFrom(Owner, false),
													text2,
													"."
												}), ' ', false, UsePopups, false, null, null);
											}
										}
										else
										{
											IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
											{
												"You",
												text5,
												" hit ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												" with ",
												Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
									}
								}
								else if (text != null)
								{
									if (Defender.IsVisible())
									{
										if (MessageAsFrom.IsVisible())
										{
											IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
											{
												text,
												text5,
												MessageAsFrom.GetVerb("hit", true, false, false, true, false),
												" ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												" (x",
												num5.ToString(),
												") with ",
												Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
												"!"
											}), Stat.GetResultColorChar(num5), false, UsePopups, false, null, null);
										}
										else
										{
											IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
											{
												Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
												Projectile.GetVerb("hit", true, false, false, true, false),
												" ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												" (x",
												num5.ToString(),
												") ",
												Defender.DescribeDirectionFrom(Owner, false),
												"!"
											}), Stat.GetResultColorChar(num5), false, UsePopups, false, null, null);
										}
									}
									else if (MessageAsFrom.IsVisible() && Defender.IsAudible(The.Player, 80))
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
										{
											text,
											MessageAsFrom.GetVerb("hit", true, false, false, true, false),
											" something ",
											Owner.DescribeDirectionToward(Defender, false, false),
											"!"
										}), ' ', false, UsePopups, false, null, null);
									}
								}
								else if (Defender.IsVisible())
								{
									IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
									{
										"You",
										text5,
										" hit ",
										(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
										" (x",
										num5.ToString(),
										") with ",
										Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
										"!"
									}), Stat.GetResultColorChar(num5), false, UsePopups, false, null, null);
								}
								else if (Defender.IsAudible(The.Player, 80))
								{
									IComponent<GameObject>.EmitMessage(Owner, "You hit something " + Owner.DescribeDirectionToward(Defender, false, false) + "!", ' ', false, UsePopups, false, null, null);
								}
							}
							else if (Defender.IsPlayer())
							{
								if (text2 != null)
								{
									if (text != null)
									{
										if (MessageAsFrom.IsVisible())
										{
											IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
											{
												text,
												text5,
												MessageAsFrom.GetVerb("hit", true, false, false, true, false),
												" you with ",
												Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
										else
										{
											IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
											{
												Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
												" you ",
												Defender.DescribeDirectionToward(Owner, false, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
									}
									else if (Owner.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(Owner, Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " you" + text2 + ".", ' ', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" you ",
											Defender.DescribeDirectionToward(Owner, false, false),
											text2,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}
								}
								else if (text != null)
								{
									if (MessageAsFrom.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
										{
											text,
											text5,
											" you with ",
											Projectile.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											"! (x",
											num5.ToString(),
											")"
										}), 'r', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" you ",
											Defender.DescribeDirectionToward(Owner, false, false),
											"! (x",
											num5.ToString(),
											")"
										}), 'r', false, UsePopups, false, null, null);
									}
								}
								else if (Owner.IsVisible())
								{
									IComponent<GameObject>.EmitMessage(Owner, Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " you! (x" + num5.ToString() + ")", 'r', false, UsePopups, false, null, null);
								}
								else
								{
									IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
									{
										Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
										" you ",
										Defender.DescribeDirectionToward(Owner, false, false),
										"! (x",
										num5.ToString(),
										")"
									}), 'r', false, UsePopups, false, null, null);
								}
							}
							else if ((PathInvolvesPlayer || ShowUninvolved) && Defender.IsVisible())
							{
								if (text2 != null)
								{
									if (text != null)
									{
										if (MessageAsFrom.IsVisible())
										{
											IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
											{
												text,
												text5,
												MessageAsFrom.GetVerb("hit", true, false, false, true, false),
												" ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
										else
										{
											IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
											{
												Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
												" ",
												(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
												" ",
												The.Player.DescribeDirectionToward(Owner, false, false),
												text2,
												"."
											}), ' ', false, UsePopups, false, null, null);
										}
									}
									else if (Owner.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
										{
											Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											text2,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" ",
											The.Player.DescribeDirectionToward(Owner, false, false),
											text2,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}
								}
								else if (text != null)
								{
									if (MessageAsFrom.IsVisible())
									{
										IComponent<GameObject>.EmitMessage(MessageAsFrom, string.Concat(new string[]
										{
											text,
											text5,
											MessageAsFrom.GetVerb("hit", true, false, false, true, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											"! (x",
											num5.ToString(),
											")"
										}), ' ', false, UsePopups, false, null, null);
									}
									else
									{
										IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
										{
											Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
											" ",
											(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" (x",
											num5.ToString(),
											") ",
											The.Player.DescribeDirectionToward(Owner, false, false),
											"!"
										}), ' ', false, UsePopups, false, null, null);
									}
								}
								else if (Owner.IsVisible())
								{
									IComponent<GameObject>.EmitMessage(Owner, string.Concat(new string[]
									{
										Owner.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
										" ",
										(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
										"! (x",
										num5.ToString(),
										")"
									}), ' ', false, UsePopups, false, null, null);
								}
								else
								{
									IComponent<GameObject>.EmitMessage(The.Player, string.Concat(new string[]
									{
										Projectile.Does("hit", int.MaxValue, null, null, text4, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false),
										" ",
										(Defender == Owner) ? Owner.itself : Defender.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
										" (x",
										num5.ToString(),
										") ",
										The.Player.DescribeDirectionToward(Owner, false, false),
										"!"
									}), ' ', false, UsePopups, false, null, null);
								}
							}
						}
						if (Owner.IsPlayer() && !this.ParentObject.HasTagOrProperty("NoMissileSetTarget") && Sidebar.CurrentTarget == null && Defender != Owner && Defender.IsCreature && Defender.IsHostileTowards(Owner) && Defender.IsVisible() && TargetWasInitiallyUnset)
						{
							Sidebar.CurrentTarget = Defender;
						}
						if (Projectile.IsValid() && ImpactCell != null)
						{
							ImpactCell.AddObject(Projectile, false, false, true, true, false, true, true, null, "Missile Transit", null, null, null, null, null, null);
						}
						Event event6 = Event.New("ProjectileHit", 0, 0, 0);
						event6.SetParameter("Attacker", Attacker);
						event6.SetParameter("Defender", Defender);
						event6.SetParameter("Projectile", Projectile);
						event6.SetParameter("Skill", this.Skill);
						event6.SetParameter("Damage", damage);
						event6.SetParameter("AimLevel", AimLevel);
						event6.SetParameter("Owner", Attacker);
						event6.SetParameter("Launcher", this.ParentObject);
						event6.SetParameter("Path", MPath);
						event6.SetParameter("Penetrations", num5);
						event6.SetParameter("ImpactCell", ImpactCell);
						event6.SetParameter("ApparentTarget", ApparentTarget);
						event6.SetParameter("AimedAt", AimedAt);
						event6.SetFlag("critical", flag);
						Projectile.FireEvent(event6);
						event6.ID = "DefenderProjectileHit";
						Defender.FireEvent(event6);
						event6.ID = "LauncherProjectileHit";
						this.ParentObject.FireEvent(event6);
						if (!for2.PenetrateCreatures)
						{
							Done = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("MissileWeapon:MissileHit", ex, "game_exception");
			}
			finally
			{
				if (GameObject.Validate(ref Defender))
				{
					Defender.WillCheckHP(new bool?(false));
				}
			}
		}

		// Token: 0x0600AD74 RID: 44404 RVA: 0x00400CB0 File Offset: 0x003FEEB0
		public bool IsSkilled(GameObject Actor)
		{
			if (Actor != null)
			{
				if (this.Skill == "Pistol")
				{
					return Actor.HasSkill("Pistol_SteadyHands");
				}
				if (this.Skill == "Rifle" || this.Skill == "Bow")
				{
					return Actor.HasSkill("Rifle_SteadyHands");
				}
			}
			return false;
		}

		// Token: 0x0600AD75 RID: 44405 RVA: 0x00400D10 File Offset: 0x003FEF10
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandFireMissile")
			{
				GameObject gameObject = E.GetGameObjectParameter("Actor") ?? E.GetGameObjectParameter("Owner");
				if (gameObject == null)
				{
					return false;
				}
				Cell cell = E.GetParameter("TargetCell") as Cell;
				Cell cell2 = E.GetParameter("StartCell") as Cell;
				GameObject Attacker = gameObject;
				FireType fireType = FireType.Normal;
				if (E.HasParameter("FireType"))
				{
					fireType = (FireType)E.GetParameter("FireType");
				}
				if (cell2 == null)
				{
					cell2 = gameObject.CurrentCell;
				}
				if (cell2 == null)
				{
					return false;
				}
				Zone parentZone = cell2.ParentZone;
				if (parentZone == null)
				{
					return false;
				}
				bool flag = parentZone.IsActive();
				bool flag2 = E.HasFlag("ActivePartsIgnoreSubject");
				bool flag3 = E.HasFlag("UsePopups");
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(true);
				ScreenBuffer screenBuffer = E.GetParameter("ScreenBuffer") as ScreenBuffer;
				if (flag)
				{
					if (screenBuffer != null)
					{
						scrapBuffer.Copy(screenBuffer);
					}
					else
					{
						XRLCore.Core.RenderBaseToBuffer(scrapBuffer);
					}
				}
				GameObject gameObject2 = null;
				int intParameter = E.GetIntParameter("AimLevel", 0);
				MissilePath missilePath = E.GetParameter("Path") as MissilePath;
				if (missilePath == null)
				{
					if (MissileWeapon.CalculatedMissilePathInUse)
					{
						if (MissileWeapon.SecondCalculatedMissilePathInUse)
						{
							missilePath = new MissilePath();
						}
						else
						{
							missilePath = MissileWeapon.SecondCalculatedMissilePath;
							MissileWeapon.SecondCalculatedMissilePathInUse = true;
						}
					}
					else
					{
						missilePath = MissileWeapon.CalculatedMissilePath;
						MissileWeapon.CalculatedMissilePathInUse = true;
					}
					MissileWeapon.CalculateMissilePath(missilePath, parentZone, cell2.X, cell2.Y, cell.X, cell.Y, E.HasFlag("IncludeStart"), false, false, gameObject);
				}
				try
				{
					int intParameter2 = E.GetIntParameter("FlatVariance", 0);
					if (!Attacker.FireEvent("BeginMissileAttack"))
					{
						return false;
					}
					bool flag4 = false;
					if (!this.NoWildfire)
					{
						int num = 0;
						foreach (Cell cell3 in Attacker.CurrentCell.GetAdjacentCells(true))
						{
							foreach (GameObject gameObject3 in cell3.LoopObjectsWithPart("Combat"))
							{
								if (gameObject3.IsHostileTowards(Attacker) && gameObject3.PhaseAndFlightMatches(Attacker) && gameObject3.CanMoveExtremities(null, false, false, true))
								{
									num++;
									if (num > 1)
									{
										if (50.in100())
										{
											flag4 = true;
										}
										goto IL_0277;
									}
								}
							}
						}
					}
					IL_0277:
					float num2 = missilePath.X1 - missilePath.X0;
					float num3 = missilePath.Y1 - missilePath.Y0;
					string text = "-";
					float num4;
					if (num2 == 0f)
					{
						num4 = 9999f;
					}
					else
					{
						num4 = Math.Abs(num3) / Math.Abs(num2);
					}
					if (num4 >= 2f)
					{
						text = "|";
					}
					else if ((double)num4 >= 0.5)
					{
						if (num2 < 0f)
						{
							if (num3 > 0f)
							{
								text = "/";
							}
							else
							{
								text = "\\";
							}
						}
						else if (num3 > 0f)
						{
							text = "\\";
						}
						else
						{
							text = "/";
						}
					}
					else
					{
						text = "-";
					}
					ScreenBuffer scrapBuffer2 = ScreenBuffer.GetScrapBuffer2(false);
					string text2;
					if (!CheckLoadAmmoEvent.Check(this.ParentObject, gameObject, out text2, flag2))
					{
						if (!text2.IsNullOrEmpty() && gameObject.IsPlayer())
						{
							base.EmitMessage(text2, 'r', false, flag3, false, null, null);
						}
						if (Attacker != null && Attacker.Brain != null && this.ParentObject.FireEvent("ReloadPossible"))
						{
							Attacker.Brain.NeedToReload = true;
						}
						return false;
					}
					int num5 = 0;
					List<GameObject> list = new List<GameObject>(this.ShotsPerAction);
					List<Projectile> list2 = new List<Projectile>(this.ShotsPerAction);
					GameObject gameObject4 = null;
					int i = 0;
					while (i < this.AmmoPerAction)
					{
						GameObject gameObject5;
						GameObject gameObject6;
						string text3;
						if (!LoadAmmoEvent.Check(this.ParentObject, gameObject, out gameObject5, out gameObject6, out text3, flag2))
						{
							if (!text3.IsNullOrEmpty() && gameObject.IsPlayer())
							{
								base.EmitMessage(text3, 'r', false, flag3, false, null, null);
								break;
							}
							break;
						}
						else
						{
							if (GameObject.Validate(ref gameObject6))
							{
								gameObject2 = gameObject6;
							}
							num5++;
							if (GameObject.Validate(ref gameObject5))
							{
								list.Add(gameObject5);
								list2.Add(gameObject5.GetPart<Projectile>());
								if (gameObject4 == null)
								{
									gameObject4 = gameObject5;
								}
							}
							i++;
						}
					}
					for (int j = this.AmmoPerAction; j < this.ShotsPerAction; j++)
					{
						int num6 = j - this.AmmoPerAction;
						if (list.Count < num6)
						{
							num6 = 0;
						}
						if (list.Count > num6)
						{
							GameObject gameObject7 = list[num6].DeepCopy(false, false, null);
							if (GameObject.Validate(ref gameObject7))
							{
								list.Add(gameObject7);
								list2.Add(gameObject7.GetPart<Projectile>());
							}
						}
					}
					for (int k = list.Count - 1; k >= 0; k--)
					{
						MissileWeapon.SetupProjectile(list[k], Attacker, this.ParentObject, list2[k]);
					}
					if (E.HasFlag("ShowEmitMessage") && list.Count > 0)
					{
						GameObject gameObject8 = ((this.ParentObject.CurrentCell == null) ? gameObject : this.ParentObject);
						if (IComponent<GameObject>.Visible(gameObject8))
						{
							if (list.Count == 1)
							{
								string text4 = "emit";
								GameObject gameObject9 = list[0];
								string text5 = null;
								string text6 = "!";
								string text7 = null;
								string text8 = null;
								GameObject gameObject10 = null;
								GameObject gameObject11 = null;
								bool flag5 = false;
								bool flag6 = false;
								bool flag7 = true;
								bool flag8 = false;
								bool flag9 = false;
								GameObject gameObject12 = null;
								GameObject gameObject13 = null;
								GameObject gameObject14 = null;
								bool flag10 = false;
								bool flag11 = false;
								bool flag12 = false;
								bool flag13 = false;
								GameObject gameObject15 = gameObject8;
								base.DidXToY(text4, gameObject9, text5, text6, text7, text8, gameObject10, gameObject11, flag5, flag6, flag7, flag8, flag9, gameObject12, gameObject13, gameObject14, flag10, flag11, flag12, flag13, flag3, gameObject15);
							}
							else
							{
								string[] array = new string[list.Count];
								bool flag14 = true;
								int l = 0;
								int count = list.Count;
								while (l < count)
								{
									array[l] = list[l].ShortDisplayName;
									if (flag14 && l > 0 && array[l] != array[0])
									{
										flag14 = false;
									}
									l++;
								}
								if (flag14)
								{
									string text9 = "emit";
									string text10 = list.Count.Things(array[0], null);
									string text11 = "!";
									string text12 = null;
									string text13 = null;
									GameObject gameObject16 = null;
									GameObject gameObject17 = null;
									bool flag15 = false;
									bool flag16 = false;
									GameObject gameObject18 = null;
									GameObject gameObject19 = null;
									bool flag17 = false;
									bool flag18 = false;
									bool flag19 = false;
									bool flag20 = false;
									GameObject gameObject15 = gameObject8;
									base.DidX(text9, text10, text11, text12, text13, gameObject16, gameObject17, flag15, flag16, gameObject18, gameObject19, flag17, flag18, flag19, flag20, flag3, gameObject15);
								}
								else
								{
									int m = 0;
									int num7 = array.Length;
									while (m < num7)
									{
										array[m] = Grammar.A(array[m], false);
										m++;
									}
									string text14 = "emit";
									string text15 = Grammar.MakeAndList(array, true);
									string text16 = "!";
									string text17 = null;
									string text18 = null;
									GameObject gameObject20 = null;
									GameObject gameObject21 = null;
									bool flag21 = false;
									bool flag22 = false;
									GameObject gameObject22 = null;
									GameObject gameObject23 = null;
									bool flag23 = false;
									bool flag24 = false;
									bool flag25 = false;
									bool flag26 = false;
									GameObject gameObject15 = gameObject8;
									base.DidX(text14, text15, text16, text17, text18, gameObject20, gameObject21, flag21, flag22, gameObject22, gameObject23, flag23, flag24, flag25, flag26, flag3, gameObject15);
								}
							}
						}
					}
					if (num5 == 0)
					{
						if (Attacker != null && Attacker.Brain != null && this.ParentObject.FireEvent("ReloadPossible"))
						{
							Attacker.Brain.NeedToReload = true;
						}
						return false;
					}
					if (flag4)
					{
						if (Attacker.IsPlayer())
						{
							base.EmitMessage("Your shot goes wild!", 'R', false, flag3, false, null, null);
						}
						else if (IComponent<GameObject>.Visible(Attacker))
						{
							base.EmitMessage(Attacker.Poss("shot", true) + " goes wild!", ColorCoding.ConsequentialColorChar(null, Attacker), false, flag3, false, null, null);
						}
					}
					int num8 = 0;
					if (num5 >= this.AmmoPerAction)
					{
						num8 = this.ShotsPerAction;
					}
					else
					{
						num8 = (int)Math.Ceiling((double)((float)this.ShotsPerAction * ((float)num5 / (float)this.AmmoPerAction)));
					}
					if (num8 > 0)
					{
						this.ParentObject.FireEvent("WeaponMissleWeaponFiring");
					}
					GameObject parentObject = this.ParentObject;
					string text19 = ((parentObject != null) ? parentObject.GetTag("MissileFireSound", null) : null);
					Event @event = Event.New("QueryMissileFireSound", 0, 0, 0);
					@event.SetParameter("Weapon", this.ParentObject);
					@event.SetParameter("Sound", text19);
					@event.SetParameter("Ammo", gameObject2);
					@event.SetParameter("Attacker", Attacker);
					GameObject parentObject2 = this.ParentObject;
					if (parentObject2 != null)
					{
						parentObject2.FireEvent(@event);
					}
					if (gameObject2 != null)
					{
						gameObject2.FireEvent(@event);
					}
					text19 = @event.GetStringParameter("Sound", null);
					if (gameObject != null)
					{
						gameObject.PlayWorldSound(text19, 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
					}
					if (fireType == FireType.SuppressingFire)
					{
						if (gameObject != null)
						{
							gameObject.PlayWorldSound("sfx_ability_weaponSkill_suppressiveFire", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (fireType == FireType.FlatteningFire)
					{
						if (gameObject != null)
						{
							gameObject.PlayWorldSound("sfx_ability_weaponSkill_suppressiveFire_upgraded", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (fireType == FireType.SureFire)
					{
						if (gameObject != null)
						{
							gameObject.PlayWorldSound("sfx_ability_weaponSkill_sureFire", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (fireType == FireType.BeaconFire)
					{
						if (gameObject != null)
						{
							gameObject.PlayWorldSound("sfx_ability_weaponSkill_sureFire_upgraded", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (fireType == FireType.WoundingFire)
					{
						if (gameObject != null)
						{
							gameObject.PlayWorldSound("sfx_ability_weaponSkill_woundingFire", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (fireType == FireType.DisorientingFire)
					{
						if (gameObject != null)
						{
							gameObject.PlayWorldSound("sfx_ability_weaponSkill_woundingFire_upgraded", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
						}
					}
					else if (fireType == FireType.OneShot && gameObject != null)
					{
						gameObject.PlayWorldSound("sfx_ability_weaponSkill_ultraFire", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
					}
					GameObject gameObject24 = E.GetGameObjectParameter("AimedAt") ?? cell.GetCombatTarget(Attacker, true, false, false, 5, gameObject4, null, null, gameObject, null, true, true, null);
					if (GameObject.Validate(ref gameObject24))
					{
						if (gameObject24.IsInGraveyard())
						{
							gameObject24 = null;
						}
						else if (gameObject24.IsPlayer())
						{
							if (AutoAct.IsActive(false) && IComponent<GameObject>.Visible(Attacker))
							{
								AutoAct.Interrupt("something is shooting at you", null, Attacker, true);
							}
						}
						else if (!gameObject24.IsHostileTowards(gameObject))
						{
							gameObject24.AddOpinion<OpinionFriendlyFire>(gameObject, 1f);
						}
					}
					else
					{
						gameObject24 = null;
					}
					GameObject gameObject25 = gameObject24;
					if (gameObject.IsPlayer())
					{
						MissilePath missilePath2 = missilePath;
						GameObject gameObject15 = Attacker;
						foreach (Point point in MissileWeapon.CalculateBulletTrajectory(missilePath2, gameObject4, null, gameObject15, null, null, 0, 0, true))
						{
							Cell cell4 = parentZone.GetCell(point.X, point.Y);
							if (cell4 != Attacker.CurrentCell)
							{
								GameObject combatTarget = cell4.GetCombatTarget(Attacker, true, false, false, 5, gameObject4, null, null, null, null, true, true, null);
								if (combatTarget != null && combatTarget != gameObject)
								{
									if (combatTarget == gameObject25 || combatTarget.IsLedBy(gameObject))
									{
										break;
									}
									if (gameObject25 == null)
									{
										gameObject25 = combatTarget;
									}
									if (combatTarget.Brain != null && (gameObject.PhaseMatches(combatTarget) || (gameObject4 != null && gameObject4.PhaseMatches(combatTarget))) && !combatTarget.IsHostileTowards(gameObject) && combatTarget.Brain.FriendlyFireIncident(gameObject, false))
									{
										combatTarget.AddOpinion<OpinionFriendlyFire>(gameObject, 1f);
										break;
									}
									break;
								}
							}
						}
					}
					List<List<Point>> list3 = new List<List<Point>>();
					int num9 = -gameObject.StatMod(this.Modifier, 0);
					if (this.IsSkilled(Attacker))
					{
						num9 -= 2;
					}
					bool flag27 = E.HasFlag("TargetUnset");
					if (gameObject.IsPlayer() && !this.ParentObject.HasTagOrProperty("NoMissileSetTarget"))
					{
						if (gameObject24 != null && Sidebar.CurrentTarget != gameObject24)
						{
							if (!flag27 && !gameObject24.IsCreature)
							{
								GameObject currentTarget = Sidebar.CurrentTarget;
								if (currentTarget != null && currentTarget.IsCreature)
								{
									goto IL_0BDC;
								}
							}
							if (gameObject24.IsVisible())
							{
								Sidebar.CurrentTarget = gameObject24;
								goto IL_0C04;
							}
						}
						IL_0BDC:
						if (flag27 && gameObject25 != null && gameObject25 != gameObject24 && gameObject25.IsHostileTowards(gameObject) && gameObject25.IsVisible())
						{
							Sidebar.CurrentTarget = gameObject25;
						}
					}
					IL_0C04:
					if (gameObject24 != null)
					{
						num9 += gameObject24.GetIntProperty("IncomingAimModifier", 0);
						if (Rifle_DrawABead.IsCompatibleMissileWeapon(this.Skill) && gameObject24.HasEffect<RifleMark>((RifleMark fx) => fx.Marker == Attacker))
						{
							num9--;
						}
					}
					num9 -= intParameter;
					num9 -= this.AimVarianceBonus;
					num9 -= Attacker.GetIntProperty("MissileWeaponAccuracyBonus", 0);
					num9 -= this.ParentObject.GetIntProperty("MissileWeaponAccuracyBonus", 0);
					MissileWeapon.eModifyAimVariance.SetParameter("Amount", 0);
					Attacker.FireEvent(MissileWeapon.eModifyAimVariance);
					this.ParentObject.FireEvent(MissileWeapon.eModifyAimVariance);
					num9 += MissileWeapon.eModifyAimVariance.GetIntParameter("Amount", 0);
					if (gameObject24 != null && gameObject24.HasRegisteredEvent("ModifyIncomingAimVariance"))
					{
						MissileWeapon.eModifyIncomingAimVariance.SetParameter("Amount", 0);
						gameObject24.FireEvent(MissileWeapon.eModifyIncomingAimVariance);
						num9 += MissileWeapon.eModifyIncomingAimVariance.GetIntParameter("Amount", 0);
					}
					int num10 = MissileWeapon.VarianceDieRoll.Resolve();
					int num11 = Math.Abs(num10 - 21) + num9;
					if (fireType == FireType.SureFire || fireType == FireType.BeaconFire || (fireType == FireType.OneShot && Attacker.HasSkill("Rifle_SureFire")))
					{
						num11 = 0;
					}
					if (num11 < 0)
					{
						num11 = 0;
					}
					if (num10 < 25)
					{
						num11 = -num11;
					}
					num11 += intParameter2;
					if (Attacker.HasEffect<Running>() && (this.Skill != "Pistol" || !Attacker.HasSkill("Pistol_SlingAndRun")) && !Attacker.HasProperty("EnhancedSprint"))
					{
						num11 += Stat.Random(-23, 23);
					}
					if (flag4)
					{
						num11 += Stat.Random(-23, 23);
					}
					bool flag28 = false;
					int num12 = 0;
					int num13 = 0;
					if (num8 > 1)
					{
						flag28 = GetFixedMissileSpreadEvent.GetFor(this.ParentObject, out num12);
						if (flag28)
						{
							num13 = Stat.Random(-this.WeaponAccuracy, this.WeaponAccuracy);
						}
					}
					List<bool> list4 = new List<bool>(num8);
					List<bool> list5 = new List<bool>(num8);
					List<bool> list6 = new List<bool>(num8);
					List<Cell> list7 = new List<Cell>(num8);
					for (int n = 0; n < num8; n++)
					{
						int num14 = intParameter2;
						int num15 = num11;
						int num16;
						if (flag28)
						{
							num16 = num13;
							int num17 = -num12 / 2 + num12 * n / (num8 - 1);
							num14 += num17;
							num15 += num17;
						}
						else
						{
							num16 = Stat.Random(-this.WeaponAccuracy, this.WeaponAccuracy);
						}
						Event event2 = Event.New("WeaponMissileWeaponShot", 0, 0, 0);
						event2.SetParameter("AimVariance", num15);
						event2.SetParameter("FlatVariance", num14);
						event2.SetParameter("WeaponAccuracy", num16);
						this.ParentObject.FireEvent(event2);
						GameObject gameObject26 = ((list.Count > n) ? list[n] : null);
						if (gameObject26 == null)
						{
							MetricsManager.LogError("had no projectile for shot " + n.ToString() + " from " + this.ParentObject.DebugName);
						}
						else
						{
							bool flag29;
							bool flag30;
							Cell cell5;
							List<Point> list8 = MissileWeapon.CalculateBulletTrajectory(out flag29, out flag30, out cell5, missilePath, gameObject26, this.ParentObject, Attacker, Attacker.CurrentZone, event2.GetIntParameter("AimVariance", 0).ToString(), event2.GetIntParameter("FlatVariance", 0), event2.GetIntParameter("WeaponAccuracy", 0), false);
							list3.Add(list8);
							list4.Add(false);
							list5.Add(flag29);
							list6.Add(flag30);
							list7.Add(cell5);
						}
					}
					if (flag)
					{
						scrapBuffer2.Copy(scrapBuffer);
					}
					int num18 = Math.Min(num8, this.ShotsPerAnimation);
					The.Player.GetCurrentCell();
					Event event3 = Event.New("ProjectileEntering", "Attacker", Attacker);
					Event event4 = Event.New("ProjectileEnteringCell", "Attacker", Attacker);
					List<GameObject> objectsThatWantEvent = cell.ParentZone.GetObjectsThatWantEvent(PooledEvent<ProjectileMovingEvent>.ID, ProjectileMovingEvent.CascadeLevel);
					ProjectileMovingEvent projectileMovingEvent = null;
					if (objectsThatWantEvent.Count > 0)
					{
						projectileMovingEvent = PooledEvent<ProjectileMovingEvent>.FromPool();
						projectileMovingEvent.Attacker = Attacker;
						projectileMovingEvent.Launcher = this.ParentObject;
						projectileMovingEvent.TargetCell = cell;
						projectileMovingEvent.ApparentTarget = gameObject25;
						projectileMovingEvent.ScreenBuffer = scrapBuffer2;
					}
					GameObject gameObjectParameter = E.GetGameObjectParameter("MessageAsFrom");
					MissileWeaponVFXConfiguration missileWeaponVFXConfiguration = null;
					for (int num19 = 0; num19 < list.Count; num19 += num18)
					{
						int num20 = 0;
						bool flag31 = false;
						int num21 = num19;
						while (num21 < num19 + num18 && num21 < list3.Count)
						{
							if (list3[num21].Count > num20)
							{
								num20 = list3[num21].Count;
							}
							num21++;
						}
						int num22 = this.AnimationDelay - num20 / 5;
						if (num22 > 0 && E.HasParameter("AnimationDelayMultiplier"))
						{
							num22 = (int)((float)num22 * E.GetParameter<float>("AnimationDelayMultiplier"));
						}
						int num23 = cell2.X - cell.X;
						int num24 = cell2.Y - cell.Y;
						int num25 = (int)Math.Sqrt((double)(num23 * num23 + num24 * num24)) / this.RangeIncrement;
						int num26 = ((this.VariableMaxRange != null) ? Math.Min(this.VariableMaxRange.RollCached(), this.MaxRange) : this.MaxRange);
						bool flag32 = false;
						int num27 = num19;
						while (num27 < list.Count && num27 < list.Count + num18)
						{
							GameObjectBlueprint blueprint = list[num27].GetBlueprint(true);
							bool flag33 = false;
							Dictionary<string, string> dictionary;
							if (list[num27].HasStringProperty("ProjectileVFX") || blueprint.HasTag("ProjectileVFX", false))
							{
								if (missileWeaponVFXConfiguration == null)
								{
									missileWeaponVFXConfiguration = MissileWeaponVFXConfiguration.next();
									CombatJuiceManager.startDelay();
								}
								missileWeaponVFXConfiguration.addStep(num27, list3[num27][0].location);
								missileWeaponVFXConfiguration.setPathProjectileVFX(num27, list[num27].GetPropertyOrTag("ProjectileVFX", null), list[num27].GetPropertyOrTag("ProjectileVFXConfiguration", null));
								flag33 = true;
							}
							else if (blueprint.xTags != null && blueprint.xTags.TryGetValue("ProjectileVFX", out dictionary))
							{
								if (missileWeaponVFXConfiguration == null)
								{
									missileWeaponVFXConfiguration = MissileWeaponVFXConfiguration.next();
									CombatJuiceManager.startDelay();
								}
								missileWeaponVFXConfiguration.addStep(num27, list3[num27][0].location);
								missileWeaponVFXConfiguration.setPathProjectileVFX(num27, dictionary);
								missileWeaponVFXConfiguration.SetPathProjectileRender(num27, list[num27]);
								flag33 = true;
							}
							if (flag33)
							{
								ConfigureMissileVisualEffectEvent.Send(missileWeaponVFXConfiguration, missileWeaponVFXConfiguration.GetPath(num27), Attacker, this.ParentObject, list[num27]);
							}
							num27++;
						}
						int num28 = 1;
						while (num28 < num20 && num28 <= num26)
						{
							if (flag && this.AmmoChar != "f" && this.AmmoChar != "m" && this.AmmoChar != "e")
							{
								scrapBuffer2.Copy(scrapBuffer);
							}
							bool flag34 = true;
							int num29 = num19;
							while (num29 < num19 + num18 && num29 < list4.Count)
							{
								if (!list4[num29])
								{
									flag34 = false;
									break;
								}
								num29++;
							}
							if (flag34)
							{
								break;
							}
							int num30 = num19;
							while (num30 < num19 + num18 && num30 < list3.Count)
							{
								List<Point> list9 = list3[num30];
								if (num28 >= list9.Count)
								{
									list4[num30] = true;
								}
								if (!list4[num30])
								{
									Projectile projectile = list2[num30];
									GameObject gameObject27 = list[num30];
									Cell cell6 = parentZone.GetCell(list9[num28 - 1].X, list9[num28 - 1].Y);
									Cell cell7 = parentZone.GetCell(list9[num28].X, list9[num28].Y);
									if (cell7 != null && missileWeaponVFXConfiguration != null)
									{
										missileWeaponVFXConfiguration.addStep(num30, list9[num28].location);
									}
									if (flag && cell7 != null && cell7.IsVisible() && missileWeaponVFXConfiguration == null)
									{
										flag31 = true;
									}
									if (flag31)
									{
										string text20 = projectile.RenderChar ?? this.AmmoChar;
										scrapBuffer2.Goto(list9[num28].X, list9[num28].Y);
										if (text20 == "sm")
										{
											scrapBuffer2.Goto(list9[num28].X, list9[num28].Y);
											int num31 = Stat.Random(1, 3);
											string text21 = "+";
											if (num31 == 1)
											{
												text21 = "&R*";
											}
											if (num31 == 2)
											{
												text21 = "&W*";
											}
											if (num31 == 3)
											{
												text21 = "&Y*";
											}
											scrapBuffer2.Write(text21, true, false, false, null, -1);
										}
										else if (text20 == "e")
										{
											float num32 = (float)Stat.Random(85, 185) / 58f;
											float num33 = (float)Math.Sin((double)num32) / 6f;
											float num34 = (float)Math.Cos((double)num32) / 6f;
											int num35 = Stat.Random(1, 3);
											string text22 = ((char)Stat.Random(191, 198)).ToString() ?? "";
											if (num35 == 1)
											{
												text22 = "&Y" + text22;
											}
											if (num35 == 2)
											{
												text22 = "&W*" + text22;
											}
											if (num35 == 3)
											{
												text22 = "&C*" + text22;
											}
											XRLCore.ParticleManager.Add(text22, (float)list9[num28].X + num33 * 2f, (float)list9[num28].Y + num34 * 2f, num33, num34, 2);
											XRLCore.ParticleManager.Frame();
											XRLCore.ParticleManager.Render(scrapBuffer2);
											scrapBuffer2.Goto(list9[num28].X, list9[num28].Y);
											if (num35 == 1)
											{
												text22 = "&Y" + text22;
											}
											if (num35 == 2)
											{
												text22 = "&W*" + text22;
											}
											if (num35 == 3)
											{
												text22 = "&C*" + text22;
											}
											scrapBuffer2.Write(text22, true, false, false, null, -1);
										}
										else if (text20.Contains("-"))
										{
											scrapBuffer2.Write(text20.Replace("-", text) ?? "", true, false, false, null, -1);
										}
										else if (text20 == "m")
										{
											float num36 = (float)Stat.Random(85, 185) / 58f;
											float num37 = (float)Math.Sin((double)num36) / 6f;
											float num38 = (float)Math.Cos((double)num36) / 6f;
											int num39 = Stat.Random(1, 3);
											string text23 = "";
											if (num39 == 1)
											{
												text23 = "°";
											}
											else if (num39 == 2)
											{
												text23 = "±";
											}
											else if (num39 == 3)
											{
												text23 = "²";
											}
											XRLCore.ParticleManager.Add(text23, (float)list9[num28].X, (float)list9[num28].Y, num37, num38);
											XRLCore.ParticleManager.Frame();
											XRLCore.ParticleManager.Render(scrapBuffer2);
											scrapBuffer2.Goto(list9[num28].X, list9[num28].Y);
											if (num39 == 1)
											{
												text23 = "&R*";
											}
											else if (num39 == 2)
											{
												text23 = "&W*";
											}
											else if (num39 == 3)
											{
												text23 = "&Y*";
											}
											scrapBuffer2.Write(text23, true, false, false, null, -1);
										}
										else if (text20.StartsWith("GG"))
										{
											string text24 = null;
											string text25 = text20.Substring(2);
											int num40 = 1;
											int num41 = Math.Min(list9.Count - 1, num28 - 1);
											while (num40 <= num41)
											{
												int x = list9[num40].X;
												int y = list9[num40].Y;
												if (num40 == num41)
												{
													text24 = "X";
												}
												else
												{
													int x2 = list9[num40 - 1].X;
													int y2 = list9[num40 - 1].Y;
													int x3 = list9[num40 + 1].X;
													int y3 = list9[num40 + 1].Y;
													if (y == y3 && y == y2)
													{
														text24 = "-";
													}
													else if (x == x3 && x == x2)
													{
														text24 = "|";
													}
													else if ((x == x3 && x != x2 && y != y3 && y == x2) || (x != x3 && x == x2 && y == y3 && y != x2))
													{
														text24 = null;
													}
													else if (y3 > y2)
													{
														text24 = ((x3 > x2) ? "\\" : "/");
													}
													else if (y3 < y2)
													{
														text24 = ((x3 > x2) ? "/" : "\\");
													}
												}
												if (!text24.IsNullOrEmpty())
												{
													if (!text25.IsNullOrEmpty())
													{
														text24 = string.Concat(new string[] { "{{", text25, "|", text24, "}}" });
													}
													scrapBuffer2.WriteAt(x, y, text24, true);
													scrapBuffer2.Draw();
													XRLCore.ParticleManager.Frame();
												}
												num40++;
											}
										}
										else if (text20 == "HR")
										{
											for (int num42 = 1; num42 < list9.Count; num42++)
											{
												if (num42 >= num28)
												{
													break;
												}
												scrapBuffer2.Goto(list9[num42].X, list9[num42].Y);
												string text26 = "&b";
												int num43 = Stat.Random(1, 3);
												if (num43 == 1)
												{
													text26 = "&r";
												}
												else if (num43 == 2)
												{
													text26 = "&b";
												}
												else if (num43 == 3)
												{
													text26 = "&r";
												}
												num43 = Stat.Random(1, 3);
												if (num43 == 1)
												{
													text26 += "^b";
												}
												else if (num43 == 2)
												{
													text26 += "^r";
												}
												else if (num43 == 3)
												{
													text26 += "^b";
												}
												num43 = Stat.Random(1, 3);
												scrapBuffer2.Write(text26 + " ", true, false, false, null, -1);
											}
										}
										else if (text20 == "FR")
										{
											for (int num44 = 1; num44 < list9.Count; num44++)
											{
												if (num44 >= num28)
												{
													break;
												}
												scrapBuffer2.Goto(list9[num44].X, list9[num44].Y);
												string text27 = "&C";
												int num45 = Stat.Random(1, 3);
												if (num45 == 1)
												{
													text27 = "&C";
												}
												else if (num45 == 2)
												{
													text27 = "&B";
												}
												else if (num45 == 3)
												{
													text27 = "&Y";
												}
												num45 = Stat.Random(1, 3);
												if (num45 == 1)
												{
													text27 += "^C";
												}
												else if (num45 == 2)
												{
													text27 += "^B";
												}
												else if (num45 == 3)
												{
													text27 += "^Y";
												}
												num45 = Stat.Random(1, 3);
												scrapBuffer2.Write(text27 + ((char)(219 + Stat.Random(0, 4))).ToString(), true, false, false, null, -1);
											}
										}
										else if (text20 == "f")
										{
											for (int num46 = 1; num46 < list9.Count; num46++)
											{
												if (num46 >= num28)
												{
													break;
												}
												Cell cell8 = ((parentZone != null) ? parentZone.GetCell(list9[num46].X, list9[num46].Y) : null);
												if (cell8 != null)
												{
													cell8.Flameburst(null);
												}
												else
												{
													scrapBuffer2.Goto(list9[num46].X, list9[num46].Y);
													string text28 = "&R";
													int num47 = Stat.Random(1, 3);
													if (num47 == 1)
													{
														text28 = "&R";
													}
													else if (num47 == 2)
													{
														text28 = "&W";
													}
													else if (num47 == 3)
													{
														text28 = "&Y";
													}
													num47 = Stat.Random(1, 3);
													if (num47 == 1)
													{
														text28 += "^R";
													}
													else if (num47 == 2)
													{
														text28 += "^W";
													}
													else if (num47 == 3)
													{
														text28 += "^Y";
													}
													num47 = Stat.Random(1, 3);
													scrapBuffer2.Write(text28 + ((char)(219 + Stat.Random(0, 4))).ToString(), true, false, false, null, -1);
												}
											}
										}
										else
										{
											scrapBuffer2.Write(text20 ?? "", true, false, false, null, -1);
										}
									}
									GetMissileWeaponPerformanceEvent @for = GetMissileWeaponPerformanceEvent.GetFor(gameObject, this.ParentObject, gameObject27, null, null, null, null, null, null, null, null, null, null, false);
									int num48 = 0;
									GameObject gameObject28;
									bool flag35;
									bool flag36;
									GameObject gameObject29;
									bool flag42;
									for (;;)
									{
										Cell cell9 = cell7;
										GameObject attacker = Attacker;
										GameObject gameObject15 = gameObject27;
										bool flag37;
										bool flag38;
										cell9.FindSolidObjectForMissile(attacker, this.ParentObject, gameObject15, out gameObject28, out flag35, out flag36, out flag37, out flag38, new bool?(@for.PenetrateCreatures), new bool?(@for.PenetrateWalls), gameObject25);
										if (!flag37 || ++num48 >= 100)
										{
											Cell cell10 = cell7;
											GameObject attacker2 = Attacker;
											bool flag39 = true;
											bool flag40 = false;
											bool flag41 = false;
											int num49 = 0;
											gameObject15 = this.ParentObject;
											gameObject29 = cell10.GetCombatTarget(attacker2, flag39, flag40, flag41, num49, gameObject27, gameObject15, gameObject27, null, null, true, true, null);
											if (gameObject29 == null)
											{
												break;
											}
											GameObject gameObject30 = gameObject27;
											gameObject15 = this.ParentObject;
											GameObject attacker3 = Attacker;
											GameObject gameObject31 = gameObject29;
											GameObject gameObject32 = gameObject25;
											bool flag43;
											bool flag44;
											flag42 = BeforeProjectileHitEvent.Check(gameObject30, attacker3, gameObject31, out flag43, out flag44, @for.PenetrateCreatures, @for.PenetrateWalls, gameObject15, gameObject32, null, null, false, false);
											if (!flag43 || ++num48 >= 100)
											{
												goto IL_1DB6;
											}
										}
									}
									IL_1DBD:
									bool flag45 = false;
									bool flag46 = false;
									if (!flag35)
									{
										cell7.WakeCreaturesInArea();
										event3.SetParameter("Cell", cell7);
										event3.SetParameter("Path", missilePath);
										event3.SetParameter("p", num28);
										gameObject27.FireEvent(event3);
										event4.SetParameter("Projectile", gameObject27);
										event4.SetParameter("Cell", cell7);
										event4.SetParameter("Path", missilePath);
										event4.SetParameter("p", num28);
										if (!cell7.FireEvent(event4))
										{
											flag45 = true;
										}
										else if (!MissileTraversingCellEvent.Check(gameObject27, cell7, cell6.GetDirectionFromCell(cell7, false), Attacker, false))
										{
											flag45 = true;
										}
										else if (projectileMovingEvent != null)
										{
											projectileMovingEvent.Projectile = gameObject27;
											projectileMovingEvent.Defender = gameObject29;
											projectileMovingEvent.Cell = cell7;
											projectileMovingEvent.Path = list9;
											projectileMovingEvent.PathIndex = num28;
											foreach (GameObject gameObject33 in objectsThatWantEvent)
											{
												bool flag47 = gameObject33.HandleEvent(projectileMovingEvent);
												if (projectileMovingEvent.HitOverride != null)
												{
													gameObject29 = projectileMovingEvent.HitOverride;
													projectileMovingEvent.HitOverride = null;
												}
												if (projectileMovingEvent.ActivateShowUninvolved)
												{
													flag46 = true;
												}
												if (!flag47)
												{
													flag45 = true;
													break;
												}
											}
										}
									}
									if (!flag45 && !GameObject.Validate(ref gameObject27))
									{
										flag45 = true;
									}
									bool flag48 = false;
									if (gameObject29 != null && (!flag4 || cell7.DistanceTo(gameObject) >= 2))
									{
										if (AutoAct.IsActive(false))
										{
											if (gameObject29.IsPlayerControlledAndPerceptible() && !gameObject29.IsTrifling && !Attacker.IsPlayerControlled())
											{
												AutoAct.Interrupt(string.Concat(new string[]
												{
													"you ",
													gameObject29.GetPerceptionVerb(),
													" something shooting at ",
													gameObject29.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
													gameObject29.IsVisible() ? "" : (" " + The.Player.DescribeDirectionToward(gameObject29, false, false))
												}), null, gameObject29, true);
											}
											else if (gameObject29.DistanceTo(The.Player) <= 1 && Attacker.IsHostileTowards(The.Player))
											{
												AutoAct.Interrupt("something is shooting at you or " + gameObject29.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false), null, gameObject29, true);
											}
										}
										int num50 = Stat.Random(1, 20);
										int for2 = GetToHitModifierEvent.GetFor(Attacker, gameObject29, this.ParentObject, 0, gameObject27, null, this.Skill, null, false, true, false);
										int num51 = num50 + for2;
										int num52 = Stats.GetCombatDV(gameObject29);
										Event event5 = Event.New("WeaponGetDefenderDV", 0, 0, 0);
										event5.SetParameter("Weapon", this.ParentObject);
										event5.SetParameter("Defender", gameObject29);
										event5.SetParameter("NaturalHitResult", num50);
										event5.SetParameter("Result", num51);
										event5.SetParameter("Skill", this.Skill);
										event5.SetParameter("DV", num52);
										if (gameObject29 != null)
										{
											gameObject29.FireEvent(event5);
										}
										event5.ID = "ProjectileGetDefenderDV";
										if (projectile != null)
										{
											projectile.FireEvent(event5);
										}
										num52 = event5.GetIntParameter("DV", 0);
										if (!gameObject29.HasSkill("Acrobatics_SwiftReflexes"))
										{
											num52 -= 5;
										}
										if (!gameObject29.IsMobile())
										{
											num52 = -100;
										}
										if (num51 > num52)
										{
											if (gameObject27.HasTagOrProperty("NoDodging"))
											{
												if (gameObject29.IsPlayer())
												{
													if (gameObject29.IsCombat && gameObject29.CanChangeMovementMode("Dodging", false, false, false, false))
													{
														IComponent<GameObject>.XDidYToZ(gameObject29, "attempt", "to flinch away, but", gameObject27, "is too wide", "!", null, null, null, gameObject29, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
													}
												}
												else if (gameObject29.IsVisible() && gameObject29.IsCombat && gameObject29.CanChangeMovementMode("Dodging", false, false, false, false))
												{
													IComponent<GameObject>.XDidYToZ(gameObject29, "attempt", "to flinch out of the way of", gameObject27, ", but it's too wide", "!", null, null, null, gameObject29, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
												}
											}
											if (IComponent<GameObject>.Visible(gameObject29))
											{
												flag32 = true;
											}
											flag48 = true;
											bool flag49 = false;
											bool flag50 = false;
											this.MissileHit(Attacker, gameObject29, gameObject, gameObject27, projectile, gameObject24, gameObject25, missilePath, cell7, fireType, intParameter, num50, num51, list5[num30], gameObjectParameter, flag3, ref flag45, ref flag49, ref flag50, flag27, flag46);
										}
										else if (num52 != -100 && gameObject29.InActiveZone() && !gameObject27.HasTagOrProperty("NoDodging"))
										{
											string passByVerb = projectile.PassByVerb;
											gameObject29.ParticleBlip("&K\t", 10, 0L, false);
											if (gameObject29.IsPlayer())
											{
												gameObject29.PlayWorldSound("sfx_missile_generic_flinched", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
												if (!passByVerb.IsNullOrEmpty())
												{
													if (gameObject29.IsCombat && gameObject29.CanChangeMovementMode("Dodging", false, false, false, false))
													{
														IComponent<GameObject>.XDidYToZ(gameObject29, "flinch", "away as", gameObject27, gameObject27.GetVerb(passByVerb, false, false, false, true, false) + " past " + The.Player.DescribeDirectionFrom(gameObject, false), "!", null, null, gameObject29, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
													}
													else
													{
														IComponent<GameObject>.XDidYToZ(gameObject27, passByVerb, "past " + The.Player.DescribeDirectionFrom(gameObject, false), gameObject29, null, "!", null, null, gameObject29, null, false, true, false, false, false, null, null, null, false, false, false, false, false, null);
													}
												}
											}
											else if (gameObject29.IsVisible())
											{
												if (gameObject29.IsCombat && gameObject29.CanChangeMovementMode("Dodging", false, false, false, false))
												{
													IComponent<GameObject>.XDidYToZ(gameObject29, "flinch", "out of the way of", gameObject27, gameObject.IsPlayerControlled() ? null : The.Player.DescribeDirectionFrom(gameObject, false), "!", null, null, gameObject29, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
												}
												else if (!passByVerb.IsNullOrEmpty())
												{
													IComponent<GameObject>.XDidYToZ(gameObject27, passByVerb, "past", gameObject29, gameObject.IsPlayerControlled() ? null : The.Player.DescribeDirectionFrom(gameObject, false), "!", null, null, gameObject29, null, false, true, false, false, false, null, null, null, false, false, false, false, false, null);
												}
											}
										}
										gameObject29.StopMoving();
									}
									if (!flag35 && !flag45)
									{
										goto IL_272B;
									}
									bool flag51 = false;
									bool flag52 = false;
									if ((flag35 || flag48) && cell7 != null)
									{
										cell7.PlayWorldSound((gameObject27 != null) ? gameObject27.GetSoundTag("ImpactSound", null) : null, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
									}
									if (flag35 && !flag48)
									{
										if (gameObject27.IsValid() && cell7 != null)
										{
											cell7.AddObject(gameObject27, false, false, true, true, false, true, true, null, "Missile Transit", null, null, null, null, null, null);
										}
										if (gameObject28 != null)
										{
											int num53 = Stat.Random(1, 20) + Math.Max(0, Attacker.StatMod(this.Modifier, 0));
											if (IComponent<GameObject>.Visible(gameObject28))
											{
												flag32 = true;
											}
											this.MissileHit(Attacker, gameObject28, gameObject, gameObject27, projectile, gameObject24, gameObject25, missilePath, cell7, fireType, intParameter, num53, num53, list5[num30], gameObjectParameter, flag3, ref flag45, ref flag51, ref flag52, flag27, flag46);
										}
										else
										{
											Event event6 = Event.New("ProjectileHit", 0, 0, 0);
											event6.SetParameter("Attacker", Attacker);
											event6.SetParameter("Defender", null);
											event6.SetParameter("Skill", this.Skill);
											event6.SetParameter("Damage", null);
											event6.SetParameter("AimLevel", intParameter);
											event6.SetParameter("Owner", Attacker);
											event6.SetParameter("Launcher", this.ParentObject);
											event6.SetParameter("Path", missilePath);
											event6.SetParameter("Penetrations", 0);
											event6.SetParameter("ApparentTarget", gameObject25);
											event6.SetParameter("AimedAt", gameObject24);
											event6.SetFlag("Critical", false);
											gameObject27.FireEvent(event6);
											event6.ID = "DefenderProjectileHit";
											if (gameObject29 != null)
											{
												gameObject29.FireEvent(event6);
											}
											event6.ID = "LauncherProjectileHit";
											this.ParentObject.FireEvent(event6);
										}
									}
									bool flag53 = !flag45 && ((flag35 && flag52) || (flag36 && flag51 && gameObject28 != null && gameObject28.IsOrganic));
									if (!flag53)
									{
										list4[num30] = true;
									}
									if (flag35 && !flag53)
									{
										if (gameObject27.IsValid() && cell6 != null)
										{
											cell6.AddObject(gameObject27, false, false, true, true, false, true, true, null, "Missile Transit", null, null, null, null, null, E);
										}
									}
									else if (flag45 && gameObject27.IsValid() && cell7 != null)
									{
										cell7.AddObject(gameObject27, false, false, true, true, false, true, true, null, "Missile Transit", null, null, null, null, null, E);
									}
									if (!flag53 && gameObject27 != null && gameObject27.IsValid())
									{
										gameObject27.WasThrown(Attacker, gameObject25);
										MissileWeapon.CleanupProjectile(gameObject27);
										goto IL_272B;
									}
									goto IL_272B;
									IL_1DB6:
									if (!flag42)
									{
										gameObject29 = null;
										goto IL_1DBD;
									}
									goto IL_1DBD;
								}
								IL_272B:
								num30++;
							}
							if (flag31)
							{
								XRLCore._Console.DrawBuffer(scrapBuffer2, null, false);
								if (num22 > 0)
								{
									Thread.Sleep(num22);
								}
							}
							num28++;
						}
						if (!flag32 && list6.Count > num19 && list6[num19] && list5.Count > num19 && !list5[num19])
						{
							GameObject gameObject34 = list[num19];
							Projectile projectile2 = list2[num19];
							if (GameObject.Validate(ref gameObject34) && !projectile2.PassByVerb.IsNullOrEmpty())
							{
								IComponent<GameObject>.EmitMessage(The.Player, gameObject34.Does(projectile2.PassByVerb, int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false) + " past " + The.Player.DescribeDirectionFrom(gameObject, false) + ".", ' ', false, false, false, null, null);
							}
							if (!Attacker.IsPlayerLed())
							{
								AutoAct.Interrupt(null, list7[num19], null, true);
							}
						}
					}
					for (int num54 = list.Count - 1; num54 >= 0; num54--)
					{
						GameObject gameObject35 = list[num54];
						if (GameObject.Validate(ref gameObject35))
						{
							gameObject35.Obliterate(null, false, null);
						}
					}
					CombatJuiceManager.endDelay();
					if (missileWeaponVFXConfiguration != null)
					{
						GameObject attacker4 = Attacker;
						if (((attacker4 != null) ? attacker4.CurrentZone : null) != null && Attacker.CurrentZone.IsActive())
						{
							CombatJuice.missileWeaponVFX(missileWeaponVFXConfiguration, false);
						}
					}
					float num55 = 1f;
					if (E.HasParameter("EnergyMultiplier"))
					{
						num55 = E.GetParameter<float>("EnergyMultiplier");
					}
					if (this.Skill == "Pistol" && num55 > 0f)
					{
						if (Attacker.HasEffect<EmptyTheClips>())
						{
							num55 *= 0.5f;
						}
						if (Attacker.HasSkill("Pistol_FastestGun"))
						{
							num55 *= 0.75f;
						}
						int intProperty = Attacker.GetIntProperty("PistolEnergyModifier", 0);
						if (intProperty != 0)
						{
							num55 *= (100f - (float)intProperty) / 100f;
						}
					}
					ShotCompleteEvent.Send(this.ParentObject, gameObject, gameObject2);
					int num56 = (int)((float)this.EnergyCost * num55);
					if (num56 > 0)
					{
						gameObject.UseEnergy(num56, "Combat Missile " + this.Skill, null, null, false);
					}
				}
				finally
				{
					if (MissileWeapon.CalculatedMissilePathInUse && missilePath == MissileWeapon.CalculatedMissilePath)
					{
						MissileWeapon.CalculatedMissilePath.Reset();
						MissileWeapon.CalculatedMissilePathInUse = false;
					}
					else if (MissileWeapon.SecondCalculatedMissilePathInUse && missilePath == MissileWeapon.SecondCalculatedMissilePath)
					{
						MissileWeapon.SecondCalculatedMissilePath.Reset();
						MissileWeapon.SecondCalculatedMissilePathInUse = false;
					}
					CombatJuiceManager.endDelay();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD76 RID: 44406 RVA: 0x004037B4 File Offset: 0x004019B4
		public bool ReadyToFire()
		{
			return CheckReadyToFireEvent.Check(this.ParentObject);
		}

		// Token: 0x0600AD77 RID: 44407 RVA: 0x004037C1 File Offset: 0x004019C1
		public string GetNotReadyToFireMessage()
		{
			return GetNotReadyToFireMessageEvent.GetFor(this.ParentObject);
		}

		// Token: 0x0600AD78 RID: 44408 RVA: 0x004037D0 File Offset: 0x004019D0
		public string Status(MissileWeaponArea.MissileWeaponAreaWeaponStatus modernUIStatus = null)
		{
			if (modernUIStatus != null)
			{
				modernUIStatus.renderable = this.ParentObject.RenderForUI(null, false);
				modernUIStatus.text = "";
				modernUIStatus.display = "";
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			GetMissileWeaponStatusEvent.Send(this.ParentObject, stringBuilder, modernUIStatus, null);
			string text = null;
			if (stringBuilder.Length > 0)
			{
				text = stringBuilder.ToString();
			}
			int num = 23;
			if (text != null)
			{
				num -= ConsoleLib.Console.ColorUtility.LengthExceptFormatting(text);
			}
			string text2;
			if (num > 0)
			{
				text2 = this.ParentObject.ShortDisplayNameStripped;
				if (text2.Length > num)
				{
					text2 = text2.Substring(0, num).Trim();
				}
				if (text != null)
				{
					text2 += text;
				}
			}
			else
			{
				text2 = text ?? "";
			}
			return text2;
		}

		// Token: 0x0600AD79 RID: 44409 RVA: 0x00403880 File Offset: 0x00401A80
		public static bool IsVorpal(GameObject Object)
		{
			MissilePerformance missilePerformance;
			if (Object.TryGetPart<MissilePerformance>(out missilePerformance) && !missilePerformance.AddAttributes.IsNullOrEmpty() && missilePerformance.AddAttributes.HasDelimitedSubstring(',', "Vorpal", StringComparison.Ordinal))
			{
				return true;
			}
			PoweredMissilePerformance poweredMissilePerformance;
			if (Object.TryGetPart<PoweredMissilePerformance>(out poweredMissilePerformance) && !poweredMissilePerformance.AddAttributes.IsNullOrEmpty() && poweredMissilePerformance.AddAttributes.HasDelimitedSubstring(',', "Vorpal", StringComparison.Ordinal))
			{
				return true;
			}
			GameObject gameObject = null;
			string text = null;
			GetMissileWeaponProjectileEvent.GetFor(Object, ref gameObject, ref text);
			if (gameObject != null)
			{
				Projectile part = gameObject.GetPart<Projectile>();
				if (part != null && part.HasAttribute("Vorpal"))
				{
					return true;
				}
			}
			if (text != null)
			{
				GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(text);
				if (blueprintIfExists != null && blueprintIfExists.GetPartParameter<string>("Projectile", "Attributes", "").HasDelimitedSubstring(',', "Vorpal", StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040047A1 RID: 18337
		public int AnimationDelay = 10;

		// Token: 0x040047A2 RID: 18338
		public int ShotsPerAction = 1;

		// Token: 0x040047A3 RID: 18339
		public int AmmoPerAction = 1;

		// Token: 0x040047A4 RID: 18340
		public int ShotsPerAnimation = 1;

		// Token: 0x040047A5 RID: 18341
		public int AimVarianceBonus;

		// Token: 0x040047A6 RID: 18342
		public int WeaponAccuracy;

		// Token: 0x040047A7 RID: 18343
		public int MaxRange = 999;

		// Token: 0x040047A8 RID: 18344
		public string VariableMaxRange;

		// Token: 0x040047A9 RID: 18345
		public string AmmoChar = "ù";

		// Token: 0x040047AA RID: 18346
		public bool NoWildfire;

		// Token: 0x040047AB RID: 18347
		public bool ShowShotsPerAction = true;

		// Token: 0x040047AC RID: 18348
		public bool FiresManually = true;

		// Token: 0x040047AD RID: 18349
		public string ProjectilePenetrationStat;

		// Token: 0x040047AE RID: 18350
		public string SlotType = "Missile Weapon";

		// Token: 0x040047AF RID: 18351
		public int EnergyCost = 1000;

		// Token: 0x040047B0 RID: 18352
		public int RangeIncrement = 3;

		// Token: 0x040047B1 RID: 18353
		public string Modifier = "Agility";

		// Token: 0x040047B2 RID: 18354
		public string Skill = "Rifle";

		// Token: 0x040047B3 RID: 18355
		[NonSerialized]
		private static Event eModifyAimVariance = new Event("ModifyAimVariance", "Amount", 0);

		// Token: 0x040047B4 RID: 18356
		[NonSerialized]
		private static Event eModifyIncomingAimVariance = new Event("ModifyIncomingAimVariance", "Amount", 0);

		// Token: 0x040047B5 RID: 18357
		[NonSerialized]
		private static DieRoll VarianceDieRoll = new DieRoll("2d20");

		// Token: 0x040047B6 RID: 18358
		[NonSerialized]
		private static bool LockActive = true;

		// Token: 0x040047B7 RID: 18359
		[NonSerialized]
		private static List<GameObject> LockObjectList = new List<GameObject>();

		// Token: 0x040047B8 RID: 18360
		[NonSerialized]
		private static List<ValueTuple<string, string>> MenuOptions = new List<ValueTuple<string, string>>();

		// Token: 0x040047B9 RID: 18361
		[NonSerialized]
		private static MissilePath PlayerMissilePath = new MissilePath();

		// Token: 0x040047BA RID: 18362
		[NonSerialized]
		private static MissilePath CalculatedMissilePath = new MissilePath();

		// Token: 0x040047BB RID: 18363
		[NonSerialized]
		private static bool CalculatedMissilePathInUse;

		// Token: 0x040047BC RID: 18364
		[NonSerialized]
		private static MissilePath SecondCalculatedMissilePath = new MissilePath();

		// Token: 0x040047BD RID: 18365
		[NonSerialized]
		private static bool SecondCalculatedMissilePathInUse;
	}
}
