using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.Core;
using XRL.World;

namespace ConsoleLib.Console
{
	// Token: 0x02001542 RID: 5442
	[GenerateSerializationPartial]
	[Serializable]
	public class Renderable : IRenderable, IComposite
	{
		// Token: 0x0600D418 RID: 54296 RVA: 0x004ECFB0 File Offset: 0x004EB1B0
		public Renderable()
		{
		}

		// Token: 0x0600D419 RID: 54297 RVA: 0x004ECFD0 File Offset: 0x004EB1D0
		public Renderable(Renderable Source)
		{
			this.Tile = Source.Tile;
			this.RenderString = Source.RenderString;
			this.ColorString = Source.ColorString;
			this.TileColor = Source.TileColor;
			this.DetailColor = Source.DetailColor;
		}

		// Token: 0x0600D41A RID: 54298 RVA: 0x004ED038 File Offset: 0x004EB238
		public Renderable(Renderable Source, string Tile = null, string RenderString = null, string ColorString = null, string TileColor = null, char? DetailColor = null)
		{
			this.Tile = Tile ?? ((Source != null) ? Source.Tile : null);
			this.RenderString = RenderString ?? ((Source != null) ? Source.RenderString : null);
			this.ColorString = ColorString ?? ((Source != null) ? Source.ColorString : null);
			this.TileColor = TileColor ?? ((Source != null) ? Source.TileColor : null);
			this.DetailColor = DetailColor ?? ((Source != null) ? Source.DetailColor : '\0');
		}

		// Token: 0x0600D41B RID: 54299 RVA: 0x004ED0E6 File Offset: 0x004EB2E6
		public Renderable(IRenderable Source)
		{
			this.Copy(Source);
		}

		// Token: 0x0600D41C RID: 54300 RVA: 0x004ED10B File Offset: 0x004EB30B
		public void Copy(IRenderable Source)
		{
			this.Tile = Source.getTile();
			this.RenderString = Source.getRenderString();
			this.ColorString = Source.getColorString();
			this.TileColor = Source.getTileColor();
			this.DetailColor = Source.getDetailColor();
		}

		// Token: 0x0600D41D RID: 54301 RVA: 0x004ED149 File Offset: 0x004EB349
		public static Renderable UITile(string tilePath)
		{
			return Renderable.UITile(tilePath, 'y', 'W', " ", '\0');
		}

		// Token: 0x0600D41E RID: 54302 RVA: 0x004ED15C File Offset: 0x004EB35C
		public static Renderable UITile(string tilePath, char foregroundColorCode = 'y', char detailColorCode = 'w', string noTileAlt = " ", char noTileColor = '\0')
		{
			char c = ((noTileColor != '\0') ? noTileColor : foregroundColorCode);
			string text = string.Format("&{0}", c);
			return new Renderable(tilePath, noTileAlt, text, "&" + foregroundColorCode.ToString(), detailColorCode);
		}

		// Token: 0x0600D41F RID: 54303 RVA: 0x004ED19E File Offset: 0x004EB39E
		private static char ColorCodeFromString(string color)
		{
			if (string.IsNullOrEmpty(color))
			{
				return '\0';
			}
			if (color.Length == 1)
			{
				return color[0];
			}
			return color[1];
		}

		// Token: 0x0600D420 RID: 54304 RVA: 0x004ED1C4 File Offset: 0x004EB3C4
		public static Renderable UITile(string tilePath, string foregroundColorCode = "y", string detailColorCode = "w", string noTileAlt = " ", string noTileColor = null)
		{
			char c = Renderable.ColorCodeFromString(noTileColor);
			if (c == '\0')
			{
				c = Renderable.ColorCodeFromString(foregroundColorCode);
			}
			string text = string.Format("&{0}", c);
			return new Renderable(tilePath, noTileAlt, text, "&" + Renderable.ColorCodeFromString(foregroundColorCode).ToString(), Renderable.ColorCodeFromString(detailColorCode));
		}

		// Token: 0x0600D421 RID: 54305 RVA: 0x004ED21C File Offset: 0x004EB41C
		public Renderable(IRenderable Source, string Tile = null, string RenderString = null, string ColorString = null, string TileColor = null, char? DetailColor = null)
		{
			this.Tile = Tile ?? Source.getTile();
			this.RenderString = RenderString ?? Source.getRenderString();
			this.ColorString = ColorString ?? Source.getColorString();
			this.TileColor = TileColor ?? Source.getTileColor();
			this.DetailColor = DetailColor ?? Source.getDetailColor();
		}

		// Token: 0x0600D422 RID: 54306 RVA: 0x004ED2AC File Offset: 0x004EB4AC
		public Renderable(string Tile, string RenderString = " ", string ColorString = "", string TileColor = null, char DetailColor = '\0')
			: this()
		{
			this.setTile(Tile);
			this.setRenderString(RenderString);
			this.setColorString(ColorString);
			this.setTileColor(TileColor);
			this.setDetailColor(DetailColor);
		}

		// Token: 0x0600D423 RID: 54307 RVA: 0x004ED2DE File Offset: 0x004EB4DE
		public Renderable(GameObjectBlueprint Blueprint)
		{
			this.Set(Blueprint);
		}

		// Token: 0x0600D424 RID: 54308 RVA: 0x004ED304 File Offset: 0x004EB504
		public void Set(GameObjectBlueprint Blueprint)
		{
			GamePartBlueprint part = Blueprint.GetPart("Render");
			this.Tile = part.GetParameterString("Tile", null);
			this.RenderString = part.GetParameterString("RenderString", " ");
			this.TileColor = part.GetParameterString("TileColor", null);
			this.ColorString = part.GetParameterString("ColorString", "");
			this.DetailColor = part.GetParameterString("DetailColor", "\0")[0];
		}

		// Token: 0x0600D425 RID: 54309 RVA: 0x004ED389 File Offset: 0x004EB589
		public Renderable setTile(string val)
		{
			this.Tile = val;
			return this;
		}

		// Token: 0x0600D426 RID: 54310 RVA: 0x004ED393 File Offset: 0x004EB593
		public string getTile()
		{
			return this.Tile;
		}

		// Token: 0x0600D427 RID: 54311 RVA: 0x004ED39B File Offset: 0x004EB59B
		public Renderable setRenderString(string val)
		{
			this.RenderString = val;
			return this;
		}

		// Token: 0x0600D428 RID: 54312 RVA: 0x004ED3A5 File Offset: 0x004EB5A5
		public string getRenderString()
		{
			return this.RenderString;
		}

		// Token: 0x0600D429 RID: 54313 RVA: 0x004ED3AD File Offset: 0x004EB5AD
		public Renderable setColorString(string val)
		{
			this.ColorString = val;
			return this;
		}

		// Token: 0x0600D42A RID: 54314 RVA: 0x004ED3B7 File Offset: 0x004EB5B7
		public string getColorString()
		{
			return this.ColorString;
		}

		// Token: 0x0600D42B RID: 54315 RVA: 0x004ED3BF File Offset: 0x004EB5BF
		public Renderable setTileColor(string val)
		{
			this.TileColor = val;
			return this;
		}

		// Token: 0x0600D42C RID: 54316 RVA: 0x004ED3C9 File Offset: 0x004EB5C9
		public string getTileColor()
		{
			return this.TileColor;
		}

		// Token: 0x0600D42D RID: 54317 RVA: 0x004ED3D1 File Offset: 0x004EB5D1
		public Renderable setDetailColor(char val)
		{
			this.DetailColor = val;
			return this;
		}

		// Token: 0x0600D42E RID: 54318 RVA: 0x004ED3DB File Offset: 0x004EB5DB
		public Renderable setDetailColor(string val)
		{
			this.DetailColor = (string.IsNullOrEmpty(val) ? '\0' : val[0]);
			return this;
		}

		// Token: 0x0600D42F RID: 54319 RVA: 0x004ED3F6 File Offset: 0x004EB5F6
		public char getDetailColor()
		{
			return this.DetailColor;
		}

		// Token: 0x0600D430 RID: 54320 RVA: 0x004ED400 File Offset: 0x004EB600
		public char GetForegroundColor()
		{
			char? c = ColorUtility.FindLastForeground(this.ResolveColorString());
			if (c == null)
			{
				return 'y';
			}
			return c.GetValueOrDefault();
		}

		// Token: 0x0600D431 RID: 54321 RVA: 0x004ED42C File Offset: 0x004EB62C
		public char GetBackgroundColor()
		{
			char? c = ColorUtility.FindLastBackground(this.ResolveColorString());
			if (c == null)
			{
				return 'k';
			}
			return c.GetValueOrDefault();
		}

		// Token: 0x0600D432 RID: 54322 RVA: 0x004ED458 File Offset: 0x004EB658
		public string ResolveColorString()
		{
			if (Globals.RenderMode == RenderModeType.Tiles)
			{
				string tileColor = this.getTileColor();
				if (!tileColor.IsNullOrEmpty())
				{
					return tileColor;
				}
			}
			return this.getColorString();
		}

		// Token: 0x0600D433 RID: 54323 RVA: 0x004ED484 File Offset: 0x004EB684
		public ColorChars getColorChars()
		{
			char c = 'y';
			char c2 = 'k';
			string text = this.ResolveColorString();
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
			if (!text.IsNullOrEmpty())
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

		// Token: 0x0600D434 RID: 54324 RVA: 0x004ED542 File Offset: 0x004EB742
		public virtual bool getHFlip()
		{
			return false;
		}

		// Token: 0x0600D435 RID: 54325 RVA: 0x004ED545 File Offset: 0x004EB745
		public virtual bool getVFlip()
		{
			return false;
		}

		// Token: 0x0600D436 RID: 54326 RVA: 0x004ED548 File Offset: 0x004EB748
		public string GetSpriteName()
		{
			if (this.Tile != null)
			{
				return this.Tile;
			}
			if (this.RenderString.Length == 0)
			{
				return "Text/" + 32.ToString() + ".bmp";
			}
			return "Text/" + ((int)this.RenderString[0]).ToString() + ".bmp";
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x0600D437 RID: 54327 RVA: 0x004ED5AE File Offset: 0x004EB7AE
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600D438 RID: 54328 RVA: 0x004ED5B1 File Offset: 0x004EB7B1
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Tile);
			Writer.WriteOptimized(this.RenderString);
			Writer.WriteOptimized(this.ColorString);
			Writer.WriteOptimized(this.TileColor);
			Writer.Write(this.DetailColor);
		}

		// Token: 0x0600D439 RID: 54329 RVA: 0x004ED5EF File Offset: 0x004EB7EF
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Tile = Reader.ReadOptimizedString();
			this.RenderString = Reader.ReadOptimizedString();
			this.ColorString = Reader.ReadOptimizedString();
			this.TileColor = Reader.ReadOptimizedString();
			this.DetailColor = Reader.ReadChar();
		}

		// Token: 0x04005515 RID: 21781
		public string Tile;

		// Token: 0x04005516 RID: 21782
		public string RenderString = " ";

		// Token: 0x04005517 RID: 21783
		public string ColorString = "";

		// Token: 0x04005518 RID: 21784
		public string TileColor;

		// Token: 0x04005519 RID: 21785
		public char DetailColor;
	}
}
