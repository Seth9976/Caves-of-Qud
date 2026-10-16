using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x020004F4 RID: 1268
	[Serializable]
	public class CryptOfPriestsAnchorSystem : ITombAnchorSystem
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060036B2 RID: 14002 RVA: 0x00108043 File Offset: 0x00106243
		public override int Depth
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x04001B52 RID: 6994
		public static List<string> priestsAllowedStairsZones = new List<string> { "JoppaWorld.53.3.0.0.7", "JoppaWorld.53.3.0.1.7", "JoppaWorld.53.3.1.0.7", "JoppaWorld.53.3.1.2.7", "JoppaWorld.53.3.2.0.7", "JoppaWorld.53.3.2.1.7", "JoppaWorld.53.3.2.2.7" };
	}
}
