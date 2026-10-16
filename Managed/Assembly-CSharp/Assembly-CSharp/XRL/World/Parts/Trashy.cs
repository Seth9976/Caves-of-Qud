using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200125B RID: 4699
	[Serializable]
	public class Trashy : IPart
	{
		// Token: 0x0600BA86 RID: 47750 RVA: 0x00441E70 File Offset: 0x00440070
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA87 RID: 47751 RVA: 0x00441E73 File Offset: 0x00440073
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA88 RID: 47752 RVA: 0x00441E8C File Offset: 0x0044008C
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
							Trashy.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA89 RID: 47753 RVA: 0x00441EF8 File Offset: 0x004400F8
		public static void PaintCell(Cell C)
		{
			C.PaintTile = "Tiles/tile-dirt1.png";
			int num = Stat.Random(1, 8);
			if (num <= 5)
			{
				C.PaintColorString = "&g";
			}
			else if (num == 6)
			{
				C.PaintColorString = "&c";
			}
			else if (num == 7)
			{
				C.PaintColorString = "&w";
			}
			else
			{
				C.PaintColorString = "&g";
			}
			C.PaintRenderString = "ú";
		}
	}
}
