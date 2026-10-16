using System;
using XRL.Liquids;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008CA RID: 2250
	[Serializable]
	public class RitualSifrahTokenLiquid : SocialSifrahTokenLiquid
	{
		// Token: 0x0600576B RID: 22379 RVA: 0x001A8877 File Offset: 0x001A6A77
		public RitualSifrahTokenLiquid()
		{
			this.Description = "offer liquid";
		}

		// Token: 0x0600576C RID: 22380 RVA: 0x001A888C File Offset: 0x001A6A8C
		public RitualSifrahTokenLiquid(string LiquidID)
			: base(LiquidID)
		{
			BaseLiquid liquid = LiquidVolume.GetLiquid(this.LiquidID);
			this.Description = "offer " + liquid.GetName();
		}

		// Token: 0x0600576D RID: 22381 RVA: 0x001A88C7 File Offset: 0x001A6AC7
		public RitualSifrahTokenLiquid(GameObject ContextObject, bool WaterRitual = false)
			: this(SocialSifrahTokenLiquid.GetAppropriate(ContextObject, WaterRitual))
		{
		}
	}
}
