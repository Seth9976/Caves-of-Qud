using System;
using XRL.Core;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	/// This class is not used in the base game.
	// Token: 0x02000D39 RID: 3385
	[Serializable]
	public class FoliageCamouflaged : ICamouflageEffect
	{
		// Token: 0x0600859B RID: 34203 RVA: 0x003032B7 File Offset: 0x003014B7
		public FoliageCamouflaged()
		{
			this.DisplayName = "{{camouflage|camouflaged}}";
		}

		// Token: 0x0600859C RID: 34204 RVA: 0x003032CC File Offset: 0x003014CC
		public override bool Render(RenderEvent E)
		{
			int currentFrameLong = XRLCore.CurrentFrameLong10;
			if (currentFrameLong >= 1000 && currentFrameLong < 3000)
			{
				E.ColorString = "&g";
				E.DetailColor = "K";
			}
			else if (currentFrameLong >= 7000 && currentFrameLong < 9000)
			{
				E.ColorString = "&K";
				E.DetailColor = "g";
			}
			return true;
		}

		// Token: 0x0600859D RID: 34205 RVA: 0x0030332E File Offset: 0x0030152E
		public override bool EnablesCamouflage(GameObject GO)
		{
			return GO.HasPart<PlantProperties>();
		}
	}
}
