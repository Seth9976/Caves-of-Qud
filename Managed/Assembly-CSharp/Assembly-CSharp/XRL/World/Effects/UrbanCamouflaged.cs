using System;
using XRL.Core;

namespace XRL.World.Effects
{
	/// This class is not used in the base game.
	// Token: 0x02000DA6 RID: 3494
	[Serializable]
	public class UrbanCamouflaged : ICamouflageEffect
	{
		// Token: 0x06008B82 RID: 35714 RVA: 0x00319546 File Offset: 0x00317746
		public UrbanCamouflaged()
		{
			this.DisplayName = "{{urban camouflage|urban camouflage}}";
		}

		// Token: 0x06008B83 RID: 35715 RVA: 0x00319559 File Offset: 0x00317759
		public override string GetStateDescription()
		{
			return "{{urban camouflage|camouflage for an urban environment}}";
		}

		// Token: 0x06008B84 RID: 35716 RVA: 0x00319560 File Offset: 0x00317760
		public override bool Render(RenderEvent E)
		{
			int currentFrameLong = XRLCore.CurrentFrameLong10;
			if ((currentFrameLong >= 9000 && currentFrameLong < 10000) || (currentFrameLong >= 0 && currentFrameLong < 1000))
			{
				E.ColorString = "&y";
				E.DetailColor = "K";
			}
			else if (currentFrameLong >= 5000 && currentFrameLong < 7000)
			{
				E.ColorString = "&K";
				E.DetailColor = "y";
			}
			return true;
		}

		// Token: 0x06008B85 RID: 35717 RVA: 0x003195CE File Offset: 0x003177CE
		public override bool EnablesCamouflage(GameObject GO)
		{
			return GO.HasTagOrProperty("EnableUrbanCamouflage");
		}
	}
}
