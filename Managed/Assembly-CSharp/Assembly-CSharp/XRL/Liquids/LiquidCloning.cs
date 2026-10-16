using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000552 RID: 1362
	[IsLiquid]
	[Serializable]
	public class LiquidCloning : BaseLiquid
	{
		// Token: 0x06003A6F RID: 14959 RVA: 0x0011FFD5 File Offset: 0x0011E1D5
		public LiquidCloning()
			: base("cloning")
		{
			this.Fluidity = 10;
			this.Evaporativity = 100;
			this.Glows = true;
		}

		// Token: 0x06003A70 RID: 14960 RVA: 0x0011FFF9 File Offset: 0x0011E1F9
		public override List<string> GetColors()
		{
			return LiquidCloning.Colors;
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x00120000 File Offset: 0x0011E200
		public override string GetColor()
		{
			return "Y";
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x00120007 File Offset: 0x0011E207
		public override float GetValuePerDram()
		{
			return 1250f;
		}

		// Token: 0x06003A73 RID: 14963 RVA: 0x0012000E File Offset: 0x0011E20E
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{cloning|cloning draught}}";
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x00120015 File Offset: 0x0011E215
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{cloning|homogenized}}";
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x0012001C File Offset: 0x0011E21C
		public override string GetWaterRitualName()
		{
			return "draught";
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x00120023 File Offset: 0x0011E223
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{cloning|homogenized}}";
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x0012002A File Offset: 0x0011E22A
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{cloning|homogenized}}";
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x00120031 File Offset: 0x0011E231
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{cloning|cloning-draught}}";
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x00120038 File Offset: 0x0011E238
		public override string GetPreparedCookingIngredient()
		{
			return "cloningMinor";
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x0012003F File Offset: 0x0011E23F
		public void Clone(GameObject Target, GameObject Actor = null, int Amount = 1)
		{
			if (Cloning.CanBeCloned(Target, null, "CloningDraught", false))
			{
				Target.ApplyEffect(new Budding(Actor, Amount, "CloningDraught"), null);
			}
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x00120064 File Offset: 0x0011E264
		public override void SmearOn(LiquidVolume Liquid, GameObject GO, GameObject By, bool FromCell)
		{
			if (Liquid.IsPureLiquid("cloning", false) && Liquid.Volume > 0 && (Liquid.ParentObject == null || !Liquid.ParentObject.IsTemporary))
			{
				this.Clone(GO, By, 1);
			}
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x0012009C File Offset: 0x0011E29C
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.IsPlayer() && Cloning.CanBeCloned(Target, Target, "CloningDraught", false))
			{
				Message.Compound("You feel unsettlingly ambivalent.");
			}
			if (Liquid.IsPureLiquid("cloning", false) && Volume > 0)
			{
				this.Clone(Target, Target, DrinkMagnifier.Magnify(Target, 1));
			}
			return true;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x001200EF File Offset: 0x0011E2EF
		public override void BeforeRender(LiquidVolume Liquid)
		{
			if (!Liquid.Sealed || Liquid.LiquidVisibleWhenSealed)
			{
				Liquid.AddLight(0, LightLevel.Light, false);
			}
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x0012010E File Offset: 0x0011E30E
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^M" + eRender.ColorString;
			}
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x00120130 File Offset: 0x0011E330
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&M^Y";
			Liquid.ParentObject.Render.TileColor = "&M";
			Liquid.ParentObject.Render.DetailColor = "Y";
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x0012017C File Offset: 0x0011E37C
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&Y";
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x001201A0 File Offset: 0x0011E3A0
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&M^Y", "&M", "Y");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&M^Y", "&M", "Y");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&M^Y";
				render.TileColor = "&M";
				render.DetailColor = "Y";
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
					render.RenderString = "\t";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x00120298 File Offset: 0x0011E498
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Paisley/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x001202AF File Offset: 0x0011E4AF
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("might", 1);
			E.Add("time", 1);
		}

		// Token: 0x04001D5B RID: 7515
		public const string REPLICATION_CONTEXT = "CloningDraught";

		// Token: 0x04001D5C RID: 7516
		public new const string ID = "cloning";

		// Token: 0x04001D5D RID: 7517
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "Y", "M" };
	}
}
