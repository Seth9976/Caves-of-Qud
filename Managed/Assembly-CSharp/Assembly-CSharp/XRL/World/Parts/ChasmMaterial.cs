using System;
using System.Linq;
using ConsoleLib.Console;

namespace XRL.World.Parts
{
	// Token: 0x020012D2 RID: 4818
	[Serializable]
	public class ChasmMaterial : IPart
	{
		// Token: 0x0600BDD7 RID: 48599 RVA: 0x00459661 File Offset: 0x00457861
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID || ID == EnteredCellEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600BDD8 RID: 48600 RVA: 0x00459687 File Offset: 0x00457887
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (this.Lazy)
			{
				this.Lazy = false;
				this.FireEvent(Event.New("FirmPitEdges", 0, 0, 0));
				this.PaintChasm();
			}
			return true;
		}

		// Token: 0x0600BDD9 RID: 48601 RVA: 0x004596B4 File Offset: 0x004578B4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckLazy();
			return true;
		}

		// Token: 0x0600BDDA RID: 48602 RVA: 0x004596BE File Offset: 0x004578BE
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckLazy();
			return true;
		}

		// Token: 0x0600BDDB RID: 48603 RVA: 0x004596C8 File Offset: 0x004578C8
		public bool PaintChasm()
		{
			Cell cellFromDirection = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("D", false);
			if (cellFromDirection == null)
			{
				return true;
			}
			if (cellFromDirection.ParentZone.ZoneID == this.ParentObject.Physics.CurrentCell.ParentZone.ZoneID)
			{
				if (cellFromDirection.HasObjectWithTag("PitDetailColor"))
				{
					this.ParentObject.Render.DetailColor = cellFromDirection.GetObjectsWithTag("PitDetailColor").FirstOrDefault<GameObject>().GetTag("PitDetailColor", null);
				}
				else
				{
					this.ParentObject.Render.DetailColor = "k";
				}
				this.ParentObject.Render.RenderString = " ";
				this.ParentObject.Render.TileColor = "&K";
				this.ParentObject.Render.ColorString = "&K";
				this.ParentObject.Render.Tile = null;
				return true;
			}
			RenderEvent renderEvent = cellFromDirection.Render(true, LightLevel.Light, true, false, false, null);
			this.ParentObject.Render.RenderString = renderEvent.RenderString;
			this.ParentObject.Render.DetailColor = "k";
			this.ParentObject.Render.TileColor = "&K";
			this.ParentObject.Render.ColorString = "&K";
			this.ParentObject.Render.Tile = renderEvent.Tile;
			return true;
		}

		// Token: 0x0600BDDC RID: 48604 RVA: 0x00459844 File Offset: 0x00457A44
		public bool CheckLazy()
		{
			if (this.Lazy && this.ParentObject.CurrentZone != null && !this.ParentObject.CurrentZone.IsActive())
			{
				string text = " ";
				this.ParentObject.Render.RenderString = ColorUtility.StripFormatting(text);
				this.ParentObject.Render.DetailColor = "k";
				this.ParentObject.Render.TileColor = "&K";
				this.ParentObject.Render.ColorString = "&K";
				this.ParentObject.Render.Tile = null;
				return true;
			}
			return this.PaintChasm();
		}

		// Token: 0x04004C79 RID: 19577
		public bool Lazy = true;
	}
}
