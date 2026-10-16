using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200055C RID: 1372
	[IsLiquid]
	[Serializable]
	public class LiquidProteanGunk : BaseLiquid
	{
		// Token: 0x06003B45 RID: 15173 RVA: 0x001224B4 File Offset: 0x001206B4
		public LiquidProteanGunk()
			: base("proteangunk")
		{
			this.VaporTemperature = 2000;
			this.Combustibility = 40;
			this.ThermalConductivity = 40;
			this.Fluidity = 25;
			this.Evaporativity = 1;
		}

		// Token: 0x06003B46 RID: 15174 RVA: 0x001224EB File Offset: 0x001206EB
		public override List<string> GetColors()
		{
			return LiquidProteanGunk.Colors;
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x001224F2 File Offset: 0x001206F2
		public override string GetColor()
		{
			return "c";
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x001224F9 File Offset: 0x001206F9
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{c|primordial soup}}";
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x00122500 File Offset: 0x00120700
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{c|soupy}}";
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x00122507 File Offset: 0x00120707
		public override string GetWaterRitualName()
		{
			return "soup";
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x0012250E File Offset: 0x0012070E
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{c|soupy}}";
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x00122515 File Offset: 0x00120715
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{c|soupy}}";
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x0012251C File Offset: 0x0012071C
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{c|soup}}";
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x00122523 File Offset: 0x00120723
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.HasPart<Stomach>())
			{
				Target.FireEvent(Event.New("AddWater", "Amount", 2 * Volume, "Forced", 1));
				Message.Compound("You feel the soup slosh around your stomach.");
			}
			return true;
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x0012255A File Offset: 0x0012075A
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^c" + eRender.ColorString;
			}
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x0012257C File Offset: 0x0012077C
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&c^C";
			Liquid.ParentObject.Render.TileColor = "&c";
			Liquid.ParentObject.Render.DetailColor = "C";
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x001225C8 File Offset: 0x001207C8
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&c";
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x001225EC File Offset: 0x001207EC
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&c^C", "&c", "C");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&c^C", "&c", "C");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&c^C";
				render.TileColor = "&c";
				render.DetailColor = "C";
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

		// Token: 0x06003B53 RID: 15187 RVA: 0x001226E4 File Offset: 0x001208E4
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&c";
			}
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x00122704 File Offset: 0x00120904
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&c";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x0012273F File Offset: 0x0012093F
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Gunk/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003B56 RID: 15190 RVA: 0x00122756 File Offset: 0x00120956
		public override float GetValuePerDram()
		{
			return 4f;
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x00122760 File Offset: 0x00120960
		public override void ProcessTurns(LiquidVolume Liquid, GameObject GO, int Turns)
		{
			bool flag;
			if (GO == null)
			{
				flag = true;
			}
			else
			{
				Zone currentZone = GO.CurrentZone;
				bool? flag2 = ((currentZone != null) ? new bool?(currentZone.IsActive()) : null);
				bool flag3 = true;
				flag = !((flag2.GetValueOrDefault() == flag3) & (flag2 != null));
			}
			if (flag)
			{
				return;
			}
			int intProperty = GO.GetIntProperty("SoupCounter", 0);
			string primaryLiquidID = Liquid.GetPrimaryLiquidID();
			string secondaryLiquidID = Liquid.GetSecondaryLiquidID();
			string stringProperty = GO.GetStringProperty("ReactingSecondary", null);
			if (secondaryLiquidID != null && Liquid.IsMixed() && !Liquid.EffectivelySealed() && primaryLiquidID == "proteangunk" && secondaryLiquidID != primaryLiquidID && Liquid.Volume >= 500 && (stringProperty == secondaryLiquidID || stringProperty == null))
			{
				if (intProperty == 1)
				{
					GameObject gameObject = GameObject.Create("SoupSludge");
					gameObject.AddPart<SoupSludge>(new SoupSludge(secondaryLiquidID), true, false);
					GO.CurrentCell.getClosestPassableCellFor(gameObject).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.MakeActive(false);
					Messaging.EmitMessage(GO, "The reacting liquids congeal into " + gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", ' ', false, false, false, null, null);
					Liquid.UseDrams(500);
					GO.SetStringProperty("ReactingSecondary", null, false);
					GO.SetIntProperty("SoupCounter", -5, false);
				}
				else if (intProperty < 0 && LiquidProteanGunk.CanSpawnSludgeIn(GO.CurrentZone))
				{
					GO.SetIntProperty("SoupCounter", Stat.Random(10, 150), false);
					GO.SetStringProperty("ReactingSecondary", secondaryLiquidID, false);
					if (secondaryLiquidID != null)
					{
						Messaging.EmitMessage(GO, string.Concat(new string[]
						{
							"The primordial soup ",
							The.Player.DescribeDirectionToward(GO, false, false),
							" starts reacting with the ",
							LiquidVolume.GetLiquid(secondaryLiquidID).GetName(Liquid),
							"."
						}), ' ', false, false, false, null, null);
					}
				}
				GO.SetIntProperty("SoupCounter", GO.GetIntProperty("SoupCounter", 0) - 1, false);
			}
			else if (intProperty > 0)
			{
				Messaging.EmitMessage(GO, "The liquids stop reacting.", ' ', false, false, false, null, null);
				GO.SetIntProperty("SoupCounter", -5, false);
			}
			base.ProcessTurns(Liquid, GO, Turns);
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x001229CA File Offset: 0x00120BCA
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("chance", 1);
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x001229D8 File Offset: 0x00120BD8
		public static bool CanSpawnSludgeIn(Zone Z)
		{
			if (!Z.IsActive())
			{
				return false;
			}
			if (Z != LiquidProteanGunk.LastZone)
			{
				LiquidProteanGunk.LastZone = Z;
				LiquidProteanGunk.LastZoneCheck = -2147483648L;
				LiquidProteanGunk.LastZoneResult = 0L;
			}
			if (The.Game.Turns - 20L >= LiquidProteanGunk.LastZoneCheck)
			{
				LiquidProteanGunk.LastZoneCheck = The.Game.Turns;
				LiquidProteanGunk.LastZoneResult = 0L;
				foreach (GameObject gameObject in Z.YieldObjects())
				{
					string text;
					if (gameObject.Blueprint == "SoupSludge" || (gameObject.Property.TryGetValue("ReactingSecondary", out text) && text != null))
					{
						LiquidProteanGunk.LastZoneResult += 1L;
					}
				}
			}
			long lastZoneResult = LiquidProteanGunk.LastZoneResult;
			LiquidProteanGunk.LastZoneResult = lastZoneResult + 1L;
			return lastZoneResult < 30L;
		}

		// Token: 0x04001D70 RID: 7536
		public new const string ID = "proteangunk";

		// Token: 0x04001D71 RID: 7537
		public const int MAXIMUM_SLUDGE = 30;

		// Token: 0x04001D72 RID: 7538
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "c", "C" };

		// Token: 0x04001D73 RID: 7539
		[NonSerialized]
		public static Zone LastZone;

		// Token: 0x04001D74 RID: 7540
		[NonSerialized]
		public static long LastZoneCheck = -2147483648L;

		// Token: 0x04001D75 RID: 7541
		[NonSerialized]
		public static long LastZoneResult = 0L;
	}
}
