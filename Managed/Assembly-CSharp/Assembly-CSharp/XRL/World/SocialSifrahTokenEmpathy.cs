using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F0 RID: 2288
	[Serializable]
	public class SocialSifrahTokenEmpathy : SifrahToken
	{
		// Token: 0x060057E4 RID: 22500 RVA: 0x001AA553 File Offset: 0x001A8753
		public SocialSifrahTokenEmpathy()
		{
			this.Description = "subtly employ empathy";
			this.Tile = "Items/sw_esper.bmp";
			this.RenderString = "÷";
			this.ColorString = "&M";
			this.DetailColor = 'W';
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x001AA58F File Offset: 0x001A878F
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.UsabilityCheckedThisTurn && !The.Player.CanMakeEmpathicContactWith(ContextObject))
			{
				this.DisabledThisTurn = true;
				return false;
			}
			return true;
		}
	}
}
