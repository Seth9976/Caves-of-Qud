using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000561 RID: 1377
	[IsLiquid]
	[Serializable]
	public class LiquidSludge : BaseLiquid
	{
		// Token: 0x06003BA4 RID: 15268 RVA: 0x001236CC File Offset: 0x001218CC
		public LiquidSludge()
			: base("sludge")
		{
			this.FlameTemperature = 575;
			this.VaporTemperature = 1575;
			this.Combustibility = 7;
			this.ThermalConductivity = 30;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 1;
			this.CirculatoryLossTerm = "oozing";
			this.CirculatoryLossNoun = "ooze";
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00123735 File Offset: 0x00121935
		public override List<string> GetColors()
		{
			return LiquidSludge.Colors;
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x0012373C File Offset: 0x0012193C
		public override string GetColor()
		{
			return "w";
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x00123743 File Offset: 0x00121943
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{w|brown sludge}}";
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0012374A File Offset: 0x0012194A
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{w|sludgy}}";
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x00123751 File Offset: 0x00121951
		public override string GetWaterRitualName()
		{
			return "sludge";
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x00123758 File Offset: 0x00121958
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{w|sludgy}}";
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x0012375F File Offset: 0x0012195F
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{w|sludgy}}";
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x00123766 File Offset: 0x00121966
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{w|sludge}}";
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x0012376D File Offset: 0x0012196D
		public override string GetPreparedCookingIngredient()
		{
			return "selfPoison";
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x00123774 File Offset: 0x00121974
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It's horrifying!");
			if (Target.ApplyEffect(new Poisoned(Stat.Roll("1d4+4", null), Stat.Roll("1d2+2", null).ToString() + "d2", 10, null, false, false), null))
			{
				Message.Compound("You feel sick!");
				ExitInterface = true;
			}
			return true;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x001237DC File Offset: 0x001219DC
		public override void ObjectGoingProne(LiquidVolume Liquid, GameObject GO, bool UsePopups)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (GO.IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(GO, "Brown sludge splashes into your mouth. You wince at the metallic taste.", ' ', false, UsePopups, false, null, GO);
			}
			GO.Splatter("&w.");
			for (int i = 0; i < 3; i++)
			{
				List<GameObject> list = Event.NewGameObjectList();
				Inventory inventory = GO.Inventory;
				if (inventory != null)
				{
					foreach (GameObject gameObject in inventory.GetObjectsDirect())
					{
						if (gameObject.HasPart<Metal>())
						{
							list.Add(gameObject);
						}
					}
				}
				Body body = GO.Body;
				if (body != null)
				{
					foreach (GameObject gameObject2 in body.GetEquippedObjects())
					{
						if (gameObject2.HasPart<Metal>())
						{
							list.Add(gameObject2);
						}
					}
				}
				if (list.Count > 0 && 75.in100())
				{
					list.GetRandomElement<GameObject>(null).ApplyEffect(new Rusted(UsePopups), null);
				}
			}
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x00123910 File Offset: 0x00121B10
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&w";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x0012394B File Offset: 0x00121B4B
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^w" + eRender.ColorString;
			}
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x0012396C File Offset: 0x00121B6C
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&Y^w";
			Liquid.ParentObject.Render.TileColor = "&Y";
			Liquid.ParentObject.Render.DetailColor = "w";
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x001239B8 File Offset: 0x00121BB8
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&w";
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x001239DC File Offset: 0x00121BDC
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&Y^w", "&Y", "w");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&Y^w", "&Y", "w");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&Y^w";
				render.TileColor = "&Y";
				render.DetailColor = "w";
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
					render.RenderString = "÷";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x00123AD4 File Offset: 0x00121CD4
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&w";
			}
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x00123AF4 File Offset: 0x00121CF4
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Splotchy/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x00123B0B File Offset: 0x00121D0B
		public override int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			if (FilthAffinity)
			{
				return 0;
			}
			if (!Smart)
			{
				return 2;
			}
			return 3;
		}

		// Token: 0x04001D7E RID: 7550
		public new const string ID = "sludge";

		// Token: 0x04001D7F RID: 7551
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "w", "Y" };
	}
}
