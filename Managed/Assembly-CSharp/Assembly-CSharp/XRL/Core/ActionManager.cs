using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using ConsoleLib.Console;
using Genkit;
using Qud.UI;
using UnityEngine;
using XRL.Collections;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.AI.Pathfinding;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.Core
{
	// Token: 0x0200062F RID: 1583
	[Serializable]
	public class ActionManager
	{
		// Token: 0x06004209 RID: 16905 RVA: 0x00154A7C File Offset: 0x00152C7C
		public void Save(SerializationWriter Writer)
		{
			Writer.WriteFields<ActionManager>(this, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			Writer.WriteOptimized(this.ActionQueue.Count);
			foreach (XRL.World.GameObject gameObject in this.ActionQueue)
			{
				Writer.WriteGameObject(gameObject);
			}
			Writer.WriteOptimized(this.Commands.Count);
			foreach (ActionCommandEntry actionCommandEntry in this.Commands)
			{
				actionCommandEntry.Write(Writer);
			}
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x00154B44 File Offset: 0x00152D44
		public static ActionManager Load(SerializationReader Reader)
		{
			ActionManager actionManager = Reader.ReadInstanceFields<ActionManager>(BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
			int num = Reader.ReadOptimizedInt32();
			bool flag = false;
			XRL.World.GameObject player = The.Player;
			actionManager.OldCommands = null;
			actionManager.ActionQueue.EnsureCapacity(num);
			for (int i = 0; i < num; i++)
			{
				XRL.World.GameObject gameObject = Reader.ReadGameObject("ActionQueue");
				if (gameObject == null)
				{
					if (!flag)
					{
						flag = true;
					}
				}
				else if (gameObject != player)
				{
					actionManager.ActionQueue.Enqueue(gameObject);
				}
			}
			if (!flag)
			{
				actionManager.ActionQueue.Enqueue(null);
			}
			if (player != null)
			{
				actionManager.ActionQueue.Push(player);
				Statistic energy = player.Energy;
				if (energy != null && energy.Value < 1000)
				{
					player.Energy.BaseValue = 1000;
				}
			}
			num = Reader.ReadOptimizedInt32();
			for (int j = 0; j < num; j++)
			{
				ActionCommandEntry actionCommandEntry = default(ActionCommandEntry);
				actionCommandEntry.Read(Reader);
				actionManager.Commands.Add(actionCommandEntry);
			}
			return actionManager;
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x00154C38 File Offset: 0x00152E38
		public void FinalizeRead()
		{
			int i = 0;
			int count = this.ActionQueue.Count;
			while (i < count)
			{
				XRL.World.GameObject gameObject = this.ActionQueue[i];
				if (gameObject != null)
				{
					gameObject.ApplyActiveRegistrar();
					if (gameObject.Abilities != null)
					{
						this.AbilityObjects.Add(gameObject);
					}
				}
				i++;
			}
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x00154C87 File Offset: 0x00152E87
		public void Release()
		{
			this.ActionQueue.Clear();
			this.AbilityObjects.Clear();
			this.LiveObjects.Clear();
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x00154CAC File Offset: 0x00152EAC
		public bool HasActionDescendedFrom<T>() where T : IActionCommand
		{
			ActionCommandEntry[] array = this.Commands.GetArray();
			int i = 0;
			int count = this.Commands.Count;
			while (i < count)
			{
				if (array[i].Command is T)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x00154CF3 File Offset: 0x00152EF3
		public bool HasAction<T>() where T : IActionCommand
		{
			return this.HasAction(typeof(T));
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x00154D08 File Offset: 0x00152F08
		public bool HasAction(Type Type)
		{
			ActionCommandEntry[] array = this.Commands.GetArray();
			int i = 0;
			int count = this.Commands.Count;
			while (i < count)
			{
				if (array[i].Command.GetType() == Type)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x00154D50 File Offset: 0x00152F50
		public bool HasAction(Guid ID)
		{
			ActionCommandEntry[] array = this.Commands.GetArray();
			int i = 0;
			int count = this.Commands.Count;
			while (i < count)
			{
				if (array[i].ID == ID)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x00154D98 File Offset: 0x00152F98
		public void EnqueueAction(IActionCommand Command, int Delay = 0)
		{
			this.EnqueueAction(new ActionCommandEntry(Command, Delay));
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x00154DA7 File Offset: 0x00152FA7
		public void EnqueueAction(Guid ID, IActionCommand Command, int Delay = 0)
		{
			this.EnqueueAction(new ActionCommandEntry(ID, Command, Delay));
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x00154DB8 File Offset: 0x00152FB8
		public void EnqueueAction(ActionCommandEntry Entry)
		{
			int num = this.Commands.Count;
			int commandChunk = this.CommandChunk;
			ActionCommandEntry[] array = this.Commands.GetArray();
			int priority = Entry.Command.Priority;
			while (num > commandChunk && array[num - 1].Command.Priority < priority)
			{
				num--;
			}
			this.Commands.Insert(num, Entry);
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x00154E1D File Offset: 0x0015301D
		public void DequeueAction(int Index)
		{
			this.Commands.RemoveAt(Index);
			if (Index < this.CommandChunk)
			{
				this.CommandChunk--;
			}
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x00154E44 File Offset: 0x00153044
		public int DequeueActionsDescendedFrom<T>() where T : IActionCommand
		{
			int num = 0;
			ActionCommandEntry[] array = this.Commands.GetArray();
			for (int i = this.Commands.Count - 1; i >= 0; i--)
			{
				if (array[i].Command is T)
				{
					this.DequeueAction(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x00154E96 File Offset: 0x00153096
		public int DequeueActions<T>() where T : IActionCommand
		{
			return this.DequeueActions(typeof(T));
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x00154EA8 File Offset: 0x001530A8
		public int DequeueActions(Type Type)
		{
			int num = 0;
			ActionCommandEntry[] array = this.Commands.GetArray();
			for (int i = this.Commands.Count - 1; i >= 0; i--)
			{
				if (array[i].Command.GetType() == Type)
				{
					this.DequeueAction(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x00154EFC File Offset: 0x001530FC
		public int DequeueActions(Guid ID)
		{
			int num = 0;
			ActionCommandEntry[] array = this.Commands.GetArray();
			for (int i = this.Commands.Count - 1; i >= 0; i--)
			{
				if (array[i].ID == ID)
				{
					this.DequeueAction(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x00154F50 File Offset: 0x00153150
		public void AddActiveObject(XRL.World.GameObject GO)
		{
			if (GO.IsInGraveyard())
			{
				MetricsManager.LogError("Attempting to add graveyard object '" + GO.DebugName + "' to action queue.");
				return;
			}
			if (!XRL.World.GameObject.Validate(GO))
			{
				MetricsManager.LogError("Attempting to add invalid object '" + GO.DebugName + "' to action queue.");
				return;
			}
			if (GO.Energy == null)
			{
				MetricsManager.LogError("Attempting to add object with no energy stat '" + GO.DebugName + "' to action queue.");
				return;
			}
			Zone currentZone = GO.CurrentZone;
			if ((this.AllowCachedTurns || currentZone == null || currentZone == The.ZoneManager.ActiveZone) && !this.ActionQueue.Contains(GO))
			{
				if (GO.Abilities != null)
				{
					this.AbilityObjects.Add(GO);
				}
				this.ActionQueue.Enqueue(GO);
				GO.ApplyActiveRegistrar();
				AfterAddActiveObjectEvent.Send(GO);
			}
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x00155020 File Offset: 0x00153220
		public void RemoveExternalObjects()
		{
			Zone activeZone = The.ActiveZone;
			for (int i = this.ActionQueue.Count - 1; i >= 0; i--)
			{
				XRL.World.GameObject gameObject = this.ActionQueue[i];
				if (gameObject != null && gameObject.CurrentZone != activeZone)
				{
					this.RemoveActiveObject(i);
				}
			}
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x0015506B File Offset: 0x0015326B
		public void RemoveActiveObject(XRL.World.GameObject Object)
		{
			if (this.ActionQueue.Remove(Object))
			{
				this.AbilityObjects.Remove(Object);
				Object.ApplyActiveUnregistrar();
				AfterRemoveActiveObjectEvent.Send(Object);
			}
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x00155094 File Offset: 0x00153294
		public void RemoveActiveObject(int Index)
		{
			XRL.World.GameObject gameObject = this.ActionQueue.RemoveAt(Index);
			this.AbilityObjects.Remove(gameObject);
			try
			{
				gameObject.ApplyActiveUnregistrar();
				AfterRemoveActiveObjectEvent.Send(gameObject);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error after removing active object", ex, "game_exception");
			}
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x001550EC File Offset: 0x001532EC
		public void UpdateMinimap()
		{
			if (this.minimapScratch == null)
			{
				this.minimapScratch = new Color32[4000];
			}
			if (!GameManager.Instance.DisplayMinimap)
			{
				return;
			}
			try
			{
				XRL.World.GameObject player = The.Player;
				Zone zone = ((player != null) ? player.CurrentZone : null);
				if (zone != null)
				{
					for (int i = 0; i < zone.Height; i++)
					{
						for (int j = 0; j < zone.Width; j++)
						{
							Cell cell = zone.GetCell(j, i);
							int num = j + (24 - i) * 2 * 80;
							int num2 = j + ((24 - i) * 2 + 1) * 80;
							cell.RefreshMinimapColor();
							this.minimapScratch[num].r = (this.minimapScratch[num2].r = cell.minimapCacheColor.r);
							this.minimapScratch[num].g = (this.minimapScratch[num2].g = cell.minimapCacheColor.g);
							this.minimapScratch[num].b = (this.minimapScratch[num2].b = cell.minimapCacheColor.b);
							this.minimapScratch[num].a = (this.minimapScratch[num2].a = cell.minimapCacheColor.a);
						}
					}
					Color32[] minimapColors = GameManager.minimapColors;
					lock (minimapColors)
					{
						for (int k = 0; k < 4000; k++)
						{
							GameManager.minimapColors[k] = this.minimapScratch[k];
						}
					}
					GameManager.Instance.uiQueue.queueSingletonTask("updateMinimap", delegate
					{
						GameManager.Instance.UpdateMinimap();
					}, false);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("minimap update", ex);
			}
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x0015532C File Offset: 0x0015352C
		public void AddLiveObject(XRL.World.GameObject Object)
		{
			this.LiveObjects.Add(Object);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x0015533C File Offset: 0x0015353C
		public void TickAbilityCooldowns(int Segments = 1)
		{
			foreach (XRL.World.GameObject gameObject in this.AbilityObjects)
			{
				gameObject.Abilities.TickCooldowns(Segments);
			}
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x00155394 File Offset: 0x00153594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ShouldRemove(XRL.World.GameObject Object)
		{
			return !Object.Live || !Object.IsValid() || Object.CurrentZone == null || Object.CurrentZone.Suspended;
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x001553BC File Offset: 0x001535BC
		public void ProcessSingleTurn(long TimeTick, int Amount)
		{
			this.ProcessingTurnTick = true;
			EndTurnEvent instance = SingletonEvent<EndTurnEvent>.Instance;
			foreach (KeyValuePair<string, Zone> keyValuePair in The.ZoneManager.CachedZones)
			{
				string text;
				Zone zone;
				keyValuePair.Deconstruct(out text, out zone);
				Zone zone2 = zone;
				if (!zone2.Suspended && !zone2.Stale)
				{
					zone2.TurnTick(TimeTick, Amount);
					zone2.HandleEvent(instance);
				}
			}
			Rack<XRL.World.GameObject> liveObjects = this.LiveObjects;
			XRL.World.GameObject[] array = liveObjects.GetArray();
			liveObjects.ShuffleInPlace(null);
			for (int i = liveObjects.Count - 1; i >= 0; i--)
			{
				XRL.World.GameObject gameObject = array[i];
				if (ActionManager.ShouldRemove(gameObject))
				{
					liveObjects.RemoveAt(i);
					gameObject.Live = false;
				}
				else
				{
					gameObject.TurnTick(TimeTick, Amount);
					gameObject.HandleEvent(instance);
				}
			}
			this.ProcessingTurnTick = false;
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x001554B4 File Offset: 0x001536B4
		public void ProcessTurnTick(long TimeTick, int Amount)
		{
			this.ProcessingTurnTick = true;
			foreach (KeyValuePair<string, Zone> keyValuePair in The.ZoneManager.CachedZones)
			{
				string text;
				Zone zone;
				keyValuePair.Deconstruct(out text, out zone);
				Zone zone2 = zone;
				if (!zone2.Suspended && !zone2.Stale)
				{
					zone2.TurnTick(TimeTick, Amount);
				}
			}
			Rack<XRL.World.GameObject> liveObjects = this.LiveObjects;
			XRL.World.GameObject[] array = liveObjects.GetArray();
			liveObjects.ShuffleInPlace(null);
			for (int i = liveObjects.Count - 1; i >= 0; i--)
			{
				XRL.World.GameObject gameObject = array[i];
				if (ActionManager.ShouldRemove(gameObject))
				{
					liveObjects.RemoveAt(i);
					gameObject.Live = false;
				}
				else
				{
					gameObject.TurnTick(TimeTick, Amount);
				}
			}
			this.ProcessingTurnTick = false;
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x00155590 File Offset: 0x00153790
		private XRL.World.GameObject FindAutoexploreObjectToOpen(XRL.World.GameObject Actor, Cell Cell, ref bool? Hostiles, Func<bool> CheckHostiles)
		{
			bool? flag = null;
			int i = 0;
			int count = Cell.Objects.Count;
			while (i < count)
			{
				bool flag2 = flag.GetValueOrDefault();
				bool flag3;
				if (flag == null)
				{
					flag2 = Options.AutoexploreChests;
					flag = new bool?(flag2);
					flag3 = flag2;
				}
				else
				{
					flag3 = flag2;
				}
				if (flag3 && Cell.Objects[i].ShouldAutoexploreAsChest())
				{
					flag2 = Hostiles.GetValueOrDefault();
					bool flag4;
					if (Hostiles == null)
					{
						flag2 = CheckHostiles();
						Hostiles = new bool?(flag2);
						flag4 = flag2;
					}
					else
					{
						flag4 = flag2;
					}
					if (!flag4)
					{
						return Cell.Objects[i];
					}
				}
				bool? flag5 = flag;
				flag2 = false;
				if (!((flag5.GetValueOrDefault() == flag2) & (flag5 != null)))
				{
					flag5 = Hostiles;
					flag2 = true;
					if (!((flag5.GetValueOrDefault() == flag2) & (flag5 != null)))
					{
						i++;
						continue;
					}
				}
				return null;
			}
			List<Cell> localAdjacentCells = Cell.GetLocalAdjacentCells();
			int j = 0;
			int count2 = localAdjacentCells.Count;
			while (j < count2)
			{
				Cell cell = localAdjacentCells[j];
				bool flag6 = cell.IsSolidFor(Actor, false);
				int k = 0;
				int count3 = cell.Objects.Count;
				while (k < count3)
				{
					XRL.World.GameObject gameObject = cell.Objects[k];
					bool flag2;
					if (!flag6 || gameObject.CanInteractInCellWithSolid(Actor))
					{
						flag2 = flag.GetValueOrDefault();
						bool flag7;
						if (flag == null)
						{
							flag2 = Options.AutoexploreChests;
							flag = new bool?(flag2);
							flag7 = flag2;
						}
						else
						{
							flag7 = flag2;
						}
						if (flag7 && gameObject.ShouldAutoexploreAsChest())
						{
							flag2 = Hostiles.GetValueOrDefault();
							bool flag8;
							if (Hostiles == null)
							{
								flag2 = CheckHostiles();
								Hostiles = new bool?(flag2);
								flag8 = flag2;
							}
							else
							{
								flag8 = flag2;
							}
							if (!flag8)
							{
								return gameObject;
							}
						}
					}
					bool? flag5 = flag;
					flag2 = false;
					if (!((flag5.GetValueOrDefault() == flag2) & (flag5 != null)))
					{
						flag5 = Hostiles;
						flag2 = true;
						if (!((flag5.GetValueOrDefault() == flag2) & (flag5 != null)))
						{
							k++;
							continue;
						}
					}
					return null;
				}
				j++;
			}
			return null;
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x00155798 File Offset: 0x00153998
		private XRL.World.GameObject FindAutoexploreObjectToProcess(XRL.World.GameObject Actor, Cell Cell, out string Command, out bool AllowRetry, ref bool? Hostiles, Func<bool> Check, bool AutogetOnly = false)
		{
			Command = null;
			AllowRetry = false;
			bool flag = Cell.IsSolidFor(Actor, false);
			int i = 0;
			int count = Cell.Objects.Count;
			while (i < count)
			{
				XRL.World.GameObject gameObject = Cell.Objects[i];
				if ((!flag || gameObject.CanInteractInCellWithSolid(Actor)) && AutoexploreObjectEvent.CheckForAdjacent(out Command, out AllowRetry, Actor, gameObject, AutoAct.Setting, AutoAct.Action, AutogetOnly))
				{
					bool flag2 = Hostiles.GetValueOrDefault();
					bool flag3;
					if (Hostiles == null)
					{
						flag2 = Check();
						Hostiles = new bool?(flag2);
						flag3 = flag2;
					}
					else
					{
						flag3 = flag2;
					}
					if (!flag3)
					{
						return gameObject;
					}
					return null;
				}
				else
				{
					i++;
				}
			}
			if (AutogetOnly && !Options.AutogetFromNearby)
			{
				return null;
			}
			List<Cell> localAdjacentCells = Cell.GetLocalAdjacentCells();
			int j = 0;
			int count2 = localAdjacentCells.Count;
			while (j < count2)
			{
				Cell cell = localAdjacentCells[j];
				flag = cell.IsSolidFor(Actor, false);
				int k = 0;
				int count3 = cell.Objects.Count;
				while (k < count3)
				{
					XRL.World.GameObject gameObject2 = cell.Objects[k];
					if ((!flag || gameObject2.CanInteractInCellWithSolid(Actor)) && AutoexploreObjectEvent.CheckForAdjacent(out Command, out AllowRetry, Actor, gameObject2, AutoAct.Setting, AutoAct.Action, AutogetOnly))
					{
						bool flag2 = Hostiles.GetValueOrDefault();
						bool flag4;
						if (Hostiles == null)
						{
							flag2 = Check();
							Hostiles = new bool?(flag2);
							flag4 = flag2;
						}
						else
						{
							flag4 = flag2;
						}
						if (!flag4)
						{
							return gameObject2;
						}
						return null;
					}
					else
					{
						k++;
					}
				}
				j++;
			}
			return null;
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x0015590C File Offset: 0x00153B0C
		public bool ValidateActor(XRL.World.GameObject Actor)
		{
			Cell cell = Actor.CurrentCell;
			if (!Actor.IsValid())
			{
				MetricsManager.LogError("ActionQueueInconsistency", Actor.IsPlayer() ? "Removing invalid player object" : "Removing invalid object");
			}
			else if (cell == null)
			{
				if (Actor.IsPlayer())
				{
					MetricsManager.LogError("ActionQueueInconsistency", "Player object has no current cell, returning to active spawn cell");
					ZoneManager zoneManager = The.ZoneManager;
					Cell cell2;
					if (zoneManager == null)
					{
						cell2 = null;
					}
					else
					{
						Zone activeZone = zoneManager.ActiveZone;
						cell2 = ((activeZone != null) ? activeZone.GetSpawnCell() : null);
					}
					cell = cell2;
					if (cell != null)
					{
						cell.AddObject(Actor, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						return true;
					}
				}
				MetricsManager.LogError("ActionQueueInconsistency", "Removing object with no current cell " + Actor.DebugName);
			}
			else if (cell.ParentZone == null)
			{
				MetricsManager.LogError("ActionQueueInconsistency", "Removing zoneless cell object " + Actor.DebugName);
			}
			else if (!The.ZoneManager.CachedZonesContains(cell.ParentZone.ZoneID))
			{
				MetricsManager.LogError("ActionQueueInconsistency", "Removing non-cached zone object " + Actor.DebugName);
			}
			else
			{
				if (Actor.Energy != null)
				{
					return true;
				}
				MetricsManager.LogError("ActionQueueInconsistency", "Removing object with no energy stat " + Actor.DebugName);
			}
			return false;
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x00155A3C File Offset: 0x00153C3C
		public void RunSegment()
		{
			try
			{
				XRLGame game = The.Game;
				GameManager.Instance.CurrentGameView = Options.StageViewID;
				XRL.World.Event.ResetPool(true);
				The.Core.AllowWorldMapParticles = false;
				XRL.World.GameObject gameObject = null;
				bool flag = false;
				int num = 0;
				XRL.World.GameObject gameObject2 = null;
				if (The.Player != null && !this.ActionQueue.Contains(The.Player))
				{
					this.ActionQueue.Enqueue(The.Player);
				}
				if (this.ActionQueue.LastIndexOf(null) == -1)
				{
					this.ActionQueue.Enqueue(null);
				}
				while (this.ActionQueue.TryDequeue(out this.Actor))
				{
					this.ActionQueue.Enqueue(this.Actor);
					if (this.Actor == null)
					{
						break;
					}
					if (!game.Running)
					{
						return;
					}
					if (!this.ValidateActor(this.Actor))
					{
						this.RemoveActiveObject(this.Actor);
					}
					else
					{
						this.Actor.Energy.BaseValue += this.Actor.Speed;
						if (this.Actor.Energy != null && this.Actor.Energy.Value >= 1000)
						{
							if (!EarlyBeforeBeginTakeActionEvent.Check(this.Actor) || !BeforeBeginTakeActionEvent.Check(this.Actor) || !BeginTakeActionEvent.Check(this.Actor))
							{
								this.Actor.Energy.BaseValue = 0;
							}
							if (this.Actor.IsPlayer() && Options.LogTurnSeparator && this.Actor.ArePerceptibleHostilesNearby(false, false, null, null, null, -2147483648, 40, false, false))
							{
								game.Player.Messages.Add("[--turn start--]");
							}
						}
						Cell cell = this.Actor.CurrentCell;
						bool flag2 = false;
						int num2 = 0;
						while (this.Actor.Energy != null && this.Actor.Energy.Value >= 1000 && game.Running && !this.SkipSegment)
						{
							game.ActionTicks += 1L;
							if (GameManager.runWholeTurnOnUIThread > 0 && this.Actor != null && this.Actor.IsPlayer())
							{
								this.Actor.Energy.BaseValue = 0;
								GameManager.runWholeTurnOnUIThread--;
								if (GameManager.runWholeTurnOnUIThread <= 0)
								{
									return;
								}
							}
							else
							{
								XRL.World.Event.ResetPool(true);
								flag2 = true;
								num2++;
								try
								{
									if (this.Actor.IsPlayer())
									{
										this.UpdateMinimap();
									}
									this.Actor.CleanEffects();
									int value = this.Actor.Energy.Value;
									if (!BeforeTakeActionEvent.Check(this.Actor))
									{
										flag2 = true;
										if (this.Actor.Energy != null)
										{
											this.Actor.Energy.BaseValue = 0;
										}
									}
									else if (CommandTakeActionEvent.Check(this.Actor))
									{
										this.Actor.CleanEffects();
										if (this.Actor.IsPlayer() && AutoAct.IsInterruptable() && AutoAct.Setting[0] != 'M' && AutoAct.Setting[0] != 'U' && AutoAct.Setting[0] != 'P' && AutoAct.Setting[0] != '!')
										{
											AutoAct.CheckHostileInterrupt();
										}
										XRL.World.GameObject actor = this.Actor;
										if (((actor != null) ? actor.Energy : null) != null && this.Actor.Energy.Value >= 1000 && this.Actor.IsPlayer())
										{
											cell = this.Actor.CurrentCell;
											if (cell != null && cell.ParentZone != null && !cell.ParentZone.IsActive())
											{
												cell.ParentZone.SetActive();
											}
											if (AutoAct.IsActive(false))
											{
												Sidebar.UpdateState();
												XRLCore.CallBeginPlayerTurnCallbacks();
												The.Core.RenderBase(false, false);
												if (AutoAct.Setting != "g" && Keyboard.kbhit())
												{
													AutoAct.Interrupt(null, null, null, false);
													Keyboard.getch();
												}
												else if (cell.X == 0 && (AutoAct.Setting == "W" || AutoAct.Setting == "NW" || AutoAct.Setting == "SW"))
												{
													AutoAct.Interrupt(null, null, null, false);
												}
												else if (cell.X == 79 && (AutoAct.Setting == "E" || AutoAct.Setting == "NE" || AutoAct.Setting == "SE"))
												{
													AutoAct.Interrupt(null, null, null, false);
												}
												else if (cell.Y == 0 && (AutoAct.Setting == "N" || AutoAct.Setting == "NW" || AutoAct.Setting == "NE"))
												{
													AutoAct.Interrupt(null, null, null, false);
												}
												else if (cell.Y == 24 && (AutoAct.Setting == "S" || AutoAct.Setting == "SW" || AutoAct.Setting == "SE"))
												{
													AutoAct.Interrupt(null, null, null, false);
												}
											}
											if (this.RestingUntilHealed && !AutoAct.IsActive(false))
											{
												SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingStatus(null, false);
												this.RestingUntilHealed = false;
												this.RestingUntilHealedCompanions.Clear();
												this.RestingUntilHealedHaveCompanions = false;
											}
											if (AutoAct.IsActive(false))
											{
												CombatJuice.startTurn();
												if (AutoAct.IsRateLimited())
												{
													ActionManager.AutomoveTimer.Reset();
													ActionManager.AutomoveTimer.Start();
												}
												string setting = AutoAct.Setting;
												char c = setting[0];
												if (c == 'X' || c == 'M' || c == 'U' || c == 'P' || c == '!')
												{
													string text = setting.Substring(1);
													int num3 = 0;
													if (c == 'P')
													{
														int num4 = text.IndexOf(':');
														if (num4 != -1)
														{
															num3 = Convert.ToInt32(text.Substring(0, num4));
															text = text.Substring(num4 + 1);
														}
													}
													XRL.World.GameObject gameObject3 = null;
													int num5;
													int num6;
													Cell cell2;
													if (text.Contains(","))
													{
														string[] array = text.Split(',', StringSplitOptions.None);
														num5 = Convert.ToInt32(array[0]);
														num6 = Convert.ToInt32(array[1]);
														cell2 = cell.ParentZone.GetCell(num5, num6);
													}
													else
													{
														gameObject3 = XRL.World.GameObject.FindByID(text);
														cell2 = ((gameObject3 != null) ? gameObject3.CurrentCell : null);
														if (cell2 == null || cell2.ParentZone != cell.ParentZone)
														{
															AutoAct.Interrupt(null, null, null, false);
															goto IL_1A29;
														}
														num5 = cell2.X;
														num6 = cell2.Y;
													}
													if (c == '!' && cell2.DistanceTo(cell) <= 1)
													{
														AutoAct.Interrupt(null, null, null, false);
														The.Core.AttemptSmartUse(cell2, 100);
													}
													else if (c == 'U' && cell2.DistanceTo(cell) <= 1)
													{
														AutoAct.Interrupt(null, null, null, false);
														The.Core.AttemptSmartUse(cell2, 0);
													}
													else if (cell == cell2)
													{
														AutoAct.Interrupt(null, null, null, false);
													}
													else if (c == 'P' && cell2.DistanceTo(this.Actor) <= num3)
													{
														AutoAct.Interrupt(null, null, null, false);
													}
													else if (cell.X == cell2.X && cell.Y == cell2.Y)
													{
														MetricsManager.LogError(string.Concat(new string[]
														{
															"Autoact ",
															setting,
															" had cells ",
															cell.ToString(),
															", ",
															cell2.ToString(),
															" with same coordinates but different objects"
														}));
														AutoAct.Interrupt(null, null, null, false);
													}
													else
													{
														bool flag3 = true;
														if (!cell2.IsExplored() && c == 'M')
														{
															flag3 = false;
															this.AutoexploreMap.ClearSeeds();
															int num7;
															int num8;
															cell.ParentZone.SetMouseMoveAutoexploreSeeds(this.AutoexploreMap, cell2, out num7, out num8);
															this.AutoexploreMap.UsingWeights();
															cell.ParentZone.SetInfluenceMapAutoexploreWeightsAndWalls(this.AutoexploreMap.Weights, this.AutoexploreMap.Walls, true);
															this.AutoexploreMap.RecalculateCostOnly(false);
															string lowestWeightedCostDirectionFrom = this.AutoexploreMap.GetLowestWeightedCostDirectionFrom(cell.Pos2D);
															if (lowestWeightedCostDirectionFrom == ".")
															{
																flag3 = true;
															}
															else
															{
																if (Keyboard.kbhit())
																{
																	AutoAct.Interrupt(null, null, null, false);
																	goto IL_1A29;
																}
																AutoAct.TryToMove(this.Actor, cell, ref gameObject2, null, lowestWeightedCostDirectionFrom, true, true, true, true, true);
															}
														}
														if (flag3)
														{
															string text2;
															if (AutoAct.TryFindPathStep(cell2, out text2))
															{
																AutoAct.TryToMove(this.Actor, cell, ref gameObject2, null, text2, true, true, true, c == 'M' || c == 'P' || c == 'S' || c == 'U' || c == '!', true);
															}
															else if (gameObject3 != null)
															{
																Popup.Show("You cannot find a path to " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																AutoAct.Interrupt(null, null, gameObject3, false);
															}
															else
															{
																Popup.Show("You cannot find a path to your destination.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																AutoAct.Interrupt(null, cell.ParentZone.GetCell(num5, num6), null, false);
															}
														}
													}
												}
												else if (c == 'G')
												{
													char c2 = setting[1];
													string text3;
													if (AutoAct.TryFindEdgeStep(setting[1], out text3))
													{
														if (text3 == ".")
														{
															AutoAct.Interrupt(null, null, null, false);
														}
														else
														{
															AutoAct.TryToMove(this.Actor, cell, ref gameObject2, null, text3, true, true, true, true, true);
														}
													}
													else
													{
														Popup.Show("You cannot find a path toward the " + Directions.GetExpandedDirection(c2.ToString() ?? "") + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
														AutoAct.Interrupt(null, null, null, false);
													}
												}
												else if (c == 'd')
												{
													string[] array2 = setting.Substring(1).Split(',', StringSplitOptions.None);
													int num9 = Convert.ToInt32(array2[0]);
													int num10 = Convert.ToInt32(array2[1]);
													int num11 = Convert.ToInt32(array2[2]);
													int num12 = Convert.ToInt32(array2[3]);
													if (cell.X == num11 && cell.Y == num12)
													{
														AutoAct.Interrupt(null, null, null, false);
													}
													else
													{
														List<Point> list = Zone.Line(num9, num10, num11, num12, true);
														Point point = null;
														for (int i = list.Count - 2; i >= 0; i--)
														{
															if (list[i].X == cell.X && list[i].Y == cell.Y)
															{
																point = list[i + 1];
																break;
															}
														}
														if (point == null)
														{
															AutoAct.Interrupt(null, null, null, false);
														}
														else
														{
															AutoAct.TryToMove(this.Actor, cell, ref gameObject2, cell.ParentZone.GetCell(point), null, true, false, true, true, true);
														}
													}
												}
												else if (c == '?')
												{
													bool? flag4 = null;
													this.AutoexploreMap.ClearSeeds();
													string text4;
													bool flag5;
													XRL.World.GameObject gameObject4 = this.FindAutoexploreObjectToProcess(this.Actor, cell, out text4, out flag5, ref flag4, new Func<bool>(AutoAct.CheckHostileInterrupt), false);
													if (gameObject4 != null)
													{
														if (gameObject4 == gameObject && (!flag5 || num >= 10))
														{
															MetricsManager.LogError(string.Concat(new string[]
															{
																"setting autoexplore suppression on ",
																gameObject4.DebugName,
																" from autoexplore, count ",
																num.ToString(),
																", allow retry ",
																flag.ToString()
															}));
															AutoAct.SetAutoexploreSuppression(gameObject4, true);
															AutoAct.Interrupt(null, null, null, false);
														}
														else
														{
															if (gameObject == gameObject4 && flag == flag5)
															{
																num++;
															}
															else
															{
																gameObject = gameObject4;
																flag = flag5;
																num = 1;
															}
															this.AutoexploreObject(this.Actor, gameObject4, text4, flag5);
														}
													}
													else
													{
														bool? flag6 = flag4;
														bool flag7 = true;
														if (!((flag6.GetValueOrDefault() == flag7) & (flag6 != null)))
														{
															XRL.World.GameObject gameObject5 = this.FindAutoexploreObjectToOpen(this.Actor, cell, ref flag4, new Func<bool>(AutoAct.CheckHostileInterrupt));
															if (gameObject5 != null)
															{
																gameObject5.SetIntProperty("Autoexplored", 1, false);
																gameObject5.FireEvent(XRL.World.Event.New("Open", "Opener", this.Actor));
															}
															else
															{
																flag6 = flag4;
																flag7 = true;
																if (!((flag6.GetValueOrDefault() == flag7) & (flag6 != null)))
																{
																	string text5 = ".";
																	bool flag8 = false;
																	if (Options.GetOption("OptionAutoexploreMode", "") == "Standard")
																	{
																		FasterDMapAutoexplore.FindAutoexploreStep(out text5, out flag8);
																	}
																	else
																	{
																		AutoAct.FindAutoexploreStep(out text5, out flag8);
																	}
																	if (text5 == ".")
																	{
																		if (!cell.ParentZone.AnyUnexplored())
																		{
																			Popup.Show("There doesn't seem to be anywhere else to explore.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																		}
																		else if (flag8)
																		{
																			Popup.Show("There is only darkness from an unusual source left to explore.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																		}
																		else
																		{
																			Popup.Show("There doesn't seem to be anywhere else to explore from here.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
																		}
																		AutoAct.Interrupt(null, null, null, false);
																	}
																	else if (flag4 != null || !AutoAct.CheckHostileInterrupt())
																	{
																		AutoAct.TryToMove(this.Actor, cell, ref gameObject2, null, text5, true, true, true, true, true);
																	}
																}
															}
														}
													}
												}
												else if (c == '<' || c == '>')
												{
													this.AutoexploreMap.ClearSeeds();
													int num13 = 0;
													string text6 = "stairways";
													if (c == '<')
													{
														if (cell.HasObjectWithPart("StairsUp"))
														{
															AutoAct.Interrupt(null, null, null, false);
															goto IL_1A29;
														}
														num13 = cell.ParentZone.SetInfluenceMapStairsUp(this.AutoexploreMap);
														text6 = "stairways leading upward";
													}
													else if (c == '>')
													{
														if (cell.HasObjectWithPart("StairsDown") && !cell.HasObject("Pit") && !cell.HasObject("LazyPit"))
														{
															AutoAct.Interrupt(null, null, null, false);
															goto IL_1A29;
														}
														num13 = cell.ParentZone.SetInfluenceMapStairsDown(this.AutoexploreMap);
														text6 = "stairways leading downward";
													}
													if (num13 == 0)
													{
														Popup.ShowFail("There are no " + text6 + " nearby.", true, true, true);
														AutoAct.Interrupt(null, null, null, false);
													}
													else
													{
														this.AutoexploreMap.UsingWeights();
														cell.ParentZone.SetInfluenceMapAutoexploreWeightsAndWalls(this.AutoexploreMap.Weights, this.AutoexploreMap.Walls, true);
														this.AutoexploreMap.RecalculateCostOnly(false);
														string lowestWeightedCostDirectionFrom2 = this.AutoexploreMap.GetLowestWeightedCostDirectionFrom(cell.Pos2D);
														if (lowestWeightedCostDirectionFrom2 == ".")
														{
															MessageQueue.AddPlayerMessage("You can't figure out how to safely reach the stairs from here.", null, true);
															AutoAct.Interrupt(null, null, null, false);
														}
														else if (!AutoAct.CheckHostileInterrupt())
														{
															if (Keyboard.kbhit())
															{
																AutoAct.Interrupt(null, null, null, false);
															}
															else
															{
																AutoAct.TryToMove(this.Actor, cell, ref gameObject2, null, lowestWeightedCostDirectionFrom2, true, true, true, true, true);
															}
														}
													}
												}
												else if (c == 'g')
												{
													XRL.World.GameObject gameObject6 = null;
													bool flag9 = false;
													bool? flag10 = null;
													bool flag11 = false;
													if (setting.Length == 1)
													{
														flag9 = true;
														string text7;
														XRL.World.GameObject gameObject7 = this.FindAutoexploreObjectToProcess(this.Actor, cell, out text7, out flag11, ref flag10, new Func<bool>(AutoAct.CheckHostileInterrupt), true);
														if (gameObject7 != null)
														{
															this.AutoexploreObject(this.Actor, gameObject7, text7, flag11);
															goto IL_1A29;
														}
														bool? flag6 = flag10;
														bool flag7 = true;
														if ((flag6.GetValueOrDefault() == flag7) & (flag6 != null))
														{
															goto IL_1A29;
														}
													}
													else if (setting[1] == 'o')
													{
														XRL.World.GameObject gameObject8 = XRL.World.GameObject.FindByID(setting.Substring(2));
														if (gameObject8 != null && gameObject8.InSameOrAdjacentCellTo(this.Actor))
														{
															Inventory inventory = gameObject8.Inventory;
															Cell currentCell = gameObject8.GetCurrentCell();
															if (inventory != null && currentCell != null && (!currentCell.IsSolidFor(this.Actor, false) || gameObject8.CanInteractInCellWithSolid(this.Actor)))
															{
																List<XRL.World.GameObject> objects = inventory.GetObjects();
																int j = 0;
																int count = objects.Count;
																while (j < count)
																{
																	if (objects[j].ShouldTakeAll())
																	{
																		if (!AutoAct.CheckHostileInterrupt(true))
																		{
																			gameObject6 = objects[j];
																			flag11 = true;
																			break;
																		}
																		goto IL_1A29;
																	}
																	else
																	{
																		j++;
																	}
																}
															}
														}
													}
													else if (setting[1] == 'd')
													{
														Cell cellFromDirection = cell.GetCellFromDirection(setting.Substring(2), true);
														if (cellFromDirection != null)
														{
															bool flag12 = cellFromDirection.IsSolidFor(this.Actor, false);
															int k = 0;
															int count2 = cellFromDirection.Objects.Count;
															while (k < count2)
															{
																XRL.World.GameObject gameObject9 = cellFromDirection.Objects[k];
																if ((!flag12 || gameObject9.CanInteractInCellWithSolid(this.Actor)) && gameObject9.ShouldTakeAll())
																{
																	if (!AutoAct.CheckHostileInterrupt(true))
																	{
																		gameObject6 = gameObject9;
																		flag11 = true;
																		break;
																	}
																	goto IL_1A29;
																}
																else
																{
																	k++;
																}
															}
														}
													}
													if (gameObject6 == null)
													{
														AutoAct.Resume();
													}
													else if (gameObject6 == gameObject && (!flag || num >= 10))
													{
														MetricsManager.LogError(string.Concat(new string[]
														{
															"setting autoexplore suppression on ",
															gameObject6.DebugName,
															" from autoget, count ",
															num.ToString(),
															", allow retry ",
															flag.ToString()
														}));
														AutoAct.SetAutoexploreSuppression(gameObject6, true);
														AutoAct.Interrupt(null, null, null, false);
													}
													else
													{
														if (gameObject == gameObject6 && flag == flag11)
														{
															num++;
														}
														else
														{
															gameObject = gameObject6;
															flag = flag11;
															num = 1;
														}
														if (!flag9 || gameObject6.CanAutoget(true))
														{
															if (this.Actor.TakeObject(gameObject6, false, false, null, "Autoget", null))
															{
																if (flag9 && XRL.World.GameObject.Validate(ref gameObject6))
																{
																	Sidebar.AddAutogotItem(gameObject6);
																	Sidebar.Update();
																}
															}
															else
															{
																AutoAct.Interrupt(null, null, null, false);
															}
														}
														else
														{
															MetricsManager.LogError("invalid object for autoget, " + gameObject6.DebugName);
															AutoAct.Interrupt(null, null, null, false);
														}
													}
												}
												else if (c == 'o')
												{
													if (AutoAct.Action == null)
													{
														MetricsManager.LogError("had autoact o but no ongoing action");
														AutoAct.Interrupt(null, null, null, false);
													}
													else if (AutoAct.Action.CanComplete())
													{
														AutoAct.Action.Complete();
														AutoAct.Action.End();
														AutoAct.Resume();
													}
													else if (AutoAct.Action.Continue())
													{
														if (AutoAct.Action.CanComplete())
														{
															AutoAct.Action.Complete();
															AutoAct.Action.End();
															AutoAct.Resume();
														}
													}
													else
													{
														AutoAct.Interrupt(null, null, null, false);
													}
												}
												else if (c == 'z')
												{
													if (Calendar.IsDay())
													{
														AutoAct.Interrupt(null, null, null, false);
													}
													else
													{
														this.Actor.PassTurn();
														ActionManager actionManager = The.ActionManager;
														int num14 = actionManager.RestingUntilHealedCount + 1;
														actionManager.RestingUntilHealedCount = num14;
														if (num14 % 10 == 0)
														{
															XRLCore.TenPlayerTurnsPassed();
															The.Core.RenderBase(false, true);
														}
													}
												}
												else if (c == 'r')
												{
													The.ActionManager.RestingUntilHealedCount++;
													bool flag13 = setting.Length > 1 && setting[1] == '+';
													if (flag13)
													{
														Loading.SetLoadingStatus("Resting until party healed... Turn: " + The.ActionManager.RestingUntilHealedCount.ToString(), false);
													}
													else
													{
														Loading.SetLoadingStatus("Resting until healed... Turn: " + The.ActionManager.RestingUntilHealedCount.ToString(), false);
													}
													this.Actor.PassTurn();
													bool flag14 = true;
													if (this.Actor.isDamaged() && this.Actor.HealsNaturally())
													{
														flag14 = false;
													}
													else if (flag13)
													{
														if (this.RestingUntilHealedHaveCompanions)
														{
															foreach (XRL.World.GameObject gameObject10 in this.RestingUntilHealedCompanions)
															{
																if (!XRL.World.GameObject.Validate(gameObject10) || gameObject10.IsNowhere() || !gameObject10.IsLedBy(this.Actor))
																{
																	this.RestingUntilHealedHaveCompanions = false;
																	this.RestingUntilHealedCompanions.Clear();
																	break;
																}
															}
														}
														if (!this.RestingUntilHealedHaveCompanions)
														{
															List<XRL.World.GameObject> companionsReadonly = this.Actor.GetCompanionsReadonly(this.Actor.CurrentZone.Width * 2, null);
															if (!companionsReadonly.IsNullOrEmpty<XRL.World.GameObject>())
															{
																this.RestingUntilHealedCompanions.AddRange(companionsReadonly);
															}
															this.RestingUntilHealedHaveCompanions = true;
														}
														int l = 0;
														int count3 = this.RestingUntilHealedCompanions.Count;
														while (l < count3)
														{
															XRL.World.GameObject gameObject11 = this.RestingUntilHealedCompanions[l];
															if (gameObject11.isDamaged() && gameObject11.HealsNaturally())
															{
																flag14 = false;
																break;
															}
															l++;
														}
													}
													if (flag14)
													{
														AutoAct.Interrupt(null, null, null, false);
													}
													else if (The.ActionManager.RestingUntilHealedCount % 10 == 0)
													{
														XRLCore.TenPlayerTurnsPassed();
														The.Core.RenderBase(false, true);
													}
												}
												else if (c == 'a')
												{
													XRL.World.GameObject target = this.Actor.Target;
													if (target == null || (The.Player.GetConfusion() > 0 && The.Player.GetFuriousConfusion() <= 0))
													{
														AutoAct.Interrupt(null, null, null, false);
													}
													else if (target.Brain != null && !target.IsHostileTowards(this.Actor))
													{
														MessageQueue.AddPlayerMessage(string.Concat(new string[]
														{
															"You will not auto-attack ",
															target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
															" because ",
															target.itis,
															" not hostile to you."
														}), null, true);
														AutoAct.Interrupt(null, null, null, false);
													}
													else
													{
														if (The.Player.GetConfusion() > 0 && The.Player.GetFuriousConfusion() > 0)
														{
															try
															{
																AutoAct.Attacking = true;
																this.Actor.FireEvent(XRL.World.Event.New("CmdMove" + Directions.GetRandomDirection(), 0, 0, 0));
																goto IL_1A29;
															}
															finally
															{
																AutoAct.Attacking = false;
															}
														}
														if (!target.IsVisible())
														{
															MessageQueue.AddPlayerMessage("You cannot see your target.", null, true);
															AutoAct.Interrupt(null, null, null, false);
														}
														else
														{
															Cell currentCell2 = target.CurrentCell;
															int num15 = this.Actor.DistanceTo(target);
															if (num15 == 0)
															{
																Cell randomElement = cell.GetLocalNavigableAdjacentCells(this.Actor, 5).GetRandomElement<Cell>(null);
																if (randomElement == null)
																{
																	MessageQueue.AddPlayerMessage("You can't find a way to navigate to " + target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
																	AutoAct.Interrupt(null, null, target, false);
																}
																else
																{
																	string directionFromCell = cell.GetDirectionFromCell(randomElement, false);
																	if (directionFromCell.IsNullOrEmpty() || directionFromCell == ".")
																	{
																		AutoAct.Interrupt(null, null, null, false);
																	}
																	else if (!this.Actor.Move(directionFromCell, false, false, false, false, true, true, null, null, false, null, null, null, true, false, null, null, 0) || this.Actor.CurrentCell != randomElement)
																	{
																		AutoAct.Interrupt(null, randomElement, null, false);
																	}
																}
															}
															else
															{
																if (num15 == 1)
																{
																	if (currentCell2.GetCombatTarget(this.Actor, false, false, false, 0, null, null, null, null, null, true, false, null) != target)
																	{
																		MessageQueue.AddPlayerMessage("You are unable to attack " + target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
																		AutoAct.Interrupt(null, null, target, true);
																		goto IL_1A29;
																	}
																	string directionFromCell2 = cell.GetDirectionFromCell(currentCell2, false);
																	if (directionFromCell2.IsNullOrEmpty() || directionFromCell2 == ".")
																	{
																		AutoAct.Interrupt(null, null, null, false);
																		goto IL_1A29;
																	}
																	try
																	{
																		AutoAct.Attacking = true;
																		if (!this.Actor.AttackDirection(directionFromCell2, true))
																		{
																			AutoAct.Interrupt(null, currentCell2, null, true);
																		}
																		else if (target.IsInvalid() || target != this.Actor.Target)
																		{
																			AutoAct.Interrupt(null, null, null, false);
																		}
																		goto IL_1A29;
																	}
																	finally
																	{
																		AutoAct.Attacking = false;
																	}
																}
																FindPath findPath = new FindPath(cell, currentCell2, false, true, this.Actor, 20, false, false, false, false, false, null);
																if (!findPath.Usable)
																{
																	MessageQueue.AddPlayerMessage("You can't seem to find a way to reach " + target.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
																	AutoAct.Interrupt(null, null, target, false);
																}
																else
																{
																	AutoAct.TryToMove(this.Actor, cell, findPath.Steps[1], findPath.Directions[0], true, true, true, true, true);
																}
															}
														}
													}
												}
												else if (setting.Contains("."))
												{
													int num16 = Convert.ToInt32(setting.Split('.', StringSplitOptions.None)[1]) - 1;
													if (num16 > 0)
													{
														this.Actor.PassTurn();
														Loading.SetLoadingStatus(The.StringBuilder.Append("Waiting for ").Append(num16.Things("turn", null)).Append("...")
															.ToString(), false);
														AutoAct.Setting = "." + num16.ToString();
														if (num16 % 10 == 0)
														{
															XRLCore.TenPlayerTurnsPassed();
															The.Core.RenderBase(false, true);
														}
													}
													else
													{
														Loading.SetLoadingStatus("Done waiting", false);
														AutoAct.Interrupt(null, null, null, false);
													}
												}
												else if (setting.StartsWith(":"))
												{
													long num17;
													if (long.TryParse(setting.AsSpan(1), out num17) && game.Turns < num17)
													{
														this.Actor.PassTurn();
														int num18 = (int)(num17 - game.Turns);
														Loading.SetLoadingStatus(The.StringBuilder.Append("Waiting for ").Append(num18.Things("round", null)).Append("...")
															.ToString(), false);
														if (num18 % 10 == 0)
														{
															The.Core.RenderBase(false, true);
														}
													}
													else
													{
														Loading.SetLoadingStatus("Done waiting", false);
														AutoAct.Interrupt(null, null, null, false);
													}
												}
												else if (!AutoAct.CheckHostileInterrupt())
												{
													Cell cellFromDirection2 = cell.GetCellFromDirection(setting, true);
													string text8;
													XRL.World.GameObject gameObject12;
													Cell cell3;
													bool flag15;
													if (InterruptAutowalkEvent.Check(this.Actor, cellFromDirection2, out text8, out gameObject12, out cell3, out flag15))
													{
														if (text8 == null && XRL.World.GameObject.Validate(ref gameObject12))
														{
															text8 = "of " + gameObject12.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " " + this.Actor.DescribeDirectionToward(gameObject12, false, false);
														}
														string text9 = text8;
														XRL.World.GameObject gameObject13 = gameObject12;
														AutoAct.Interrupt(text9, cell3, gameObject13, flag15);
													}
													else
													{
														AutoAct.TryToMove(this.Actor, cell, ref gameObject2, null, setting, false, true, true, true, true);
													}
												}
												IL_1A29:
												if (ActionManager.AutomoveTimer.IsRunning)
												{
													ActionManager.AutomoveTimer.Stop();
													if (AutoAct.IsRateLimited())
													{
														int autoexploreRate = Options.AutoexploreRate;
														if (autoexploreRate != 0)
														{
															int num19 = 1000 / autoexploreRate;
															long elapsedMilliseconds = ActionManager.AutomoveTimer.ElapsedMilliseconds;
															if (elapsedMilliseconds < (long)num19)
															{
																Thread.Sleep((int)((long)num19 - elapsedMilliseconds));
															}
														}
													}
												}
											}
											else
											{
												if (this.Actor.Brain.Goals.Count > 0)
												{
													this.Actor.Brain.FireEvent(XRL.World.Event.New("CommandTakeAction", 0, 0, 0));
													The.Core.RenderBase(true, false);
												}
												if (this.SkipPlayerTurn)
												{
													this.SkipPlayerTurn = false;
												}
												else
												{
													if (GameManager.runWholeTurnOnUIThread > 0 && Thread.CurrentThread != XRLCore.CoreThread)
													{
														The.Player.Energy.BaseValue = 0;
														return;
													}
													if (GameManager.runPlayerTurnOnUIThread && Thread.CurrentThread == XRLCore.CoreThread)
													{
														while (!Keyboard.kbhit())
														{
														}
														bool turnWaitFlag = true;
														GameManager.Instance.uiQueue.queueTask(delegate
														{
															The.Core.PlayerTurn();
															turnWaitFlag = false;
														}, 1);
														while (turnWaitFlag)
														{
														}
													}
													else if (Thread.CurrentThread == XRLCore.CoreThread)
													{
														The.Core.PlayerTurn();
														if (!game.Running)
														{
															return;
														}
													}
												}
											}
										}
										else if (this.Actor.IsPlayer())
										{
											The.Core.RenderBase(true, false);
										}
										else if (num2 > 5)
										{
											XRL.World.GameObject actor2 = this.Actor;
											if (((actor2 != null) ? actor2.Energy : null) != null)
											{
												this.Actor.Energy.BaseValue -= 1000;
											}
										}
									}
								}
								catch (Exception ex)
								{
									MetricsManager.LogException("RunSegment", ex, "game_exception");
									XRL.World.GameObject actor3 = this.Actor;
									if (((actor3 != null) ? actor3.Energy : null) != null && !this.Actor.IsPlayer())
									{
										this.Actor.Energy.BaseValue = 0;
									}
								}
							}
						}
						if (flag2)
						{
							EndActionEvent.Send(this.Actor);
						}
						if (this.SkipSegment)
						{
							this.SkipSegment = false;
							break;
						}
					}
				}
				ActionManager.SegCount++;
				game.Segments += 1L;
				this.TickAbilityCooldowns(1);
				The.ZoneManager.CheckEventQueue();
				if (ActionManager.SegCount >= 10)
				{
					if (game.HasRemovedSystems)
					{
						game.RemoveFlaggedSystems();
					}
					EndTurnEvent.Send(game);
					this.ProcessSingleTurn(game.TimeTicks, 1);
					ActionManager.SegCount = 0;
					The.ZoneManager.CheckEventQueue();
					The.ZoneManager.Tick(false);
					game.Turns += 1L;
					game.TimeTicks += 1L;
					XRL.World.GameObject body = game.Player.Body;
					Zone zone = ((body != null) ? body.CurrentZone : null);
					if (zone != null)
					{
						zone.LastPlayerPresence = game.TimeTicks;
					}
				}
				this.RunCommands(true);
			}
			catch (Exception ex2)
			{
				MetricsManager.LogException("RunSegment (final)", ex2, "game_exception");
			}
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x00157824 File Offset: 0x00155A24
		public unsafe void RunCommands(bool Decrement = true)
		{
			this.CommandChunk = this.Commands.Count;
			try
			{
				XRLGame game = The.Game;
				for (int i = 0; i < this.CommandChunk; i++)
				{
					if (!game.Running)
					{
						break;
					}
					ActionCommandEntry actionCommandEntry = *this.Commands.GetReference(i);
					if (Decrement)
					{
						actionCommandEntry.SegmentDelay--;
					}
					if (actionCommandEntry.SegmentDelay <= 0)
					{
						IActionCommand command = actionCommandEntry.Command;
						this.DequeueAction(i--);
						try
						{
							command.Execute(game, this);
						}
						catch (Exception ex)
						{
							Type type = command.GetType();
							string text = "Exception executing command action\n";
							Exception ex2 = ex;
							MetricsManager.LogAssemblyError(type, text + ((ex2 != null) ? ex2.ToString() : null));
						}
					}
				}
			}
			finally
			{
				this.CommandChunk = 0;
			}
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x001578FC File Offset: 0x00155AFC
		public void ProcessIndependentEndTurn(XRL.World.GameObject obj)
		{
			EndTurnEvent.Send(obj);
			obj.CleanEffects();
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x0015790B File Offset: 0x00155B0B
		public bool IsPlayerTurn()
		{
			if (this.Actor != null)
			{
				XRLGame game = XRLCore.Core.Game;
				return ((game != null) ? game.Player._Body : null) == this.Actor;
			}
			return false;
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x0015793C File Offset: 0x00155B3C
		private bool AutoexploreObject(XRL.World.GameObject Actor, XRL.World.GameObject Item, string Command, bool AllowRetry)
		{
			if (!AllowRetry)
			{
				AutoAct.SetAutoexploreActionProperty(Item, Command, 1);
			}
			bool flag2;
			try
			{
				AutoAct.Attacking = Command == "Attack";
				InventoryActionEvent inventoryActionEvent;
				bool flag = InventoryActionEvent.Check(out inventoryActionEvent, Item, Actor, Item, Command, true, false, false, false, false, 0, 0, 0, null, null, null, null);
				if (inventoryActionEvent != null)
				{
					foreach (XRL.World.GameObject gameObject in inventoryActionEvent.Generated)
					{
						if (XRL.World.GameObject.Validate(gameObject))
						{
							Sidebar.AddAutogotItem(gameObject);
							Sidebar.Update();
						}
					}
				}
				if (!flag && AllowRetry)
				{
					MetricsManager.LogError(string.Concat(new string[]
					{
						"setting autoexplore suppression on ",
						Item.DebugName,
						" from failed command ",
						Command,
						", allow retry ",
						AllowRetry.ToString()
					}));
					AutoAct.SetAutoexploreSuppression(Item, true);
				}
				flag2 = flag;
			}
			finally
			{
				AutoAct.Attacking = false;
			}
			return flag2;
		}

		// Token: 0x04002074 RID: 8308
		[NonSerialized]
		public bool SkipPlayerTurn;

		// Token: 0x04002075 RID: 8309
		[NonSerialized]
		public bool SkipSegment;

		// Token: 0x04002076 RID: 8310
		[NonSerialized]
		public XRL.World.GameObject Actor;

		// Token: 0x04002077 RID: 8311
		[NonSerialized]
		public RingDeque<XRL.World.GameObject> ActionQueue = new RingDeque<XRL.World.GameObject>(32);

		// Token: 0x04002078 RID: 8312
		[NonSerialized]
		public List<XRL.World.GameObject> AbilityObjects = new List<XRL.World.GameObject>(32);

		// Token: 0x04002079 RID: 8313
		[NonSerialized]
		public Rack<XRL.World.GameObject> LiveObjects = new Rack<XRL.World.GameObject>(32);

		// Token: 0x0400207A RID: 8314
		[NonSerialized]
		public bool ProcessingTurnTick;

		// Token: 0x0400207B RID: 8315
		[NonSerialized]
		public bool AllowCachedTurns;

		// Token: 0x0400207C RID: 8316
		[NonSerialized]
		public Dictionary<string, int> ZoneSuspendFailures = new Dictionary<string, int>();

		// Token: 0x0400207D RID: 8317
		[NonSerialized]
		public Rack<ActionCommandEntry> Commands = new Rack<ActionCommandEntry>();

		// Token: 0x0400207E RID: 8318
		[NonSerialized]
		public int CommandChunk;

		// Token: 0x0400207F RID: 8319
		public List<CommandQueueEntry> OldCommands;

		// Token: 0x04002080 RID: 8320
		public bool RestingUntilHealed;

		// Token: 0x04002081 RID: 8321
		public int RestingUntilHealedCount;

		// Token: 0x04002082 RID: 8322
		public List<XRL.World.GameObject> RestingUntilHealedCompanions = new List<XRL.World.GameObject>();

		// Token: 0x04002083 RID: 8323
		public bool RestingUntilHealedHaveCompanions;

		// Token: 0x04002084 RID: 8324
		[NonSerialized]
		private InfluenceMap AutoexploreMap = new InfluenceMap(80, 25);

		// Token: 0x04002085 RID: 8325
		[NonSerialized]
		private Cell AutoexploreCell;

		// Token: 0x04002086 RID: 8326
		[NonSerialized]
		private Rack<XRL.World.GameObject> AutoexploreObjects = new Rack<XRL.World.GameObject>();

		// Token: 0x04002087 RID: 8327
		[NonSerialized]
		private Color32[] minimapScratch;

		// Token: 0x04002088 RID: 8328
		[NonSerialized]
		private static int SegCount = 0;

		// Token: 0x04002089 RID: 8329
		[NonSerialized]
		private static Stopwatch AutomoveTimer = new Stopwatch();
	}
}
