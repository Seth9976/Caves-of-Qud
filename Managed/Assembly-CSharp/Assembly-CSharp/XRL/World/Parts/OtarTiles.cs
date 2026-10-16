using System;
using Genkit;

namespace XRL.World.Parts
{
	// Token: 0x02001277 RID: 4727
	[Serializable]
	public class OtarTiles : IPart
	{
		// Token: 0x0600BB29 RID: 47913 RVA: 0x00445D44 File Offset: 0x00443F44
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB2A RID: 47914 RVA: 0x00445D47 File Offset: 0x00443F47
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB2B RID: 47915 RVA: 0x00445D5D File Offset: 0x00443F5D
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupTiles();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB2C RID: 47916 RVA: 0x00445D6C File Offset: 0x00443F6C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneLoaded")
			{
				this.SetupTiles();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB2D RID: 47917 RVA: 0x00445D8D File Offset: 0x00443F8D
		public static int GetSeededRange(string Seed, int Low, int High)
		{
			return new Random(Hash.String(Seed)).Next(Low, High);
		}

		// Token: 0x0600BB2E RID: 47918 RVA: 0x00445DA4 File Offset: 0x00443FA4
		private void SetupTiles()
		{
			foreach (Cell cell in this.ParentObject.CurrentZone.GetCells())
			{
				if (!cell.HasWall())
				{
					cell.PaintTile = "tiles/sw_floor_squares.bmp";
					cell.PaintTileColor = "&r";
					cell.PaintRenderString = "-";
					cell.PaintColorString = "&r";
				}
			}
			this.ParentObject.RemovePart(this);
		}
	}
}
