using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200125D RID: 4701
	[Serializable]
	public class ConcreteHologramFloor : IPart
	{
		// Token: 0x0600BA90 RID: 47760 RVA: 0x00442049 File Offset: 0x00440249
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA91 RID: 47761 RVA: 0x0044204C File Offset: 0x0044024C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA92 RID: 47762 RVA: 0x00442064 File Offset: 0x00440264
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
							ConcreteHologramFloor.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA93 RID: 47763 RVA: 0x004420D0 File Offset: 0x004402D0
		public static void PaintCell(Cell C)
		{
			if (string.IsNullOrEmpty(C.PaintTile))
			{
				C.PaintColorString = "&y";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
				C.PaintColorString = "&b";
				C.PaintRenderString = "ú";
			}
		}
	}
}
