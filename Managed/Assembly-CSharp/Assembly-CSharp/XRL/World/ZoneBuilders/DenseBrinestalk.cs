using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B10 RID: 2832
	public class DenseBrinestalk
	{
		// Token: 0x060071A9 RID: 29097 RVA: 0x002A77F4 File Offset: 0x002A59F4
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (Z.GetCell(j, i).IsEmpty() && 25.in100())
					{
						Z.GetCell(j, i).AddObject("Brinestalk", null, null, null, null);
					}
				}
			}
			return true;
		}
	}
}
