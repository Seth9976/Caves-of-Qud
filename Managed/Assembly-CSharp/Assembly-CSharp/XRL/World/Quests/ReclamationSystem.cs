using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Language;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Quests.GolemQuest;
using XRL.World.ZoneBuilders;

namespace XRL.World.Quests
{
	// Token: 0x02000B1E RID: 2846
	[HasWishCommand]
	[Serializable]
	public class ReclamationSystem : IQuestSystem
	{
		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600725E RID: 29278 RVA: 0x002B46C3 File Offset: 0x002B28C3
		// (set) Token: 0x0600725F RID: 29279 RVA: 0x002B46D9 File Offset: 0x002B28D9
		public bool Super
		{
			get
			{
				return base.Quest.GetProperty("Super", 0) == 1;
			}
			set
			{
				base.Quest.SetProperty("Super", value ? 1 : 0);
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06007260 RID: 29280 RVA: 0x002B46F4 File Offset: 0x002B28F4
		public HashSet<string> Perimeter
		{
			get
			{
				if (this._Perimeter == null)
				{
					this._Perimeter = new HashSet<string>(25);
					foreach (Location2D location2D in Zone.zoneIDTo240x72Location("JoppaWorld.53.3.1.1.10").YieldPerimeter(2))
					{
						this._Perimeter.Add(ZoneID.Assemble("JoppaWorld", location2D, 10));
					}
				}
				return this._Perimeter;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06007261 RID: 29281 RVA: 0x002B4778 File Offset: 0x002B2978
		public override bool RemoveWithQuest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007262 RID: 29282 RVA: 0x002B477B File Offset: 0x002B297B
		public override void Start()
		{
			this.NextAttempt++;
			this.Timer = Stat.Roll(base.GetProperty("ArrivalTimer", null), null);
			this.CreateWarleaders();
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x002B47AC File Offset: 0x002B29AC
		public override void FinishStep(QuestStep Step)
		{
			if (this.Stage == 0 && Step.ID.StartsWith("Warleader"))
			{
				if (this.WarleaderSteps().All<QuestStep>((QuestStep x) => x.Finished))
				{
					this.Timer = 0;
				}
			}
		}

		// Token: 0x06007264 RID: 29284 RVA: 0x002B4806 File Offset: 0x002B2A06
		public override void Finish()
		{
			if (this.Stage < 2)
			{
				this.Stage = 2;
			}
			The.ActiveZone.PlayMusic();
			CheckpointingSystem.ManualCheckpoint();
		}

		// Token: 0x06007265 RID: 29285 RVA: 0x002B4827 File Offset: 0x002B2A27
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
			Registrar.Register(SingletonEvent<EndTurnEvent>.ID, 0, false);
			Registrar.Register(SingletonEvent<BeforePlayMusicEvent>.ID, 0, false);
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x002B4850 File Offset: 0x002B2A50
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(EnteringZoneEvent.ID, 0, false);
		}

		// Token: 0x06007267 RID: 29287 RVA: 0x002B4860 File Offset: 0x002B2A60
		public override bool HandleEvent(BeforePlayMusicEvent E)
		{
			if (!this.Active || base.Quest.Finished || E.Channel != "music")
			{
				return true;
			}
			Zone activeZone = The.ActiveZone;
			return activeZone == null || !this.Perimeter.Contains(activeZone.ZoneID) || E.Track == "Music/Arrival of the Official Party";
		}

		// Token: 0x06007268 RID: 29288 RVA: 0x002B48C4 File Offset: 0x002B2AC4
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.OnZoneActivated(E.Zone);
			return base.HandleEvent(E);
		}

		// Token: 0x06007269 RID: 29289 RVA: 0x002B48D9 File Offset: 0x002B2AD9
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.OnEndTurn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600726A RID: 29290 RVA: 0x002B48E8 File Offset: 0x002B2AE8
		public override bool HandleEvent(EnteringZoneEvent E)
		{
			if (this.Active && this.Stage < 2 && !E.Forced && !E.System)
			{
				Zone zone = E.Cell.ParentZone;
				InteriorZone interiorZone = zone as InteriorZone;
				if (interiorZone != null)
				{
					zone = interiorZone.ResolveBasisZone() ?? zone;
				}
				if (!this.Perimeter.Contains(zone.ZoneID) && Popup.ShowYesNo(base.GetProperty("MessageLeaving", null), "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600726B RID: 29291 RVA: 0x002B4970 File Offset: 0x002B2B70
		public void OnZoneActivated(Zone Z)
		{
			InteriorZone interiorZone = Z as InteriorZone;
			if (interiorZone != null)
			{
				Z = interiorZone.ResolveBasisZone() ?? Z;
			}
			if (this.Perimeter.Contains(Z.ZoneID))
			{
				SoundManager.PlayMusic("Music/Arrival of the Official Party", "music", true, 12f, 1f, null, true);
				this.Active = true;
				The.ActionManager.AllowCachedTurns = true;
				if (this.Attempt < this.NextAttempt)
				{
					this.StartAttempt();
					return;
				}
			}
			else if (this.Active)
			{
				this.Active = false;
				The.ActionManager.AllowCachedTurns = false;
				if (this.Stage == 2)
				{
					this.RestoreFauna();
					base.Game.FlagSystemForRemoval(this);
					return;
				}
				this.LeftTick = The.Game.TimeTicks;
				this.FailAttempt();
			}
		}

		// Token: 0x0600726C RID: 29292 RVA: 0x002B4A38 File Offset: 0x002B2C38
		public void OnEndTurn()
		{
			if (!this.Active)
			{
				return;
			}
			int num = this.RechargeTimer - 1;
			this.RechargeTimer = num;
			bool flag = num <= 0;
			if (flag)
			{
				this.RechargeTimer = 100;
			}
			ZoneManager zoneManager = The.ZoneManager;
			foreach (string text in this.Perimeter)
			{
				Zone zone;
				if (zoneManager.CachedZones.TryGetValue(text, out zone))
				{
					zone.MarkActive();
				}
				else
				{
					zone = zoneManager.GetZone(text);
					zoneManager.SetCachedZone(zone);
				}
				if (flag)
				{
					this.RechargeMecha(zone);
				}
			}
			if (this.Stage == 0)
			{
				num = this.Timer - 1;
				this.Timer = num;
				if (num <= 0)
				{
					this.SpawnNephilim(null);
				}
			}
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x002B4B14 File Offset: 0x002B2D14
		private void RechargeMecha(Zone Z)
		{
			foreach (GameObject gameObject in Z.IterateObjects())
			{
				Vehicle vehicle;
				EnergyCellSocket energyCellSocket;
				IEnergyCell energyCell;
				if (gameObject.HasTag("Mecha") && !gameObject.IsPlayerControlled() && gameObject.TryGetPart<Vehicle>(out vehicle) && vehicle.PilotID != null && gameObject.TryGetPart<EnergyCellSocket>(out energyCellSocket) && energyCellSocket.Cell != null && energyCellSocket.Cell.TryGetPartDescendedFrom<IEnergyCell>(out energyCell))
				{
					energyCell.MaximizeCharge();
				}
			}
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x002B4B94 File Offset: 0x002B2D94
		public IEnumerable<QuestStep> WarleaderSteps()
		{
			foreach (KeyValuePair<string, QuestStep> keyValuePair in base.Quest.StepsByID)
			{
				QuestStep value = keyValuePair.Value;
				if (!value.Value.IsNullOrEmpty())
				{
					yield return value;
				}
			}
			Dictionary<string, QuestStep>.Enumerator enumerator = default(Dictionary<string, QuestStep>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600726F RID: 29295 RVA: 0x002B4BA4 File Offset: 0x002B2DA4
		public Dictionary<string, string> GetSpawnTables()
		{
			Dictionary<string, string> dictionary = base.GetDictionary((this.NextAttempt <= 1) ? "BattlePopulations" : "ReinforcementPopulations");
			Dictionary<string, string> dictionary2 = base.GetDictionary("FixedSpawnLocations");
			int property = base.GetProperty("MajorBattles", 0);
			int property2 = base.GetProperty("MinorBattles", 0);
			List<QuestStep> list = ((this.NextAttempt <= 1) ? this.WarleaderSteps().ToList<QuestStep>() : null);
			int num = ((this.NextAttempt <= 1) ? 100 : base.GetProperty("ReinforcementChance", 0));
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>(property + property2 + dictionary2.Count + list.Count);
			using (Dictionary<string, string>.Enumerator enumerator = dictionary2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, string> keyValuePair = enumerator.Current;
					if (!num.in100())
					{
						dictionary3[keyValuePair.Key] = null;
					}
					else if (!list.IsNullOrEmpty<QuestStep>() && keyValuePair.Value == "Major")
					{
						dictionary3[keyValuePair.Key] = list.RemoveRandomElement<QuestStep>(null).Value;
					}
					else
					{
						dictionary3[keyValuePair.Key] = dictionary[keyValuePair.Value];
					}
				}
				goto IL_015D;
			}
			IL_0129:
			QuestStep randomElement = list.GetRandomElement<QuestStep>(null);
			string randomElement2 = this.Perimeter.GetRandomElement<string>(null);
			if (dictionary3.TryAdd(randomElement2, randomElement.Value))
			{
				list.Remove(randomElement);
			}
			IL_015D:
			if (list.IsNullOrEmpty<QuestStep>())
			{
				string text = dictionary["Major"];
				for (int i = 0; i < property; i++)
				{
					if (num.in100())
					{
						string randomElement3 = this.Perimeter.GetRandomElement<string>(null);
						if (!dictionary3.TryAdd(randomElement3, text))
						{
							i--;
						}
					}
				}
				text = dictionary["Minor"];
				for (int j = 0; j < property2; j++)
				{
					if (num.in100())
					{
						string randomElement4 = this.Perimeter.GetRandomElement<string>(null);
						if (dictionary3.TryAdd(randomElement4, text))
						{
							j--;
						}
					}
				}
				return dictionary3;
			}
			goto IL_0129;
		}

		// Token: 0x06007270 RID: 29296 RVA: 0x002B4DB8 File Offset: 0x002B2FB8
		public void CreateWarleaders()
		{
			List<string> list = new List<string>();
			foreach (QuestStep questStep in this.WarleaderSteps())
			{
				PopulationInfo populationInfo = PopulationManager.ResolvePopulation(questStep.Value, true);
				object obj;
				if (populationInfo == null)
				{
					obj = null;
				}
				else
				{
					obj = populationInfo.Find((PopulationItem x) => x.Hint == "Warleader");
				}
				PopulationObject populationObject = obj as PopulationObject;
				if (populationObject != null)
				{
					GameObject gameObject = GameObject.Create(populationObject.Blueprint);
					Vehicle vehicle;
					if (gameObject.TryGetPart<Vehicle>(out vehicle) && vehicle.Pilot != null)
					{
						vehicle.Pilot.AddPart<FinishQuestStepWhenSlain>(new FinishQuestStepWhenSlain
						{
							Quest = this.QuestID,
							Step = questStep.ID
						}, true, false);
					}
					gameObject.AddPart<OmonporchBattleWarleader>(new OmonporchBattleWarleader
					{
						Quest = this.QuestID,
						Step = questStep.ID
					}, true, false);
					gameObject.SetImportant(true, false, false);
					questStep.Name = questStep.Name.Replace("=name=", gameObject.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, false, false, null, false));
					list.Add(gameObject.ID);
					The.ZoneManager.CacheObject(gameObject, false, false);
				}
			}
			base.SetProperty("Warleaders", list);
		}

		// Token: 0x06007271 RID: 29297 RVA: 0x002B4F30 File Offset: 0x002B3130
		public void StartAttempt()
		{
			this.Attempt++;
			long num = The.Game.TimeTicks - this.LeftTick;
			if (this.Attempt == 1 || num >= (long)base.GetProperty("ReinforcementDelay", 0))
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.GetSpawnTables())
				{
					if (keyValuePair.Value != null)
					{
						Zone zone = The.ZoneManager.GetZone(keyValuePair.Key);
						foreach (PopulationResult populationResult in PopulationManager.Generate(keyValuePair.Value, null, null, null, null, null))
						{
							this.PlacePopulation(zone, populationResult);
						}
						if (this.Attempt > 1)
						{
							this.RemoveBarathrumites(zone);
						}
						else if (!zone.IsCheckpoint())
						{
							this.DisplaceFauna(zone);
						}
						ZoneManager.ActivateObjects(zone);
					}
				}
			}
			if (this.Attempt > 1)
			{
				if (this.Stage == 0)
				{
					this.Timer += 500;
					return;
				}
				if (this.Stage == 1 && !base.Quest.IsStepFinished("Nephal"))
				{
					this.NephalOccupy();
					return;
				}
			}
			else
			{
				foreach (string text in this.Perimeter)
				{
					Zone zone2 = The.ZoneManager.GetZone(text);
					if (!zone2.IsCheckpoint())
					{
						this.DisplaceFlora(zone2);
					}
				}
			}
		}

		// Token: 0x06007272 RID: 29298 RVA: 0x002B50F4 File Offset: 0x002B32F4
		public void RemoveBarathrumites(Zone Zone)
		{
			foreach (GameObject gameObject in Zone.IterateObjects())
			{
				if (gameObject.IsCombatObject() && gameObject.HasIntProperty("BattleParticipant") && gameObject.IsFactionMember("Barathrumites") && !gameObject.IsPlayerControlled())
				{
					gameObject.RemoveFromContext(null);
					The.ActionManager.RemoveActiveObject(gameObject);
					gameObject.Pool();
				}
			}
		}

		// Token: 0x06007273 RID: 29299 RVA: 0x002B5168 File Offset: 0x002B3368
		public void DisplaceFlora(Zone Zone)
		{
			List<int> list = base.GetList<int>("DisplaceFloraChance");
			if (list[0].in100())
			{
				int num = list[1];
				foreach (GameObject gameObject in Zone.IterateObjects())
				{
					if (gameObject.Physics != null && gameObject.Render != null && (gameObject.Physics.Solid || gameObject.Render.Occluding) && !gameObject.IsCombatObject() && !gameObject.IsImportant() && gameObject.HasTagOrProperty("Plant") && num.in100())
					{
						gameObject.Destroy(null, true, false, null);
					}
				}
			}
		}

		// Token: 0x06007274 RID: 29300 RVA: 0x002B5220 File Offset: 0x002B3420
		public void DisplaceFauna(Zone Zone)
		{
			int property = base.GetProperty("DisplaceFaunaChance", 0);
			foreach (GameObject gameObject in Zone.IterateObjects())
			{
				if (gameObject.IsCombatObject() && !gameObject.IsImportant() && !gameObject.IsFactionMember("Barathrumites") && !gameObject.IsFactionMember("Templar") && !gameObject.IsPlayerControlled() && property.in100())
				{
					Cell currentCell = gameObject.CurrentCell;
					gameObject.RemoveFromContext(null);
					The.ActionManager.RemoveActiveObject(gameObject);
					this.Displaced.Add(gameObject.ID);
					The.ZoneManager.CacheObject(gameObject, false, false);
					Brain brain = gameObject.Brain;
					GlobalLocation globalLocation;
					if ((globalLocation = brain.StartingCell) == null)
					{
						globalLocation = (brain.StartingCell = new GlobalLocation());
					}
					globalLocation.SetCell(currentCell);
				}
			}
		}

		// Token: 0x06007275 RID: 29301 RVA: 0x002B530C File Offset: 0x002B350C
		public void RestoreFauna()
		{
			foreach (string text in this.Displaced)
			{
				GameObject value = The.ZoneManager.CachedObjects.GetValue(text, null);
				The.ZoneManager.CachedObjects.Remove(text);
				if (value != null)
				{
					Cell cell = value.Brain.StartingCell.ResolveCell();
					if (cell != null)
					{
						value.SystemMoveTo(cell, null, true, true, false, false);
					}
					else
					{
						value.Pool();
					}
				}
			}
			this.Displaced.Clear();
		}

		// Token: 0x06007276 RID: 29302 RVA: 0x002B53BC File Offset: 0x002B35BC
		public void PlacePopulation(Zone Zone, PopulationResult Result)
		{
			int property = base.GetProperty("ClusterChance", 0);
			int property2 = base.GetProperty("ClusterDistance", 0);
			int property3 = base.GetProperty("ClusterRadius", 4);
			int i = 0;
			while (i < Result.Number)
			{
				if (Result.Hint == "Warleader")
				{
					List<string> list = base.GetList("Warleaders");
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							GameObject value = The.ZoneManager.CachedObjects.GetValue(text, null);
							if (value != null && value.Blueprint == Result.Blueprint)
							{
								The.ZoneManager.CachedObjects.Remove(text);
								value.SetIntProperty("BattleParticipant", 1, false);
								this.PlaceClusteredObject(Zone, value, (property > 0) ? 100 : 0, property2, property3);
								list.Remove(text);
								if (list.Count == 0)
								{
									base.RemoveProperty("Warleaders");
									break;
								}
								break;
							}
						}
						goto IL_012D;
					}
					goto IL_0105;
				}
				goto IL_0105;
				IL_012D:
				i++;
				continue;
				IL_0105:
				GameObject gameObject = GameObject.Create(Result.Blueprint);
				gameObject.SetIntProperty("BattleParticipant", 1, false);
				this.PlaceClusteredObject(Zone, gameObject, property, property2, property3);
				goto IL_012D;
			}
		}

		// Token: 0x06007277 RID: 29303 RVA: 0x002B5518 File Offset: 0x002B3718
		public void PlaceClusteredObject(Zone Zone, GameObject Object, int Chance = 50, int Distance = 0, int Radius = 4)
		{
			Cell cell = null;
			if (Chance.in100())
			{
				if (this.CurrentZone != Zone)
				{
					Dictionary<string, List<Location2D>> clusters = this.Clusters;
					if (clusters != null)
					{
						clusters.Clear();
					}
					this.CurrentZone = Zone;
				}
				if (this.Clusters == null)
				{
					this.Clusters = new Dictionary<string, List<Location2D>>(8);
				}
				string primaryFaction = Object.GetPrimaryFaction(false);
				List<Location2D> list;
				if (!this.Clusters.TryGetValue(primaryFaction, out list))
				{
					list = new List<Location2D>(16);
					for (int i = 0; i < 100; i++)
					{
						cell = Zone.GetRandomCell(4);
						if (cell.IsEmpty() && cell.IsReachable() && this.GetClusterDistance(cell) > Distance)
						{
							break;
						}
					}
					foreach (Cell cell2 in cell.IterateAdjacent(Radius, true, true, true))
					{
						list.Add(cell2.Location);
					}
					this.Clusters[primaryFaction] = list;
				}
				for (int j = 0; j < list.Count; j++)
				{
					Location2D randomElement = list.GetRandomElement<Location2D>(null);
					cell = Zone.GetCell(randomElement);
					if (cell.IsReachable() && cell.IsEmptyFor(Object))
					{
						cell.AddObject(Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						return;
					}
				}
			}
			cell = Zone.GetRandomCell(0);
			if (!cell.IsEmptyFor(Object) || !cell.IsReachable())
			{
				foreach (Cell cell3 in cell.IterateAdjacent(10, false, true, true))
				{
					if (cell3.IsEmptyFor(Object) && cell3.IsReachable())
					{
						cell = cell3;
						break;
					}
				}
			}
			cell.AddObject(Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x06007278 RID: 29304 RVA: 0x002B56C0 File Offset: 0x002B38C0
		public int GetClusterDistance(Cell Cell)
		{
			int num = 9999999;
			foreach (List<Location2D> list in this.Clusters.Values)
			{
				if (!list.IsNullOrEmpty<Location2D>())
				{
					int num2 = Cell.PathDistanceTo(list[0]);
					if (num > num2)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		// Token: 0x06007279 RID: 29305 RVA: 0x002B5734 File Offset: 0x002B3934
		public void FailAttempt()
		{
			this.NextAttempt++;
			base.Game.PopupFailQuest(base.Quest);
			base.Quest.Name = base.Blueprint.Name + ", attempt #" + this.NextAttempt.ToString();
			base.Game.PopupStartQuest(base.Quest);
			The.ActionManager.RemoveExternalObjects();
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x002B57A8 File Offset: 0x002B39A8
		public void NephalOccupy()
		{
			Zone zone = The.ZoneManager.GetZone(base.GetProperty("NephalRetryZone", null));
			List<Zone> list = new List<Zone>(2);
			foreach (string text in this.Perimeter)
			{
				Zone zone2 = The.ZoneManager.GetZone(text);
				if (zone.ResolvedLocation.ManhattanDistance(zone2.ResolvedLocation) == 1)
				{
					list.Add(zone2);
				}
			}
			foreach (GameObject gameObject in zone.IterateObjects())
			{
				if (gameObject.IsCombatObject() && !gameObject.IsPlayerControlled())
				{
					Zone randomElement = list.GetRandomElement<Zone>(null);
					gameObject.CurrentCell.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					ZoneBuilderSandbox.PlaceObjectInArea(randomElement, randomElement.area, gameObject, 0, 0, null, null, false);
				}
			}
			Cell cell = this.NephalLocation.ResolveCell();
			GameObject firstObject = cell.GetFirstObject((GameObject x) => x.HasPart(typeof(OmonporchBattleNephilim)));
			cell.RemoveObject(firstObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			ZoneBuilderSandbox.PlaceObjectInArea(zone, zone.area, firstObject, 0, 0, null, null, false);
		}

		// Token: 0x0600727B RID: 29307 RVA: 0x002B5908 File Offset: 0x002B3B08
		public GameObject GetValidNephal(string Blueprint = null)
		{
			if (Blueprint.IsNullOrEmpty())
			{
				List<string> list = new List<string>(NephalProperties.Nephilim.Length);
				foreach (string text in NephalProperties.Nephilim)
				{
					if (!NephalProperties.IsFoiled(text))
					{
						list.Add(text);
					}
				}
				if (list.Count > 1)
				{
					list.Remove("Ehalcodon");
				}
				Blueprint = list.GetRandomElement<string>(null);
			}
			else
			{
				Blueprint = Grammar.ClosestMatch(NephalProperties.Nephilim, Blueprint, true);
			}
			if (Blueprint.IsNullOrEmpty())
			{
				return null;
			}
			return GameObject.Create(Blueprint);
		}

		// Token: 0x0600727C RID: 29308 RVA: 0x002B5990 File Offset: 0x002B3B90
		public void SpawnNephilim(string Blueprint = null)
		{
			if (!this.Active || this.Stage >= 1)
			{
				return;
			}
			this.Timer = 0;
			this.Stage = 1;
			string property = base.GetProperty("ArrivalSound", null);
			foreach (ReadOnlySpan<char> readOnlySpan in property.DelimitedBy(','))
			{
				SoundManager.PreloadClipSet(new string(readOnlySpan));
			}
			QuestStep questStep = base.Quest.StepsByID["Nephal"];
			GameObject validNephal = this.GetValidNephal(Blueprint);
			if (validNephal == null)
			{
				questStep.Optional = true;
				The.Game.CheckQuestFinishState(base.Quest);
				return;
			}
			if (validNephal.Blueprint == "Ehalcodon")
			{
				this.Super = true;
			}
			validNephal.RequirePart<OmonporchBattleNephilim>(false);
			validNephal.AddPart<FinishQuestStepWhenSlain>(new FinishQuestStepWhenSlain
			{
				Quest = "Reclamation",
				Step = "Nephal"
			}, true, false);
			questStep.Hidden = false;
			questStep.Text = questStep.Text.Replace("=name=", validNephal.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, false, false, null, false));
			questStep.Value = validNephal.Blueprint;
			string text = this.Perimeter.GetRandomElement<string>(null);
			while (text == "JoppaWorld.53.4.0.0.10")
			{
				text = this.Perimeter.GetRandomElement<string>(null);
			}
			Zone zone = The.ZoneManager.GetZone(text);
			ZoneBuilderSandbox.PlaceObjectInArea(zone, zone.area, validNephal, 0, 0, null, null, false);
			string text2 = The.Player.DescribeDirectionToward(validNephal, true, false);
			foreach (ReadOnlySpan<char> readOnlySpan2 in property.DelimitedBy(','))
			{
				SoundManager.PlaySound(new string(readOnlySpan2), 0f, 1f, 0.65f, SoundRequest.SoundEffectType.None, 0f);
			}
			if (CombatJuice.enabled)
			{
				CombatJuice.cameraShake(2f, false);
			}
			Popup.Show(base.GetProperty(this.Super ? "MessageArrivalSuper" : "MessageArrival", null).StartReplace().AddReplacer("name", validNephal.ShortDisplayName)
				.AddReplacer("direction", text2)
				.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x002B5BB5 File Offset: 0x002B3DB5
		[WishCommand("reclamation:start", null)]
		public static void WishStart()
		{
			The.Game.StartQuest("Reclamation", null, null, null);
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x002B5BCA File Offset: 0x002B3DCA
		[WishCommand("reclamation:nephal", null)]
		public static void WishNephal()
		{
			The.Game.GetSystem<ReclamationSystem>().SpawnNephilim(null);
		}

		// Token: 0x0600727F RID: 29311 RVA: 0x002B5BDC File Offset: 0x002B3DDC
		[WishCommand("reclamation:nephal", null)]
		public static void WishNephal(string Blueprint)
		{
			The.Game.GetSystem<ReclamationSystem>().SpawnNephilim(Blueprint);
		}

		// Token: 0x06007280 RID: 29312 RVA: 0x002B5BF0 File Offset: 0x002B3DF0
		[WishCommand("reclamation:timer", null)]
		public static void WishTimer()
		{
			int timer = The.Game.GetSystem<ReclamationSystem>().Timer;
			MessageQueue.AddPlayerMessage(string.Format("Turns until nephal arrives: {0}", timer), null, true);
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x002B5C24 File Offset: 0x002B3E24
		[WishCommand("reclamation:reinforce", null)]
		public static void WishReinforce()
		{
			ReclamationSystem system = The.Game.GetSystem<ReclamationSystem>();
			system.LeftTick = The.Game.TimeTicks - (long)system.GetProperty("ReinforcementDelay", 0);
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x002B5C5C File Offset: 0x002B3E5C
		[WishCommand("reclamation:heal", null)]
		public static void WishHeal()
		{
			ReclamationSystem system = The.Game.GetSystem<ReclamationSystem>();
			system.LeftTick = The.Game.TimeTicks - (long)system.GetProperty("HealDelay", 0);
		}

		// Token: 0x06007283 RID: 29315 RVA: 0x002B5C94 File Offset: 0x002B3E94
		[WishCommand("reclamation:kill", null)]
		public static void WishKill()
		{
			Zone[] array = The.ZoneManager.CachedZones.Values.ToArray<Zone>();
			for (int i = 0; i < array.Length; i++)
			{
				foreach (GameObject gameObject in array[i].IterateObjects())
				{
					FinishQuestStepWhenSlain finishQuestStepWhenSlain;
					if (gameObject.TryGetPartDescendedFrom<FinishQuestStepWhenSlain>(out finishQuestStepWhenSlain) && finishQuestStepWhenSlain.Quest == "Reclamation")
					{
						gameObject.Die(The.Player, null, null, null, false, null, null, false, false, null, null, null);
					}
				}
			}
		}

		// Token: 0x06007284 RID: 29316 RVA: 0x002B5D20 File Offset: 0x002B3F20
		[WishCommand("startehalcodon", null)]
		public static void WishTestEhalcodon()
		{
			foreach (string text in NephalProperties.Nephilim)
			{
				if (!(text == "Ehalcodon"))
				{
					GameObjectBlueprint gameObjectBlueprint;
					if (GameObjectFactory.Factory.Blueprints.TryGetValue("LightCircle" + text, out gameObjectBlueprint))
					{
						The.Player.ReceiveObject(gameObjectBlueprint.createOne(), false, null);
					}
					The.Game.TryAddDelimitedGameState(text, ',', "Pacified");
				}
			}
			ReclamationSystem.WishTestBattle();
		}

		// Token: 0x06007285 RID: 29317 RVA: 0x002B5D9C File Offset: 0x002B3F9C
		public static void WishStage()
		{
			GameObject player = The.Player;
			XRLGame game = The.Game;
			game.CompleteQuest("A Canticle for Barathrum");
			game.CompleteQuest("Decoding the Signal");
			game.CompleteQuest("More Than a Willing Spirit");
			game.CompleteQuest("The Earl of Omonporch");
			game.SetIntGameState("ForcePostEarlSpawn", 1);
			game.CompleteQuest("A Call to Arms");
			game.CompleteQuest("Pax Klanq, I Presume?");
			GritGateScripts.PromoteToJourneyfriend();
			GritGateScripts.OpenRank2Doors();
			int num = Leveler.GetXPForLevel(37) - player.GetStatValue("XP", 0);
			if (num > 0)
			{
				player.AwardXP(num, -1, 0, int.MaxValue, null, null, null, null, null, null);
			}
			if (!game.GetBooleanGameState("Recame", false))
			{
				BodyPart body = player.Body.GetBody();
				string text = "Floating Nearby";
				int num2 = 0;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				string text5 = null;
				string text6 = null;
				bool? flag = new bool?(true);
				body.AddPart(text, num2, text2, text3, text4, text5, text6, null, null, null, null, null, null, null, null, flag, null, null, null, null, true);
				game.SetBooleanGameState("Recame", true);
			}
		}

		// Token: 0x06007286 RID: 29318 RVA: 0x002B5EF8 File Offset: 0x002B40F8
		[WishCommand("startbattle", null)]
		public static void WishTestBattle()
		{
			Popup.Suppress = true;
			ItemNaming.Suppress = true;
			GameObject player = The.Player;
			XRLGame game = The.Game;
			ReclamationSystem.WishStage();
			AscensionSystem.WishSetBarathrumState();
			if (!game.GetBooleanGameState("Recame", false))
			{
				BodyPart body = player.Body.GetBody();
				string text = "Floating Nearby";
				int num = 0;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				string text5 = null;
				string text6 = null;
				bool? flag = new bool?(true);
				body.AddPart(text, num, text2, text3, text4, text5, text6, null, null, null, null, null, null, null, null, flag, null, null, null, null, true);
				game.SetBooleanGameState("Recame", true);
			}
			Zone zone = The.ZoneManager.GetZone("JoppaWorld.53.4.1.1.10");
			Cell cell = zone.GetCell(26, 16);
			Cell cell2 = zone.GetCell(40, 16);
			player.SystemMoveTo(cell, null, true, true, false, false);
			GameObject gameObject = zone.FastFloodFindFirstBlueprint(40, 16, 10, "Adiyy", player, false, false);
			if (gameObject != null)
			{
				gameObject.Destroy(null, false, false, null);
			}
			GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("Chair");
			if (firstObjectWithPart != null)
			{
				firstObjectWithPart.GetPart<Chair>().SitDown(player, null);
			}
			if (!zone.HasObject("Barathrum"))
			{
				cell.getClosestPassableCell().AddObject("Barathrum", null, null, null, null);
			}
			if (!game.HasFinishedQuest("The Golem"))
			{
				try
				{
					GolemQuestSelection.PlaceFinalMound(cell2);
					game.CompleteQuest("The Golem");
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("OmonporchBattle:Start", ex, "game_exception");
				}
			}
			game.StartQuest("We Are Starfreight", null, null, null);
			Popup.Suppress = false;
			ItemNaming.Suppress = false;
			if (!game.HasQuest("Reclamation"))
			{
				MapChunkPlacement.PlaceFromFile(zone, "preset_tile_chunks/HamilcrabShop.rpm", 45, 2, 6, 7, 0, 1, null, null, null);
				GameObject gameObject2 = GameObject.Create("DromadTrader8", 0, 0, null, delegate(GameObject x)
				{
					x.RemovePart(typeof(DromadCaravan));
				}, null, null, null);
				zone.GetCell(49, 5).getClosestEmptyCell().AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				game.StartQuest("Reclamation", null, null, null);
				GameObject gameObject3 = zone.GetCell(39, 12).AddObject("RelicChest", null, null, null, null);
				for (int i = 1; i <= 8; i++)
				{
					gameObject3.ReceiveObject(RelicGenerator.GenerateRelic(i, true), false, null);
				}
				zone.GetCell(40, 12).AddObject("RelicChest", null, null, null, null).ReceivePopulation("FinalSupply");
			}
		}

		// Token: 0x0400351C RID: 13596
		public GlobalLocation NephalLocation = new GlobalLocation();

		// Token: 0x0400351D RID: 13597
		public List<string> Displaced = new List<string>(16);

		// Token: 0x0400351E RID: 13598
		public long LeftTick = long.MaxValue;

		// Token: 0x0400351F RID: 13599
		public int NextAttempt;

		// Token: 0x04003520 RID: 13600
		public int Attempt;

		// Token: 0x04003521 RID: 13601
		public int Stage;

		// Token: 0x04003522 RID: 13602
		public int Timer;

		// Token: 0x04003523 RID: 13603
		public bool Active;

		// Token: 0x04003524 RID: 13604
		[NonSerialized]
		private int RechargeTimer;

		// Token: 0x04003525 RID: 13605
		[NonSerialized]
		private HashSet<string> _Perimeter;

		// Token: 0x04003526 RID: 13606
		[NonSerialized]
		private Zone CurrentZone;

		// Token: 0x04003527 RID: 13607
		[NonSerialized]
		private Dictionary<string, List<Location2D>> Clusters;
	}
}
