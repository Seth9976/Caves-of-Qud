using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012CE RID: 4814
	[Serializable]
	public class AnimatedMaterialOverlandWater : IPart
	{
		// Token: 0x0600BDCA RID: 48586 RVA: 0x004593B6 File Offset: 0x004575B6
		public AnimatedMaterialOverlandWater()
		{
			this.nFrameOffset = Stat.RandomCosmetic(0, 60);
			this.Rushing = true;
		}

		// Token: 0x0600BDCB RID: 48587 RVA: 0x004593D3 File Offset: 0x004575D3
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDCC RID: 48588 RVA: 0x004593D8 File Offset: 0x004575D8
		public override bool Render(RenderEvent E)
		{
			if (Globals.RenderMode == RenderModeType.Tiles)
			{
				return true;
			}
			if (this.ParentObject.Physics.IsFreezing())
			{
				E.RenderString = "~";
				E.TileVariantColors("&c^K", "&c", "K");
			}
			else
			{
				long num = (XRLCore.FrameTimer.ElapsedMilliseconds + (long)(this.nFrameOffset * 100)) % 20000L;
				if (Stat.RandomCosmetic(1, 60) == 1)
				{
					if (num < 5000L)
					{
						this.ParentObject.Render.RenderString = "÷";
					}
					else if (num < 10000L)
					{
						this.ParentObject.Render.RenderString = "~";
					}
					else if (num < 15000L)
					{
						this.ParentObject.Render.RenderString = "÷";
					}
					else
					{
						this.ParentObject.Render.RenderString = "~";
					}
				}
				if (Stat.RandomCosmetic(1, 600) == 1)
				{
					E.RenderString = "~";
					E.TileVariantColors("&Y^k", "&Y", "k");
				}
			}
			return true;
		}

		// Token: 0x04004C70 RID: 19568
		public int nFrameOffset;

		// Token: 0x04004C71 RID: 19569
		public bool Rushing;

		// Token: 0x04004C72 RID: 19570
		public bool Fresh;

		// Token: 0x04004C73 RID: 19571
		public bool Acid;

		// Token: 0x04004C74 RID: 19572
		public bool Bloody;
	}
}
