using System;
using XRL.Liquids;

namespace XRL.World.Parts
{
	// Token: 0x020012EA RID: 4842
	[Serializable]
	public class RenderLiquidBackground : IPart
	{
		// Token: 0x0600BE50 RID: 48720 RVA: 0x0045B810 File Offset: 0x00459A10
		public override bool Render(RenderEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				GameObject openLiquidVolume = currentCell.GetOpenLiquidVolume();
				if (openLiquidVolume != null)
				{
					LiquidVolume liquidVolume = openLiquidVolume.LiquidVolume;
					if (liquidVolume != null)
					{
						BaseLiquid primaryLiquid = liquidVolume.GetPrimaryLiquid();
						if (primaryLiquid != null)
						{
							primaryLiquid.RenderBackgroundPrimary(liquidVolume, E);
						}
						BaseLiquid secondaryLiquid = liquidVolume.GetSecondaryLiquid();
						if (secondaryLiquid != null)
						{
							secondaryLiquid.RenderBackgroundSecondary(liquidVolume, E);
						}
					}
				}
			}
			return true;
		}
	}
}
