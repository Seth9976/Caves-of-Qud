using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004F6 RID: 1270
	[Serializable]
	public class CryptOfLandlordsAnchorSystem : ITombAnchorSystem
	{
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x00108124 File Offset: 0x00106324
		public override int Depth
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x00108128 File Offset: 0x00106328
		public override string GetAnchorZoneFor(Zone Z)
		{
			if (Z.X == 2)
			{
				if (Z.Y == 0)
				{
					return "JoppaWorld.53.3.2.2.9";
				}
				if (Z.Y == 2)
				{
					return "JoppaWorld.53.3.2.0.9";
				}
			}
			if (!50.in100())
			{
				return "JoppaWorld.53.3.2.0.9";
			}
			return "JoppaWorld.53.3.2.2.9";
		}
	}
}
