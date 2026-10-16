using System;

namespace XRL
{
	/// This class is not used in the base game.
	// Token: 0x02000519 RID: 1305
	public class SifrahSlotConfiguration : SifrahRenderable
	{
		// Token: 0x060037F2 RID: 14322 RVA: 0x00111570 File Offset: 0x0010F770
		public SifrahSlotConfiguration()
		{
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x00111578 File Offset: 0x0010F778
		public SifrahSlotConfiguration(string Description, string Tile, string RenderString, string ColorString, char DetailColor, int UptakeOrder = 0)
		{
			this.Description = Description;
			this.Tile = Tile;
			this.RenderString = RenderString;
			this.ColorString = ColorString;
			this.DetailColor = DetailColor;
			this.UptakeOrder = UptakeOrder;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x001115B0 File Offset: 0x0010F7B0
		public SifrahSlotConfiguration(SifrahSlotConfiguration From)
		{
			this.Description = From.Description;
			this.Tile = From.Tile;
			this.RenderString = From.RenderString;
			this.ColorString = From.ColorString;
			this.DetailColor = From.DetailColor;
			this.UptakeOrder = From.UptakeOrder;
		}

		// Token: 0x04001C09 RID: 7177
		public string Description;

		// Token: 0x04001C0A RID: 7178
		public int UptakeOrder;
	}
}
