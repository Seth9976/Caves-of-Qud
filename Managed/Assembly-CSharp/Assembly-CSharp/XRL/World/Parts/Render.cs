using System;
using System.CodeDom.Compiler;
using System.Text;
using ConsoleLib.Console;
using Kobold;
using Newtonsoft.Json;
using Occult.Engine.CodeGeneration;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02000DC8 RID: 3528
	[GeneratePoolingPartial(Capacity = 128)]
	[GenerateSerializationPartial]
	[Serializable]
	public class Render : IPart, IRenderable
	{
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06008F09 RID: 36617 RVA: 0x0033965F File Offset: 0x0033785F
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return XRL.World.Parts.Render.RenderPool;
			}
		}

		// Token: 0x06008F0A RID: 36618 RVA: 0x00339668 File Offset: 0x00337868
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override void Reset()
		{
			base.Reset();
			this.DisplayName = null;
			this.RenderString = "?";
			this.ColorString = "&y";
			this.DetailColor = "";
			this.TileColor = "";
			this.RenderLayer = 0;
			this.Flags = 4;
			this.Tile = null;
		}

		// Token: 0x06008F0B RID: 36619 RVA: 0x003396C4 File Offset: 0x003378C4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.DisplayName);
			Writer.WriteOptimized(this.RenderString);
			Writer.WriteOptimized(this.ColorString);
			Writer.WriteOptimized(this.DetailColor);
			Writer.WriteOptimized(this.TileColor);
			Writer.WriteOptimized(this.RenderLayer);
			Writer.WriteOptimized(this.Flags);
			Writer.WriteOptimized(this.Tile);
		}

		// Token: 0x06008F0C RID: 36620 RVA: 0x00339734 File Offset: 0x00337934
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.DisplayName = Reader.ReadOptimizedString();
			this.RenderString = Reader.ReadOptimizedString();
			this.ColorString = Reader.ReadOptimizedString();
			this.DetailColor = Reader.ReadOptimizedString();
			this.TileColor = Reader.ReadOptimizedString();
			this.RenderLayer = Reader.ReadOptimizedInt32();
			this.Flags = Reader.ReadOptimizedInt32();
			this.Tile = Reader.ReadOptimizedString();
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06008F0D RID: 36621 RVA: 0x003397A1 File Offset: 0x003379A1
		// (set) Token: 0x06008F0E RID: 36622 RVA: 0x003397AE File Offset: 0x003379AE
		[JsonIgnore]
		public bool Occluding
		{
			get
			{
				return (this.Flags & 1) == 1;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1) : (this.Flags & -2));
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06008F0F RID: 36623 RVA: 0x003397CC File Offset: 0x003379CC
		// (set) Token: 0x06008F10 RID: 36624 RVA: 0x003397D9 File Offset: 0x003379D9
		[JsonIgnore]
		public bool RenderIfDark
		{
			get
			{
				return (this.Flags & 2) == 2;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2) : (this.Flags & -3));
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06008F11 RID: 36625 RVA: 0x003397F7 File Offset: 0x003379F7
		// (set) Token: 0x06008F12 RID: 36626 RVA: 0x00339804 File Offset: 0x00337A04
		[JsonIgnore]
		public new bool Visible
		{
			get
			{
				return (this.Flags & 4) == 4;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4) : (this.Flags & -5));
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06008F13 RID: 36627 RVA: 0x00339822 File Offset: 0x00337A22
		// (set) Token: 0x06008F14 RID: 36628 RVA: 0x0033982F File Offset: 0x00337A2F
		[JsonIgnore]
		public bool CustomRender
		{
			get
			{
				return (this.Flags & 8) == 8;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8) : (this.Flags & -9));
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06008F15 RID: 36629 RVA: 0x0033984D File Offset: 0x00337A4D
		// (set) Token: 0x06008F16 RID: 36630 RVA: 0x0033985C File Offset: 0x00337A5C
		[JsonIgnore]
		public bool HFlip
		{
			get
			{
				return (this.Flags & 16) == 16;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16) : (this.Flags & -17));
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06008F17 RID: 36631 RVA: 0x0033987B File Offset: 0x00337A7B
		// (set) Token: 0x06008F18 RID: 36632 RVA: 0x0033988A File Offset: 0x00337A8A
		[JsonIgnore]
		public bool VFlip
		{
			get
			{
				return (this.Flags & 32) == 32;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 32) : (this.Flags & -33));
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06008F19 RID: 36633 RVA: 0x003398A9 File Offset: 0x00337AA9
		// (set) Token: 0x06008F1A RID: 36634 RVA: 0x003398B8 File Offset: 0x00337AB8
		[JsonIgnore]
		public bool IgnoreColorForStack
		{
			get
			{
				return (this.Flags & 64) == 64;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 64) : (this.Flags & -65));
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06008F1B RID: 36635 RVA: 0x003398D7 File Offset: 0x00337AD7
		// (set) Token: 0x06008F1C RID: 36636 RVA: 0x003398EC File Offset: 0x00337AEC
		[JsonIgnore]
		public bool IgnoreTileForStack
		{
			get
			{
				return (this.Flags & 128) == 128;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 128) : (this.Flags & -129));
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06008F1D RID: 36637 RVA: 0x00339911 File Offset: 0x00337B11
		// (set) Token: 0x06008F1E RID: 36638 RVA: 0x00339926 File Offset: 0x00337B26
		[JsonIgnore]
		public bool Never
		{
			get
			{
				return (this.Flags & 256) == 256;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 256) : (this.Flags & -257));
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06008F1F RID: 36639 RVA: 0x0033994B File Offset: 0x00337B4B
		// (set) Token: 0x06008F20 RID: 36640 RVA: 0x00339960 File Offset: 0x00337B60
		[JsonIgnore]
		public bool PartyFlip
		{
			get
			{
				return (this.Flags & 512) == 512;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 512) : (this.Flags & -513));
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06008F21 RID: 36641 RVA: 0x00339985 File Offset: 0x00337B85
		[JsonIgnore]
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x06008F22 RID: 36642 RVA: 0x0033998C File Offset: 0x00337B8C
		public override void Initialize()
		{
			this.RenderString = XRL.World.Parts.Render.ProcessRenderString(this.RenderString);
		}

		// Token: 0x06008F23 RID: 36643 RVA: 0x0033999F File Offset: 0x00337B9F
		public override void Attach()
		{
			this.ParentObject.Render = this;
		}

		// Token: 0x06008F24 RID: 36644 RVA: 0x003399AD File Offset: 0x00337BAD
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Render : null) == this)
			{
				this.ParentObject.Render = null;
			}
		}

		// Token: 0x06008F25 RID: 36645 RVA: 0x003399D0 File Offset: 0x00337BD0
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == SingletonEvent<GetDebugInternalsEvent>.ID;
		}

		// Token: 0x06008F26 RID: 36646 RVA: 0x003399DC File Offset: 0x00337BDC
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "DisplayName", this.DisplayName);
			E.AddEntry(this, "RenderString", this.RenderString);
			E.AddEntry(this, "ColorString", this.ColorString);
			E.AddEntry(this, "TileColor", this.TileColor);
			E.AddEntry(this, "DetailColor", this.DetailColor);
			E.AddEntry(this, "RenderLayer", this.RenderLayer);
			E.AddEntry(this, "Visible", this.Visible);
			E.AddEntry(this, "Occluding", this.Occluding);
			E.AddEntry(this, "RenderIfDark", this.RenderIfDark);
			E.AddEntry(this, "CustomRender", this.CustomRender);
			E.AddEntry(this, "Tile", this.Tile);
			return base.HandleEvent(E);
		}

		// Token: 0x06008F27 RID: 36647 RVA: 0x00339AB8 File Offset: 0x00337CB8
		public string GetTileForegroundColor()
		{
			if (this.TileColor != null)
			{
				int num = this.TileColor.LastIndexOf('&');
				if (num >= 0)
				{
					return this.TileColor[num + 1].ToString();
				}
			}
			return this.GetForegroundColor();
		}

		// Token: 0x06008F28 RID: 36648 RVA: 0x00339AFC File Offset: 0x00337CFC
		public string GetForegroundColor()
		{
			if (this.ColorString != null)
			{
				int num = this.ColorString.LastIndexOf('&');
				if (num >= 0)
				{
					return this.ColorString[num + 1].ToString();
				}
			}
			return "y";
		}

		// Token: 0x06008F29 RID: 36649 RVA: 0x00339B40 File Offset: 0x00337D40
		public char GetTileForegroundColorChar()
		{
			if (this.TileColor != null)
			{
				int num = this.TileColor.LastIndexOf('&');
				if (num >= 0)
				{
					return this.TileColor[num + 1];
				}
			}
			return this.GetForegroundColorChar();
		}

		// Token: 0x06008F2A RID: 36650 RVA: 0x00339B7C File Offset: 0x00337D7C
		public char GetForegroundColorChar()
		{
			if (this.ColorString != null)
			{
				int num = this.ColorString.LastIndexOf('&');
				if (num >= 0)
				{
					return this.ColorString[num + 1];
				}
			}
			return 'y';
		}

		// Token: 0x06008F2B RID: 36651 RVA: 0x00339BB4 File Offset: 0x00337DB4
		public string GetBackgroundColor()
		{
			if (this.ColorString != null)
			{
				int num = this.ColorString.LastIndexOf('^');
				if (num >= 0)
				{
					return this.ColorString[num + 1].ToString();
				}
			}
			return "k";
		}

		// Token: 0x06008F2C RID: 36652 RVA: 0x00339BF8 File Offset: 0x00337DF8
		public void SetForegroundColor(char color)
		{
			if (string.IsNullOrEmpty(this.ColorString))
			{
				this.ColorString = "&" + color.ToString();
			}
			else if (this.ColorString.Contains("&"))
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append(this.ColorString);
				stringBuilder[this.ColorString.LastIndexOf('&') + 1] = color;
				this.ColorString = stringBuilder.ToString();
			}
			else
			{
				this.ColorString = "&" + color.ToString() + this.ColorString;
			}
			if (!string.IsNullOrEmpty(this.TileColor))
			{
				if (this.TileColor.Contains("&"))
				{
					StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
					stringBuilder2.Append(this.TileColor);
					stringBuilder2[this.TileColor.LastIndexOf('&') + 1] = color;
					this.TileColor = stringBuilder2.ToString();
					return;
				}
				this.TileColor = "&" + color.ToString() + this.TileColor;
			}
		}

		// Token: 0x06008F2D RID: 36653 RVA: 0x00339D05 File Offset: 0x00337F05
		public void SetForegroundColor(string color)
		{
			if (!string.IsNullOrEmpty(color))
			{
				if (color[0] == '&' && color.Length > 1)
				{
					this.SetForegroundColor(color[1]);
					return;
				}
				this.SetForegroundColor(color[0]);
			}
		}

		// Token: 0x06008F2E RID: 36654 RVA: 0x00339D40 File Offset: 0x00337F40
		public void SetBackgroundColor(char color)
		{
			if (string.IsNullOrEmpty(this.ColorString))
			{
				this.ColorString = "^" + color.ToString();
			}
			else if (this.ColorString.Contains("^"))
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append(this.ColorString);
				stringBuilder[this.ColorString.LastIndexOf('^') + 1] = color;
				this.ColorString = stringBuilder.ToString();
			}
			else
			{
				this.ColorString = this.ColorString + "^" + color.ToString();
			}
			if (!string.IsNullOrEmpty(this.TileColor))
			{
				if (this.TileColor.Contains("^"))
				{
					StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
					stringBuilder2.Append(this.TileColor);
					stringBuilder2[this.TileColor.LastIndexOf('^') + 1] = color;
					this.TileColor = stringBuilder2.ToString();
					return;
				}
				this.TileColor = this.TileColor + "^" + color.ToString();
			}
		}

		// Token: 0x06008F2F RID: 36655 RVA: 0x00339E4D File Offset: 0x0033804D
		public void SetBackgroundColor(string color)
		{
			if (!string.IsNullOrEmpty(color))
			{
				if (color[0] == '^' && color.Length > 1)
				{
					this.SetBackgroundColor(color[1]);
					return;
				}
				this.SetBackgroundColor(color[0]);
			}
		}

		// Token: 0x06008F30 RID: 36656 RVA: 0x00339E88 File Offset: 0x00338088
		public bool SetRenderString(string s)
		{
			string text = XRL.World.Parts.Render.ProcessRenderString(s);
			if (text != this.RenderString)
			{
				this.RenderString = text;
				return true;
			}
			return false;
		}

		// Token: 0x06008F31 RID: 36657 RVA: 0x00339EB4 File Offset: 0x003380B4
		public override bool SameAs(IPart p)
		{
			Render render = p as Render;
			return !(render.DisplayName != this.DisplayName) && !(render.RenderString != this.RenderString) && (!(render.ColorString != this.ColorString) || this.IgnoreColorForStack) && (!(render.DetailColor != this.DetailColor) || this.IgnoreColorForStack) && (!(render.TileColor != this.TileColor) || this.IgnoreColorForStack) && render.RenderLayer == this.RenderLayer && render.Visible == this.Visible && render.Occluding == this.Occluding && render.RenderIfDark == this.RenderIfDark && render.CustomRender == this.CustomRender && (!(render.Tile != this.Tile) || this.IgnoreTileForStack) && render.Flags == this.Flags && base.SameAs(p);
		}

		// Token: 0x06008F32 RID: 36658 RVA: 0x00339FD0 File Offset: 0x003381D0
		public static string ProcessRenderString(string what)
		{
			if (what.Length > 1)
			{
				what = ((char)Convert.ToInt32(what)).ToString();
			}
			else if (what == "&")
			{
				what = "&&";
			}
			else if (what == "^")
			{
				what = "^^";
			}
			return what;
		}

		// Token: 0x06008F33 RID: 36659 RVA: 0x0033A024 File Offset: 0x00338224
		public bool getHFlip()
		{
			if (!this.PartyFlip)
			{
				return this.HFlip;
			}
			return !this.HFlip;
		}

		// Token: 0x06008F34 RID: 36660 RVA: 0x0033A03E File Offset: 0x0033823E
		public bool getVFlip()
		{
			return this.VFlip;
		}

		// Token: 0x06008F35 RID: 36661 RVA: 0x0033A046 File Offset: 0x00338246
		public string getTile()
		{
			return this.Tile;
		}

		// Token: 0x06008F36 RID: 36662 RVA: 0x0033A04E File Offset: 0x0033824E
		public string getRenderString()
		{
			return this.RenderString;
		}

		// Token: 0x06008F37 RID: 36663 RVA: 0x0033A056 File Offset: 0x00338256
		public string getColorString()
		{
			return this.ColorString;
		}

		// Token: 0x06008F38 RID: 36664 RVA: 0x0033A05E File Offset: 0x0033825E
		public string getTileOrRenderColor()
		{
			if (!string.IsNullOrEmpty(this.TileColor))
			{
				return this.TileColor;
			}
			return this.GetForegroundColor();
		}

		// Token: 0x06008F39 RID: 36665 RVA: 0x0033A07A File Offset: 0x0033827A
		public string getTileColor()
		{
			if (!string.IsNullOrEmpty(this.TileColor))
			{
				return this.TileColor;
			}
			return null;
		}

		// Token: 0x06008F3A RID: 36666 RVA: 0x0033A091 File Offset: 0x00338291
		public char getDetailColor()
		{
			if (!string.IsNullOrEmpty(this.DetailColor))
			{
				return this.DetailColor[0];
			}
			return '\0';
		}

		// Token: 0x06008F3B RID: 36667 RVA: 0x0033A0AE File Offset: 0x003382AE
		public string GetRenderColor()
		{
			if (Globals.RenderMode != RenderModeType.Tiles)
			{
				return this.ColorString;
			}
			if (!string.IsNullOrEmpty(this.TileColor))
			{
				return this.TileColor;
			}
			return this.ColorString;
		}

		// Token: 0x06008F3C RID: 36668 RVA: 0x0033A0DC File Offset: 0x003382DC
		public ColorChars getColorChars()
		{
			char c = 'y';
			char c2 = 'k';
			string text;
			if (Globals.RenderMode == RenderModeType.Tiles)
			{
				text = this.getTileColor();
				if (text.IsNullOrEmpty())
				{
					text = this.getColorString();
				}
			}
			else
			{
				text = this.getColorString();
			}
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.LastIndexOf(ColorChars.FOREGROUND_INDICATOR);
				int num2 = text.LastIndexOf(ColorChars.BACKGROUND_INDICATOR);
				if (num >= 0 && num < text.Length - 1)
				{
					c = text[num + 1];
				}
				if (num2 >= 0 && num2 < text.Length - 1)
				{
					c2 = text[num2 + 1];
				}
			}
			return new ColorChars
			{
				detail = this.getDetailColor(),
				foreground = c,
				background = c2
			};
		}

		// Token: 0x06008F3D RID: 36669 RVA: 0x0033A194 File Offset: 0x00338394
		public override void BasisError(GameObject Basis, SerializationReader Reader)
		{
			if (this.Tile.IsNullOrEmpty() || SpriteManager.HasTextureInfo(this.Tile))
			{
				return;
			}
			GameObjectBlueprint blueprint = Basis.GetBlueprint(false);
			string text = ((blueprint != null) ? blueprint.GetPartParameter<string>("Render", "Tile", "") : null);
			if (!text.IsNullOrEmpty() && SpriteManager.HasTextureInfo(this.Tile))
			{
				this.Tile = text;
				return;
			}
			this.Tile = null;
		}

		// Token: 0x04003BA0 RID: 15264
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool RenderPool = new IPartPool(128);

		// Token: 0x04003BA1 RID: 15265
		public const int RENDER_FLAG_OCCLUDING = 1;

		// Token: 0x04003BA2 RID: 15266
		public const int RENDER_FLAG_IF_DARK = 2;

		// Token: 0x04003BA3 RID: 15267
		public const int RENDER_FLAG_VISIBLE = 4;

		// Token: 0x04003BA4 RID: 15268
		public const int RENDER_FLAG_CUSTOM = 8;

		// Token: 0x04003BA5 RID: 15269
		public const int RENDER_FLAG_HFLIP = 16;

		// Token: 0x04003BA6 RID: 15270
		public const int RENDER_FLAG_VFLIP = 32;

		// Token: 0x04003BA7 RID: 15271
		public const int RENDER_FLAG_IGNORE_COLOR_FOR_STACK = 64;

		// Token: 0x04003BA8 RID: 15272
		public const int RENDER_FLAG_IGNORE_TILE_FOR_STACK = 128;

		// Token: 0x04003BA9 RID: 15273
		public const int RENDER_FLAG_NEVER = 256;

		// Token: 0x04003BAA RID: 15274
		public const int RENDER_FLAG_PARTY_FLIP = 512;

		// Token: 0x04003BAB RID: 15275
		public string DisplayName;

		// Token: 0x04003BAC RID: 15276
		public string RenderString = "?";

		// Token: 0x04003BAD RID: 15277
		public string ColorString = "&y";

		// Token: 0x04003BAE RID: 15278
		public string DetailColor = "";

		// Token: 0x04003BAF RID: 15279
		public string TileColor = "";

		// Token: 0x04003BB0 RID: 15280
		public int RenderLayer;

		// Token: 0x04003BB1 RID: 15281
		public int Flags = 4;

		// Token: 0x04003BB2 RID: 15282
		public string Tile;
	}
}
