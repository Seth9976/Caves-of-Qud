using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD7 RID: 2775
	public class CollapseAtLevel
	{
		// Token: 0x06006FE3 RID: 28643 RVA: 0x00277D30 File Offset: 0x00275F30
		public bool BuildZone(Zone Z)
		{
			if (The.Player.Statistics["Level"].Value < this.Level)
			{
				return true;
			}
			Z.ClearReachableMap(true);
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					Z.GetCell(j, i).Clear(null, false, false, null);
					Z.GetCell(j, i).AddObject("Halite", null, null, null, null);
				}
			}
			return true;
		}

		// Token: 0x04003378 RID: 13176
		public int Level = 5;
	}
}
