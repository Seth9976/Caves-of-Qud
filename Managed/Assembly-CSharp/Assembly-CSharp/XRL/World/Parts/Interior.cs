using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.EditorFormats.Map;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x02000F71 RID: 3953
	[Serializable]
	public class Interior : IPart
	{
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06009D3D RID: 40253 RVA: 0x0038F3E7 File Offset: 0x0038D5E7
		public bool IsZoneLive
		{
			get
			{
				return The.ZoneManager.IsZoneLive(this.ZoneID);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06009D3E RID: 40254 RVA: 0x0038F3F9 File Offset: 0x0038D5F9
		// (set) Token: 0x06009D3F RID: 40255 RVA: 0x0038F407 File Offset: 0x0038D607
		public bool Unique
		{
			get
			{
				return this.Flags.HasBit(1);
			}
			set
			{
				(ref this.Flags).SetBit(1, value);
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06009D40 RID: 40256 RVA: 0x0038F416 File Offset: 0x0038D616
		// (set) Token: 0x06009D41 RID: 40257 RVA: 0x0038F424 File Offset: 0x0038D624
		public bool IgnoreWeight
		{
			get
			{
				return this.Flags.HasBit(2);
			}
			set
			{
				(ref this.Flags).SetBit(2, value);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06009D42 RID: 40258 RVA: 0x0038F433 File Offset: 0x0038D633
		public InteriorZone Zone
		{
			get
			{
				return this.GetZone(false, false);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06009D43 RID: 40259 RVA: 0x0038F440 File Offset: 0x0038D640
		public string ZoneID
		{
			get
			{
				if (this._ZoneID == null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder("Interior");
					if (!this.Cell.IsNullOrEmpty())
					{
						stringBuilder.Compound(this.Cell, '@');
					}
					if (this.Unique)
					{
						stringBuilder.Compound(this.ParentObject.ID, '@');
					}
					stringBuilder.Compound(this.WX, '.');
					stringBuilder.Compound(this.WY, '.');
					stringBuilder.Compound(this.X, '.');
					stringBuilder.Compound(this.Y, '.');
					stringBuilder.Compound(this.Z, '.');
					this._ZoneID = stringBuilder.ToString();
				}
				return this._ZoneID;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06009D44 RID: 40260 RVA: 0x0038F4F8 File Offset: 0x0038D6F8
		public MapBuilder Builder
		{
			get
			{
				if (this._Builder == null)
				{
					ZoneBuilderBlueprint zoneBuilderBlueprint = The.ZoneManager.GetZoneBlueprint(this.ZoneID).Builders.FirstOrDefault<ZoneBuilderBlueprint>((ZoneBuilderBlueprint x) => x.Class == "MapBuilder");
					this._Builder = ((zoneBuilderBlueprint != null) ? zoneBuilderBlueprint.Create() : null) as MapBuilder;
				}
				return this._Builder;
			}
		}

		// Token: 0x06009D45 RID: 40261 RVA: 0x0038F564 File Offset: 0x0038D764
		public InteriorZone GetZone(bool LiveOnly = false, bool BuiltOnly = false)
		{
			if (!this.Collapsed)
			{
				InteriorZone zone = this._Zone;
				if (zone == null || zone.Stale)
				{
					this._Zone = null;
					string zoneID = this.ZoneID;
					ZoneManager zoneManager = The.ZoneManager;
					if (LiveOnly)
					{
						Zone zone2;
						if (zoneManager.CachedZones.TryGetValue(zoneID, out zone2))
						{
							this._Zone = zone2 as InteriorZone;
						}
					}
					else if (BuiltOnly)
					{
						if (zoneManager.IsZoneBuilt(zoneID))
						{
							this._Zone = zoneManager.GetZone(zoneID) as InteriorZone;
						}
					}
					else
					{
						this._Zone = zoneManager.GetZone(zoneID) as InteriorZone;
						if (this._Zone != null && this._Zone.Location.IsClear())
						{
							this._Zone.ParentObject = this.ParentObject;
						}
					}
					return this._Zone;
				}
			}
			return this._Zone;
		}

		// Token: 0x06009D46 RID: 40262 RVA: 0x0038F62A File Offset: 0x0038D82A
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009D47 RID: 40263 RVA: 0x0038F630 File Offset: 0x0038D830
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID || ID == InventoryActionEvent.ID || ID == PooledEvent<GetTransitiveLocationEvent>.ID || ID == GetZoneSuspendabilityEvent.ID || ID == GetZoneFreezabilityEvent.ID || ID == GetInventoryActionsEvent.ID || ID == GetCarriedWeightEvent.ID || ID == GetExtrinsicWeightEvent.ID || (ID == SingletonEvent<FlushWeightCacheEvent>.ID && this.WeightCacheTurn != -1L) || ID == PooledEvent<JoinPartyLeaderPossibleEvent>.ID || ID == TookDamageEvent.ID || ID == AfterDieEvent.ID || ID == BeforeDestroyObjectEvent.ID || ID == PooledEvent<IsVehicleOperationalEvent>.ID || ID == PooledEvent<IsRepairableEvent>.ID || ID == PooledEvent<RepairedEvent>.ID || ID == EnteringZoneEvent.ID || ID == EnteringCellEvent.ID || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID;
		}

		// Token: 0x06009D48 RID: 40264 RVA: 0x0038F705 File Offset: 0x0038D905
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06009D49 RID: 40265 RVA: 0x0038F71D File Offset: 0x0038D91D
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				this.ParentObject.Twiddle(false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D4A RID: 40266 RVA: 0x0038F744 File Offset: 0x0038D944
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.CurrentCell != null && E.Actor != this.ParentObject)
			{
				E.AddAction("Enter", "enter", "EnterInterior", null, 'e', false, 15, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D4B RID: 40267 RVA: 0x0038F797 File Offset: 0x0038D997
		public override bool HandleEvent(GetCarriedWeightEvent E)
		{
			E.Weight += (double)this.GetCarriedWeight();
			return base.HandleEvent(E);
		}

		// Token: 0x06009D4C RID: 40268 RVA: 0x0038F7B4 File Offset: 0x0038D9B4
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			E.Weight += (double)this.GetCarriedWeight();
			return base.HandleEvent(E);
		}

		// Token: 0x06009D4D RID: 40269 RVA: 0x0038F7D1 File Offset: 0x0038D9D1
		public override bool HandleEvent(FlushWeightCacheEvent E)
		{
			this.FlushWeightCache();
			return base.HandleEvent(E);
		}

		// Token: 0x06009D4E RID: 40270 RVA: 0x0038F7E0 File Offset: 0x0038D9E0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "EnterInterior")
			{
				this.TryEnter(E.Actor, false, true);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D4F RID: 40271 RVA: 0x0038F810 File Offset: 0x0038DA10
		public override bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			if (E.TargetCell.ParentZone.ZoneID == this.ZoneID)
			{
				return E.Result = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D50 RID: 40272 RVA: 0x0038F84C File Offset: 0x0038DA4C
		public override bool HandleEvent(IsVehicleOperationalEvent E)
		{
			return (E.Object != this.ParentObject || this.Validate()) && base.HandleEvent(E);
		}

		// Token: 0x06009D51 RID: 40273 RVA: 0x0038F870 File Offset: 0x0038DA70
		public override bool HandleEvent(EnteringZoneEvent E)
		{
			if (E.Cell.ParentZone is InteriorZone && XRL.World.ZoneID.Match(E.Cell.ParentZone.ZoneID, this.ZoneID) >= 0)
			{
				E.RequestInterfaceExit();
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D52 RID: 40274 RVA: 0x0038F8BC File Offset: 0x0038DABC
		public override bool HandleEvent(EnteringCellEvent E)
		{
			InteriorZone zone = this.GetZone(true, false);
			if (zone != null)
			{
				zone.Location.SetCell(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D53 RID: 40275 RVA: 0x0038F8F0 File Offset: 0x0038DAF0
		public int GetCarriedWeight()
		{
			if (this.IgnoreWeight)
			{
				return this.CarriedWeight = 0;
			}
			if (this.WeightCacheTurn >= 0L || !this.IsZoneLive)
			{
				return this.CarriedWeight;
			}
			MapBuilder builder = this.Builder;
			MapBuilder builder2 = this.Builder;
			if (((builder2 != null) ? builder2.Map : null) == null)
			{
				return this.CarriedWeight;
			}
			InteriorZone zone = this.Zone;
			int num = builder.X;
			int num2 = builder.Y;
			int width = builder.Width;
			int height = builder.Height;
			if (num == -1)
			{
				num = zone.Width / 2 - width / 2;
			}
			if (num2 == -1)
			{
				num2 = zone.Height / 2 - height / 2;
			}
			this.CarriedWeight = 0;
			this.WeightCacheTurn = The.Game.Turns;
			List<int> list = null;
			Vehicle part = this.ParentObject.GetPart<Vehicle>();
			int num3 = ((part != null) ? part.Passengers : 0);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Cell cell = zone.GetCell(num + j, num2 + i);
					if (cell != null)
					{
						foreach (GameObject gameObject in cell.Objects)
						{
							if (gameObject.IsReal && !gameObject.HasIntProperty("InteriorRequired") && !gameObject.HasTag("Cosmetic"))
							{
								int num4 = gameObject.Weight;
								if (num3 > 0 && gameObject.IsCombatObject())
								{
									int num5 = gameObject.IntrinsicWeight + Math.Min(gameObject.GetCarriedWeight(), gameObject.GetMaxCarriedWeight());
									if (list == null)
									{
										list = new List<int>();
									}
									list.Add(num5);
									num4 -= num5;
								}
								this.CarriedWeight += num4;
							}
						}
					}
				}
			}
			if (!list.IsNullOrEmpty<int>() && list.Count > num3)
			{
				list.Sort();
				int k = 0;
				int num6 = list.Count - num3;
				while (k < num6)
				{
					this.CarriedWeight += list[k];
					k++;
				}
			}
			return this.CarriedWeight;
		}

		// Token: 0x06009D54 RID: 40276 RVA: 0x0038FB30 File Offset: 0x0038DD30
		public void FlushWeightCache()
		{
			this.WeightCacheTurn = -1L;
			this.ParentObject.FlushCarriedWeightCache();
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			if (interiorZone != null)
			{
				FlushWeightCacheEvent.Send(interiorZone.ParentObject);
			}
		}

		// Token: 0x06009D55 RID: 40277 RVA: 0x0038FB70 File Offset: 0x0038DD70
		public bool TryGetMap(out MapFile Map, out int MX, out int MY, out int Width, out int Height)
		{
			MapBuilder builder = this.Builder;
			Map = ((builder != null) ? builder.Map : null);
			if (Map == null)
			{
				MX = -1;
				MY = -1;
				Width = 0;
				Height = 0;
				return false;
			}
			MX = builder.X;
			MY = builder.Y;
			Width = builder.Width;
			Height = builder.Height;
			if (MX == -1)
			{
				MX = 40 - Width / 2;
			}
			if (MY == -1)
			{
				MY = 12 - Height / 2;
			}
			return true;
		}

		// Token: 0x06009D56 RID: 40278 RVA: 0x0038FBE8 File Offset: 0x0038DDE8
		public bool Validate()
		{
			MapFile mapFile;
			int num;
			int num2;
			int num3;
			int num4;
			if (!this.TryGetMap(out mapFile, out num, out num2, out num3, out num4))
			{
				return true;
			}
			InteriorZone zone = this.Zone;
			for (int i = 0; i < num4; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					Cell cell = zone.GetCell(num + j, num2 + i);
					if (cell != null)
					{
						foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFile.Cells[j, i].Objects)
						{
							if (mapFileObjectBlueprint.IntProperties != null)
							{
								int value = mapFileObjectBlueprint.IntProperties.GetValue("InteriorRequired", 0);
								if (value > 0)
								{
									GameObject firstObject = cell.GetFirstObject(mapFileObjectBlueprint.Name);
									if (firstObject == null)
									{
										return false;
									}
									if (value == 2 && (firstObject.IsBroken() || firstObject.IsRusted()))
									{
										return false;
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06009D57 RID: 40279 RVA: 0x0038FD00 File Offset: 0x0038DF00
		public bool HasRequired(string Blueprint)
		{
			MapFile mapFile;
			int num;
			int num2;
			int num3;
			int num4;
			if (!this.TryGetMap(out mapFile, out num, out num2, out num3, out num4))
			{
				return false;
			}
			for (int i = 0; i < num4; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFile.Cells[j, i].Objects)
					{
						if (mapFileObjectBlueprint.IntProperties != null && !(mapFileObjectBlueprint.Name != Blueprint) && mapFileObjectBlueprint.IntProperties.ContainsKey("InteriorRequired"))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06009D58 RID: 40280 RVA: 0x0038FDCC File Offset: 0x0038DFCC
		public int GetRequired(GameObject Object)
		{
			Cell currentCell = Object.CurrentCell;
			if (currentCell == null || currentCell.ParentZone.ZoneID != this.ZoneID)
			{
				return -1;
			}
			MapFile mapFile;
			int num;
			int num2;
			int num3;
			int num4;
			if (!this.TryGetMap(out mapFile, out num, out num2, out num3, out num4))
			{
				return -1;
			}
			int num5 = currentCell.X - num;
			int num6 = currentCell.Y - num2;
			foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFile.Cells[num5, num6].Objects)
			{
				if (!(mapFileObjectBlueprint.Name != Object.Blueprint))
				{
					if (mapFileObjectBlueprint.IntProperties == null)
					{
						return -1;
					}
					return mapFileObjectBlueprint.IntProperties.GetValue("InteriorRequired", -1);
				}
			}
			return -1;
		}

		// Token: 0x06009D59 RID: 40281 RVA: 0x0038FEB0 File Offset: 0x0038E0B0
		public void Repair(GameObject Actor = null, GameObject Tool = null, IEvent ParentEvent = null)
		{
			MapBuilder builder = this.Builder;
			MapBuilder builder2 = this.Builder;
			MapFile mapFile = ((builder2 != null) ? builder2.Map : null);
			if (mapFile == null)
			{
				return;
			}
			if (this.Collapsed)
			{
				this._Zone = null;
				this.Collapsed = false;
			}
			InteriorZone zone = this.Zone;
			int num = builder.X;
			int num2 = builder.Y;
			int width = builder.Width;
			int height = builder.Height;
			if (num == -1)
			{
				num = zone.Width / 2 - width / 2;
			}
			if (num2 == -1)
			{
				num2 = zone.Height / 2 - height / 2;
			}
			RepairedEvent repairedEvent = PooledEvent<RepairedEvent>.FromPool();
			repairedEvent.Actor = Actor;
			repairedEvent.Tool = Tool;
			List<GameObject> list = Event.NewGameObjectList();
			List<GameObject> list2 = Event.NewGameObjectList();
			List<ValueTuple<Cell, MapFileObjectBlueprint>> list3 = new List<ValueTuple<Cell, MapFileObjectBlueprint>>();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Cell cell = zone.GetCell(num + j, num2 + i);
					if (cell != null)
					{
						list2.Clear();
						list2.AddRange(cell.Objects);
						using (List<MapFileObjectBlueprint>.Enumerator enumerator = mapFile.Cells[j, i].Objects.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								MapFileObjectBlueprint obj = enumerator.Current;
								if (obj.HasProperty("InteriorRequired"))
								{
									int num3 = list2.FindIndex((GameObject x) => x.Blueprint == obj.Name);
									if (num3 >= 0)
									{
										repairedEvent.Subject = list2[num3];
										list2[num3].HandleEvent<RepairedEvent>(repairedEvent, ParentEvent);
										list2.RemoveAt(num3);
									}
									else
									{
										list3.Add(new ValueTuple<Cell, MapFileObjectBlueprint>(cell, obj));
									}
								}
							}
						}
						foreach (GameObject gameObject in list2)
						{
							if (gameObject.HasProperty("InteriorRequired"))
							{
								list.Add(gameObject);
							}
						}
					}
				}
			}
			using (List<ValueTuple<Cell, MapFileObjectBlueprint>>.Enumerator enumerator3 = list3.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					ValueTuple<Cell, MapFileObjectBlueprint> tuple = enumerator3.Current;
					int num4 = list.FindIndex((GameObject x) => x.Blueprint == tuple.Item2.Name);
					if (num4 >= 0)
					{
						repairedEvent.Subject = list[num4];
						list[num4].HandleEvent<RepairedEvent>(repairedEvent, ParentEvent);
						list[num4].SystemMoveTo(tuple.Item1, new int?(0), true, true, false, false);
						list.RemoveAt(num4);
					}
					else
					{
						tuple.Item1.AddObject(tuple.Item2.Create(null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x06009D5A RID: 40282 RVA: 0x003901D0 File Offset: 0x0038E3D0
		public override bool HandleEvent(IsRepairableEvent E)
		{
			string tagOrStringProperty = this.ParentObject.GetTagOrStringProperty("RepairTool", null);
			return !tagOrStringProperty.IsNullOrEmpty() && (E.Tool == null || !E.Tool.GetBlueprint(true).DescendsFrom(tagOrStringProperty)) && base.HandleEvent(E);
		}

		// Token: 0x06009D5B RID: 40283 RVA: 0x0039021E File Offset: 0x0038E41E
		public override bool HandleEvent(RepairedEvent E)
		{
			this.Repair(E.Actor, E.Tool, E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009D5C RID: 40284 RVA: 0x0039023C File Offset: 0x0038E43C
		public override bool HandleEvent(GetTransitiveLocationEvent E)
		{
			if (E.Zone == this.ParentObject.CurrentZone)
			{
				Cell origin = E.Origin;
				string text;
				if (origin == null)
				{
					text = null;
				}
				else
				{
					Zone parentZone = origin.ParentZone;
					text = ((parentZone != null) ? parentZone.ZoneID : null);
				}
				if (text == this.ZoneID && E.IsEgress)
				{
					Cell placementCell = this.GetPlacementCell(E.Actor);
					int num = 500;
					GameObject source = E.Source;
					string text2 = ((source != null) ? source.GetTagOrStringProperty("PortalKey", null) : null);
					if (!text2.IsNullOrEmpty() && text2 == this.ParentObject.GetTagOrStringProperty("PortalKey", null))
					{
						num = 1000;
					}
					E.AddLocation(placementCell, this.ParentObject, num);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D5D RID: 40285 RVA: 0x003902FC File Offset: 0x0038E4FC
		public Cell GetPlacementCell(GameObject For = null)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (!currentCell.IsPassable(For, true))
			{
				return currentCell.getClosestPassableCellFor(For);
			}
			return currentCell;
		}

		// Token: 0x06009D5E RID: 40286 RVA: 0x00390328 File Offset: 0x0038E528
		public override bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			InteriorZone active = InteriorZone.Active;
			if (((active != null) ? active.ParentObject : null) == this.ParentObject)
			{
				E.Suspendability = Suspendability.InsideInterior;
				E.Zone.MarkActive();
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D5F RID: 40287 RVA: 0x0039035E File Offset: 0x0038E55E
		public override bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			InteriorZone active = InteriorZone.Active;
			if (((active != null) ? active.ParentObject : null) == this.ParentObject)
			{
				E.Freezability = Freezability.InsideInterior;
				E.Zone.MarkActive();
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D60 RID: 40288 RVA: 0x00390394 File Offset: 0x0038E594
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			if (E.NewBody == this.ParentObject || E.OldBody == this.ParentObject)
			{
				InteriorZone zone = this.Zone;
				if (zone != null)
				{
					zone.HandleEvent(E);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D61 RID: 40289 RVA: 0x003903CC File Offset: 0x0038E5CC
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				InteriorZone active = InteriorZone.Active;
				if (((active != null) ? active.ParentObject : null) == this.ParentObject)
				{
					CombatJuice.cameraShake(0.5f, false);
					IComponent<GameObject>.AddPlayerMessage(Event.NewStringBuilder(null).Append(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)).Append(' ')
						.Append(this.ParentObject.GetVerb("take", false, false, false, true, false))
						.Append(' ')
						.Append(E.Damage.Amount)
						.Append(' ')
						.Append("damage!")
						.ToString(), "R", true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D62 RID: 40290 RVA: 0x003904A6 File Offset: 0x0038E6A6
		public override bool HandleEvent(AfterDieEvent E)
		{
			this.ParentObject.PullDown(false);
			return this.Collapse(E) && base.HandleEvent(E);
		}

		// Token: 0x06009D63 RID: 40291 RVA: 0x003904C6 File Offset: 0x0038E6C6
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.ParentObject.PullDown(false);
			return (this.Collapse(E) || E.Obliterate) && base.HandleEvent(E);
		}

		// Token: 0x06009D64 RID: 40292 RVA: 0x003904F0 File Offset: 0x0038E6F0
		public bool Collapse(IEvent ParentEvent = null)
		{
			if (this.Collapsed)
			{
				return true;
			}
			if (!The.ZoneManager.IsZoneBuilt(this.ZoneID))
			{
				this.Collapsed = true;
				return true;
			}
			BeforeInteriorCollapseEvent beforeInteriorCollapseEvent = PooledEvent<BeforeInteriorCollapseEvent>.FromPool();
			InteriorZone interiorZone = (beforeInteriorCollapseEvent.Zone = this.Zone);
			beforeInteriorCollapseEvent.Object = this.ParentObject;
			if (!interiorZone.HandleEvent<BeforeInteriorCollapseEvent>(beforeInteriorCollapseEvent, ParentEvent))
			{
				return false;
			}
			for (int i = 0; i < interiorZone.Height; i++)
			{
				for (int j = 0; j < interiorZone.Width; j++)
				{
					for (int k = interiorZone.Map[j][i].Objects.Count - 1; k >= 0; k--)
					{
						GameObject gameObject = interiorZone.Map[j][i].Objects[k];
						if (gameObject.IsCombatObject())
						{
							Cell escapeCell = interiorZone.GetEscapeCell(gameObject);
							gameObject.SystemMoveTo(escapeCell, new int?(0), true, true, false, false);
							if (this.FallDistance > 1)
							{
								StairsDown.InflictFallDamage(gameObject, this.FallDistance, null, null);
							}
						}
					}
				}
			}
			The.ZoneManager.DeleteZone(interiorZone);
			this.Collapsed = true;
			return true;
		}

		// Token: 0x06009D65 RID: 40293 RVA: 0x00390614 File Offset: 0x0038E814
		public bool CanEnter(GameObject Actor, bool Action = false, bool ShowMessage = false)
		{
			int num = 0;
			if (this.ParentObject.CurrentCell == null || this.Collapsed)
			{
				num = 2;
			}
			else if (!this.ParentObject.PhaseMatches(Actor))
			{
				num = 4;
			}
			else if (!Actor.FlightCanReach(this.ParentObject))
			{
				num = 8;
			}
			else if (Actor.IsGiganticCreature || Actor.HasPart(typeof(Vehicle)))
			{
				num = 16;
			}
			else if (this.ParentObject.IsHostileTowards(Actor))
			{
				num = 1;
			}
			if (!CanEnterInteriorEvent.Check(Actor, this.ParentObject, this, ref num, ref Action, ref ShowMessage))
			{
				if (ShowMessage)
				{
					this.ShowMessage(Actor, num);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06009D66 RID: 40294 RVA: 0x003906B4 File Offset: 0x0038E8B4
		public bool ShowMessage(GameObject Actor, int Status)
		{
			switch (Status)
			{
			case 1:
				base.DidXToY("refuse", Actor, "entry", null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, true, false, Actor.IsPlayerControlled(), null);
				return true;
			case 2:
				Actor.Physics.DidXToY("are", "unable to enter", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, true, false, Actor.IsPlayerControlled(), null);
				return true;
			case 3:
				break;
			case 4:
				Actor.Physics.DidXToY("are", "out of phase with", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, true, false, Actor.IsPlayerControlled(), null);
				return true;
			default:
				if (Status == 8)
				{
					Actor.Physics.DidXToY("cannot", "reach", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, true, false, Actor.IsPlayerControlled(), null);
					return true;
				}
				if (Status == 16)
				{
					Actor.Physics.DidXToY("are", "too large to enter", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, true, false, Actor.IsPlayerControlled(), null);
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x06009D67 RID: 40295 RVA: 0x003907F0 File Offset: 0x0038E9F0
		public bool TryEnter(GameObject Actor, bool Force = false, bool ShowMessage = true)
		{
			if (Force || this.CanEnter(Actor, true, ShowMessage))
			{
				Cell pullDownLocation;
				GameObject gameObject;
				GetTransitiveLocationEvent.GetFor(this.Zone, "Ingress,Interior", Actor, this.ParentObject, this.ParentObject.CurrentCell, out pullDownLocation, out gameObject);
				if (pullDownLocation == null)
				{
					pullDownLocation = this.Zone.GetPullDownLocation(Actor);
				}
				if (pullDownLocation != null && Actor.DirectMoveTo(pullDownLocation, 0, Force, false, false, null))
				{
					string propertyOrTag = this.ParentObject.GetPropertyOrTag("EnterSound", null);
					if (!propertyOrTag.IsNullOrEmpty())
					{
						base.PlayWorldSound(propertyOrTag, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					}
					else
					{
						string propertyOrTag2 = this.ParentObject.GetPropertyOrTag("OpenSound", null);
						string text = ((gameObject != null) ? gameObject.GetPropertyOrTag("OpenSound", null) : null);
						string text2 = propertyOrTag2.Coalesce(text);
						if (!text2.IsNullOrEmpty())
						{
							if (Actor.IsPlayer())
							{
								if (propertyOrTag2 != text)
								{
									SoundManager.PlayWorldSound(text2, 15, true, 0.5f, pullDownLocation.Location, 0f, 0f, 1f);
								}
							}
							else
							{
								base.PlayWorldSound(text2, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
							}
						}
					}
					this.FlushWeightCache();
					if (gameObject != null)
					{
						gameObject.FireEvent("PortalTransition");
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009D68 RID: 40296 RVA: 0x00390952 File Offset: 0x0038EB52
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009D69 RID: 40297 RVA: 0x00390958 File Offset: 0x0038EB58
		public override void TurnTick(long TimeTick, int Amount)
		{
			InteriorZone active = InteriorZone.Active;
			if (((active != null) ? active.ParentObject : null) == this.ParentObject)
			{
				this.ParentObject.CurrentZone.MarkActive();
				if (this.ParentObject.Brain != null)
				{
					this.ParentObject.Brain.Hibernating = true;
					return;
				}
			}
			else if (this.ParentObject.Brain != null)
			{
				this.ParentObject.Brain.Hibernating = false;
			}
		}

		// Token: 0x0400410B RID: 16651
		public const int FLAG_UNIQUE = 1;

		// Token: 0x0400410C RID: 16652
		public const int FLAG_IGNORE_WEIGHT = 2;

		// Token: 0x0400410D RID: 16653
		public string Cell;

		// Token: 0x0400410E RID: 16654
		public int WX = 40;

		// Token: 0x0400410F RID: 16655
		public int WY = 12;

		// Token: 0x04004110 RID: 16656
		public int X = 1;

		// Token: 0x04004111 RID: 16657
		public int Y = 1;

		// Token: 0x04004112 RID: 16658
		public int Z = 10;

		// Token: 0x04004113 RID: 16659
		public int FallDistance;

		// Token: 0x04004114 RID: 16660
		public int CarriedWeight;

		// Token: 0x04004115 RID: 16661
		public int Flags = 1;

		// Token: 0x04004116 RID: 16662
		[NonSerialized]
		private static bool ErrorMessage;

		// Token: 0x04004117 RID: 16663
		[NonSerialized]
		private InteriorZone _Zone;

		// Token: 0x04004118 RID: 16664
		[NonSerialized]
		private string _ZoneID;

		// Token: 0x04004119 RID: 16665
		[NonSerialized]
		private MapBuilder _Builder;

		// Token: 0x0400411A RID: 16666
		[NonSerialized]
		private long WeightCacheTurn = -1L;

		// Token: 0x0400411B RID: 16667
		[NonSerialized]
		private bool Collapsed;

		// Token: 0x02001C94 RID: 7316
		public static class Enter
		{
			// Token: 0x0400708A RID: 28810
			public const int SUCCESS = 0;

			// Token: 0x0400708B RID: 28811
			public const int HOSTILE = 1;

			// Token: 0x0400708C RID: 28812
			public const int INVALID = 2;

			// Token: 0x0400708D RID: 28813
			public const int OUT_OF_PHASE = 4;

			// Token: 0x0400708E RID: 28814
			public const int UNREACHABLE = 8;

			// Token: 0x0400708F RID: 28815
			public const int GIGANTIC = 16;

			// Token: 0x04007090 RID: 28816
			public const int PREVENTED = 30;

			// Token: 0x04007091 RID: 28817
			public const int FORBIDDEN = 1;
		}
	}
}
