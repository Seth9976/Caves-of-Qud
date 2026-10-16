using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A85 RID: 2693
	public class FlagInside
	{
		// Token: 0x06006E92 RID: 28306 RVA: 0x0025E2D0 File Offset: 0x0025C4D0
		public bool BuildZone(Zone Z)
		{
			Z.SetInside(new bool?(true));
			foreach (GameObject gameObject in Z.GetObjects("DaylightWidget"))
			{
				gameObject.Destroy(null, false, false, null);
			}
			return true;
		}
	}
}
