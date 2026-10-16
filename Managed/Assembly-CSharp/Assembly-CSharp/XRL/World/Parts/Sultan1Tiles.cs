using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001262 RID: 4706
	[Serializable]
	public class Sultan1Tiles : IPart
	{
		// Token: 0x0600BAA9 RID: 47785 RVA: 0x004425ED File Offset: 0x004407ED
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BAAA RID: 47786 RVA: 0x004425F0 File Offset: 0x004407F0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BAAB RID: 47787 RVA: 0x00442608 File Offset: 0x00440808
		public override bool HandleEvent(EnteredCellEvent E)
		{
			try
			{
				if (!Options.DisableFloorTextureObjects)
				{
					Zone parentZone = E.Cell.ParentZone;
					for (int i = 0; i < parentZone.Height; i++)
					{
						for (int j = 0; j < parentZone.Width; j++)
						{
							Sultan1Tiles.Paint(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAAC RID: 47788 RVA: 0x00442674 File Offset: 0x00440874
		public static void Paint(Cell C)
		{
			if (string.IsNullOrEmpty(C.PaintTile))
			{
				C.PaintColorString = "&K";
				C.PaintTile = "assets_content_textures_tiles_sw_floor_diamonds.bmp";
				C.PaintDetailColor = "k";
				if (Stat.RandomCosmetic(1, 100) <= 5)
				{
					C.PaintDetailColor = "k";
				}
				if (Stat.RandomCosmetic(1, 100) <= 1)
				{
					C.PaintDetailColor = "W";
				}
				C.PaintRenderString = ".";
			}
		}
	}
}
