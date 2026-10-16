using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000909 RID: 2313
	[Serializable]
	public class TinkeringSifrahTokenPsychometry : SifrahToken
	{
		// Token: 0x06005866 RID: 22630 RVA: 0x001AC2A2 File Offset: 0x001AA4A2
		public TinkeringSifrahTokenPsychometry()
		{
			this.Description = "psychometric inspection";
			this.Tile = "Items/sw_esper.bmp";
			this.RenderString = "\u00a8";
			this.ColorString = "&m";
			this.DetailColor = 'M';
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x001AC2DE File Offset: 0x001AA4DE
		public TinkeringSifrahTokenPsychometry(string AltDesc)
			: this()
		{
			this.Description = AltDesc;
		}

		// Token: 0x06005868 RID: 22632 RVA: 0x001AC2ED File Offset: 0x001AA4ED
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.UsabilityCheckedThisTurn && !ContextObject.Physics.UsePsychometry(The.Player, ContextObject))
			{
				this.DisabledThisTurn = true;
				return false;
			}
			return true;
		}
	}
}
