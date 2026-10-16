using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x020004F3 RID: 1267
	[Serializable]
	public class CryptOfWarriorsAnchorSystem : ITombAnchorSystem
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x00107FC8 File Offset: 0x001061C8
		public override int Depth
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x04001B51 RID: 6993
		public static List<string> warriorsAllowedStairsZones = new List<string> { "JoppaWorld.53.3.0.0.8", "JoppaWorld.53.3.0.1.8", "JoppaWorld.53.3.0.2.8", "JoppaWorld.53.3.1.0.8", "JoppaWorld.53.3.1.2.8", "JoppaWorld.53.3.2.0.8", "JoppaWorld.53.3.2.1.8", "JoppaWorld.53.3.2.2.8" };
	}
}
