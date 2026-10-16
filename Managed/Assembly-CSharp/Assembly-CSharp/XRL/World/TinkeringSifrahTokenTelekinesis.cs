using System;
using XRL.World.Parts.Mutation;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200090A RID: 2314
	[Serializable]
	public class TinkeringSifrahTokenTelekinesis : SifrahToken
	{
		// Token: 0x06005869 RID: 22633 RVA: 0x001AC314 File Offset: 0x001AA514
		public TinkeringSifrahTokenTelekinesis()
		{
			this.Description = "telekinetic manipulation";
			this.Tile = "Items/sw_wind_turbine_2.bmp";
			this.RenderString = "\u00a8";
			this.ColorString = "&M";
			this.DetailColor = 'm';
		}

		// Token: 0x0600586A RID: 22634 RVA: 0x001AC350 File Offset: 0x001AA550
		public TinkeringSifrahTokenTelekinesis(string AltDesc)
			: this()
		{
			this.Description = AltDesc;
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x001AC360 File Offset: 0x001AA560
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.UsabilityCheckedThisTurn)
			{
				Telekinesis part = The.Player.GetPart<Telekinesis>();
				if (part == null || !part.Activate(true))
				{
					this.DisabledThisTurn = true;
					return false;
				}
			}
			return true;
		}
	}
}
