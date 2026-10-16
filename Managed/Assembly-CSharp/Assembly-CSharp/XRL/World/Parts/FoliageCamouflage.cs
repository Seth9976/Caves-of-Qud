using System;

namespace XRL.World.Parts
{
	// Token: 0x0200102E RID: 4142
	[Serializable]
	public class FoliageCamouflage : ICamouflage
	{
		// Token: 0x0600A4EB RID: 42219 RVA: 0x003C4A4F File Offset: 0x003C2C4F
		public FoliageCamouflage()
		{
			base.EffectClass = "FoliageCamouflaged";
			this.Description = "Foliage camouflage: This item grants the wearer +=level= DV in foliage.";
		}
	}
}
