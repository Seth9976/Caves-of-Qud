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
	// Token: 0x02000B09 RID: 2825
	public class VillageCodaBase : ZoneBuilderSandbox
	{
		// Token: 0x06007138 RID: 28984 RVA: 0x0029B5A9 File Offset: 0x002997A9
		public void generateVillageTheme()
		{
			this.villageTheme = this.RollOneFrom("Villages_VillageTheme");
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06007139 RID: 28985 RVA: 0x0029B5BC File Offset: 0x002997BC
		// (set) Token: 0x0600713A RID: 28986 RVA: 0x0029B5F4 File Offset: 0x002997F4
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

		// Token: 0x0600713B RID: 28987 RVA: 0x0029B60F File Offset: 0x0029980F
		public static bool IsVillagerEligible(GameObjectBlueprint Blueprint)
		{
			return Blueprint.HasTag("SemanticChiliad", false);
		}

		// Token: 0x0600713C RID: 28988 RVA: 0x0029B620 File Offset: 0x00299820
		public static List<GameObjectBlueprint> GetEligibleVillagers(string FactionName, bool ReadOnly = true)
		{
			List<GameObjectBlueprint> list = Faction.GetMembers(FactionName, new Predicate<GameObjectBlueprint>(VillageCodaBase.IsVillagerEligible), false, true);
			if (list.IsNullOrEmpty<GameObjectBlueprint>())
			{
				list = Faction.GetMembers(FactionName, null, true, true);
				if (list.IsNullOrEmpty<GameObjectBlueprint>())
				{
					list = Faction.GetMembers(FactionName, null, false, true);
					list.RemoveAll((GameObjectBlueprint x) => x.HasProperName());
				}
			}
			if (!ReadOnly)
			{
				return new List<GameObjectBlueprint>(list);
			}
			return list;
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x0029B698 File Offset: 0x00299898
		public GameObject getBaseVillager(bool NoRep = false)
		{
			if (this.FactionMembers == null)
			{
				this.FactionMembers = VillageCodaBase.GetEligibleVillagers(this.villagerBaseFaction, false);
			}
			GameObjectBlueprint randomElement = this.FactionMembers.GetRandomElement<GameObjectBlueprint>(null);
			GameObject gameObject = ((randomElement != null) ? randomElement.createOne() : null);
			if (gameObject == null)
			{
				gameObject = this.getARegionalCreature(false);
			}
			if (gameObject == null)
			{
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

		// Token: 0x0600713E RID: 28990 RVA: 0x0029B774 File Offset: 0x00299974
		public void setVillagerProperties(GameObject obj, bool RemoveConversation = true)
		{
			try
			{
				obj.Brain.Factions = "";
				obj.Brain.Allegiance.Clear();
				obj.Brain.Allegiance.Add(this.villageFaction, 100);
				obj.Brain.Allegiance.Hostile = false;
				obj.Brain.Allegiance.Calm = true;
				obj.RemovePart<AIPilgrim>();
				obj.SetIntProperty("ParticipantVillager", 1, false);
				if (RemoveConversation)
				{
					obj.RemovePart<ConversationScript>();
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Failed to set villager properties.", ex, "game_exception");
			}
		}

		// Token: 0x0600713F RID: 28991 RVA: 0x0029B820 File Offset: 0x00299A20
		public void AddVillagerConversation(GameObject obj, string message, string response = "Live and drink.", string Q1 = null, string A1 = null, bool AppendConversation = false, bool ClearLost = true)
		{
			string property = this.villageSnapshot.GetProperty("signatureTemperament", null);
			string text = (If.d100(100, null, null) ? ("\n\n" + this.villageSnapshot.GetProperty("proverb", "unknown")) : null);
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

		// Token: 0x06007140 RID: 28992 RVA: 0x0029B8CC File Offset: 0x00299ACC
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

		// Token: 0x06007141 RID: 28993 RVA: 0x0029B97C File Offset: 0x00299B7C
		public string RollOneFrom(string PoptablePrefix)
		{
			return PopulationManager.RollOneFrom(this.ResolvePopulationTableName(PoptablePrefix), null, null).Blueprint;
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x0029B994 File Offset: 0x00299B94
		public GameObject generateVillager(bool bUnique = false)
		{
			GameObject gameObject = this.getBaseVillager(false);
			this.setVillagerProperties(gameObject, true);
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

		// Token: 0x06007143 RID: 28995 RVA: 0x0029BA14 File Offset: 0x00299C14
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
						if (If.Chance(25, null, null))
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

		// Token: 0x06007144 RID: 28996 RVA: 0x0029BC8C File Offset: 0x00299E8C
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

		// Token: 0x06007145 RID: 28997 RVA: 0x0029BC9C File Offset: 0x00299E9C
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

		// Token: 0x06007146 RID: 28998 RVA: 0x0029BE44 File Offset: 0x0029A044
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

		// Token: 0x06007147 RID: 28999 RVA: 0x0029BEB7 File Offset: 0x0029A0B7
		public List<PopulationResult> ResolveOnePopulationFromArgs(List<string> arg, string def = null, int Tier = 1)
		{
			if (arg == null || arg.Count == 0)
			{
				return PopulationManager.Generate(def, null, null, null, null, null);
			}
			return PopulationManager.Generate(arg.GetRandomElement<string>(null), "zonetier", Tier.ToString(), null, null, null);
		}

		// Token: 0x06007148 RID: 29000 RVA: 0x0029BEEB File Offset: 0x0029A0EB
		public string ResolveOneTableFromArgs(List<string> arg, string def = null)
		{
			if (arg == null || arg.Count == 0)
			{
				return def;
			}
			return arg.GetRandomElement<string>(null);
		}

		// Token: 0x06007149 RID: 29001 RVA: 0x0029BF01 File Offset: 0x0029A101
		public void LogMem(string step)
		{
		}

		// Token: 0x0600714A RID: 29002 RVA: 0x0029BF04 File Offset: 0x0029A104
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

		// Token: 0x0600714B RID: 29003 RVA: 0x0029BF3C File Offset: 0x0029A13C
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

		// Token: 0x0600714C RID: 29004 RVA: 0x0029C020 File Offset: 0x0029A220
		public void FabricateAerie(PopulationLayout layout)
		{
			this.zone.CacheZoneConnection("U", layout.position, "aerie", null);
			this.zone.GetCell(layout.position.FromDirection("NW")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position.FromDirection("NE")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position.FromDirection("SW")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position.FromDirection("SE")).AddObject(this.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(layout.position).AddObject("StairsUp", null, null, null, null).SetIntProperty("IdleStairs", 1, false);
		}

		// Token: 0x0600714D RID: 29005 RVA: 0x0029C14D File Offset: 0x0029A34D
		public bool onEdge(Location2D location)
		{
			return location.X <= 1 || location.X >= 79 || (location.Y <= 1 || location.Y >= 24);
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x0029C17C File Offset: 0x0029A37C
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

		// Token: 0x0600714F RID: 29007 RVA: 0x0029C244 File Offset: 0x0029A444
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

		// Token: 0x06007150 RID: 29008 RVA: 0x0029C2D4 File Offset: 0x0029A4D4
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

		// Token: 0x06007151 RID: 29009 RVA: 0x0029C3D4 File Offset: 0x0029A5D4
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

		// Token: 0x06007152 RID: 29010 RVA: 0x0029C52C File Offset: 0x0029A72C
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

		// Token: 0x06007153 RID: 29011 RVA: 0x0029C5F4 File Offset: 0x0029A7F4
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

		// Token: 0x06007154 RID: 29012 RVA: 0x0029CC90 File Offset: 0x0029AE90
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

		// Token: 0x06007155 RID: 29013 RVA: 0x0029D354 File Offset: 0x0029B554
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

		// Token: 0x06007156 RID: 29014 RVA: 0x0029D6B8 File Offset: 0x0029B8B8
		public Rect2D GetRandomEdgePlot(int Width, int Height, int Padding = 1, int Flags = 0)
		{
			int num = 0;
			int num3;
			int num4;
			int num5;
			int num6;
			for (;;)
			{
				int num2 = Stat.Random(1, 4);
				if (num++ > 100)
				{
					break;
				}
				if ((!Flags.HasBit(1) || num2 != 1) && (!Flags.HasBit(2) || num2 != 2) && (!Flags.HasBit(4) || num2 != 3) && (!Flags.HasBit(8) || num2 != 4))
				{
					if (num2 <= 2)
					{
						num3 = Stat.Random(Padding, this.zone.Width - Width - Padding - 1);
						num4 = num3 + Width - 1;
						if (num2 == 2)
						{
							num5 = Padding;
						}
						else
						{
							num5 = this.zone.Height - Height - Padding - 1;
						}
						num6 = num5 + Height - 1;
					}
					else
					{
						num5 = Stat.Random(Padding, this.zone.Height - Height - Padding - 1);
						num6 = num5 + Height - 1;
						if (num2 == 4)
						{
							num3 = Padding;
						}
						else
						{
							num3 = this.zone.Width - Width - Padding - 1;
						}
						num4 = num3 + Width - 1;
					}
					if (!this.IsBoxAvoided(num3, num5, num4, num6) || ++num >= 100)
					{
						goto IL_00F8;
					}
				}
			}
			return Rect2D.zero;
			IL_00F8:
			return new Rect2D(num3, num5, num4, num6);
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x0029D7C8 File Offset: 0x0029B9C8
		public void FabricateVantabudCluster()
		{
			Rect2D randomEdgePlot = this.GetRandomEdgePlot(Stat.Random(2, 3), Stat.Random(2, 3), 1, 0);
			if (randomEdgePlot.Area == 0)
			{
				return;
			}
			base.ClearRect(this.zone, randomEdgePlot);
			int num = Stat.Random(1, randomEdgePlot.Area / 2);
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = GameObject.Create("Vantabud");
				ZoneBuilderSandbox.PlaceObjectInRect(this.zone, randomEdgePlot, gameObject, null);
				if (gameObject.CurrentCell != null)
				{
					this.AvoidPoint(gameObject.CurrentCell.Location);
				}
			}
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x0029D854 File Offset: 0x0029BA54
		public Rect2D ReserveUltraPlot(bool Canvas = false)
		{
			bool flag = Stat.Chance(50);
			Rect2D randomEdgePlot = this.GetRandomEdgePlot(flag ? 7 : Stat.Random(5, 6), flag ? Stat.Random(5, 6) : 7, 1, 4);
			this.AvoidBox(randomEdgePlot.ReduceBy(1, 1));
			for (int i = randomEdgePlot.x1 + 1; i < randomEdgePlot.x2; i++)
			{
				this.AvoidPoint(i, randomEdgePlot.y1);
				this.AvoidPoint(i, randomEdgePlot.y2);
			}
			for (int j = randomEdgePlot.y1 + 1; j < randomEdgePlot.y2; j++)
			{
				this.AvoidPoint(randomEdgePlot.x1, j);
				this.AvoidPoint(randomEdgePlot.x2, j);
			}
			this.AvoidPoint(randomEdgePlot.x1 + 1, randomEdgePlot.y1 + 1);
			this.AvoidPoint(randomEdgePlot.x2 - 1, randomEdgePlot.y1 + 1);
			this.AvoidPoint(randomEdgePlot.x1 + 1, randomEdgePlot.y2 - 1);
			this.AvoidPoint(randomEdgePlot.x2 - 1, randomEdgePlot.y2 - 1);
			string text = this.zone.GetCell(randomEdgePlot.Center).GetGeneralDirectionFrom(Location2D.Get(40, 12));
			if (text.Length > 1)
			{
				text = text.GetRandomElement(null).ToString();
			}
			Point2D randomDoorCell = randomEdgePlot.GetRandomDoorCell(text, 2);
			Point2D point2D = randomDoorCell.FromDirection(randomEdgePlot.DoorDirection);
			this.AvoidPoint(point2D);
			if (!Canvas)
			{
				this.zone.GetCell(randomDoorCell).AddObject("PathConnection", null, null, null, null);
			}
			return randomEdgePlot;
		}

		// Token: 0x06007159 RID: 29017 RVA: 0x0029D9E0 File Offset: 0x0029BBE0
		public void FabricateUltraHut(Rect2D Plot, bool Canvas = false)
		{
			if (Plot.Area == 0)
			{
				return;
			}
			GameObject gameObject = (Canvas ? this.getAVillageCanvas() : this.getAVillageWall());
			base.ClearRect(this.zone, Plot);
			base.FillRect(this.zone, Plot.ReduceBy(1, 1), "DirtPath");
			for (int i = Plot.x1 + 1; i < Plot.x2; i++)
			{
				this.zone.GetCell(i, Plot.y1).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.zone.GetCell(i, Plot.y2).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			for (int j = Plot.y1 + 1; j < Plot.y2; j++)
			{
				this.zone.GetCell(Plot.x1, j).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.zone.GetCell(Plot.x2, j).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			this.zone.GetCell(Plot.x1 + 1, Plot.y1 + 1).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(Plot.x2 - 1, Plot.y1 + 1).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(Plot.x1 + 1, Plot.y2 - 1).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.zone.GetCell(Plot.x2 - 1, Plot.y2 - 1).AddObject(gameObject.DeepCopy(false, false, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Cell cell = this.zone.GetCell(Plot.Center);
			Cell cell2 = this.zone.GetCell(Plot.Door);
			string oppositeDirection = Directions.GetOppositeDirection(Plot.DoorDirection);
			GameObject gameObject2 = GameObject.Create(this.GetUltraConversant());
			this.setVillagerProperties(gameObject2, false);
			cell2.Clear(null, false, false, null);
			if (Canvas)
			{
				cell.AddObject("Campfire", null, null, null, null);
				cell.GetCellFromDirection(oppositeDirection, true).AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				ZoneBuilderSandbox.PlaceObjectInRect(this.zone, Plot, "Bedroll", "AlongWall:" + oppositeDirection, 0, null);
				return;
			}
			cell2.AddObject(this.getAVillageDoor(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			cell.AddObject("Hookah", null, null, null, null);
			cell.GetCellFromDirection(Plot.DoorDirection, true).AddObject("Chiliad Floor Cusion", null, null, null, null);
			cell.GetCellFromDirection(oppositeDirection, true).AddObject("Chiliad Floor Cusion", null, null, null, null);
			cell.GetCellFromDirection(oppositeDirection, true).AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			ZoneBuilderSandbox.PlaceObjectInRect(this.zone, Plot, this.getAVillageLightSource(), "AlongWall:" + oppositeDirection);
		}

		// Token: 0x0600715A RID: 29018 RVA: 0x0029DD50 File Offset: 0x0029BF50
		private string GetUltraConversant()
		{
			if (NephalProperties.AllDead())
			{
				return "Fool of the Gyre";
			}
			if (NephalProperties.AllPacified())
			{
				return "True Godling";
			}
			return "Godling";
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x0029DD74 File Offset: 0x0029BF74
		public void FabricatePlagueyard()
		{
			Rect2D randomEdgePlot = this.GetRandomEdgePlot(Stat.Random(5, 7), Stat.Random(5, 7), 1, 4);
			if (randomEdgePlot.Area == 0)
			{
				return;
			}
			this.AvoidBox(randomEdgePlot);
			base.ClearRect(this.zone, randomEdgePlot);
			string text = null;
			if (Stat.Random(1, 100) <= 75)
			{
				string text2 = "?";
				if (randomEdgePlot.Center.x <= 40)
				{
					text2 = "E";
				}
				if (randomEdgePlot.Center.x >= 40)
				{
					text2 = "W";
				}
				if (randomEdgePlot.y2 > 16)
				{
					text2 = "N";
				}
				if (randomEdgePlot.y1 < 8)
				{
					text2 = "S";
				}
				if (randomEdgePlot.y1 <= 0)
				{
					text2 = "S";
				}
				if (randomEdgePlot.y2 >= 24)
				{
					text2 = "N";
				}
				if (randomEdgePlot.y1 <= 0 && randomEdgePlot.y2 >= 24)
				{
					if (randomEdgePlot.Center.x <= 40)
					{
						text2 = "E";
					}
					if (randomEdgePlot.Center.x >= 40)
					{
						text2 = "W";
					}
				}
				Point2D randomDoorCell = randomEdgePlot.GetRandomDoorCell(text2, 2);
				ZoneBuilderSandbox.PlaceObjectOnRect(this.zone, "BrinestalkFence", randomEdgePlot, false);
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
				text = randomEdgePlot.GetCellSide(randomDoorCell);
			}
			Rect2D rect2D = randomEdgePlot.ReduceBy(0, 0);
			if (Stat.Random(1, 100) <= 75)
			{
				if (text == null)
				{
					text = Directions.GetRandomCardinalDirection();
				}
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
			Rect2D rect2D2 = randomEdgePlot.ReduceBy(1, 1);
			int num2 = Stat.Random(rect2D2.Area / 4, rect2D2.Area / 2);
			string disease = this.villageSnapshot.GetList("sharedDiseases").FirstOrDefault<string>();
			Action<GameObject> <>9__0;
			for (int i = 0; i < num2; i++)
			{
				string text3 = "Tombstone";
				int num3 = 0;
				int num4 = 0;
				string text4 = null;
				Action<GameObject> action;
				if ((action = <>9__0) == null)
				{
					action = (<>9__0 = delegate(GameObject o)
					{
						Tombstone tombstone;
						if (o.TryGetPart<Tombstone>(out tombstone))
						{
							GameObject baseVillager = this.getBaseVillager(false);
							tombstone.name = NameMaker.MakeName(baseVillager, null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
							baseVillager.Pool();
							if (!disease.IsNullOrEmpty() && If.Chance(25, null, null))
							{
								if (disease == "Glotrot")
								{
									tombstone.Inscription = "Succumbed to glotrot";
									return;
								}
								if (disease == "Ironshank")
								{
									tombstone.Inscription = "Succumbed to ironshank";
								}
							}
						}
					});
				}
				GameObject gameObject = GameObject.Create(text3, num3, num4, text4, action, null, null, null);
				ZoneBuilderSandbox.PlaceObjectInRect(this.zone, rect2D2, gameObject, null);
			}
			if (!randomEdgePlot.DoorDirection.IsNullOrEmpty())
			{
				this.zone.GetCell(randomEdgePlot.Door).AddObject("PathConnection", null, null, null, null);
			}
		}

		// Token: 0x0600715C RID: 29020 RVA: 0x0029E134 File Offset: 0x0029C334
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

		// Token: 0x0600715D RID: 29021 RVA: 0x0029E550 File Offset: 0x0029C750
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

		// Token: 0x0600715E RID: 29022 RVA: 0x0029E65C File Offset: 0x0029C85C
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

		// Token: 0x0600715F RID: 29023 RVA: 0x0029E700 File Offset: 0x0029C900
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

		// Token: 0x06007160 RID: 29024 RVA: 0x0029E8D0 File Offset: 0x0029CAD0
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

		// Token: 0x06007161 RID: 29025 RVA: 0x0029E938 File Offset: 0x0029CB38
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

		// Token: 0x06007162 RID: 29026 RVA: 0x0029E9A0 File Offset: 0x0029CBA0
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

		// Token: 0x06007163 RID: 29027 RVA: 0x0029EDC4 File Offset: 0x0029CFC4
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

		// Token: 0x06007164 RID: 29028 RVA: 0x0029EF30 File Offset: 0x0029D130
		public virtual void cleanup()
		{
			this.buildings = null;
			this.villageSnapshot = null;
			this.templates = null;
			MemoryHelper.GCCollect();
		}

		// Token: 0x06007165 RID: 29029 RVA: 0x0029EF4C File Offset: 0x0029D14C
		public string ResolvePopulationTableName(string Prefix)
		{
			string text = Prefix + "_Coda_" + this.region;
			if (PopulationManager.HasTable(text))
			{
				return text;
			}
			text = Prefix + "_Coda_*Default";
			if (PopulationManager.HasTable(text))
			{
				return text;
			}
			text = Prefix + "_Faction_" + this.villagerBaseFaction;
			if (PopulationManager.HasTable(text))
			{
				return text;
			}
			text = Prefix + "_" + this.region;
			if (PopulationManager.HasTable(text))
			{
				return text;
			}
			return Prefix + "_*Default";
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x0029EFCE File Offset: 0x0029D1CE
		public GameObject generateVillageOven()
		{
			GameObject gameObject = GameObjectFactory.create("Oven");
			gameObject.SetIntProperty("QuestItem", 1, false);
			Campfire part = gameObject.GetPart<Campfire>();
			part.specificProcgenMeals = new List<CookingRecipe>();
			part.specificProcgenMeals.Add(this.signatureDish);
			return gameObject;
		}

		// Token: 0x06007167 RID: 29031 RVA: 0x0029F00C File Offset: 0x0029D20C
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

		// Token: 0x06007168 RID: 29032 RVA: 0x0029F0B8 File Offset: 0x0029D2B8
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

		// Token: 0x06007169 RID: 29033 RVA: 0x0029F12C File Offset: 0x0029D32C
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

		// Token: 0x0600716A RID: 29034 RVA: 0x0029F15C File Offset: 0x0029D35C
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

		// Token: 0x0600716B RID: 29035 RVA: 0x0029F1E0 File Offset: 0x0029D3E0
		public GameObject getARegionalPlant()
		{
			string text = null;
			string text2 = this.ResolvePopulationTableName("Village_Plants");
			if (PopulationManager.HasPopulation(text2))
			{
				text = PopulationManager.RollOneFrom(text2, null, null).Blueprint;
			}
			if (text == null)
			{
				text = PopulationManager.RollOneFrom("DynamicObjectsTable:Coda_" + this.region + "_Plants", null, null).Blueprint;
			}
			if (text == null)
			{
				text = PopulationManager.RollOneFrom("DynamicObjectsTable:" + this.region + "_Plants", null, null).Blueprint;
			}
			if (text != null)
			{
				return GameObject.Create(text);
			}
			throw new Exception("Invalid regional plant for coda.");
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x0029F273 File Offset: 0x0029D473
		public GameObject getAFarmablePlant()
		{
			return this.getARegionalPlant();
		}

		// Token: 0x0600716D RID: 29037 RVA: 0x0029F27C File Offset: 0x0029D47C
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

		// Token: 0x0600716E RID: 29038 RVA: 0x0029F2C0 File Offset: 0x0029D4C0
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

		// Token: 0x0600716F RID: 29039 RVA: 0x0029F33C File Offset: 0x0029D53C
		public GameObject getAVillageLightSource()
		{
			if (this.villageLightSourcePrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_LightSourceType"), null, null).Blueprint;
				this.villageLightSourcePrototype = GameObject.Create(blueprint);
			}
			return this.villageLightSourcePrototype.DeepCopy(false, false, null);
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x0029F384 File Offset: 0x0029D584
		public GameObject getAVillageBook()
		{
			if (this.villageBookPrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Villages_BookType"), null, null).Blueprint;
				this.villageBookPrototype = GameObject.Create(blueprint);
			}
			return this.villageBookPrototype.DeepCopy(false, false, null);
		}

		// Token: 0x06007171 RID: 29041 RVA: 0x0029F3CC File Offset: 0x0029D5CC
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

		// Token: 0x06007172 RID: 29042 RVA: 0x0029F464 File Offset: 0x0029D664
		protected HistoricPerspective storyPerspective(JournalVillageNote story)
		{
			return this.villageSnapshot.requirePerspective(this.villageEntity.history.GetEvent(story.EventID), Stat.Random(300, 1000));
		}

		// Token: 0x06007173 RID: 29043 RVA: 0x0029F49C File Offset: 0x0029D69C
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

		// Token: 0x06007174 RID: 29044 RVA: 0x0029F66C File Offset: 0x0029D86C
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

		// Token: 0x06007175 RID: 29045 RVA: 0x0029F6C8 File Offset: 0x0029D8C8
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

		// Token: 0x06007176 RID: 29046 RVA: 0x0029FA50 File Offset: 0x0029DC50
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

		// Token: 0x06007177 RID: 29047 RVA: 0x0029FAA8 File Offset: 0x0029DCA8
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

		// Token: 0x06007178 RID: 29048 RVA: 0x0029FAE0 File Offset: 0x0029DCE0
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

		// Token: 0x06007179 RID: 29049 RVA: 0x0029FB2C File Offset: 0x0029DD2C
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

		// Token: 0x0600717A RID: 29050 RVA: 0x002A003C File Offset: 0x0029E23C
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

		// Token: 0x0600717B RID: 29051 RVA: 0x002A03F8 File Offset: 0x0029E5F8
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

		// Token: 0x0600717C RID: 29052 RVA: 0x002A0528 File Offset: 0x0029E728
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
									goto IL_0FDB;
								}
							}
							if (this.storyType == "Town Crier")
							{
								GameObject gameObject2 = GameObject.Create("Village Crier");
								this.setVillagerProperties(gameObject2, true);
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
										goto IL_0FDB;
									}
								}
								if (this.storyType == "Animatronic")
								{
									GameObject gameObject8 = GameObject.Create("Village Robot");
									this.setVillagerProperties(gameObject8, true);
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
													goto IL_0F77;
												}
												goto IL_0F00;
											}
											goto IL_0F00;
											IL_0F77:
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
											IL_0F00:
											if (this.storyType == "Walls")
											{
												using (List<Location2D>.Enumerator enumerator5 = populationLayout2.originalRegion.Cells.GetEnumerator())
												{
													while (enumerator5.MoveNext())
													{
														Location2D location2D4 = enumerator5.Current;
														action(this.zone.GetCell(location2D4).GetFirstWall());
													}
													goto IL_0F77;
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
						IL_0FDB:
						if ((storyPlacements != 0 && !flag) || ++num2 >= 5)
						{
							goto IL_0FFC;
						}
						num += 5;
					}
					throw new Exception("unsupported story type " + this.storyType);
					IL_0FFC:;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("placeStories", ex, "game_exception");
			}
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x002A16B4 File Offset: 0x0029F8B4
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

		// Token: 0x0600717E RID: 29054 RVA: 0x002A1834 File Offset: 0x0029FA34
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

		// Token: 0x0600717F RID: 29055 RVA: 0x002A1938 File Offset: 0x0029FB38
		public bool TryGeneratePlayerSignatureDish()
		{
			List<JournalRecipeNote> list = new List<JournalRecipeNote>();
			foreach (JournalRecipeNote journalRecipeNote in JournalAPI.RecipeNotes)
			{
				if (journalRecipeNote.Attributes.Contains("chef:player"))
				{
					list.Add(journalRecipeNote);
				}
			}
			JournalRecipeNote randomElement = list.GetRandomElement<JournalRecipeNote>(null);
			CookingRecipe cookingRecipe = ((randomElement != null) ? randomElement.Recipe.DeepCopy() : null);
			if (cookingRecipe != null)
			{
				cookingRecipe.ChefName = null;
				this.signatureDish = cookingRecipe;
				return true;
			}
			return false;
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x002A19D0 File Offset: 0x0029FBD0
		public void generateSignatureDish(string Style = "Focal")
		{
			if (this.TryGeneratePlayerSignatureDish())
			{
				return;
			}
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

		// Token: 0x06007181 RID: 29057 RVA: 0x002A1CB0 File Offset: 0x0029FEB0
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

		// Token: 0x06007182 RID: 29058 RVA: 0x002A1D5C File Offset: 0x0029FF5C
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

		// Token: 0x06007183 RID: 29059 RVA: 0x002A1E14 File Offset: 0x002A0014
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

		// Token: 0x06007184 RID: 29060 RVA: 0x002A1EEC File Offset: 0x002A00EC
		public virtual void generateStoryType()
		{
			string text = this.ResolvePopulationTableName("Village_StoryEmbodiment");
			PopulationResult populationResult = PopulationManager.RollOneFrom(text, null, null);
			if (this.villageSnapshot.GetProperty("abandoned", "unknown") == "true")
			{
				while (populationResult.Hint.HasDelimitedSubstring(',', "NotAbandoned", StringComparison.Ordinal))
				{
					populationResult = PopulationManager.RollOneFrom(text, null, null);
				}
			}
			this.storyType = populationResult.Blueprint;
			this.villageEntity.SetEntityPropertyAtCurrentYear("storyType", this.storyType);
		}

		// Token: 0x06007185 RID: 29061 RVA: 0x002A1F74 File Offset: 0x002A0174
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

		// Token: 0x06007186 RID: 29062 RVA: 0x002A2810 File Offset: 0x002A0A10
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

		// Token: 0x06007187 RID: 29063 RVA: 0x002A2890 File Offset: 0x002A0A90
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

		// Token: 0x06007188 RID: 29064 RVA: 0x002A2908 File Offset: 0x002A0B08
		public GameObject getAVillageCanvas()
		{
			if (this.villageCanvasPrototype == null)
			{
				string blueprint = PopulationManager.RollOneFrom(this.ResolvePopulationTableName("Village_ConstructionAdjectives"), null, null).Blueprint;
				GameObject aregionalCreature = this.getARegionalCreature(false);
				this.villageCanvasPrototype = GameObject.Create("Chiliad CanvasWall");
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

		// Token: 0x06007189 RID: 29065 RVA: 0x002A2A8C File Offset: 0x002A0C8C
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

		// Token: 0x0600718A RID: 29066 RVA: 0x002A3360 File Offset: 0x002A1560
		public PopulationLayout PickBuilding(bool Unoccupied = false)
		{
			PopulationLayout populationLayout = this.buildings.GetRandomElement<PopulationLayout>(null);
			int num = int.MinValue;
			int num2 = int.MaxValue;
			int i = 0;
			int count = this.buildings.Count;
			while (i < count)
			{
				PopulationLayout populationLayout2 = this.buildings[i];
				int value = this.BuildingOccupants.GetValue(populationLayout2, 0);
				if (value <= num2)
				{
					int area = populationLayout2.innerRect.Area;
					if (value < num2 || num < area)
					{
						populationLayout = populationLayout2;
						num2 = value;
						num = area;
					}
				}
				i++;
			}
			if (Unoccupied && num2 > 0)
			{
				return null;
			}
			this.BuildingOccupants[populationLayout] = num2 + 1;
			return populationLayout;
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x002A3400 File Offset: 0x002A1600
		public void AddAvoidedToMap(InfluenceMap Map)
		{
			if (this.Avoid == null)
			{
				return;
			}
			int i = 0;
			int upperBound = this.Avoid.GetUpperBound(0);
			while (i <= upperBound)
			{
				int j = 0;
				int upperBound2 = this.Avoid.GetUpperBound(1);
				while (j <= upperBound2)
				{
					if (this.Avoid[i, j])
					{
						Map.Walls[i, j] = 1;
					}
					j++;
				}
				i++;
			}
		}

		// Token: 0x0600718C RID: 29068 RVA: 0x002A3468 File Offset: 0x002A1668
		public bool IsBoxAvoided(int X1, int Y1, int X2, int Y2)
		{
			if (this.Avoid == null)
			{
				return false;
			}
			for (int i = X1; i <= X2; i++)
			{
				for (int j = Y1; j <= Y2; j++)
				{
					if (this.Avoid[i, j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600718D RID: 29069 RVA: 0x002A34AA File Offset: 0x002A16AA
		public bool IsPointAvoided(int X, int Y)
		{
			return this.Avoid != null && this.Avoid[X, Y];
		}

		// Token: 0x0600718E RID: 29070 RVA: 0x002A34C6 File Offset: 0x002A16C6
		public void AvoidPoint(Point2D Point)
		{
			this.AvoidPoint(Point.x, Point.y);
		}

		// Token: 0x0600718F RID: 29071 RVA: 0x002A34DA File Offset: 0x002A16DA
		public void AvoidPoint(Location2D Location)
		{
			this.AvoidPoint(Location.X, Location.Y);
		}

		// Token: 0x06007190 RID: 29072 RVA: 0x002A34EE File Offset: 0x002A16EE
		public void AvoidPoint(int X, int Y)
		{
			if (this.Avoid == null)
			{
				this.Avoid = new bool[this.zone.Width, this.zone.Height];
			}
			this.Avoid[X, Y] = true;
		}

		// Token: 0x06007191 RID: 29073 RVA: 0x002A3528 File Offset: 0x002A1728
		public void AvoidBox(int X1, int Y1, int X2, int Y2)
		{
			if (this.Avoid == null)
			{
				this.Avoid = new bool[this.zone.Width, this.zone.Height];
			}
			for (int i = Y1; i <= Y2; i++)
			{
				for (int j = X1; j <= X2; j++)
				{
					this.Avoid[j, i] = true;
				}
			}
		}

		// Token: 0x06007192 RID: 29074 RVA: 0x002A3585 File Offset: 0x002A1785
		public void AvoidBox(Rect2D Box)
		{
			this.AvoidBox(Box.x1, Box.y1, Box.x2, Box.y2);
		}

		// Token: 0x04003478 RID: 13432
		public const int CHANCE_FOR_NONDROMAD_MERCHANT = 20;

		// Token: 0x04003479 RID: 13433
		public const int CHANCE_FOR_NONHUMAN_TINKER = 50;

		// Token: 0x0400347A RID: 13434
		public const int CHANCE_FOR_NONHUMAN_APOTHECARY = 50;

		// Token: 0x0400347B RID: 13435
		public const int CHANCE_FOR_STATIC_PER_VILLAGE_OBJECT_TO_VARY = 20;

		// Token: 0x0400347C RID: 13436
		public const int CHANCE_FOR_STATIC_PER_BUILDING_OBJECT_TO_VARY = 20;

		// Token: 0x0400347D RID: 13437
		public const int MERCHANT_CHANCE = 100;

		// Token: 0x0400347E RID: 13438
		public const int TINKER_CHANCE = 100;

		// Token: 0x0400347F RID: 13439
		public const int APOTHECARY_CHANCE = 100;

		// Token: 0x04003480 RID: 13440
		public const int SHARED_MUTATION_CHANCE = 80;

		// Token: 0x04003481 RID: 13441
		public const int SHARED_DISEASE_CHANCE = 25;

		// Token: 0x04003482 RID: 13442
		public const int CHANCE_FOR_SIGNATURE_LIQUID_IN_VESSEL = 80;

		// Token: 0x04003483 RID: 13443
		public const int CHANCE_ABANDONED_VILLAGE_REPOPULATED = 0;

		// Token: 0x04003484 RID: 13444
		public const int CHANCE_REGIONAL_PLANT = 98;

		// Token: 0x04003485 RID: 13445
		public const int CHANCE_ANY_PLANT = 2;

		// Token: 0x04003486 RID: 13446
		public bool isVillageZero;

		// Token: 0x04003487 RID: 13447
		public int villageTier;

		// Token: 0x04003488 RID: 13448
		public int villageTechTier = 1;

		// Token: 0x04003489 RID: 13449
		public string villageTheme = "*Default";

		// Token: 0x0400348A RID: 13450
		public string VillageEntityID;

		// Token: 0x0400348B RID: 13451
		public HistoricEntitySnapshot villageSnapshot;

		// Token: 0x0400348C RID: 13452
		public string region;

		// Token: 0x0400348D RID: 13453
		public string locationName;

		// Token: 0x0400348E RID: 13454
		public string regionName;

		// Token: 0x0400348F RID: 13455
		public string stairs = "";

		// Token: 0x04003490 RID: 13456
		public string monstertable = "";

		// Token: 0x04003491 RID: 13457
		public string villageFaction = "";

		// Token: 0x04003492 RID: 13458
		public string villagerBaseFaction = "Joppa";

		// Token: 0x04003493 RID: 13459
		public const int APPEND_PROVERB_CHANCE = 100;

		// Token: 0x04003494 RID: 13460
		public PopulationLayout townSquareLayout;

		// Token: 0x04003495 RID: 13461
		public InfluenceMapRegion townSquare;

		// Token: 0x04003496 RID: 13462
		public bool[,] Avoid;

		// Token: 0x04003497 RID: 13463
		public Rect2D townSquareRect;

		// Token: 0x04003498 RID: 13464
		public string villageDoorStyle;

		// Token: 0x04003499 RID: 13465
		public int townSquareDoorStyle;

		// Token: 0x0400349A RID: 13466
		[NonSerialized]
		private HistoricEntity _VillageEntity;

		// Token: 0x0400349B RID: 13467
		private List<GameObjectBlueprint> FactionMembers;

		// Token: 0x0400349C RID: 13468
		public long lastmem;

		// Token: 0x0400349D RID: 13469
		public string signatureItemBlueprint;

		// Token: 0x0400349E RID: 13470
		public GameObject signatureItemExample;

		// Token: 0x0400349F RID: 13471
		public string signatureHistoricObjectType;

		// Token: 0x040034A0 RID: 13472
		public GameObject signatureHistoricObjectInstance;

		// Token: 0x040034A1 RID: 13473
		public string storyType;

		// Token: 0x040034A2 RID: 13474
		public GameObject villageMonumentPrototype;

		// Token: 0x040034A3 RID: 13475
		public GameObject villageLightSourcePrototype;

		// Token: 0x040034A4 RID: 13476
		public GameObject villageBookPrototype;

		// Token: 0x040034A5 RID: 13477
		public string villageName;

		// Token: 0x040034A6 RID: 13478
		protected CookingRecipe signatureDish;

		// Token: 0x040034A7 RID: 13479
		protected string signatureSkill;

		// Token: 0x040034A8 RID: 13480
		public string signatureLiquid;

		// Token: 0x040034A9 RID: 13481
		protected List<PopulationLayout> buildings = new List<PopulationLayout>();

		// Token: 0x040034AA RID: 13482
		protected Dictionary<PopulationLayout, int> BuildingOccupants = new Dictionary<PopulationLayout, int>();

		// Token: 0x040034AB RID: 13483
		protected List<Location2D> burrowedDoors = new List<Location2D>();

		// Token: 0x040034AC RID: 13484
		protected List<GameObject> requiredPlacementObjects = new List<GameObject>();

		// Token: 0x040034AD RID: 13485
		protected List<GameObject> originalWalls = new List<GameObject>();

		// Token: 0x040034AE RID: 13486
		protected List<GameObject> originalCreatures = new List<GameObject>();

		// Token: 0x040034AF RID: 13487
		protected List<GameObject> originalPlants = new List<GameObject>();

		// Token: 0x040034B0 RID: 13488
		protected List<GameObject> originalItems = new List<GameObject>();

		// Token: 0x040034B1 RID: 13489
		protected List<GameObject> originalLiquids = new List<GameObject>();

		// Token: 0x040034B2 RID: 13490
		protected List<GameObject> originalFurniture = new List<GameObject>();

		// Token: 0x040034B3 RID: 13491
		protected List<Location2D> buildingPaths = new List<Location2D>();

		// Token: 0x040034B4 RID: 13492
		public GameObject villageDoorPrototype;

		// Token: 0x040034B5 RID: 13493
		public GameObject villageCanvasPrototype;

		// Token: 0x040034B6 RID: 13494
		public GameObject villageWallPrototype;
	}
}
