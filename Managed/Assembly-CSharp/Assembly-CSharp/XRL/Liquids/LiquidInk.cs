using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000557 RID: 1367
	[IsLiquid]
	[Serializable]
	public class LiquidInk : BaseLiquid
	{
		// Token: 0x06003AD7 RID: 15063 RVA: 0x0012107C File Offset: 0x0011F27C
		public LiquidInk()
			: base("ink")
		{
			this.FlameTemperature = 350;
			this.VaporTemperature = 1350;
			this.Combustibility = 30;
			this.PureElectricalConductivity = 40;
			this.MixedElectricalConductivity = 40;
			this.ThermalConductivity = 40;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 3;
			this.SlipperyWhenWet = true;
			this.SlipperyWhenFrozen = true;
			this.SlipperySaveTargetBase = 5;
			this.SlipperySaveTargetScale = 0.3;
			this.SlipperySaveVs = "Ink Slip Move";
			this.SlipperyMessage = "{{K|=subject.T= =verb:slip= on the ink!}}";
			this.SlipperyParticle = "&K\u001a";
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00121125 File Offset: 0x0011F325
		public override List<string> GetColors()
		{
			return LiquidInk.Colors;
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x0012112C File Offset: 0x0011F32C
		public override string GetColor()
		{
			return "K";
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x00121133 File Offset: 0x0011F333
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{K|ink}}";
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x0012113A File Offset: 0x0011F33A
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{K|inky}}";
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x00121141 File Offset: 0x0011F341
		public override string GetWaterRitualName()
		{
			return "ink";
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x00121148 File Offset: 0x0011F348
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{K|inky}}";
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x0012114F File Offset: 0x0011F34F
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{K|inky}}";
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x00121156 File Offset: 0x0011F356
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{K|ink}}";
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x0012115D File Offset: 0x0011F35D
		public override float GetValuePerDram()
		{
			return 1.5f;
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00121164 File Offset: 0x0011F364
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It's very inky.");
			return true;
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x00121174 File Offset: 0x0011F374
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&K";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x001211AF File Offset: 0x0011F3AF
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^k" + eRender.ColorString;
			}
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x001211D0 File Offset: 0x0011F3D0
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&y^k";
			Liquid.ParentObject.Render.TileColor = "&y";
			Liquid.ParentObject.Render.DetailColor = "k";
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x0012121C File Offset: 0x0011F41C
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&k";
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00121240 File Offset: 0x0011F440
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&y^k", "&y", "k");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&y^k", "&y", "k");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&y^k";
				render.TileColor = "&y";
				render.DetailColor = "k";
				if (num < 15)
				{
					render.RenderString = "÷";
					return;
				}
				if (num < 30)
				{
					render.RenderString = "~";
					return;
				}
				if (num < 45)
				{
					render.RenderString = " ";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x00121338 File Offset: 0x0011F538
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&k";
			}
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x00121358 File Offset: 0x0011F558
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("scholarship", 1);
		}

		// Token: 0x04001D66 RID: 7526
		public new const string ID = "ink";

		// Token: 0x04001D67 RID: 7527
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "K", "y" };
	}
}
