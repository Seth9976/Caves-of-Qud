using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using Qud.API;
using UnityEngine;
using XRL.EditorFormats.Map;
using XRL.Language;
using XRL.UI;
using XRL.Wish;
using XRL.World;

namespace XRL
{
	// Token: 0x020004ED RID: 1261
	[HasWishCommand]
	[Serializable]
	public class ChavvahSystem : IGameSystem
	{
		// Token: 0x06003667 RID: 13927 RVA: 0x001065A4 File Offset: 0x001047A4
		public void RestoreDisplaced()
		{
			Zone zone = The.ZoneManager.GetZone(this.DisplacedID);
			Dictionary<string, XRL.World.GameObject> cachedObjects = The.ZoneManager.CachedObjects;
			foreach (XRL.World.GameObject gameObject in zone.GetObjectsWithProperty("ChavvahTrunkObject"))
			{
				Cell currentCell = gameObject.CurrentCell;
				if (currentCell != null)
				{
					string id = gameObject.ID;
					this.Trunk[id] = currentCell.X | (currentCell.Y << 16);
					cachedObjects[id] = gameObject;
					gameObject.RemoveFromContext(null);
				}
			}
			foreach (KeyValuePair<string, int> keyValuePair in this.Displaced)
			{
				XRL.World.GameObject gameObject2;
				if (cachedObjects.TryGetValue(keyValuePair.Key, out gameObject2))
				{
					cachedObjects.Remove(keyValuePair.Key);
					Cell cell = zone.GetCell(keyValuePair.Value & 255, keyValuePair.Value >> 16);
					if (cell != null)
					{
						cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			this.DisplacedID = null;
			this.Displaced.Clear();
			The.ZoneManager.SuspendZone(zone);
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x00106708 File Offset: 0x00104908
		public void PlaceTrunk(Zone Z)
		{
			this.DisplacedID = Z.ZoneID;
			List<XRL.World.GameObject> remove = XRL.World.Event.NewGameObjectList();
			if (this.Trunk.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				GamePartBlueprint part = GameObjectFactory.Factory.Blueprints["RoamingChavvahTrunkBase"].GetPart("MapChunkPlacement");
				MapFile mapFile = MapFile.Resolve(part.GetParameterString("Map", null), true);
				int num;
				part.TryGetParameter<int>("Width", out num);
				int num2;
				part.TryGetParameter<int>("Height", out num2);
				Action<Cell> <>9__0;
				for (int i = 0; i < num2; i++)
				{
					for (int j = 0; j < num; j++)
					{
						Cell cell = Z.GetCell((Z.Width - num) / 2 + j, (Z.Height - num2) / 2 + i);
						if (cell != null)
						{
							MapFileCell mapFileCell = mapFile.Cells[j, i];
							Cell cell2 = cell;
							bool flag = true;
							Action<Cell> action;
							if ((action = <>9__0) == null)
							{
								action = (<>9__0 = delegate(Cell x)
								{
									this.DisplaceSolid(x, remove);
								});
							}
							mapFileCell.ApplyTo(cell2, flag, action, null, null, null, new Action<XRL.World.GameObject>(this.MarkObject), null, null);
						}
					}
				}
				return;
			}
			Dictionary<string, XRL.World.GameObject> cachedObjects = The.ZoneManager.CachedObjects;
			foreach (KeyValuePair<string, int> keyValuePair in this.Trunk)
			{
				XRL.World.GameObject gameObject;
				if (cachedObjects.TryGetValue(keyValuePair.Key, out gameObject))
				{
					cachedObjects.Remove(keyValuePair.Key);
					Cell cell3 = Z.GetCell(keyValuePair.Value & 255, keyValuePair.Value >> 16);
					if (cell3 != null)
					{
						this.DisplaceSolid(cell3, remove);
						cell3.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			this.Trunk.Clear();
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x001068F0 File Offset: 0x00104AF0
		public void MarkObject(XRL.World.GameObject Object)
		{
			Object.SetIntProperty("ChavvahTrunkObject", 1, false);
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x00106900 File Offset: 0x00104B00
		public void DisplaceSolid(Cell Cell, List<XRL.World.GameObject> ToRemove)
		{
			foreach (XRL.World.GameObject gameObject in Cell.Objects)
			{
				if (gameObject.Physics.IsReal && gameObject.Render != null && gameObject.Render.RenderLayer > 1 && !gameObject.IsPlayerControlled())
				{
					ToRemove.Add(gameObject);
				}
			}
			if (ToRemove.Count != 0)
			{
				Dictionary<string, XRL.World.GameObject> cachedObjects = The.ZoneManager.CachedObjects;
				for (int i = ToRemove.Count - 1; i >= 0; i--)
				{
					XRL.World.GameObject gameObject2 = ToRemove[i];
					string id = gameObject2.ID;
					this.Displaced[id] = Cell.X | (Cell.Y << 16);
					cachedObjects[id] = gameObject2;
					gameObject2.RemoveFromContext(null);
					ToRemove.RemoveAt(i);
				}
			}
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x001069EC File Offset: 0x00104BEC
		private bool NoMoonStair(Cell Cell)
		{
			XRL.World.GameObject firstObjectThatInheritsFrom = Cell.GetFirstObjectThatInheritsFrom("Terrain");
			if (firstObjectThatInheritsFrom == null)
			{
				Debug.LogError(string.Format("No terrain in world cell at {0}", Cell));
				return true;
			}
			return firstObjectThatInheritsFrom.Blueprint != "TerrainMoonStair";
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x00106A2C File Offset: 0x00104C2C
		public List<Location2D> GetTargetParasangs()
		{
			Zone zone = The.ZoneManager.GetZone("JoppaWorld");
			List<Location2D> list = new List<Location2D>(20);
			for (int i = 0; i < zone.Height; i++)
			{
				for (int j = 0; j < zone.Width; j++)
				{
					Cell cell = zone.GetCell(j, i);
					if (!this.NoMoonStair(cell) && !cell.AnyLocalAdjacentCell(new Predicate<Cell>(this.NoMoonStair)))
					{
						list.Add(Location2D.Get(j, i));
					}
				}
			}
			return list;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x00106AAA File Offset: 0x00104CAA
		public bool IsValidLocation(string ZoneID)
		{
			return ZoneID != this.TrunkID && !The.ZoneManager.IsZoneLive(ZoneID) && JournalAPI.GetMapNotesForZone(ZoneID).IsNullOrEmpty<JournalMapNote>() && The.ZoneManager.CountBuildersFor(ZoneID, null) == 0;
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x00106AE8 File Offset: 0x00104CE8
		public string PickLocation()
		{
			List<Location2D> targetParasangs = this.GetTargetParasangs();
			if (targetParasangs.Count == 0)
			{
				return null;
			}
			targetParasangs.ShuffleInPlace<Location2D>(null);
			List<Location2D> within = Location2D.GetWithin(0, 0, 2, 2);
			foreach (Location2D location2D in targetParasangs)
			{
				within.ShuffleInPlace<Location2D>(null);
				foreach (Location2D location2D2 in within)
				{
					string text = ZoneID.Assemble("JoppaWorld", location2D.X, location2D.Y, location2D2.X, location2D2.Y, 10);
					if (this.IsValidLocation(text))
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x00106BD0 File Offset: 0x00104DD0
		public bool Reveal(bool Attune = true)
		{
			JournalMapNote journalMapNote = JournalAPI.GetMapNote("$chavvahcrown");
			if (journalMapNote == null)
			{
				if (this.ActiveAt == -1L)
				{
					this.ActiveAt = Calendar.TotalTimeTicks;
				}
				JournalAPI.AddMapNote(this.TrunkID, "The roaming keter of Chavvah, Tree of Life", "Oddities", null, "$chavvahcrown", false, true, -1L, false);
				journalMapNote = JournalAPI.GetMapNote("$chavvahcrown");
			}
			else if (this.Attuned)
			{
				return false;
			}
			Popup.Show(Attune ? "You touch the chiming rock. White noise carries on a distant wind." : ("You discover " + Grammar.InitLowerIfArticle(journalMapNote.Text) + "!"), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			JournalMapNote journalMapNote2 = journalMapNote;
			this.Attuned = Attune;
			journalMapNote2.Tracked = Attune;
			journalMapNote.Reveal(null, false);
			return true;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x00106C84 File Offset: 0x00104E84
		public void Hide()
		{
			JournalMapNote mapNote = JournalAPI.GetMapNote("$chavvahcrown");
			if (mapNote == null)
			{
				return;
			}
			if (this.Attuned && mapNote.Revealed)
			{
				Popup.Show("Chavvah roams out of feeling range. You are no longer attuned.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			JournalAPI.DeleteMapNote(mapNote);
			this.ActiveAt = -1L;
			if (The.ActiveZone.IsWorldMap())
			{
				The.ActiveZone.Activated();
			}
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x00106CE9 File Offset: 0x00104EE9
		public static bool IsChavvahBuilder(ZoneBuilderBlueprint Blueprint)
		{
			return Blueprint.Class == "MapBuilder" && Blueprint.GetParameter<string>("FileName", "").StartsWith("Chavvah");
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x00106D1C File Offset: 0x00104F1C
		public void SetCrownLocation(string ZoneID)
		{
			string text = ZoneID;
			string text2 = this.TrunkID;
			for (int i = 1; i <= 3; i++)
			{
				text = The.ZoneManager.GetZoneFromIDAndDirection(text, "U");
				if (text2 != null)
				{
					text2 = The.ZoneManager.GetZoneFromIDAndDirection(text2, "U");
					The.ZoneManager.RemoveZoneBuilders(text2, new Predicate<ZoneBuilderBlueprint>(ChavvahSystem.IsChavvahBuilder));
					The.ZoneManager.RemoveZoneProperty(text2, "ConfirmPullDown");
					The.ZoneManager.MoveZone(text2, text, true);
				}
				The.ZoneManager.AddZoneBuilder(text, 6000, "MapBuilder", "FileName", string.Format("ChavvahCrownLv{0}.rpm", i), "ClearChasms", true);
				The.ZoneManager.SetZoneProperty(text, "ConfirmPullDown", true);
			}
			this.TrunkID = ZoneID;
			ChavvahSystem.RoamLocation[0] = text2;
			ChavvahSystem.RoamLocation[1] = ZoneID;
			GenericCommandEvent.Send(base.Game, "AfterChavvahLocationSet", null, ChavvahSystem.RoamLocation, this, 0, 0);
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x00106E1B File Offset: 0x0010501B
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
			Registrar.Register(TravelSpeedEvent.ID, 0, false);
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x00106E37 File Offset: 0x00105037
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.TickLocation(E.Zone);
			return base.HandleEvent(E);
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x00106E4C File Offset: 0x0010504C
		public override bool HandleEvent(TravelSpeedEvent E)
		{
			Zone currentZone = E.Actor.CurrentZone;
			if (currentZone != null)
			{
				this.TickLocation(currentZone);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x00106E78 File Offset: 0x00105078
		public void TickLocation(Zone Z)
		{
			if (this.TrunkID == null)
			{
				this.SetCrownLocation(this.PickLocation());
			}
			if (this.ActiveAt != -1L && Calendar.TotalTimeTicks - this.ActiveAt > 36000L)
			{
				ZoneID.Match(this.TrunkID, The.ZoneManager.ActiveZone.ZoneID);
			}
			if (this.DisplacedID != this.TrunkID)
			{
				if (!this.DisplacedID.IsNullOrEmpty())
				{
					this.RestoreDisplaced();
				}
				if (this.TrunkID == Z.ZoneID)
				{
					this.PlaceTrunk(Z);
					this.Reveal(false);
				}
			}
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x00106F1C File Offset: 0x0010511C
		public override void Read(SerializationReader Reader)
		{
			this.Attuned = Reader.ReadBoolean();
			this.ActiveAt = Reader.ReadInt64();
			this.TrunkID = Reader.ReadOptimizedString();
			this.Trunk = Reader.ReadDictionary<string, int>();
			this.DisplacedID = Reader.ReadOptimizedString();
			this.Displaced = Reader.ReadDictionary<string, int>();
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x00106F74 File Offset: 0x00105174
		public override void Write(SerializationWriter Writer)
		{
			Writer.Write(this.Attuned);
			Writer.Write(this.ActiveAt);
			Writer.Write(this.TrunkID);
			Writer.Write<string, int>(this.Trunk);
			Writer.Write(this.DisplacedID);
			Writer.Write<string, int>(this.Displaced);
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x00106FCC File Offset: 0x001051CC
		[WishCommand("chavvah:warp", null)]
		public static void ChavvahWarp()
		{
			ChavvahSystem system = The.Game.GetSystem<ChavvahSystem>();
			if (!string.IsNullOrEmpty((system != null) ? system.TrunkID : null))
			{
				The.Player.ZoneTeleport(system.TrunkID, -1, -1, null, null, null, null, "You are transported!", true, null);
			}
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x00107015 File Offset: 0x00105215
		[WishCommand("chavvah:roam", null)]
		public static void ChavvahRoam()
		{
			The.Game.GetSystem<ChavvahSystem>().ActiveAt = 0L;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x00107028 File Offset: 0x00105228
		[WishCommand("chavvah:possible", null)]
		public static void ChavvahPossibleBare()
		{
			ChavvahSystem.ChavvahPossible(null);
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x00107030 File Offset: 0x00105230
		[WishCommand("chavvah:possible", null)]
		public static void ChavvahPossible(string Parameter)
		{
			bool flag = Parameter != null && Parameter.StartsWith("detail");
			ChavvahSystem system = The.Game.GetSystem<ChavvahSystem>();
			List<Location2D> targetParasangs = system.GetTargetParasangs();
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			Zone zone = The.ZoneManager.GetZone("JoppaWorld");
			zone.LightAll(LightLevel.Light);
			zone.VisAll();
			zone.Render(scrapBuffer);
			List<Location2D> within = Location2D.GetWithin(0, 0, 2, 2);
			foreach (Location2D location2D in targetParasangs)
			{
				ConsoleChar consoleChar = scrapBuffer[location2D.X, location2D.Y];
				consoleChar.Background = Color.red;
				if (flag)
				{
					int num = 0;
					foreach (Location2D location2D2 in within)
					{
						string text = ZoneID.Assemble("JoppaWorld", location2D.X, location2D.Y, location2D2.X, location2D2.Y, 10);
						if (system.IsValidLocation(text))
						{
							num++;
						}
					}
					consoleChar.Tile = null;
					consoleChar.Char = (char)(num + 48);
				}
			}
			scrapBuffer.Draw();
			Keyboard.getch();
		}

		// Token: 0x04001B3A RID: 6970
		[NonSerialized]
		public const string SECRET_ID = "$chavvahcrown";

		// Token: 0x04001B3B RID: 6971
		[NonSerialized]
		public const int ROAM_DAYS = 30;

		// Token: 0x04001B3C RID: 6972
		[NonSerialized]
		public const int CROWN_LEVELS = 3;

		// Token: 0x04001B3D RID: 6973
		[NonSerialized]
		public bool Attuned;

		// Token: 0x04001B3E RID: 6974
		[NonSerialized]
		public long ActiveAt = -1L;

		// Token: 0x04001B3F RID: 6975
		[NonSerialized]
		public string TrunkID;

		// Token: 0x04001B40 RID: 6976
		[NonSerialized]
		public Dictionary<string, int> Trunk = new Dictionary<string, int>();

		// Token: 0x04001B41 RID: 6977
		[NonSerialized]
		public string DisplacedID;

		// Token: 0x04001B42 RID: 6978
		[NonSerialized]
		public Dictionary<string, int> Displaced = new Dictionary<string, int>();

		// Token: 0x04001B43 RID: 6979
		[NonSerialized]
		private static readonly string[] RoamLocation = new string[2];
	}
}
