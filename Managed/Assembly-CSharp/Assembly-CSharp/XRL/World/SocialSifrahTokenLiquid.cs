using System;
using XRL.Liquids;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F5 RID: 2293
	[Serializable]
	public class SocialSifrahTokenLiquid : TinkeringSifrahTokenLiquid
	{
		// Token: 0x060057F4 RID: 22516 RVA: 0x001AA7C4 File Offset: 0x001A89C4
		public SocialSifrahTokenLiquid()
		{
			this.Description = "share liquid";
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x001AA7D8 File Offset: 0x001A89D8
		public SocialSifrahTokenLiquid(string LiquidID)
			: base(LiquidID)
		{
			BaseLiquid liquid = LiquidVolume.GetLiquid(this.LiquidID);
			this.Description = "share " + liquid.GetName();
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x001AA813 File Offset: 0x001A8A13
		public SocialSifrahTokenLiquid(GameObject ContextObject, bool WaterRitual = false)
			: this(SocialSifrahTokenLiquid.GetAppropriate(ContextObject, WaterRitual))
		{
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x001AA824 File Offset: 0x001A8A24
		public static string GetAppropriate(GameObject ContextObject, bool WaterRitual = false)
		{
			string waterRitualLiquid = ContextObject.GetWaterRitualLiquid(The.Player);
			if (WaterRitual)
			{
				if (!waterRitualLiquid.IsNullOrEmpty())
				{
					return waterRitualLiquid;
				}
				return "water";
			}
			else
			{
				if (waterRitualLiquid == "water")
				{
					return "wine";
				}
				if (waterRitualLiquid == "oil")
				{
					return "gel";
				}
				if (waterRitualLiquid == "slime")
				{
					return "proteangunk";
				}
				if (waterRitualLiquid == "blood")
				{
					return "brainbrine";
				}
				if (!waterRitualLiquid.IsNullOrEmpty())
				{
					return waterRitualLiquid;
				}
				return "water";
			}
		}
	}
}
