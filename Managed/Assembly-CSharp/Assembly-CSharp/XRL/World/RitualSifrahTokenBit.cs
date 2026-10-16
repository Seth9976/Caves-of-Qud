using System;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008C9 RID: 2249
	[Serializable]
	public class RitualSifrahTokenBit : SocialSifrahTokenBit
	{
		// Token: 0x06005769 RID: 22377 RVA: 0x001A8845 File Offset: 0x001A6A45
		public RitualSifrahTokenBit()
		{
			this.Description = "offer bit";
		}

		// Token: 0x0600576A RID: 22378 RVA: 0x001A8858 File Offset: 0x001A6A58
		public RitualSifrahTokenBit(BitType bitType)
			: base(bitType)
		{
			this.Description = "offer " + bitType.Description;
		}
	}
}
