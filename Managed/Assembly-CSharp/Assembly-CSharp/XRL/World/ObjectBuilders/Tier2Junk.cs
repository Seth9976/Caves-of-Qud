using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A02 RID: 2562
	public class Tier2Junk : IObjectBuilder
	{
		// Token: 0x06006CFC RID: 27900 RVA: 0x00247490 File Offset: 0x00245690
		public override void Apply(GameObject GO, string Context)
		{
			for (int i = 0; i < 3; i++)
			{
				if (25.in100())
				{
					GO.ReceiveObjectFromPopulation("Junk 2", null, false, 0, 0, null, null, null, Context, null);
				}
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 3", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
