using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Annals;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.ObjectBuilders;
using XRL.World.Parts;
using XRL.World.Skills.Cooking;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B07 RID: 2823
	public class VillageBase : ZoneBuilderSandbox
	{
		// Token: 0x060070B6 RID: 28854 RVA: 0x0028D995 File Offset: 0x0028BB95
		public void generateVillageTheme()
		{
			this.villageTheme = this.RollOneFrom("Villages_VillageTheme");
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060070B7 RID: 28855 RVA: 0x0028D9A8 File Offset: 0x0028BBA8
		// (set) Token: 0x060070B8 RID: 28856 RVA: 0x0028D9E0 File Offset: 0x0028BBE0
		public HistoricEntity villageEntity
		{
			get
			{
				if (this._VillageEntity == null && !this.VillageEntityID.IsNullOrEmpty())
				{
					this._VillageEntity = The.Game.sultanHistory.GetEntity(this.VillageEntityID);
				}
				return this._VillageEntity;
			}
			set
			{
				this._VillageEntity = value;
				this.VillageEntityID = ((value != null) ? value.id : null);
			}
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x0028D9FC File Offset: 0x0028BBFC
		public static GameObject getBaseVillager(string villagerBaseFaction, string region, string outlierChance = "10", string FactionVSRegionChance = "50", bool NoRep = false)
		{
			string dynTableName = "DynamicObjectsTable:" + region + "_Creatures";
			Func<GameObjectBlueprint, bool> <>9__3;
			Func<GameObjectBlueprint, bool> <>9__4;
			Predicate<GameObjectBlueprint> <>9__5;
			Func<GameObject> func = FuzzyFunctions.DoThisButRarelyDoThat<GameObject>(delegate
			{
				IEnumerable<GameObjectBlueprint> factionMembers = GameObjectFactory.Factory.GetFactionMembers(villagerBaseFaction, false, true);
				Func<GameObjectBlueprint, bool> func2;
				if ((func2 = <>9__3) == null)
				{
					func2 = (<>9__3 = (GameObjectBlueprint m) => (m.HasPart("Body") || m.HasTagOrProperty("BodySubstitute")) && (m.HasPart("Combat") || m.HasTagOrProperty("BodySubstitute")) && m.HasTag(dynTableName, false) && !m.HasTag("Merchant", false) && !m.HasTag("ExcludeFromVillagePopulations", false) && (!NoRep || !m.HasPart("GivesRep")));
				}
				IEnumerable<GameObjectBlueprint> enumerable = factionMembers.Where<GameObjectBlueprint>(func2);
				if (enumerable.Count<GameObjectBlueprint>() > 0)
				{
					return enumerable.GetRandomElement<GameObjectBlueprint>(null).createOne();
				}
				return null;
			}, FuzzyFunctions.DoThisButRarelyDoThat<GameObject>(delegate
			{
				IEnumerable<GameObjectBlueprint> factionMembers2 = GameObjectFactory.Factory.GetFactionMembers(villagerBaseFaction, false, true);
				Func<GameObjectBlueprint, bool> func3;
				if ((func3 = <>9__4) == null)
				{
					func3 = (<>9__4 = (GameObjectBlueprint m) => (m.HasPart("Body") || m.HasTagOrProperty("BodySubstitute")) && (m.HasPart("Combat") || m.HasPart("BodySubstitute")) && !m.HasTag("Merchant", false) && !m.HasTag("ExcludeFromVillagePopulations", false) && (!NoRep || !m.HasPart("GivesRep")));
				}
				IEnumerable<GameObjectBlueprint> enumerable2 = factionMembers2.Where<GameObjectBlueprint>(func3);
				if (enumerable2.Count<GameObjectBlueprint>() > 0)
				{
					return enumerable2.GetRandomElement<GameObjectBlueprint>(null).createOne();
				}
				return null;
			}, delegate
			{
				Predicate<GameObjectBlueprint> predicate;
				if ((predicate = <>9__5) == null)
				{
					predicate = (<>9__5 = (GameObjectBlueprint ob) => ob.HasTag(dynTableName, false) && (ob.HasPart("Body") || ob.HasTagOrProperty("BodySubstitute")) && (ob.HasPart("Combat") || ob.HasPart("BodySubstitute")) && !ob.HasTag("Merchant", false) && !ob.HasTag("ExcludeFromVillagePopulations", false) && (!NoRep || !ob.HasPart("GivesRep")));
				}
				return EncountersAPI.GetACreature(predicate);
			}, FactionVSRegionChance), outlierChance);
			GameObject gameObject;
			try
			{
				gameObject = func();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("getBaseVillager", ex, "game_exception");
				gameObject = null;
			}
			return gameObject;
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x0028DA94 File Offset: 0x0028BC94
		public GameObject getBaseVillager(bool NoRep = false)
		{
			GameObject gameObject;
			try
			{
				if (this.villageSnapshot.GetProperty("villageGatherReason", "unknown") == "commune")
				{
					gameObject = VillageBase.getBaseVillager(this.villagerBaseFaction, this.region, "70", "70", NoRep);
				}
				else
				{
					gameObject = VillageBase.getBaseVillager(this.villagerBaseFaction, this.region, "10", "50", NoRep);
				}
				if (gameObject == null)
				{
					gameObject = this.getARegionalCreature(false);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Failed to generate villager.", ex, "game_exception");
				gameObject = EncountersAPI.GetACreature((GameObjectBlueprint ob) => ob.HasPart("Combat") && ob.HasPart("Body") && (!NoRep || !ob.HasPart("GivesRep")));
			}
			if (this.signatureSkill != null)
			{
				gameObject.SetStringProperty("WaterRitual_Skill", this.signatureSkill, false);
			}
			if (this.signatureItemBlueprint != null && this.signatureItemExample.IsTakeable())
			{
				gameObject.SetStringProperty("SignatureItemBlueprint", this.signatureItemBlueprint, false);
				gameObject.ReceiveObject(this.signatureItemBlueprint, Stat.Random(1, 3), false, 0, 0, null, null, null, null);
			}
			gameObject.SetIntProperty("Villager", 1, false);
			return gameObject;
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x0028DBC4 File Offset: 0x0028BDC4
		public void setVillagerProperties(GameObject obj)
		{
			try
			{
				obj.Brain.Factions = "";
				obj.Brain.Allegiance.Clear();
				obj.Brain.Allegiance.Add(this.villageFaction, 100);
				obj.Brain.Allegiance.Hostile = false;
				obj.Brain.Allegiance.Calm = true;
				obj.RemovePart<AIPilgrim>();
				obj.RemovePart<ConversationScript>();
				obj.SetIntProperty("ParticipantVillager", 1, false);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Failed to set villager properties.", ex, "game_exception");
			}
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x0028DC6C File Offset: 0x0028BE6C
		public void AddVillagerConversation(GameObject obj, string message, string response = "Live and drink.", string Q1 = null, string A1 = null, bool AppendConversation = false, bool ClearLost = true)
		{
			string property = this.villageSnapshot.GetProperty("signatureTemperament", null);
			string text = (If.d100(40, null, null) ? ("\n\n" + this.villageSnapshot.GetProperty("proverb", "unknown")) : null);
			if (!Q1.IsNullOrEmpty() && !A1.IsNullOrEmpty())
			{
				if (AppendConversation)
				{
					ConversationsAPI.appendSimpleConversationToObject(obj, message, response, Q1, A1, property, null, text, ClearLost);
					return;
				}
				ConversationsAPI.addSimpleConversationToObject(obj, message, response, Q1, A1, property, null, text, ClearLost, true);
				return;
			}
			else
			{
				if (AppendConversation)
				{
					ConversationsAPI.appendSimpleConversationToObject(obj, message, response, property, null, text, ClearLost);
					return;
				}
				ConversationsAPI.addSimpleConversationToObject(obj, message, response, property, null, text, ClearLost, true);
				return;
			}
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x0028DD18 File Offset: 0x0028BF18
		public void setVillageDomesticatedProperties(GameObject obj)
		{
			try
			{
				if (obj.Brain != null)
				{
					obj.Brain.Factions = "";
					obj.Brain.Allegiance.Clear();
					obj.Brain.Allegiance.Add(this.villageFaction, 100);
					obj.Brain.Allegiance.Hostile = false;
					obj.Brain.Allegiance.Calm = true;
				}
				obj.RemovePart<AIPilgrim>();
				obj.RemovePart<ConversationScript>();
				obj.SetIntProperty("VillageDomesticated", 1, false);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Failed to set village domesticated properties.", ex, "game_exception");
			}
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x0028DDC8 File Offset: 0x0028BFC8
		public string RollOneFrom(string PoptablePrefix)
		{
			return PopulationManager.RollOneFrom(this.ResolvePopulationTableName(PoptablePrefix), null, null).Blueprint;
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x0028DDE0 File Offset: 0x0028BFE0
		public GameObject generateVillager(bool bUnique = false)
		{
			GameObject gameObject = this.getBaseVillager(false);
			gameObject.Render.ColorString = "&y";
			this.setVillagerProperties(gameObject);
			if (bUnique)
			{
				gameObject = HeroMaker.MakeHero(gameObject, null, "SpecialVillagerHeroTemplate_Villager", -1, "Hero");
				gameObject.SetIntProperty("NamedVillager", 1, false);
				gameObject.RequirePart<Interesting>(false);
			}
			gameObject.SetIntProperty("SuppressSimpleConversation", 1, false);
			this.AddVillagerConversation(gameObject, gameObject.GetTag("SimpleConversation", "Moon and Sun. Wisdom and will.~May the earth yield for us this season.~Peace, =player.formalAddressTerm=."), "Live and drink.", null, null, false, true);
			this.preprocessVillager(gameObject, false);
			return gameObject;
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x0028DE70 File Offset: 0x0028C070
		public void preprocessVillager(GameObject obj, bool foreign = false)
		{
			obj.RemovePart<Lovely>();
			obj.RemovePart<SecretObject>();
			obj.RemovePart<ConvertSpawner>();
			if (obj.HasPart<AIShopper>())
			{
				if (obj.Brain != null)
				{
					obj.Brain.Wanders = true;
					obj.Brain.WandersRandomly = true;
				}
				obj.RemovePart<AIShopper>();
			}
			if (obj.HasPart<AIPilgrim>())
			{
				if (obj.Brain != null)
				{
					obj.Brain.Wanders = true;
					obj.Brain.WandersRandomly = true;
				}
				obj.RemovePart<AIPilgrim>();
			}
			if (!foreign)
			{
				if (this.villageSnapshot.listProperties.ContainsKey("sharedMutations"))
				{
					foreach (string text in this.villageSnapshot.GetList("sharedMutations"))
					{
						if (If.Chance(80, null, null) && obj.HasPart<Mutations>())
						{
							Mutations part = obj.GetPart<Mutations>();
							if (!part.HasMutation(text))
							{
								part.AddMutation(text, "1d4".RollCached());
							}
						}
					}
				}
				if (this.villageSnapshot.listProperties.ContainsKey("sharedDiseases"))
				{
					foreach (string text2 in this.villageSnapshot.GetList("sharedDiseases"))
					{
						if (If.Chance(80, null, null))
						{
							if (text2 == "Glotrot")
							{
								obj.ApplyEffect(new Glotrot(), null);
								Glotrot effect = obj.GetEffect<Glotrot>();
								if (effect != null)
								{
									effect.Stage = Stat.Random(0, 3);
								}
							}
							if (text2 == "Ironshank")
							{
								obj.ApplyEffect(new Ironshank(), null);
								Ironshank effect2 = obj.GetEffect<Ironshank>();
								if (effect2 != null)
								{
									effect2.SetPenalty(Stat.Random(1, 75));
								}
							}
						}
					}
				}
				if (this.villageSnapshot.listProperties.ContainsKey("sharedTransformations"))
				{
					foreach (string text3 in this.villageSnapshot.GetList("sharedTransformations"))
					{
						if (If.Chance(80, null, null) && text3 == "Mechanical")
						{
							XRL.World.ObjectBuilders.Roboticized.Roboticize(obj, "{{c|mechanical}}", "There is a low, persistent hum emanating outward.");
						}
					}
				}
			}
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x0028E0E8 File Offset: 0x0028C2E8
		public int clamp(int n, int low, int high)
		{
			if (n < low)
			{
				return low;
			}
			if (n > high)
			{
				return high;
			}
			return n;
		}

		// Token: 0x060070C2 RID: 28866 RVA: 0x0028E0F8 File Offset: 0x0028C2F8
		public void partition(Rect2D rect, ref int nSegments, List<ISultanDungeonSegment> segments)
		{
			if (nSegments <= 0)
			{
				segments.Add(new SultanRectDungeonSegment(rect));
				return;
			}
			if (Stat.Random(0, 1) == 0)
			{
				int num = rect.y2 - rect.y1;
				int num2 = (rect.y2 - rect.y1) / 2;
				int num3 = (rect.y2 - rect.y1) / 8;
				int num4 = this.clamp((int)Stat.GaussianRandom(0f, (float)num3) + num2, rect.y1 + 4, rect.y2 - 4);
				if (num <= 8)
				{
					segments.Add(new SultanRectDungeonSegment(rect));
					return;
				}
				nSegments--;
				Rect2D rect2D = new Rect2D(rect.x1, rect.y1, rect.x2, num4);
				this.partition(rect2D, ref nSegments, segments);
				nSegments--;
				Rect2D rect2D2 = new Rect2D(rect.x1, num4 + 1, rect.x2, rect.y2);
				this.partition(rect2D2, ref nSegments, segments);
				return;
			}
			else
			{
				int num5 = rect.x2 - rect.x1;
				int num6 = (rect.x2 - rect.x1) / 2;
				int num7 = (rect.x2 - rect.x1) / 8;
				int num8 = this.clamp((int)Stat.GaussianRandom(0f, (float)num7) + num6, rect.x1 + 4, rect.x2 - 4);
				if (num5 <= 16)
				{
					segments.Add(new SultanRectDungeonSegment(rect));
					return;
				}
				nSegments--;
				Rect2D rect2D3 = new Rect2D(rect.x1, rect.y1, num8, rect.y2);
				this.partition(rect2D3, ref nSegments, segments);
				nSegments--;
				Rect2D rect2D4 = new Rect2D(num8 + 1, rect.y1, rect.x2, rect.y2);
				this.partition(rect2D4, ref nSegments, segments);
				return;
			}
		}

		// Token: 0x060070C3 RID: 28867 RVA: 0x0028E2A0 File Offset: 0x0028C4A0
		public string ResolveOneObjectFromArgs(List<string> arg, string def = null, int tier = 1)
		{
			if (arg == null || arg.Count == 0)
			{
				return def;
			}
			string text = arg[Stat.Random(0, arg.Count - 1)];
			if (text.Length == 0)
			{
				return def;
			}
			if (text[0] == '$')
			{
				return PopulationManager.RollOneFrom(text.Substring(1), new Dictionary<string, string> { 
				{
					"zonetier",
					tier.ToString()
				} }, "SmallBoulder").Blueprint;
			}
			return text;
		}

		// Token: 0x060070C4 RID: 28868 RVA: 0x0028E313 File Offset: 0x0028C513
		public List<PopulationResult> ResolveOnePopulationFromArgs(List<string> arg, string def = null, int Tier = 1)
		{
			if (arg == null || arg.Count == 0)
			{
				return PopulationManager.Generate(def, null, null, null, null, null);
			}
			return PopulationManager.Generate(arg.GetRandomElement<string>(null), "zonetier", Tier.ToString(), null, null, null);
		}

		// Token: 0x060070C5 RID: 28869 RVA: 0x0028E347 File Offset: 0x0028C547
		public string ResolveOneTableFromArgs(List<string> arg, string def = null)
		{
			if (arg == null || arg.Count == 0)
			{
				return def;
			}
			return arg.GetRandomElement<string>(null);
		}

		// Token: 0x060070C6 RID: 28870 RVA: 0x0028E35D File Offset: 0x0028C55D
		public void LogMem(string step)
		{
		}

		// Token: 0x060070C7 RID: 28871 RVA: 0x0028E360 File Offset: 0x0028C560
		public bool PointInSegments(List<ISultanDungeonSegment> segments, int x, int y)
		{
			for (int i = 0; i < segments.Count; i++)
			{
				if (segments[i].contains(Location2D.Get(x, y)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060070C8 RID: 28872 RVA: 0x0028E398 File Offset: 0x0028C598
		public void FabricateIslandPond(PopulationLayout layout, string liquidBlueprint)
		{
			List<Location2D> list = layout.region.reducyBy(1);
			list.RemoveAll(new Predicate<Location2D>(this.onEdge));
			foreach (Location2D location2D in list)
			{
				this.zone.GetCell(location2D).ClearWalls();
				this.zone.GetCell(location2D).AddObject(liquidBlueprint, null, null, null, null);
			}
			foreach (Location2D location2D2 in layout.region.reducyBy(4))
			{
				this.zone.GetCell(location2D2).Clear(null, false, false, null);
			}
		}

		// Token: 0x060070C9 RID: 28873 RVA: 0x0028E47C File Offset: 0x0028C67C
		public void FabricateAerie(PopulationLayout layout)
		{
			this.zone.CacheZoneConnection("U", layout.position, "aerie", null);
			this.zone.GetCell(layout.position.FromDirection("NW")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position.FromDirection("NE")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position.FromDirection("SW")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position.FromDirection("SE")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position).AddObject("StairsUp", null, null, null, null).SetIntProperty("IdleStairs", 1, false);
		}

		// Token: 0x060070CA RID: 28874 RVA: 0x0028E5A9 File Offset: 0x0028C7A9
		public bool onEdge(Location2D location)
		{
			return location.X <= 1 || location.X >= 79 || (location.Y <= 1 || location.Y >= 24);
		}

		// Token: 0x060070CB RID: 28875 RVA: 0x0028E5D8 File Offset: 0x0028C7D8
		public void FabricateBurrow(PopulationLayout layout)
		{
			this.zone.CacheZoneConnection("D", layout.position, "burrow", null);
			List<Location2D> border = layout.region.getBorder(2);
			border.RemoveAll(new Predicate<Location2D>(this.onEdge));
			foreach (Location2D location2D in border)
			{
				this.zone.GetCell(location2D).AddObject("Mudroot", null, null, null, null);
			}
			this.zone.GetCell(layout.position).AddObject("StairsDown", null, null, null, null).SetIntProperty("IdleStairs", 1, false);
		}

		// Token: 0x060070CC RID: 28876 RVA: 0x0028E6A0 File Offset: 0x0028C8A0
		public void FabricatePond(PopulationLayout layout, string liquidBlueprint)
		{
			List<Location2D> list = layout.region.reducyBy(1);
			list.RemoveAll(new Predicate<Location2D>(this.onEdge));
			foreach (Location2D location2D in list)
			{
				this.zone.GetCell(location2D).ClearWalls();
				this.zone.GetCell(location2D).AddObject(liquidBlueprint, null, null, null, null);
			}
		}

		// Token: 0x060070CD RID: 28877 RVA: 0x0028E730 File Offset: 0x0028C930
		public void FabricateWalledPond(PopulationLayout layout, string liquidBlueprint)
		{
			List<Location2D> border = layout.region.getBorder(2);
			border.RemoveAll(new Predicate<Location2D>(this.onEdge));
			List<Location2D> list = layout.region.reducyBy(3);
			list.RemoveAll(new Predicate<Location2D>(this.onEdge));
			foreach (Location2D location2D in border)
			{
				this.zone.GetCell(location2D).AddObject("Mudroot", null, null, null, null);
			}
			foreach (Location2D location2D2 in list)
			{
				this.zone.GetCell(location2D2).ClearWalls();
				this.zone.GetCell(location2D2).AddObject(liquidBlueprint, null, null, null, null);
			}
		}

		// Token: 0x060070CE RID: 28878 RVA: 0x0028E830 File Offset: 0x0028CA30
		public void FabricateWalledIslandPond(PopulationLayout layout, string liquidBlueprint)
		{
			layout.region.getBorder(2).RemoveAll(new Predicate<Location2D>(this.onEdge));
			List<Location2D> list = layout.region.reducyBy(3);
			list.RemoveAll(new Predicate<Location2D>(this.onEdge));
			foreach (Location2D location2D in list)
			{
				this.zone.GetCell(location2D).AddObject("Mudroot", null, null, null, null);
			}
			foreach (Location2D location2D2 in list)
			{
				this.zone.GetCell(location2D2).ClearWalls();
				this.zone.GetCell(location2D2).AddObject(liquidBlueprint, null, null, null, null);
			}
			foreach (Location2D location2D3 in layout.region.reducyBy(5))
			{
				this.zone.GetCell(location2D3).Clear(null, false, false, null);
			}
		}

		// Token: 0x060070CF RID: 28879 RVA: 0x0028E988 File Offset: 0x0028CB88
		public Rect2D BackFromEdge(Rect2D Rect)
		{
			if (Rect.x1 == 0)
			{
				Rect = new Rect2D(1, Rect.y1, Rect.x2, Rect.y2);
			}
			if (Rect.x2 == this.zone.Width - 1)
			{
				Rect = new Rect2D(Rect.x1, Rect.y1, this.zone.Width - 2, Rect.y2);
			}
			if (Rect.y1 == 0)
			{
				Rect = new Rect2D(Rect.x1, 1, Rect.x2, Rect.y2);
			}
			if (Rect.y2 == this.zone.Height - 1)
			{
				Rect = new Rect2D(Rect.x1, Rect.y1, Rect.x2, this.zone.Height - 2);
			}
			return Rect;
		}

		// Token: 0x060070D0 RID: 28880 RVA: 0x0028EA50 File Offset: 0x0028CC50
		public void FabricateHistoricHall(InfluenceMapRegion region)
		{
			this.townSquareRect = this.BackFromEdge(GridTools.MaxRectByArea(region.GetGrid(), false, 0, 0).Translate(region.BoundingBox.UpperLeft));
			this.townSquareDoorStyle = ((Stat.Random(0, 1) == 0) ? Stat.Random(1, 3) : Stat.Random(1, 7));
			int num = (this.townSquareRect.x1 + this.townSquareRect.x2) / 2;
			bool flag = (this.townSquareRect.x2 - this.townSquareRect.x1 + 1) % 2 == 1;
			int num2 = (this.townSquareRect.y1 + this.townSquareRect.y2) / 2;
			bool flag2 = (this.townSquareRect.y2 - this.townSquareRect.y1 + 1) % 2 == 1;
			for (int i = this.townSquareRect.x1; i <= this.townSquareRect.x2; i++)
			{
				Cell cell = this.zone.GetCell(i, this.townSquareRect.y1);
				Cell cell2 = this.zone.GetCell(i, this.townSquareRect.y2);
				cell.Clear(null, false, false, null);
				cell2.Clear(null, false, false, null);
				bool flag3 = i == num || (!flag && i == num + 1);
				if (flag3 && (this.townSquareDoorStyle == 1 || this.townSquareDoorStyle == 2 || this.townSquareDoorStyle == 4))
				{
					cell.AddObject(flag ? this.getAVillageDoor() : ((i == num) ? GameObject.Create("Double " + this.villageDoorStyle + " W") : GameObject.Create("Double " + this.villageDoorStyle + " E")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag || i == num)
					{
						this.tryPlace(i - 1, this.townSquareRect.y1 - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag || i == num + 1)
					{
						this.tryPlace(i + 1, this.townSquareRect.y1 - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				if (flag3 && (this.townSquareDoorStyle == 1 || this.townSquareDoorStyle == 2 || this.townSquareDoorStyle == 5))
				{
					cell2.AddObject(flag ? this.getAVillageDoor() : ((i == num) ? GameObject.Create("Double " + this.villageDoorStyle + " W") : GameObject.Create("Double " + this.villageDoorStyle + " E")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag || i == num)
					{
						this.tryPlace(i - 1, this.townSquareRect.y2 + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag || i == num + 1)
					{
						this.tryPlace(i + 1, this.townSquareRect.y2 + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell2.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			for (int j = this.townSquareRect.y1 + 1; j <= this.townSquareRect.y2 - 1; j++)
			{
				Cell cell3 = this.zone.GetCell(this.townSquareRect.x1, j);
				Cell cell4 = this.zone.GetCell(this.townSquareRect.x2, j);
				cell3.Clear(null, false, false, null);
				cell4.Clear(null, false, false, null);
				bool flag4 = j == num2 || (!flag2 && j == num2 + 1);
				if (flag4 && (this.townSquareDoorStyle == 1 || this.townSquareDoorStyle == 3 || this.townSquareDoorStyle == 7))
				{
					cell3.AddObject(flag2 ? this.getAVillageDoor() : ((j == num2) ? GameObject.Create("Double " + this.villageDoorStyle + " N") : GameObject.Create("Double " + this.villageDoorStyle + " S")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag2 || j == num2)
					{
						this.tryPlace(this.townSquareRect.x1 - 1, j - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag2 || j == num2 + 1)
					{
						this.tryPlace(this.townSquareRect.x1 - 1, j + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell3.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				if (flag4 && (this.townSquareDoorStyle == 1 || this.townSquareDoorStyle == 3 || this.townSquareDoorStyle == 6))
				{
					cell4.AddObject(flag2 ? this.getAVillageDoor() : ((j == num2) ? GameObject.Create("Double " + this.villageDoorStyle + " N") : GameObject.Create("Double " + this.villageDoorStyle + " S")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag2 || j == num2)
					{
						this.tryPlace(this.townSquareRect.x2 + 1, j - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag2 || j == num2 + 1)
					{
						this.tryPlace(this.townSquareRect.x2 + 1, j + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell4.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			Rect2D rect2D = this.townSquareRect.ReduceBy(2, 2);
			if (rect2D.Area > 4)
			{
				this.tryPlace(rect2D.x1, rect2D.y1, new Producer<GameObject>(this.getAVillageLightSource));
				this.tryPlace(rect2D.x2, rect2D.y2, new Producer<GameObject>(this.getAVillageLightSource));
				this.tryPlace(rect2D.x1, rect2D.y2, new Producer<GameObject>(this.getAVillageLightSource));
				this.tryPlace(rect2D.x2, rect2D.y1, new Producer<GameObject>(this.getAVillageLightSource));
			}
		}

		// Token: 0x060070D1 RID: 28881 RVA: 0x0028F0EC File Offset: 0x0028D2EC
		public void FabricateLibrary(InfluenceMapRegion region)
		{
			this.townSquareRect = this.BackFromEdge(GridTools.MaxRectByArea(region.GetGrid(), false, 0, 0).Translate(region.BoundingBox.UpperLeft));
			this.townSquareDoorStyle = Stat.Random(1, 4);
			int num = (this.townSquareRect.x1 + this.townSquareRect.x2) / 2;
			bool flag = (this.townSquareRect.x2 - this.townSquareRect.x1 + 1) % 2 == 1;
			int num2 = (this.townSquareRect.y1 + this.townSquareRect.y2) / 2;
			bool flag2 = (this.townSquareRect.y2 - this.townSquareRect.y1 + 1) % 2 == 1;
			for (int i = this.townSquareRect.x1; i <= this.townSquareRect.x2; i++)
			{
				Cell cell = this.zone.GetCell(i, this.townSquareRect.y1);
				Cell cell2 = this.zone.GetCell(i, this.townSquareRect.y2);
				cell.Clear(null, false, false, null);
				cell2.Clear(null, false, false, null);
				bool flag3 = i == num || (!flag && i == num + 1);
				if (flag3 && this.townSquareDoorStyle == 1)
				{
					cell.AddObject(flag ? this.getAVillageDoor() : ((i == num) ? GameObject.Create("Double " + this.villageDoorStyle + " W") : GameObject.Create("Double " + this.villageDoorStyle + " E")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag || i == num)
					{
						this.tryPlace(i - 1, this.townSquareRect.y1 - 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(i - 1, this.townSquareRect.y1 + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag || i == num + 1)
					{
						this.tryPlace(i + 1, this.townSquareRect.y1 - 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(i + 1, this.townSquareRect.y1 + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				if (flag3 && this.townSquareDoorStyle == 2)
				{
					cell2.AddObject(flag ? this.getAVillageDoor() : ((i == num) ? GameObject.Create("Double " + this.villageDoorStyle + " W") : GameObject.Create("Double " + this.villageDoorStyle + " E")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag || i == num)
					{
						this.tryPlace(i - 1, this.townSquareRect.y2 + 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(i - 1, this.townSquareRect.y2 - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag || i == num + 1)
					{
						this.tryPlace(i + 1, this.townSquareRect.y2 + 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(i + 1, this.townSquareRect.y2 - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell2.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			for (int j = this.townSquareRect.y1 + 1; j <= this.townSquareRect.y2 - 1; j++)
			{
				Cell cell3 = this.zone.GetCell(this.townSquareRect.x1, j);
				Cell cell4 = this.zone.GetCell(this.townSquareRect.x2, j);
				cell3.Clear(null, false, false, null);
				cell4.Clear(null, false, false, null);
				bool flag4 = j == num2 || (!flag2 && j == num2 + 1);
				if (flag4 && this.townSquareDoorStyle == 4)
				{
					cell3.AddObject(flag2 ? this.getAVillageDoor() : ((j == num2) ? GameObject.Create("Double " + this.villageDoorStyle + " N") : GameObject.Create("Double " + this.villageDoorStyle + " S")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag2 || j == num2)
					{
						this.tryPlace(this.townSquareRect.x1 - 1, j - 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(this.townSquareRect.x1 + 1, j - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag2 || j == num2 + 1)
					{
						this.tryPlace(this.townSquareRect.x1 - 1, j + 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(this.townSquareRect.x1 + 1, j + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell3.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				if (flag4 && this.townSquareDoorStyle == 3)
				{
					cell4.AddObject(flag2 ? this.getAVillageDoor() : ((j == num2) ? GameObject.Create("Double " + this.villageDoorStyle + " N") : GameObject.Create("Double " + this.villageDoorStyle + " S")), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).SetIntProperty("ReplaceWithWall", 1, false);
					if (flag2 || j == num2)
					{
						this.tryPlace(this.townSquareRect.x2 + 1, j - 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(this.townSquareRect.x2 - 1, j - 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
					if (flag2 || j == num2 + 1)
					{
						this.tryPlace(this.townSquareRect.x2 + 1, j + 1, new Producer<GameObject>(this.getAVillageLightSource));
						this.tryPlace(this.townSquareRect.x2 - 1, j + 1, new Producer<GameObject>(this.getAVillageLightSource));
					}
				}
				else
				{
					cell4.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x060070D2 RID: 28882 RVA: 0x0028F7B0 File Offset: 0x0028D9B0
		public void FabricateGraveyard(InfluenceMapRegion region)
		{
			this.townSquareRect = this.BackFromEdge(GridTools.MaxRectByArea(region.GetGrid(), false, 0, 0).Translate(region.BoundingBox.UpperLeft));
			string text = null;
			if (Stat.Random(1, 100) <= 75)
			{
				string text2 = "?";
				if (region.Center.X <= 40)
				{
					text2 = "E";
				}
				if (region.Center.X >= 40)
				{
					text2 = "W";
				}
				if (this.townSquareRect.y2 > 16)
				{
					text2 = "N";
				}
				if (this.townSquareRect.y1 < 8)
				{
					text2 = "S";
				}
				if (this.townSquareRect.y1 <= 0)
				{
					text2 = "S";
				}
				if (this.townSquareRect.y2 >= 24)
				{
					text2 = "N";
				}
				if (this.townSquareRect.y1 <= 0 && this.townSquareRect.y2 >= 24)
				{
					if (region.Center.X <= 40)
					{
						text2 = "E";
					}
					if (region.Center.X >= 40)
					{
						text2 = "W";
					}
				}
				Point2D randomDoorCell = this.townSquareRect.GetRandomDoorCell(text2, 2);
				ZoneBuilderSandbox.PlaceObjectOnRect(this.zone, "BrinestalkFence", this.townSquareRect, false);
				Cell cell = base.GetCell(this.zone, randomDoorCell);
				if (cell != null)
				{
					cell.Clear(null, false, false, null);
				}
				Cell cell2 = base.GetCell(this.zone, randomDoorCell);
				if (cell2 != null)
				{
					cell2.AddObject("Brinestalk Gate", null, null, null, null);
				}
				text = this.townSquareRect.GetCellSide(randomDoorCell);
			}
			if (Stat.Random(1, 100) <= 75)
			{
				if (text == null)
				{
					text = Directions.GetRandomCardinalDirection();
				}
				Rect2D rect2D = this.townSquareRect.ReduceBy(0, 0);
				int num = 0;
				if (text == "N")
				{
					num = ((Stat.Random(0, 1) == 0) ? 2 : 3);
				}
				if (text == "S")
				{
					num = ((Stat.Random(0, 1) == 0) ? 0 : 1);
				}
				if (text == "E")
				{
					num = ((Stat.Random(0, 1) == 0) ? 0 : 3);
				}
				if (text == "W")
				{
					num = ((Stat.Random(0, 1) == 0) ? 1 : 2);
				}
				if (num == 0 || num == 1)
				{
					rect2D.y2 = rect2D.y1 + 3;
				}
				else
				{
					rect2D.y1 = rect2D.y2 - 3;
				}
				if (num == 0 || num == 3)
				{
					rect2D.x2 = rect2D.x1 + 3;
				}
				else
				{
					rect2D.x1 = rect2D.x2 - 3;
				}
				base.ClearRect(this.zone, rect2D);
				ZoneBuilderSandbox.PlaceObjectOnRect(this.zone, this.getAVillageWall(), rect2D, false);
				Point2D randomDoorCell2 = rect2D.GetRandomDoorCell(text, 1);
				this.burrowedDoors.Add(Location2D.Get(randomDoorCell2.x, randomDoorCell2.y));
				Cell cell3 = this.zone.GetCell(randomDoorCell2);
				if (cell3 != null)
				{
					cell3.Clear(null, false, false, null);
				}
				Cell cell4 = this.zone.GetCell(randomDoorCell2);
				if (cell4 != null)
				{
					cell4.AddObject(this.getAVillageDoor(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			int num2 = Stat.Random(3, 10);
			for (int i = 0; i < num2; i++)
			{
				GameObject gameObject = GameObject.Create("Tombstone", 0, 0, null, delegate(GameObject o)
				{
					Tombstone tombstone;
					if (o.TryGetPart<Tombstone>(out tombstone))
					{
						tombstone.name = NameMaker.MakeName(this.getBaseVillager(false), null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
					}
				}, null, null, null);
				ZoneBuilderSandbox.PlaceObjectInRect(this.zone, this.townSquareRect.ReduceBy(1, 1), gameObject, null);
			}
		}

		// Token: 0x060070D3 RID: 28883 RVA: 0x0028FB14 File Offset: 0x0028DD14
		public void FabricateHut(PopulationLayout layout, bool isRound)
		{
			Location2D position = layout.position;
			Zone zone = layout.zone;
			int num = Math.Max(6, Stat.Random(layout.innerRect.Width - 2, layout.innerRect.Width));
			int num2 = Math.Max(6, Stat.Random(layout.innerRect.Height - 2, layout.innerRect.Height));
			int num3 = position.X - num / 2;
			int num4 = position.X + num / 2;
			int num5 = position.Y - num2 / 2;
			int num6 = position.Y + num2 / 2;
			int num7 = 0;
			int num8 = 24;
			int num9 = 0;
			if (num8 > num5)
			{
			}
			if (num7 < num4)
			{
			}
			if (num9 < num6)
			{
			}
			for (int i = num5; i <= num6; i++)
			{
				for (int j = num3; j <= num4; j++)
				{
					zone.GetCell(j, i).Clear(null, false, false, null).AddObject("DirtPath", null, null, null, null);
				}
			}
			if (isRound)
			{
				for (int k = num3 + 1; k <= num4 - 1; k++)
				{
					zone.GetCell(k, num5).Clear(null, false, false, null);
					zone.GetCell(k, num5).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					zone.GetCell(k, num6).Clear(null, false, false, null);
					zone.GetCell(k, num6).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				for (int l = num5 + 1; l <= num6 - 1; l++)
				{
					zone.GetCell(num3, l).Clear(null, false, false, null);
					zone.GetCell(num3, l).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					zone.GetCell(num4, l).Clear(null, false, false, null);
					zone.GetCell(num4, l).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				zone.GetCell(num3 + 1, num5 + 1).Clear(null, false, false, null);
				zone.GetCell(num3 + 1, num5 + 1).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				zone.GetCell(num4 - 1, num5 + 1).Clear(null, false, false, null);
				zone.GetCell(num4 - 1, num5 + 1).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				zone.GetCell(num3 + 1, num6 - 1).Clear(null, false, false, null);
				zone.GetCell(num3 + 1, num6 - 1).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				zone.GetCell(num4 - 1, num6 - 1).Clear(null, false, false, null);
				zone.GetCell(num4 - 1, num6 - 1).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				return;
			}
			for (int m = num3; m <= num4; m++)
			{
				zone.GetCell(m, num5).Clear(null, false, false, null);
				zone.GetCell(m, num5).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				zone.GetCell(m, num6).Clear(null, false, false, null);
				zone.GetCell(m, num6).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			for (int n = num5; n <= num6; n++)
			{
				zone.GetCell(num3, n).Clear(null, false, false, null);
				zone.GetCell(num3, n).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				zone.GetCell(num4, n).Clear(null, false, false, null);
				zone.GetCell(num4, n).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x060070D4 RID: 28884 RVA: 0x0028FF30 File Offset: 0x0028E130
		public void FabricateTent(PopulationLayout layout)
		{
			Zone zone = layout.zone;
			Rect2D innerRect = layout.innerRect;
			Location2D location = innerRect.Center.location;
			Location2D location2 = innerRect.GetRandomDoorCell(Calc.GetOppositeDirection(location.RegionDirection(80, 25)[0]), 2).location;
			if (innerRect.DoorDirection == "N" || innerRect.DoorDirection == "S")
			{
				innerRect.Pinch = innerRect.Height / 2;
			}
			if (innerRect.DoorDirection == "E" || innerRect.DoorDirection == "W")
			{
				innerRect.Pinch = innerRect.Width / 2;
			}
			ZoneBuilderSandbox.PlaceObjectOnRect(zone, this.getAVillageCanvas(), innerRect, true);
			base.GetCell(zone, location2).Clear(null, false, false, null);
			this.burrowedDoors.Add(base.GetCell(zone, location2).Location);
			layout.position = innerRect.Center.location;
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x0029003C File Offset: 0x0028E23C
		public static void MakeCaveBuilding(Zone Z, InfluenceMap IF, InfluenceMapRegion R, string WallObject = "Shale")
		{
			int num = 4;
			foreach (Location2D location2D in R.Cells)
			{
				if (IF.CostMap[location2D.X, location2D.Y] == num)
				{
					Z.GetCell(location2D.X, location2D.Y).Clear(null, false, false, null);
					Z.GetCell(location2D.X, location2D.Y).AddObject(WallObject, null, null, null, null);
				}
			}
		}

		// Token: 0x060070D6 RID: 28886 RVA: 0x002900E0 File Offset: 0x0028E2E0
		public void SnakeToConnections(Location2D townCenter)
		{
			foreach (CachedZoneConnection cachedZoneConnection in this.zone.ZoneConnectionCache)
			{
				if (cachedZoneConnection.TargetDirection == "-" && !this.zone.IsReachable(cachedZoneConnection.X, cachedZoneConnection.Y))
				{
					using (Pathfinder pathfinder = this.zone.getPathfinder(null))
					{
						if (pathfinder.FindPath(Location2D.Get(cachedZoneConnection.X, cachedZoneConnection.Y), townCenter, false, true, 9999, false))
						{
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								if (this.zone.GetCell(pathfinderNode.X, pathfinderNode.Y).HasWall())
								{
									this.burrowedDoors.Add(Location2D.Get(pathfinderNode.X, pathfinderNode.Y));
									this.zone.GetCell(pathfinderNode.X, pathfinderNode.Y).ClearWalls();
									pathfinder.CurrentNavigationMap[pathfinderNode.X, pathfinderNode.Y] = 0;
									pathfinderNode.weight = 0;
								}
								this.zone.SetReachable(pathfinderNode.X, pathfinderNode.Y, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x002902B0 File Offset: 0x0028E4B0
		public void CarvePathwaysFromLocations(Zone Z, bool bCarveDoors, InfluenceMap map, Location2D townCenter)
		{
			List<Location2D> list = new List<Location2D>();
			if (map != null)
			{
				foreach (Location2D location2D in map.Seeds)
				{
					list.Add(location2D);
				}
			}
			this.CarvePathwaysFromLocations(Z, bCarveDoors, list, townCenter);
		}

		// Token: 0x060070D8 RID: 28888 RVA: 0x00290318 File Offset: 0x0028E518
		public void CarvePathwaysFromLocations(Zone Z, bool bCarveDoors, List<PopulationLayout> buildings, Location2D townCenter)
		{
			List<Location2D> list = new List<Location2D>();
			if (buildings != null)
			{
				foreach (PopulationLayout populationLayout in buildings)
				{
					list.Add(populationLayout.position);
				}
			}
			this.CarvePathwaysFromLocations(Z, bCarveDoors, list, townCenter);
		}

		// Token: 0x060070D9 RID: 28889 RVA: 0x00290380 File Offset: 0x0028E580
		public void CarvePathwaysFromLocations(Zone Z, bool bCarveDoors, List<Location2D> locations, Location2D townCenter)
		{
			int width = Z.Width;
			int height = Z.Height;
			InfluenceMap influenceMap = new InfluenceMap(width, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					influenceMap.Walls[j, i] = (Z.GetCell(j, i).HasWall() ? 1 : 0);
				}
			}
			if (locations != null)
			{
				foreach (Location2D location2D in locations)
				{
					influenceMap.AddSeed(location2D, true);
				}
			}
			influenceMap.SeedAllUnseeded(false, true);
			if (influenceMap.Seeds.Count == 0)
			{
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.SeedAllUnseeded(false, true);
			}
			int num = 0;
			using (Pathfinder pathfinder = this.zone.getPathfinder(null))
			{
				NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 4, 80, 80, 6, 3, -3, 1, new List<NoiseMapNode>());
				for (int k = 0; k < height; k++)
				{
					for (int l = 0; l < width; l++)
					{
						if (influenceMap.Walls[l, k] > 0)
						{
							pathfinder.CurrentNavigationMap[l, k] = 999;
						}
						else
						{
							pathfinder.CurrentNavigationMap[l, k] = noiseMap.Noise[l, k];
						}
					}
				}
				this.LogMem("step5");
				List<int> list = new List<int>();
				list.Add(num);
				for (int m = 0; m < influenceMap.Seeds.Count; m++)
				{
					if (!list.Contains(m))
					{
						int num2 = influenceMap.FindClosestSeedToInList(m, list);
						if (num2 == -1)
						{
							num2 = num;
						}
						Location2D location2D2 = influenceMap.Seeds[num2];
						if (pathfinder.FindPath(influenceMap.Seeds[num2], influenceMap.Seeds[m], false, true, 9999, false))
						{
							int num3 = 0;
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								if (Z.GetCell(pathfinderNode.X, pathfinderNode.Y).HasWall())
								{
									this.burrowedDoors.Add(Location2D.Get(pathfinderNode.X, pathfinderNode.Y));
									Z.GetCell(pathfinderNode.X, pathfinderNode.Y).ClearWalls();
									if (num3 == 0 && bCarveDoors)
									{
										this.burrowedDoors.Add(Location2D.Get(pathfinderNode.X, pathfinderNode.Y));
										Z.GetCell(pathfinderNode.X, pathfinderNode.Y).AddObject(this.getAVillageDoor(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
										num3++;
									}
									pathfinder.CurrentNavigationMap[pathfinderNode.X, pathfinderNode.Y] = 0;
									pathfinderNode.weight = 0;
								}
							}
						}
						list.Add(m);
					}
				}
				for (int n = 0; n < list.Count; n++)
				{
					if (!Z.IsReachable(influenceMap.Seeds[n].X, influenceMap.Seeds[n].Y))
					{
						Z.BuildReachableMap(influenceMap.Seeds[n].X, influenceMap.Seeds[n].Y);
					}
				}
			}
		}

		// Token: 0x060070DA RID: 28890 RVA: 0x002907A4 File Offset: 0x0028E9A4
		public void Clean(Zone Z, int minsize = 9)
		{
			int width = Z.Width;
			int height = Z.Height;
			InfluenceMap influenceMap = new InfluenceMap(width, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					influenceMap.Walls[j, i] = (Z.GetCell(j, i).HasWall() ? 0 : 1);
				}
			}
			influenceMap.SeedAllUnseeded(false, true);
			if (influenceMap.Seeds.Count == 0)
			{
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.SeedAllUnseeded(false, true);
			}
			foreach (InfluenceMapRegion influenceMapRegion in influenceMap.Regions)
			{
				if (influenceMapRegion.Size < minsize)
				{
					foreach (Location2D location2D in influenceMapRegion.Cells)
					{
						Z.GetCell(location2D).ClearWalls();
					}
				}
			}
		}

		// Token: 0x060070DB RID: 28891 RVA: 0x00290910 File Offset: 0x0028EB10
		public virtual void cleanup()
		{
			this.buildings = null;
			this.villageSnapshot = null;
			this.templates = null;
			MemoryHelper.GCCollect();
		}

		// Token: 0x060070DC RID: 28892 RVA: 0x0029092C File Offset: 0x0028EB2C
		public string ResolvePopulationTableName(string tablePrefix)
		{
			if (PopulationManager.HasTable(tablePrefix + "_Faction_" + this.villagerBaseFaction))
			{
				return tablePrefix + "_Faction_" + this.villagerBaseFaction;
			}
			if (PopulationManager.HasTable(tablePrefix + "_" + this.region))
			{
				return tablePrefix + "_" + this.region;
			}
			return tablePrefix + "_*Default";
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x00290998 File Offset: 0x0028EB98
		public GameObject generateVillageOven()
		{
			GameObject gameObject = GameObjectFactory.create("Oven");
			gameObject.SetIntProperty("QuestItem", 1, false);
			Campfire part = gameObject.GetPart<Campfire>();
			part.specificProcgenMeals = new List<CookingRecipe>();
			part.specificProcgenMeals.Add(this.signatureDish);
			return gameObject;
		}

		// Token: 0x060070DE RID: 28894 RVA: 0x002909D4 File Offset: 0x0028EBD4
		public void generateSignatureItems()
		{
			if (this.villageSnapshot.hasProperty("signatureItem"))
			{
				this.signatureItemBlueprint = this.villageSnapshot.GetProperty("signatureItem", "unknown");
				this.signatureItemExample = GameObject.Create(this.signatureItemBlueprint);
			}
			if (this.villageSnapshot.hasProperty("signatureHistoricObjectType"))
			{
				this.signatureHistoricObjectType = this.villageSnapshot.GetProperty("signatureHistoricObjectType", "unknown");
				this.signatureHistoricObjectInstance = GameObject.Create(this.signatureHistoricObjectType);
				this.signatureHistoricObjectInstance.DisplayName = this.villageSnapshot.GetProperty("signatureHistoricObjectName", "unknown");
			}
		}

		// Token: 0x060070DF RID: 28895 RVA: 0x00290A80 File Offset: 0x0028EC80
		public void fenceOff(GameObject go)
		{
			if (go.GetCurrentCell() != null)
			{
				foreach (Cell cell in go.GetCurrentCell().GetLocalAdjacentCells())
				{
					if (!cell.HasWall())
					{
						cell.AddObject("IronFence", null, null, null, null);
					}
				}
			}
		}

		// Token: 0x060070E0 RID: 28896 RVA: 0x00290AF4 File Offset: 0x0028ECF4
		public string getZoneDefaultLiquid(Zone zone)
		{
			string text;
			if (zone == null)
			{
				text = null;
			}
			else
			{
				GameObject terrainObject = zone.GetTerrainObject();
				text = ((terrainObject != null) ? terrainObject.GetTag("VillageDefaultLiquid", "SaltyWaterPuddle") : null);
			}
			return text ?? "SaltyWaterPuddle";
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x00290B24 File Offset: 0x0028ED24
		public void placeNonTakeableSignatureItems()
		{
			if (this.signatureItemBlueprint != null && !this.signatureItemExample.IsTakeable())
			{
				string text = null;
				if (this.signatureItemExample.HasTag("Furniture"))
				{
					text = "Inside";
				}
				int num = this.signatureItemExample.GetTag("VillageSignatureItemNumber", "2-6").RollCached();
				for (int i = 0; i < num; i++)
				{
					base.PlaceObjectInBuilding(GameObject.Create(this.signatureItemBlueprint), this.buildings.GetRandomElement<PopulationLayout>(null), text, null);
				}
			}
		}

		// Token: 0x060070E2 RID: 28898 RVA: 0x00290BA8 File Offset: 0x0028EDA8
		public GameObject getAGenericFarmPlantNearTier()
		{
			int num = 0;
			GameObject gameObject;
			for (;;)
			{
				gameObject = GameObject.Create(PopulationManager.RollOneFrom("DynamicObjectsTable:FarmablePlants", null, null).Blueprint);
				if (num > 10)
				{
					break;
				}
				num++;
				if (!gameObject.HasTag("NoVillageFarming") && gameObject.GetTier() <= this.villageTier + 1)
				{
					return gameObject;
				}
			}
			gameObject = GameObject.Create("Watervine");
			return gameObject;
		}

		// Token: 0x060070E3 RID: 28899 RVA: 0x00290C04 File Offset: 0x0028EE04
		public GameObject getARegionalPlant()
		{
			GameObject gameObject;
			if (this.isVillageZero)
			{
				gameObject = GameObject.Create(PopulationManager.RollOneFrom("DynamicObjectsTable:" + this.region + "_FarmablePlants", null, null).Blueprint);
			}
			else
			{
				int i = 0;
				while (i <= 10)
				{
					if (this.originalPlants.Count > i && i < 3)
					{
						gameObject = this.originalPlants.GetRandomElement<GameObject>(null);
					}
					else
					{
						string blueprint = PopulationManager.RollOneFrom("DynamicObjectsTable:" + this.region + "_Plants", null, null).Blueprint;
						if (blueprint != null)
						{
							gameObject = GameObject.Create(blueprint);
						}
						else
						{
							gameObject = this.getAGenericFarmPlantNearTier();
						}
					}
					i++;
					if (!gameObject.HasTag("NoVillageFarming"))
					{
						return gameObject;
					}
				}
				gameObject = GameObject.Create("Watervine");
			}
			return gameObject;
		}

		// Token: 0x060070E4 RID: 28900 RVA: 0x00290CBE File Offset: 0x0028EEBE
		public GameObject getARandomPlantNearTier()
		{
			return EncountersAPI.GetAnObject((GameObjectBlueprint o) => (o.HasTagOrProperty("Plant") || o.HasTagOrProperty("PlantLike")) && (o.Tier == this.villageTier || o.Tier == this.villageTier - 1 || o.Tier == this.villageTier - 2) && !o.HasTagOrProperty("NoVillageFarming"));
		}

		// Token: 0x060070E5 RID: 28901 RVA: 0x00290CD4 File Offset: 0x0028EED4
		public GameObject getAFarmablePlant()
		{
			GameObject gameObject;
			if (this.isVillageZero)
			{
				gameObject = this.getARegionalPlant();
			}
			else
			{
				int num = Stat.Roll(1, 100);
				string tagOrStringProperty = GameObject.Create("Terrain" + this.region).GetTagOrStringProperty("VillageRegionalFarmPlantChance", null);
				int num2 = ((!string.IsNullOrEmpty(tagOrStringProperty)) ? int.Parse(tagOrStringProperty) : 98);
				if (num <= num2)
				{
					gameObject = this.getARegionalPlant();
				}
				else if (num <= num2 + 2)
				{
					gameObject = this.getARandomPlantNearTier();
				}
				else
				{
					gameObject = this.getAGenericFarmPlantNearTier();
				}
			}
			this.setVillageDomesticatedProperties(gameObject);
			return gameObject;
		}

		// Token: 0x060070E6 RID: 28902 RVA: 0x00290D5C File Offset: 0x0028EF5C
		public GameObject getARegionalCreature(bool NoRep = false)
		{
			if (this.originalCreatures.Count > 0)
			{
				GameObject randomElement = this.originalCreatures.GetRandomElement<GameObject>(null);
				if (!NoRep || EncountersAPI.IsLegendaryEligible(randomElement.GetBlueprint(true)))
				{
					return randomElement;
				}
			}
			return EncountersAPI.GetALegendaryEligibleCreature(null);
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x00290DA0 File Offset: 0x0028EFA0
		public GameObject getAVillageMonument()
		{
			if (this.villageMonumentPrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_MonumentType"), null, null).Blueprint;
				this.villageMonumentPrototype = GameObject.Create(blueprint);
				LiquidVolume liquidVolume = this.villageMonumentPrototype.LiquidVolume;
				if (liquidVolume != null)
				{
					liquidVolume.InitialLiquid = this.signatureLiquid + "-1000";
				}
				this.villageMonumentPrototype.RequirePart<Interesting>(false);
			}
			return this.villageMonumentPrototype.DeepCopy(false, false, null);
		}

		// Token: 0x060070E8 RID: 28904 RVA: 0x00290E1C File Offset: 0x0028F01C
		public GameObject getAVillageLightSource()
		{
			if (this.villageLightSourcePrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_LightSourceType"), null, null).Blueprint;
				this.villageLightSourcePrototype = GameObject.Create(blueprint);
			}
			return this.villageLightSourcePrototype.DeepCopy(false, false, null);
		}

		// Token: 0x060070E9 RID: 28905 RVA: 0x00290E64 File Offset: 0x0028F064
		public GameObject getAVillageBook()
		{
			if (this.villageBookPrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_BookType"), null, null).Blueprint;
				this.villageBookPrototype = GameObject.Create(blueprint);
			}
			return this.villageBookPrototype.DeepCopy(false, false, null);
		}

		// Token: 0x060070EA RID: 28906 RVA: 0x00290EAC File Offset: 0x0028F0AC
		public bool fabricateStoryBuilding()
		{
			if (JournalAPI.GetNotesForVillage(this.villageEntity.id).Count > 0)
			{
				if (this.storyType == "Historic Hall" || this.storyType == "Holograms")
				{
					this.FabricateHistoricHall(this.townSquare);
					return true;
				}
				if (this.storyType == "Library")
				{
					this.FabricateLibrary(this.townSquare);
					return true;
				}
				if (this.storyType == "Graveyard")
				{
					this.FabricateGraveyard(this.townSquare);
					return true;
				}
			}
			return true;
		}

		// Token: 0x060070EB RID: 28907 RVA: 0x00290F44 File Offset: 0x0028F144
		protected HistoricPerspective storyPerspective(JournalVillageNote story)
		{
			return this.villageSnapshot.requirePerspective(this.villageEntity.history.GetEvent(story.EventID), Stat.Random(300, 1000));
		}

		// Token: 0x060070EC RID: 28908 RVA: 0x00290F7C File Offset: 0x0028F17C
		public void clearDegenerateDoors()
		{
			try
			{
				bool flag;
				do
				{
					flag = false;
					int i = 1;
					int num = this.zone.Height - 1;
					while (i < num)
					{
						int j = 1;
						int num2 = this.zone.Width - 1;
						while (j < num2)
						{
							Cell cell = this.zone.GetCell(j, i);
							GameObject firstObjectWithTag = cell.GetFirstObjectWithTag("Door");
							if (firstObjectWithTag != null && cell != null)
							{
								bool sync = false;
								bool foundSync = false;
								if (firstObjectWithTag.HasPart<Door>())
								{
									sync = firstObjectWithTag.GetPart<Door>().SyncAdjacent;
								}
								Predicate<Cell> predicate = delegate(Cell AC)
								{
									if (AC == null)
									{
										return false;
									}
									if (AC.HasWall())
									{
										return true;
									}
									bool result = false;
									if (sync)
									{
										AC.ForeachObjectWithPart("Door", delegate(GameObject obj)
										{
											if (obj.GetPart<Door>().SyncAdjacent)
											{
												result = true;
												foundSync = true;
												return false;
											}
											return true;
										});
									}
									return result;
								};
								int num3 = 0;
								int num4 = 0;
								if (predicate(this.zone.GetCell(j - 1, i)))
								{
									num4++;
								}
								if (predicate(this.zone.GetCell(j + 1, i)))
								{
									num4++;
								}
								if (predicate(this.zone.GetCell(j, i - 1)))
								{
									num3++;
								}
								if (predicate(this.zone.GetCell(j, i + 1)))
								{
									num3++;
								}
								if (num3 + num4 != 2 || num3 == 1 || num4 == 1 || (sync && !foundSync))
								{
									bool flag2 = cell.HasObjectWithIntProperty("ReplaceWithWall");
									cell.Clear(null, false, false, null);
									flag = true;
									if (flag2)
									{
										cell.AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
									}
								}
							}
							j++;
						}
						i++;
					}
				}
				while (flag);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("clearDegenerateDoors", ex, "game_exception");
			}
		}

		// Token: 0x060070ED RID: 28909 RVA: 0x0029114C File Offset: 0x0028F34C
		public Cell checkForOpenWall(GameObject go, string deltaUp, string deltaLeft, string deltaRight)
		{
			Cell cellFromDirection = go.GetCurrentCell().GetCellFromDirection(deltaUp, true);
			if (cellFromDirection == null)
			{
				return null;
			}
			if (!cellFromDirection.HasWall())
			{
				return null;
			}
			Cell cellFromDirection2 = cellFromDirection.GetCellFromDirection(deltaLeft, true);
			if (cellFromDirection2 == null)
			{
				return null;
			}
			if (cellFromDirection2.HasWall())
			{
				return null;
			}
			Cell cellFromDirection3 = cellFromDirection.GetCellFromDirection(deltaRight, true);
			if (cellFromDirection3 == null)
			{
				return null;
			}
			if (cellFromDirection3.HasWall())
			{
				return null;
			}
			return cellFromDirection;
		}

		// Token: 0x060070EE RID: 28910 RVA: 0x002911A8 File Offset: 0x0028F3A8
		public void applyDoorFilters()
		{
			string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_DoorFilter"), null, null).Blueprint;
			foreach (string text in blueprint.Split(',', StringSplitOptions.None))
			{
				if (blueprint == "Pair")
				{
					List<Cell> list = new List<Cell>();
					foreach (GameObject gameObject in this.zone.GetObjectsWithTag("Door"))
					{
						if (!gameObject.HasPart<Door>() || !gameObject.GetPart<Door>().SyncAdjacent)
						{
							string text2 = "N";
							string text3 = "S";
							string text4 = "W";
							string text5 = "E";
							for (int j = 0; j <= 1; j++)
							{
								list.Clear();
								if (j == 1)
								{
									text2 = "E";
									text3 = "W";
									text4 = "N";
									text5 = "S";
								}
								list.AddIfNotNull(this.checkForOpenWall(gameObject, text2, text4, text5));
								list.AddIfNotNull(this.checkForOpenWall(gameObject, text3, text4, text5));
								if (list.Count > 0)
								{
									list.GetRandomElement<Cell>(null).Clear(null, false, false, null).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
						}
					}
				}
				if (blueprint == "Divided")
				{
					List<Cell> list2 = new List<Cell>();
					foreach (GameObject gameObject2 in this.zone.GetObjectsWithTag("Door"))
					{
						if (!gameObject2.HasPart<Door>() || !gameObject2.GetPart<Door>().SyncAdjacent)
						{
							string text6 = "N";
							string text7 = "S";
							string text8 = "W";
							string text9 = "E";
							for (int k = 0; k <= 1; k++)
							{
								list2.Clear();
								if (k == 1)
								{
									text6 = "E";
									text7 = "W";
									text8 = "N";
									text9 = "S";
								}
								if (this.checkForOpenWall(gameObject2, text6, text8, text9) != null && this.checkForOpenWall(gameObject2.GetCurrentCell().GetCellFromDirection(text6, true).Objects[0], text6, text8, text9) != null && this.checkForOpenWall(gameObject2, text7, text8, text9) != null && this.checkForOpenWall(gameObject2.GetCurrentCell().GetCellFromDirection(text7, true).Objects[0], text6, text8, text9) != null)
								{
									gameObject2.GetCurrentCell().GetCellFromDirection(text6, true).Clear(null, false, false, null)
										.AddObject(gameObject2.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
									gameObject2.GetCurrentCell().GetCellFromDirection(text7, true).Clear(null, false, false, null)
										.AddObject(gameObject2.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
									gameObject2.GetCurrentCell().Clear(null, false, false, null).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x00291530 File Offset: 0x0028F730
		protected bool tryPlace(int x, int y, List<GameObject> objs)
		{
			if (objs.Count == 0)
			{
				return false;
			}
			Cell cell = this.zone.GetCell(x, y);
			if (cell != null && cell.IsEmptyForPopulation())
			{
				cell.AddObject(objs[0], false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				objs.RemoveAt(0);
				return true;
			}
			return false;
		}

		// Token: 0x060070F0 RID: 28912 RVA: 0x00291588 File Offset: 0x0028F788
		protected bool tryPlace(int x, int y, string Blueprint)
		{
			Cell cell = this.zone.GetCell(x, y);
			if (cell != null && cell.IsEmptyForPopulation())
			{
				cell.AddObject(Blueprint, null, null, null, null);
				return true;
			}
			return false;
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x002915C0 File Offset: 0x0028F7C0
		protected bool tryPlace(int x, int y, Producer<GameObject> generator)
		{
			Cell cell = this.zone.GetCell(x, y);
			if (cell != null && cell.IsEmptyForPopulation())
			{
				GameObject gameObject = generator();
				if (gameObject != null)
				{
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060070F2 RID: 28914 RVA: 0x0029160C File Offset: 0x0028F80C
		protected int placeInRectSymmetricallyIfPossible(Rect2D rect, List<GameObject> objs, bool Walled, PopulationLayout backupLayout = null, string backupLayoutHint = null)
		{
			int num = objs.Count;
			if (objs.Count > 0)
			{
				int num2 = (rect.x1 + rect.x2) / 2;
				bool flag = (rect.x2 - rect.x1 + 1) % 2 == 1;
				int num3 = (rect.y1 + rect.y2) / 2;
				bool flag2 = (rect.y2 - rect.y1 + 1) % 2 == 1;
				if (objs.Count == 1)
				{
					this.tryPlace(num2, num3, objs);
					if (objs.Count > 0)
					{
						this.tryPlace(num2 + 1, num3, objs);
					}
					if (objs.Count > 0)
					{
						this.tryPlace(num2, num3 + 1, objs);
					}
					if (objs.Count > 0)
					{
						this.tryPlace(num2 + 1, num3 + 1, objs);
					}
				}
				else if (objs.Count == 2)
				{
					switch (Stat.Random(0, 3))
					{
					case 0:
						this.tryPlace((flag || !Walled) ? (num2 - 1) : num2, num3, objs);
						this.tryPlace(num2 + 1, num3, objs);
						break;
					case 1:
						this.tryPlace(num2, (flag2 || !Walled) ? (num3 - 1) : num3, objs);
						this.tryPlace(num2, num3 + 1, objs);
						break;
					case 2:
						this.tryPlace((flag || !Walled) ? (num2 - 1) : num2, (flag2 || !Walled) ? (num3 - 1) : num3, objs);
						this.tryPlace(num2 + 1, num3 + 1, objs);
						break;
					case 3:
						this.tryPlace((flag || !Walled) ? (num2 - 1) : num2, num3 + 1, objs);
						this.tryPlace(num2 + 1, (flag2 || !Walled) ? (num3 - 1) : num3, objs);
						break;
					}
					this.tryPlace(num2, num3, objs);
				}
				else if (objs.Count == 3)
				{
					if (Stat.Random(0, 1) == 0)
					{
						this.tryPlace(num2 - 1, num3 - 1, objs);
						this.tryPlace(num2, num3, objs);
						this.tryPlace(num2 + 1, num3 + 1, objs);
					}
					else
					{
						this.tryPlace(num2 + 1, num3 - 1, objs);
						this.tryPlace(num2, num3, objs);
						this.tryPlace(num2 - 1, num3 + 1, objs);
					}
				}
				else if (objs.Count == 5)
				{
					if (Stat.Random(0, 1) == 0)
					{
						this.tryPlace(num2, num3, objs);
						this.tryPlace(num2 + 1, num3 + 1, objs);
						this.tryPlace(num2 - 1, num3 - 1, objs);
						this.tryPlace(num2 - 1, num3 + 1, objs);
						this.tryPlace(num2 + 1, num3 - 1, objs);
					}
					else if (Stat.Random(0, 1) == 0)
					{
						this.tryPlace(num2 - 2, num3 - 2, objs);
						this.tryPlace(num2 - 1, num3 - 1, objs);
						this.tryPlace(num2, num3, objs);
						this.tryPlace(num2 + 1, num3 + 1, objs);
						this.tryPlace(num2 + 2, num3 + 2, objs);
					}
					else
					{
						this.tryPlace(num2 + 2, num3 - 2, objs);
						this.tryPlace(num2 + 1, num3 - 1, objs);
						this.tryPlace(num2, num3, objs);
						this.tryPlace(num2 - 1, num3 + 1, objs);
						this.tryPlace(num2 - 2, num3 + 2, objs);
					}
				}
				else if (Stat.Random(0, 1) == 0)
				{
					this.tryPlace(num2, num3, objs);
					if ((!flag && !flag2) || !Walled)
					{
						this.tryPlace(num2 + 1, num3 + 1, objs);
					}
					if (!flag || !Walled)
					{
						this.tryPlace(num2 + 1, num3, objs);
					}
					if (!flag2 || !Walled)
					{
						this.tryPlace(num2, num3 + 1, objs);
					}
					if (objs.Count > 0)
					{
						Rect2D rect2D = rect.ReduceBy(1, 1);
						if (rect2D.Area > 4)
						{
							this.tryPlace(rect2D.x1, rect2D.y1, objs);
							this.tryPlace(rect2D.x2, rect2D.y2, objs);
							this.tryPlace(rect2D.x1, rect2D.y2, objs);
							this.tryPlace(rect2D.x2, rect2D.y1, objs);
						}
					}
				}
				else
				{
					Rect2D rect2D2 = rect.ReduceBy(1, 1);
					if (rect2D2.Area > 4)
					{
						this.tryPlace(rect2D2.x1, rect2D2.y1, objs);
						this.tryPlace(rect2D2.x2, rect2D2.y2, objs);
						this.tryPlace(rect2D2.x1, rect2D2.y2, objs);
						this.tryPlace(rect2D2.x2, rect2D2.y1, objs);
					}
					this.tryPlace(num2, num3, objs);
					if ((!flag && !flag2) || !Walled)
					{
						this.tryPlace(num2 + 1, num3 + 1, objs);
					}
					if (!flag || !Walled)
					{
						this.tryPlace(num2 + 1, num3, objs);
					}
					if (!flag2 || !Walled)
					{
						this.tryPlace(num2, num3 + 1, objs);
					}
				}
				foreach (GameObject gameObject in objs)
				{
					if (!ZoneBuilderSandbox.PlaceObjectInRect(this.zone, rect, gameObject, null) && (backupLayout == null || !base.PlaceObjectInBuilding(gameObject, backupLayout, backupLayoutHint, null)))
					{
						MetricsManager.LogWarning("failed to place " + gameObject.DebugName);
						num--;
					}
				}
			}
			return num;
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x00291B1C File Offset: 0x0028FD1C
		public void placeStatues()
		{
			List<GameObject> list = new List<GameObject>();
			if (this.villageSnapshot.hasProperty("worships_creature"))
			{
				GameObject cachedObjects = The.ZoneManager.GetCachedObjects(this.villageSnapshot.GetProperty("worships_creature_id", "unknown"));
				int num = Stat.Random(1, 3);
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = GameObject.Create(this.RollOneFrom("Village_RandomBaseStatue"));
					gameObject.GetPart<RandomStatue>().SetCreature(cachedObjects.DeepCopy(false, true, null));
					list.Add(gameObject);
				}
			}
			if (this.villageSnapshot.hasProperty("despises_creature"))
			{
				GameObject cachedObjects2 = The.ZoneManager.GetCachedObjects(this.villageSnapshot.GetProperty("despises_creature_id", "unknown"));
				int num2 = Stat.Random(1, 3);
				for (int j = 0; j < num2; j++)
				{
					GameObject gameObject2 = GameObject.Create(this.RollOneFrom("Village_RandomBaseStatue"));
					gameObject2.GetPart<RandomStatue>().SetCreature(cachedObjects2.DeepCopy(false, true, null));
					gameObject2.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
					list.Add(gameObject2);
				}
			}
			if (this.villageSnapshot.hasProperty("worships_faction"))
			{
				int num3 = Stat.Random(1, 3);
				for (int k = 0; k < num3; k++)
				{
					GameObject gameObject3 = EncountersAPI.GetALegendaryEligibleCreatureFromFaction(this.villageSnapshot.GetProperty("worships_faction", "unknown"), null) ?? EncountersAPI.GetACreatureFromFaction(this.villageSnapshot.GetProperty("worships_faction", "unknown"), null);
					if (gameObject3 != null)
					{
						GameObject gameObject4 = GameObject.Create(this.RollOneFrom("Village_RandomBaseStatue"));
						gameObject4.GetPart<RandomStatue>().SetCreature(gameObject3.DeepCopy(false, true, null));
						list.Add(gameObject4);
					}
				}
			}
			if (this.villageSnapshot.hasProperty("despises_faction"))
			{
				int num4 = Stat.Random(1, 3);
				for (int l = 0; l < num4; l++)
				{
					GameObject gameObject5 = EncountersAPI.GetALegendaryEligibleCreatureFromFaction(this.villageSnapshot.GetProperty("despises_faction", "unknown"), null) ?? EncountersAPI.GetACreatureFromFaction(this.villageSnapshot.GetProperty("despises_faction", "unknown"), null);
					if (gameObject5 != null)
					{
						GameObject gameObject6 = GameObject.Create(this.RollOneFrom("Village_RandomBaseStatue"));
						gameObject6.GetPart<RandomStatue>().SetCreature(gameObject5.DeepCopy(false, true, null));
						gameObject6.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
						list.Add(gameObject6);
					}
				}
			}
			if (this.villageSnapshot.hasProperty("worships_sultan"))
			{
				int num5 = Stat.Random(1, 3);
				for (int m = 0; m < num5; m++)
				{
					GameObject gameObject7 = GameObject.Create("SultanShrine");
					gameObject7.SetStringProperty("ForceSultan", this.villageSnapshot.GetProperty("worships_sultan_id", "unknown"), false);
					list.Add(gameObject7);
				}
			}
			if (this.villageSnapshot.hasProperty("despises_sultan"))
			{
				int num6 = Stat.Random(1, 3);
				for (int n = 0; n < num6; n++)
				{
					GameObject gameObject8 = GameObject.Create("SultanShrine");
					gameObject8.SetStringProperty("ForceSultan", this.villageSnapshot.GetProperty("despises_sultan_id", "unknown"), false);
					gameObject8.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
					list.Add(gameObject8);
				}
			}
			if (list.Count > 0)
			{
				foreach (GameObject gameObject9 in list)
				{
					int num7 = Stat.Random(0, 1);
					if (num7 == 0)
					{
						base.PlaceObjectInBuilding(gameObject9, this.buildings.GetRandomElement<PopulationLayout>(null), null, null);
					}
					else if (num7 == 1)
					{
						ZoneBuilderSandbox.PlaceObject(gameObject9, this.zone, null);
					}
				}
			}
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x00291ED8 File Offset: 0x002900D8
		protected void placeExtraBooks(List<GameObject> bookshelves)
		{
			string text = this.ResolvePopulationTableName("Villages_ExtraBooks");
			if (text.IsNullOrEmpty())
			{
				return;
			}
			foreach (GameObject gameObject in bookshelves)
			{
				foreach (PopulationResult populationResult in PopulationManager.Generate(text, null, null, null, null, null))
				{
					if (!populationResult.Blueprint.IsNullOrEmpty())
					{
						for (int i = 0; i < populationResult.Number; i++)
						{
							GameObject gameObject2;
							if (populationResult.Blueprint.EndsWith(".json"))
							{
								gameObject2 = GameObject.Create("MarkovBook");
								gameObject2.GetPart<MarkovBook>().SetContents(Stat.Random(0, 2147483646), populationResult.Blueprint);
							}
							else
							{
								gameObject2 = GameObject.Create(populationResult.Blueprint);
							}
							gameObject.Inventory.AddObject(gameObject2, null, false, false, true, null);
						}
					}
				}
			}
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x00292008 File Offset: 0x00290208
		public void placeStories()
		{
			try
			{
				List<JournalVillageNote> notesForVillage = JournalAPI.GetNotesForVillage(this.villageEntity.id);
				int num = 75;
				if (notesForVillage.Count > 0)
				{
					string objectMarkingStyle = null;
					HistoricPerspective perspective = null;
					JournalVillageNote story = null;
					int storyPlacements = 0;
					Action<GameObject> action = delegate(GameObject obj)
					{
						if (obj != null && !obj.HasPart<VillageStoryReveal>())
						{
							string text;
							string text2;
							if (objectMarkingStyle == "engraving")
							{
								text = "Y";
								text2 = "C";
							}
							else
							{
								text = perspective.mainColor;
								text2 = perspective.supportColor;
							}
							if (objectMarkingStyle == "tattoo")
							{
								string text3 = string.Concat(new string[]
								{
									"a scene from the history of the village {{M|",
									this.villageName,
									"}}{{C|: ",
									story.Text.Split('|', StringSplitOptions.None)[0],
									"}}"
								});
								if (Tattoos.IsSuccess(Tattoos.ApplyTattoo(obj, true, true, text3, "&" + text, text2)))
								{
									obj.AddPart<VillageStoryReveal>(new VillageStoryReveal(story, objectMarkingStyle), true, false);
								}
							}
							else
							{
								obj.AddPart<VillageStoryReveal>(new VillageStoryReveal(story, objectMarkingStyle), true, false);
								obj.Render.ColorString = "&" + text;
								obj.Render.DetailColor = text2;
								if (!obj.Render.TileColor.IsNullOrEmpty())
								{
									obj.Render.TileColor = obj.Render.ColorString;
								}
							}
							int storyPlacements4 = storyPlacements;
							storyPlacements = storyPlacements4 + 1;
						}
					};
					int num2 = 0;
					bool flag = false;
					for (;;)
					{
						if (this.storyType == "Monuments")
						{
							objectMarkingStyle = "monument";
							List<GameObject> list = new List<GameObject>(notesForVillage.Count);
							foreach (JournalVillageNote journalVillageNote in notesForVillage)
							{
								story = journalVillageNote;
								perspective = this.storyPerspective(story);
								GameObject avillageMonument = this.getAVillageMonument();
								action(avillageMonument);
								list.Add(avillageMonument);
							}
							Rect2D rect2D = this.BackFromEdge(GridTools.MaxRectByArea(this.townSquare.GetGrid(), false, 0, 0).Translate(this.townSquare.BoundingBox.UpperLeft));
							while (rect2D.Area > 36)
							{
								rect2D = rect2D.ReduceBy((rect2D.Width >= rect2D.Height) ? 1 : 0, (rect2D.Height >= rect2D.Width) ? 1 : 0);
							}
							storyPlacements -= notesForVillage.Count;
							storyPlacements += this.placeInRectSymmetricallyIfPossible(rect2D, list, false, this.townSquareLayout, null);
						}
						else
						{
							if (this.storyType == "Historic Hall")
							{
								objectMarkingStyle = "monument";
								List<GameObject> list2 = new List<GameObject>(notesForVillage.Count);
								foreach (JournalVillageNote journalVillageNote2 in notesForVillage)
								{
									story = journalVillageNote2;
									perspective = this.storyPerspective(story);
									GameObject avillageMonument2 = this.getAVillageMonument();
									action(avillageMonument2);
									list2.Add(avillageMonument2);
								}
								storyPlacements -= notesForVillage.Count;
								storyPlacements += this.placeInRectSymmetricallyIfPossible(this.townSquareRect, list2, true, this.townSquareLayout, "Inside");
								using (List<PopulationResult>.Enumerator enumerator2 = PopulationManager.Generate(this.ResolvePopulationTableName("Village_HistoricHallContents"), null, null, null, null, null).GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										PopulationResult populationResult = enumerator2.Current;
										for (int i = 0; i < populationResult.Number; i++)
										{
											GameObject gameObject = GameObject.Create(populationResult.Blueprint);
											if (!ZoneBuilderSandbox.PlaceObjectInRect(this.zone, this.townSquareRect, gameObject, null) && !base.PlaceObjectInBuilding(gameObject, this.townSquareLayout, "Inside", null))
											{
												MetricsManager.LogWarning("failed to place " + gameObject.DebugName);
											}
										}
									}
									goto IL_0FD9;
								}
							}
							if (this.storyType == "Town Crier")
							{
								GameObject gameObject2 = GameObject.Create("Village Crier");
								this.setVillagerProperties(gameObject2);
								this.AddVillagerConversation(gameObject2, gameObject2.GetTag("SimpleConversation", "I know a lot about the town!"), "Live and drink.", null, null, false, true);
								for (int j = 0; j < notesForVillage.Count; j++)
								{
									story = notesForVillage[j];
									perspective = this.storyPerspective(story);
									ConversationsAPI.addSimpleRootInformationOption(gameObject2, string.Concat(new string[]
									{
										"Tell me about {{",
										perspective.mainColor,
										"|",
										story.Text.Snippet(4, 10),
										"}}..."
									}), story.Text.Split('|', StringSplitOptions.None)[0]);
									int num3 = storyPlacements;
									storyPlacements = num3 + 1;
								}
								gameObject2.Inventory.AddObject(this.generatePreacherBook(), null, false, false, true, null);
								if (!ZoneBuilderSandbox.PlaceObject(this.zone, this.townSquare, gameObject2, null, null, false))
								{
									MetricsManager.LogWarning("failed to place " + gameObject2.DebugName);
									storyPlacements -= notesForVillage.Count;
								}
							}
							else if (this.storyType == "Books")
							{
								List<GameObject> list3 = this.generateHistoryBooks();
								List<GameObject> list4 = new List<GameObject>(list3.Count);
								foreach (GameObject gameObject3 in list3)
								{
									if (Stat.Random(0, 9) < list4.Count)
									{
										GameObject gameObject4 = list4.GetRandomElement<GameObject>(null);
										gameObject4.Inventory.AddObject(gameObject3, null, false, false, true, null);
									}
									else
									{
										GameObject gameObject4 = GameObject.Create("Bookshelf");
										if (base.PlaceObjectInBuilding(gameObject4, this.buildings.GetRandomElement<PopulationLayout>(null), "AlongInsideWall", null))
										{
											gameObject4.Inventory.AddObject(gameObject3, null, false, false, true, null);
											list4.Add(gameObject4);
											int num3 = storyPlacements;
											storyPlacements = num3 + 1;
										}
									}
								}
								this.placeExtraBooks(list4);
							}
							else if (this.storyType == "Library")
							{
								List<GameObject> list5 = this.generateHistoryBooks();
								int num4 = (this.townSquareRect.x1 + this.townSquareRect.x2) / 2;
								int num5 = (this.townSquareRect.y1 + this.townSquareRect.y2) / 2;
								int num6;
								int num7;
								bool flag2;
								if (this.townSquareDoorStyle == 1)
								{
									num6 = num4;
									num7 = this.townSquareRect.y2 - 1;
									flag2 = true;
								}
								else if (this.townSquareDoorStyle == 2)
								{
									num6 = num4;
									num7 = this.townSquareRect.y1 + 1;
									flag2 = true;
								}
								else if (this.townSquareDoorStyle == 3)
								{
									num6 = this.townSquareRect.x1 + 1;
									num7 = num5;
									flag2 = false;
								}
								else if (this.townSquareDoorStyle == 4)
								{
									num6 = this.townSquareRect.x2 - 1;
									num7 = num5;
									flag2 = false;
								}
								else
								{
									num6 = num4;
									num7 = num5;
									flag2 = Stat.Random(0, 1) == 0;
								}
								List<GameObject> list6 = new List<GameObject>(list5.Count);
								int num8 = 0;
								foreach (GameObject gameObject5 in list5)
								{
									if (Stat.Random(0, 9) < list6.Count)
									{
										GameObject gameObject6 = list6.GetRandomElement<GameObject>(null);
										gameObject6.Inventory.AddObject(gameObject5, null, false, false, true, null);
									}
									else
									{
										GameObject gameObject6 = GameObject.Create("Bookshelf");
										if (flag2)
										{
											num6 += num8;
										}
										else
										{
											num7 += num8;
										}
										if (num8 > 0)
										{
											num8++;
											num8 = -num8;
										}
										else
										{
											num8 = -num8;
											num8++;
										}
										Cell cell = this.zone.GetCell(num6, num7);
										if (cell.IsEmptyForPopulation() && this.townSquareRect.PointIn(Location2D.Get(num6, num7)))
										{
											cell.AddObject(gameObject6, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
											gameObject6.Inventory.AddObject(gameObject5, null, false, false, true, null);
											list6.Add(gameObject6);
											int num3 = storyPlacements;
											storyPlacements = num3 + 1;
										}
										else if (ZoneBuilderSandbox.PlaceObjectInRect(this.zone, this.townSquareRect, gameObject6, null) || base.PlaceObjectInBuilding(gameObject6, this.townSquareLayout, null, null))
										{
											gameObject6.Inventory.AddObject(gameObject5, null, false, false, true, null);
											list6.Add(gameObject6);
											int num3 = storyPlacements;
											storyPlacements = num3 + 1;
										}
									}
								}
								this.placeExtraBooks(list6);
							}
							else if (this.storyType == "Graveyard")
							{
								objectMarkingStyle = "monument";
								List<GameObject> list7 = new List<GameObject>(notesForVillage.Count);
								foreach (JournalVillageNote journalVillageNote3 in notesForVillage)
								{
									story = journalVillageNote3;
									perspective = this.storyPerspective(story);
									GameObject avillageMonument3 = this.getAVillageMonument();
									action(avillageMonument3);
									list7.Add(avillageMonument3);
								}
								storyPlacements -= notesForVillage.Count;
								storyPlacements += this.placeInRectSymmetricallyIfPossible(this.townSquareRect, list7, false, this.townSquareLayout, "Inside");
							}
							else
							{
								if (this.storyType == "Tattoos" || this.storyType == "Tattoo")
								{
									objectMarkingStyle = "tattoo";
									List<GameObject> list8 = this.zone.GetObjectsWithProperty("ParticipantVillager").ShuffleInPlace<GameObject>(null);
									int num9 = 50;
									using (List<GameObject>.Enumerator enumerator3 = list8.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											GameObject gameObject7 = enumerator3.Current;
											if (num9.in100() && gameObject7.HasProperty("ParticipantVillager"))
											{
												story = notesForVillage.GetRandomElement<JournalVillageNote>(null);
												perspective = this.storyPerspective(story);
												action(gameObject7);
											}
										}
										goto IL_0FD9;
									}
								}
								if (this.storyType == "Animatronic")
								{
									GameObject gameObject8 = GameObject.Create("Village Robot");
									this.setVillagerProperties(gameObject8);
									this.AddVillagerConversation(gameObject8, gameObject8.GetTag("SimpleConversation", "I know a lot about the town!"), "Live and drink.", null, null, false, true);
									for (int k = 0; k < notesForVillage.Count; k++)
									{
										story = notesForVillage[k];
										perspective = this.storyPerspective(story);
										ConversationsAPI.addSimpleRootInformationOption(gameObject8, string.Concat(new string[]
										{
											"Tell me about {{",
											perspective.mainColor,
											"|",
											story.Text.Snippet(4, 10),
											"}}..."
										}), story.Text.Split('|', StringSplitOptions.None)[0]);
										int num3 = storyPlacements;
										storyPlacements = num3 + 1;
									}
									if (!ZoneBuilderSandbox.PlaceObject(this.zone, this.townSquare, gameObject8, null, null, false))
									{
										storyPlacements -= notesForVillage.Count;
										MetricsManager.LogWarning("failed to place " + gameObject8.DebugName);
									}
								}
								else if (this.storyType == "Holograms")
								{
									objectMarkingStyle = "light-pattern";
									List<GameObject> list9 = new List<GameObject>(notesForVillage.Count);
									foreach (JournalVillageNote journalVillageNote4 in notesForVillage)
									{
										story = journalVillageNote4;
										perspective = this.storyPerspective(story);
										GameObject gameObject9 = (If.CoinFlip(null, null) ? GameObject.Create("Village Hologram 1") : GameObject.Create("Village Hologram 2"));
										action(gameObject9);
										list9.Add(gameObject9);
									}
									storyPlacements -= notesForVillage.Count;
									storyPlacements += this.placeInRectSymmetricallyIfPossible(this.townSquareRect, list9, true, this.townSquareLayout, "Inside");
								}
								else if (this.storyType == "Vessels" || this.storyType == "Furniture")
								{
									if (objectMarkingStyle == null)
									{
										objectMarkingStyle = ((Stat.Random(0, 2) == 0) ? "engraving" : "painting");
									}
									Dictionary<int, bool> dictionary = new Dictionary<int, bool>(notesForVillage.Count);
									for (int l = 0; l < notesForVillage.Count; l++)
									{
										dictionary.Add(l, false);
									}
									int num10 = 0;
									foreach (PopulationLayout populationLayout in this.buildings)
									{
										if (num.in100())
										{
											if (this.storyType == "Vessels")
											{
												using (List<Location2D>.Enumerator enumerator5 = populationLayout.originalRegion.Cells.GetEnumerator())
												{
													while (enumerator5.MoveNext())
													{
														Location2D location2D = enumerator5.Current;
														int storyPlacements5 = storyPlacements;
														story = notesForVillage.GetRandomElement<JournalVillageNote>(null);
														perspective = this.storyPerspective(story);
														action(this.zone.GetCell(location2D).GetFirstObjectWithPropertyOrTag("Vessel"));
														if (storyPlacements5 > storyPlacements)
														{
															int num11 = notesForVillage.IndexOf(story);
															if (!dictionary[num11])
															{
																dictionary[num11] = true;
																num10++;
															}
														}
													}
													continue;
												}
											}
											if (this.storyType == "Furniture")
											{
												using (List<Location2D>.Enumerator enumerator5 = populationLayout.originalRegion.Cells.GetEnumerator())
												{
													while (enumerator5.MoveNext())
													{
														Location2D location2D2 = enumerator5.Current;
														int storyPlacements2 = storyPlacements;
														story = notesForVillage.GetRandomElement<JournalVillageNote>(null);
														perspective = this.storyPerspective(story);
														action(this.zone.GetCell(location2D2).GetFirstObjectWithPropertyOrTagAndNotPropertyOrTag("Furniture", "Door"));
														if (storyPlacements2 > storyPlacements)
														{
															int num12 = notesForVillage.IndexOf(story);
															if (!dictionary[num12])
															{
																dictionary[num12] = true;
																num10++;
															}
														}
													}
													continue;
												}
											}
											throw new Exception("internally inconsistent story type " + this.storyType);
										}
									}
									if (num10 < notesForVillage.Count)
									{
										flag = true;
									}
								}
								else
								{
									if (!(this.storyType == "Doors") && !(this.storyType == "Walls"))
									{
										break;
									}
									if (objectMarkingStyle == null)
									{
										objectMarkingStyle = ((Stat.Random(0, 2) == 0) ? "engraving" : "painting");
									}
									int num13 = Stat.Random(0, notesForVillage.Count - 1);
									int num14 = 0;
									foreach (PopulationLayout populationLayout2 in this.buildings)
									{
										if (Stat.Random(1, 100) <= num)
										{
											int storyPlacements3 = storyPlacements;
											story = notesForVillage[num13];
											perspective = this.storyPerspective(story);
											if (this.storyType == "Doors")
											{
												using (List<Location2D>.Enumerator enumerator5 = populationLayout2.originalRegion.Cells.GetEnumerator())
												{
													while (enumerator5.MoveNext())
													{
														Location2D location2D3 = enumerator5.Current;
														action(this.zone.GetCell(location2D3).GetFirstObjectWithTag("Door"));
													}
													goto IL_0F75;
												}
												goto IL_0EFE;
											}
											goto IL_0EFE;
											IL_0F75:
											if (storyPlacements3 <= storyPlacements)
											{
												continue;
											}
											num13++;
											num14++;
											if (num13 >= notesForVillage.Count)
											{
												num13 = 0;
												continue;
											}
											continue;
											IL_0EFE:
											if (this.storyType == "Walls")
											{
												using (List<Location2D>.Enumerator enumerator5 = populationLayout2.originalRegion.Cells.GetEnumerator())
												{
													while (enumerator5.MoveNext())
													{
														Location2D location2D4 = enumerator5.Current;
														action(this.zone.GetCell(location2D4).GetFirstWall());
													}
													goto IL_0F75;
												}
											}
											throw new Exception("internally inconsistent story type " + this.storyType);
										}
									}
									if (num14 < notesForVillage.Count)
									{
										flag = true;
									}
								}
							}
						}
						IL_0FD9:
						if ((storyPlacements != 0 && !flag) || ++num2 >= 5)
						{
							goto IL_0FFA;
						}
						num += 5;
					}
					throw new Exception("unsupported story type " + this.storyType);
					IL_0FFA:;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("placeStories", ex, "game_exception");
			}
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x00293194 File Offset: 0x00291394
		public List<GameObject> generateHistoryBooks()
		{
			List<JournalVillageNote> notesForVillage = JournalAPI.GetNotesForVillage(this.villageEntity.id);
			List<GameObject> list = new List<GameObject>(notesForVillage.Count);
			int num = 0;
			foreach (JournalVillageNote journalVillageNote in notesForVillage)
			{
				num++;
				GameObject avillageBook = this.getAVillageBook();
				string text = "History of " + this.villageName + ", Vol. " + Grammar.GetRomanNumeral(num);
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				HistoricPerspective historicPerspective = this.villageSnapshot.requirePerspective(this.villageEntity.history.GetEvent(journalVillageNote.EventID), null);
				stringBuilder.Append("{{w|This book recounts a tale from the history of the village " + this.villageName + ".}}\n\n");
				stringBuilder.Append(journalVillageNote.Text.Split('|', StringSplitOptions.None)[0]);
				stringBuilder2.Append(journalVillageNote.ID);
				avillageBook.AddPart<VillageHistoryBook>(new VillageHistoryBook(text, stringBuilder.ToString(), stringBuilder2.ToString()), true, false);
				avillageBook.Render.DisplayName = text.ToString();
				avillageBook.Render.ColorString = "&" + historicPerspective.mainColor;
				avillageBook.Render.DetailColor = historicPerspective.supportColor;
				list.Add(avillageBook);
			}
			return list;
		}

		// Token: 0x060070F7 RID: 28919 RVA: 0x00293314 File Offset: 0x00291514
		public GameObject generatePreacherBook()
		{
			List<JournalVillageNote> notesForVillage = JournalAPI.GetNotesForVillage(this.villageEntity.id);
			GameObject gameObject = GameObject.Create("Book");
			string text = "History of " + this.villageName;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder.Append("{{w|This book recounts a tale from the history of the village " + this.villageName + ".}}\n\n");
			foreach (JournalVillageNote journalVillageNote in notesForVillage)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(journalVillageNote.Text);
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Append(",");
				}
				stringBuilder2.Append(journalVillageNote.ID);
			}
			gameObject.AddPart<VillageHistoryBook>(new VillageHistoryBook(text, stringBuilder.ToString(), stringBuilder2.ToString()), true, false);
			gameObject.Render.DisplayName = text.ToString();
			return gameObject;
		}

		// Token: 0x060070F8 RID: 28920 RVA: 0x00293418 File Offset: 0x00291618
		public void generateSignatureDish(string Style = "Focal")
		{
			bool flag = this.isVillageZero;
			int num = 0;
			int num2 = 25;
			int num3 = 50;
			if (this.villageTier >= 0 && this.villageTier <= 1)
			{
				num2 = 25;
				num3 = 85;
			}
			if (this.villageTier >= 2 && this.villageTier <= 3)
			{
				num2 = 15;
				num3 = 65;
			}
			if (this.villageTier >= 4 && this.villageTier <= 5)
			{
				num2 = 5;
				num3 = 52;
			}
			if (this.villageTier >= 6 && this.villageTier <= 7)
			{
				num2 = 0;
				num3 = 35;
			}
			if (this.villageTier >= 8)
			{
				num2 = 0;
				num3 = 25;
			}
			int num4 = Stat.Random(1, 100);
			if (num4 <= num2)
			{
				num = 1;
			}
			else if (num4 <= num3)
			{
				num = 2;
			}
			else
			{
				num = 3;
			}
			List<GameObject> list = new List<GameObject>();
			int num5 = 0;
			if (this.villageSnapshot.listProperties.ContainsKey("signatureDishIngredients"))
			{
				List<string> list2 = this.villageSnapshot.GetList("signatureDishIngredients");
				num = Math.Max(num, list2.Count);
				foreach (string text in list2)
				{
					list.Add(GameObject.Create(text));
				}
			}
			for (int i = list.Count; i < num; i++)
			{
				string ingredientBlueprint = null;
				if (Stat.Random(0, 100) <= 80)
				{
					do
					{
						ingredientBlueprint = PopulationManager.RollOneFrom("DynamicObjectsTable:" + this.region + "_Ingredients", null, null).Blueprint;
						num5++;
					}
					while (list.Any<GameObject>((GameObject o) => o.Blueprint == ingredientBlueprint) && num5 < 25);
					num5 = 0;
					if (ingredientBlueprint == null)
					{
					}
				}
				else
				{
					ingredientBlueprint = CookingRecipe.RollOvenSafeIngredient("Ingredients" + this.villageTier.ToString());
				}
				if (ingredientBlueprint == null)
				{
					ingredientBlueprint = CookingRecipe.RollOvenSafeIngredient("Ingredients" + this.villageTier.ToString());
				}
				if (ingredientBlueprint != null)
				{
					list.Add(GameObjectFactory.Factory.CreateObject(ingredientBlueprint));
				}
			}
			string text2;
			if (GameObjectFactory.Factory.Blueprints.ContainsKey(this.villagerBaseFaction + "_Data"))
			{
				text2 = GameObjectFactory.Factory.Blueprints[this.villagerBaseFaction + "_Data"].GetTag("DishNames", "generic");
			}
			else
			{
				text2 = "generic";
			}
			this.signatureDish = CookingRecipe.FromIngredients(list, null, null, text2);
			if (this.villageSnapshot.GetProperty("signatureDishName", "unknown") != "unknown")
			{
				this.signatureDish.DisplayName = this.villageSnapshot.GetProperty("signatureDishName", "unknown");
			}
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x002936F0 File Offset: 0x002918F0
		public void generateSignatureLiquid()
		{
			if (this.villageSnapshot.hasProperty("signatureLiquid"))
			{
				this.signatureLiquid = this.villageSnapshot.GetProperty("signatureLiquid", "unknown");
				return;
			}
			if (this.villageSnapshot.listProperties.ContainsKey("signatureLiquids"))
			{
				this.signatureLiquid = this.villageSnapshot.GetList("signatureLiquids").GetRandomElement<string>(null);
				return;
			}
			this.signatureLiquid = PopulationManager.RollOneFrom("Villages_LiquidType_" + ImportedFoodorDrink.getLiquidTierGroup(this.villageSnapshot.Tier) + "_*Default", null, null).Blueprint + "-1000";
		}

		// Token: 0x060070FA RID: 28922 RVA: 0x0029379C File Offset: 0x0029199C
		public void generateSignatureSkill()
		{
			if (this.villageSnapshot.hasProperty("signatureSkill"))
			{
				this.signatureSkill = this.villageSnapshot.GetProperty("signatureSkill", "unknown");
			}
			if (this.isVillageZero)
			{
				if (this.region == "Saltmarsh")
				{
					this.signatureSkill = "CookingAndGathering_Harvestry";
				}
				if (this.region == "DesertCanyon")
				{
					this.signatureSkill = "Survival";
				}
				if (this.region == "Saltdunes")
				{
					this.signatureSkill = "Discipline_FastingWay";
				}
				if (this.region == "Hills")
				{
					this.signatureSkill = "CookingAndGathering_Butchery";
				}
			}
		}

		// Token: 0x060070FB RID: 28923 RVA: 0x00293854 File Offset: 0x00291A54
		public void makeSureThereIsEnoughSpace()
		{
			int i;
			for (i = 3; i > 0; i--)
			{
				if (this.zone.GetCells().Count<Cell>((Cell c) => c.HasWall()) <= 1000)
				{
					break;
				}
				NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 4, 80, 80, 6, 3, -3, 1, new List<NoiseMapNode>());
				for (int j = 0; j < 80; j++)
				{
					for (int k = 0; k < 25; k++)
					{
						if (noiseMap.Noise[j, k] > 1)
						{
							this.zone.GetCell(j, k).ClearWalls();
						}
					}
				}
			}
			if (i < 3)
			{
				this.Clean(this.zone, 9);
				this.CarvePathwaysFromLocations(this.zone, false, new List<Location2D>(), Location2D.Get(40, 14));
			}
		}

		// Token: 0x060070FC RID: 28924 RVA: 0x0029392C File Offset: 0x00291B2C
		public virtual void generateStoryType()
		{
			this.storyType = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_StoryEmbodiment"), null, null).Blueprint;
			this.villageEntity.SetEntityPropertyAtCurrentYear("storyType", this.storyType);
		}

		// Token: 0x060070FD RID: 28925 RVA: 0x00293964 File Offset: 0x00291B64
		public virtual void addInitialStructures()
		{
			List<ISultanDungeonSegment> list = new List<ISultanDungeonSegment>();
			int num = 7;
			int num2 = 72;
			int num3 = 7;
			int num4 = 17;
			string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_InitialStructureSegmentation"), null, "Full").Blueprint;
			if (blueprint == "None")
			{
				return;
			}
			foreach (string text in blueprint.Split(';', StringSplitOptions.None))
			{
				if (text == "FullHMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HMirror"
					});
				}
				else if (text == "FullVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "VMirror"
					});
				}
				else if (text == "FullHVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HVMirror"
					});
				}
				else if (text == "Full")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
				}
				else if (text.StartsWith("BSP:"))
				{
					int num5 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
					this.partition(new Rect2D(2, 2, 78, 24), ref num5, list);
				}
				else if (text.StartsWith("Ring:"))
				{
					int num6 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
					if (num6 == 2)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(20, 8, 60, 16)));
					}
					if (num6 == 3)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(15, 8, 65, 16)));
						list.Add(new SultanRectDungeonSegment(new Rect2D(25, 10, 55, 14)));
					}
				}
				else if (text.StartsWith("Blocks"))
				{
					string[] array2 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					int num7 = array2[0].RollCached();
					for (int j = 0; j < num7; j++)
					{
						int num8 = array2[1].RollCached();
						int num9 = array2[2].RollCached();
						int num10 = Stat.Random(2, 78 - num8);
						int num11 = Stat.Random(2, 23 - num9);
						int num12 = num10 + num8;
						int num13 = num11 + num9;
						if (num < num10)
						{
							num = num10;
						}
						if (num2 > num12)
						{
							num2 = num12;
						}
						if (num3 < num11)
						{
							num3 = num11;
						}
						if (num4 > num13)
						{
							num4 = num13;
						}
						SultanRectDungeonSegment sultanRectDungeonSegment = new SultanRectDungeonSegment(new Rect2D(num10, num11, num10 + num8, num11 + num9));
						if (text.Contains("[HMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HMirror";
						}
						if (text.Contains("[VMirror]"))
						{
							sultanRectDungeonSegment.mutator = "VMirror";
						}
						if (text.Contains("[HVMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HVMirror";
						}
						list.Add(sultanRectDungeonSegment);
					}
				}
				else if (text.StartsWith("Circle"))
				{
					string[] array3 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanCircleDungeonSegment(Location2D.Get(array3[0].RollCached(), array3[1].RollCached()), array3[2].RollCached()));
				}
				else if (text.StartsWith("Tower"))
				{
					string[] array4 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanTowerDungeonSegment(Location2D.Get(array4[0].RollCached(), array4[1].RollCached()), array4[2].RollCached(), array4[3].RollCached()));
				}
			}
			ColorOutputMap colorOutputMap = new ColorOutputMap(80, 25);
			for (int k = 0; k < list.Count; k++)
			{
				string text2 = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num14 = 3;
				string text3 = "";
				string blueprint2 = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num15 = 3;
				if (text2.Contains(","))
				{
					string[] array5 = text2.Split(',', StringSplitOptions.None);
					text2 = array5[0];
					text3 = array5[1];
				}
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(text2, num14, list[k].width(), list[k].height(), true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				if (!text3.IsNullOrEmpty())
				{
					waveCollapseFastModel.ClearColors(text3, "border1");
				}
				waveCollapseFastModel.UpdateSample(blueprint2.Split(',', StringSplitOptions.None)[0], num15, true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				ColorOutputMap colorOutputMap2 = new ColorOutputMap(waveCollapseFastModel);
				colorOutputMap2.ReplaceBorders(new Color32(byte.MaxValue, 0, 0, byte.MaxValue), new Color32(0, 0, 0, byte.MaxValue));
				if (list[k].mutator == "HMirror")
				{
					colorOutputMap2.HMirror();
				}
				if (list[k].mutator == "VMirror")
				{
					colorOutputMap2.VMirror();
				}
				if (list[k].mutator == "HVMirror")
				{
					colorOutputMap2.HMirror();
					colorOutputMap2.VMirror();
				}
				colorOutputMap.Paste(colorOutputMap2, list[k].x1, list[k].y1);
				MemoryHelper.GCCollect();
			}
			string text4 = this.RollOneFrom("Village_InitialStructureSegmentationFullscreenMutation");
			int num16 = 0;
			int num17 = 0;
			for (int l = 0; l < list.Count; l++)
			{
				string text5 = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_StructureWall"), null, null).Blueprint;
				if (text5 == "*auto")
				{
					text5 = base.GetDefaultWall(this.zone);
				}
				for (int m = list[l].y1; m < list[l].y2; m++)
				{
					for (int n = list[l].x1; n < list[l].x2; n++)
					{
						if (list[l].contains(n, m))
						{
							for (int num18 = l + 1; num18 < list.Count; num18++)
							{
								if (list[num18].contains(n, m))
								{
									goto IL_0847;
								}
							}
							Color32 color = colorOutputMap.getPixel(n, m);
							if (list[l].HasCustomColor(n, m))
							{
								color = list[l].GetCustomColor(n, m);
							}
							if (WaveCollapseTools.equals(color, ColorOutputMap.BLACK))
							{
								this.zone.GetCell(n + num16, m + num17).ClearWalls();
								this.zone.GetCell(n + num16, m + num17).AddObject(text5, null, null, null, null);
								if (text4 == "VMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
							}
						}
						IL_0847:;
					}
				}
			}
		}

		// Token: 0x060070FE RID: 28926 RVA: 0x00294200 File Offset: 0x00292400
		public GameObject getAVillageDoor()
		{
			if (this.villageDoorPrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_DoorType"), null, null).Blueprint;
				if (blueprint == "Door")
				{
					this.villageDoorPrototype = GameObjectFactory.Factory.CreateObject(this.villageDoorStyle);
				}
				if (blueprint == "Archway")
				{
					this.villageDoorPrototype = GameObjectFactory.Factory.CreateObject("Archway");
				}
			}
			return this.villageDoorPrototype.DeepCopy(false, false, null);
		}

		// Token: 0x060070FF RID: 28927 RVA: 0x00294280 File Offset: 0x00292480
		public void getVillageDoorStyle()
		{
			if (this.villageTechTier < 3)
			{
				this.villageDoorStyle = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_DoorStyle_Lowtier"), null, null).Blueprint;
				return;
			}
			if (this.villageTechTier < 6)
			{
				this.villageDoorStyle = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_DoorStyle_Midtier"), null, null).Blueprint;
				return;
			}
			this.villageDoorStyle = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_DoorStyle_Hightier"), null, null).Blueprint;
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x002942F8 File Offset: 0x002924F8
		public GameObject getAVillageCanvas()
		{
			if (this.villageCanvasPrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_ConstructionAdjectives"), null, null).Blueprint;
				GameObject aregionalCreature = this.getARegionalCreature(false);
				this.villageCanvasPrototype = GameObject.Create("CanvasWall");
				string text = aregionalCreature.GetxTag("TextFragments", "Skin", "skin");
				this.villageCanvasPrototype.DisplayName = aregionalCreature.GetSpecies() + " " + text + " tent";
				this.villageCanvasPrototype.Render.ColorString = aregionalCreature.Render.ColorString;
				this.villageCanvasPrototype.Render.TileColor = aregionalCreature.Render.TileColor;
				this.villageCanvasPrototype.Render.DetailColor = aregionalCreature.Render.DetailColor;
				if (aregionalCreature.HasProperName)
				{
					this.villageCanvasPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("A leather wrought from the peeled and tanned {0} of {1} was hung in a fashion inspired by {2}.", text, aregionalCreature.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false), Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.nouns.!random>", null))));
				}
				else
				{
					this.villageCanvasPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("A leather wrought from the peeled and tanned {0} of {1} was hung in a fashion inspired by {2}.", text, aregionalCreature.a + aregionalCreature.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false), Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.nouns.!random>", null))));
				}
			}
			return this.villageCanvasPrototype.DeepCopy(false, false, null);
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x0029447C File Offset: 0x0029267C
		public GameObject getAVillageWall()
		{
			if (this.villageWallPrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_ConstructionAdjectives"), null, null).Blueprint;
				string blueprint2 = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_VillageWallStyle"), null, null).Blueprint;
				if (blueprint2 == "canvas")
				{
					this.villageWallPrototype = this.getAVillageCanvas();
				}
				else if (blueprint2 == "planks")
				{
					if (Stat.Random(1, 2) == 1)
					{
						GameObject aregionalPlant = this.getARegionalPlant();
						GameObject aregionalPlant2 = this.getARegionalPlant();
						this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("BrinestalkWall");
						this.villageWallPrototype.DisplayName = aregionalPlant.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false).Replace(" tree", "").Replace(" plant", "") + " hedge";
						if (aregionalPlant.Blueprint == aregionalPlant2.Blueprint)
						{
							this.villageWallPrototype.Render.ColorString = "&" + aregionalPlant.Render.GetForegroundColor() + "^y";
							this.villageWallPrototype.Render.TileColor = aregionalPlant.Render.TileColor;
							this.villageWallPrototype.Render.DetailColor = aregionalPlant2.Render.DetailColor;
							this.villageWallPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("{0} of {1} have been cut in a {2} style and bound together with {3} and rope.", new object[]
							{
								Grammar.InitCap(aregionalPlant.getPluralSemantic("Plank", "plank")),
								aregionalPlant.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false),
								HistoricStringExpander.ExpandString(blueprint, null),
								HistoricStringExpander.ExpandString("<spice.instancesOf.tar.!random>", null)
							}));
						}
						else
						{
							this.villageWallPrototype.Render.ColorString = "&" + aregionalPlant.Render.GetForegroundColor() + "^y";
							this.villageWallPrototype.Render.TileColor = aregionalPlant.Render.TileColor;
							this.villageWallPrototype.Render.DetailColor = aregionalPlant2.Render.DetailColor;
							this.villageWallPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("{0} of {1} have been cut in a {2} style and bound together with {3} and {4} of {5} {6}.", new object[]
							{
								Grammar.InitCap(aregionalPlant.getPluralSemantic("Plank", "plank")),
								aregionalPlant.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false),
								HistoricStringExpander.ExpandString(blueprint, null),
								HistoricStringExpander.ExpandString("<spice.instancesOf.tar.!random>", null),
								aregionalPlant2.getPluralSemantic("Fiber", "strip"),
								aregionalPlant2.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false),
								aregionalPlant2.getSingularSemantic("FiberMaterial", "bark")
							}));
						}
					}
					else
					{
						GameObject aregionalPlant3 = this.getARegionalPlant();
						GameObject aregionalCreature = this.getARegionalCreature(false);
						this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("BrinestalkWall");
						this.villageWallPrototype.DisplayName = aregionalPlant3.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false).Replace(" tree", "").Replace(" plant", "") + " hedge";
						this.villageWallPrototype.Render.ColorString = "&" + aregionalPlant3.Render.GetForegroundColor() + "^y";
						this.villageWallPrototype.Render.TileColor = aregionalPlant3.Render.TileColor;
						this.villageWallPrototype.Render.TileColor = aregionalCreature.Render.DetailColor;
						if (aregionalCreature.HasProperName)
						{
							this.villageWallPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("{0} of {1} have been cut in a {2} style and bound together with {3} and the {4} of {5}.", new object[]
							{
								Grammar.InitCap(aregionalPlant3.getPluralSemantic("Plank", "plank")),
								aregionalPlant3.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false),
								HistoricStringExpander.ExpandString(blueprint, null),
								HistoricStringExpander.ExpandString("<spice.instancesOf.tar.!random>", null),
								aregionalCreature.GetxTag("TextFragments", "Skin", "skin"),
								aregionalCreature.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false)
							}));
						}
						else
						{
							this.villageWallPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("{0} of {1} have been cut in a {2} style and bound together with {3} and {4} {5}.", new object[]
							{
								Grammar.InitCap(aregionalPlant3.getPluralSemantic("Plank", "plank")),
								aregionalPlant3.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false),
								HistoricStringExpander.ExpandString(blueprint, null),
								HistoricStringExpander.ExpandString("<spice.instancesOf.tar.!random>", null),
								aregionalCreature.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false),
								aregionalCreature.GetxTag("TextFragments", "Skin", "skin")
							}));
						}
					}
				}
				else if (blueprint2 == "bone")
				{
					GameObject aregionalCreature2 = this.getARegionalCreature(false);
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("BaseSedimentaryRock");
					string singularSemantic = aregionalCreature2.getSingularSemantic("HardMaterial", "bone");
					if (aregionalCreature2.HasProperName)
					{
						this.villageWallPrototype.DisplayName = "compacted " + singularSemantic + " of " + aregionalCreature2.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false);
					}
					else
					{
						this.villageWallPrototype.DisplayName = "compacted " + aregionalCreature2.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false) + " " + singularSemantic;
					}
					this.villageWallPrototype.Render.ColorString = "&Y^" + aregionalCreature2.Render.GetForegroundColor();
					this.villageWallPrototype.Render.TileColor = "&Y";
					this.villageWallPrototype.Render.DetailColor = aregionalCreature2.Render.GetForegroundColor();
					if (aregionalCreature2.HasProperName)
					{
						this.villageWallPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("Crack-stuck {0} binds together the stiff and {1} {2} of {3}.", new object[]
						{
							HistoricStringExpander.ExpandString("<spice.instancesOf.tar.!random>", null),
							HistoricStringExpander.ExpandString(blueprint, null),
							aregionalCreature2.getPluralSemantic("HardMaterial", "bone"),
							Grammar.Pluralize(aregionalCreature2.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false))
						}));
					}
					else
					{
						this.villageWallPrototype.GetPart<Description>().Short = Grammar.ConvertAtoAn(string.Format("Crack-stuck {0} binds together the stiff and {1} {2} of several slaughtered {3}.", new object[]
						{
							HistoricStringExpander.ExpandString("<spice.instancesOf.tar.!random>", null),
							HistoricStringExpander.ExpandString(blueprint, null),
							aregionalCreature2.getPluralSemantic("HardMaterial", "bone"),
							Grammar.Pluralize(aregionalCreature2.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, false, false, null, false))
						}));
					}
				}
				else if (blueprint2 == "stone")
				{
					this.villageWallPrototype = GameObject.Create("Slate");
				}
				else if (blueprint2 == "brick")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("BrickWall");
				}
				else if (blueprint2 == "cyber")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("CrysteelPlatedWall");
				}
				else if (blueprint2 == "fluted")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("SixDayStiltPillarWall1");
				}
				else if (blueprint2 == "granite")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("Granite");
				}
				else if (blueprint2 == "marble")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("Marble");
				}
				else if (blueprint2 == "metal")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("MetalWall");
				}
				else if (blueprint2 == "dirt")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("ThatchedWall");
				}
				else if (blueprint2 == "rock")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("Sandstone");
				}
				else if (blueprint2 == "secure")
				{
					this.villageWallPrototype = GameObjectFactory.Factory.CreateObject("MetalWall");
				}
				this.villageWallPrototype.GetPart<Description>().Short = Grammar.InitCapWithFormatting(ConsoleLib.Console.ColorUtility.StripFormatting(HistoricStringExpander.ExpandString(this.villageWallPrototype.GetPart<Description>().Short, null)));
			}
			return this.villageWallPrototype.DeepCopy(false, false, null);
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x00294D50 File Offset: 0x00292F50
		public static Faction CreateVillageFaction(HistoricEntitySnapshot Snapshot)
		{
			Faction faction = new Faction();
			string property = Snapshot.GetProperty("name", "unknown");
			HistoricEvent historicEvent = Snapshot.entity.events.Find((HistoricEvent x) => x is InitializeVillage);
			faction.Old = false;
			faction.ExtradimensionalVersions = false;
			faction.Visible = !string.Equals(Snapshot.GetProperty("abandoned", "unknown"), "true", StringComparison.CurrentCultureIgnoreCase);
			faction.Name = "villagers of " + property;
			faction.EntityID = Snapshot.entity.id;
			faction.EventID = ((historicEvent != null) ? historicEvent.id : 0L);
			faction.PositiveSound = "Sounds/Reputation/sfx_reputation_village_positive";
			faction.NegativeSound = "Sounds/Reputation/sfx_reputation_village_negative";
			faction.SetProperty("Village", 1);
			if (Snapshot.GetProperty("newFactionName", "unknown") != "unknown")
			{
				faction.DisplayName = Snapshot.GetProperty("newFactionName", "unknown");
			}
			else
			{
				faction.DisplayName = "villagers of " + property;
				faction.FormatWithArticle = true;
			}
			VillageBase.SetVillageFactionEmblem(faction, faction.Name);
			faction.SetFactionFeeling("Wardens", 100);
			faction.SetFactionFeeling("Girsh", -50);
			int num;
			if (Snapshot.hasProperty("highlyEntropicBeingWorshipAttitude") && int.TryParse(Snapshot.GetProperty("highlyEntropicBeingWorshipAttitude", "unknown"), out num))
			{
				if (faction.FactionWorshipAttitudes.ContainsKey("Entropic"))
				{
					faction.ApplyWorshipAttitude("Entropic", num);
				}
				else
				{
					faction.FactionWorshipAttitudes["Entropic"] = num;
				}
			}
			faction.ApplyWorshipAttitude("Girsh", -100);
			if (Snapshot.hasProperty("signatureLiquid"))
			{
				if (50.in100())
				{
					faction.WaterRitualLiquid = Snapshot.GetProperty("signatureLiquid", "unknown");
				}
			}
			else if (Snapshot.listProperties.ContainsKey("signatureLiquids") && 50.in100())
			{
				faction.WaterRitualLiquid = Snapshot.GetList("signatureLiquids").GetRandomElement<string>(null);
			}
			Factions.AddNewFaction(faction);
			Factions.Get("Wardens").SetFactionFeeling(faction.Name, 100);
			return faction;
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x00294F88 File Offset: 0x00293188
		public static void SetVillageFactionEmblem(Faction Faction, string Seed)
		{
			FactionEmblem factionEmblem;
			if ((factionEmblem = Faction.Emblem) == null)
			{
				factionEmblem = (Faction.Emblem = new FactionEmblem());
			}
			FactionEmblem factionEmblem2 = factionEmblem;
			global::System.Random seededRandomGenerator = Stat.GetSeededRandomGenerator(Seed);
			factionEmblem2.Tile = "Terrain/sw_joppa.bmp";
			factionEmblem2.TileColor = (factionEmblem2.ColorString = "&" + Crayons.GetRandomColorAll(seededRandomGenerator));
			factionEmblem2.RenderString = "Ø";
			factionEmblem2.DetailColor = 'y';
			do
			{
				factionEmblem2.DetailColor = Crayons.GetRandomColorAll(seededRandomGenerator)[0];
			}
			while (factionEmblem2.DetailColor == factionEmblem2.ColorString[1]);
		}

		// Token: 0x0400342A RID: 13354
		public const int CHANCE_FOR_NONDROMAD_MERCHANT = 20;

		// Token: 0x0400342B RID: 13355
		public const int CHANCE_FOR_NONHUMAN_TINKER = 50;

		// Token: 0x0400342C RID: 13356
		public const int CHANCE_FOR_NONHUMAN_APOTHECARY = 50;

		// Token: 0x0400342D RID: 13357
		public const int CHANCE_FOR_STATIC_PER_VILLAGE_OBJECT_TO_VARY = 20;

		// Token: 0x0400342E RID: 13358
		public const int CHANCE_FOR_STATIC_PER_BUILDING_OBJECT_TO_VARY = 20;

		// Token: 0x0400342F RID: 13359
		public const int MERCHANT_CHANCE = 30;

		// Token: 0x04003430 RID: 13360
		public const int TINKER_CHANCE = 25;

		// Token: 0x04003431 RID: 13361
		public const int APOTHECARY_CHANCE = 25;

		// Token: 0x04003432 RID: 13362
		public const int SHARED_MUTATION_CHANCE = 80;

		// Token: 0x04003433 RID: 13363
		public const int CHANCE_FOR_SIGNATURE_LIQUID_IN_VESSEL = 80;

		// Token: 0x04003434 RID: 13364
		public const int CHANCE_ABANDONED_VILLAGE_REPOPULATED = 70;

		// Token: 0x04003435 RID: 13365
		public const int CHANCE_REGIONAL_PLANT = 98;

		// Token: 0x04003436 RID: 13366
		public const int CHANCE_ANY_PLANT = 2;

		// Token: 0x04003437 RID: 13367
		public bool isVillageZero;

		// Token: 0x04003438 RID: 13368
		public int villageTier;

		// Token: 0x04003439 RID: 13369
		public int villageTechTier = 1;

		// Token: 0x0400343A RID: 13370
		public string villageTheme = "*Default";

		// Token: 0x0400343B RID: 13371
		public string VillageEntityID;

		// Token: 0x0400343C RID: 13372
		public HistoricEntitySnapshot villageSnapshot;

		// Token: 0x0400343D RID: 13373
		public string region;

		// Token: 0x0400343E RID: 13374
		public string locationName;

		// Token: 0x0400343F RID: 13375
		public string regionName;

		// Token: 0x04003440 RID: 13376
		public string stairs = "";

		// Token: 0x04003441 RID: 13377
		public string monstertable = "";

		// Token: 0x04003442 RID: 13378
		public string villageFaction = "";

		// Token: 0x04003443 RID: 13379
		public string villagerBaseFaction = "Joppa";

		// Token: 0x04003444 RID: 13380
		public const int APPEND_PROVERB_CHANCE = 40;

		// Token: 0x04003445 RID: 13381
		public PopulationLayout townSquareLayout;

		// Token: 0x04003446 RID: 13382
		public InfluenceMapRegion townSquare;

		// Token: 0x04003447 RID: 13383
		public Rect2D townSquareRect;

		// Token: 0x04003448 RID: 13384
		public string villageDoorStyle;

		// Token: 0x04003449 RID: 13385
		public int townSquareDoorStyle;

		// Token: 0x0400344A RID: 13386
		[NonSerialized]
		private HistoricEntity _VillageEntity;

		// Token: 0x0400344B RID: 13387
		public long lastmem;

		// Token: 0x0400344C RID: 13388
		public string signatureItemBlueprint;

		// Token: 0x0400344D RID: 13389
		public GameObject signatureItemExample;

		// Token: 0x0400344E RID: 13390
		public string signatureHistoricObjectType;

		// Token: 0x0400344F RID: 13391
		public GameObject signatureHistoricObjectInstance;

		// Token: 0x04003450 RID: 13392
		public string storyType;

		// Token: 0x04003451 RID: 13393
		public GameObject villageMonumentPrototype;

		// Token: 0x04003452 RID: 13394
		public GameObject villageLightSourcePrototype;

		// Token: 0x04003453 RID: 13395
		public GameObject villageBookPrototype;

		// Token: 0x04003454 RID: 13396
		public string villageName;

		// Token: 0x04003455 RID: 13397
		protected CookingRecipe signatureDish;

		// Token: 0x04003456 RID: 13398
		protected string signatureSkill;

		// Token: 0x04003457 RID: 13399
		public string signatureLiquid;

		// Token: 0x04003458 RID: 13400
		protected List<PopulationLayout> buildings = new List<PopulationLayout>();

		// Token: 0x04003459 RID: 13401
		protected List<Location2D> burrowedDoors = new List<Location2D>();

		// Token: 0x0400345A RID: 13402
		protected List<GameObject> requiredPlacementObjects = new List<GameObject>();

		// Token: 0x0400345B RID: 13403
		protected List<GameObject> originalWalls = new List<GameObject>();

		// Token: 0x0400345C RID: 13404
		protected List<GameObject> originalCreatures = new List<GameObject>();

		// Token: 0x0400345D RID: 13405
		protected List<GameObject> originalPlants = new List<GameObject>();

		// Token: 0x0400345E RID: 13406
		protected List<GameObject> originalItems = new List<GameObject>();

		// Token: 0x0400345F RID: 13407
		protected List<GameObject> originalLiquids = new List<GameObject>();

		// Token: 0x04003460 RID: 13408
		protected List<GameObject> originalFurniture = new List<GameObject>();

		// Token: 0x04003461 RID: 13409
		protected List<Location2D> buildingPaths = new List<Location2D>();

		// Token: 0x04003462 RID: 13410
		public GameObject villageDoorPrototype;

		// Token: 0x04003463 RID: 13411
		public GameObject villageCanvasPrototype;

		// Token: 0x04003464 RID: 13412
		public GameObject villageWallPrototype;
	}
}
