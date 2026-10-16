using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008F6 RID: 2294
	[Serializable]
	public class SocialSifrahTokenCharge : TinkeringSifrahTokenCharge
	{
		// Token: 0x060057F8 RID: 22520 RVA: 0x001AA8AB File Offset: 0x001A8AAB
		public SocialSifrahTokenCharge()
		{
			this.Description = "offer {{C|1}} charge from an energy cell";
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x001AA8BE File Offset: 0x001A8ABE
		public SocialSifrahTokenCharge(int Amount)
			: base(Amount)
		{
			this.Description = "offer {{C|" + Amount.ToString() + "}} charge from an energy cell";
		}
	}
}
