using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200102D RID: 4141
	[Serializable]
	public class FlipTileByAdjacentWall : IPart
	{
		// Token: 0x0600A4E6 RID: 42214 RVA: 0x003C494D File Offset: 0x003C2B4D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600A4E7 RID: 42215 RVA: 0x003C496C File Offset: 0x003C2B6C
		public void checkTile()
		{
			if (this.ParentObject.Render.Tile == null)
			{
				return;
			}
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			Cell cellFromDirection = currentCell.GetCellFromDirection(this.WallCheckDirection, true);
			Cell cellFromDirection2 = currentCell.GetCellFromDirection(Directions.GetOppositeDirection(this.WallCheckDirection), true);
			if (cellFromDirection != null && cellFromDirection2 != null)
			{
				bool partParameter = this.ParentObject.GetBlueprint(true).GetPartParameter<bool>("Render", "HFlip", false);
				if (cellFromDirection.HasWall())
				{
					if (!cellFromDirection2.HasWall())
					{
						this.ParentObject.Render.HFlip = !partParameter;
						return;
					}
				}
				else if (cellFromDirection2.HasWall())
				{
					this.ParentObject.Render.HFlip = partParameter;
				}
			}
		}

		// Token: 0x0600A4E8 RID: 42216 RVA: 0x003C4A1E File Offset: 0x003C2C1E
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.checkTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4E9 RID: 42217 RVA: 0x003C4A2D File Offset: 0x003C2C2D
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.checkTile();
			return base.HandleEvent(E);
		}

		// Token: 0x04004442 RID: 17474
		public string WallCheckDirection = "W";
	}
}
