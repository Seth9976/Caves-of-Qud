using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD5 RID: 2773
	public class GenericSolid
	{
		// Token: 0x06006FDF RID: 28639 RVA: 0x00277C3C File Offset: 0x00275E3C
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (this.ClearFirst)
					{
						Z.GetCell(j, i).Clear(null, false, false, null);
					}
					Z.GetCell(j, i).AddObject(this.Material, null, null, null, null);
				}
			}
			return true;
		}

		// Token: 0x04003376 RID: 13174
		public string Material = "Shale";

		// Token: 0x04003377 RID: 13175
		public bool ClearFirst = true;
	}
}
