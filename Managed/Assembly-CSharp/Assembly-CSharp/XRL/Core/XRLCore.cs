using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Platform.IO;
using Platform.Performance;
using Qud.API;
using Qud.UI;
using Sheeter;
using UnityEngine;
using XRL.CharacterBuilds;
using XRL.Collections;
using XRL.Help;
using XRL.Messages;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World;
using XRL.World.AI;
using XRL.World.AI.Pathfinding;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Conversations.Parts;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.Core
{
	// Token: 0x02000634 RID: 1588
	[HasModSensitiveStaticCache]
	[Serializable]
	public class XRLCore
	{
		// Token: 0x06004235 RID: 16949 RVA: 0x00157B20 File Offset: 0x00155D20
		public void Reset()
		{
			XRLCore.FrameTimer.Reset();
			XRLCore.FrameTimer.Start();
			if (XRLCore.Core.Game.WallTime == null)
			{
				XRLCore.Core.Game.WallTime = new Stopwatch();
			}
			XRLCore.Core.Game.WallTime.Reset();
			XRLCore.Core.Game.WallTime.Start();
			JournalAPI.Init();
			GameManager.Instance.gameQueue.clear();
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x00157BA2 File Offset: 0x00155DA2
		public static bool IsCoreThread
		{
			get
			{
				return XRLCore.CoreThread == Thread.CurrentThread;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x00157BB0 File Offset: 0x00155DB0
		public static XRL.World.GameObject player
		{
			get
			{
				if (XRLCore.Core == null)
				{
					return null;
				}
				if (The.Player == null)
				{
					return null;
				}
				return The.Player;
			}
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x00157BC9 File Offset: 0x00155DC9
		public static int GetCurrentFrameAtFPS(int fps, float speed = 1f)
		{
			return (int)((float)XRLCore.FrameTimer.ElapsedMilliseconds * speed / (float)(1000 / fps));
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x00157BE2 File Offset: 0x00155DE2
		// (set) Token: 0x0600423A RID: 16954 RVA: 0x00157C0F File Offset: 0x00155E0F
		public static int PlayerVisibilityRadius
		{
			get
			{
				if (XRLCore._PlayerVisibilityRadius == -2147483648)
				{
					XRL.World.GameObject player = The.Player;
					XRLCore._PlayerVisibilityRadius = ((player != null) ? player.GetVisibilityRadius() : int.MinValue);
				}
				return XRLCore._PlayerVisibilityRadius;
			}
			set
			{
				XRLCore._PlayerVisibilityRadius = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x00157C17 File Offset: 0x00155E17
		// (set) Token: 0x0600423C RID: 16956 RVA: 0x00157C36 File Offset: 0x00155E36
		[Obsolete("Don't use this use player.GetConfusion() instead! Will be removed ~Q3 2021")]
		public int ConfusionLevel
		{
			get
			{
				if (this.Game == null)
				{
					return 0;
				}
				if (The.Player == null)
				{
					return 0;
				}
				return The.Player.GetConfusion();
			}
			set
			{
				if (this.Game == null)
				{
					MetricsManager.LogWarning("Trying to set confusion level without a game.");
					return;
				}
				if (The.Player == null)
				{
					MetricsManager.LogWarning("Trying to set confusion level without a player body.");
					return;
				}
				The.Player.SetIntProperty("ConfusionLevel", value, false);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x00157C6F File Offset: 0x00155E6F
		// (set) Token: 0x0600423E RID: 16958 RVA: 0x00157C8E File Offset: 0x00155E8E
		[Obsolete("Don't use this use player.GetFuriousConfusion() instead! Will be removed ~Q3 2021")]
		public int FuriousConfusion
		{
			get
			{
				if (this.Game == null)
				{
					return 0;
				}
				if (The.Player == null)
				{
					return 0;
				}
				return The.Player.GetFuriousConfusion();
			}
			set
			{
				if (this.Game == null)
				{
					MetricsManager.LogWarning("Trying to set confusion level without a game.");
					return;
				}
				if (The.Player == null)
				{
					MetricsManager.LogWarning("Trying to set confusion level without a player body.");
					return;
				}
				The.Player.SetIntProperty("FuriousConfusionLevel", value, false);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x00157CC7 File Offset: 0x00155EC7
		// (set) Token: 0x06004240 RID: 16960 RVA: 0x00157CDD File Offset: 0x00155EDD
		public float XPMul
		{
			get
			{
				return The.Game.GetFloatGameState("XPMul", 1f);
			}
			set
			{
				The.Game.SetFloatGameState("XPMul", value);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x00157CEF File Offset: 0x00155EEF
		public static long CurrentTurn
		{
			get
			{
				if (XRLCore.Core.Game == null)
				{
					return 0L;
				}
				return XRLCore.Core.Game.TimeTicks;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06004242 RID: 16962 RVA: 0x00157D0F File Offset: 0x00155F0F
		// (set) Token: 0x06004243 RID: 16963 RVA: 0x00157D17 File Offset: 0x00155F17
		public string PlayerWalking
		{
			get
			{
				return this._PlayerWalking;
			}
			set
			{
				this._PlayerWalking = value;
				if (value == "")
				{
					Loading.SetLoadingStatus(null, false);
					this.PlayerAvoid.Clear();
					this.AutoexploreTarget = null;
				}
			}
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00157D48 File Offset: 0x00155F48
		public bool InAvoidList(XRLCore.SortPoint P)
		{
			for (int i = 0; i < this.PlayerAvoid.Items.Count; i++)
			{
				XRLCore.SortPoint sortPoint = this.PlayerAvoid.Items[i];
				if (sortPoint.X == P.X && sortPoint.Y == P.Y)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x00157DA1 File Offset: 0x00155FA1
		public bool CheckInventoryObject(XRL.World.GameObject GO)
		{
			return GO.CurrentCell == null && GO.Equipped == null && GO.InInventory == The.Player;
		}

		/// <summary>Force all mod/game sensitive static caches to be loaded</summary>
		// Token: 0x06004246 RID: 16966 RVA: 0x00157DC8 File Offset: 0x00155FC8
		public void LoadEverything()
		{
			if (this._isNewGameModFlow)
			{
				return;
			}
			foreach (Type type in ModManager.GetTypesWithAttribute(typeof(HasGameBasedStaticCacheAttribute), true).Union<Type>(ModManager.GetTypesWithAttribute(typeof(HasModSensitiveStaticCacheAttribute), true)))
			{
				foreach (MethodInfo methodInfo in from mi in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
					where mi.GetCustomAttributes(typeof(PreGameCacheInitAttribute), false).Count<object>() > 0
					select mi)
				{
					methodInfo.Invoke(null, new object[0]);
				}
			}
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x00157E9C File Offset: 0x0015609C
		public void HotloadConfiguration(bool bGenerateCorpusData = false)
		{
			ModManager.ResetModSensitiveStaticCaches();
			this.LoadEverything();
			WorldFactory.Factory.Init();
			MutationFactory.CheckInit();
			MessageQueue.AddPlayerMessage("Configuration hotloaded...", null, true);
			this.WriteConsoleLine("Hot Loading Books...\n");
			BookUI.InitBooks(bGenerateCorpusData);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00157ED8 File Offset: 0x001560D8
		public bool AttemptSmartUse(Cell TargetCell, int MinPriority = 0)
		{
			XRL.World.GameObject gameObject = SmartUse.FindPlayerSmartUseObject(TargetCell, MinPriority);
			if ((gameObject == null || gameObject.IsOpenLiquidVolume()) && TargetCell == The.Player.CurrentCell && TargetCell.HasObjectWithPart("Physics", new Predicate<XRL.World.GameObject>(SmartUse.CanPlayerTake)))
			{
				return The.Player.FireEvent(XRL.World.Event.New("CommandGet", "GetOne", false, "TargetCell", TargetCell, "SmartUse", true));
			}
			return gameObject != null && SmartUse.PlayerPerformSmartUse(gameObject, MinPriority);
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x00157F5B File Offset: 0x0015615B
		public void UpdateOverlay()
		{
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x00157F5D File Offset: 0x0015615D
		public static void RegisterOnBeginPlayerTurnCallback(Action<XRLCore> action)
		{
			XRLCore.OnBeginPlayerTurnCallbacks.Add(action);
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x00157F6A File Offset: 0x0015616A
		public static void RemoveOnBeginPlayerTurnCallback(Action<XRLCore> action)
		{
			XRLCore.OnBeginPlayerTurnCallbacks.Remove(action);
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x00157F78 File Offset: 0x00156178
		public static void RegisterOnEndPlayerTurnCallback(Action<XRLCore> action, bool Single = false)
		{
			if (Single)
			{
				XRLCore.OnEndPlayerTurnSingleCallbacks.Add(action);
				return;
			}
			XRLCore.OnEndPlayerTurnCallbacks.Add(action);
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x00157F94 File Offset: 0x00156194
		public static void RemoveOnEndPlayerTurnCallback(Action<XRLCore> action, bool Single = false)
		{
			if (Single)
			{
				XRLCore.OnEndPlayerTurnSingleCallbacks.Remove(action);
				return;
			}
			XRLCore.OnEndPlayerTurnCallbacks.Remove(action);
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x00157FB2 File Offset: 0x001561B2
		public static void RegisterOnPassedTenPlayerTurnCallback(Action<XRLCore> action)
		{
			XRLCore.OnPassedTenPlayerTurnCallbacks.Add(action);
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x00157FC0 File Offset: 0x001561C0
		public static void TenPlayerTurnsPassed()
		{
			int i = 0;
			int count = XRLCore.OnPassedTenPlayerTurnCallbacks.Count;
			while (i < count)
			{
				XRLCore.OnPassedTenPlayerTurnCallbacks[i](XRLCore.Core);
				i++;
			}
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x00157FF9 File Offset: 0x001561F9
		public static void RegisterAfterRenderCallback(Action<XRLCore, ScreenBuffer> action)
		{
			XRLCore.AfterRenderCallbacks.Add(action);
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x00158006 File Offset: 0x00156206
		public static void RegisterNewMessageLogEntryCallback(Action<string> action)
		{
			if (!XRLCore.OnNewMessageLogEntryCallbacks.Contains(action))
			{
				XRLCore.OnNewMessageLogEntryCallbacks.Add(action);
			}
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x00158020 File Offset: 0x00156220
		public static void CallNewMessageLogEntryCallbacks(string log)
		{
			try
			{
				if (XRLCore.OnNewMessageLogEntryCallbacks != null)
				{
					for (int i = 0; i < XRLCore.OnNewMessageLogEntryCallbacks.Count; i++)
					{
						XRLCore.OnNewMessageLogEntryCallbacks[i](log);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x00158070 File Offset: 0x00156270
		public static void CallBeginPlayerTurnCallbacks()
		{
			for (int i = 0; i < XRLCore.OnBeginPlayerTurnCallbacks.Count; i++)
			{
				XRLCore.OnBeginPlayerTurnCallbacks[i](XRLCore.Core);
			}
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x001580A8 File Offset: 0x001562A8
		public void PlayerTurn()
		{
			TutorialManager.GameSync();
			XRLCore.CallBeginPlayerTurnCallbacks();
			Sidebar.SidebarTick += 1L;
			bool flag = false;
			if (The.Player != null)
			{
				if (!The.Player.HasEffect<Confused>() && !The.Player.HasEffect<HulkHoney_Tonic>())
				{
					this.ConfusionLevel = 0;
					this.FuriousConfusion = 0;
				}
				Brain brain = The.Player.Brain;
				AllegianceSet allegianceSet = ((brain != null) ? brain.Allegiance : null);
				if (allegianceSet != null && allegianceSet.Count == 0)
				{
					allegianceSet["Player"] = 100;
				}
			}
			double totalMilliseconds = XRLCore.FrameTimer.Elapsed.TotalMilliseconds;
			this.Game.Player.Messages.BeginPlayerTurn();
			if (!XRLCore.sixArmsSet)
			{
				XRL.World.GameObject player = The.Player;
				if (((player != null) ? player.GetBodyPartCount("Arm") : 0) >= 6)
				{
					Achievement.SIX_ARMS.Unlock();
					XRLCore.sixArmsSet = true;
				}
			}
			if (Options.InventoryConsistencyCheck)
			{
				XRL.World.GameObject player2 = The.Player;
				Inventory inventory = ((player2 != null) ? player2.Inventory : null);
				if (inventory != null)
				{
					int i2 = 0;
					int count = inventory.Objects.Count;
					while (i2 < count)
					{
						XRL.World.GameObject gameObject = inventory.Objects[i2];
						if (!this.CheckInventoryObject(gameObject))
						{
							string text = "Invalid inventory object: ";
							XRL.World.GameObject gameObject2 = gameObject;
							MessageQueue.AddPlayerMessage(text + ((gameObject2 != null) ? gameObject2.ToString() : null), 'R', true);
						}
						i2++;
					}
				}
			}
			if (Options.CheckMemory)
			{
				XRLCore.MemCheckTurns++;
				if (XRLCore.MemCheckTurns > 100)
				{
					XRLCore.MemCheckTurns = 0;
					long totalMemory = GC.GetTotalMemory(false);
					if (totalMemory >= XRLCore.MemCheckHeadRoom)
					{
						MetricsManager.SendTelemetry("mem_warning", totalMemory.ToString(), null);
						Popup.ShowSpace("WARNING: Caves of Qud is using a lot of memory. To avoid a crash, we recommend you save your game, close the program, and then relaunch and continue.\n\nYou'll be warned again after an additional 500MB of RAM is used. Currently using " + string.Format("{0:n0}", totalMemory / 1000000L) + "MB. At 3,500MB, or before, the game WILL crash. Please send a saved game with this issue to support@freeholdgames.com.", null, "Sounds/UI/ui_notification", null, true, true, null);
						if (XRLCore.MemCheckHeadRoom == (long)((ulong)(-1794967296)))
						{
							MetricsManager.LogEvent("Memwarning");
						}
						XRLCore.MemCheckHeadRoom += 500000000L;
					}
				}
			}
			Sidebar.UpdateState();
			Sidebar.Update();
			Keyboard.ClearMouseEvents(true);
			while (The.Player.Energy.Value >= 1000 && this.Game.Running)
			{
				while (!GameManager.focused && this.Game.Running)
				{
					Thread.Sleep(200);
				}
				GameManager.Instance.CurrentGameView = Options.StageViewID;
				XRL.World.Event.ResetPool(true);
				int num = The.Player.Energy.Value;
				if (The.Player.CurrentZone != null)
				{
					The.Player.CurrentZone.SetActive();
				}
				if (this.HPWarning)
				{
					Popup.ShowSpace("{{R|Your health has dropped below {{C|" + Globals.HPWarningThreshold.ToString() + "%}}!}}", null, "Sounds/UI/ui_notification", null, true, true, null);
					this.HPWarning = false;
				}
				ActionManager actionManager = this.Game.ActionManager;
				if (Keyboard.kbhit() || Keyboard.HasMouseEvent(false) || !this.EnableAnimation || XRLCore.Core.PlayerWalking == "ReopenMissileUI")
				{
					string text2 = null;
					string text3 = "CmdNone";
					if (!The.Game.Running)
					{
						return;
					}
					CombatJuice.startTurn();
					if (Keyboard.HasMouseEvent(true))
					{
						Keyboard.MouseEvent mouseEvent = Keyboard.PopMouseEvent();
						if (!TutorialManager.AllowCommand((mouseEvent != null) ? mouseEvent.Event : null))
						{
							return;
						}
						while (mouseEvent != null)
						{
							if (The.Player.CurrentCell == null)
							{
								break;
							}
							if (mouseEvent.Event == "AdventureMouseLook")
							{
								Look.ShowLooker(0, mouseEvent.x, mouseEvent.y);
							}
							if (mouseEvent.Event.StartsWith("Command:"))
							{
								string @event = mouseEvent.Event;
								text3 = @event.Substring(8, @event.Length - 8);
								break;
							}
							if (mouseEvent.Event == "AdventureMouseForceMove" && Options.MouseMovement)
							{
								Cell currentCell = The.Player.CurrentCell;
								Cell cell = currentCell.ParentZone.GetCell(mouseEvent.x, mouseEvent.y);
								if (cell.PathDistanceTo(currentCell) != 1)
								{
									AutoAct.Setting = "M" + cell.X.ToString() + "," + cell.Y.ToString();
									return;
								}
								text2 = currentCell.GetDirectionFromCell(cell, false);
								text3 = "CmdMove" + text2;
							}
							else if (mouseEvent.Event == "AdventureMouseForceAttack")
							{
								Cell currentCell2 = The.Player.CurrentCell;
								Cell cell2 = currentCell2.ParentZone.GetCell(mouseEvent.x, mouseEvent.y);
								if (cell2 != null && cell2.IsExplored())
								{
									if (cell2.PathDistanceTo(currentCell2) != 1)
									{
										AutoAct.Setting = "M" + cell2.X.ToString() + "," + cell2.Y.ToString();
										return;
									}
									text2 = currentCell2.GetDirectionFromCell(cell2, false);
									text3 = "CmdAttack" + text2;
								}
							}
							else if (mouseEvent.Event == "AdventureMouseContextAction")
							{
								Cell currentCell3 = The.Player.CurrentCell;
								Cell cell3 = currentCell3.ParentZone.GetCell(mouseEvent.x, mouseEvent.y);
								if (cell3 != null && !cell3.IsExplored())
								{
									AutoAct.Setting = "M" + cell3.X.ToString() + "," + cell3.Y.ToString();
									return;
								}
								if (cell3 != null && cell3.IsExplored())
								{
									if (cell3 == currentCell3)
									{
										text3 = "CmdWait";
									}
									else if (cell3.PathDistanceTo(currentCell3) == 1)
									{
										text2 = currentCell3.GetDirectionFromCell(cell3, false);
										XRL.World.GameObject gameObject3 = SmartUse.FindPlayerSmartUseObject(cell3, 100);
										if (cell3.HasObjectWithPart("Brain", (XRL.World.GameObject GO) => GO.IsHostileTowards(The.Player)))
										{
											text3 = "CmdAttack" + text2;
										}
										else if (gameObject3 != null && !gameObject3.IsOpenLiquidVolume())
										{
											text3 = "CmdNone";
											SmartUse.PlayerPerformSmartUse(gameObject3, 0);
										}
										else
										{
											text3 = "CmdMove" + text2;
										}
									}
									else
									{
										XRL.World.GameObject gameObject4 = SmartUse.FindPlayerSmartUseObject(cell3, 100);
										if (gameObject4 != null && !gameObject4.IsOpenLiquidVolume())
										{
											AutoAct.Setting = "!" + gameObject4.ID;
											return;
										}
										AutoAct.Setting = "M" + cell3.X.ToString() + "," + cell3.Y.ToString();
										return;
									}
								}
							}
							else if (mouseEvent.Event == "AdventureMouseInteract")
							{
								Cell cell4 = The.Player.CurrentCell.ParentZone.GetCell(mouseEvent.x, mouseEvent.y);
								if (cell4 != null && cell4.IsExplored())
								{
									XRL.World.GameObject highestRenderLayerObject = cell4.GetHighestRenderLayerObject();
									if (highestRenderLayerObject != null && highestRenderLayerObject.GetTagOrStringProperty("DefaultRightClickInventoryAction", null) != null)
									{
										InventoryActionEvent.Check(highestRenderLayerObject, The.Player, highestRenderLayerObject, highestRenderLayerObject.GetTagOrStringProperty("DefaultRightClickInventoryAction", null), false, false, false, false, false, 0, 0, 0, null, null, null, null);
									}
									else if (highestRenderLayerObject != null)
									{
										global::UnityEngine.Debug.Log(string.Format("Mouse: {0}", mouseEvent.Vector2));
										highestRenderLayerObject.Twiddle(true);
									}
									else
									{
										SoundManager.PlayUISound("Sounds/UI/ui_cursor_scroll", 1f, false, true, SoundRequest.SoundEffectType.None);
									}
								}
							}
							else
							{
								if (!(mouseEvent.Event == "AdventureMouseInteractAll"))
								{
									text3 = mouseEvent.Event;
									break;
								}
								Cell currentCell4 = The.Player.CurrentCell;
								Cell cell5 = currentCell4.ParentZone.GetCell(mouseEvent.x, mouseEvent.y);
								if (cell5 != null && cell5.IsExplored())
								{
									The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", currentCell4.GetDirectionFromCell(cell5, false)));
								}
							}
							if (Keyboard.HasMouseEvent(true))
							{
								mouseEvent = Keyboard.PopMouseEvent();
							}
							else
							{
								mouseEvent = null;
							}
						}
					}
					else if (AutoAct.Setting == "ReopenMissileUI")
					{
						AutoAct.Interrupt(null, null, null, false);
						text3 = "CmdFire";
					}
					else if (Keyboard.kbhit() || !this.EnableAnimation)
					{
						text3 = CommandBindingManager.GetNextCommand(CommandBindingManager.CoreLoopExcludes);
					}
					if (!TutorialManager.AllowCommand(text3))
					{
						break;
					}
					if (text3 != null && text3 != null && text3.StartsWith("CmdMoveToBorder:"))
					{
						List<int> list = text3.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None).ToList<string>().Map<string, int>((string i) => int.Parse(i));
						Vector2i vector2i = new Vector2i(list[0], list[1]);
						Vector2i vector2i2 = new Vector2i(list[0], list[1]).Clamp(0, 0, 79, 24);
						if (vector2i.DistanceTo(The.PlayerCell.Location.Vector2i) != 1)
						{
							this.PlayerAvoid.Clear();
							AutoAct.Setting = string.Format("M{0},{1}", vector2i2.x, vector2i2.y);
							return;
						}
						text3 = "CmdMove" + The.PlayerCell.Location.Vector2i.DirectionTo(vector2i);
					}
					if (text3 == "CmdWaitMenu")
					{
						string[] array = new string[] { "Wait 1 Turn", "Wait N Turns", "Wait 20 Turns", "Wait 100 Turns", "Wait Until Healed", "Wait Until Party Healed", "Wait Until Morning" };
						char[] array2 = new char[array.Length];
						char c = 'a';
						int j = 0;
						int num2 = array.Length;
						while (j < num2)
						{
							char[] array3 = array2;
							int num3 = j;
							char c2 = c;
							c = c2 + '\u0001';
							array3[num3] = c2;
							j++;
						}
						int num4 = Popup.PickOption("Select Wait Style", null, "", "Sounds/UI/ui_notification", array, array2, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num4 >= 0)
						{
							if (num4 == 0)
							{
								text3 = "CmdWait";
							}
							else if (num4 == 1)
							{
								text3 = "CmdWaitN";
							}
							else if (num4 == 2)
							{
								text3 = "CmdWait20";
							}
							else if (num4 == 3)
							{
								text3 = "CmdWait100";
							}
							else if (num4 == 4)
							{
								text3 = "CmdWaitUntilHealed";
							}
							else if (num4 == 5)
							{
								text3 = "CmdWaitUntilPartyHealed";
							}
							else if (num4 == 6)
							{
								text3 = "CmdWaitUntilMorning";
							}
						}
					}
					else if (text3 == "CmdMoveMenu")
					{
						GetMovementCapabilitiesEvent @for = GetMovementCapabilitiesEvent.GetFor(The.Player, true);
						string[] array4 = @for.Descriptions.ToArray();
						char[] array5 = new char[array4.Length];
						char c3 = 'a';
						int k = 0;
						int num5 = array4.Length;
						while (k < num5)
						{
							char[] array6 = array5;
							int num6 = k;
							char c4 = c3;
							c3 = c4 + '\u0001';
							array6[num6] = c4;
							k++;
						}
						int num7 = Popup.PickOption("Select Move Style", null, "", "Sounds/UI/ui_notification", array4, array5, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num7 >= 0)
						{
							ActivatedAbilityEntry activatedAbilityEntry = @for.Abilities[num7];
							string text4 = ((activatedAbilityEntry != null) ? activatedAbilityEntry.NotUsableDescription : null);
							if (text4 != null)
							{
								Popup.ShowFail(text4, true, true, true);
							}
							else
							{
								text3 = @for.Commands[num7];
							}
						}
					}
					uint num8 = <PrivateImplementationDetails>.ComputeStringHash(text3);
					if (num8 <= 2453832838U)
					{
						if (num8 <= 1249711583U)
						{
							if (num8 <= 751332348U)
							{
								if (num8 <= 382683129U)
								{
									if (num8 <= 113003371U)
									{
										if (num8 != 18625473U)
										{
											if (num8 != 57379240U)
											{
												if (num8 != 113003371U)
												{
													goto IL_4989;
												}
												if (!(text3 == "CmdUnknown"))
												{
													goto IL_4989;
												}
											}
											else
											{
												if (!(text3 == "CmdAutoMoveE"))
												{
													goto IL_4989;
												}
												The.Player.AutoMove("E");
											}
										}
										else
										{
											if (!(text3 == "CmdWait100"))
											{
												goto IL_4989;
											}
											goto IL_3336;
										}
									}
									else if (num8 <= 255651684U)
									{
										if (num8 != 210634615U)
										{
											if (num8 != 255651684U)
											{
												goto IL_4989;
											}
											if (!(text3 == "CmdAutoMoveNE"))
											{
												goto IL_4989;
											}
											The.Player.AutoMove("NE");
										}
										else
										{
											if (!(text3 == "CmdMoveCenter"))
											{
												goto IL_4989;
											}
											goto IL_289F;
										}
									}
									else if (num8 != 378298892U)
									{
										if (num8 != 382683129U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdMoveNW"))
										{
											goto IL_4989;
										}
										AutoAct.ClearAutoMoveStop();
										The.Player.Move("NW", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
										if (Options.DrawStepImmediately)
										{
											this.RenderBase(false, false);
										}
									}
									else
									{
										if (!(text3 == "CmdMoveSW"))
										{
											goto IL_4989;
										}
										AutoAct.ClearAutoMoveStop();
										The.Player.Move("SW", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
										if (Options.DrawStepImmediately)
										{
											this.RenderBase(false, false);
										}
									}
								}
								else if (num8 <= 618253368U)
								{
									if (num8 <= 583968151U)
									{
										if (num8 != 557648826U)
										{
											if (num8 != 583968151U)
											{
												goto IL_4989;
											}
											if (!(text3 == "CmdAttackNearest"))
											{
												goto IL_4989;
											}
											if (The.Player.GetTotalConfusion() > 0)
											{
												if (The.Player.GetFuriousConfusion() > 0)
												{
													The.Player.FireEvent(XRL.World.Event.New("CmdMove" + Directions.GetRandomDirection(), 0, 0, 0));
													return;
												}
												Popup.ShowFail("You cannot autoattack while you are confused.", true, true, true);
												return;
											}
											else
											{
												Cell currentCell5 = The.Player.CurrentCell;
												XRL.World.GameObject gameObject5 = The.Player.Target;
												Engulfed effect = The.Player.GetEffect<Engulfed>();
												if (effect != null && XRL.World.GameObject.Validate(effect.EngulfedBy) && effect.EngulfedBy.IsHostileTowards(The.Player))
												{
													gameObject5 = effect.EngulfedBy;
												}
												if (gameObject5 != null && gameObject5.Brain != null && !gameObject5.IsHostileTowards(The.Player))
												{
													Popup.ShowFail(string.Concat(new string[]
													{
														"You do not autoattack ",
														gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
														" because ",
														gameObject5.itis,
														" not hostile to you."
													}), true, true, true);
													return;
												}
												bool flag2 = false;
												int num9 = ((gameObject5 == null) ? 9999999 : currentCell5.DistanceTo(gameObject5));
												if (num9 == 1)
												{
													string directionFromCell = currentCell5.GetDirectionFromCell(gameObject5.CurrentCell, false);
													if (directionFromCell.IsNullOrEmpty() || !(directionFromCell != "."))
													{
														goto IL_3EE5;
													}
													try
													{
														AutoAct.Attacking = true;
														The.Player.AttackDirection(directionFromCell, true);
														break;
													}
													finally
													{
														AutoAct.Attacking = false;
													}
												}
												if (num9 == 0)
												{
													Cell randomElement = currentCell5.GetLocalNavigableAdjacentCells(The.Player, 5).GetRandomElement<Cell>(null);
													if (randomElement != null)
													{
														string directionFromCell2 = currentCell5.GetDirectionFromCell(randomElement, false);
														if (!directionFromCell2.IsNullOrEmpty() && directionFromCell2 != ".")
														{
															int count2 = The.Game.Player.Messages.Messages.Count;
															if (The.Player.Move(directionFromCell2, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0) || count2 < The.Game.Player.Messages.Messages.Count)
															{
																break;
															}
														}
													}
													flag2 = true;
												}
												IL_3EE5:
												if (gameObject5 != null && !gameObject5.IsVisible())
												{
													Popup.ShowFail("You cannot see your target.", true, true, true);
													return;
												}
												List<Cell> localAdjacentCells = currentCell5.GetLocalAdjacentCells();
												int? num10 = null;
												int l = 0;
												int count3 = localAdjacentCells.Count;
												while (l < count3)
												{
													XRL.World.GameObject combatTarget = localAdjacentCells[l].GetCombatTarget(The.Player, false, false, false, 0, null, null, null, null, null, true, false, null);
													if (combatTarget != null && combatTarget.IsHostileTowards(The.Player) && combatTarget.IsVisible())
													{
														int? num11 = The.Player.Con(combatTarget, false);
														if (num11 != null)
														{
															if (num10 != null)
															{
																int? num12 = num11;
																int? num13 = num10;
																if (!((num12.GetValueOrDefault() < num13.GetValueOrDefault()) & ((num12 != null) & (num13 != null))))
																{
																	num13 = num11;
																	num12 = num10;
																	if (!((num13.GetValueOrDefault() == num12.GetValueOrDefault()) & (num13 != null == (num12 != null))) || combatTarget.Health() >= gameObject5.Health())
																	{
																		goto IL_3FF4;
																	}
																}
															}
															gameObject5 = combatTarget;
															num9 = 1;
														}
													}
													IL_3FF4:
													l++;
												}
												if (gameObject5 != null && num9 <= 1)
												{
													string directionFromCell3 = currentCell5.GetDirectionFromCell(gameObject5.CurrentCell, false);
													if (!directionFromCell3.IsNullOrEmpty() && directionFromCell3 != ".")
													{
														try
														{
															AutoAct.Attacking = true;
															The.Player.AttackDirection(directionFromCell3, true);
															break;
														}
														finally
														{
															AutoAct.Attacking = false;
														}
													}
												}
												num9 = int.MaxValue;
												List<XRL.World.GameObject> list2 = currentCell5.ParentZone.FastSquareVisibility(currentCell5.X, currentCell5.Y, 80, "Brain", The.Player, true, true, null);
												int m = 0;
												int count4 = list2.Count;
												while (m < count4)
												{
													XRL.World.GameObject gameObject6 = list2[m];
													if (gameObject6.IsHostileTowards(The.Player))
													{
														int num14 = currentCell5.DistanceTo(gameObject6);
														bool flag3 = false;
														if (num14 < num9)
														{
															flag3 = true;
														}
														else if (num14 == num9)
														{
															int? num12 = The.Player.Con(gameObject6, false);
															int? num13 = The.Player.Con(gameObject5, false);
															if ((num12.GetValueOrDefault() < num13.GetValueOrDefault()) & ((num12 != null) & (num13 != null)))
															{
																flag3 = true;
															}
														}
														if (flag3 && gameObject6.CurrentCell.GetCombatTarget(The.Player, false, false, false, 0, null, null, null, null, null, true, false, null) == gameObject6)
														{
															gameObject5 = gameObject6;
															num9 = num14;
														}
													}
													m++;
												}
												if (gameObject5 != null)
												{
													FindPath findPath = new FindPath(currentCell5, gameObject5.CurrentCell, false, true, The.Player, 20, true, false, false, false, false, null);
													if (findPath.Usable)
													{
														The.Player.Move(findPath.Directions[0], false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
														return;
													}
													if (flag2)
													{
														Popup.ShowFail("You can't find a way to flee from " + gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
														return;
													}
													Popup.ShowFail("You can't find a way to reach " + gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
													return;
												}
												else
												{
													if (flag2)
													{
														gameObject5 = The.Player.Target;
														Popup.ShowFail("You can't find a way to flee from " + gameObject5.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
														return;
													}
													MessageQueue.AddPlayerMessage("You don't see any hostiles nearby.", null, true);
													return;
												}
											}
										}
										else
										{
											if (!(text3 == "CmdAutoMoveNW"))
											{
												goto IL_4989;
											}
											The.Player.AutoMove("NW");
										}
									}
									else if (num8 != 617569795U)
									{
										if (num8 != 618253368U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdCompanions"))
										{
											goto IL_4989;
										}
										Zone currentZone = The.Player.CurrentZone;
										if (currentZone != null)
										{
											List<XRL.World.GameObject> companionsReadonly = The.Player.GetCompanionsReadonly(currentZone.Width * 2, null);
											if (companionsReadonly.IsNullOrEmpty<XRL.World.GameObject>())
											{
												if (The.Player.Brain.PartyMembers.Count == 0)
												{
													Popup.ShowFail("You have no companions.", true, true, true);
													goto IL_4A02;
												}
												Popup.ShowFail("None of your companions are nearby.", true, true, true);
												goto IL_4A02;
											}
											else
											{
												companionsReadonly.Sort((XRL.World.GameObject a, XRL.World.GameObject b) => a.SortVs(b, null, false, true, true, false, null));
												XRL.World.GameObject gameObject7 = Popup.PickGameObject("Interact with which companion?", companionsReadonly, true, false, true, (XRL.World.GameObject o) => GetCompanionStatusEvent.GetFor(o, The.Player), 0, false);
												if (gameObject7 != null)
												{
													gameObject7.Twiddle(false);
												}
											}
										}
									}
									else
									{
										if (!(text3 == "CmdMoveNE"))
										{
											goto IL_4989;
										}
										AutoAct.ClearAutoMoveStop();
										The.Player.Move("NE", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
										if (Options.DrawStepImmediately)
										{
											this.RenderBase(false, false);
										}
									}
								}
								else if (num8 <= 680296034U)
								{
									if (num8 != 637459718U)
									{
										if (num8 != 680296034U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdMoveSE"))
										{
											goto IL_4989;
										}
										AutoAct.ClearAutoMoveStop();
										The.Player.Move("SE", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
										if (Options.DrawStepImmediately)
										{
											this.RenderBase(false, false);
										}
									}
									else
									{
										if (!(text3 == "CmdExplore"))
										{
											goto IL_4989;
										}
										The.Player.CurrentCell.ParentZone.ExploreAll();
										XRLCore.Core.VisAllToggle = !XRLCore.Core.VisAllToggle;
									}
								}
								else if (num8 != 744131859U)
								{
									if (num8 != 751332348U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdAttackDirection"))
									{
										goto IL_4989;
									}
									string text5 = PickDirection.ShowPicker("Attack in what direction?");
									if (!text5.IsNullOrEmpty() && text5 != "." && text5 != "?")
									{
										The.Player.AttackDirection(text5, true);
									}
								}
								else
								{
									if (!(text3 == "CmdMoveS"))
									{
										goto IL_4989;
									}
									AutoAct.ClearAutoMoveStop();
									The.Player.Move("S", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
									if (Options.DrawStepImmediately)
									{
										this.RenderBase(false, false);
									}
								}
							}
							else if (num8 <= 1039527375U)
							{
								if (num8 <= 807577018U)
								{
									if (num8 != 762100818U)
									{
										if (num8 != 792940293U)
										{
											if (num8 != 807577018U)
											{
												goto IL_4989;
											}
											if (!(text3 == "CmdJournal"))
											{
												goto IL_4989;
											}
											Screens.CurrentScreen = 6;
											Screens.Show(The.Player);
										}
										else
										{
											if (!(text3 == "CmdInventory"))
											{
												goto IL_4989;
											}
											InventoryAndEquipmentStatusScreen.EnteringFromEquipmentSide = false;
											Screens.CurrentScreen = 2;
											Screens.Show(The.Player);
										}
									}
									else
									{
										if (!(text3 == "CmdReload"))
										{
											goto IL_4989;
										}
										CommandReloadEvent.Execute(The.Player, false, false, 0);
									}
								}
								else if (num8 <= 844797573U)
								{
									if (num8 != 811242335U)
									{
										if (num8 != 844797573U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdMoveU"))
										{
											goto IL_4989;
										}
										if (The.Player.CurrentCell.FireEvent(XRL.World.Event.New("ClimbUp", "GO", The.Player)))
										{
											Cell currentCell6 = The.Player.CurrentCell;
											if (currentCell6 != null && !currentCell6.ParentZone.SpecialUpMessage().IsNullOrEmpty())
											{
												Popup.Show(currentCell6.ParentZone.SpecialUpMessage(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
											}
											else if (currentCell6 != null && !currentCell6.ParentZone.IsWorldMap() && currentCell6.ParentZone.Z <= 10 && !currentCell6.ParentZone.IsInside())
											{
												if (The.Player.AreHostilesNearby() && !The.Player.IsFlying)
												{
													Popup.ShowFail("There are hostiles nearby!", true, true, true);
												}
												else
												{
													List<XRL.World.GameObject> list3 = currentCell6.ParentZone.FindObjects((XRL.World.GameObject x) => x.CurrentCell.IsExplored() && x.HasRegisteredEvent("ClimbUp") && x.Render != null && x.Render.Visible && !x.HasTagOrIntProperty("ExcludeFromAscentSelection") && GenericQueryEvent.Check(x, "AscentEligible", null, null, 0, true));
													int num15 = -1;
													if (list3.Count > 0)
													{
														string[] array7 = new string[list3.Count + 1];
														char[] array8 = new char[array7.Length];
														IRenderable[] array9 = new IRenderable[array7.Length];
														char c5 = 'b';
														array7[0] = "world map";
														array8[0] = 'a';
														array9[0] = new Renderable("Mutations/teleport_other.bmp", " ", "&W", null, 'Y');
														int n = 1;
														int count5 = list3.Count;
														while (n <= count5)
														{
															XRL.World.GameObject gameObject8 = list3[n - 1];
															array7[n] = gameObject8.DisplayNameOnlyDirect + " [" + The.Player.DescribeDirectionToward(gameObject8, true, true) + "]";
															char[] array10 = array8;
															int num16 = n;
															int num17;
															if (c5 > 'z')
															{
																num17 = 32;
															}
															else
															{
																c5 = (char)((num17 = (int)c5) + 1);
															}
															array10[num16] = num17;
															array9[n] = gameObject8.RenderForUI(null, false);
															n++;
														}
														num15 = Popup.PickOption("Select a destination", null, "", "Sounds/UI/ui_notification", array7, array8, array9, null, null, null, null, 1, 78, 0, -1, true, false, false, true, false, null, null);
														if (num15 == -1)
														{
															goto IL_49CA;
														}
														if (num15 > 0)
														{
															AutoAct.Setting = "M" + list3[num15 - 1].ID;
															return;
														}
													}
													if (The.Player.CanTravel() && (num15 != -1 || !Options.AskForWorldmap || Popup.ShowYesNoCancel("Are you sure you want to go to the world map?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes))
													{
														try
														{
															string address = currentCell6.GetAddress();
															if (address.Contains("."))
															{
																this.Game.SetStringGameState("LastLocationOnSurface", address);
															}
														}
														catch (Exception ex)
														{
															XRLCore.LogError(ex);
														}
														string zoneWorld = this.Game.ZoneManager.ActiveZone.GetZoneWorld();
														int zonewX = this.Game.ZoneManager.ActiveZone.GetZonewX();
														int zonewY = this.Game.ZoneManager.ActiveZone.GetZonewY();
														Zone zone = this.Game.ZoneManager.GetZone(zoneWorld);
														Cell cell6 = zone.GetCell(zonewX, zonewY);
														XRL.World.GameObject firstObjectWithPart = cell6.GetFirstObjectWithPart("TerrainTravel");
														if (The.Player.DirectMoveTo(cell6, 0, false, false, false, firstObjectWithPart))
														{
															The.Player.PlayWorldOrUISound("sfx_worldMap_enter", null);
															Cell currentCell7 = The.Player.CurrentCell;
															if (currentCell7 != null && currentCell7.ParentZone == zone)
															{
																The.ZoneManager.SetActiveZone(zone);
															}
															The.ZoneManager.ProcessGoToPartyLeader();
														}
													}
												}
											}
											else if (!The.Player.CurrentCell.ParentZone.IsWorldMap() && The.Player.GetTotalConfusion() <= 0 && (!Options.AskAutostair || Popup.ShowYesNoCancel("Would you like to walk to the nearest stairway up?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes))
											{
												AutoAct.Setting = "<";
												return;
											}
										}
									}
									else
									{
										if (!(text3 == "CmdMoveW"))
										{
											goto IL_4989;
										}
										AutoAct.ClearAutoMoveStop();
										The.Player.Move("W", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
										this.RenderBase(false, false);
									}
								}
								else if (num8 != 887650942U)
								{
									if (num8 != 1039527375U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdAbilities"))
									{
										goto IL_4989;
									}
									The.Player.ModIntProperty("HasAccessedAbilities", 1, false);
									string text6 = AbilityManager.Show(The.Player);
									if (!text6.IsNullOrEmpty())
									{
										if (The.Player.OnWorldMap() && !AbilityManager.IsWorldMapUsable(text6))
										{
											The.Player.ShowFailure("You cannot do that on the world map.");
										}
										else
										{
											CommandEvent.Send(The.Player, text6, null, null, 0, false, false, null);
										}
									}
								}
								else
								{
									if (!(text3 == "CmdShowSidebarMessages"))
									{
										goto IL_4989;
									}
									Sidebar.SidebarState++;
									if (Sidebar.SidebarState >= 4)
									{
										Sidebar.SidebarState = 0;
									}
								}
							}
							else if (num8 <= 1166056283U)
							{
								if (num8 <= 1096461858U)
								{
									if (num8 != 1071403817U)
									{
										if (num8 != 1096461858U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdMoveD"))
										{
											goto IL_4989;
										}
										Cell currentCell8 = The.Player.CurrentCell;
										if (currentCell8 != null && currentCell8.ParentZone.IsWorldMap())
										{
											The.Player.PullDown(true);
										}
										else if (currentCell8.FireEvent(XRL.World.Event.New("ClimbDown", "GO", The.Player)) && !currentCell8.ParentZone.IsWorldMap() && The.Player.GetTotalConfusion() <= 0 && (!Options.AskAutostair || Popup.ShowYesNoCancel("Would you like to walk to the nearest stairway down?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes))
										{
											AutoAct.Setting = ">";
											return;
										}
									}
									else
									{
										if (!(text3 == "CmdTalk"))
										{
											goto IL_4989;
										}
										string text7 = PickDirection.ShowPicker("Talk");
										if (text7 != null)
										{
											Cell cellFromDirection = The.Player.CurrentCell.GetCellFromDirection(text7, true);
											if (cellFromDirection != null)
											{
												XRL.World.GameObject firstObjectWithPart2 = cellFromDirection.GetFirstObjectWithPart("ConversationScript", (XRL.World.GameObject GO) => !GO.IsPlayer() && (GO.Render == null || GO.Render.Visible));
												if (firstObjectWithPart2 == null || !firstObjectWithPart2.GetPart<ConversationScript>().AttemptConversation(true, null, null))
												{
													XRL.World.GameObject firstObjectWithRegisteredEvent = cellFromDirection.GetFirstObjectWithRegisteredEvent("ObjectTalking", (XRL.World.GameObject GO) => !GO.IsPlayer());
													if ((firstObjectWithRegisteredEvent == null || !firstObjectWithRegisteredEvent.FireEvent("ObjectTalking")) && firstObjectWithPart2 != null)
													{
														firstObjectWithPart2.GetPart<ConversationScript>().AttemptConversation(false, null, null);
													}
												}
											}
										}
									}
								}
								else if (num8 != 1113239477U)
								{
									if (num8 != 1166056283U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdDismemberLimb"))
									{
										goto IL_4989;
									}
									Body body = The.Player.Body;
									BodyPart dismemberableBodyPart = Axe_Dismember.GetDismemberableBodyPart(The.Player, null, null, false, false);
									if (dismemberableBodyPart != null)
									{
										body.Dismember(dismemberableBodyPart, null, null, false, false, null);
									}
								}
								else
								{
									if (!(text3 == "CmdMoveE"))
									{
										goto IL_4989;
									}
									AutoAct.ClearAutoMoveStop();
									The.Player.Move("E", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
									if (Options.DrawStepImmediately)
									{
										this.RenderBase(false, false);
									}
								}
							}
							else if (num8 <= 1213710488U)
							{
								if (num8 != 1197127572U)
								{
									if (num8 != 1213710488U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdQuests"))
									{
										goto IL_4989;
									}
									Screens.CurrentScreen = 5;
									Screens.Show(The.Player);
								}
								else
								{
									if (!(text3 == "CmdMoveN"))
									{
										goto IL_4989;
									}
									AutoAct.ClearAutoMoveStop();
									The.Player.Move("N", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
									if (Options.DrawStepImmediately)
									{
										this.RenderBase(false, false);
									}
								}
							}
							else if (num8 != 1241439802U)
							{
								if (num8 != 1249711583U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdGetFromSW"))
								{
									goto IL_4989;
								}
								The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "SW"));
							}
							else
							{
								if (!(text3 == "CmdFactions"))
								{
									goto IL_4989;
								}
								Screens.ShowPopup("Factions", The.Player);
							}
						}
						else if (num8 <= 1657699442U)
						{
							if (num8 <= 1395691133U)
							{
								if (num8 <= 1318581031U)
								{
									if (num8 != 1284135801U)
									{
										if (num8 != 1308276319U)
										{
											if (num8 != 1318581031U)
											{
												goto IL_4989;
											}
											if (!(text3 == "CmdMoveTo"))
											{
												goto IL_4989;
											}
											Cell currentCell9 = The.Player.CurrentCell;
											if (currentCell9 != null)
											{
												Cell cell7 = PickTarget.ShowPicker(PickTarget.PickStyle.EmptyCell, 1, 9999, currentCell9.X, currentCell9.Y, false, AllowVis.OnlyExplored, null, null, null, null, "Move where?", false, true);
												if (cell7 != null && cell7 != currentCell9)
												{
													if (!cell7.IsAdjacentTo(currentCell9, true))
													{
														this.PlayerAvoid.Clear();
														AutoAct.Setting = "M" + cell7.X.ToString() + "," + cell7.Y.ToString();
														return;
													}
													string directionFromCell4 = currentCell9.GetDirectionFromCell(cell7, false);
													if (!directionFromCell4.IsNullOrEmpty() && directionFromCell4 != "." && directionFromCell4 != "?")
													{
														The.Player.Move(directionFromCell4, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
													}
												}
											}
										}
										else
										{
											if (!(text3 == "CmdAttackSE"))
											{
												goto IL_4989;
											}
											The.Player.AttackDirection("SE", true);
										}
									}
									else
									{
										if (!(text3 == "CmdWaitUntilPartyHealed"))
										{
											goto IL_4989;
										}
										if (!AutoAct.ShouldHostilesInterrupt("r", null, false, true, true))
										{
											AutoAct.Setting = "r+";
											actionManager.RestingUntilHealed = true;
											actionManager.RestingUntilHealedCount = 0;
											actionManager.RestingUntilHealedCompanions.Clear();
											actionManager.RestingUntilHealedHaveCompanions = false;
											SoundManager.PlayUISound("sfx_wait", 0.1f, false, true, SoundRequest.SoundEffectType.None);
											The.Player.PassTurn();
											Loading.SetLoadingStatus("Resting until party healed...", false);
										}
									}
								}
								else if (num8 <= 1346179231U)
								{
									if (num8 != 1337555300U)
									{
										if (num8 != 1346179231U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdAutoExplore"))
										{
											goto IL_4989;
										}
										if (!The.Player.CurrentZone.IsWorldMap())
										{
											The.Player.CurrentZone.FlushNavigationCaches();
											AutoAct.Setting = "?";
											return;
										}
										break;
									}
									else
									{
										if (!(text3 == "CmdQuit"))
										{
											goto IL_4989;
										}
										if (Popup.ShowYesNoCancel("Are you sure you want to quit?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
										{
											DialogResult dialogResult = DialogResult.No;
											if (!The.Game.DontSaveThisIsAReplay)
											{
												Popup.ShowYesNoCancel("Do you want to save first?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
											}
											if (dialogResult == DialogResult.Yes)
											{
												ZoneManager.instance.CheckCached(true, true);
												this.SaveGame("Primary");
												this.Game.DeathReason = "<nodeath>";
												this.Game.DeathCategory = "exit";
												this.Game.forceNoDeath = true;
												this.Game.Running = false;
												return;
											}
											if (dialogResult == DialogResult.No)
											{
												bool flag4 = true;
												if (!Options.DisablePermadeath && !CheckpointingSystem.IsCheckpointingEnabled() && !The.Game.DontSaveThisIsAReplay)
												{
													flag4 = false;
													string text8 = Popup.AskString("If you quit without saving, you will lose all your unsaved progress. Are you sure you want to QUIT and LOSE YOUR PROGRESS?\n\nType 'ABANDON' to confirm.", "", "Sounds/UI/ui_notification", null, "ABANDON", 7, 0, false, true, new bool?(false));
													if (!text8.IsNullOrEmpty() && text8.ToUpper() == "ABANDON")
													{
														flag4 = true;
													}
												}
												if (flag4)
												{
													if (EndGame.IsAnyEnding && !The.Game.DontSaveThisIsAReplay)
													{
														CodaSystem.EndGamePrompt();
														return;
													}
													if (CheckpointingSystem.IsCheckpointingEnabled() || The.Game.DontSaveThisIsAReplay)
													{
														this.Game.DeathReason = "<nodeath>";
														this.Game.DeathCategory = "exit";
														this.Game.forceNoDeath = true;
													}
													else
													{
														this.Game.DeathReason = "You abandoned all hope.";
														JournalAPI.AddAccomplishment(string.Concat(new string[]
														{
															"On the ",
															XRL.World.Calendar.GetDay(),
															" of ",
															XRL.World.Calendar.GetMonth(),
															", you abandoned all hope."
														}), null, null, null, "general", MuralCategory.Generic, MuralWeight.Nil, null, -1L, true);
													}
													this.Game.Running = false;
													The.Player.Energy.BaseValue = 0;
													return;
												}
											}
										}
									}
								}
								else if (num8 != 1387625829U)
								{
									if (num8 != 1395691133U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdAutoMoveSE"))
									{
										goto IL_4989;
									}
									The.Player.AutoMove("SE");
								}
								else
								{
									if (!(text3 == "CmdGet"))
									{
										goto IL_4989;
									}
									The.Player.FireEvent("CommandGet");
								}
							}
							else if (num8 <= 1551708725U)
							{
								if (num8 <= 1495350577U)
								{
									if (num8 != 1486337255U)
									{
										if (num8 != 1495350577U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdShowReachability"))
										{
											goto IL_4989;
										}
										Zone activeZone = The.ZoneManager.ActiveZone;
										ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
										for (int num18 = 0; num18 < activeZone.Height; num18++)
										{
											for (int num19 = 0; num19 < activeZone.Width; num19++)
											{
												scrapBuffer.Goto(num19, num18);
												if (activeZone.IsReachable(num19, num18))
												{
													scrapBuffer.Write(".", true, false, false, null, -1);
												}
												else
												{
													scrapBuffer.Write("#", true, false, false, null, -1);
												}
											}
										}
										Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
										Keyboard.getch();
									}
									else
									{
										if (!(text3 == "CmdToggleAnimation"))
										{
											goto IL_4989;
										}
										this.EnableAnimation = !this.EnableAnimation;
									}
								}
								else if (num8 != 1525541682U)
								{
									if (num8 != 1551708725U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdGetFromSE"))
									{
										goto IL_4989;
									}
									The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "SE"));
								}
								else
								{
									if (!(text3 == "CmdWaitUntilMorning"))
									{
										goto IL_4989;
									}
									if (!AutoAct.ShouldHostilesInterrupt("z", null, false, true, true))
									{
										AutoAct.Setting = "z";
										SoundManager.PlayUISound("sfx_wait", 0.1f, false, true, SoundRequest.SoundEffectType.None);
										The.Player.PassTurn();
										Loading.SetLoadingStatus("Resting until morning...", false);
									}
								}
							}
							else if (num8 <= 1610273461U)
							{
								if (num8 != 1580649543U)
								{
									if (num8 != 1610273461U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdAttackSW"))
									{
										goto IL_4989;
									}
									The.Player.AttackDirection("SW", true);
								}
								else
								{
									if (!(text3 == "CmdFire"))
									{
										goto IL_4989;
									}
									bool flag5 = false;
									bool flag6 = false;
									string text9 = null;
									List<XRL.World.GameObject> missileWeapons = The.Player.GetMissileWeapons(null, null);
									XRL.World.GameObject gameObject9 = null;
									if (missileWeapons != null && missileWeapons.Count > 0)
									{
										int num20 = 0;
										int count6 = missileWeapons.Count;
										while (num20 < count6)
										{
											MissileWeapon missileWeapon;
											if (missileWeapons[num20].TryGetPart<MissileWeapon>(out missileWeapon))
											{
												flag6 = true;
												if (missileWeapon.ReadyToFire())
												{
													flag5 = true;
													break;
												}
												if (text9 == null)
												{
													text9 = missileWeapon.GetNotReadyToFireMessage();
													gameObject9 = missileWeapons[num20];
												}
											}
											num20++;
										}
									}
									if (!flag6)
									{
										Popup.ShowFail("You do not have a missile weapon equipped!", true, true, true);
									}
									else if (!flag5)
									{
										SoundManager.PlaySound((gameObject9 != null) ? gameObject9.GetSoundTag("NoAmmoSound", null) : null, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
										Popup.ShowFail(text9 ?? ("You need to reload! (" + ControlManager.getCommandInputDescription("CmdReload", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + ")"), true, true, true);
									}
									else
									{
										Combat.FireMissileWeapon(The.Player, null, null, FireType.Normal, null, 0, 0, 90, null, false);
									}
								}
							}
							else if (num8 != 1630577799U)
							{
								if (num8 != 1657699442U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdWait"))
								{
									goto IL_4989;
								}
								int num21 = 1000;
								if (The.Player.IsAflame())
								{
									if (Firefighting.AttemptFirefighting(The.Player, The.Player, 1000, true, false))
									{
										num21 = 0;
									}
								}
								else if (The.Player.IsFrozen())
								{
									The.Player.TemperatureChange(20, null, false, false, false, false, 0, null, null);
								}
								else
								{
									The.Player.Physics.Search();
								}
								The.Player.FireEvent("PassedTurn");
								if (num21 > 0)
								{
									SoundManager.PlayUISound("sfx_wait", 0.1f, false, true, SoundRequest.SoundEffectType.None);
									The.Player.PassTurn();
								}
							}
							else
							{
								if (!(text3 == "CmdAutoMoveSW"))
								{
									goto IL_4989;
								}
								The.Player.AutoMove("SW");
							}
						}
						else if (num8 <= 2012072022U)
						{
							if (num8 <= 1766452597U)
							{
								if (num8 != 1759960786U)
								{
									if (num8 != 1760407737U)
									{
										if (num8 != 1766452597U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdSystemMenu"))
										{
											goto IL_4989;
										}
										int num22;
										bool flag7;
										int num23;
										for (;;)
										{
											num22 = 0;
											flag7 = CheckpointingSystem.IsCheckpointingEnabled();
											bool flag8 = The.Player.HasEffect(typeof(WakingDream));
											bool transient = this.Game.Transient;
											string text10 = ((flag7 && !flag8) ? "Quit Without Saving" : "Abandon Character");
											string text11 = (flag7 ? "QUIT" : "ABANDON");
											string[] array11;
											char[] array12;
											if (transient)
											{
												array11 = new string[] { "Control Mapping", "Options", "Game Info", "End Test" };
												array12 = new char[] { 'c', 'o', 'g', 'a' };
											}
											else if (flag7)
											{
												num22 = 2;
												if (CheckpointingSystem.IsPlayerInCheckpoint())
												{
													array11 = new string[] { "Set Checkpoint", "&KRestore Checkpoint", "Control Mapping", "Options", "Game Info", "Save and Quit", text10 };
													array12 = new char[] { 'k', 'r', 'c', 'o', 'g', 's', 'q' };
												}
												else
												{
													array11 = new string[] { "&KSet Checkpoint", "Restore Checkpoint", "Control Mapping", "Options", "Game Info", "Save and Quit", text10 };
													array12 = new char[] { 'k', 'r', 'c', 'o', 'g', 's', 'q' };
												}
											}
											else
											{
												array11 = new string[] { "Control Mapping", "Options", "Game Info", "Save and Quit", text10 };
												array12 = new char[] { 'c', 'o', 'g', 's', 'a' };
											}
											num23 = Popup.PickOption("", null, "", "Sounds/UI/ui_notification", array11, array12, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
											if (transient && num23 == 3)
											{
												num23 = 4;
											}
											if (num23 == 4 + num22)
											{
												bool flag9 = true;
												if (flag8)
												{
													goto Block_389;
												}
												if (The.Game.DontSaveThisIsAReplay || transient)
												{
													goto Block_390;
												}
												if (!Options.DisablePermadeath)
												{
													flag9 = false;
													string text12 = "\n\nType '" + text11 + "' to confirm.";
													string text13 = string.Empty;
													if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
													{
														text12 = string.Empty;
														text13 = null;
													}
													string text14;
													if (!flag7)
													{
														text14 = Popup.AskString("If you quit without saving, you will lose all your progress and your character will be lost. Are you sure you want to QUIT and LOSE YOUR PROGRESS?" + text12, text13, "Sounds/UI/ui_notification", null, text11, text11.Length, 0, false, true, null);
													}
													else
													{
														text14 = Popup.AskString("If you quit without saving, you will lose all your unsaved progress. Are you sure you want to QUIT and LOSE YOUR PROGRESS?" + text12, text13, "Sounds/UI/ui_notification", null, text11, text11.Length, 0, false, true, null);
													}
													if (!text14.IsNullOrEmpty() && text14.ToUpper() == text11)
													{
														flag9 = true;
													}
												}
												if (flag9)
												{
													goto Block_396;
												}
											}
											if (num23 == 0 && flag7)
											{
												if (CheckpointingSystem.IsPlayerInCheckpoint())
												{
													CheckpointingSystem.DoCheckpoint();
												}
												else
												{
													Popup.Show("You can only set your checkpoint in settlements.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
												}
											}
											if (num23 == 1 && flag7)
											{
												if (CheckpointingSystem.IsPlayerInCheckpoint())
												{
													Popup.Show("You can only restore your checkpoint outside settlements.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
												}
												else if (Popup.ShowYesNoCancel("Are you sure you want to restore your checkpoint?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
												{
													CheckpointingSystem.QueueRestore();
												}
											}
											if (num23 != 2 + num22)
											{
												break;
											}
											if (!this.Game.HasStringGameState("OriginalWorldSeed"))
											{
												Popup.ShowFail("This saved game predates world seed info.", true, true, true);
											}
											else
											{
												Popup.ShowBlockWithCopy(string.Concat(new string[]
												{
													"\n\n           ",
													The.Game.GetStringGameState("GameMode", ""),
													" mode.\n\n           Turn ",
													The.Game.Turns.ToString(),
													"\n\n          World seed: ",
													this.Game.GetStringGameState("OriginalWorldSeed", ""),
													"     \n\n\n   "
												}), " {{W|C}} - Copy seed  {{W|ESC}} - Exit ", "", this.Game.GetStringGameState("OriginalWorldSeed", ""), true);
											}
										}
										if (num23 == 3 + num22)
										{
											ZoneManager.instance.CheckCached(true, true);
											this.SaveGame("Primary");
											Popup.Show("Game saved!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
											this.Game.DeathReason = "<nodeath>";
											this.Game.DeathCategory = "exit";
											this.Game.forceNoDeath = true;
											this.Game.Running = false;
											return;
										}
										if (num23 == num22)
										{
											KeyMappingUI.Show();
										}
										if (num23 == 1 + num22)
										{
											OptionsUI.Show();
											goto IL_49CA;
										}
										goto IL_49CA;
										Block_389:
										The.Player.RemoveEffect<WakingDream>();
										return;
										Block_390:
										this.Game.DeathReason = "<nodeath>";
										this.Game.DeathCategory = "exit";
										this.Game.forceNoDeath = true;
										this.Game.Running = false;
										The.Player.Energy.BaseValue = 0;
										return;
										Block_396:
										if (EndGame.IsAnyEnding)
										{
											CodaSystem.EndGamePrompt();
											return;
										}
										if (flag7)
										{
											this.Game.DeathReason = "<nodeath>";
											this.Game.DeathCategory = "exit";
											this.Game.forceNoDeath = true;
										}
										else
										{
											this.Game.DeathReason = "You abandoned all hope.";
											this.Game.DeathCategory = "exit";
											JournalAPI.AddAccomplishment(string.Concat(new string[]
											{
												"On the ",
												XRL.World.Calendar.GetDay(),
												" of ",
												XRL.World.Calendar.GetMonth(),
												", you abandoned all hope."
											}), null, null, null, "general", MuralCategory.Generic, MuralWeight.Nil, null, -1L, true);
										}
										this.Game.Running = false;
										The.Player.Energy.BaseValue = 0;
										return;
									}
									else
									{
										if (!(text3 == "CmdNoclipU"))
										{
											goto IL_4989;
										}
										Cell cellFromDirection2 = The.Player.CurrentCell.GetCellFromDirection("U", false);
										if (cellFromDirection2 != null)
										{
											The.Player.SystemMoveTo(cellFromDirection2, null, false, true, false, false);
											The.ZoneManager.SetActiveZone(cellFromDirection2.ParentZone);
											The.ZoneManager.ProcessGoToPartyLeader();
										}
									}
								}
								else
								{
									if (!(text3 == "CmdShowSidebar"))
									{
										goto IL_4989;
									}
									Sidebar.Hidden = !Sidebar.Hidden;
								}
							}
							else if (num8 <= 1976032918U)
							{
								if (num8 != 1872504173U)
								{
									if (num8 != 1976032918U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdWalk"))
									{
										goto IL_4989;
									}
									Zone activeZone2 = The.ZoneManager.ActiveZone;
									if (activeZone2 != null && !activeZone2.IsWorldMap())
									{
										string text15 = PickDirection.ShowPicker("Walk");
										if (text15 == null)
										{
											break;
										}
										Cell currentCell10 = The.Player.CurrentCell;
										if (currentCell10 != null)
										{
											Cell cellFromDirection3 = currentCell10.GetCellFromDirection(text15, true);
											if (cellFromDirection3 != null)
											{
												if (cellFromDirection3.IsEmpty())
												{
													AutoAct.Setting = text15;
												}
												else
												{
													if (cellFromDirection3.HasObjectWithPart("Combat", (XRL.World.GameObject GO) => GO.IsHostileTowards(The.Player)))
													{
														Popup.ShowFail("You may not {{Y|w}}alk into a hostile creature!", true, true, true);
														goto IL_4A02;
													}
													AutoAct.Setting = text15;
												}
											}
										}
										if (AutoAct.Setting == ".")
										{
											AutoAct.Setting = ".20";
											return;
										}
										break;
									}
								}
								else if (!(text3 == "CmdNone"))
								{
									goto IL_4989;
								}
							}
							else if (num8 != 2007896696U)
							{
								if (num8 != 2012072022U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdNoclipD"))
								{
									goto IL_4989;
								}
								Cell cellFromDirection4 = The.Player.CurrentCell.GetCellFromDirection("D", false);
								if (cellFromDirection4 != null)
								{
									The.Player.SystemMoveTo(cellFromDirection4, null, false, true, false, false);
									The.ZoneManager.SetActiveZone(cellFromDirection4.ParentZone);
									The.ZoneManager.ProcessGoToPartyLeader();
								}
							}
							else if (!(text3 == "CmdZoomOut"))
							{
								goto IL_4989;
							}
						}
						else if (num8 <= 2043086145U)
						{
							if (num8 <= 2024880556U)
							{
								if (num8 != 2017037423U)
								{
									if (num8 != 2024880556U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdGetFromNE"))
									{
										goto IL_4989;
									}
									The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "NE"));
								}
								else
								{
									if (!(text3 == "CmdAttackCell"))
									{
										goto IL_4989;
									}
									Cell currentCell11 = The.Player.CurrentCell;
									if (currentCell11 != null)
									{
										Cell cell8 = PickTarget.ShowPicker(PickTarget.PickStyle.EmptyCell, 1, 1, currentCell11.X, currentCell11.Y, false, AllowVis.OnlyExplored, null, null, null, null, "Attack where?", true, true);
										if (cell8 != null && cell8 != currentCell11 && cell8.IsAdjacentTo(currentCell11, true))
										{
											string directionFromCell5 = currentCell11.GetDirectionFromCell(cell8, false);
											if (!directionFromCell5.IsNullOrEmpty() && directionFromCell5 != "." && directionFromCell5 != "?")
											{
												The.Player.AttackDirection(directionFromCell5, true);
											}
										}
									}
								}
							}
							else if (num8 != 2025476775U)
							{
								if (num8 != 2043086145U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdShowFPS"))
								{
									goto IL_4989;
								}
								this.ShowFPS = !this.ShowFPS;
							}
							else
							{
								if (!(text3 == "CmdMoveDirection"))
								{
									goto IL_4989;
								}
								string text16 = PickDirection.ShowPicker("Move in what direction?");
								if (!text16.IsNullOrEmpty() && text16 != "." && text16 != "?")
								{
									The.Player.Move(text16, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
								}
							}
						}
						else if (num8 <= 2314524661U)
						{
							if (num8 != 2166188786U)
							{
								if (num8 != 2314524661U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdGetFrom"))
								{
									goto IL_4989;
								}
								The.Player.FireEvent("CommandGetFrom");
							}
							else
							{
								if (!(text3 == "CmdMessageHistory"))
								{
									goto IL_4989;
								}
								The.Game.Player.Messages.Show();
							}
						}
						else if (num8 != 2326877698U)
						{
							if (num8 != 2453832838U)
							{
								goto IL_4989;
							}
							if (!(text3 == "CmdAttackNE"))
							{
								goto IL_4989;
							}
							The.Player.AttackDirection("NE", true);
						}
						else
						{
							if (!(text3 == "CmdGetFromNW"))
							{
								goto IL_4989;
							}
							The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "NW"));
						}
					}
					else if (num8 <= 3394979603U)
					{
						if (num8 <= 3048746753U)
						{
							if (num8 <= 2692137152U)
							{
								if (num8 <= 2523382224U)
								{
									if (num8 != 2464398136U)
									{
										if (num8 != 2486128106U)
										{
											if (num8 != 2523382224U)
											{
												goto IL_4989;
											}
											if (!(text3 == "CmdWait20"))
											{
												goto IL_4989;
											}
											if (!AutoAct.ShouldHostilesInterrupt(".", null, false, true, true))
											{
												AutoAct.Setting = ".20";
												SoundManager.PlayUISound("sfx_wait", 0.1f, false, true, SoundRequest.SoundEffectType.None);
												The.Player.PassTurn();
												Loading.SetLoadingStatus("Waiting...", false);
											}
										}
										else
										{
											if (!(text3 == "CmdCharacter"))
											{
												goto IL_4989;
											}
											Screens.CurrentScreen = 1;
											Screens.Show(The.Player);
										}
									}
									else
									{
										if (!(text3 == "CmdSaveAndQuit"))
										{
											goto IL_4989;
										}
										if (Popup.ShowYesNoCancel("Are you sure you want to save and quit?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
										{
											ZoneManager.instance.CheckCached(true, true);
											this.SaveGame("Primary");
											this.Game.Running = false;
											return;
										}
									}
								}
								else if (num8 <= 2686561804U)
								{
									if (num8 != 2619451328U)
									{
										if (num8 != 2686561804U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdAttackW"))
										{
											goto IL_4989;
										}
										The.Player.AttackDirection("W", true);
									}
									else
									{
										if (!(text3 == "CmdAttackS"))
										{
											goto IL_4989;
										}
										The.Player.AttackDirection("S", true);
									}
								}
								else if (num8 != 2688719504U)
								{
									if (num8 != 2692137152U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdShowEffects"))
									{
										goto IL_4989;
									}
									The.Player.ShowActiveEffects();
								}
								else
								{
									if (!(text3 == "CmdAttackNW"))
									{
										goto IL_4989;
									}
									The.Player.AttackDirection("NW", true);
								}
							}
							else if (num8 <= 2880370807U)
							{
								if (num8 <= 2746749611U)
								{
									if (num8 != 2720117042U)
									{
										if (num8 != 2746749611U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdUseNE"))
										{
											goto IL_4989;
										}
										this.PerformSmartUse("NE", 0);
									}
									else
									{
										if (!(text3 == "CmdAttackU"))
										{
											goto IL_4989;
										}
										if (The.Player.CurrentCell.HasObjectWithPart("StairsUp"))
										{
											The.Player.AttackDirection("U", true);
										}
									}
								}
								else if (num8 != 2783992323U)
								{
									if (num8 != 2880370807U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdToggleMessageVerbosity"))
									{
										goto IL_4989;
									}
									this.Game.Player.Messages.Terse = !this.Game.Player.Messages.Terse;
									if (this.Game.Player.Messages.Terse)
									{
										MessageQueue.AddPlayerMessage("Set Terse messages", null, true);
									}
									if (!this.Game.Player.Messages.Terse)
									{
										MessageQueue.AddPlayerMessage("Set Verbose messages", null, true);
									}
								}
								else
								{
									if (!(text3 == "CmdSkillsPowers"))
									{
										goto IL_4989;
									}
									Screens.CurrentScreen = 0;
									Screens.Show(The.Player);
								}
							}
							else if (num8 <= 2988558946U)
							{
								if (num8 != 2972662296U)
								{
									if (num8 != 2988558946U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdAttackE"))
									{
										goto IL_4989;
									}
									The.Player.AttackDirection("E", true);
								}
								else
								{
									if (!(text3 == "CmdMoveToPointOfInterest"))
									{
										goto IL_4989;
									}
									List<PointOfInterest> for2 = GetPointsOfInterestEvent.GetFor(The.Player, null);
									if (for2 == null || for2.Count <= 0)
									{
										Popup.ShowFail("You haven't found any points of interest nearby.", true, true, true);
										return;
									}
									for2.Sort(new Comparison<PointOfInterest>(PointOfInterest.Compare));
									string[] array13 = new string[for2.Count];
									char[] array14 = new char[for2.Count];
									IRenderable[] array15 = new IRenderable[for2.Count];
									char c6 = 'a';
									int num24 = 0;
									int count7 = for2.Count;
									while (num24 < count7)
									{
										PointOfInterest pointOfInterest = for2[num24];
										array13[num24] = pointOfInterest.GetDisplayName(The.Player);
										char[] array16 = array14;
										int num25 = num24;
										int num26;
										if (c6 > 'z')
										{
											num26 = 32;
										}
										else
										{
											c6 = (char)((num26 = (int)c6) + 1);
										}
										array16[num25] = num26;
										array15[num24] = pointOfInterest.GetIcon();
										num24++;
									}
									int num27 = Popup.PickOption("Go to which point of interest?", null, "", "Sounds/UI/ui_notification", array13, array14, array15, null, null, null, null, 1, 78, 0, -1, true, true, false, true, false, null, "CmdMoveToPointOfInterestMenu");
									if (num27 >= 0)
									{
										this.PlayerAvoid.Clear();
										for2[num27].NavigateTo(The.Player);
									}
								}
							}
							else if (num8 != 3005336565U)
							{
								if (num8 != 3048746753U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdUseNW"))
								{
									goto IL_4989;
								}
								this.PerformSmartUse("NW", 0);
							}
							else
							{
								if (!(text3 == "CmdAttackD"))
								{
									goto IL_4989;
								}
								if (The.Player.CurrentCell.HasObjectWithPart("StairsDown"))
								{
									The.Player.AttackDirection("D", true);
								}
							}
						}
						else if (num8 <= 3298356013U)
						{
							if (num8 <= 3115125612U)
							{
								if (num8 != 3052145252U)
								{
									if (num8 != 3106002279U)
									{
										if (num8 != 3115125612U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdAutoAttack"))
										{
											goto IL_4989;
										}
										if (The.Player.Target == null)
										{
											Popup.ShowFail("You don't have a target.", true, true, true);
											return;
										}
										if (The.Player.IsConfused && The.Player.GetFuriousConfusion() <= 0)
										{
											Popup.ShowFail("You cannot autoattack while you are confused.", true, true, true);
											return;
										}
										AutoAct.Setting = "a";
										return;
									}
									else
									{
										if (!(text3 == "CmdAttackN"))
										{
											goto IL_4989;
										}
										The.Player.AttackDirection("N", true);
									}
								}
								else
								{
									if (!(text3 == "CmdUseSW"))
									{
										goto IL_4989;
									}
									this.PerformSmartUse("SW", 0);
								}
							}
							else if (num8 <= 3168017815U)
							{
								if (num8 != 3146321582U)
								{
									if (num8 != 3168017815U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdOpen"))
									{
										goto IL_4989;
									}
									string text17 = PickDirection.ShowPicker("Open");
									if (text17 != null)
									{
										Cell cellFromDirection5 = The.Player.CurrentCell.GetCellFromDirection(text17, true);
										if (cellFromDirection5 != null)
										{
											List<XRL.World.GameObject> objectsWithRegisteredEvent = cellFromDirection5.GetObjectsWithRegisteredEvent("Open");
											if (objectsWithRegisteredEvent.Count == 1)
											{
												objectsWithRegisteredEvent[0].FireEvent(XRL.World.Event.New("Open", "Opener", The.Player));
											}
											else if (objectsWithRegisteredEvent.Count > 1)
											{
												XRL.World.GameObject gameObject10 = Popup.PickGameObject("Open", objectsWithRegisteredEvent, true, false, true, null, 0, false);
												if (gameObject10 != null)
												{
													gameObject10.FireEvent(XRL.World.Event.New("Open", "Opener", The.Player));
												}
											}
										}
									}
								}
								else
								{
									if (!(text3 == "CmdTarget"))
									{
										goto IL_4989;
									}
									Cell currentCell12 = The.Player.CurrentCell;
									Cell cell9 = PickTarget.ShowPicker(PickTarget.PickStyle.Burst, 0, 9999, currentCell12.X, currentCell12.Y, true, AllowVis.OnlyVisible, null, null, null, null, "Pick Target", false, true);
									if (cell9 != null)
									{
										SoundManager.PlayUISound("ui_cursor_scroll", 1f, false, true, SoundRequest.SoundEffectType.None);
										Sidebar.CurrentTarget = cell9.GetCombatTarget(The.Player, true, false, false, 5, null, null, null, null, null, true, false, null);
									}
								}
							}
							else if (num8 != 3281415317U)
							{
								if (num8 != 3298356013U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdThrow"))
								{
									goto IL_4989;
								}
								Combat.ThrowWeapon(The.Player, null, null, null, null);
							}
							else
							{
								if (!(text3 == "CmdEquipment"))
								{
									goto IL_4989;
								}
								InventoryAndEquipmentStatusScreen.EnteringFromEquipmentSide = true;
								Screens.CurrentScreen = 3;
								Screens.Show(The.Player);
							}
						}
						else if (num8 <= 3339555444U)
						{
							if (num8 <= 3315211544U)
							{
								if (num8 != 3304103251U)
								{
									if (num8 != 3315211544U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdLook"))
									{
										goto IL_4989;
									}
									if (The.Player.GetTotalConfusion() > 0)
									{
										if (The.Player.GetFuriousConfusion() > 0)
										{
											Popup.ShowFail("You cannot examine things while you are enraged.", true, true, true);
										}
										else
										{
											Popup.ShowFail("You cannot examine things while you are confused.", true, true, true);
										}
									}
									else
									{
										Cell currentCell13 = The.Player.CurrentCell;
										Look.ShowLooker(0, currentCell13.X, currentCell13.Y);
									}
								}
								else
								{
									if (!(text3 == "CmdWishMenu"))
									{
										goto IL_4989;
									}
									WishMenu.OpenWishMenu();
								}
							}
							else if (num8 != 3317848865U)
							{
								if (num8 != 3339555444U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdWaitN"))
								{
									goto IL_4989;
								}
								try
								{
									if (!AutoAct.ShouldHostilesInterrupt(".", null, false, true, true))
									{
										int highestActivatedAbilityCooldownRounds = The.Player.GetHighestActivatedAbilityCooldownRounds();
										int num28 = Math.Max(XRLCore.lastWait, highestActivatedAbilityCooldownRounds);
										int? num29 = Popup.AskNumber("How many turns would you like to wait?", "Sounds/UI/ui_notification", "", num28, 0, int.MaxValue);
										if (num29 != null)
										{
											int value = num29.Value;
											if (value > 0)
											{
												if (value != highestActivatedAbilityCooldownRounds)
												{
													XRLCore.lastWait = value;
												}
												AutoAct.Setting = "." + value.ToString();
												SoundManager.PlayUISound("sfx_wait", 0.1f, false, true, SoundRequest.SoundEffectType.None);
												The.Player.PassTurn();
												Loading.SetLoadingStatus(The.StringBuilder.Append("Waiting for ").Append(value.Things("turn", null)).Append("...")
													.ToString(), false);
											}
											else
											{
												MessageQueue.AddPlayerMessage(value.ToString() + " is not a valid number of turns to wait.", 'K', true);
											}
										}
									}
									goto IL_49CA;
								}
								catch (Exception ex2)
								{
									MetricsManager.LogError("Encountered exception inside CmdWaitN.", ex2);
									goto IL_49CA;
								}
								goto IL_3336;
							}
							else
							{
								if (!(text3 == "CmdRegenerateLimb"))
								{
									goto IL_4989;
								}
								RegenerateLimbEvent.Send(The.Player, null, null, false, false, true, true, null, null, null, null, null);
							}
						}
						else if (num8 <= 3354142394U)
						{
							if (num8 != 3346105387U)
							{
								if (num8 != 3354142394U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdUseSE"))
								{
									goto IL_4989;
								}
								this.PerformSmartUse("SE", 0);
							}
							else if (!(text3 == "CmdZoomIn"))
							{
								goto IL_4989;
							}
						}
						else if (num8 != 3364682461U)
						{
							if (num8 != 3394979603U)
							{
								goto IL_4989;
							}
							if (!(text3 == "CmdMoveFarN"))
							{
								goto IL_4989;
							}
							goto IL_289F;
						}
						else
						{
							if (!(text3 == "CmdWaitUntilHealed"))
							{
								goto IL_4989;
							}
							if (!AutoAct.ShouldHostilesInterrupt("r", null, false, true, true))
							{
								AutoAct.Setting = "r";
								actionManager.RestingUntilHealed = true;
								actionManager.RestingUntilHealedCount = 0;
								SoundManager.PlayUISound("sfx_wait", 0.1f, false, true, SoundRequest.SoundEffectType.None);
								The.Player.PassTurn();
								Loading.SetLoadingStatus("Resting until healed...", false);
							}
						}
					}
					else if (num8 <= 3747209798U)
					{
						if (num8 <= 3545978174U)
						{
							if (num8 <= 3502031019U)
							{
								if (num8 != 3432823244U)
								{
									if (num8 != 3463410694U)
									{
										if (num8 != 3502031019U)
										{
											goto IL_4989;
										}
										if (!(text3 == "CmdLoad"))
										{
											goto IL_4989;
										}
										if (Options.AllowSaveLoad && Popup.ShowYesNoCancel("Load your last quick save?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
										{
											Task<bool> task = The.Game.QuickLoad();
											task.Wait();
											if (task.Result)
											{
												Sidebar.UpdateState();
												Loading.SetLoadingStatus("Game loaded!", false);
												Loading.SetLoadingStatus(null, false);
												return;
											}
										}
									}
									else
									{
										if (!(text3 == "CmdGetFromW"))
										{
											goto IL_4989;
										}
										The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "W"));
									}
								}
								else
								{
									if (!(text3 == "CmdUseN"))
									{
										goto IL_4989;
									}
									this.PerformSmartUse("N", 0);
								}
							}
							else if (num8 <= 3522434553U)
							{
								if (num8 != 3518050316U)
								{
									if (num8 != 3522434553U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdMoveFarSW"))
									{
										goto IL_4989;
									}
									goto IL_289F;
								}
								else
								{
									if (!(text3 == "CmdMoveFarNW"))
									{
										goto IL_4989;
									}
									goto IL_289F;
								}
							}
							else if (num8 != 3530521170U)
							{
								if (num8 != 3545978174U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdMoveFarE"))
								{
									goto IL_4989;
								}
								goto IL_289F;
							}
							else
							{
								if (!(text3 == "CmdGetFromS"))
								{
									goto IL_4989;
								}
								The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "S"));
							}
						}
						else if (num8 <= 3698297360U)
						{
							if (num8 <= 3617377053U)
							{
								if (num8 != 3584621920U)
								{
									if (num8 != 3617377053U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdUseE"))
									{
										goto IL_4989;
									}
									this.PerformSmartUse("E", 0);
								}
								else if (!(text3 == "CmdShowBrainlog"))
								{
									goto IL_4989;
								}
							}
							else if (num8 != 3637471611U)
							{
								if (num8 != 3698297360U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdGetFromE"))
								{
									goto IL_4989;
								}
								The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "E"));
							}
							else
							{
								if (!(text3 == "CmdXP"))
								{
									goto IL_4989;
								}
								XRL.World.GameObject player3 = The.Player;
								this.Game.Player.Body = null;
								Popup.Suppress = true;
								player3.AwardXP(250000, -1, 0, int.MaxValue, null, null, null, null, null, null);
								Popup.Suppress = false;
								XRLCore.ParticleManager.Banners.Clear();
								this.Game.Player.Body = player3;
							}
						}
						else if (num8 <= 3734575560U)
						{
							if (num8 != 3722118782U)
							{
								if (num8 != 3734575560U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdHelp"))
								{
									goto IL_4989;
								}
								XRLCore.Manual.ShowHelp("");
							}
							else
							{
								if (!(text3 == "CmdLastStatusPage"))
								{
									goto IL_4989;
								}
								Screens.Show(The.Player);
							}
						}
						else if (num8 != 3745827682U)
						{
							if (num8 != 3747209798U)
							{
								goto IL_4989;
							}
							if (!(text3 == "CmdSave"))
							{
								goto IL_4989;
							}
							if (Options.AllowSaveLoad && Popup.ShowYesNoCancel("Quick save the game?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
							{
								The.Game.QuickSave();
								Loading.SetLoadingStatus("Game saved!", false);
								Loading.SetLoadingStatus(null, false);
							}
						}
						else
						{
							if (!(text3 == "CmdMoveToEdge"))
							{
								goto IL_4989;
							}
							string text18 = PickDirection.ShowPicker("Move to which edge? (\u0018\u0019\u001a\u001b)");
							if (text18 == "N" || text18 == "S" || text18 == "E" || text18 == "W")
							{
								this.PlayerAvoid.Clear();
								AutoAct.Setting = "G" + text18;
								return;
							}
						}
					}
					else if (num8 <= 3847975316U)
					{
						if (num8 <= 3780864840U)
						{
							if (num8 != 3757321219U)
							{
								if (num8 != 3771360908U)
								{
									if (num8 != 3780864840U)
									{
										goto IL_4989;
									}
									if (!(text3 == "CmdMoveFarW"))
									{
										goto IL_4989;
									}
									goto IL_289F;
								}
								else
								{
									if (!(text3 == "CmdTinkering"))
									{
										goto IL_4989;
									}
									Screens.CurrentScreen = 7;
									Screens.Show(The.Player);
								}
							}
							else
							{
								if (!(text3 == "CmdMoveFarSE"))
								{
									goto IL_4989;
								}
								goto IL_289F;
							}
						}
						else if (num8 <= 3820047458U)
						{
							if (num8 != 3785153243U)
							{
								if (num8 != 3820047458U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdMoveFarNE"))
								{
									goto IL_4989;
								}
								goto IL_289F;
							}
							else
							{
								if (!(text3 == "CmdUseS"))
								{
									goto IL_4989;
								}
								this.PerformSmartUse("S", 0);
							}
						}
						else if (num8 != 3826753308U)
						{
							if (num8 != 3847975316U)
							{
								goto IL_4989;
							}
							if (!(text3 == "CmdMoveFarS"))
							{
								goto IL_4989;
							}
							goto IL_289F;
						}
						else
						{
							if (!(text3 == "CmdWish"))
							{
								goto IL_4989;
							}
							WishManager.AskWish();
						}
					}
					else if (num8 <= 3967878565U)
					{
						if (num8 <= 3854426517U)
						{
							if (num8 != 3852263719U)
							{
								if (num8 != 3854426517U)
								{
									goto IL_4989;
								}
								if (!(text3 == "CmdReplaceCell"))
								{
									goto IL_4989;
								}
								CommandReplaceCellEvent.Execute(The.Player);
							}
							else
							{
								if (!(text3 == "CmdUseW"))
								{
									goto IL_4989;
								}
								this.PerformSmartUse("W", 0);
							}
						}
						else if (num8 != 3882851169U)
						{
							if (num8 != 3967878565U)
							{
								goto IL_4989;
							}
							if (!(text3 == "CmdActiveEffects"))
							{
								goto IL_4989;
							}
							The.Player.ShowActiveEffects();
						}
						else
						{
							if (!(text3 == "CmdGetFromN"))
							{
								goto IL_4989;
							}
							The.Player.FireEvent(XRL.World.Event.New("CommandGetFrom", "Direction", "N"));
						}
					}
					else if (num8 <= 4170100426U)
					{
						if (num8 != 4117459870U)
						{
							if (num8 != 4170100426U)
							{
								goto IL_4989;
							}
							if (!(text3 == "CmdUse"))
							{
								goto IL_4989;
							}
							this.PerformSmartUse(text2, 100);
						}
						else
						{
							if (!(text3 == "CmdAutoMoveW"))
							{
								goto IL_4989;
							}
							The.Player.AutoMove("W");
						}
					}
					else if (num8 != 4184570346U)
					{
						if (num8 != 4268458441U)
						{
							goto IL_4989;
						}
						if (!(text3 == "CmdAutoMoveN"))
						{
							goto IL_4989;
						}
						The.Player.AutoMove("N");
					}
					else
					{
						if (!(text3 == "CmdAutoMoveS"))
						{
							goto IL_4989;
						}
						The.Player.AutoMove("S");
					}
					IL_49CA:
					Sidebar.UpdateState();
					Sidebar.Update();
					flag = true;
					this.UpdateOverlay();
					if (Thread.CurrentThread != XRLCore.CoreThread)
					{
						GameManager.runPlayerTurnOnUIThread = false;
						goto IL_4A02;
					}
					goto IL_4A02;
					IL_3336:
					if (!AutoAct.ShouldHostilesInterrupt(".", null, false, true, true))
					{
						AutoAct.Setting = ".100";
						SoundManager.PlayUISound("sfx_wait", 0.1f, false, true, SoundRequest.SoundEffectType.None);
						The.Player.PassTurn();
						Loading.SetLoadingStatus("Waiting 100 turns...", false);
						goto IL_49CA;
					}
					goto IL_49CA;
					IL_4989:
					if (The.Player == null)
					{
						goto IL_49CA;
					}
					if (The.Player.OnWorldMap() && !AbilityManager.IsWorldMapUsable(text3))
					{
						The.Player.ShowFailure("You cannot do that on the world map.");
						goto IL_49CA;
					}
					CommandEvent.Send(The.Player, text3, null, null, 0, false, false, null);
					goto IL_49CA;
					IL_289F:
					if (The.Player.OnWorldMap())
					{
						Popup.ShowFail("You cannot do that on the world map.", true, true, true);
						return;
					}
					this.PlayerAvoid.Clear();
					if (text3 == "CmdMoveFarN")
					{
						AutoAct.Setting = "GN";
						return;
					}
					if (text3 == "CmdMoveFarS")
					{
						AutoAct.Setting = "GS";
						return;
					}
					if (text3 == "CmdMoveFarE")
					{
						AutoAct.Setting = "GE";
						return;
					}
					if (text3 == "CmdMoveFarW")
					{
						AutoAct.Setting = "GW";
						return;
					}
					int num30;
					int num31;
					if (!(text3 == "CmdMoveFarNW"))
					{
						if (!(text3 == "CmdMoveFarNE"))
						{
							if (!(text3 == "CmdMoveFarSW"))
							{
								if (!(text3 == "CmdMoveFarSE"))
								{
									num30 = 40;
									num31 = 12;
								}
								else
								{
									num30 = 79;
									num31 = 24;
								}
							}
							else
							{
								num30 = 0;
								num31 = 24;
							}
						}
						else
						{
							num30 = 79;
							num31 = 0;
						}
					}
					else
					{
						num30 = 0;
						num31 = 0;
					}
					AutoAct.Setting = "M" + num30.ToString() + "," + num31.ToString();
					return;
				}
				else if (Thread.CurrentThread != XRLCore.CoreThread)
				{
					GameManager.runPlayerTurnOnUIThread = false;
				}
				IL_4A02:
				int num32 = 0;
				int count8 = XRLCore.OnEndPlayerTurnCallbacks.Count;
				while (num32 < count8)
				{
					XRLCore.OnEndPlayerTurnCallbacks[num32](this);
					num32++;
				}
				double totalMilliseconds2 = XRLCore.FrameTimer.Elapsed.TotalMilliseconds;
				if (The.Player.Energy.Value > 0)
				{
					if (num != The.Player.Energy.Value)
					{
						num = The.Player.Energy.Value;
						this.Game.Player.Messages.EndPlayerTurn();
					}
					this.RenderBase(false, false);
				}
				else
				{
					this.Game.Player.Messages.LastMessage = this.Game.Player.Messages.Messages.Count;
					this.RenderBase(false, false);
				}
				AmbientSoundsSystem.Update();
				if (flag)
				{
					Sidebar.SidebarTick += 1L;
					actionManager.UpdateMinimap();
					actionManager.RunCommands(false);
					int num33 = 0;
					int count9 = XRLCore.OnEndPlayerTurnSingleCallbacks.Count;
					while (num33 < count9)
					{
						XRLCore.OnEndPlayerTurnSingleCallbacks[num33](this);
						num33++;
					}
					flag = false;
					XRLCore.Core.Game.Player.EnqueuePlayerCell();
					XRLCore.Core.Game.Player.CheckPlayerLocations();
				}
				if (The.Player.Energy.Value >= 1000)
				{
					Keyboard.IdleWait();
					if (actionManager.SkipPlayerTurn)
					{
						actionManager.SkipPlayerTurn = false;
						return;
					}
				}
				if (Thread.CurrentThread != XRLCore.CoreThread)
				{
					GameManager.runPlayerTurnOnUIThread = false;
					return;
				}
			}
		}

		// Token: 0x06004255 RID: 16981 RVA: 0x0015CC9C File Offset: 0x0015AE9C
		public void RenderMapToBuffer(ScreenBuffer Buffer)
		{
			double totalMilliseconds = XRLCore.FrameTimer.Elapsed.TotalMilliseconds;
			this.Game.ZoneManager.ActiveZone.Render(Buffer);
			double totalMilliseconds2 = XRLCore.FrameTimer.Elapsed.TotalMilliseconds;
			TimeSpan elapsed = XRLCore.FrameTimer.Elapsed;
			XRLCore.CurrentFrameAccumulator = (double)((int)(elapsed.TotalMilliseconds / 16.666669845581055));
			XRLCore.CurrentFrame = (int)(elapsed.TotalMilliseconds % 1000.0) / 16;
			XRLCore.CurrentFrameLong = (int)(elapsed.TotalMilliseconds % 1000.0);
			XRLCore.CurrentFrame10 = (int)(elapsed.TotalMilliseconds % 10000.0) / 16;
			XRLCore.CurrentFrameLong10 = (int)(elapsed.TotalMilliseconds % 10000.0);
			if (Options.DisableTextAnimationEffects)
			{
				XRLCore.CurrentFrame = 8;
				XRLCore.CurrentFrameLong = 8;
				XRLCore.CurrentFrame10 = 8;
				XRLCore.CurrentFrameLong10 = 8;
			}
			if (Confusion.CurrentConfusionLevel > 0)
			{
				this.ConfusionShuffle(Buffer);
			}
			int i = 0;
			int count = XRLCore.AfterRenderCallbacks.Count;
			while (i < count)
			{
				XRLCore.AfterRenderCallbacks[i](this, Buffer);
				i++;
			}
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x0015CDC4 File Offset: 0x0015AFC4
		public void RenderBaseToBuffer(ScreenBuffer Buffer)
		{
			Sidebar.UpdateState();
			XRLCore.CludgeTargetRendered = false;
			XRLCore.RenderFloorTextures = !Options.DisableFloorTextures;
			if (GameManager.bDraw == 7)
			{
				return;
			}
			Zone activeZone = this.Game.ZoneManager.ActiveZone;
			if (activeZone == null)
			{
				return;
			}
			if (activeZone.IsWorldMap())
			{
				bool physics = The.Player.Physics != null;
				BeforeRenderEvent.Send(activeZone);
				if (physics)
				{
					activeZone.ExploreAll();
					activeZone.LightAll(LightLevel.Light);
					activeZone.VisAll();
				}
				activeZone.Render(Buffer);
				int i = 0;
				int count = XRLCore.AfterRenderCallbacks.Count;
				while (i < count)
				{
					XRLCore.AfterRenderCallbacks[i](this, XRLCore._Buffer);
					i++;
				}
				if (!XRLCore.CludgeTargetRendered && Sidebar.CurrentTarget != null && !Sidebar.CurrentTarget.IsVisible())
				{
					if (XRL.World.GameObject.Validate(Sidebar.CurrentTarget))
					{
						MessageQueue.AddPlayerMessage("You have lost sight of " + Sidebar.CurrentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
					}
					Sidebar.CurrentTarget = null;
				}
				Sidebar.Render(Buffer);
				if (this.AllowWorldMapParticles)
				{
					XRLCore.ParticleManager.Render(Buffer);
				}
				else
				{
					XRLCore.ParticleManager.Particles.Clear();
				}
			}
			else
			{
				activeZone.ClearLightMap();
				activeZone.ClearVisiblityMap();
				BeforeRenderEvent.Send(activeZone);
				Cell currentCell = The.Player.CurrentCell;
				if (currentCell != null && activeZone != null)
				{
					XRLCore.PlayerVisibilityRadius = The.Player.GetVisibilityRadius();
					activeZone.AddVisibility(currentCell.X, currentCell.Y, XRLCore.PlayerVisibilityRadius);
				}
				if (XRLCore.Core.VisAllToggle)
				{
					activeZone.VisAll();
					activeZone.ExploreAll();
					activeZone.LightAll(LightLevel.Light);
				}
				if (GameManager.bDraw == 11)
				{
					return;
				}
				activeZone.Render(Buffer);
				for (int j = 0; j < XRLCore.AfterRenderCallbacks.Count; j++)
				{
					XRLCore.AfterRenderCallbacks[j](this, XRLCore._Buffer);
				}
				if (GameManager.bDraw > 0 && GameManager.bDraw <= 20)
				{
					return;
				}
				if (GameManager.bDraw == 21)
				{
					return;
				}
				if (!XRLCore.CludgeTargetRendered && Sidebar.CurrentTarget != null && !Sidebar.CurrentTarget.IsVisible())
				{
					if (XRL.World.GameObject.Validate(Sidebar.CurrentTarget))
					{
						MessageQueue.AddPlayerMessage("You have lost sight of " + Sidebar.CurrentTarget.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
					}
					Sidebar.CurrentTarget = null;
				}
				XRLCore.ParticleManager.Render(Buffer);
				if (Confusion.CurrentConfusionLevel > 0)
				{
					this.ConfusionShuffle(Buffer);
				}
				if (GameManager.bDraw == 22)
				{
					return;
				}
				Sidebar.Render(Buffer);
			}
			XRLCore.RenderHiddenPlayer = true;
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x0015D078 File Offset: 0x0015B278
		public void ConfusionShuffle(ScreenBuffer Buffer)
		{
			this.ConfusionRng = new global::System.Random((int)XRLCore.Core.Game.Turns);
			for (int i = 0; i < Buffer.Height; i++)
			{
				for (int j = 0; j < Buffer.Width; j++)
				{
					if (The.Player != null && The.Player.CurrentCell != null && (j != The.Player.CurrentCell.X || i != The.Player.CurrentCell.Y))
					{
						int num = j + this.ConfusionRng.Next(-1, 1);
						int num2 = i + this.ConfusionRng.Next(-1, 1);
						if ((num != The.Player.Physics.CurrentCell.X || num2 != The.Player.Physics.CurrentCell.Y) && num >= 0 && num < Buffer.Width - 1 && num2 >= 0 && num2 < Buffer.Height - 1)
						{
							ConsoleChar consoleChar = Buffer[j, i];
							Buffer[j, i] = Buffer[num, num2];
							Buffer[num, num2] = consoleChar;
							if (The.Player != null && The.Player.GetFuriousConfusion() > 0)
							{
								Buffer[j, i].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Red, TextColor.Black));
								Buffer[num, num2].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Red, TextColor.Black));
							}
						}
					}
					if (The.Player != null && The.Player.GetFuriousConfusion() > 0)
					{
						Buffer[j, i].SetColorsFromOldCharCode(ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Red, TextColor.Black));
					}
				}
			}
		}

		/// <returns>true if the type delay executed to completion; false if interrupted via input.</returns>
		// Token: 0x06004258 RID: 16984 RVA: 0x0015D204 File Offset: 0x0015B404
		public bool RenderDelay(int Milliseconds, bool Interruptible = true, bool SkipInput = true, bool AllowHiddenPlayer = false)
		{
			long num = XRLCore.FrameTimer.ElapsedMilliseconds + (long)Milliseconds;
			while (XRLCore.FrameTimer.ElapsedMilliseconds < num)
			{
				if (Interruptible && Keyboard.kbhit())
				{
					if (SkipInput)
					{
						Keyboard.getch();
					}
					return false;
				}
				if (AllowHiddenPlayer)
				{
					XRLCore.RenderHiddenPlayer = false;
				}
				this.RenderBase(true, false);
			}
			return true;
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x0015D258 File Offset: 0x0015B458
		public string GenerateRandomPlayerName(string Type = "")
		{
			return NameMaker.MakeName(null, null, Type, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x0015D28C File Offset: 0x0015B48C
		public string GenerateRandomPlayerName(XRL.World.GameObject Player)
		{
			return NameMaker.MakeName(Player, null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
		}

		// Token: 0x0600425B RID: 16987 RVA: 0x0015D2C0 File Offset: 0x0015B4C0
		public void RenderBase(bool UpdateSidebar = true, bool DuringRestOkay = false)
		{
			if (Thread.CurrentThread == XRLCore.CoreThread)
			{
				GameManager.Instance.gameQueue.executeTasks();
			}
			if (GameManager.bDraw == 1)
			{
				return;
			}
			if (UpdateSidebar)
			{
				Sidebar.Update();
			}
			string setting = AutoAct.Setting;
			if (!DuringRestOkay && (setting == "r" || setting == "z" || (setting.Length > 0 && setting[0] == '.')))
			{
				return;
			}
			if (this.HostileWalkObjects.Count > 0)
			{
				this.HostileWalkObjects.Clear();
			}
			if (GameManager.bDraw == 2)
			{
				return;
			}
			XRLCore.ParticleManager.Frame();
			TimeSpan elapsed = XRLCore.FrameTimer.Elapsed;
			XRLCore.CurrentFrame = (int)(elapsed.TotalMilliseconds % 1000.0) / 16;
			XRLCore.CurrentFrameLong = (int)(elapsed.TotalMilliseconds % 1000.0);
			XRLCore.CurrentFrame10 = (int)(elapsed.TotalMilliseconds % 10000.0) / 16;
			XRLCore.CurrentFrameLong10 = (int)(elapsed.TotalMilliseconds % 10000.0);
			XRLCore.nFrame++;
			if (Options.DisableTextAnimationEffects)
			{
				XRLCore.CurrentFrame = 8;
				XRLCore.CurrentFrameLong = 8;
				XRLCore.CurrentFrame10 = 8;
				XRLCore.CurrentFrameLong10 = 8;
			}
			if (GameManager.bDraw == 3)
			{
				return;
			}
			this.RenderBaseToBuffer(XRLCore._Buffer);
			if (GameManager.bDraw > 0 && GameManager.bDraw <= 15)
			{
				return;
			}
			if (this.ShowFPS)
			{
				XRLCore._Buffer.Goto(0, 0);
				XRLCore._Buffer.Write(string.Concat(new string[]
				{
					"Frame: ",
					XRLCore.nFrame.ToString(),
					" GC:",
					GC.CollectionCount(0).ToString(),
					" M:",
					GC.GetTotalMemory(false).ToString()
				}), true, false, false, null, -1);
				XRLCore._Buffer.Goto(0, 1);
				XRLCore._Buffer.Write("Objects Created: " + GameObjectFactory.Factory.ObjectsCreated.ToString(), true, false, false, null, -1);
				XRLCore._Buffer.Goto(0, 2);
				XRLCore._Buffer.Write("Rendered Objects: " + XRLCore.Core.RenderedObjects.ToString(), true, false, false, null, -1);
				XRLCore._Buffer.Goto(0, 3);
				XRLCore._Buffer.Write("FPS: " + ((long)XRLCore.nFrame / (XRLCore.FrameTimer.ElapsedMilliseconds / 1000L)).ToString(), true, false, false, null, -1);
			}
			if (GameManager.bDraw == 22)
			{
				return;
			}
			XRLCore._Console.DrawBuffer(XRLCore._Buffer, ImposterManager.getImposterUpdateFrame(XRLCore._Buffer), false);
			if (GameManager.bDraw == 23)
			{
				return;
			}
			if (setting != "")
			{
				if (this.OldHostileWalkObjects != null && this.HostileWalkObjects != null)
				{
					int i = 0;
					int count = this.HostileWalkObjects.Count;
					while (i < count)
					{
						XRL.World.GameObject gameObject = this.HostileWalkObjects[i];
						if (!this.OldHostileWalkObjects.Contains(gameObject))
						{
							AutoAct.Interrupt(gameObject, true, false);
							this.OldHostileWalkObjects.Clear();
							this.OldHostileWalkObjects.AddRange(this.HostileWalkObjects);
							this.HostileWalkObjects.Clear();
							break;
						}
						i++;
					}
				}
				if (setting != "")
				{
					this.OldHostileWalkObjects.Clear();
					this.OldHostileWalkObjects.AddRange(this.HostileWalkObjects);
					this.HostileWalkObjects.Clear();
				}
			}
		}

		// Token: 0x0600425C RID: 16988 RVA: 0x0015D638 File Offset: 0x0015B838
		public void PerformSmartUse(string Direction = null, int MinPriority = 0)
		{
			Cell currentCell = The.Player.CurrentCell;
			List<Cell> list;
			if (Direction == null)
			{
				list = new List<Cell>(currentCell.GetAdjacentCells(true));
				list.Add(currentCell);
			}
			else
			{
				XRLCore.SmartUseCells.Clear();
				list = XRLCore.SmartUseCells;
				Cell cellFromDirection = currentCell.GetCellFromDirection(Direction, true);
				if (cellFromDirection != null)
				{
					list.Add(cellFromDirection);
				}
				list.Add(currentCell);
			}
			XRL.World.GameObject gameObject = null;
			bool flag = false;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				XRL.World.GameObject gameObject2 = SmartUse.FindPlayerSmartUseObject(list[i], MinPriority);
				if (gameObject2 != null)
				{
					if (gameObject != null)
					{
						flag = true;
					}
					else
					{
						gameObject = gameObject2;
					}
				}
				i++;
			}
			bool flag2 = false;
			if (list.Contains(currentCell))
			{
				flag2 = currentCell.HasObjectWithPart("Physics", new Predicate<XRL.World.GameObject>(SmartUse.CanPlayerTake));
			}
			XRLCore.SmartUseCells.Clear();
			if (gameObject != null)
			{
				if (!flag && !flag2)
				{
					SmartUse.PlayerPerformSmartUse(gameObject, 0);
					return;
				}
				string text = ((Direction != null) ? Direction : PickDirection.ShowPicker("Use"));
				if (text == null)
				{
					return;
				}
				Cell cellFromDirection2 = currentCell.GetCellFromDirection(text, true);
				if (cellFromDirection2 != null)
				{
					this.AttemptSmartUse(cellFromDirection2, 0);
					return;
				}
			}
			else
			{
				if (MinPriority > 0)
				{
					this.PerformSmartUse(Direction, 0);
					return;
				}
				The.Player.FireEvent(XRL.World.Event.New("CommandGet", "GetOne", false, "SmartUse", true));
			}
		}

		// Token: 0x0600425D RID: 16989 RVA: 0x0015D780 File Offset: 0x0015B980
		public void ResetGameBasedStaticCaches()
		{
			Type typeFromHandle = typeof(HasGameBasedStaticCacheAttribute);
			Type typeFromHandle2 = typeof(GameBasedStaticCacheAttribute);
			Type typeFromHandle3 = typeof(GameBasedCacheInitAttribute);
			foreach (FieldInfo fieldInfo in ModManager.GetFieldsWithAttribute(typeFromHandle2, typeFromHandle, true))
			{
				try
				{
					if (fieldInfo.IsStatic)
					{
						GameBasedStaticCacheAttribute customAttribute = fieldInfo.GetCustomAttribute<GameBasedStaticCacheAttribute>();
						object obj;
						if (customAttribute.ClearInstance)
						{
							obj = fieldInfo.GetValue(null);
							if (obj != null)
							{
								IDictionary dictionary = obj as IDictionary;
								if (dictionary != null)
								{
									dictionary.Clear();
									continue;
								}
								IList list = obj as IList;
								if (list != null)
								{
									list.Clear();
									continue;
								}
								try
								{
									MethodInfo method = obj.GetType().GetMethod("Clear", Array.Empty<Type>());
									if (method != null)
									{
										method.Invoke(obj, Array.Empty<object>());
										continue;
									}
								}
								catch (Exception ex)
								{
									MemberInfo memberInfo = fieldInfo;
									string text = "Error clearing field ";
									string name = fieldInfo.Name;
									string text2 = ": ";
									Exception ex2 = ex;
									MetricsManager.LogAssemblyError(memberInfo, text + name + text2 + ((ex2 != null) ? ex2.ToString() : null));
								}
							}
						}
						obj = ((customAttribute.CreateInstance || fieldInfo.FieldType.IsValueType) ? Activator.CreateInstance(fieldInfo.FieldType) : null);
						fieldInfo.SetValue(null, obj);
					}
				}
				catch (Exception ex3)
				{
					MetricsManager.LogException("resetting field " + ((fieldInfo != null) ? fieldInfo.Name : null), ex3, "game_exception");
				}
			}
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeFromHandle3, typeFromHandle, true))
			{
				try
				{
					if (methodInfo.IsStatic)
					{
						methodInfo.Invoke(null, Array.Empty<object>());
					}
				}
				catch (Exception ex4)
				{
					MetricsManager.LogException(string.Format("Invoking game based cache init {0}.{1}", methodInfo.DeclaringType, methodInfo.Name), ex4, "game_exception");
				}
			}
		}

		// Token: 0x0600425E RID: 16990 RVA: 0x0015D9F8 File Offset: 0x0015BBF8
		public XRLGame NewGame()
		{
			GameManager.Instance.gameQueue.clear();
			if (this.Game != null)
			{
				this.Game.Release();
			}
			using (CPUBoost.AutoScopeForceMainThread())
			{
				this.LoadEverything();
				this.ResetGameBasedStaticCaches();
				MemoryHelper.GCCollectMax();
			}
			this.Game = new XRLGame(XRLCore._Console, XRLCore._Buffer);
			this.Game.CreateNewGame();
			this.Reset();
			EmbarkInfo result = EmbarkBuilder.Begin().Result;
			if (result == null)
			{
				return null;
			}
			XRLGame xrlgame;
			try
			{
				result.bootGame(this.Game);
				xrlgame = this.Game;
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Booting game", ex);
				GameManager.Instance.SetGameViewStack("MainMenu");
				xrlgame = null;
			}
			return xrlgame;
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x0015DAD8 File Offset: 0x0015BCD8
		public void UpdateGlobalChoices()
		{
			if (this.Game != null && !this.Game.StringGameState.ContainsKey("SeekerEnemyFaction"))
			{
				for (int i = 0; i < 1000; i++)
				{
					Faction randomFaction = Factions.GetRandomFaction("Seekers");
					if (randomFaction != null)
					{
						this.Game.SetStringGameState("SeekerEnemyFaction", randomFaction.DisplayName);
						Factions.Get("Seekers").SetFactionFeeling(randomFaction.Name, -100);
						break;
					}
				}
			}
			Factions.RequireCachedHeirlooms();
			BookUI.InitBooks(false);
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x0015DB60 File Offset: 0x0015BD60
		public void CreateMarkOfDeath()
		{
			char[] array = new char[] { '!', '@', '#', '$', '%', '*', '(', ')', '/' };
			string text = "";
			for (int i = 0; i < 3; i++)
			{
				text += array.GetRandomElement<char>(null).ToString();
			}
			for (int j = 2; j >= 0; j--)
			{
				text += text[j].ToString();
			}
			this.Game.SetStringGameState("MarkOfDeath", text);
			JournalAPI.AddObservation("The lost Mark of Death from the late sultanate was " + text + ".", "MarkOfDeathSecret", "Gossip", "MarkOfDeathSecret", new string[] { "gossip", "old" }, false, -1L, "You recover the Mark of Death of the late sultanate.", false, true);
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x0015DC24 File Offset: 0x0015BE24
		public void CreateCures()
		{
			List<string> list = new List<string>();
			list.Add("asphalt");
			list.Add("oil");
			list.Add("honey");
			list.Add("blood");
			list.Add("wine");
			list.Add("salt");
			list.Add("cider");
			list.Add("sap");
			List<string> list2 = new List<string>();
			list2.Add("wine");
			list2.Add("honey");
			list2.Add("water");
			list2.Add("cider");
			list2.Add("sap");
			int num;
			for (int i = 1; i <= 2; i++)
			{
				num = Stat.Random(0, list.Count - 1);
				this.Game.SetStringGameState("GlotrotCure" + i.ToString(), list[num]);
				if (list2.CleanContains(list[num]))
				{
					list2.Remove(list[num]);
				}
				list.RemoveAt(num);
			}
			num = Stat.Random(0, list2.Count - 1);
			this.Game.SetStringGameState("GlotrotCure3", list2[num]);
			List<string> list3 = new List<string>();
			list3.Add("blood");
			list3.Add("honey");
			list3.Add("wine");
			list3.Add("oil");
			list3.Add("asphalt");
			list3.Add("sap");
			this.Game.SetStringGameState("IronshankCure", list3.GetRandomElement<string>(null));
			List<string> list4 = new List<string>();
			list4.Add("salt");
			list4.Add("cider");
			list4.Add("ink");
			this.Game.SetStringGameState("MonochromeCure", list4.GetRandomElement<string>(null));
			this.GenerateFungalCure();
		}

		// Token: 0x06004262 RID: 16994 RVA: 0x0015DE04 File Offset: 0x0015C004
		public void GenerateFungalCure()
		{
			string randomElement = new List<string>
			{
				"cider", "honey", "wine", "oil", "asphalt", "blood", "slime", "acid", "putrid", "convalessence",
				"proteangunk", "sap"
			}.GetRandomElement<string>(null);
			this.Game.SetStringGameState("FungalCureLiquid", randomElement);
			this.Game.SetStringGameState("FungalCureLiquidDisplay", LiquidVolume.GetLiquid(randomElement).GetName(null).Strip());
			List<string> list = new List<string>();
			for (int i = 0; i < GameObjectFactory.Factory.BlueprintList.Count; i++)
			{
				GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.BlueprintList[i];
				if (gameObjectBlueprint.DescendsFrom("BaseWorm") && !gameObjectBlueprint.Tags.ContainsKey("NoCure") && gameObjectBlueprint.HasPart("Corpse") && gameObjectBlueprint.GetPartParameter<int>("Corpse", "CorpseChance", 0) > 0)
				{
					string partParameter = gameObjectBlueprint.GetPartParameter<string>("Corpse", "CorpseBlueprint", null);
					if (GameObjectFactory.Factory.GetBlueprint(partParameter).DescendsFrom("Corpse"))
					{
						list.Add(partParameter);
						if (XRL.World.GameObject.CreateSample(partParameter).DisplayName.Strip().Contains("object"))
						{
							MetricsManager.LogError("Invalid fungal worm selected: " + partParameter);
						}
					}
				}
			}
			string randomElement2 = list.GetRandomElement<string>(null);
			this.Game.SetStringGameState("FungalCureWorm", randomElement2);
			this.Game.SetStringGameState("FungalCureWormDisplay", XRL.World.GameObject.Create(randomElement2).DisplayName.Strip());
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x0015DFF8 File Offset: 0x0015C1F8
		public void RunGame()
		{
			The.ParticleManager.reset();
			if (this.Game.Player.Body.GetHPPercent() < Globals.HPWarningThreshold)
			{
				this.HPWarning = true;
			}
			this.RenderBase(true, false);
			this.UpdateGlobalChoices();
			while (this.Game.Running)
			{
				try
				{
					if (GameManager.runWholeTurnOnUIThread > 0 && Thread.CurrentThread == XRLCore.CoreThread)
					{
						XRLCore.waitForSegmentOnGameThread = true;
						GameManager.Instance.uiQueue.queueTask(delegate
						{
							Thread.Sleep(100);
							while (GameManager.runWholeTurnOnUIThread > 0)
							{
								this.Game.ActionManager.RunSegment();
							}
							XRLCore.waitForSegmentOnGameThread = false;
						}, 10);
						while (XRLCore.waitForSegmentOnGameThread)
						{
						}
					}
					else
					{
						this.Game.ActionManager.RunSegment();
					}
				}
				catch (ThreadAbortException)
				{
					MetricsManager.LogInfo("Game thread ended");
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("TurnError", ex);
				}
			}
			The.ParticleManager.reset();
			FungalVisionary.VisionLevel = 0;
			GameManager.Instance.GreyscaleLevel = 0;
			Sidebar.MaxHP = 0;
			this.HPWarning = false;
			if (this.Game.DeathReason != "<nodeath>" && !this.Game.forceNoDeath)
			{
				this.BuildScore(true, null, true);
				if (!Options.DisablePermadeath)
				{
					DataManager.DeleteSaveDirectory(this.Game.GetCacheDirectory(null));
				}
			}
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x0015E148 File Offset: 0x0015C348
		public void StashScore()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			int num = this.ConstructScore(The.Game.GetStringGameState("EndgameCause", ""), stringBuilder, out text);
			The.Game.SetIntGameState("StashedScore", num);
			The.Game.SetStringGameState("StashedScoreDetails", stringBuilder.ToString());
			The.Game.SetStringGameState("StashedScoreCause", text);
			XRLGame game = The.Game;
			string text2 = "StashedReferenceDisplayName";
			XRL.World.GameObject player = The.Player;
			game.SetStringGameState(text2, (player != null) ? player.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false) : null);
			XRLGame game2 = The.Game;
			string text3 = "StashedLevel";
			XRL.World.GameObject player2 = The.Player;
			game2.SetIntGameState(text3, (player2 != null) ? player2.Level : 0);
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x0015E200 File Offset: 0x0015C400
		public int ConstructScore(string FakeDeathReason, StringBuilder SB, out string Cause)
		{
			int num = 0;
			XRLGame game = The.Game;
			XRL.World.GameObject player = The.Player;
			num += (int)((double)player.Stat("XP", 0) * 0.334);
			XRL.World.GameObject gameObject = null;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num8;
			using (ScopeDisposedList<XRL.World.GameObject> fromPool = ScopeDisposedList<XRL.World.GameObject>.GetFromPool())
			{
				player.GetContents(fromPool);
				foreach (XRL.World.GameObject gameObject2 in fromPool)
				{
					if (!gameObject2.HasTag("ExcludeFromGameScore"))
					{
						num += gameObject2.Count;
						Examiner examiner;
						if (gameObject2.TryGetPart<Examiner>(out examiner) && examiner.Complexity > 0)
						{
							int techTier = gameObject2.GetTechTier();
							int tier = gameObject2.GetTier();
							int num7 = techTier + tier + examiner.Complexity + examiner.Difficulty;
							bool flag = false;
							if (num7 > num6)
							{
								flag = true;
							}
							else if (num7 == num6)
							{
								if (techTier > num5)
								{
									flag = true;
								}
								else if (techTier == num5)
								{
									if (tier > num4)
									{
										flag = true;
									}
									else if (tier == num4)
									{
										if (examiner.Complexity > num2)
										{
											flag = true;
										}
										else if (examiner.Complexity == num2 && examiner.Difficulty > num3)
										{
											flag = true;
										}
									}
								}
							}
							if (flag)
							{
								gameObject = gameObject2;
								num5 = techTier;
								num4 = tier;
								num2 = examiner.Complexity;
								num3 = examiner.Difficulty;
								num6 = num7;
							}
							num += Math.Max(techTier, examiner.Complexity) * 250 * gameObject2.Count;
						}
					}
				}
				num += game.FinishedQuests.Count * 733;
				num += game.Quests.Count * 133;
				num += (int)((double)game.Turns * 0.01);
				num += game.ZoneManager.VisitedTime.Keys.Count * 35;
				num += 1211 * game.GetIntGameState("ArtifactsGenerated", 0);
				int intGameState = game.GetIntGameState("LairsFound", 0);
				num += intGameState * 75;
				num -= 387;
				num -= 35;
				char c = 'ê';
				if (!string.IsNullOrEmpty(game.GetStringGameState("EndType", "")))
				{
					if (EndGame.IsUltimate)
					{
						num = (int)((float)num * 10f);
						c = 'í';
					}
					else if (EndGame.IsAnyNorthSheva)
					{
						num *= 5;
						c = 'í';
					}
					else if (EndGame.IsBrightsheol)
					{
						num = (int)((double)num * 2.5);
						c = 'í';
					}
					else
					{
						num += 1000000;
						c = 'í';
					}
				}
				SB.Clear();
				SB.Append("{{C|").Append(c).Append("}} Game summary for {{W|")
					.Append(game.PlayerName)
					.Append("}} {{C|")
					.Append(c)
					.Append("}}\n\n");
				SB.Append("This game ended on ").Append(DateTime.Now.ToLocalTime().ToLongDateString()).Append(" at ")
					.Append(DateTime.Now.ToLocalTime().ToLongTimeString())
					.Append(".\n");
				Cause = "";
				if (!FakeDeathReason.IsNullOrEmpty())
				{
					Cause = FakeDeathReason;
					SB.Append(FakeDeathReason).Append('\n');
				}
				else
				{
					Cause = game.DeathReason.Replace("!", ".");
					SB.Append(Cause).Append('\n');
				}
				string text = "You were level {{C|";
				num8 = player.Stat("Level", 0);
				SB.Append(text + num8.ToString() + "}}.\n");
				SB.Append("You scored {{C|").Append(num).Append("}} ")
					.Append((num == 1) ? "point" : "points")
					.Append(".\n");
				MetricsManager.LogEvent("Death:score", (float)num);
				SB.Append("You survived for {{C|").Append(game.Turns).Append("}} ")
					.Append((game.Turns == 1L) ? "turn" : "turns")
					.Append(".\n");
				MetricsManager.LogEvent("Death:Turns", (float)game.Turns);
				MetricsManager.LogEvent("Death:Zones", (float)game.ZoneManager.VisitedTime.Keys.Count);
				if (intGameState > 0)
				{
					SB.Append("You found {{C|").Append(intGameState).Append("}} ")
						.Append((intGameState == 1) ? "lair" : "lairs")
						.Append(".\n");
					MetricsManager.LogEvent("Death:Lairs", (float)intGameState);
				}
				int intGameState2 = game.GetIntGameState("PlayerItemNamingDone", 0);
				if (intGameState2 > 0)
				{
					SB.Append("You named {{C|").Append(intGameState2).Append("}} ")
						.Append((intGameState2 == 1) ? "item" : "items")
						.Append(".\n");
					MetricsManager.LogEvent("Death:ItemsNamed", (float)intGameState2);
				}
				if (game.HasIntGameState("ArtifactsGenerated"))
				{
					int intGameState3 = game.GetIntGameState("ArtifactsGenerated", 0);
					SB.Append("You generated {{C|").Append(intGameState3).Append("}} storied ")
						.Append((intGameState3 == 1) ? "item" : "items")
						.Append(".\n");
					MetricsManager.LogEvent("Death:Artifacts", (float)intGameState3);
				}
				game.HasIntGameState("MetempsychosisCount");
				if (gameObject != null)
				{
					string text2 = gameObject.an(int.MaxValue, null, null, true, true, true, false, false, false, true, false, false, true, false, null, false, null, false);
					SB.Append("The most advanced artifact in your possession was " + text2 + ".\n");
					MetricsManager.LogEvent(string.Concat(new string[] { "Death:artifact:", text2, " [", gameObject.Blueprint, "]" }));
				}
				SB.Append("This game was played in ").Append(game.gameMode).Append(" mode.\n");
				SB.Append("\n\n");
				SB.Append("{{C|").Append(c).Append("}} Chronology for {{W|")
					.Append(game.PlayerName)
					.Append("}} {{C|")
					.Append(c)
					.Append("}}\n\n");
				foreach (JournalAccomplishment journalAccomplishment in JournalAPI.Accomplishments)
				{
					SB.Append("{{C|").Append('ú').Append("}} ")
						.Append(journalAccomplishment.GetDisplayText())
						.Append("\n");
				}
				SB.Append("\n\n");
				SB.Append("{{C|").Append(c).Append("}} Final messages for {{W|")
					.Append(game.PlayerName)
					.Append("}} {{C|")
					.Append(c)
					.Append("}}\n\n");
				int num9 = 0;
				foreach (string text3 in The.Game.Player.Messages.GetLinesList(0, 30))
				{
					if (num9 != 0)
					{
						SB.Append("\n");
					}
					num9++;
					SB.Append(text3);
				}
				num8 = num;
			}
			return num8;
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x0015EA00 File Offset: 0x0015CC00
		public void BuildScore(bool Real = true, string FakeDeathReason = null, bool showUI = true)
		{
			int score = 0;
			string text = "";
			string text2 = "";
			string text3 = The.Player.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false);
			int num = The.Player.Level;
			if (The.Game.HasIntGameState("StashedScore") && The.Game.HasStringGameState("StashedScoreDetails") && The.Game.HasStringGameState("StashedScoreCause"))
			{
				score = The.Game.GetIntGameState("StashedScore", 0);
				text = The.Game.GetStringGameState("StashedScoreDetails", "");
				text2 = The.Game.GetStringGameState("StashedScoreCause", "");
				text3 = The.Game.GetStringGameState("StashedReferenceDisplayName", text3);
				num = The.Game.GetIntGameState("StashedLevel", num);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				score = this.ConstructScore(FakeDeathReason, stringBuilder, out text2);
				text = stringBuilder.ToString();
			}
			if (string.IsNullOrEmpty(FakeDeathReason) && The.Game.HasStringGameState("EndgameCause"))
			{
				FakeDeathReason = The.Game.GetStringGameState("EndgameCause", "");
				text2 = FakeDeathReason;
			}
			MetricsManager.LogEvent("Death:score:" + score.ToString());
			string leaderboardID = null;
			if (Real)
			{
				try
				{
					if (!this.Game.GetStringGameState("leaderboardMode", "").IsNullOrEmpty())
					{
						string prefID = "leaderboardresult_" + this.Game.GetStringGameState("leaderboardMode", "");
						if (!Prefs.HasString(prefID))
						{
							Func<string> submitResult = null;
							Action<DialogResult> <>9__2;
							Action<LeaderboardScores> <>9__1;
							submitResult = delegate
							{
								string stringGameState = this.Game.GetStringGameState("leaderboardMode", "");
								int score2 = score;
								Action<LeaderboardScores> action;
								if ((action = <>9__1) == null)
								{
									action = (<>9__1 = delegate(LeaderboardScores result)
									{
										if (result != null)
										{
											Prefs.SetString(prefID, score.ToString());
											StringBuilder stringBuilder2 = new StringBuilder();
											int num2 = Math.Min(result.GetEntryCount(), 10);
											for (int i = 0; i < num2; i++)
											{
												LeaderboardEntry leaderboardEntry = result.GetLeaderboardEntry(i);
												stringBuilder2.Append(string.Concat(new string[]
												{
													leaderboardEntry.rank.ToString(),
													": {{Y|",
													leaderboardEntry.displayName,
													"}} ({{W|",
													leaderboardEntry.score.ToString(),
													"}})\n"
												}));
											}
											if (!LeaderboardManager.leaderboardresults.ContainsKey(leaderboardID))
											{
												LeaderboardManager.leaderboardresults.Add(leaderboardID, "");
											}
											LeaderboardManager.leaderboardresults[leaderboardID] = stringBuilder2.ToString();
											global::UnityEngine.Debug.Log(stringBuilder2.ToString());
											Keyboard.PushMouseEvent("LeaderboardResultsUpdated", null);
											return;
										}
										global::UnityEngine.Debug.Log("Could not get online for leaderboards.");
										string text4 = "An error occurred while posting to the leaderbaord, would you like to try again?";
										string text5 = "Sounds/UI/ui_notification";
										bool flag = true;
										DialogResult dialogResult = DialogResult.Yes;
										Action<DialogResult> action2;
										if ((action2 = <>9__2) == null)
										{
											action2 = (<>9__2 = delegate(DialogResult result)
											{
												switch (result)
												{
												case DialogResult.Yes:
												case DialogResult.OK:
													submitResult();
													return;
												}
												Prefs.SetString(prefID, score.ToString());
												PlatformManager.SetOfflineMode(true);
											});
										}
										Popup.ShowYesNo(text4, text5, flag, dialogResult, action2);
									});
								}
								return LeaderboardManager.SubmitResult(stringGameState, score2, action);
							};
							leaderboardID = submitResult();
						}
					}
				}
				catch (Exception ex)
				{
					leaderboardID = null;
					XRLCore.LogError(ex);
				}
				if (!The.Game.DontSaveThisIsAReplay)
				{
					Scores.Scoreboard.Add(score, text, this.Game.Turns, this.Game.GameID, this.Game.GetStringGameState("GameMode", ""), CheckpointingSystem.IsCheckpointingEnabled(), num, text3);
				}
				if (leaderboardID != null)
				{
					text = "<%leaderboard%>\n\n" + text;
				}
			}
			if (showUI)
			{
				GameSummaryUI.Show(score, text, text2, this.Game.PlayerName, leaderboardID, Real);
			}
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x0015EC88 File Offset: 0x0015CE88
		public void SaveGame(string GameName)
		{
			this.Game.SaveGame(GameName, "Saving game", false, false);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x0015EC9E File Offset: 0x0015CE9E
		[Obsolete("ShowThinker does nothing and will be removed in a future version")]
		public static void ShowThinker()
		{
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x0015ECA0 File Offset: 0x0015CEA0
		public static void SetClipboard(string Msg)
		{
			global::UnityEngine.Debug.LogError(Msg);
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x0015ECA8 File Offset: 0x0015CEA8
		public static void LogError(string Error)
		{
			global::UnityEngine.Debug.LogError("ERROR:" + Error);
			if (!Keyboard.Closed)
			{
				MetricsManager.LogException("Unknown", new Exception(Error), "game_exception");
			}
			if (Keyboard.Closed)
			{
				Thread.CurrentThread.Abort();
				throw new Exception("Stopping game thread with an exception!");
			}
		}

		// Token: 0x0600426B RID: 17003 RVA: 0x0015ECFD File Offset: 0x0015CEFD
		public static void Log(string Info)
		{
			global::UnityEngine.Debug.Log(Info);
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x0015ED05 File Offset: 0x0015CF05
		public static void LogError(Exception ex)
		{
			global::UnityEngine.Debug.LogError("ERROR:" + ex.ToString());
			if (Keyboard.Closed)
			{
				Thread.CurrentThread.Abort();
				throw new Exception("Stopping game thread with an exception!");
			}
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x0015ED38 File Offset: 0x0015CF38
		public static void LogError(string Category, Exception ex)
		{
			global::UnityEngine.Debug.LogError("ERROR:" + Category + ":" + ex.ToString());
			if (Keyboard.Closed)
			{
				Thread.CurrentThread.Abort();
				throw new Exception("Stopping game thread with an exception!");
			}
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x0015ED71 File Offset: 0x0015CF71
		public static void LogError(string Category, string error)
		{
			global::UnityEngine.Debug.LogError("ERROR:" + Category + ":" + error);
			if (Keyboard.Closed)
			{
				Thread.CurrentThread.Abort();
				throw new Exception("Stopping game thread with an exception!");
			}
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x0015EDA5 File Offset: 0x0015CFA5
		public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			XRLCore.LogError("FATAL thread exception:" + e.ToString());
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x0015EDBC File Offset: 0x0015CFBC
		public bool RestoreModsLoaded(List<string> Enabled)
		{
			return this.RestoreModsLoadedAsync(Enabled).Result;
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x0015EDCC File Offset: 0x0015CFCC
		public async Task<bool> RestoreModsLoadedAsync(List<string> Enabled)
		{
			List<string> list = new List<string>(Enabled.Except<string>(ModManager.GetAvailableMods()));
			List<string> extraLoaded = new List<string>(ModManager.GetRunningMods().Except<string>(Enabled));
			List<string> notLoaded = new List<string>(Enabled.Except<string>(ModManager.GetRunningMods()).Except<string>(list));
			if (list.Count > 0)
			{
				TaskAwaiter<QudMenuItem> taskAwaiter = Popup.NewPopupMessageAsync(XRL.World.Event.NewStringBuilder("One or more mods enabled in this save are {{red|not available}}:{{red|").Compound(list.Select<string, string>(new Func<string, string>(ModManager.GetModTitle)), "\n{{y|:}} ").Compound("}}Do you still wish to try to load this save?", "\n\n")
					.ToString(), PopupMessage.YesNoButton, null, "Incomplete Mod Configuration", null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<QudMenuItem> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<QudMenuItem>);
				}
				if (taskAwaiter.GetResult().command != PopupMessage.YesNoButton[0].command)
				{
					return false;
				}
			}
			if (extraLoaded.Count > 0 || notLoaded.Count > 0)
			{
				StringBuilder stringBuilder = XRL.World.Event.NewStringBuilder(null);
				if (extraLoaded.Count > 0)
				{
					stringBuilder.Compound("These mods are {{red|disabled}} in the save:{{red|", '\n').Compound(extraLoaded.Select<string, string>(new Func<string, string>(ModManager.GetModTitle)), "\n{{y|:}} ").Append("}}")
						.AppendLine();
				}
				if (notLoaded.Count > 0)
				{
					stringBuilder.Compound("These mods are {{green|enabled}} in the save:{{green|", '\n').Compound(notLoaded.Select<string, string>(new Func<string, string>(ModManager.GetModTitle)), "\n{{y|:}} ").Append("}}")
						.AppendLine();
				}
				stringBuilder.AppendLine();
				IReadOnlyList<string> readOnlyList = new string[] { "Restart using save game's mod configuration", "Load keeping current mod configuration" };
				int num = await Popup.PickOptionAsync("Mod Configuration Differs", stringBuilder.ToString(), "", readOnlyList, null, null, null, null, 0, 60, 0, -1, false, true, false, true);
				if (num == -1)
				{
					return false;
				}
				if (num == 0)
				{
					foreach (string text in extraLoaded)
					{
						ModManager.GetMod(text).IsEnabled = false;
					}
					foreach (string text2 in notLoaded)
					{
						ModManager.GetMod(text2).IsEnabled = true;
					}
					ModManager.WriteModSettings();
					GameManager.Restart();
				}
			}
			return true;
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x0015EE10 File Offset: 0x0015D010
		public void WriteConsoleLine(string s)
		{
			try
			{
				global::UnityEngine.Debug.Log(s);
			}
			catch
			{
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x0015EE38 File Offset: 0x0015D038
		// (set) Token: 0x06004274 RID: 17012 RVA: 0x0015EE4C File Offset: 0x0015D04C
		public static string SavePath
		{
			get
			{
				if (XRLCore._SavePath == null)
				{
					return Path.PersistentDataPath();
				}
				return XRLCore._SavePath;
			}
			set
			{
				XRLCore._SavePath = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x0015EE54 File Offset: 0x0015D054
		// (set) Token: 0x06004276 RID: 17014 RVA: 0x0015EE82 File Offset: 0x0015D082
		public static string LocalPath
		{
			get
			{
				if (XRLCore._LocalPath == null)
				{
					XRLCore._LocalPath = Path.Combine(new string[]
					{
						XRLCore.SavePath,
						"Local"
					});
				}
				return XRLCore._LocalPath;
			}
			set
			{
				XRLCore._LocalPath = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x0015EE8A File Offset: 0x0015D08A
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x0015EEB8 File Offset: 0x0015D0B8
		public static string SyncedPath
		{
			get
			{
				if (XRLCore._SyncedPath == null)
				{
					XRLCore._SyncedPath = Path.Combine(new string[]
					{
						XRLCore.SavePath,
						"Synced"
					});
				}
				return XRLCore._SyncedPath;
			}
			set
			{
				XRLCore._SyncedPath = value;
			}
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0015EEC0 File Offset: 0x0015D0C0
		public static void Stop()
		{
			global::UnityEngine.Debug.Log("Stopping...");
			GameManager.bDraw = 0;
			Keyboard.Closed = true;
			XRLCore.CoreThread.Interrupt();
			XRLCore.CoreThread.Abort();
			XRLCore.Core.Game.Running = false;
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x0015EEFC File Offset: 0x0015D0FC
		private static string ArgumentPath(string Argument)
		{
			string fullPath = Path.GetFullPath(Argument);
			int length = Argument.Length;
			if (length == 0 || !Argument[length - 1].IsDirectorySeparator())
			{
				Argument += Path.DirectorySeparatorChar.ToString();
			}
			if (!Directory.Exists(Argument))
			{
				Directory.CreateDirectory(Argument);
			}
			return fullPath;
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x0015EF50 File Offset: 0x0015D150
		public static void InitializePaths()
		{
			XRLCore.Log("Initializing paths...");
			XRLCore.DataPath = Path.Combine(new string[]
			{
				Application.streamingAssetsPath,
				"Base"
			});
			XRLCore.SavePath = Path.PersistentDataPath();
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			if (commandLineArgs != null && commandLineArgs.Length > 0)
			{
				for (int i = 0; i < commandLineArgs.Length - 1; i++)
				{
					string text = commandLineArgs[i].ToUpper();
					if (text == "-SAVEPATH")
					{
						XRLCore.SavePath = XRLCore.ArgumentPath(commandLineArgs[++i]);
					}
					else if (text == "-SHAREDPATH")
					{
						XRLCore.LocalPath = XRLCore.ArgumentPath(commandLineArgs[++i]);
					}
					else if (text == "-SYNCEDPATH")
					{
						XRLCore.SyncedPath = XRLCore.ArgumentPath(commandLineArgs[++i]);
					}
				}
			}
			XRLCore.Log("Data path: " + XRLCore.DataPath);
			XRLCore.Log("Save path: " + XRLCore.SavePath);
			XRLCore.Log("Shared path: " + XRLCore.LocalPath);
			XRLCore.Log("Synced path: " + XRLCore.SyncedPath);
			Folder.CreateDirectory(DataManager.LocalPath("Mods")).LogIfErrored();
			Folder.CreateDirectory(DataManager.SyncedPath("Saves")).LogIfErrored();
			DataManager.PerformFileLocationChangesAsAppropriate().Wait();
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x0015F0A4 File Offset: 0x0015D2A4
		public static Thread Start()
		{
			global::UnityEngine.Debug.Log("Starting core...");
			XRLCore.CoreThread = new Thread(new ThreadStart(XRLCore._ThreadStart))
			{
				Name = "XRLCore Thread"
			};
			XRLCore.CoreThread.CurrentCulture = CultureInfo.InvariantCulture;
			XRLCore.CoreThread.Priority = global::System.Threading.ThreadPriority.Highest;
			XRLCore.CoreThread.Start();
			XRLCore.bStarted = true;
			global::UnityEngine.Debug.Log("Started!");
			return XRLCore.CoreThread;
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x0015F118 File Offset: 0x0015D318
		public static void _ThreadStart()
		{
			try
			{
				SynchronizationContext synchronizationContext = new SynchronizationContext();
				SynchronizationContext.SetSynchronizationContext(synchronizationContext);
				GameManager.Instance.gameThreadSynchronizationContext = synchronizationContext;
				XRLCore.Core = new XRLCore();
				XRLCore.Core._Start();
			}
			catch (ThreadInterruptedException)
			{
				global::UnityEngine.Debug.Log("Core thread shut down...");
			}
			catch (Exception ex)
			{
				global::UnityEngine.Debug.LogError(ex);
			}
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x0015F184 File Offset: 0x0015D384
		public void ReloadUIViews()
		{
			this.WriteConsoleLine("Init Console UIs...\n");
			foreach (Type type in ModManager.GetTypesWithAttribute(typeof(UIView), true))
			{
				if (typeof(IWantsTextConsoleInit).IsAssignableFrom(type))
				{
					IWantsTextConsoleInit wantsTextConsoleInit = Activator.CreateInstance(type) as IWantsTextConsoleInit;
					if (wantsTextConsoleInit != null)
					{
						wantsTextConsoleInit.Init(XRLCore._Console, XRLCore._Buffer);
					}
				}
			}
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x0015F218 File Offset: 0x0015D418
		public void _Start()
		{
			XRLCore.Core = this;
			if (TextConsole.bExtended)
			{
				XRLCore._Buffer = ScreenBuffer.create(80, 33);
				if (TextConsole.Mode != TerminalMode.OpenGL)
				{
				}
			}
			else
			{
				XRLCore._Buffer = ScreenBuffer.create(80, 25);
				TerminalMode mode = TextConsole.Mode;
			}
			XRLCore._Console = new TextConsole();
			XRLCore._Console.ShowCursor(false);
			this.WriteConsoleLine("Cleaning up...\n");
			this.WriteConsoleLine("Starting up XRL...\n");
			using (CPUBoost cpuboost = CPUBoost.AutoScopeForceMainThread())
			{
				this.WriteConsoleLine("Initialize Genders and Pronoun Sets...\n");
				Gender.Init();
				PronounSet.Init();
				this.WriteConsoleLine("Initialize Name Styles...\n");
				NameStyles.CheckInit();
				this.WriteConsoleLine("Loading World Blueprints...\n");
				WorldFactory.Factory.Init();
				this.WriteConsoleLine("Loading Object Blueprints...\n");
				GameObjectFactory.Init();
				this.WriteConsoleLine("Loading Help...\n");
				XRLCore.Manual = new XRLManual(XRLCore._Console);
				this.WriteConsoleLine("Init Pathfinder...\n");
				FindPath.Initalize();
				this.ReloadUIViews();
				XRLCore.ParticleManager = new ParticleManager();
				XRLCore.ParticleManager.Init(XRLCore._Console, XRLCore._Buffer);
				if (TextConsole.Mode == TerminalMode.OpenGL)
				{
					XRLCore._Console.Hide();
					XRLCore._Console.FocusUI();
				}
				int num = 0;
				string text = "{{K|" + XRLGame.CoreVersion.ToString() + "}}";
				this.LoadEverything();
				this.WriteConsoleLine("Starting Game...");
				SoundManager.PlayMusic("Music/Pilgrims Path", "music", true, 12f, 1f, null, true);
				bool flag = SavesAPI.HasSavedGameInfo();
				cpuboost.Dispose();
				for (;;)
				{
					IL_0176:
					if (GameManager.Instance.CurrentGameView != "MainMenu")
					{
						GameManager.Instance.SetGameViewStack("MainMenu");
					}
					GameManager.Instance.ClearRegions();
					XRL.World.Event.ResetPool(true);
					XRLCore._Buffer.Clear();
					XRLCore._Buffer.SingleBox(0, 0, 79, 24, ConsoleLib.Console.ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					XRLCore._Buffer.Goto(35, 9);
					XRLCore._Buffer.Write(CapabilityManager.AllowKeyboardHotkeys ? "{{W|N}}ew game" : "New game", true, false, false, null, -1);
					if (num == 0)
					{
						XRLCore._Buffer.WriteAt(33, 9, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|New game}}", true);
					}
					GameManager.Instance.AddRegion(35, 9, 43, 9, "Pick:New Game", null, "Select:0");
					XRLCore._Buffer.Goto(35, 10);
					if (flag)
					{
						XRLCore._Buffer.Write(CapabilityManager.AllowKeyboardHotkeys ? "{{W|C}}ontinue" : "Continue", true, false, false, null, -1);
					}
					else
					{
						XRLCore._Buffer.Write("{{K|Continue}}", true, false, false, null, -1);
					}
					GameManager.Instance.AddRegion(35, 10, 43, 10, "Pick:Continue", null, "Select:1");
					if (num == 1)
					{
						XRLCore._Buffer.WriteAt(33, 10, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|Continue}}", true);
					}
					XRLCore._Buffer.Goto(35, 12);
					XRLCore._Buffer.Write(CapabilityManager.AllowKeyboardHotkeys ? "{{W|O}}ptions" : "Options", true, false, false, null, -1);
					if (num == 2)
					{
						XRLCore._Buffer.WriteAt(33, 12, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|Options}}", true);
					}
					GameManager.Instance.AddRegion(35, 12, 43, 12, "Pick:Options", null, "Select:2");
					XRLCore._Buffer.Goto(35, 13);
					XRLCore._Buffer.Write(CapabilityManager.AllowKeyboardHotkeys ? "{{W|H}}igh Scores" : "High Scores", true, false, false, null, -1);
					if (num == 3)
					{
						XRLCore._Buffer.WriteAt(33, 13, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|High Scores}}", true);
					}
					GameManager.Instance.AddRegion(35, 13, 43, 13, "Pick:High Scores", null, "Select:3");
					XRLCore._Buffer.Goto(35, 14);
					XRLCore._Buffer.Write(CapabilityManager.AllowKeyboardHotkeys ? "[{{W|?}}] Help" : "Help", true, false, false, null, -1);
					if (num == 4)
					{
						XRLCore._Buffer.WriteAt(33, 14, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|Help}}", true);
					}
					GameManager.Instance.AddRegion(35, 14, 43, 14, "Pick:Help", null, "Select:4");
					XRLCore._Buffer.Goto(35, 15);
					XRLCore._Buffer.Write(CapabilityManager.AllowKeyboardHotkeys ? "{{W|Q}}uit" : "Quit", true, false, false, null, -1);
					if (num == 5)
					{
						XRLCore._Buffer.WriteAt(33, 15, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|Quit}}", true);
					}
					GameManager.Instance.AddRegion(35, 15, 43, 15, "Pick:Quit", null, "Select:5");
					XRLCore._Buffer.Goto(35, 18);
					XRLCore._Buffer.Write(CapabilityManager.AllowKeyboardHotkeys ? "{{W|R}}edeem Code" : "Redeem Code", true, false, false, null, -1);
					if (num == 6)
					{
						XRLCore._Buffer.WriteAt(33, 18, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|Redeem Code}}", true);
					}
					GameManager.Instance.AddRegion(35, 18, 43, 18, "Pick:Redeem Code", null, "Select:6");
					XRLCore._Buffer.Goto(35, 17);
					XRLCore._Buffer.Write("{{g|Dromad Edition}}", true, false, false, null, -1);
					if (ModManager.Mods != null && ModManager.Mods.Count > 0)
					{
						XRLCore._Buffer.WriteAt(35, 21, CapabilityManager.AllowKeyboardHotkeys ? "{{W|M}}ods" : "Mods", true);
						if (num == 7)
						{
							XRLCore._Buffer.WriteAt(33, 21, CapabilityManager.AllowKeyboardHotkeys ? "{{Y|>}}" : "{{Y|>}} {{W|Mods}}", true);
						}
						GameManager.Instance.AddRegion(35, 21, 43, 21, "ModManager", null, "Select:7");
					}
					if (ModManager.IsAnyModFailed())
					{
						XRLCore._Buffer.WriteAt(40, 21, "{{y|-}} {{R|You have mods with errors.}}", true);
					}
					else if (ModManager.IsAnyModMissingDependency())
					{
						XRLCore._Buffer.WriteAt(40, 21, "{{y|-}} {{R|You have mods with missing dependencies.}}", true);
					}
					else if (ModManager.IsScriptingUndetermined())
					{
						XRLCore._Buffer.WriteAt(40, 21, "{{y|-}} {{R|You have unapproved scripting mods.}}", true);
					}
					XRLCore._Buffer.WriteAt(32, 0, "  {{C|Caves of Qud}}  ", true);
					XRLCore._Buffer.WriteAt(55, 0, text, true);
					XRLCore._Buffer.WriteAt(27, 24, " {{Y|Copyright ({{w|c}}) Freehold Games({{w|tm}})}} ", true);
					XRLCore._Console.DrawBuffer(XRLCore._Buffer, null, false);
					Keys keys;
					for (;;)
					{
						keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
						int num2 = 6;
						if (ModManager.Mods != null && ModManager.Mods.Count > 0)
						{
							num2 = 7;
						}
						if (keys == Keys.Enter)
						{
							keys = Keys.Space;
						}
						if (keys == Keys.NumPad8 && num > 0)
						{
							num--;
						}
						if (keys == Keys.NumPad2 && num < num2)
						{
							num++;
						}
						if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event.StartsWith("Select:"))
						{
							num = Convert.ToInt32(Keyboard.CurrentMouseEvent.Event.Split(':', StringSplitOptions.None)[1]);
						}
						if ((keys == Keys.Space || keys == Keys.Escape) && Options.ModernUI)
						{
							goto IL_0176;
						}
						if (keys == Keys.M || (keys == Keys.Space && num == 7) || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:Installed Mod Configuration"))
						{
							UIManager.pushWindow("ModManager", false);
							SingletonWindowBase<ModManagerUI>.instance.nextHideCallback.AddListener(delegate
							{
								Keyboard.PushMouseEvent("Refresh", null);
							});
						}
						if (keys == Keys.R || (keys == Keys.Space && num == 6) || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:Redeem Code"))
						{
							string text2 = Popup.AskString("Redeem a Code", "", "Sounds/UI/ui_notification", null, null, 32, 0, false, true, null);
							if (!text2.IsNullOrEmpty())
							{
								CodeRedemptionManager.redeem(text2);
							}
						}
						if (keys == Keys.Q || (keys == Keys.Space && num == 5) || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Quit"))
						{
							goto IL_0B34;
						}
						if (keys == Keys.N || (keys == Keys.Space && num == 0) || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:New Game"))
						{
							this.NewGame();
						}
						XRLGame xrlgame;
						if ((keys == Keys.C || (keys == Keys.Space && num == 1) || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:Continue")) && flag)
						{
							if (Options.ModernUI)
							{
								try
								{
									GameManager.Instance.PushGameView("ModernSaveManagement", true);
									SingletonWindowBase<MainMenu>.instance.DisableNavContext(true);
									SingletonWindowBase<Qud.UI.SaveManagement>.instance.ContinueMenu().Wait();
									goto IL_08D7;
								}
								finally
								{
									GameManager.Instance.PopGameView(false);
									xrlgame = this.Game;
									if (xrlgame == null || !xrlgame.Running)
									{
										SingletonWindowBase<MainMenu>.instance.Reshow();
									}
								}
								goto IL_08CC;
							}
							goto IL_08CC;
							IL_08D7:
							flag = SavesAPI.HasSavedGameInfo();
							goto IL_08DD;
							IL_08CC:
							this.SaveManagement().Wait();
							goto IL_08D7;
						}
						IL_08DD:
						if (keys == Keys.O || (keys == Keys.Space && num == 2) || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:Options"))
						{
							OptionsUI.Show();
						}
						if (keys == Keys.H || (keys == Keys.Space && num == 3) || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:High Scores"))
						{
							if (Options.ModernUI)
							{
								try
								{
									GameManager.Instance.PushGameView("ModernHighScores", true);
									SingletonWindowBase<MainMenu>.instance.DisableNavContext(true);
									SingletonWindowBase<HighScoresScreen>.instance.ShowScreen().Wait();
									xrlgame = this.Game;
									if (xrlgame == null || !xrlgame.Running)
									{
										SingletonWindowBase<MainMenu>.instance.Reshow();
									}
									goto IL_09CA;
								}
								finally
								{
									GameManager.Instance.PopGameView(false);
									xrlgame = this.Game;
									if (xrlgame == null || !xrlgame.Running)
									{
										SingletonWindowBase<MainMenu>.instance.Reshow();
									}
								}
							}
							Scores.Show();
						}
						IL_09CA:
						xrlgame = this.Game;
						if (xrlgame != null && xrlgame.Running)
						{
							bool transient = this.Game.Transient;
							string[] exitViewStack = this.Game.ExitViewStack;
							try
							{
								this.RunGame();
								flag = SavesAPI.HasSavedGameInfo();
								SoundManager.PlayMusic("Music/Pilgrims Path", "music", true, 2f, 1f, null, true);
								AmbientSoundsSystem.PlayAmbientBeds(null, null, null, null);
								if (transient && !exitViewStack.IsNullOrEmpty<string>())
								{
									GameManager.Instance.SetGameViewStack(exitViewStack);
									Thread.Sleep(2000);
									ControlManager.ResetInput(false, false);
									Keyboard.ClearInput(false, true);
									Keyboard.getvk(Options.MapDirectionsToKeypad, false, false);
									continue;
								}
								goto IL_0176;
							}
							catch (Exception ex)
							{
								MetricsManager.LogException("RunGame", ex, "game_exception");
								goto IL_0176;
							}
							break;
						}
						break;
					}
					if (keys == Keys.OemQuestion || (keys & Keys.OemQuestion) == Keys.OemQuestion || (keys == Keys.Space && num == 4) || keys == Keys.F1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:Help"))
					{
						XRLCore.Manual.ShowHelp("");
						MainMenu.EnsureReselect = true;
					}
					if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Pick:Credits")
					{
						XRLCore.Manual.ShowHelp("Credits");
						MainMenu.EnsureReselect = true;
					}
				}
				IL_0B34:
				Popup._TextConsole.Close();
			}
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x0015FDD8 File Offset: 0x0015DFD8
		public Task<XRLGame> SaveManagement()
		{
			XRLCore.<SaveManagement>d__157 <SaveManagement>d__;
			<SaveManagement>d__.<>t__builder = AsyncTaskMethodBuilder<XRLGame>.Create();
			<SaveManagement>d__.<>1__state = -1;
			<SaveManagement>d__.<>t__builder.Start<XRLCore.<SaveManagement>d__157>(ref <SaveManagement>d__);
			return <SaveManagement>d__.<>t__builder.Task;
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x0015FE14 File Offset: 0x0015E014
		public static bool WantEvent(int ID, int cascade)
		{
			XRLCore core = XRLCore.Core;
			bool? flag;
			if (core == null)
			{
				flag = null;
			}
			else
			{
				XRLGame game = core.Game;
				flag = ((game != null) ? new bool?(game.WantEvent(ID, cascade)) : null);
			}
			bool? flag2 = flag;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x0015FE60 File Offset: 0x0015E060
		public static bool HandleEvent<T>(T E) where T : MinEvent
		{
			XRLCore core = XRLCore.Core;
			bool? flag;
			if (core == null)
			{
				flag = null;
			}
			else
			{
				XRLGame game = core.Game;
				flag = ((game != null) ? new bool?(game.HandleEvent(E)) : null);
			}
			bool? flag2 = flag;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x0015FEAD File Offset: 0x0015E0AD
		public static bool HandleEvent<T>(T E, IEvent ParentEvent) where T : MinEvent
		{
			bool flag = XRLCore.HandleEvent<T>(E);
			ParentEvent.ProcessChildEvent(E);
			return flag;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x0015FEC1 File Offset: 0x0015E0C1
		public bool TilesEnabled
		{
			get
			{
				return Globals.RenderMode == RenderModeType.Tiles;
			}
		}

		// Token: 0x0400209B RID: 8347
		public static XRLCore Core;

		// Token: 0x0400209C RID: 8348
		public XRLGame Game;

		// Token: 0x0400209D RID: 8349
		public static XRLManual Manual;

		// Token: 0x0400209E RID: 8350
		public static TextConsole _Console;

		// Token: 0x0400209F RID: 8351
		public static ScreenBuffer _Buffer;

		// Token: 0x040020A0 RID: 8352
		public static ParticleManager ParticleManager;

		// Token: 0x040020A1 RID: 8353
		public static int CurrentFrame;

		// Token: 0x040020A2 RID: 8354
		public static int CurrentFrameLong;

		// Token: 0x040020A3 RID: 8355
		public static int CurrentFrame10;

		// Token: 0x040020A4 RID: 8356
		public static int CurrentFrameLong10;

		// Token: 0x040020A5 RID: 8357
		public static double CurrentFrameAccumulator;

		// Token: 0x040020A6 RID: 8358
		public static Stopwatch FrameTimer = new Stopwatch();

		// Token: 0x040020A7 RID: 8359
		private static int _PlayerVisibilityRadius = int.MinValue;

		// Token: 0x040020A8 RID: 8360
		public bool EnableAnimation = true;

		// Token: 0x040020A9 RID: 8361
		public bool ShowFPS;

		// Token: 0x040020AA RID: 8362
		public bool VisAllToggle;

		// Token: 0x040020AB RID: 8363
		public bool CheatMaxMod;

		// Token: 0x040020AC RID: 8364
		public bool AllowWorldMapParticles;

		// Token: 0x040020AD RID: 8365
		public bool HPWarning;

		// Token: 0x040020AE RID: 8366
		private const int MAINMENU_INDEX_CONTROLREMAP = 0;

		// Token: 0x040020AF RID: 8367
		private const int MAINMENU_INDEX_OPTIONS = 1;

		// Token: 0x040020B0 RID: 8368
		private const int MAINMENU_INDEX_WORLDINFO = 2;

		// Token: 0x040020B1 RID: 8369
		private const int MAINMENU_INDEX_SAVEANDEXIT = 3;

		// Token: 0x040020B2 RID: 8370
		private const int MAINMENU_INDEX_ABANDON = 4;

		// Token: 0x040020B3 RID: 8371
		[NonSerialized]
		public bool cool;

		// Token: 0x040020B4 RID: 8372
		[NonSerialized]
		public bool IDKFA;

		// Token: 0x040020B5 RID: 8373
		[NonSerialized]
		public bool IgnoreMe;

		// Token: 0x040020B6 RID: 8374
		[NonSerialized]
		public bool Calm;

		// Token: 0x040020B7 RID: 8375
		[NonSerialized]
		public static bool bThreadFocus = true;

		// Token: 0x040020B8 RID: 8376
		[NonSerialized]
		public string MoveConfirmDirection;

		// Token: 0x040020B9 RID: 8377
		[NonSerialized]
		public int RenderedObjects;

		// Token: 0x040020BA RID: 8378
		public string _PlayerWalking = "";

		// Token: 0x040020BB RID: 8379
		public XRLCore.SortPoint AutoexploreTarget;

		// Token: 0x040020BC RID: 8380
		public CleanQueue<XRLCore.SortPoint> PlayerAvoid = new CleanQueue<XRLCore.SortPoint>();

		// Token: 0x040020BD RID: 8381
		public XRLCore.SortPoint LastCell;

		// Token: 0x040020BE RID: 8382
		[NonSerialized]
		public static int MemCheckTurns = 0;

		// Token: 0x040020BF RID: 8383
		[NonSerialized]
		public const long InitialMemCheckHeadRoom = 2500000000L;

		// Token: 0x040020C0 RID: 8384
		[NonSerialized]
		public static long MemCheckHeadRoom = (long)((ulong)(-1794967296));

		// Token: 0x040020C1 RID: 8385
		[NonSerialized]
		public static bool sixArmsSet = false;

		// Token: 0x040020C2 RID: 8386
		[ModSensitiveStaticCache(true)]
		[NonSerialized]
		private static List<Action<XRLCore>> OnBeginPlayerTurnCallbacks = new List<Action<XRLCore>>();

		// Token: 0x040020C3 RID: 8387
		[ModSensitiveStaticCache(true)]
		[NonSerialized]
		private static List<Action<XRLCore>> OnEndPlayerTurnCallbacks = new List<Action<XRLCore>>();

		// Token: 0x040020C4 RID: 8388
		[ModSensitiveStaticCache(true)]
		[NonSerialized]
		private static List<Action<XRLCore>> OnEndPlayerTurnSingleCallbacks = new List<Action<XRLCore>>();

		// Token: 0x040020C5 RID: 8389
		[ModSensitiveStaticCache(true)]
		[NonSerialized]
		private static List<Action<XRLCore>> OnPassedTenPlayerTurnCallbacks = new List<Action<XRLCore>>();

		// Token: 0x040020C6 RID: 8390
		[ModSensitiveStaticCache(true)]
		[NonSerialized]
		private static List<Action<XRLCore, ScreenBuffer>> AfterRenderCallbacks = new List<Action<XRLCore, ScreenBuffer>>();

		// Token: 0x040020C7 RID: 8391
		[ModSensitiveStaticCache(true)]
		[NonSerialized]
		private static List<Action<string>> OnNewMessageLogEntryCallbacks = new List<Action<string>>();

		// Token: 0x040020C8 RID: 8392
		private static List<Cell> SmartUseCells = new List<Cell>();

		// Token: 0x040020C9 RID: 8393
		public static bool CludgeTargetRendered = false;

		// Token: 0x040020CA RID: 8394
		public List<XRL.World.GameObject> CludgeCreaturesRendered;

		// Token: 0x040020CB RID: 8395
		public static bool RenderFloorTextures = true;

		// Token: 0x040020CC RID: 8396
		public static bool RenderHiddenPlayer = true;

		// Token: 0x040020CD RID: 8397
		[NonSerialized]
		public global::System.Random ConfusionRng = new global::System.Random();

		// Token: 0x040020CE RID: 8398
		private static int nFrame = 0;

		// Token: 0x040020CF RID: 8399
		public List<XRL.World.GameObject> OldHostileWalkObjects = new List<XRL.World.GameObject>();

		// Token: 0x040020D0 RID: 8400
		public List<XRL.World.GameObject> HostileWalkObjects = new List<XRL.World.GameObject>();

		// Token: 0x040020D1 RID: 8401
		private bool _isNewGameModFlow;

		// Token: 0x040020D2 RID: 8402
		public static int lastWait = 10;

		// Token: 0x040020D3 RID: 8403
		public static bool waitForSegmentOnGameThread = false;

		// Token: 0x040020D4 RID: 8404
		public static Thread CoreThread = null;

		// Token: 0x040020D5 RID: 8405
		public static string OSXDLCPath = "";

		// Token: 0x040020D6 RID: 8406
		public static string DLCPath = "";

		// Token: 0x040020D7 RID: 8407
		public static string EmbeddedModsPath = "";

		// Token: 0x040020D8 RID: 8408
		public static string DataPath = "";

		// Token: 0x040020D9 RID: 8409
		public static string _SavePath = null;

		// Token: 0x040020DA RID: 8410
		public static string _LocalPath = null;

		// Token: 0x040020DB RID: 8411
		public static string _SyncedPath = null;

		// Token: 0x040020DC RID: 8412
		public static bool bStarted = false;

		// Token: 0x02001A05 RID: 6661
		public class SortObjectBydistanceToPlayer : Comparer<XRL.World.GameObject>
		{
			// Token: 0x0600E5F2 RID: 58866 RVA: 0x00530108 File Offset: 0x0052E308
			public override int Compare(XRL.World.GameObject ox, XRL.World.GameObject oy)
			{
				Cell currentCell = The.Player.CurrentCell;
				if (currentCell == null)
				{
					return 0;
				}
				Cell currentCell2 = ox.CurrentCell;
				Cell currentCell3 = oy.CurrentCell;
				if (currentCell2 == null)
				{
					return 1;
				}
				if (currentCell3 == null)
				{
					return 0;
				}
				Point point = new Point(currentCell2.X, currentCell2.Y);
				Point point2 = new Point(currentCell3.X, currentCell3.Y);
				int x = currentCell.X;
				int y = currentCell.Y;
				int num = (x - point.X) * (x - point.X) + (y - point.Y) * (y - point.Y);
				int num2 = (x - point2.X) * (x - point2.X) + (y - point2.Y) * (y - point2.Y);
				return num.CompareTo(num2);
			}
		}

		// Token: 0x02001A06 RID: 6662
		public class SortCellBydistanceToObject : Comparer<Cell>
		{
			// Token: 0x0600E5F4 RID: 58868 RVA: 0x005301D9 File Offset: 0x0052E3D9
			public SortCellBydistanceToObject(XRL.World.GameObject Target)
			{
				this._Target = Target;
			}

			// Token: 0x0600E5F5 RID: 58869 RVA: 0x005301E8 File Offset: 0x0052E3E8
			public override int Compare(Cell x, Cell y)
			{
				if (this._Target.Physics.CurrentCell == null)
				{
					return 0;
				}
				if (x == y)
				{
					return 0;
				}
				if (x.X == y.X && x.Y == y.Y)
				{
					return 0;
				}
				int x2 = this._Target.Physics.CurrentCell.X;
				int y2 = this._Target.Physics.CurrentCell.Y;
				int num = (x2 - x.X) * (x2 - x.X) + (y2 - x.Y) * (y2 - x.Y);
				int num2 = (x2 - y.X) * (x2 - y.X) + (y2 - y.Y) * (y2 - y.Y);
				if (num == num2)
				{
					return Stat.Random(-1, 1);
				}
				return num.CompareTo(num2);
			}

			// Token: 0x040067CF RID: 26575
			private XRL.World.GameObject _Target;
		}

		// Token: 0x02001A07 RID: 6663
		public class SortPoint
		{
			// Token: 0x0600E5F6 RID: 58870 RVA: 0x005302B5 File Offset: 0x0052E4B5
			public SortPoint(int _x, int _y)
			{
				this.X = _x;
				this.Y = _y;
			}

			// Token: 0x040067D0 RID: 26576
			public int X;

			// Token: 0x040067D1 RID: 26577
			public int Y;
		}

		// Token: 0x02001A08 RID: 6664
		public class SortBydistanceToPlayer : Comparer<XRLCore.SortPoint>
		{
			// Token: 0x0600E5F7 RID: 58871 RVA: 0x005302CC File Offset: 0x0052E4CC
			public override int Compare(XRLCore.SortPoint x, XRLCore.SortPoint y)
			{
				if (x.Equals(y))
				{
					return 0;
				}
				if (x == y)
				{
					return 0;
				}
				Cell currentCell = The.Player.CurrentCell;
				if (currentCell == null)
				{
					return 0;
				}
				if (x == y)
				{
					return 0;
				}
				if (x.X == y.X && x.Y == y.Y)
				{
					return 0;
				}
				int x2 = currentCell.X;
				int y2 = currentCell.Y;
				int num = (x2 - x.X) * (x2 - x.X) + (y2 - x.Y) * (y2 - x.Y);
				int num2 = (x2 - y.X) * (x2 - y.X) + (y2 - y.Y) * (y2 - y.Y);
				if (num == num2)
				{
					return Stat.Random(-1, 1);
				}
				return num.CompareTo(num2);
			}
		}

		// Token: 0x02001A09 RID: 6665
		[UIView("NewGame", true, true, false, "Menu", null, false, 0, false)]
		public class EmptyCoreUIs
		{
		}
	}
}
