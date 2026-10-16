using System;
using System.Collections.Generic;
using Genkit;
using XRL.EditorFormats.Map;

namespace XRL.World.Parts
{
	// Token: 0x02001304 RID: 4868
	[Serializable]
	public class MultiMapChunkPlacement : IPart
	{
		// Token: 0x0600BEB6 RID: 48822 RVA: 0x0045D381 File Offset: 0x0045B581
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BEB7 RID: 48823 RVA: 0x0045D384 File Offset: 0x0045B584
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BEB8 RID: 48824 RVA: 0x0045D39C File Offset: 0x0045B59C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				List<MapFile> list = new List<MapFile>();
				foreach (string text in this.Maps.Split(',', StringSplitOptions.None))
				{
					list.Add(MapFile.Resolve(text, true));
				}
				MapFileCell[,] array2 = new MapFileCell[(this.Width + this.ColumnPadding) * this.MapsWide - this.ColumnPadding, (this.Height + this.RowPadding) * this.MapsHigh - this.RowPadding];
				int num = 0;
				for (int j = 0; j < this.MapsHigh; j++)
				{
					for (int k = 0; k < this.MapsWide; k++)
					{
						MapFile mapFile = list[num];
						for (int l = 0; l < this.Width; l++)
						{
							for (int m = 0; m < this.Height; m++)
							{
								array2[l + k * this.Width + k * this.ColumnPadding, m + j * this.Height + j * this.RowPadding] = mapFile.Cells[l, m];
							}
						}
						num++;
					}
				}
				Zone parentZone = this.ParentObject.GetCurrentCell().ParentZone;
				Point2D pos2D = this.ParentObject.GetCurrentCell().Pos2D;
				int num2 = 2;
				if (base.HasTag("ChunkPadding"))
				{
					num2 = Convert.ToInt32("ChunkPadding");
				}
				int num3 = this.Width * this.MapsWide;
				int num4 = this.Height * this.MapsHigh;
				if (pos2D.x + num3 + num2 >= parentZone.Width)
				{
					pos2D.x -= pos2D.x + num3 + num2 - parentZone.Width;
				}
				if (pos2D.y + num4 + num2 >= parentZone.Height)
				{
					pos2D.y -= pos2D.y + num4 + num2 - parentZone.Height;
				}
				if (base.HasTag("ChunkHint") && base.GetTag("ChunkHint", "") == "Center")
				{
					pos2D.x = parentZone.Width / 2 - num3 / 2;
					pos2D.y = parentZone.Height / 2 - num4 / 2;
				}
				for (int n = 0; n < num4; n++)
				{
					for (int num5 = 0; num5 < num3; num5++)
					{
						Cell cell = parentZone.GetCell(pos2D.x + num5, pos2D.y + n);
						if (cell != null)
						{
							MapFileCell mapFileCell = array2[num5, n];
							if (mapFileCell != null)
							{
								mapFileCell.ApplyTo(cell, true, delegate(Cell X)
								{
									X.ClearWalls();
								}, null, null, null, null, null, null);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04004CC6 RID: 19654
		public string Maps;

		// Token: 0x04004CC7 RID: 19655
		public int MapsWide = 1;

		// Token: 0x04004CC8 RID: 19656
		public int MapsHigh = 1;

		// Token: 0x04004CC9 RID: 19657
		public int Width;

		// Token: 0x04004CCA RID: 19658
		public int Height;

		// Token: 0x04004CCB RID: 19659
		public int ColumnPadding;

		// Token: 0x04004CCC RID: 19660
		public int RowPadding;
	}
}
