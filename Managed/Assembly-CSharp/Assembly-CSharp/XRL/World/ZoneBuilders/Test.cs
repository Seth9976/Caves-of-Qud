using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF7 RID: 2807
	public class Test
	{
		// Token: 0x0600706F RID: 28783 RVA: 0x0028072C File Offset: 0x0027E92C
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					if (5.in100())
					{
						Z.GetCell(j, i).AddObject("Wall", null, null, null, null);
					}
					else if (5.in100())
					{
						Z.GetCell(j, i).AddObject("Battle Axe", null, null, null, null);
					}
					else if (5.in100())
					{
						Z.GetCell(j, i).AddObject("Wof", null, null, null, null).MakeActive(false);
					}
				}
			}
			return true;
		}
	}
}
