using System;
using System.Linq;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012D3 RID: 4819
	[Serializable]
	public class PitMaterial : IPart
	{
		// Token: 0x0600BDDE RID: 48606 RVA: 0x004598FA File Offset: 0x00457AFA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID || ID == ZoneActivatedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600BDDF RID: 48607 RVA: 0x00459920 File Offset: 0x00457B20
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("PaintPitEdges");
			Registrar.Register("FirmPitEdges");
		}

		// Token: 0x0600BDE0 RID: 48608 RVA: 0x00459938 File Offset: 0x00457B38
		private bool isPitAdjacent(Cell ce)
		{
			return ce == null || ce.HasObject("Pit") || ce.HasObject("LazyPit") || ce.HasObjectWithTag("Pit");
		}

		// Token: 0x0600BDE1 RID: 48609 RVA: 0x00459964 File Offset: 0x00457B64
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (this.Lazy)
			{
				this.Lazy = false;
				this.FireEvent(Event.New("FirmPitEdges", 0, 0, 0));
				this.PaintPit();
			}
			return true;
		}

		// Token: 0x0600BDE2 RID: 48610 RVA: 0x00459990 File Offset: 0x00457B90
		private void PaintPit()
		{
			this.ParentObject.RemovePart<StairHighlight>();
			if (this.Lazy && this.ParentObject.CurrentZone != null && !this.ParentObject.CurrentZone.IsActive())
			{
				return;
			}
			if (this.ParentObject.Physics.CurrentCell.AllInDirections(Directions.DirectionList, 1, new Predicate<Cell>(this.isPitAdjacent)))
			{
				Cell cellFromDirection = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("D", false);
				if (cellFromDirection != null)
				{
					RenderEvent renderEvent = cellFromDirection.Render(true, LightLevel.Light, true, false, false, null);
					this.ParentObject.Render.RenderString = renderEvent.RenderString;
					if (cellFromDirection.HasObjectWithTag("PitDetailColor"))
					{
						this.ParentObject.Render.DetailColor = cellFromDirection.GetObjectsWithTag("PitDetailColor").FirstOrDefault<GameObject>().GetTag("PitDetailColor", null);
					}
					else
					{
						this.ParentObject.Render.DetailColor = "k";
					}
					this.ParentObject.Render.Tile = renderEvent.Tile;
				}
				else
				{
					this.ParentObject.Render.Tile = null;
					this.ParentObject.Render.RenderString = " ";
				}
				StairsDown stairsDown;
				if (this.ParentObject.TryGetPart<StairsDown>(out stairsDown))
				{
					stairsDown.PullDown = true;
				}
				this.ParentObject.Render.TileColor = "&K";
				this.ParentObject.Render.ColorString = "&K";
				this.ParentObject.Render.DetailColor = "k";
				this.ParentObject.RemoveStringProperty("PaintedWall");
				this.ParentObject.RemoveStringProperty("PaintWith");
				this.ParentObject.SetStringProperty("PaintWith", "PitVoid", false);
				this.ParentObject.Render.DisplayName = "open air";
				this.ParentObject.RemoveStringProperty("OverrideIArticle");
				this.ParentObject.Render.RenderLayer = 7;
			}
			else
			{
				StairsDown stairsDown2;
				if (this.ParentObject.TryGetPart<StairsDown>(out stairsDown2))
				{
					stairsDown2.PullDown = false;
				}
				this.ParentObject.SetStringProperty("PaintedWall", "tile_pit_", false);
				this.ParentObject.SetStringProperty("PaintWith", "!PitVoid", false);
				this.ParentObject.Render.DisplayName = "craggy ledge";
				this.ParentObject.SetStringProperty("OverrideIArticle", "a", false);
				this.ParentObject.Render.RenderString = "ú";
				string text = this.ParentObject.Physics.CurrentCell.ParentZone.GetZoneProperty("ChasmColor", "");
				if (text.IsNullOrEmpty())
				{
					text = this.ParentObject.Physics.CurrentCell.ParentZone.GetZoneProperty("primaryFloorColor1", "");
				}
				if (!text.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = text + "^k";
					this.ParentObject.Render.TileColor = text + "^k";
				}
				this.ParentObject.GetPart<Description>()._Short = "Ground material splinters and opens onto a void.";
				this.ParentObject.Render.RenderLayer = 1;
			}
			if (this.ParentObject.CurrentCell.HasObjectWithTagOrProperty("SuspendedPlatform"))
			{
				this.ParentObject.Render.RenderLayer = 1;
			}
		}

		// Token: 0x0600BDE3 RID: 48611 RVA: 0x00459CFF File Offset: 0x00457EFF
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			if (!this.Lazy)
			{
				this.FireEvent(Event.New("FirmPitEdges", 0, 0, 0));
				this.PaintPit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDE4 RID: 48612 RVA: 0x00459D2A File Offset: 0x00457F2A
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.PaintPit();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDE5 RID: 48613 RVA: 0x00459D3C File Offset: 0x00457F3C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "FirmPitEdges")
			{
				if (!this.Lazy && this.ParentObject.Physics.CurrentCell.AllInDirections(Directions.DirectionList, 1, new Predicate<Cell>(this.isPitAdjacent)))
				{
					StairsDown stairsDown;
					if (this.ParentObject.TryGetPart<StairsDown>(out stairsDown))
					{
						stairsDown.PullDown = true;
					}
					this.ParentObject.Render.DisplayName = "open air";
					this.ParentObject.RemoveStringProperty("OverrideIArticle");
					this.ParentObject.Render.RenderLayer = 7;
				}
				else
				{
					StairsDown stairsDown2;
					if (this.ParentObject.TryGetPart<StairsDown>(out stairsDown2))
					{
						stairsDown2.PullDown = false;
					}
					this.ParentObject.Render.DisplayName = "craggy ledge";
					this.ParentObject.SetStringProperty("OverrideIArticle", "a", false);
					this.ParentObject.Render.RenderString = "ú";
					this.ParentObject.GetPart<Description>()._Short = "Ground material splinters and opens onto a void.";
					this.ParentObject.Render.RenderLayer = 0;
				}
				if (this.ParentObject.CurrentCell.HasObjectWithTagOrProperty("SuspendedPlatform"))
				{
					this.ParentObject.Render.RenderLayer = 0;
				}
			}
			else if (E.ID == "PaintPitEdges")
			{
				this.PaintPit();
			}
			return true;
		}

		// Token: 0x04004C7A RID: 19578
		public bool Lazy;
	}
}
