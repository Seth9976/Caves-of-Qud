using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001312 RID: 4882
	[Serializable]
	public class ConveyorPad : IPart
	{
		// Token: 0x0600BEFB RID: 48891 RVA: 0x0045ED1C File Offset: 0x0045CF1C
		public ConveyorPad()
		{
			this.nFrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BEFC RID: 48892 RVA: 0x0045ED48 File Offset: 0x0045CF48
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BEFD RID: 48893 RVA: 0x0045ED4B File Offset: 0x0045CF4B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BEFE RID: 48894 RVA: 0x0045ED61 File Offset: 0x0045CF61
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.CheckConnections();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BEFF RID: 48895 RVA: 0x0045ED70 File Offset: 0x0045CF70
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BF00 RID: 48896 RVA: 0x0045ED7C File Offset: 0x0045CF7C
		public override bool Render(RenderEvent E)
		{
			Physics physics = this.ParentObject.Physics;
			Render render = this.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + this.nFrameOffset) % 60;
			string text = "º";
			if (this.Connections == "S")
			{
				text = "º";
			}
			if (this.Connections == "E")
			{
				text = "Í";
			}
			if (this.Connections == "W")
			{
				text = "Í";
			}
			if (this.Connections == "EW")
			{
				text = "Í";
			}
			if (this.Connections == "WE")
			{
				text = "Í";
			}
			if (this.Connections == "NW" || this.Connections == "WN")
			{
				text = "¼";
			}
			if (this.Connections == "NE" || this.Connections == "EN")
			{
				text = "È";
			}
			if (this.Connections == "SW" || this.Connections == "WS")
			{
				text = "»";
			}
			if (this.Connections == "SE" || this.Connections == "ES")
			{
				text = "É";
			}
			if (this.Connections == "NWE")
			{
				text = "Ê";
			}
			if (this.Connections == "SWE")
			{
				text = "Ë";
			}
			if (this.Connections == "NSE")
			{
				text = "Ê";
			}
			if (this.Connections == "NSW")
			{
				text = "¹";
			}
			if (this.Connections == "NSEW")
			{
				text = "Î";
			}
			string text2 = "Tiles/sw_conveyor_w_";
			if (this.Connections == "S")
			{
				text2 = "Tiles/sw_conveyor_w_";
			}
			if (this.Connections == "E")
			{
				text2 = "Tiles/sw_conveyor_n_";
			}
			if (this.Connections == "W")
			{
				text2 = "Tiles/sw_conveyor_n_";
			}
			if (this.Connections == "EW")
			{
				text2 = "Tiles/sw_conveyor_n_";
			}
			if (this.Connections == "WE")
			{
				text2 = "Tiles/sw_conveyor_n_";
			}
			if (this.Connections == "NW" || this.Connections == "WN")
			{
				text2 = "Tiles/sw_conveyor_se_";
			}
			if (this.Connections == "NE" || this.Connections == "EN")
			{
				text2 = "Tiles/sw_conveyor_sw_";
			}
			if (this.Connections == "SW" || this.Connections == "WS")
			{
				text2 = "Tiles/sw_conveyor_ne_";
			}
			if (this.Connections == "SE" || this.Connections == "ES")
			{
				text2 = "Tiles/sw_conveyor_nw_";
			}
			if (this.Connections == "NWE")
			{
				text2 = "Tiles/sw_conveyor_ne_";
			}
			if (this.Connections == "SWE")
			{
				text2 = "Tiles/sw_conveyor_se_";
			}
			if (this.Connections == "NSE")
			{
				text2 = "Tiles/sw_conveyor_sw_";
			}
			if (this.Connections == "NSW")
			{
				text2 = "Tiles/sw_conveyor_se_";
			}
			if (this.Connections == "NSEW")
			{
				text2 = "Tiles/sw_conveyor_n_";
			}
			if (this.tileTickTimer == 0L)
			{
				this.tileTickTimer = IComponent<GameObject>.wallTime;
			}
			int num2 = 1;
			if (this.Connections == "NS" && this.Direction == "S")
			{
				num2 = -1;
			}
			if (this.Connections == "SN" && this.Direction == "S")
			{
				num2 = -1;
			}
			if (this.Connections == "WS" && this.Direction == "W")
			{
				num2 = -1;
			}
			if (this.Connections == "SW" && this.Direction == "W")
			{
				num2 = -1;
			}
			if (this.Connections == "NW" && this.Direction == "N")
			{
				num2 = -1;
			}
			if (this.Connections == "WN" && this.Direction == "N")
			{
				num2 = -1;
			}
			if (this.Connections == "EN" && this.Direction == "E")
			{
				num2 = -1;
			}
			if (this.Connections == "NE" && this.Direction == "E")
			{
				num2 = -1;
			}
			if (this.Connections == "SE" && this.Direction == "S")
			{
				num2 = -1;
			}
			if (this.Connections == "ES" && this.Direction == "S")
			{
				num2 = -1;
			}
			if (this.Connections == "WE" && this.Direction == "W")
			{
				num2 = -1;
			}
			if (this.Connections == "EW" && this.Direction == "W")
			{
				num2 = -1;
			}
			if ((this.lastImpuse <= 0L || this.lastImpuse >= The.Game.Turns - 2L) && IComponent<GameObject>.wallTime - this.tileTickTimer >= 150L)
			{
				this.nTile += num2;
				this.tileTickTimer = IComponent<GameObject>.wallTime;
			}
			if (this.nTile > 4)
			{
				this.nTile = 1;
			}
			else if (this.nTile < 1)
			{
				this.nTile = 4;
			}
			E.Tile = text2 + this.nTile.ToString() + ".bmp";
			string text3;
			if (Globals.RenderMode == RenderModeType.Tiles)
			{
				text3 = "&y";
			}
			else if (num < 15)
			{
				E.RenderString = text;
				text3 = "&K";
			}
			else if (num < 30)
			{
				E.RenderString = text;
				text3 = "&y";
			}
			else if (num < 45)
			{
				E.RenderString = text;
				text3 = "&Y";
			}
			else
			{
				E.RenderString = text;
				text3 = "&y";
			}
			if (!text3.IsNullOrEmpty())
			{
				E.ApplyColors(text3, ConveyorPad.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x0600BF01 RID: 48897 RVA: 0x0045F3D4 File Offset: 0x0045D5D4
		public bool CanBeMovedByConveyor(GameObject obj)
		{
			return obj != this.ParentObject && obj.IsReal && !obj.HasTag("NoConveyor") && !obj.HasPart<Door>() && !obj.HasPart<LiquidVolume>() && obj.GetMatterPhase() < 3 && !obj.HasPart<Forcefield>() && !obj.HasPart<CryoZone>() && !obj.HasPart<PyroZone>() && !obj.HasTagOrProperty("ExcavatoryTerrainFeature") && obj.CanBeInvoluntarilyMoved() && obj.PhaseAndFlightMatches(this.ParentObject);
		}

		// Token: 0x0600BF02 RID: 48898 RVA: 0x0045F468 File Offset: 0x0045D668
		public bool ConveyorImpulse(int Timing, List<GameObject> Pads)
		{
			if (Pads.Contains(this.ParentObject))
			{
				return true;
			}
			if (base.IsEMPed() || base.IsBroken() || base.IsRusted())
			{
				return true;
			}
			this.lastImpuse = The.Game.Turns;
			this.CheckConnections();
			Pads.Add(this.ParentObject);
			this.nFrameOffset = Timing;
			Timing -= 10;
			if (Timing < 0)
			{
				Timing = 60;
			}
			if (this.ParentObject.CurrentCell != null)
			{
				Cell cellFromDirection = this.ParentObject.CurrentCell.GetCellFromDirection(this.Direction, !this.ParentObject.IsVisible());
				if (cellFromDirection != null)
				{
					foreach (GameObject gameObject in cellFromDirection.GetObjectsWithPartReadonly("ConveyorPad"))
					{
						gameObject.GetPart<ConveyorPad>().ConveyorImpulse(Timing, Pads);
					}
				}
				foreach (GameObject gameObject2 in this.ParentObject.CurrentCell.GetObjectsWithPartReadonly("Physics"))
				{
					if (this.CanBeMovedByConveyor(gameObject2))
					{
						gameObject2.Move(this.Direction, true, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
					}
				}
			}
			return true;
		}

		// Token: 0x0600BF03 RID: 48899 RVA: 0x0045F5E0 File Offset: 0x0045D7E0
		private void CheckConnections()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				this.Connections = this.Direction;
				Cell cell = currentCell.GetCellFromDirection("N", true);
				if (cell != null && cell.HasObjectWithPart("ConveyorPad") && cell.GetFirstObjectWithPart("ConveyorPad").GetPart<ConveyorPad>().Direction == "S")
				{
					this.Connections = this.Direction + "N";
				}
				cell = currentCell.GetCellFromDirection("S", true);
				if (cell != null && cell.HasObjectWithPart("ConveyorPad") && cell.GetFirstObjectWithPart("ConveyorPad").GetPart<ConveyorPad>().Direction == "N")
				{
					this.Connections = this.Direction + "S";
				}
				cell = currentCell.GetCellFromDirection("E", true);
				if (cell != null && cell.HasObjectWithPart("ConveyorPad") && cell.GetFirstObjectWithPart("ConveyorPad").GetPart<ConveyorPad>().Direction == "W")
				{
					this.Connections = this.Direction + "E";
				}
				cell = currentCell.GetCellFromDirection("W", true);
				if (cell != null && cell.HasObjectWithPart("ConveyorPad") && cell.GetFirstObjectWithPart("ConveyorPad").GetPart<ConveyorPad>().Direction == "E")
				{
					this.Connections = this.Direction + "W";
				}
			}
		}

		// Token: 0x04004D00 RID: 19712
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004D01 RID: 19713
		public string Direction = "N";

		// Token: 0x04004D02 RID: 19714
		public string Connections = "";

		// Token: 0x04004D03 RID: 19715
		public int nFrameOffset;

		// Token: 0x04004D04 RID: 19716
		[NonSerialized]
		private long lastImpuse;

		// Token: 0x04004D05 RID: 19717
		[NonSerialized]
		private long tileTickTimer;

		// Token: 0x04004D06 RID: 19718
		[NonSerialized]
		private int nTile;
	}
}
