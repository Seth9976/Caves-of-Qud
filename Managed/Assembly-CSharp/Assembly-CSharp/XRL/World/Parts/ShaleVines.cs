using System;

namespace XRL.World.Parts
{
	// Token: 0x020012EB RID: 4843
	[Serializable]
	public class ShaleVines : IPart
	{
		// Token: 0x0600BE53 RID: 48723 RVA: 0x0045B878 File Offset: 0x00459A78
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BE54 RID: 48724 RVA: 0x0045B890 File Offset: 0x00459A90
		public void GrowVines()
		{
			if (this.ParentObject.CurrentCell != null)
			{
				if (this.ParentObject.CurrentCell.AnyAdjacentCell((Cell c) => c.HasObjectWithPart("LiquidVolume") && c.HasOpenLiquidVolume() && c.GetOpenLiquidVolume().IsWaterPuddle() && c.HasWadingDepthLiquid(), true))
				{
					this.ParentObject.SetStringProperty("PaintedWall", "vineshale1", false);
					this.ParentObject.SetStringProperty("PaintedWallExtension", ".png", false);
					this.ParentObject.RemoveStringProperty("paintedWallSubstring");
					this.ParentObject.Render.ColorString = "&r^g";
					this.ParentObject.Render.TileColor = "&r";
					this.ParentObject.Render.DetailColor = "g";
				}
			}
		}

		// Token: 0x0600BE55 RID: 48725 RVA: 0x0045B95A File Offset: 0x00459B5A
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.GrowVines();
			return base.HandleEvent(E);
		}
	}
}
