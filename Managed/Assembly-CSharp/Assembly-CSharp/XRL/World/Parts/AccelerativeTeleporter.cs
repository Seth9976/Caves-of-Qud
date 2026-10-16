using System;
using XRL.Core;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, teleport distance is increased by
	/// a percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02000FE8 RID: 4072
	[Serializable]
	public class AccelerativeTeleporter : IPoweredPart
	{
		// Token: 0x0600A237 RID: 41527 RVA: 0x003B4A10 File Offset: 0x003B2C10
		public AccelerativeTeleporter()
		{
			this.ChargeUse = 250;
			this.IsPowerLoadSensitive = true;
			this.IsPowerSwitchSensitive = true;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A238 RID: 41528 RVA: 0x003B4A78 File Offset: 0x003B2C78
		public override bool SameAs(IPart p)
		{
			AccelerativeTeleporter accelerativeTeleporter = p as AccelerativeTeleporter;
			return accelerativeTeleporter.TriggerDistance == this.TriggerDistance && accelerativeTeleporter.TeleportDistance == this.TeleportDistance && accelerativeTeleporter.MaximumShuntDistance == this.MaximumShuntDistance && accelerativeTeleporter.AccelerateTravel == this.AccelerateTravel && accelerativeTeleporter.WorksOnWorldMap == this.WorksOnWorldMap && accelerativeTeleporter.GroundContactRequired == this.GroundContactRequired && accelerativeTeleporter.DoSpatialDistortionBlip == this.DoSpatialDistortionBlip && accelerativeTeleporter.DoTeleportVisualEffects == this.DoTeleportVisualEffects && accelerativeTeleporter.DoTeleportReducedVisualEffects == this.DoTeleportReducedVisualEffects && base.SameAs(p);
		}

		// Token: 0x0600A239 RID: 41529 RVA: 0x003B4B23 File Offset: 0x003B2D23
		private bool IsOutOfGroundContact(GameObject obj)
		{
			return obj.IsFlying || !obj.PhaseMatches(1);
		}

		// Token: 0x0600A23A RID: 41530 RVA: 0x003B4B39 File Offset: 0x003B2D39
		private bool TriggerSpatialDistortionBlip(GameObject obj)
		{
			obj.SpatialDistortionBlip(null, "&C");
			return true;
		}

		// Token: 0x0600A23B RID: 41531 RVA: 0x003B4B48 File Offset: 0x003B2D48
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.GroundContactRequired && this.GetActivePartFirstSubject(new Predicate<GameObject>(this.IsOutOfGroundContact)) != null;
		}

		// Token: 0x0600A23C RID: 41532 RVA: 0x003B4B69 File Offset: 0x003B2D69
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return "NoGroundContact";
		}

		// Token: 0x0600A23D RID: 41533 RVA: 0x003B4B70 File Offset: 0x003B2D70
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == UnequippedEvent.ID || ID == PooledEvent<AfterTravelEvent>.ID;
		}

		// Token: 0x0600A23E RID: 41534 RVA: 0x003B4BB0 File Offset: 0x003B2DB0
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.CurrentDirection != null && !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ResetAcceleration();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A23F RID: 41535 RVA: 0x003B4BFC File Offset: 0x003B2DFC
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			if (this.CurrentDirection != null && !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ResetAcceleration();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A240 RID: 41536 RVA: 0x003B4C48 File Offset: 0x003B2E48
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "AfterDirectMove");
			E.Actor.RegisterPartEvent(this, "AfterMoved");
			E.Actor.RegisterPartEvent(this, "AfterTeleport");
			E.Actor.RegisterPartEvent(this, "TravelSpeed");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A241 RID: 41537 RVA: 0x003B4CA0 File Offset: 0x003B2EA0
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "AfterDirectMove");
			E.Actor.UnregisterPartEvent(this, "AfterMoved");
			E.Actor.UnregisterPartEvent(this, "AfterTeleport");
			E.Actor.UnregisterPartEvent(this, "TravelSpeed");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A242 RID: 41538 RVA: 0x003B4CF8 File Offset: 0x003B2EF8
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A243 RID: 41539 RVA: 0x003B4D1B File Offset: 0x003B2F1B
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A244 RID: 41540 RVA: 0x003B4D1E File Offset: 0x003B2F1E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CellChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A245 RID: 41541 RVA: 0x003B4D33 File Offset: 0x003B2F33
		public void ResetAcceleration()
		{
			this.CurrentDirection = null;
			this.CurrentDistance = 0;
		}

		// Token: 0x0600A246 RID: 41542 RVA: 0x003B4D44 File Offset: 0x003B2F44
		public static Cell FindTargetCell(GameObject who, string dir, int dist)
		{
			Cell cell = who.CurrentCell;
			Cell cell2 = null;
			int num = 0;
			while (num < dist && cell != null)
			{
				cell = cell.GetCellFromDirection(dir, false);
				if (cell != null)
				{
					cell2 = cell;
				}
				num++;
			}
			return cell ?? cell2;
		}

		// Token: 0x0600A247 RID: 41543 RVA: 0x003B4D80 File Offset: 0x003B2F80
		public static Cell FindDestinationCell(GameObject who, string dir, int dist, int maxShunt)
		{
			Cell cell = AccelerativeTeleporter.FindTargetCell(who, dir, dist);
			if (cell == null || cell == who.CurrentCell)
			{
				return null;
			}
			if (cell.IsSolidFor(who, false))
			{
				Cell cell2 = cell;
				cell = cell2.getClosestReachableCellFor(who);
				if (cell != null && cell.PathDistanceTo(cell2) > maxShunt)
				{
					cell = null;
				}
			}
			return cell;
		}

		// Token: 0x0600A248 RID: 41544 RVA: 0x003B4DC8 File Offset: 0x003B2FC8
		public bool PerformAccelerativeTeleport(GameObject who, string dir, Event E, int? PowerLoad = null)
		{
			Cell cell = AccelerativeTeleporter.FindDestinationCell(who, dir, this.GetEffectiveTeleportDistance(PowerLoad ?? this.MyPowerLoadLevel()), this.MaximumShuntDistance);
			if (cell != null && (!who.IsPlayer() || !AutoAct.IsExploration() || who.InSameZone(cell)) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Cell cell2 = cell;
				GameObject parentObject = this.ParentObject;
				IPart part = null;
				string text = null;
				bool doTeleportVisualEffects = this.DoTeleportVisualEffects;
				bool doTeleportReducedVisualEffects = this.DoTeleportReducedVisualEffects;
				who.CellTeleport(cell2, E, parentObject, who, part, text, new int?(0), false, doTeleportVisualEffects, doTeleportReducedVisualEffects, false, "disappear", "appear", null);
			}
			return true;
		}

		// Token: 0x0600A249 RID: 41545 RVA: 0x003B4E80 File Offset: 0x003B3080
		private bool ProcessMoveInner(Cell oldCell, Cell newCell, Event E, bool forced)
		{
			if (forced)
			{
				return true;
			}
			if (oldCell == null)
			{
				return false;
			}
			if (newCell == null)
			{
				return false;
			}
			if (oldCell.OnWorldMap() != newCell.OnWorldMap())
			{
				return false;
			}
			if (!this.WorksOnWorldMap && newCell.OnWorldMap())
			{
				return false;
			}
			if (!oldCell.IsAdjacentTo(newCell, true))
			{
				return false;
			}
			if (!base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, true, 0L, null))
			{
				return false;
			}
			string dir = oldCell.GetDirectionFromCell(newCell, false);
			if (dir == this.CurrentDirection)
			{
				this.CurrentDistance++;
				if (this.CurrentDistance >= this.TriggerDistance)
				{
					if (this.DoTeleportVisualEffects && newCell.OnWorldMap())
					{
						XRLCore.Core.AllowWorldMapParticles = true;
					}
					this.ForeachActivePartSubjectWhile((GameObject o) => this.PerformAccelerativeTeleport(o, dir, E, null), true);
					return false;
				}
				if (this.DoSpatialDistortionBlip)
				{
					if (newCell.OnWorldMap())
					{
						XRLCore.Core.AllowWorldMapParticles = true;
					}
					this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.TriggerSpatialDistortionBlip), false);
					return true;
				}
			}
			else if (!string.IsNullOrEmpty(dir) && dir != ".")
			{
				this.CurrentDirection = dir;
				this.CurrentDistance = 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600A24A RID: 41546 RVA: 0x003B4FDD File Offset: 0x003B31DD
		public void ProcessMove(Cell oldCell, Cell newCell, Event E, bool forced = false)
		{
			if (!this.ProcessMoveInner(oldCell, newCell, E, forced))
			{
				this.ResetAcceleration();
			}
		}

		// Token: 0x0600A24B RID: 41547 RVA: 0x003B4FF4 File Offset: 0x003B31F4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterMoved")
			{
				Cell cell = E.GetParameter("FromCell") as Cell;
				Cell currentCell = this.ParentObject.GetCurrentCell();
				this.ProcessMove(cell, currentCell, E, E.HasParameter("Forced"));
			}
			else if (E.ID == "TravelSpeed")
			{
				if (this.AccelerateTravel)
				{
					int num = this.MyPowerLoadLevel();
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = false;
					bool flag9 = false;
					bool flag10 = false;
					int num2 = 1;
					int? num3 = new int?(num);
					if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
					{
						int effectiveTeleportDistance = this.GetEffectiveTeleportDistance(num);
						int num4;
						if (this.ChargeUse <= 0)
						{
							num4 = effectiveTeleportDistance * 100 / this.TriggerDistance;
						}
						else
						{
							int num5 = this.ParentObject.QueryCharge(false, 0L, true, true);
							if (num5 >= this.ChargeUse * 20)
							{
								num4 = effectiveTeleportDistance * 100 / this.TriggerDistance;
							}
							else if (num5 >= this.ChargeUse * 10)
							{
								num4 = effectiveTeleportDistance * 50 / this.TriggerDistance;
							}
							else if (num5 >= this.ChargeUse * 5)
							{
								num4 = effectiveTeleportDistance * 25 / this.TriggerDistance;
							}
							else
							{
								num4 = effectiveTeleportDistance / this.TriggerDistance;
							}
						}
						if (num4 > 0)
						{
							if (this.DoSpatialDistortionBlip)
							{
								XRLCore.Core.AllowWorldMapParticles = true;
								this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.TriggerSpatialDistortionBlip), false);
							}
							E.SetParameter("PercentageBonus", E.GetIntParameter("PercentageBonus", 0) + num4);
							this.AcceleratedTravel = true;
						}
					}
				}
			}
			else if (E.ID == "AfterTeleport")
			{
				this.ResetAcceleration();
			}
			else if (E.ID == "CellChanged" && this.CurrentDirection != null)
			{
				bool flag11 = false;
				bool flag12 = false;
				bool flag13 = false;
				bool flag14 = false;
				bool flag15 = false;
				bool flag16 = false;
				bool flag17 = false;
				bool flag18 = false;
				bool flag19 = false;
				bool flag20 = false;
				int num6 = 1;
				int? num3 = null;
				int? num7 = num3;
				bool flag21 = false;
				long num8 = 0L;
				num3 = null;
				if (!base.IsReady(flag11, flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, num6, num7, flag21, num8, num3))
				{
					this.ResetAcceleration();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A24C RID: 41548 RVA: 0x003B51F0 File Offset: 0x003B33F0
		public override bool HandleEvent(AfterTravelEvent E)
		{
			if (this.AcceleratedTravel && this.ChargeUse > 0 && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num = E.Segments / 10;
				this.ParentObject.UseCharge(this.ChargeUse * num, false, 0L, true, true, null);
				this.AcceleratedTravel = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A24D RID: 41549 RVA: 0x003B5280 File Offset: 0x003B3480
		public int GetEffectiveTeleportDistance()
		{
			int num = this.TeleportDistance;
			int num2 = this.MyPowerLoadBonus(int.MinValue, 100, 10);
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return num;
		}

		// Token: 0x0600A24E RID: 41550 RVA: 0x003B52B4 File Offset: 0x003B34B4
		public int GetEffectiveTeleportDistance(int PowerLoad)
		{
			int num = this.TeleportDistance;
			int num2 = this.MyPowerLoadBonus(PowerLoad, 100, 10);
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return num;
		}

		// Token: 0x0400433B RID: 17211
		public int TriggerDistance = 3;

		// Token: 0x0400433C RID: 17212
		public int TeleportDistance = 9;

		// Token: 0x0400433D RID: 17213
		public int MaximumShuntDistance = 3;

		// Token: 0x0400433E RID: 17214
		public bool AccelerateTravel;

		// Token: 0x0400433F RID: 17215
		public bool WorksOnWorldMap;

		// Token: 0x04004340 RID: 17216
		public bool GroundContactRequired = true;

		// Token: 0x04004341 RID: 17217
		public bool DoSpatialDistortionBlip = true;

		// Token: 0x04004342 RID: 17218
		public bool DoTeleportVisualEffects = true;

		// Token: 0x04004343 RID: 17219
		public bool DoTeleportReducedVisualEffects = true;

		// Token: 0x04004344 RID: 17220
		public string CurrentDirection;

		// Token: 0x04004345 RID: 17221
		public int CurrentDistance;

		// Token: 0x04004346 RID: 17222
		[NonSerialized]
		private bool AcceleratedTravel;
	}
}
