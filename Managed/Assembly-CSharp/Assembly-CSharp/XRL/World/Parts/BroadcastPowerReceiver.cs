using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011B8 RID: 4536
	[Serializable]
	public class BroadcastPowerReceiver : IPoweredPart
	{
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600B40D RID: 46093 RVA: 0x0041AF87 File Offset: 0x00419187
		// (set) Token: 0x0600B40C RID: 46092 RVA: 0x0041AF70 File Offset: 0x00419170
		public static long SatellitePowerOcclusionCheckTurn
		{
			get
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return 0L;
				}
				return game.GetInt64GameState("SatellitePowerOcclusionCheckTurn", 0L);
			}
			set
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return;
				}
				game.SetInt64GameState("SatellitePowerOcclusionCheckTurn", value);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600B40F RID: 46095 RVA: 0x0041AFB8 File Offset: 0x004191B8
		// (set) Token: 0x0600B40E RID: 46094 RVA: 0x0041AFA1 File Offset: 0x004191A1
		public static bool SatellitePowerOccluded
		{
			get
			{
				XRLGame game = The.Game;
				return game != null && game.GetBooleanGameState("SatellitePowerOccluded", false);
			}
			set
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return;
				}
				game.SetBooleanGameState("SatellitePowerOccluded", value);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600B411 RID: 46097 RVA: 0x0041AFE7 File Offset: 0x004191E7
		// (set) Token: 0x0600B410 RID: 46096 RVA: 0x0041AFD0 File Offset: 0x004191D0
		public static string SatellitePowerOcclusionReason
		{
			get
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return null;
				}
				return game.GetStringGameState("SatellitePowerOcclusionReason", "");
			}
			set
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return;
				}
				game.SetStringGameState("SatellitePowerOcclusionReason", value);
			}
		}

		// Token: 0x0600B412 RID: 46098 RVA: 0x0041B004 File Offset: 0x00419204
		public static void CheckSatellitePowerOcclusion(int Turns = 1)
		{
			if (BroadcastPowerReceiver.SatellitePowerOcclusionCheckTurn >= The.CurrentTurn)
			{
				return;
			}
			int intSetting = GlobalConfig.GetIntSetting("SatellitePowerOcclusionChance", 1);
			int intSetting2 = GlobalConfig.GetIntSetting("SatellitePowerDeocclusionChance", 5);
			for (int i = 0; i < Turns; i++)
			{
				if (BroadcastPowerReceiver.SatellitePowerOccluded)
				{
					if (intSetting2.in1000())
					{
						BroadcastPowerReceiver.SatellitePowerOccluded = false;
						BroadcastPowerReceiver.SatellitePowerOcclusionReason = null;
					}
				}
				else if (intSetting.in1000())
				{
					BroadcastPowerReceiver.SatellitePowerOccluded = true;
					BroadcastPowerReceiver.SatellitePowerOcclusionReason = null;
				}
			}
			BroadcastPowerReceiver.SatellitePowerOcclusionCheckTurn = The.CurrentTurn;
		}

		// Token: 0x0600B413 RID: 46099 RVA: 0x0041B07D File Offset: 0x0041927D
		public BroadcastPowerReceiver()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B414 RID: 46100 RVA: 0x0041B0B8 File Offset: 0x004192B8
		public override bool SameAs(IPart p)
		{
			BroadcastPowerReceiver broadcastPowerReceiver = p as BroadcastPowerReceiver;
			return broadcastPowerReceiver.ChargeRate == this.ChargeRate && broadcastPowerReceiver.CanReceiveSatellitePower == this.CanReceiveSatellitePower && broadcastPowerReceiver.MaxSatellitePowerDepth == this.MaxSatellitePowerDepth && !(broadcastPowerReceiver.SatelliteWorld != this.SatelliteWorld) && broadcastPowerReceiver.IgnoresSatellitePowerOcclusion == this.IgnoresSatellitePowerOcclusion && broadcastPowerReceiver.Obvious == this.Obvious && broadcastPowerReceiver.SatellitePowerOcclusionReadout == this.SatellitePowerOcclusionReadout && base.SameAs(p);
		}

		// Token: 0x0600B415 RID: 46101 RVA: 0x0041B148 File Offset: 0x00419348
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<QueryBroadcastDrawEvent>.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B416 RID: 46102 RVA: 0x0041B170 File Offset: 0x00419370
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Obvious || IComponent<GameObject>.ThePlayer.GetIntProperty("TechScannerEquipped", 0) > 0)
			{
				E.Postfix.Append("\n{{rules|This object has a broadcast power receiver that can pick up electrical charge").Append(this.CanReceiveSatellitePower ? " either from satellites if not too far underground or" : "").Append(" from a nearby broadcast power transmitter.");
				base.AddStatusSummary(E.Postfix);
				E.Postfix.Append("}}");
			}
			if (!this.IgnoresSatellitePowerOcclusion && BroadcastPowerReceiver.SatellitePowerOccluded && (this.SatellitePowerOcclusionReadout || Scanning.HasScanningFor(IComponent<GameObject>.ThePlayer, Scanning.Scan.Tech)) && this.CouldReceivePowerFromSatellite(null))
			{
				if (BroadcastPowerReceiver.SatellitePowerOcclusionReason == null)
				{
					BroadcastPowerReceiver.SatellitePowerOcclusionReason = HistoricStringExpander.ExpandString("<spice.satellitePower.occlusionReasons.!random>", null, null, null, null);
				}
				E.Postfix.AppendRules("\n{{rules|Satellite broadcast power is currently occluded by {{R|" + BroadcastPowerReceiver.SatellitePowerOcclusionReason + "}}.}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B417 RID: 46103 RVA: 0x0041B257 File Offset: 0x00419457
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ReceiveCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B418 RID: 46104 RVA: 0x0041B27C File Offset: 0x0041947C
		public override bool HandleEvent(QueryBroadcastDrawEvent E)
		{
			if (base.IsReady(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Draw += this.ChargeRate;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B419 RID: 46105 RVA: 0x0041B2CC File Offset: 0x004194CC
		public bool CouldReceivePowerFromSatellite(Cell C = null)
		{
			if (C == null)
			{
				C = this.GetAnyBasisCell();
				if (C == null)
				{
					return false;
				}
			}
			Zone zone = C.ParentZone;
			InteriorZone interiorZone = zone as InteriorZone;
			if (interiorZone != null)
			{
				zone = interiorZone.ResolveBasisZone() ?? zone;
			}
			if (string.IsNullOrEmpty(this.SatelliteWorld))
			{
				if (zone == null)
				{
					return false;
				}
				this.SatelliteWorld = zone.ResolveZoneWorld();
				this.SatelliteWorlds = new List<string>(1) { this.SatelliteWorld };
			}
			else if (this.SatelliteWorld != "*")
			{
				if (this.SatelliteWorlds == null)
				{
					this.SatelliteWorlds = this.SatelliteWorld.CachedCommaExpansion();
				}
				if (zone == null || !this.SatelliteWorlds.Contains(zone.ResolveZoneWorld()))
				{
					return false;
				}
			}
			return zone.IsWorldMap() || zone.Z <= this.MaxSatellitePowerDepth || C.ConsideredOutside();
		}

		// Token: 0x0600B41A RID: 46106 RVA: 0x0041B3A2 File Offset: 0x004195A2
		public bool IsReceivingPowerFromSatellite(Cell C = null)
		{
			return this.CanReceiveSatellitePower && (this.IgnoresSatellitePowerOcclusion || !BroadcastPowerReceiver.SatellitePowerOccluded) && this.CouldReceivePowerFromSatellite(C);
		}

		// Token: 0x0600B41B RID: 46107 RVA: 0x0041B3C6 File Offset: 0x004195C6
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B41C RID: 46108 RVA: 0x0041B3C9 File Offset: 0x004195C9
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ReceiveCharge(Amount);
		}

		// Token: 0x0600B41D RID: 46109 RVA: 0x0041B3D4 File Offset: 0x004195D4
		public void ReceiveCharge(int Turns = 1)
		{
			Cell anyBasisCell = this.GetAnyBasisCell();
			if (anyBasisCell == null || (anyBasisCell.ParentZone != null && !anyBasisCell.ParentZone.IsActive()))
			{
				return;
			}
			BroadcastPowerReceiver.CheckSatellitePowerOcclusion(Turns);
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				if (this.IsReceivingPowerFromSatellite(anyBasisCell))
				{
					this.ParentObject.ChargeAvailable(this.ChargeRate, 0L, Turns, false, null);
					return;
				}
				if (!anyBasisCell.OnWorldMap())
				{
					int @for = CollectBroadcastChargeEvent.GetFor(this.ParentObject, anyBasisCell.ParentZone, anyBasisCell, this.ChargeRate, Turns);
					if (@for < this.ChargeRate)
					{
						int num = this.ChargeRate - @for;
						this.ParentObject.ChargeAvailable(num, 0L, Turns, false, null);
					}
				}
			}
		}

		// Token: 0x04004893 RID: 18579
		public int ChargeRate = 10;

		// Token: 0x04004894 RID: 18580
		public bool CanReceiveSatellitePower = true;

		// Token: 0x04004895 RID: 18581
		public int MaxSatellitePowerDepth = 12;

		// Token: 0x04004896 RID: 18582
		public string SatelliteWorld = "JoppaWorld";

		// Token: 0x04004897 RID: 18583
		public bool IgnoresSatellitePowerOcclusion;

		// Token: 0x04004898 RID: 18584
		public bool Obvious;

		// Token: 0x04004899 RID: 18585
		public bool SatellitePowerOcclusionReadout;

		// Token: 0x0400489A RID: 18586
		public List<string> SatelliteWorlds;
	}
}
