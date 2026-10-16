using System;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AA1 RID: 2721
	public class InteriorGround
	{
		// Token: 0x06006F1D RID: 28445 RVA: 0x0026826B File Offset: 0x0026646B
		public bool BuildZone(Zone Z)
		{
			Z.ClearReachableMap();
			Z.ForeachCell(delegate(Cell c)
			{
				c.AddObject("InteriorVoid", null, null, null, null);
				Rocky.Paint(c);
			});
			return true;
		}
	}
}
