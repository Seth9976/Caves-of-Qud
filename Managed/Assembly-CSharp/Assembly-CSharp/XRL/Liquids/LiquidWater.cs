using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.Liquids
{
	// Token: 0x02000564 RID: 1380
	[IsLiquid]
	[Serializable]
	public class LiquidWater : BaseLiquid
	{
		// Token: 0x06003BFC RID: 15356 RVA: 0x00125728 File Offset: 0x00123928
		public LiquidWater()
			: base("water")
		{
			this.Combustibility = -50;
			this.VaporObject = "SteamGas";
			this.Fluidity = 30;
			this.Evaporativity = 2;
			this.Cleansing = 5;
			this.PureElectricalConductivity = 0;
			this.MixedElectricalConductivity = 100;
			this.EnableCleaning = true;
			this.SlipperyWhenFrozen = true;
			this.SlipperySaveTargetBase = 5;
			this.SlipperySaveTargetScale = 0.3;
			this.SlipperySaveVs = "Ice Slip Move";
			this.SlipperyMessage = "{{C|=subject.T= =verb:slip= on the ice!}}";
			this.SlipperyParticle = "&C\u001a";
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x001257BD File Offset: 0x001239BD
		public override List<string> GetColors()
		{
			return LiquidWater.Colors;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x001257C4 File Offset: 0x001239C4
		public override string GetColor()
		{
			return "B";
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x001257CB File Offset: 0x001239CB
		public override string GetName(LiquidVolume Liquid)
		{
			if (Liquid != null && Liquid.IsPureLiquid("water", false))
			{
				return "{{B|fresh water}}";
			}
			return "{{B|water}}";
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x001257E9 File Offset: 0x001239E9
		public override string GetAdjective(LiquidVolume Liquid)
		{
			if (Liquid == null)
			{
				return "{{Y|dilute}}";
			}
			if (Liquid.ComponentLiquids["water"] > 0)
			{
				return "{{Y|dilute}}";
			}
			return null;
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x0012580E File Offset: 0x00123A0E
		public override string GetWaterRitualName()
		{
			return "water";
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x00125818 File Offset: 0x00123A18
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			if (Liquid.IsMixed())
			{
				if (Liquid.Proportion("salt", "blood") > Liquid.Proportion("water"))
				{
					return null;
				}
				if (Liquid.Primary != "oil" && Liquid.Primary != "lava" && Liquid.Primary != "wax")
				{
					return "{{Y|dilute}}";
				}
			}
			return "{{B|wet}}";
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x0012588D File Offset: 0x00123A8D
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{B|wet}}";
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x00125894 File Offset: 0x00123A94
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{B|water}}";
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x0012589C File Offset: 0x00123A9C
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.HasPart<Amphibious>() && Message != null)
			{
				Message.Compound("You pour the water on " + Target.itself + "!");
			}
			Event @event = new Event("AddWater");
			@event.SetParameter("Amount", Volume * 10 * ((Liquid != null) ? Liquid.ComponentLiquids["water"] : 1000));
			@event.SetParameter("MessageHolder", Message);
			try
			{
				if (!Target.FireEvent(@event))
				{
					return false;
				}
				if (Liquid.IsFreshWater(false))
				{
					if (Message != null)
					{
						Message.Compound("Ahh, refreshing!");
					}
				}
			}
			finally
			{
				if (@event.InterfaceExitRequested())
				{
					ExitInterface = true;
				}
			}
			return true;
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x00125964 File Offset: 0x00123B64
		public override void SmearOn(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
			if (Target.HasPart<Amphibious>() && Liquid.IsFreshWater(false))
			{
				Target.FireEvent(Event.New("AddWater", "Amount", 100000 * Liquid.Volume, "Forced", 1));
				Liquid.Empty(false, false);
			}
			base.SmearOn(Liquid, Target, By, FromCell);
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x001259C0 File Offset: 0x00123BC0
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					utf16ValueStringBuilder.Append("^b");
					utf16ValueStringBuilder.Append(eRender.ColorString);
					eRender.ColorString = utf16ValueStringBuilder.ToString();
				}
			}
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x00125A28 File Offset: 0x00123C28
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&b^B";
			Liquid.ParentObject.Render.TileColor = "&b";
			Liquid.ParentObject.Render.DetailColor = "B";
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x00125A74 File Offset: 0x00123C74
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&b";
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x00125A98 File Offset: 0x00123C98
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.Secondary == "algae")
			{
				return;
			}
			if (Liquid.ParentObject == null)
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&c^C", "&c", "C");
				return;
			}
			if (Liquid.IsFreshWaterFast())
			{
				Render render = Liquid.ParentObject.Render;
				int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
				if (Stat.RandomCosmetic(1, 600) == 1)
				{
					eRender.RenderString = "÷";
					eRender.TileVariantColors("&Y^B", "&Y", "B");
				}
				if (render.ColorString == "&b" || Stat.RandomCosmetic(1, 60) == 1)
				{
					if (num < 15)
					{
						render.RenderString = "÷";
						render.ColorString = "&b^B";
						render.TileColor = "&b";
						render.DetailColor = "B";
						return;
					}
					if (num < 30)
					{
						render.RenderString = " ";
						render.ColorString = "&Y^B";
						render.TileColor = "&Y";
						render.DetailColor = "B";
						return;
					}
					if (num < 45)
					{
						render.RenderString = "÷";
						render.ColorString = "&b^B";
						render.TileColor = "&b";
						render.DetailColor = "B";
						return;
					}
					render.RenderString = "~";
					render.ColorString = "&y^B";
					render.TileColor = "&y";
					render.DetailColor = "B";
					return;
				}
			}
			else if (Liquid.Flowing)
			{
				int num2 = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
				if (num2 < 15)
				{
					eRender.RenderString = "~";
					eRender.TileVariantColors("&B^b", "&B", "b");
					return;
				}
				if (num2 < 30)
				{
					eRender.RenderString = Liquid.ParentObject.Render.RenderString;
					eRender.TileVariantColors("&Y^b", "&Y", "b");
					return;
				}
				if (num2 < 45)
				{
					eRender.RenderString = "~";
					eRender.TileVariantColors("&B^b", "&B", "b");
					return;
				}
				eRender.RenderString = Liquid.ParentObject.Render.RenderString;
				eRender.TileVariantColors("&B^b", "&B", "b");
				return;
			}
			else
			{
				Render render2 = Liquid.ParentObject.Render;
				int num3 = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
				if (Stat.RandomCosmetic(1, 600) == 1)
				{
					eRender.RenderString = "~";
					eRender.TileVariantColors("&Y^b", "&Y", "b");
				}
				if (Stat.RandomCosmetic(1, 60) == 1)
				{
					if (num3 < 15)
					{
						render2.RenderString = "÷";
						render2.ColorString = "&B^b";
						render2.TileColor = "&B";
						render2.DetailColor = "b";
						return;
					}
					if (num3 < 30)
					{
						render2.RenderString = "~";
						render2.ColorString = "&B^b";
						render2.TileColor = "&B";
						render2.DetailColor = "b";
						return;
					}
					if (num3 < 45)
					{
						render2.RenderString = " ";
						render2.ColorString = "&B^b";
						render2.TileColor = "&B";
						render2.DetailColor = "b";
						return;
					}
					render2.RenderString = "~";
					render2.ColorString = "&B^b";
					render2.TileColor = "&B";
					render2.DetailColor = "b";
				}
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x00125E12 File Offset: 0x00124012
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&b";
			}
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x00125E32 File Offset: 0x00124032
		public override float GetValuePerDram()
		{
			return 0.01f;
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x00125E39 File Offset: 0x00124039
		public override float GetPureLiquidValueMultipler()
		{
			return 100f;
		}

		// Token: 0x04001D8A RID: 7562
		public new const string ID = "water";

		// Token: 0x04001D8B RID: 7563
		[NonSerialized]
		public static List<string> Colors = new List<string>(4) { "B", "y", "Y", "b" };
	}
}
