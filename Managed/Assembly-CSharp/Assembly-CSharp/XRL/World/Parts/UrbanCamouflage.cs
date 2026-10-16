using System;

namespace XRL.World.Parts
{
	// Token: 0x020010BB RID: 4283
	[Serializable]
	public class UrbanCamouflage : ICamouflage
	{
		// Token: 0x0600AA9B RID: 43675 RVA: 0x003E67C0 File Offset: 0x003E49C0
		public UrbanCamouflage()
		{
			base.EffectClass = "UrbanCamouflaged";
			this.Description = "Urban camouflage: This item grants the wearer +=level= DV near trash and furniture.";
		}
	}
}
