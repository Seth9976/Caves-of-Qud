using System;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F4 RID: 2292
	[Serializable]
	public class SocialSifrahTokenBit : TinkeringSifrahTokenBit
	{
		// Token: 0x060057F2 RID: 22514 RVA: 0x001AA792 File Offset: 0x001A8992
		public SocialSifrahTokenBit()
		{
			this.Description = "gift bit";
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x001AA7A5 File Offset: 0x001A89A5
		public SocialSifrahTokenBit(BitType bitType)
			: base(bitType)
		{
			this.Description = "gift " + bitType.Description;
		}
	}
}
