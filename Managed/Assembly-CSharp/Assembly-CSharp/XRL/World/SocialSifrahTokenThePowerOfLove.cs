using System;
using XRL.World.Effects;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000905 RID: 2309
	[Serializable]
	public class SocialSifrahTokenThePowerOfLove : SifrahToken
	{
		// Token: 0x0600585C RID: 22620 RVA: 0x001AC143 File Offset: 0x001AA343
		public SocialSifrahTokenThePowerOfLove()
		{
			this.Description = "rhapsodize about your true love";
			this.Tile = "Items/ms_heart.png";
			this.RenderString = "\u0003";
			this.ColorString = "&W";
			this.DetailColor = 'R';
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x001AC17F File Offset: 0x001AA37F
		public static bool IsAvailable()
		{
			return The.Player.HasEffect<Lovesick>();
		}

		// Token: 0x0600585E RID: 22622 RVA: 0x001AC18B File Offset: 0x001AA38B
		public override int GetPowerup(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (Slot.CurrentMove == Slot.Token)
			{
				return 1;
			}
			return base.GetPowerup(Game, Slot, ContextObject);
		}
	}
}
