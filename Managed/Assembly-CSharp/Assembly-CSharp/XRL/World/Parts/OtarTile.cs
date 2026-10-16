using System;
using Genkit;

namespace XRL.World.Parts
{
	// Token: 0x02001278 RID: 4728
	[Serializable]
	public class OtarTile : IPart
	{
		// Token: 0x0600BB30 RID: 47920 RVA: 0x00445E44 File Offset: 0x00444044
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB31 RID: 47921 RVA: 0x00445E47 File Offset: 0x00444047
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB32 RID: 47922 RVA: 0x00445E5D File Offset: 0x0044405D
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB33 RID: 47923 RVA: 0x00445E6C File Offset: 0x0044406C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneLoaded")
			{
				this.SetupTile();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB34 RID: 47924 RVA: 0x00445E8D File Offset: 0x0044408D
		public static int GetSeededRange(string Seed, int Low, int High)
		{
			return new Random(Hash.String(Seed)).Next(Low, High);
		}

		// Token: 0x0600BB35 RID: 47925 RVA: 0x00445EA4 File Offset: 0x004440A4
		private void SetupTile()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			currentCell.PaintTile = "tiles/sw_floor_squares.bmp";
			currentCell.PaintTileColor = "&r";
			currentCell.PaintRenderString = "-";
			currentCell.PaintColorString = "&r";
			this.ParentObject.RemovePart(this);
		}
	}
}
