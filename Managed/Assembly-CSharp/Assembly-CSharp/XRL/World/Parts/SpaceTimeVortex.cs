using System;
using System.Collections.Generic;
using Genkit;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200128A RID: 4746
	[Serializable]
	public class SpaceTimeVortex : IPart
	{
		// Token: 0x0600BBB7 RID: 48055 RVA: 0x00448A56 File Offset: 0x00446C56
		public override void Initialize()
		{
			this.InitialTurn = true;
		}

		// Token: 0x0600BBB8 RID: 48056 RVA: 0x00448A5F File Offset: 0x00446C5F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BBB9 RID: 48057 RVA: 0x00448A64 File Offset: 0x00446C64
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeingConsumedEvent>.ID || ID == PooledEvent<BlocksRadarEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<InterruptAutowalkEvent>.ID || ID == ObjectEnteredCellEvent.ID || ID == PooledEvent<RealityStabilizeEvent>.ID;
		}

		// Token: 0x0600BBBA RID: 48058 RVA: 0x00448AC5 File Offset: 0x00446CC5
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600BBBB RID: 48059 RVA: 0x00448AD0 File Offset: 0x00446CD0
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BBBC RID: 48060 RVA: 0x00448AEC File Offset: 0x00446CEC
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (E.Check(true))
			{
				this.ParentObject.ParticleBlip("&K-", 10, 0L, false);
				base.DidX("collapse", "under the pressure of normality", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.Destroy(null, false, false, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBBD RID: 48061 RVA: 0x00448B54 File Offset: 0x00446D54
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.ApplyVortex(E.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBBE RID: 48062 RVA: 0x00448B6A File Offset: 0x00446D6A
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			E.IndicateObject = this.ParentObject;
			return false;
		}

		// Token: 0x0600BBBF RID: 48063 RVA: 0x00448B7C File Offset: 0x00446D7C
		public void RequireDestinationZone()
		{
			if (this.DestinationZoneID.IsNullOrEmpty())
			{
				Zone currentZone = this.ParentObject.CurrentZone;
				string text = ((currentZone != null) ? currentZone.GetZoneWorld() : null);
				this.DestinationZoneID = SpaceTimeVortex.GetRandomDestinationZoneID(text, true);
				if (this.Queue != null && this.Locations != null)
				{
					if (!this.DestinationZoneID.IsNullOrEmpty())
					{
						using (List<GameObject>.Enumerator enumerator = this.Queue.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GameObject gameObject = enumerator.Current;
								try
								{
									if (GameObject.Validate(gameObject) && !gameObject.HasContext())
									{
										Location2D location2D = this.Locations[gameObject];
										Cell destinationCellFor = SpaceTimeVortex.GetDestinationCellFor(this.DestinationZoneID, gameObject, location2D);
										if (destinationCellFor != null)
										{
											SpaceTimeVortex.Teleport(gameObject, destinationCellFor, this.ParentObject, (currentZone != null) ? currentZone.GetCell(location2D) : null, false);
											gameObject.MakeActive(false);
										}
										else
										{
											gameObject.Obliterate(null, false, null);
										}
									}
								}
								catch (Exception ex)
								{
									MetricsManager.LogException("Spacetime vortex queue unload", ex, "game_exception");
								}
							}
							goto IL_0158;
						}
					}
					foreach (GameObject gameObject2 in this.Queue)
					{
						if (GameObject.Validate(gameObject2) && !gameObject2.HasContext())
						{
							gameObject2.Obliterate(null, false, null);
						}
					}
					IL_0158:
					this.Queue = null;
					this.Locations = null;
				}
			}
		}

		// Token: 0x0600BBC0 RID: 48064 RVA: 0x00448D18 File Offset: 0x00446F18
		public static Cell GetDestinationCellFor(string ZoneID, GameObject Target, Cell Origin = null)
		{
			if (ZoneID != null)
			{
				Zone zone = The.ZoneManager.GetZone(ZoneID);
				if (zone != null)
				{
					Cell cell = Origin ?? Target.CurrentCell;
					Cell cell2 = zone.GetCell(cell.X, cell.Y);
					if (cell2 != null && !cell2.IsPassable(Target, true))
					{
						cell2 = cell2.getClosestPassableCellFor(Target);
					}
					return cell2;
				}
			}
			return Target.GetRandomTeleportTargetCell(0);
		}

		// Token: 0x0600BBC1 RID: 48065 RVA: 0x00448D74 File Offset: 0x00446F74
		public static Cell GetDestinationCellFor(string ZoneID, GameObject Target, Location2D Origin)
		{
			if (ZoneID != null && Origin != null && GameObject.Validate(ref Target))
			{
				Zone zone = The.ZoneManager.GetZone(ZoneID);
				if (zone != null)
				{
					Cell cell = zone.GetCell(Origin);
					Cell cell2 = zone.GetCell(cell.X, cell.Y);
					if (cell2 != null && !cell2.IsPassable(Target, true))
					{
						cell2 = cell2.getClosestPassableCellFor(Target);
					}
					return cell2;
				}
			}
			return Target.GetRandomTeleportTargetCell(0);
		}

		// Token: 0x0600BBC2 RID: 48066 RVA: 0x00448DE0 File Offset: 0x00446FE0
		public override bool HandleEvent(EnteredCellEvent E)
		{
			foreach (GameObject gameObject in E.Cell.GetObjectsWithPartReadonly("Render"))
			{
				if (!this.ParentObject.IsValid())
				{
					break;
				}
				if (this.ParentObject.CurrentCell != E.Cell)
				{
					break;
				}
				this.ApplyVortex(gameObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBC3 RID: 48067 RVA: 0x00448E68 File Offset: 0x00447068
		public override bool HandleEvent(BeingConsumedEvent E)
		{
			this.ApplyVortex(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBC4 RID: 48068 RVA: 0x00448E7E File Offset: 0x0044707E
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.SpaceTimeAnomalyPeriodicEvents();
			this.InitialTurn = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBC5 RID: 48069 RVA: 0x00448E94 File Offset: 0x00447094
		public override bool HandleEvent(BlocksRadarEvent E)
		{
			return false;
		}

		// Token: 0x0600BBC6 RID: 48070 RVA: 0x00448E97 File Offset: 0x00447097
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BBC7 RID: 48071 RVA: 0x00448E9A File Offset: 0x0044709A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DefendMeleeHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BBC8 RID: 48072 RVA: 0x00448EAF File Offset: 0x004470AF
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DefendMeleeHit")
			{
				this.ApplyVortex(E.GetGameObjectParameter("Attacker"));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BBC9 RID: 48073 RVA: 0x00448EDC File Offset: 0x004470DC
		public override bool Render(RenderEvent E)
		{
			if (Stat.RandomCosmetic(1, 60) < 3 * (Options.ThrottleAnimationRate / 20))
			{
				string text = "&C";
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&W";
				}
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&R";
				}
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					text = "&B";
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				The.ParticleManager.AddRadial(text + "ù", (float)currentCell.X, (float)currentCell.Y, (float)Stat.RandomCosmetic(0, 7), (float)Stat.RandomCosmetic(5, 10), 0.01f * (float)Stat.RandomCosmetic(4, 6), -0.01f * (float)Stat.RandomCosmetic(3, 7));
			}
			int num = Stat.RandomCosmetic(0, 4);
			if (num == 0)
			{
				E.ColorString = "&B^k";
			}
			else if (num == 1)
			{
				E.ColorString = "&R^k";
			}
			else if (num == 2)
			{
				E.ColorString = "&C^k";
			}
			else if (num == 3)
			{
				E.ColorString = "&W^k";
			}
			else if (num == 4)
			{
				E.ColorString = "&K^k";
			}
			int num2 = Stat.RandomCosmetic(0, 3);
			if (num2 == 0)
			{
				E.RenderString = "\t";
			}
			else if (num2 == 1)
			{
				E.RenderString = "é";
			}
			else if (num2 == 2)
			{
				E.RenderString = "\u0015";
			}
			else if (num2 == 3)
			{
				E.RenderString = "\u000f";
			}
			return true;
		}

		// Token: 0x0600BBCA RID: 48074 RVA: 0x00449038 File Offset: 0x00447238
		public static string GetRandomDestinationZoneID(string World, bool Validate = true)
		{
			if (World != "JoppaWorld")
			{
				return null;
			}
			string text;
			for (;;)
			{
				int num = Stat.Random(0, 79);
				int num2 = Stat.Random(0, 24);
				int num3 = Stat.Random(0, 2);
				int num4 = Stat.Random(0, 2);
				int num5 = (50.in100() ? Stat.Random(10, 40) : 10);
				text = ZoneID.Assemble(World, num, num2, num3, num4, num5);
				if (!Validate)
				{
					break;
				}
				Zone zone = The.ZoneManager.GetZone(text);
				if (zone.GetEmptyCellCount() >= 100)
				{
					Cell cell = null;
					int num6 = 0;
					while (++num6 < 5)
					{
						Cell randomCell = zone.GetRandomCell(3 - num6 / 25);
						if (randomCell.IsReachable() && !randomCell.IsSolid())
						{
							cell = randomCell;
							break;
						}
					}
					if (cell != null)
					{
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x0600BBCB RID: 48075 RVA: 0x004490FC File Offset: 0x004472FC
		public static bool IsValidTarget(GameObject Object)
		{
			return GameObject.Validate(ref Object) && ((Object != null) ? Object.Render : null) != null && Object.IsReal && !Object.IsScenery && Object.GetPhase() != 4 && !Object.HasTagOrProperty("ExcavatoryTerrainFeature") && !Object.HasTagOrProperty("IgnoreSpaceTimeVortex");
		}

		// Token: 0x0600BBCC RID: 48076 RVA: 0x00449158 File Offset: 0x00447358
		public static bool Teleport(GameObject Object, Cell C, GameObject Device, Cell FromCell = null, bool VisualEffects = true)
		{
			if (Object.CurrentZone != C.ParentZone)
			{
				if (Object.Brain != null && !Object.IsPlayerLed())
				{
					Lost lost;
					if (Object.TryGetEffect<Lost>(out lost))
					{
						lost.DisableUnlost = false;
					}
					else if (C.ParentZone.Z == 10)
					{
						Object.ApplyEffect(new Lost(), null);
					}
				}
				if (!Object.IsPlayer() && !Object.WasPlayer())
				{
					if (Object.PartyLeader != null && !Object.HasEffect<Incommunicado>())
					{
						Object.ApplyEffect(new Incommunicado(), null);
					}
					Brain brain = Object.Brain;
					if (brain != null)
					{
						brain.Goals.Clear();
					}
				}
			}
			Object.PlayWorldSound("Sounds/Abilities/sfx_ability_spacetimeVortex_interact_in", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			bool flag = Object.CellTeleport(C, null, Device, null, null, null, new int?(1000), true, VisualEffects, !Object.IsPlayer(), true, null, "appear", FromCell);
			Object.PlayWorldSound("Sounds/Abilities/sfx_ability_spacetimeVortex_interact_out", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (Object.IsPlayer())
			{
				Achievement.VORTICES_ENTERED.Progress.Increment(1);
			}
			return flag;
		}

		// Token: 0x0600BBCD RID: 48077 RVA: 0x00449290 File Offset: 0x00447490
		public bool ApplyVortex(GameObject GO)
		{
			if (this.ParentObject == GO || !SpaceTimeVortex.IsValidTarget(GO))
			{
				return false;
			}
			if (GO.HasPartDescendedFrom<SpaceTimeVortex>())
			{
				bool flag = IComponent<GameObject>.Visible(this.ParentObject) || IComponent<GameObject>.Visible(GO);
				string displayName = this.ParentObject.DisplayName;
				string displayName2 = GO.DisplayName;
				if (displayName == displayName2)
				{
					base.EmitMessage("Two " + displayName.Pluralize() + " come into contact and both explode!", ' ', false, flag, false, null, null);
				}
				else
				{
					base.DidXToY("come", "into contact with", GO, "and both explode", "!", null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, flag, null);
				}
				GO.Obliterate(null, false, null);
				this.ParentObject.Explode(3000, null, "1d200", 1f, true, false, false, 0, null);
				return true;
			}
			if (this.DestinationZoneID == null && !SpaceTimeVortex.ObjectCallsForExplicitTracking(GO))
			{
				IComponent<GameObject>.XDidYToZ(GO, "are", "sucked into", this.ParentObject, null, "!", null, null, null, GO, false, false, true, false, false, null, null, null, true, false, false, false, false, null);
				Cell currentCell = GO.CurrentCell;
				Location2D location2D = ((currentCell != null) ? currentCell.Location : null);
				if (location2D != null)
				{
					if (this.Queue == null)
					{
						this.Queue = new List<GameObject>();
					}
					if (this.Locations == null)
					{
						this.Locations = new Dictionary<GameObject, Location2D>();
					}
					this.Queue.Add(GO);
					this.Locations[GO] = location2D;
					GO.RemoveFromContext(null);
					GO.MakeInactive();
				}
				else
				{
					GO.Obliterate(null, false, null);
				}
			}
			else
			{
				this.RequireDestinationZone();
				Cell destinationCellFor = SpaceTimeVortex.GetDestinationCellFor(this.DestinationZoneID, GO, this.ParentObject.CurrentCell);
				if (destinationCellFor == null || !GO.FireEvent(Event.New("SpaceTimeVortexContact", "Object", this.ParentObject, "DestinationCell", destinationCellFor)))
				{
					return false;
				}
				if (GO.IsPlayerLed() && !GO.IsTrifling)
				{
					Popup.Show(string.Concat(new string[]
					{
						"Your companion, ",
						GO.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, true, false, null, false, false, false, false, null, false, null, false, true),
						",",
						GO.GetVerb("have", true, false, false, true, false),
						" been sucked into ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" ",
						The.Player.DescribeDirectionToward(this.ParentObject, false, false),
						"!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					IComponent<GameObject>.XDidYToZ(GO, "are", "sucked into", this.ParentObject, null, "!", null, null, null, GO, false, false, true, false, false, null, null, null, true, false, false, false, false, null);
				}
				SpaceTimeVortex.Teleport(GO, destinationCellFor, this.ParentObject, null, true);
			}
			return true;
		}

		// Token: 0x0600BBCE RID: 48078 RVA: 0x00449597 File Offset: 0x00447797
		public static bool IsBlueprintSummonable(GameObjectBlueprint BP)
		{
			return EncountersAPI.IsEligibleForDynamicEncounters(BP) && BP.HasPart("Brain");
		}

		// Token: 0x0600BBCF RID: 48079 RVA: 0x004495B4 File Offset: 0x004477B4
		public static List<string> GetSummonableBlueprints()
		{
			if (SpaceTimeVortex.Summonable == null)
			{
				SpaceTimeVortex.Summonable = new List<string>(128);
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (SpaceTimeVortex.IsBlueprintSummonable(gameObjectBlueprint))
					{
						SpaceTimeVortex.Summonable.Add(gameObjectBlueprint.Name);
					}
				}
			}
			return SpaceTimeVortex.Summonable;
		}

		// Token: 0x0600BBD0 RID: 48080 RVA: 0x00449638 File Offset: 0x00447838
		public static string GetSummonableBlueprint()
		{
			return SpaceTimeVortex.GetSummonableBlueprints().GetRandomElement<string>(null);
		}

		// Token: 0x0600BBD1 RID: 48081 RVA: 0x00449645 File Offset: 0x00447845
		public virtual int SpaceTimeAnomalyEmergencePermillageBaseChance()
		{
			return 5;
		}

		// Token: 0x0600BBD2 RID: 48082 RVA: 0x00449648 File Offset: 0x00447848
		public virtual int SpaceTimeAnomalyEmergenceExplodePercentageBaseChance()
		{
			return 0;
		}

		// Token: 0x0600BBD3 RID: 48083 RVA: 0x0044964B File Offset: 0x0044784B
		public virtual bool SpaceTimeAnomalyStationary()
		{
			return false;
		}

		// Token: 0x0600BBD4 RID: 48084 RVA: 0x0044964E File Offset: 0x0044784E
		public int SpaceTimeAnomalyEmergencePermillageChance()
		{
			return GetSpaceTimeAnomalyEmergencePermillageChanceEvent.GetFor(this.ParentObject, this.SpaceTimeAnomalyEmergencePermillageBaseChance());
		}

		// Token: 0x0600BBD5 RID: 48085 RVA: 0x00449661 File Offset: 0x00447861
		public int SpaceTimeAnomalyEmergenceExplodePercentageChance()
		{
			return GetSpaceTimeAnomalyEmergenceExplodePercentageChanceEvent.GetFor(this.ParentObject, this.SpaceTimeAnomalyEmergenceExplodePercentageBaseChance());
		}

		// Token: 0x0600BBD6 RID: 48086 RVA: 0x00449674 File Offset: 0x00447874
		public void SpaceTimeAnomalyPeriodicEvents()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			if (this.Delay > 0)
			{
				this.Delay--;
				return;
			}
			bool flag = this.InitialTurn || this.SpaceTimeAnomalyStationary();
			List<Cell> list = (flag ? null : currentCell.GetAdjacentCells(true));
			if (!flag)
			{
				currentCell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			}
			if (this.SpaceTimeAnomalyEmergencePermillageChance().in1000())
			{
				if (list == null)
				{
					list = currentCell.GetLocalEmptyAdjacentCells();
				}
				Cell cell = (flag ? list.GetRandomElement<Cell>(null) : currentCell);
				if (cell != null)
				{
					GameObject gameObject = GameObject.Create(SpaceTimeVortex.GetSummonableBlueprint());
					if (gameObject != null)
					{
						cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject.MakeActive(false);
						string text = (gameObject.IsMobile() ? "climb" : "fall");
						GameObject gameObject2 = gameObject;
						string text2 = text;
						string text3 = "through";
						GameObject parentObject = this.ParentObject;
						GameObject thePlayer = IComponent<GameObject>.ThePlayer;
						IComponent<GameObject>.XDidYToZ(gameObject2, text2, text3, parentObject, (thePlayer != null) ? thePlayer.DescribeDirectionToward(this.ParentObject, false, false) : null, "!", null, null, gameObject, null, false, true, false, false, false, null, null, null, false, false, false, false, false, null);
						gameObject.PlayWorldSound("Sounds/Abilities/sfx_ability_spacetimeVortex_interact_out", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						if (this.SpaceTimeAnomalyEmergenceExplodePercentageChance().in100())
						{
							base.DidX("destabilize", null, "!", null, null, null, IComponent<GameObject>.ThePlayer, false, false, null, null, false, false, false, false, false, null);
							this.ParentObject.Explode(3000, null, "1d200", 1f, true, false, false, 0, null);
						}
					}
				}
			}
			if (!flag && list != null)
			{
				Cell randomElement = list.GetRandomElement<Cell>(null);
				if (randomElement == null)
				{
					return;
				}
				randomElement.AddObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x0600BBD7 RID: 48087 RVA: 0x0044984D File Offset: 0x00447A4D
		public static bool ObjectCallsForExplicitTracking(GameObject Object)
		{
			return Object.IsPlayer() || (Object.IsPlayerLed() && !Object.IsTrifling) || Object.IsImportant() || Object.IsInteresting();
		}

		// Token: 0x04004B44 RID: 19268
		public string DestinationZoneID;

		// Token: 0x04004B45 RID: 19269
		public int Delay;

		// Token: 0x04004B46 RID: 19270
		[NonSerialized]
		private List<GameObject> Queue;

		// Token: 0x04004B47 RID: 19271
		[NonSerialized]
		private Dictionary<GameObject, Location2D> Locations;

		// Token: 0x04004B48 RID: 19272
		[NonSerialized]
		private bool InitialTurn;

		// Token: 0x04004B49 RID: 19273
		[NonSerialized]
		private static List<string> Summonable;
	}
}
