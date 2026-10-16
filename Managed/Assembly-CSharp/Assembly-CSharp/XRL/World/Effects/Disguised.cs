using System;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D28 RID: 3368
	[Serializable]
	public class Disguised : Effect, ITierInitialized
	{
		// Token: 0x060084C2 RID: 33986 RVA: 0x002FFA35 File Offset: 0x002FDC35
		public Disguised()
		{
			this.Duration = 9999;
			this.DisplayName = "{{K|disguised}}";
		}

		// Token: 0x060084C3 RID: 33987 RVA: 0x002FFA54 File Offset: 0x002FDC54
		public Disguised(string BlueprintName)
			: this()
		{
			this.BlueprintName = BlueprintName;
			GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(BlueprintName);
			if (blueprintIfExists == null)
			{
				throw new Exception("no such blueprint " + BlueprintName);
			}
			string partParameter = blueprintIfExists.GetPartParameter<string>("RandomTile", "Tiles", null);
			if (!partParameter.IsNullOrEmpty())
			{
				this.Tile = partParameter.CachedCommaExpansion().GetRandomElement<string>(null);
			}
			else
			{
				this.Tile = blueprintIfExists.GetPartParameter<string>("Render", "Tile", null);
			}
			this.RenderString = XRL.World.Parts.Render.ProcessRenderString(blueprintIfExists.GetPartParameter<string>("Render", "RenderString", null));
			this.ColorString = blueprintIfExists.GetPartParameter<string>("Render", "ColorString", null);
			this.TileColor = blueprintIfExists.GetPartParameter<string>("Render", "TileColor", this.ColorString);
			this.DetailColor = blueprintIfExists.GetPartParameter<string>("Render", "DetailColor", null);
		}

		// Token: 0x060084C4 RID: 33988 RVA: 0x002FFB39 File Offset: 0x002FDD39
		public Disguised(string BlueprintName, string Appearance)
			: this(BlueprintName)
		{
			this.Appearance = Appearance;
		}

		// Token: 0x060084C5 RID: 33989 RVA: 0x002FFB4C File Offset: 0x002FDD4C
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(2400, 7200);
			this.BlueprintName = EncountersAPI.GetACreatureBlueprintModel(new Predicate<GameObjectBlueprint>(ModDisguise.IsBlueprintUsableForDisguise)).Name;
			GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(this.BlueprintName);
			if (blueprintIfExists == null)
			{
				throw new Exception("no such blueprint " + this.BlueprintName);
			}
			string partParameter = blueprintIfExists.GetPartParameter<string>("RandomTile", "Tiles", null);
			if (!partParameter.IsNullOrEmpty())
			{
				this.Tile = partParameter.CachedCommaExpansion().GetRandomElement<string>(null);
			}
			else
			{
				this.Tile = blueprintIfExists.GetPartParameter<string>("Render", "Tile", null);
			}
			this.RenderString = XRL.World.Parts.Render.ProcessRenderString(blueprintIfExists.GetPartParameter<string>("Render", "RenderString", null));
			this.ColorString = blueprintIfExists.GetPartParameter<string>("Render", "ColorString", null);
			this.TileColor = blueprintIfExists.GetPartParameter<string>("Render", "TileColor", this.ColorString);
			this.DetailColor = blueprintIfExists.GetPartParameter<string>("Render", "DetailColor", null);
			string partParameter2 = blueprintIfExists.GetPartParameter<string>("Render", "DisplayName", "creature of some kind");
			string text = blueprintIfExists.GetxTag("Grammar", "iArticle", null);
			text = base.GetPropertyOrTag("OverrideIArticle", text);
			if (text == null)
			{
				this.Appearance = Grammar.A(partParameter2, false);
				return;
			}
			this.Appearance = text + " " + partParameter2;
		}

		// Token: 0x060084C6 RID: 33990 RVA: 0x002FFCB7 File Offset: 0x002FDEB7
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060084C7 RID: 33991 RVA: 0x002FFCBA File Offset: 0x002FDEBA
		public override int GetEffectType()
		{
			return 131072;
		}

		// Token: 0x060084C8 RID: 33992 RVA: 0x002FFCC1 File Offset: 0x002FDEC1
		public override string GetDetails()
		{
			return "Has the appearance of " + (this.Appearance ?? "another creature") + ".";
		}

		// Token: 0x060084C9 RID: 33993 RVA: 0x002FFCE1 File Offset: 0x002FDEE1
		public override bool Apply(GameObject Object)
		{
			return true;
		}

		// Token: 0x060084CA RID: 33994 RVA: 0x002FFCE4 File Offset: 0x002FDEE4
		public override bool Render(RenderEvent E)
		{
			bool useTiles = Options.UseTiles;
			if (useTiles && !this.Tile.IsNullOrEmpty())
			{
				E.Tile = this.Tile;
			}
			if (!this.RenderString.IsNullOrEmpty())
			{
				E.RenderString = this.RenderString;
			}
			string text = ((!this.TileColor.IsNullOrEmpty() && !this.Tile.IsNullOrEmpty() && useTiles) ? this.TileColor : this.ColorString);
			if (!text.IsNullOrEmpty())
			{
				E.ColorString = text;
			}
			if (!this.DetailColor.IsNullOrEmpty())
			{
				E.DetailColor = this.DetailColor;
			}
			return base.Render(E);
		}

		// Token: 0x060084CB RID: 33995 RVA: 0x002FFD8C File Offset: 0x002FDF8C
		public override bool OverlayRender(RenderEvent E)
		{
			if (Options.UseTiles && !this.Tile.IsNullOrEmpty())
			{
				E.Tile = this.Tile;
			}
			if (!this.RenderString.IsNullOrEmpty())
			{
				E.RenderString = this.RenderString;
			}
			return base.OverlayRender(E);
		}

		// Token: 0x0400399A RID: 14746
		public string BlueprintName;

		// Token: 0x0400399B RID: 14747
		public string Tile;

		// Token: 0x0400399C RID: 14748
		public string RenderString;

		// Token: 0x0400399D RID: 14749
		public string ColorString;

		// Token: 0x0400399E RID: 14750
		public string TileColor;

		// Token: 0x0400399F RID: 14751
		public string DetailColor;

		// Token: 0x040039A0 RID: 14752
		public string Appearance;
	}
}
