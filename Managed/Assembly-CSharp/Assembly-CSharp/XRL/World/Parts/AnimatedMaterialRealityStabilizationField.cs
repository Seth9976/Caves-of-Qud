using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012C8 RID: 4808
	[Serializable]
	public class AnimatedMaterialRealityStabilizationField : IPart
	{
		// Token: 0x0600BDAE RID: 48558 RVA: 0x00458105 File Offset: 0x00456305
		public AnimatedMaterialRealityStabilizationField()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 2000);
		}

		// Token: 0x0600BDAF RID: 48559 RVA: 0x0045811E File Offset: 0x0045631E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDB0 RID: 48560 RVA: 0x00458124 File Offset: 0x00456324
		public override bool FinalRender(RenderEvent E)
		{
			if (!base.Visible() || !E.ColorsVisible)
			{
				return true;
			}
			if (E.Tile != null)
			{
				return true;
			}
			if (!Options.DisableImposters)
			{
				return true;
			}
			int num = (XRLCore.CurrentFrame10 + this.FrameOffset) % 2000;
			if (num >= 250 && num <= 750)
			{
				E.BackgroundString = "^k";
				E.DetailColor = "b";
			}
			else if (num >= 1250 && num <= 1750)
			{
				E.BackgroundString = "^k";
				E.DetailColor = "B";
			}
			return true;
		}

		// Token: 0x0600BDB1 RID: 48561 RVA: 0x004581B8 File Offset: 0x004563B8
		public override bool Render(RenderEvent E)
		{
			if (!Options.DisableImposters)
			{
				return true;
			}
			E.Tile = "assets_content_textures_tiles_tile-dirt.png";
			int num = (XRLCore.CurrentFrame10 + this.FrameOffset) % 2000;
			if (num < 500)
			{
				E.RenderString = AnimatedMaterialRealityStabilizationField.AltRender;
				if (E.ColorsVisible)
				{
					E.ColorString = "&y";
				}
			}
			else if (num < 1000)
			{
				E.RenderString = AnimatedMaterialRealityStabilizationField.AltRender;
				if (E.ColorsVisible)
				{
					E.ColorString = "&K";
				}
			}
			else if (num < 1500)
			{
				E.RenderString = AnimatedMaterialRealityStabilizationField.AltRender;
				if (E.ColorsVisible)
				{
					E.ColorString = "&K";
				}
			}
			else
			{
				E.RenderString = this.ParentObject.Render.RenderString;
				if (E.ColorsVisible)
				{
					E.ColorString = "&Y";
				}
			}
			return true;
		}

		// Token: 0x0600BDB2 RID: 48562 RVA: 0x00458291 File Offset: 0x00456491
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x04004C52 RID: 19538
		public int FrameOffset;

		// Token: 0x04004C53 RID: 19539
		[NonSerialized]
		private static StringBuilder tileBuilder = new StringBuilder();

		// Token: 0x04004C54 RID: 19540
		[NonSerialized]
		private static string AltRender = "ú";
	}
}
