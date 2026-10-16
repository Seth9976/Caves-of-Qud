using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A04 RID: 2564
	public class Tier3Junk : IObjectBuilder
	{
		// Token: 0x06006D00 RID: 27904 RVA: 0x00247554 File Offset: 0x00245754
		public override void Apply(GameObject GO, string Context)
		{
			for (int i = 0; i < 3; i++)
			{
				if (25.in100())
				{
					GO.ReceiveObjectFromPopulation("Junk 2", null, false, 0, 0, null, null, null, Context, null);
				}
			}
			for (int j = 0; j < 3; j++)
			{
				if (25.in100())
				{
					GO.ReceiveObjectFromPopulation("Junk 3", null, false, 0, 0, null, null, null, Context, null);
				}
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 4", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
