using System;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A01 RID: 2561
	public class Tier1Junk : IObjectBuilder
	{
		// Token: 0x06006CFA RID: 27898 RVA: 0x00247408 File Offset: 0x00245608
		public override void Apply(GameObject GO, string Context)
		{
			for (int i = 0; i < 3; i++)
			{
				if (25.in100())
				{
					GO.ReceiveObjectFromPopulation("Scrap 1", null, false, 0, 0, null, null, null, Context, null);
				}
			}
			for (int j = 0; j < 3; j++)
			{
				if (25.in100())
				{
					GO.ReceiveObjectFromPopulation("Junk 1", null, false, 0, 0, null, null, null, Context, null);
				}
			}
			if (5.in100())
			{
				GO.ReceiveObjectFromPopulation("Junk 2", null, false, 0, 0, null, null, null, Context, null);
			}
		}
	}
}
