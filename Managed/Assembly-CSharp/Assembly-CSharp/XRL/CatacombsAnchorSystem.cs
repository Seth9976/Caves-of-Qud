using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x020004F5 RID: 1269
	[Serializable]
	public class CatacombsAnchorSystem : ITombAnchorSystem
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x001080B4 File Offset: 0x001062B4
		public override int Depth
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x04001B53 RID: 6995
		public static List<string> catacombsAllowedStairsZones = new List<string> { "JoppaWorld.53.3.0.0.11", "JoppaWorld.53.3.0.1.11", "JoppaWorld.53.3.1.0.11", "JoppaWorld.53.3.1.2.11", "JoppaWorld.53.3.2.0.11", "JoppaWorld.53.3.2.1.11", "JoppaWorld.53.3.2.2.11" };
	}
}
