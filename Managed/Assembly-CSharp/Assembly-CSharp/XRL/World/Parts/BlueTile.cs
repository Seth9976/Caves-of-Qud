using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001260 RID: 4704
	[Serializable]
	public class BlueTile : IPart
	{
		// Token: 0x0600BA9F RID: 47775 RVA: 0x004423F3 File Offset: 0x004405F3
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BAA0 RID: 47776 RVA: 0x004423F6 File Offset: 0x004405F6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BAA1 RID: 47777 RVA: 0x0044240C File Offset: 0x0044060C
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
							BlueTile.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAA2 RID: 47778 RVA: 0x00442478 File Offset: 0x00440678
		public static void PaintCell(Cell C)
		{
			if ((C.X + C.Y) % 2 == 0)
			{
				C.PaintColorString = "&y";
				C.PaintTile = "terrain/sw_ground_dots1.png";
				C.PaintDetailColor = "Y";
				return;
			}
			C.PaintColorString = "&c";
			C.PaintTile = "terrain/sw_ground_dots1.png";
			C.PaintDetailColor = "C";
		}
	}
}
