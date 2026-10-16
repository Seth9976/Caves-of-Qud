using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FBA RID: 4026
	[Serializable]
	public class TeleportGate : IPoweredPart
	{
		// Token: 0x06009FF9 RID: 40953 RVA: 0x003A2F11 File Offset: 0x003A1111
		public TeleportGate()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009FFA RID: 40954 RVA: 0x003A2F28 File Offset: 0x003A1128
		public override bool SameAs(IPart p)
		{
			TeleportGate teleportGate = p as TeleportGate;
			return teleportGate.RingBasedName == this.RingBasedName && teleportGate.Target == this.Target && base.SameAs(p);
		}

		// Token: 0x06009FFB RID: 40955 RVA: 0x003A2F63 File Offset: 0x003A1163
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009FFC RID: 40956 RVA: 0x003A2F66 File Offset: 0x003A1166
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckPossibleSubjects();
		}

		// Token: 0x06009FFD RID: 40957 RVA: 0x003A2F70 File Offset: 0x003A1170
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == GetAdjacentNavigationWeightEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetNavigationWeightEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == ObjectEnteredCellEvent.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x06009FFE RID: 40958 RVA: 0x003A2FC4 File Offset: 0x003A11C4
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null))
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, 1, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FFF RID: 40959 RVA: 0x003A301C File Offset: 0x003A121C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			GameObject parentObject = this.ParentObject;
			bool flag;
			if (parentObject == null)
			{
				flag = false;
			}
			else
			{
				Zone currentZone = parentObject.CurrentZone;
				bool? flag2 = ((currentZone != null) ? new bool?(currentZone.Built) : null);
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
			}
			if (flag)
			{
				this.CheckIncomingTarget();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A000 RID: 40960 RVA: 0x003A3078 File Offset: 0x003A1278
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood())
			{
				GameObject parentObject = this.ParentObject;
				Zone zone = ((parentObject != null) ? parentObject.CurrentZone : null);
				if (zone != null)
				{
					string text = The.ZoneManager.GetZoneProperty(zone.ZoneID, "TeleportGateName", false, null) as string;
					if (!text.IsNullOrEmpty())
					{
						E.ReplacePrimaryBase(text, 0);
					}
					else
					{
						string text2 = "secant";
						if (The.ZoneManager.HasZoneProperty(zone.ZoneID, "TeleportGateRingSize"))
						{
							int num = (int)The.ZoneManager.GetZoneProperty(zone.ZoneID, "TeleportGateRingSize", false, null);
							if (num > 0)
							{
								text2 = num.ToString() + "-ring";
							}
						}
						E.AddBase(text2, -5, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A001 RID: 40961 RVA: 0x003A3139 File Offset: 0x003A1339
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.CheckIncomingTarget();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A002 RID: 40962 RVA: 0x003A3148 File Offset: 0x003A1348
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			E.MinWeight(60);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A003 RID: 40963 RVA: 0x003A3159 File Offset: 0x003A1359
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A004 RID: 40964 RVA: 0x003A316C File Offset: 0x003A136C
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			GameObject parentObject = this.ParentObject;
			bool flag;
			if (parentObject == null)
			{
				flag = false;
			}
			else
			{
				Zone currentZone = parentObject.CurrentZone;
				bool? flag2 = ((currentZone != null) ? new bool?(currentZone.Built) : null);
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
			}
			if (flag)
			{
				this.CheckPossibleSubject(E.Object, E, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A005 RID: 40965 RVA: 0x003A31D4 File Offset: 0x003A13D4
		public int CheckPossibleSubjects()
		{
			int num = 0;
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int i = 0;
				int num2 = currentCell.Objects.Count;
				while (i < num2)
				{
					if (this.CheckPossibleSubject(currentCell.Objects[i], null, true))
					{
						num++;
						i = -1;
						num2 = currentCell.Objects.Count;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600A006 RID: 40966 RVA: 0x003A3260 File Offset: 0x003A1460
		public bool CheckPossibleSubject(GameObject Object, IEvent FromEvent = null, bool ReadyKnown = false)
		{
			if (Object != this.ParentObject && Object.IsReal && !Object.IsScenery && (Object.IsCreature || Object.IsTakeable()) && Object.PhaseAndFlightMatches(this.ParentObject) && (ReadyKnown || base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)))
			{
				GlobalLocation target = this.GetTarget();
				if (target != null)
				{
					if (target.ResolveCell() != null && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						Zone zone = The.ZoneManager.GetZone(target.ZoneID);
						Cell currentCell = Object.CurrentCell;
						bool flag = zone.LastPlayerPresence != -1L;
						bool flag2 = Object.ZoneTeleport(zone.ZoneID, target.CellX, target.CellY, FromEvent, this.ParentObject, Object.IsCreature ? Object : null, null, "You are transported!", true, null);
						if (flag2 && !flag && Object.IsPlayer() && Object.ApplyEffect(new Lost(9999, zone.ZoneID, null, 10, false), null))
						{
							Popup.ShowSpace("You're lost! Regain your bearings by exploring your surroundings.", null, "Sounds/UI/ui_notification", null, true, true, null);
							Object.FireEvent(Event.New("AfterLost", "FromCell", currentCell));
						}
						return flag2;
					}
				}
				else
				{
					Object.RandomTeleport(true, null, null, null, null, 0, 10, true, null, false, true, true, false);
				}
			}
			return false;
		}

		// Token: 0x0600A007 RID: 40967 RVA: 0x003A33EC File Offset: 0x003A15EC
		public void CheckIncomingTarget()
		{
			GameObject parentObject = this.ParentObject;
			Cell cell = ((parentObject != null) ? parentObject.CurrentCell : null);
			if (cell == null)
			{
				return;
			}
			Zone parentZone = cell.ParentZone;
			if (parentZone == null)
			{
				return;
			}
			string zoneID = parentZone.ZoneID;
			if (The.ZoneManager.HasZoneProperty(zoneID, "TeleportGateIncomingX") && The.ZoneManager.HasZoneProperty(zoneID, "TeleportGateIncomingY"))
			{
				return;
			}
			Cell cell2;
			if ((cell2 = cell.GetEmptyConnectedAdjacentCells(1, true).GetRandomElement<Cell>(null)) == null)
			{
				cell2 = cell.GetEmptyConnectedAdjacentCells(2, true).GetRandomElement<Cell>(null) ?? parentZone.GetEmptyCells(null, null).GetRandomElement<Cell>(null);
			}
			Cell cell3 = cell2;
			if (cell3 == null)
			{
				return;
			}
			The.ZoneManager.SetZoneProperty(zoneID, "TeleportGateIncomingX", cell3.X);
			The.ZoneManager.SetZoneProperty(zoneID, "TeleportGateIncomingY", cell3.Y);
		}

		// Token: 0x0600A008 RID: 40968 RVA: 0x003A34B4 File Offset: 0x003A16B4
		private string GetRandomDestinationZoneID(string World)
		{
			if (World != "JoppaWorld")
			{
				return null;
			}
			int num = Stat.Random(0, 79);
			int num2 = Stat.Random(0, 24);
			int num3 = Stat.Random(0, 2);
			int num4 = Stat.Random(0, 2);
			int num5 = (50.in100() ? Stat.Random(10, 40) : 10);
			return ZoneID.Assemble(World, num, num2, num3, num4, num5);
		}

		// Token: 0x0600A009 RID: 40969 RVA: 0x003A3518 File Offset: 0x003A1718
		private string GetRandomTeleportGateZoneID(string World)
		{
			List<string> list = The.Game.GetObjectGameState((World ?? "Default") + "TeleportGateZones") as List<string>;
			if (list != null && list.Count > 0)
			{
				return list.GetRandomElement<string>(null);
			}
			return null;
		}

		// Token: 0x0600A00A RID: 40970 RVA: 0x003A3560 File Offset: 0x003A1760
		private GlobalLocation GetRandomTarget()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			string text = ((currentZone != null) ? currentZone.GetZoneWorld() : null);
			string text2 = this.GetRandomTeleportGateZoneID(text);
			if (text2 == null)
			{
				text2 = this.GetRandomDestinationZoneID(text);
				if (text2 == null)
				{
					return null;
				}
			}
			return this.GetTargetFromZone(text2);
		}

		// Token: 0x0600A00B RID: 40971 RVA: 0x003A35A4 File Offset: 0x003A17A4
		private GlobalLocation GetPreferredTarget()
		{
			GameObject parentObject = this.ParentObject;
			string text;
			if (parentObject == null)
			{
				text = null;
			}
			else
			{
				Zone currentZone = parentObject.CurrentZone;
				text = ((currentZone != null) ? currentZone.ZoneID : null);
			}
			string text2 = text;
			if (!text2.IsNullOrEmpty())
			{
				string text3 = The.ZoneManager.GetZoneProperty(text2, "TeleportGateDestinationZone", false, null) as string;
				if (!text3.IsNullOrEmpty())
				{
					return this.GetTargetFromZone(text3);
				}
			}
			return null;
		}

		// Token: 0x0600A00C RID: 40972 RVA: 0x003A3604 File Offset: 0x003A1804
		private GlobalLocation GetTarget()
		{
			GlobalLocation globalLocation;
			if ((globalLocation = this.Target) == null)
			{
				globalLocation = (this.Target = this.GetPreferredTarget() ?? this.GetRandomTarget());
			}
			return globalLocation;
		}

		// Token: 0x0600A00D RID: 40973 RVA: 0x003A3634 File Offset: 0x003A1834
		private GlobalLocation GetTargetFromZone(string zoneID)
		{
			if (The.ZoneManager.HasZoneProperty(zoneID, "TeleportGateIncomingX") && The.ZoneManager.HasZoneProperty(zoneID, "TeleportGateIncomingY"))
			{
				return GlobalLocation.FromZoneId(zoneID, (int)The.ZoneManager.GetZoneProperty(zoneID, "TeleportGateIncomingX", false, null), (int)The.ZoneManager.GetZoneProperty(zoneID, "TeleportGateIncomingY", false, null));
			}
			Zone zone = The.ZoneManager.GetZone(zoneID);
			if (The.ZoneManager.HasZoneProperty(zoneID, "TeleportGateIncomingX") && The.ZoneManager.HasZoneProperty(zoneID, "TeleportGateIncomingY"))
			{
				return GlobalLocation.FromZoneId(zoneID, (int)The.ZoneManager.GetZoneProperty(zoneID, "TeleportGateIncomingX", false, null), (int)The.ZoneManager.GetZoneProperty(zoneID, "TeleportGateIncomingY", false, null));
			}
			Cell cell = zone.GetEmptyCells(null, null).GetRandomElement<Cell>(null) ?? zone.GetCells().GetRandomElement<Cell>(null);
			if (cell != null)
			{
				return GlobalLocation.FromZoneId(zoneID, cell.X, cell.Y);
			}
			return GlobalLocation.FromZoneId(zoneID, zone.Width / 2, zone.Height / 2);
		}

		// Token: 0x04004284 RID: 17028
		public bool RingBasedName = true;

		// Token: 0x04004285 RID: 17029
		public GlobalLocation Target;
	}
}
