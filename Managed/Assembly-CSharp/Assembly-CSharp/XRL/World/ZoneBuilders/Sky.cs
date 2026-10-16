using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AD3 RID: 2771
	public class Sky
	{
		// Token: 0x06006FDB RID: 28635 RVA: 0x00277A4C File Offset: 0x00275C4C
		public bool BuildZone(Zone Z)
		{
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			foreach (Cell cell in Z.GetCells())
			{
				cell.AddObject("Air", null, null, null, null);
			}
			Z.ClearReachableMap();
			return Z.BuildReachableMap(0, 0, false) >= 400;
		}
	}
}
