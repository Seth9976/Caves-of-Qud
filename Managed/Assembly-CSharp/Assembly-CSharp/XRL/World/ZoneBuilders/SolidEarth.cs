using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD6 RID: 2774
	public class SolidEarth
	{
		// Token: 0x06006FE1 RID: 28641 RVA: 0x00277CB8 File Offset: 0x00275EB8
		public bool BuildZone(Zone Z)
		{
			for (int i = 0; i < Z.Width; i++)
			{
				for (int j = 0; j < Z.Height; j++)
				{
					Z.GetCell(i, j).Clear(null, false, false, null);
					Z.GetCell(i, j).AddObject(GameObjectFactory.Factory.CreateObject("Shale"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			return true;
		}
	}
}
