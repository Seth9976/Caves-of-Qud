using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008EF RID: 2287
	[Serializable]
	public class SocialSifrahTokenTelepathy : SifrahToken
	{
		// Token: 0x060057E2 RID: 22498 RVA: 0x001AA4F6 File Offset: 0x001A86F6
		public SocialSifrahTokenTelepathy()
		{
			this.Description = "subtly employ telepathy";
			this.Tile = "Items/sw_esper.bmp";
			this.RenderString = "÷";
			this.ColorString = "&b";
			this.DetailColor = 'C';
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x001AA532 File Offset: 0x001A8732
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.UsabilityCheckedThisTurn && !The.Player.CanMakeTelepathicContactWith(ContextObject))
			{
				this.DisabledThisTurn = true;
				return false;
			}
			return true;
		}
	}
}
