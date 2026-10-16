using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Genkit;
using ModelShark;
using Qud.UI;
using QupKit;
using UnityEngine;
using UnityEngine.UI;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Text;

namespace XRL.UI
{
	// Token: 0x020014A7 RID: 5287
	[UIView("Looker", false, false, false, "Looker", "PickTargetFrame", false, 1, false)]
	public class Look : IWantsTextConsoleInit
	{
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x0600CF90 RID: 53136 RVA: 0x004C229F File Offset: 0x004C049F
		// (set) Token: 0x0600CF91 RID: 53137 RVA: 0x004C22A6 File Offset: 0x004C04A6
		public static bool bLocked
		{
			get
			{
				return Options.LookLocked;
			}
			set
			{
				Options.LookLocked = value;
			}
		}

		// Token: 0x0600CF92 RID: 53138 RVA: 0x004C22AE File Offset: 0x004C04AE
		public void Init(TextConsole TextConsole_, ScreenBuffer ScreenBuffer_)
		{
			Look._TextConsole = TextConsole_;
			Look._ScreenBuffer = ScreenBuffer_;
		}

		// Token: 0x0600CF93 RID: 53139 RVA: 0x004C22BC File Offset: 0x004C04BC
		public static XRL.World.GameObject GetTargetAt(int X, int Y, Zone Z, Predicate<XRL.World.GameObject> ObjectTest = null, Predicate<XRL.World.GameObject> ExtraVisibility = null)
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
			XRL.World.GameObject gameObject = null;
			if (Z.GetVisibility(X, Y))
			{
				if (ExtraVisibility != null)
				{
					gameObject = cell.GetFirstObjectWithPart("Brain", ObjectTest, (XRL.World.GameObject o) => Look.GameObjectIsVisible(o) || ExtraVisibility(o));
				}
				else
				{
					gameObject = cell.GetFirstObjectWithPart("Brain", ObjectTest, new Predicate<XRL.World.GameObject>(Look.GameObjectIsVisible));
				}
			}
			else if (ExtraVisibility != null)
			{
				gameObject = cell.GetFirstObjectWithPart("Brain", ObjectTest, ExtraVisibility);
			}
			return gameObject;
		}

		// Token: 0x0600CF94 RID: 53140 RVA: 0x004C236C File Offset: 0x004C056C
		private static bool GameObjectIsVisible(XRL.World.GameObject obj)
		{
			return obj.IsVisible();
		}

		// Token: 0x0600CF95 RID: 53141 RVA: 0x004C2374 File Offset: 0x004C0574
		public static void GetObjectListCone(int StartX, int StartY, List<XRL.World.GameObject> ObjectList, string Direction, Predicate<XRL.World.GameObject> ObjectTest = null, Predicate<XRL.World.GameObject> ExtraVisibility = null)
		{
			Zone currentZone = The.Player.CurrentZone;
			ObjectList.Clear();
			int num = 1;
			int num2 = 1;
			int num3 = 1;
			int num4 = 1;
			int num5 = 1;
			int num6 = 1;
			if (Direction == "nw")
			{
				num = -1;
				num2 = -1;
				num3 = 1;
				num4 = 0;
				num5 = 0;
				num6 = 1;
			}
			else if (Direction == "n")
			{
				num = 0;
				num2 = -1;
				num3 = -1;
				num4 = 0;
				num5 = 1;
				num6 = 0;
			}
			else if (Direction == "ne")
			{
				num = 1;
				num2 = -1;
				num3 = -1;
				num4 = 0;
				num5 = 0;
				num6 = 1;
			}
			else if (Direction == "e")
			{
				num = 1;
				num2 = 0;
				num3 = 0;
				num4 = 1;
				num5 = 0;
				num6 = -1;
			}
			else if (Direction == "se")
			{
				num = 1;
				num2 = 1;
				num3 = 0;
				num4 = -1;
				num5 = -1;
				num6 = 0;
			}
			else if (Direction == "s")
			{
				num = 0;
				num2 = 1;
				num3 = 1;
				num4 = 0;
				num5 = -1;
				num6 = 0;
			}
			else if (Direction == "sw")
			{
				num = -1;
				num2 = 1;
				num3 = 0;
				num4 = -1;
				num5 = 1;
				num6 = 0;
			}
			else if (Direction == "w")
			{
				num = -1;
				num2 = 0;
				num3 = 0;
				num4 = -1;
				num5 = 0;
				num6 = 1;
			}
			int num7 = StartX;
			int num8 = StartY;
			int num9 = 0;
			while (num7 >= 0 && num7 < currentZone.Width && num8 >= 0 && num8 < currentZone.Height)
			{
				XRL.World.GameObject gameObject = Look.GetTargetAt(num7, num8, currentZone, ObjectTest, ExtraVisibility);
				if (gameObject != null && gameObject.Render != null && gameObject.Render.Visible && !gameObject.HasProperty("HideCon"))
				{
					ObjectList.Add(gameObject);
				}
				for (int i = 0; i <= num9; i++)
				{
					gameObject = Look.GetTargetAt(num7 + num3 * i, num8 + num4 * i, currentZone, ObjectTest, ExtraVisibility);
					if (gameObject != null && gameObject.Render != null && gameObject.Render.Visible && !gameObject.HasProperty("HideCon"))
					{
						ObjectList.Add(gameObject);
					}
					gameObject = Look.GetTargetAt(num7 + num5 * i, num8 + num6 * i, currentZone, ObjectTest, ExtraVisibility);
					if (gameObject != null && gameObject.Render != null && gameObject.Render.Visible && !gameObject.HasProperty("HideCon"))
					{
						ObjectList.Add(gameObject);
					}
				}
				num9++;
				num7 += num;
				num8 += num2;
			}
		}

		/// <summary>
		///     Generate the text only block of information used for the text UI and other
		///     lookers.
		/// </summary>
		// Token: 0x0600CF96 RID: 53142 RVA: 0x004C25CC File Offset: 0x004C07CC
		public static string GenerateTooltipContent(XRL.World.GameObject O)
		{
			Look.TooltipInformation tooltipInformation = Look.GenerateTooltipInformation(O);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(tooltipInformation.DisplayName);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(tooltipInformation.LongDescription);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(tooltipInformation.SubHeader);
			stringBuilder.AppendLine(tooltipInformation.WoundLevel);
			return Markup.Transform(stringBuilder.ToString(), false);
		}

		/// <summary>
		///     Generates a list of text fields and a renderable to use for the looker tooltip.
		/// </summary>
		// Token: 0x0600CF97 RID: 53143 RVA: 0x004C2640 File Offset: 0x004C0840
		public static Look.TooltipInformation GenerateTooltipInformation(XRL.World.GameObject go)
		{
			Description part = go.GetPart<Description>();
			StringBuilder stringBuilder = new StringBuilder();
			part.GetLongDescription(stringBuilder);
			return new Look.TooltipInformation
			{
				DisplayName = go.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, false, false, false, go.HasProperName, null, false, false, false, false, null, false, null, false, true),
				FeelingText = part.GetFeelingDescription(null),
				DifficultyText = part.GetDifficultyDescription(null),
				WoundLevel = Strings.WoundLevel(go),
				LongDescription = stringBuilder.ToString(),
				IconRenderable = go.RenderForUI("Look,Tooltip", false)
			};
		}

		// Token: 0x0600CF98 RID: 53144 RVA: 0x004C26EB File Offset: 0x004C08EB
		public static void QueueItemTooltip(Vector3 screenPos, XRL.World.GameObject O, bool stayOpen, global::UnityEngine.GameObject tooltipTrigger = null)
		{
			Look.ShowItemTooltipAsync(screenPos, O, stayOpen, tooltipTrigger);
		}

		// Token: 0x0600CF99 RID: 53145 RVA: 0x004C26F8 File Offset: 0x004C08F8
		public static async void ShowItemTooltipAsync(Vector3 screenPos, XRL.World.GameObject O, bool stayOpen, global::UnityEngine.GameObject tooltipTrigger = null)
		{
			TooltipTrigger tooltip = GameManager.Instance.generalTooltip;
			if (!(tooltip == null))
			{
				if (O != null)
				{
					string text = await GameManager.Instance.gameQueue.executeAsync<string>(() => Look.GenerateTooltipContent(O), null, false);
					string contents = text;
					await The.UiContext;
					if (tooltip.IsDisplayed())
					{
						if (tooltip.remoteTrigger == tooltipTrigger)
						{
							return;
						}
						tooltip.ForceHideTooltip();
					}
					tooltip.SetText("BodyText", RTF.FormatToRTF(contents.ToString(), "FF", -1, false));
					tooltip.staysOpen = stayOpen;
					tooltip.ShowManually(true, screenPos, false, stayOpen);
					tooltip.remoteTrigger = tooltipTrigger;
					Action <>9__2;
					tooltip.onHideAction = delegate
					{
						ThreadTaskQueue gameQueue = GameManager.Instance.gameQueue;
						Action action;
						if ((action = <>9__2) == null)
						{
							action = (<>9__2 = delegate
							{
								if (XRL.World.GameObject.Validate(ref O))
								{
									O.FireEvent(XRL.World.Event.New("AfterLookedAt", "Looker", The.Player));
									XRL.World.GameObject player = The.Player;
									if (player == null)
									{
										return;
									}
									player.FireEvent(XRL.World.Event.New("LookedAt", "Object", O));
								}
							});
						}
						gameQueue.queueTask(action, 0);
					};
					contents = null;
				}
			}
		}

		// Token: 0x0600CF9A RID: 53146 RVA: 0x004C2748 File Offset: 0x004C0948
		public static async Task SetupItemTooltipAsync(XRL.World.GameObject item, TooltipTrigger trigger)
		{
			if (XRL.World.GameObject.Validate(item))
			{
				string text = await GameManager.Instance.gameQueue.executeAsync<string>(() => Look.GenerateTooltipContent(item), null, false);
				trigger.SetText("BodyText", Sidebar.FormatToRTF(text, "FF"));
				Action <>9__2;
				trigger.onHideAction = delegate
				{
					ThreadTaskQueue gameQueue = GameManager.Instance.gameQueue;
					Action action;
					if ((action = <>9__2) == null)
					{
						action = (<>9__2 = delegate
						{
							if (XRL.World.GameObject.Validate(ref item))
							{
								item.FireEvent(XRL.World.Event.New("AfterLookedAt", "Looker", The.Player));
								XRL.World.GameObject player = The.Player;
								if (player == null)
								{
									return;
								}
								player.FireEvent(XRL.World.Event.New("LookedAt", "Object", item));
							}
						});
					}
					gameQueue.queueTask(action, 0);
				};
			}
		}

		// Token: 0x0600CF9B RID: 53147 RVA: 0x004C2794 File Offset: 0x004C0994
		public static void QueueLookerTooltip(int x, int y, string mode = "rightclick", int pickObject = 0)
		{
			if (The.Player != null && The.Player.InACell())
			{
				Cell cell = The.Player.CurrentZone.GetCell(x, y);
				bool flag = cell.IsSolid();
				XRL.World.GameObject O = null;
				int num = 0;
				List<XRL.World.GameObject> list = new List<XRL.World.GameObject>(cell.Objects);
				list.Sort(Look.lookSorter);
				foreach (XRL.World.GameObject gameObject in list)
				{
					if (gameObject.HasPart<Description>() && (flag ? gameObject.CanInteractInCellWithSolid(The.Player) : gameObject.IsVisible()))
					{
						O = gameObject;
						num++;
						if (num > pickObject)
						{
							break;
						}
					}
				}
				if (O != null)
				{
					Look.TooltipInformation contents = Look.GenerateTooltipInformation(O);
					Action <>9__2;
					Action <>9__1;
					GameManager.Instance.uiQueue.queueTask(delegate
					{
						TooltipTrigger tooltipTrigger = ((mode == "looker") ? GameManager.Instance.lookerTooltip : GameManager.Instance.tileTooltip);
						foreach (ParameterizedTextField parameterizedTextField in tooltipTrigger.parameterizedTextFields)
						{
							ParameterizedTextField parameterizedTextField2;
							string name = (parameterizedTextField2 = parameterizedTextField).name;
							string text;
							if (!(name == "DisplayName"))
							{
								if (!(name == "ConText"))
								{
									if (!(name == "WoundLevel"))
									{
										if (!(name == "LongDescription"))
										{
											text = "";
										}
										else
										{
											text = contents.LongDescription.Trim();
										}
									}
									else
									{
										text = contents.WoundLevel;
									}
								}
								else
								{
									text = contents.SubHeader;
								}
							}
							else
							{
								text = contents.DisplayName;
							}
							parameterizedTextField2.value = RTF.FormatToRTF(Markup.Color("y", text), "FF", 60, false) ?? "";
						}
						tooltipTrigger.AdditionalData = contents.IconRenderable;
						if (mode == "looker")
						{
							if (!tooltipTrigger.transform.parent.gameObject.activeInHierarchy)
							{
								tooltipTrigger.transform.parent.gameObject.SetActive(true);
							}
							GameManager.Instance.SetPlayerCell(new Point2D(x, y), true);
							Vector3 cellCenter = GameManager.Instance.GetCellCenter(x, y, -10);
							tooltipTrigger.ShowManually(true, Camera.main.WorldToScreenPoint(cellCenter), true, false);
						}
						else
						{
							tooltipTrigger.ShowManually(true);
						}
						if (O != null)
						{
							TooltipTrigger tooltipTrigger2 = tooltipTrigger;
							Action action;
							if ((action = <>9__1) == null)
							{
								action = (<>9__1 = delegate
								{
									ThreadTaskQueue gameQueue = GameManager.Instance.gameQueue;
									string text2 = "LookedAt" + O.GetHashCode().ToString();
									Action action2;
									if ((action2 = <>9__2) == null)
									{
										action2 = (<>9__2 = delegate
										{
											O.FireEvent(XRL.World.Event.New("AfterLookedAt", "Looker", The.Player));
											XRL.World.GameObject player = The.Player;
											if (player == null)
											{
												return;
											}
											player.FireEvent(XRL.World.Event.New("LookedAt", "Object", O));
										});
									}
									gameQueue.queueSingletonTask(text2, action2, false);
								});
							}
							tooltipTrigger2.onHideAction = action;
						}
					}, 0);
				}
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x0600CF9C RID: 53148 RVA: 0x004C28D4 File Offset: 0x004C0AD4
		private static ScrollRect lookerScrollRect
		{
			get
			{
				ScrollRect scrollRect;
				if ((scrollRect = Look._lookerScrollRect) == null)
				{
					Tooltip tooltip = GameManager.Instance.lookerTooltip.Tooltip;
					ScrollRect scrollRect2;
					if (tooltip == null)
					{
						scrollRect2 = null;
					}
					else
					{
						global::UnityEngine.GameObject gameObject = tooltip.GameObject;
						scrollRect2 = ((gameObject != null) ? gameObject.GetComponentInChildren<ScrollRect>() : null);
					}
					scrollRect = (Look._lookerScrollRect = scrollRect2);
				}
				return scrollRect;
			}
		}

		// Token: 0x0600CF9D RID: 53149 RVA: 0x004C290C File Offset: 0x004C0B0C
		public static Cell ShowLooker(int Range, int StartX, int StartY)
		{
			PickTargetWindow.currentMode = PickTargetWindow.TargetMode.PickCells;
			GameManager.Instance.PushGameView("Looker", true);
			Look.Buffer.Copy(TextConsole.CurrentBuffer);
			Look.OldBuffer.Copy(TextConsole.CurrentBuffer);
			Cell currentCell = The.Player.CurrentCell;
			Zone zone = ((currentCell != null) ? currentCell.ParentZone : null);
			bool flag = false;
			Cell cell = null;
			if (zone != null)
			{
				int num = StartX;
				int num2 = StartY;
				int num3 = 0;
				XRL.World.GameObject gameObject = null;
				IRenderable renderable = null;
				TextBlock textBlock = null;
				int num4 = 0;
				int num5 = 0;
				string text = "";
				int num6 = 3;
				int num7 = 3;
				int num8 = 0;
				bool flag2 = true;
				Cell cell2 = null;
				while (!flag)
				{
					XRL.World.Event.ResetPool(false);
					XRLCore.Core.RenderMapToBuffer(Look.Buffer);
					XRLCore.ParticleManager.Frame();
					XRLCore.ParticleManager.Render(Look.Buffer);
					List<XRL.World.GameObject> list = XRL.World.Event.NewGameObjectList();
					List<Point> list2 = Zone.Line(currentCell.X, currentCell.Y, num, num2, true);
					Cell cell3 = null;
					if (list2.Count == 0 || list2.Count == 1)
					{
						Look.Buffer.Goto(num, num2);
						Look.Buffer.Buffer[num, num2].imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
						cell3 = zone.GetCell(num, num2);
					}
					else
					{
						for (int i = 1; i < list2.Count; i++)
						{
							Look.Buffer.Goto(list2[i].X, list2[i].Y);
							Cell cell4 = zone.GetCell(list2[i].X, list2[i].Y);
							if (i == list2.Count - 1)
							{
								ConsoleChar currentChar = Look.Buffer.CurrentChar;
								Look.Buffer.Buffer[num, num2].imposterExtra.Add("Prefabs/Imposters/TargetReticle", 0, null);
								cell3 = cell4;
							}
						}
					}
					if (cell3 != cell2)
					{
						num8 = 0;
						cell2 = cell3;
					}
					List<XRL.World.GameObject> objectsInCell = cell3.GetObjectsInCell();
					objectsInCell.Sort(Look.lookSorter);
					num8 = Math.Max(0, Math.Min(((objectsInCell != null) ? objectsInCell.Count : 0) - 1, num8));
					XRL.World.GameObject gameObject2 = null;
					Description description = null;
					int num9 = 0;
					int num10 = 0;
					foreach (XRL.World.GameObject gameObject3 in objectsInCell)
					{
						Description part = gameObject3.GetPart<Description>();
						if (part != null && gameObject3.IsVisible())
						{
							if (num9 <= num8)
							{
								gameObject2 = gameObject3;
								description = part;
								num9++;
							}
							num10++;
						}
					}
					using (TextBuilder textBuilder = TextBuilder.Get(64))
					{
						if (Options.ModernUI)
						{
							if (num10 > 1)
							{
								textBuilder.Append(string.Format("Look [{{{{C|{0}}}}}/{{{{C|{1}}}}}] | ", num9 - num10 + 2, objectsInCell.Count));
							}
							else
							{
								textBuilder.Append("Look | ");
							}
						}
						if (Look.bLocked)
						{
							textBuilder.Append(string.Concat(new string[]
							{
								"{{W|",
								ControlManager.getCommandInputFormatted("Cancel", true, false),
								"}} | {{hotkey|(",
								ControlManager.getCommandInputFormatted("CmdLockUnlock", true, false),
								")}} {{W|u}}nlock"
							}));
						}
						else
						{
							textBuilder.Append(string.Concat(new string[]
							{
								"{{W|",
								ControlManager.getCommandInputFormatted("Cancel", true, false),
								"}} | {{hotkey|(",
								ControlManager.getCommandInputFormatted("CmdLockUnlock", true, false),
								")}} {{W|l}}ock"
							}));
						}
						if (num10 > 1)
						{
							textBuilder.Append(string.Concat(new string[]
							{
								" | {{hotkey|",
								ControlManager.getCommandInputFormatted("V Positive", true, false),
								"/",
								ControlManager.getCommandInputFormatted("V Negative", true, false),
								"}} change selection"
							}));
						}
						if (gameObject2 != null)
						{
							textBuilder.Append(" | {{hotkey|" + ControlManager.getCommandInputFormatted("Accept", true, false) + "}} interact");
						}
						textBuilder.Append(" | {{hotkey|" + ControlManager.getCommandInputFormatted("CmdWalk", true, false) + "}} walk");
						if (Options.DebugInternals)
						{
							textBuilder.Append(" | {{hotkey|n}} show navweight");
						}
						string text2 = textBuilder.ToString();
						int num11 = ((num < 40) ? (79 - ConsoleLib.Console.ColorUtility.LengthExceptFormatting(text2)) : 1);
						if (Options.ModernUI)
						{
							PickTargetWindow.currentText = textBuilder.ToString();
						}
						else if (!Options.ModernUI)
						{
							Look.Buffer.WriteAt(num11, 0, text2, true);
							if (gameObject2 != null && description != null)
							{
								if (gameObject != gameObject2)
								{
									if (gameObject != null)
									{
										gameObject.FireEvent(XRL.World.Event.New("AfterLookedAt", "Looker", The.Player));
										if (The.Player != null)
										{
											The.Player.FireEvent(XRL.World.Event.New("LookedAt", "Object", gameObject));
										}
									}
									num6 = num;
									num7 = num2;
									gameObject = gameObject2;
									renderable = gameObject2.RenderForUI("Look", false);
									int num12 = ((renderable != null) ? (-2) : 0);
									text = Strings.WoundLevel(gameObject2);
									string displayName = gameObject2.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, false, false, false, gameObject2.HasProperName, null, false, false, false, false, null, false, null, false, true);
									Look.LookSB.Clear().Append(" {{Y|").Append(displayName)
										.Append("}} \n\n");
									description.GetLongDescription(Look.LookSB);
									int num13 = ConsoleLib.Console.ColorUtility.LengthExceptFormatting(displayName) + 2;
									if (renderable != null)
									{
										num13 += 4;
									}
									num4 = Math.Max(35, num13);
									int num14;
									if (num >= 40)
									{
										num14 = num - 3;
									}
									else
									{
										num14 = 80 - num - 3;
									}
									if (num4 > num14)
									{
										num4 = num14;
									}
									textBlock = new TextBlock(Look.LookSB, num4, 100, false, num12);
									while (textBlock.Lines.Count > 22 && num4 < num14)
									{
										num4 += Math.Max(2, (num4 - num14) / 2);
										if (num4 > num14)
										{
											num4 = num14;
										}
										textBlock = new TextBlock(Look.LookSB, num4, 100, false, num12);
									}
									num5 = Math.Min(textBlock.Lines.Count, 22);
									if (num >= 40)
									{
										num6 = num - num4 - 2;
									}
									else
									{
										num6++;
									}
									if (num7 >= 12)
									{
										num7 -= num5 + 2;
									}
									else
									{
										num7++;
									}
									if (num7 < 0)
									{
										num7 = 0;
									}
									if (num7 + num5 > 24)
									{
										num7 -= num7 + num5 - 23;
									}
									num3 = 0;
								}
								Look.Buffer.Fill(num6, num7, num6 + num4 + 1, num7 + num5 + 1, 32, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
								Look.Buffer.SingleBox(num6, num7, num6 + num4 + 1, num7 + num5 + 1, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Cyan, TextColor.Black));
								int num15 = 0;
								while (num15 < num5 && num15 + num3 < textBlock.Lines.Count)
								{
									if (num15 == 1 && num3 != 0)
									{
										Look.Buffer.Goto(num6 + 1, num7 + num15);
										Look.Buffer.Write("<MORE - use {{W|PgUp}} to scroll up>", true, false, false, null, -1);
									}
									else
									{
										Look.Buffer.Goto(num6 + 1, num7 + num15);
										Look.Buffer.Write(textBlock.Lines[num15 + ((num15 == 0) ? 0 : num3)], true, false, false, null, -1);
									}
									if (num15 == 0 && num3 == 0 && renderable != null)
									{
										Look.Buffer.Goto(num6 + num4 - 2, num7 + num15);
										Look.Buffer.Write("   ", true, false, false, null, -1);
										Look.Buffer.Goto(num6 + num4 - 1, num7 + num15);
										Look.Buffer.Write(renderable);
									}
									num15++;
								}
								Look.LookSB.Length = 0;
								string difficultyDescription = description.GetDifficultyDescription(null);
								if (!string.IsNullOrEmpty(difficultyDescription))
								{
									Look.LookSB.Append(difficultyDescription);
								}
								string feelingDescription = description.GetFeelingDescription(null);
								if (!string.IsNullOrEmpty(feelingDescription))
								{
									Look.LookSB.Compound(feelingDescription, ", ");
								}
								int num16 = num7 + num5 + 1;
								if (num16 > 24)
								{
									num16 = 24;
								}
								if (Look.LookSB.Length > 0)
								{
									Look.Buffer.Goto(num6 + num4 - ConsoleLib.Console.ColorUtility.LengthExceptFormatting(Look.LookSB), num16);
									Look.Buffer.Write(Look.LookSB, -1);
								}
								Look.Buffer.Goto(num6 + 1, num16);
								Look.Buffer.Write(text, true, false, false, null, -1);
								if (textBlock != null && num3 + num5 < textBlock.Lines.Count)
								{
									Look.Buffer.Goto(num6 + 1, num7 + num5);
									Look.Buffer.Write("<MORE - use {{W|PgDown}} to scroll down>", true, false, false, null, -1);
								}
							}
							else if (gameObject != null)
							{
								gameObject.FireEvent(XRL.World.Event.New("AfterLookedAt", "Looker", The.Player));
								if (The.Player != null)
								{
									The.Player.FireEvent(XRL.World.Event.New("LookedAt", "Object", gameObject));
								}
								gameObject = null;
							}
						}
						Look.Buffer.focusPosition = new Point2D(num, num2);
						if (Options.ModernUI)
						{
							if (gameObject != gameObject2)
							{
								flag2 = true;
							}
							if (flag2)
							{
								GameManager.Instance.uiQueue.awaitTask(delegate
								{
									GameManager.Instance.lookerTooltip.ForceHideTooltip();
								});
								GameManager.Instance.gameQueue.executeTasks();
								if (gameObject2 != null && gameObject2.Physics != null)
								{
									Look.QueueLookerTooltip(gameObject2.CurrentCell.X, gameObject2.CurrentCell.Y, "looker", num8);
								}
								flag2 = false;
								gameObject = gameObject2;
							}
						}
						Look.lookingAt = gameObject;
						Look._TextConsole.DrawBuffer(Look.Buffer, null, false);
						if (Keyboard.kbhit())
						{
							ScreenBuffer.ClearImposterSuppression();
							Keys keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
							if (keys == Keys.MouseEvent)
							{
								if (Keyboard.CurrentMouseEvent.Event == "RightClick")
								{
									flag = true;
								}
								if (Keyboard.CurrentMouseEvent.Event == "LeftClick")
								{
									num = Keyboard.CurrentMouseEvent.x;
									num2 = Keyboard.CurrentMouseEvent.y;
								}
								flag2 = true;
							}
							if ((keys == Keys.Space || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Interact") || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Interact")) && gameObject2 != null)
							{
								Look.HideTooltips();
								gameObject = null;
								if (gameObject2.Twiddle(false))
								{
									flag = true;
								}
							}
							if (keys == Keys.Enter)
							{
								flag = true;
							}
							if (Keyboard.IsCommandKey("V Positive"))
							{
								num8++;
								flag2 = true;
							}
							if (Keyboard.IsCommandKey("V Negative"))
							{
								num8--;
								flag2 = true;
							}
							if (Keyboard.IsCommandKey("Page Up") || keys == Keys.Prior)
							{
								if (GameManager.Instance.ModernUI)
								{
									GameManager.Instance.uiQueue.queueTask(delegate
									{
										Look.lookerScrollRect.verticalNormalizedPosition += Look.lookerScrollRect.viewport.rect.height / Look.lookerScrollRect.content.rect.height / 2f;
									}, 0);
								}
								if (num3 > 0)
								{
									num3--;
								}
							}
							if (Keyboard.IsCommandKey("Page Down") || keys == Keys.Next)
							{
								if (GameManager.Instance.ModernUI)
								{
									GameManager.Instance.uiQueue.queueTask(delegate
									{
										Look.lookerScrollRect.verticalNormalizedPosition -= Look.lookerScrollRect.viewport.rect.height / Look.lookerScrollRect.content.rect.height / 2f;
									}, 0);
								}
								if (textBlock != null && num3 + num5 < textBlock.Lines.Count)
								{
									num3++;
								}
							}
							if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdWalk" && TutorialManager.currentStep == null)
							{
								AutoAct.Setting = "M" + num.ToString() + "," + num2.ToString();
								The.ActionManager.SkipPlayerTurn = true;
								flag = true;
							}
							if (keys == Keys.N && Options.DebugInternals)
							{
								Popup.Show(string.Concat(new string[]
								{
									cell2.X.ToString(),
									", ",
									cell2.Y.ToString(),
									": ",
									cell2.GetNavigationWeightFor(The.Player, false, false, false, false, false).ToString()
								}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							if (keys == Keys.NumPad5 || keys == Keys.Escape)
							{
								flag = true;
							}
							if (keys == Keys.U || keys == Keys.L || keys == Keys.F1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdLockUnlock"))
							{
								Look.bLocked = !Look.bLocked;
							}
							if (Look.bLocked && !The.Player.OnWorldMap())
							{
								list.Clear();
								if (keys == Keys.NumPad1)
								{
									Look.GetObjectListCone(num - 1, num2 + 1, list, "sw", null, null);
								}
								if (keys == Keys.NumPad2)
								{
									Look.GetObjectListCone(num, num2 + 1, list, "s", null, null);
								}
								if (keys == Keys.NumPad3)
								{
									Look.GetObjectListCone(num + 1, num2 + 1, list, "se", null, null);
								}
								if (keys == Keys.NumPad4)
								{
									Look.GetObjectListCone(num - 1, num2, list, "w", null, null);
								}
								if (keys == Keys.NumPad6)
								{
									Look.GetObjectListCone(num + 1, num2, list, "e", null, null);
								}
								if (keys == Keys.NumPad7)
								{
									Look.GetObjectListCone(num - 1, num2 - 1, list, "nw", null, null);
								}
								if (keys == Keys.NumPad8)
								{
									Look.GetObjectListCone(num, num2 - 1, list, "n", null, null);
								}
								if (keys == Keys.NumPad9)
								{
									Look.GetObjectListCone(num + 1, num2 - 1, list, "ne", null, null);
								}
								if (list.Count > 0)
								{
									Cell currentCell2 = list[0].CurrentCell;
									num = currentCell2.X;
									num2 = currentCell2.Y;
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
							if (keys == Keys.F || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdFire"))
							{
								flag = true;
								cell = zone.GetCell(num, num2);
							}
							if (num < 0)
							{
								num = 0;
							}
							if (num >= zone.Width)
							{
								num = zone.Width - 1;
							}
							if (num2 < 0)
							{
								num2 = 0;
							}
							if (num2 >= zone.Height)
							{
								num2 = zone.Height - 1;
							}
						}
						else
						{
							Keyboard.IdleWait();
						}
					}
				}
				if (flag)
				{
					if (Options.ModernUI)
					{
						Look.HideTooltips();
					}
					else if (gameObject != null)
					{
						gameObject.FireEvent(XRL.World.Event.New("AfterLookedAt", "Looker", The.Player));
						if (The.Player != null)
						{
							The.Player.FireEvent(XRL.World.Event.New("LookedAt", "Object", gameObject));
						}
					}
				}
			}
			Look.HideTooltips();
			GameManager.Instance.PopGameView(false);
			Look._TextConsole.DrawBuffer(Look.OldBuffer, null, true);
			Look.lookingAt = null;
			return cell;
		}

		// Token: 0x0600CF9E RID: 53150 RVA: 0x004C38D8 File Offset: 0x004C1AD8
		public static void HideTooltips()
		{
			if (Options.ModernUI)
			{
				GameManager.Instance.uiQueue.awaitTask(delegate
				{
					GameManager.Instance.lookerTooltip.ForceHideTooltip();
				});
				GameManager.Instance.gameQueue.executeTasks();
			}
		}

		// Token: 0x040050C8 RID: 20680
		public static TextConsole _TextConsole;

		// Token: 0x040050C9 RID: 20681
		public static ScreenBuffer _ScreenBuffer;

		// Token: 0x040050CA RID: 20682
		[NonSerialized]
		private static StringBuilder LookSB = new StringBuilder();

		// Token: 0x040050CB RID: 20683
		[NonSerialized]
		private static ScreenBuffer Buffer = ScreenBuffer.create(80, 25);

		// Token: 0x040050CC RID: 20684
		[NonSerialized]
		private static ScreenBuffer OldBuffer = ScreenBuffer.create(80, 25);

		// Token: 0x040050CD RID: 20685
		private static LookSorter lookSorter = new LookSorter();

		// Token: 0x040050CE RID: 20686
		public static XRL.World.GameObject lookingAt;

		// Token: 0x040050CF RID: 20687
		private static ScrollRect _lookerScrollRect = null;

		// Token: 0x02001DB5 RID: 7605
		public struct TooltipInformation
		{
			// Token: 0x17000FF2 RID: 4082
			// (get) Token: 0x0600F50F RID: 62735 RVA: 0x00554846 File Offset: 0x00552A46
			public string SubHeader
			{
				get
				{
					if (string.IsNullOrEmpty(this.FeelingText))
					{
						return this.DifficultyText;
					}
					if (string.IsNullOrEmpty(this.DifficultyText))
					{
						return this.FeelingText;
					}
					return this.FeelingText + ", " + this.DifficultyText;
				}
			}

			// Token: 0x040073C4 RID: 29636
			public string DisplayName;

			// Token: 0x040073C5 RID: 29637
			public string FeelingText;

			// Token: 0x040073C6 RID: 29638
			public string DifficultyText;

			// Token: 0x040073C7 RID: 29639
			public string WoundLevel;

			// Token: 0x040073C8 RID: 29640
			public string LongDescription;

			// Token: 0x040073C9 RID: 29641
			public IRenderable IconRenderable;
		}
	}
}
