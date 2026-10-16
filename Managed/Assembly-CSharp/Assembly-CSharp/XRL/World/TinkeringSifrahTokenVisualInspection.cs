using System;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000907 RID: 2311
	[Serializable]
	public class TinkeringSifrahTokenVisualInspection : SifrahToken
	{
		// Token: 0x06005864 RID: 22628 RVA: 0x001AC22A File Offset: 0x001AA42A
		public TinkeringSifrahTokenVisualInspection()
		{
			this.Description = "visual inspection";
			this.Tile = "Mutations/night_vision_mutation.bmp";
			this.RenderString = "\u001d";
			this.ColorString = "&y";
			this.DetailColor = 'B';
		}
	}
}
