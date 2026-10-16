using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Core;

namespace XRL.World
{
	// Token: 0x0200093E RID: 2366
	public class RenderEvent : IRenderable, IDisposable
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06005B4A RID: 23370 RVA: 0x001B96E9 File Offset: 0x001B78E9
		// (set) Token: 0x06005B4B RID: 23371 RVA: 0x001B96F1 File Offset: 0x001B78F1
		public string Tile
		{
			get
			{
				return this._Tile;
			}
			set
			{
				if (value == null)
				{
					this.HFlip = false;
					this.VFlip = false;
				}
				this._Tile = value;
			}
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x001B970B File Offset: 0x001B790B
		string IRenderable.getTile()
		{
			return this.Tile;
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x001B9713 File Offset: 0x001B7913
		public string getColorString()
		{
			return this.ColorString;
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x001B971B File Offset: 0x001B791B
		public string getRenderString()
		{
			return this.RenderString;
		}

		// Token: 0x06005B4F RID: 23375 RVA: 0x001B9723 File Offset: 0x001B7923
		public char getDetailColor()
		{
			if (string.IsNullOrEmpty(this.DetailColor))
			{
				return '\0';
			}
			return this.DetailColor[0];
		}

		// Token: 0x06005B50 RID: 23376 RVA: 0x001B9740 File Offset: 0x001B7940
		public RenderEvent GreyOutForUI()
		{
			this.DetailColor = "K";
			this.ColorString = "&K";
			return this;
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x001B975C File Offset: 0x001B795C
		public void RenderEffectIndicator(string renderString, string tile, string colorString, string detailColor, int frameHint, int durationHint = 10)
		{
			if (this.UI)
			{
				return;
			}
			int num = XRLCore.CurrentFrame % 60;
			if (num >= frameHint && num < frameHint + durationHint)
			{
				this.Tile = tile;
				this.RenderString = renderString;
				this.ColorString = colorString;
				this.DetailColor = detailColor;
			}
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x001B97A8 File Offset: 0x001B79A8
		public bool ImpostersMatch(RenderEvent ev)
		{
			if (ev.Imposters.Count != this.Imposters.Count)
			{
				return false;
			}
			for (int i = 0; i < ev.Imposters.Count; i++)
			{
				if (this.Imposters[i].SameAs(ev.Imposters[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x001B980C File Offset: 0x001B7A0C
		public void Reset()
		{
			this.Final = null;
			this.ColorString = "&y";
			this.BackgroundString = "";
			this.HighestLayer = -1;
			this.WantsToPaint = false;
			this.Alt = false;
			this.CustomDraw = false;
			this.AsIfKnown = false;
			this._Tile = null;
			this.RenderString = " ";
			this.DetailColor = null;
			this.Lit = LightLevel.Light;
			this.Visible = false;
			this.NoWake = false;
			this.HFlip = false;
			this.VFlip = false;
			this.UI = false;
			this.Context = null;
			this.Imposters.Clear();
			if (this.Builder.Length > 0)
			{
				this.Builder.Clear();
			}
			this.ColorStringPriority = 0;
			this.DetailColorPriority = 0;
			this.WantsBackdrop = null;
			this.BackdropBleedthrough = false;
			this.BackgroundStringPriority = 0;
		}

		// Token: 0x06005B54 RID: 23380 RVA: 0x001B98EF File Offset: 0x001B7AEF
		public bool getHFlip()
		{
			return this.HFlip;
		}

		// Token: 0x06005B55 RID: 23381 RVA: 0x001B98F7 File Offset: 0x001B7AF7
		public bool getVFlip()
		{
			return this.VFlip;
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x001B9900 File Offset: 0x001B7B00
		public ColorChars getColorChars()
		{
			char c = 'y';
			char c2 = 'k';
			string colorString = this.getColorString();
			if (!string.IsNullOrEmpty(colorString))
			{
				int num = colorString.LastIndexOf(ColorChars.FOREGROUND_INDICATOR);
				int num2 = colorString.LastIndexOf(ColorChars.BACKGROUND_INDICATOR);
				if (num >= 0 && num < colorString.Length - 1)
				{
					c = colorString[num + 1];
				}
				if (num2 >= 0 && num2 < colorString.Length - 1)
				{
					c2 = colorString[num2 + 1];
				}
			}
			return new ColorChars
			{
				detail = this.getDetailColor(),
				foreground = c,
				background = c2
			};
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x001B9997 File Offset: 0x001B7B97
		public bool ColorsVisible
		{
			get
			{
				return Zone.ColorsVisible(this.Lit) && !this.Alt;
			}
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x001B99B4 File Offset: 0x001B7BB4
		public Color GetForegroundColor()
		{
			if (this.ColorString != null)
			{
				int num = this.ColorString.LastIndexOf('&');
				if (num != -1)
				{
					return ConsoleLib.Console.ColorUtility.FastColorMap[(int)this.ColorString[num + 1]];
				}
			}
			return The.Color.Gray;
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x001B99FE File Offset: 0x001B7BFE
		public char GetForegroundColorChar()
		{
			if (this.ColorString != null && this.ColorString.Length > 1)
			{
				return this.ColorString[1];
			}
			return 'y';
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x001B9A28 File Offset: 0x001B7C28
		public Color GetDetailColor()
		{
			if (this.DetailColor != null && this.DetailColor.Length > 0)
			{
				return ConsoleLib.Console.ColorUtility.FastColorMap[(int)this.DetailColor[0]];
			}
			return this.GetForegroundColor();
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x001B9A6A File Offset: 0x001B7C6A
		public char GetDetailColorChar()
		{
			if (this.DetailColor != null && this.DetailColor.Length > 0)
			{
				return this.DetailColor[0];
			}
			return 'w';
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x001B9A94 File Offset: 0x001B7C94
		public char GetBackgroundColorChar()
		{
			if (this.BackgroundString != null)
			{
				int num = this.BackgroundString.LastIndexOf('^');
				if (num != -1)
				{
					return this.BackgroundString[num + 1];
				}
			}
			if (this.ColorString != null)
			{
				int num2 = this.ColorString.LastIndexOf('^');
				if (num2 != -1)
				{
					return this.ColorString[num2 + 1];
				}
			}
			return 'k';
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x001B9AF8 File Offset: 0x001B7CF8
		public Color GetBackgroundColor()
		{
			if (this.BackgroundString != null)
			{
				int num = this.BackgroundString.LastIndexOf('^');
				if (num != -1)
				{
					return ConsoleLib.Console.ColorUtility.FastColorMap[(int)this.BackgroundString[num + 1]];
				}
			}
			if (this.ColorString != null)
			{
				int num2 = this.ColorString.LastIndexOf('^');
				if (num2 != -1)
				{
					return ConsoleLib.Console.ColorUtility.FastColorMap[(int)this.ColorString[num2 + 1]];
				}
			}
			return ConsoleLib.Console.ColorUtility.FastColorMap[107];
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x001B9B77 File Offset: 0x001B7D77
		string IRenderable.getTileColor()
		{
			return null;
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x001B9B7C File Offset: 0x001B7D7C
		public void TileVariantColors(string SingleColor, string TileForeground, string TileDetail)
		{
			if (this.ColorsVisible)
			{
				if (!this.Tile.IsNullOrEmpty())
				{
					if (!TileForeground.IsNullOrEmpty())
					{
						this.ColorString = TileForeground;
					}
					if (!TileForeground.IsNullOrEmpty())
					{
						this.DetailColor = TileDetail;
						return;
					}
				}
				else if (!SingleColor.IsNullOrEmpty())
				{
					this.ColorString = SingleColor;
				}
			}
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x001B9BCC File Offset: 0x001B7DCC
		public void ApplyColors(string TextForeground, string TileForeground, string TileDetail, string Background, int TextForegroundPriority, int TileForegroundPriority, int TileDetailPriority, int BackgroundPriority)
		{
			if (this.ColorsVisible)
			{
				if (!this.Tile.IsNullOrEmpty())
				{
					if (TileForegroundPriority > this.ColorStringPriority && !TileForeground.IsNullOrEmpty())
					{
						this.ColorString = TileForeground;
						this.ColorStringPriority = TileForegroundPriority;
					}
					if (TileDetailPriority > this.DetailColorPriority && !TileDetail.IsNullOrEmpty())
					{
						this.DetailColor = TileDetail;
						this.DetailColorPriority = TileDetailPriority;
					}
				}
				else if (TextForegroundPriority > this.ColorStringPriority && !TextForeground.IsNullOrEmpty())
				{
					this.ColorString = TextForeground;
					this.ColorStringPriority = TextForegroundPriority;
				}
				if (BackgroundPriority > this.BackgroundStringPriority && !Background.IsNullOrEmpty())
				{
					this.BackgroundString = Background;
					this.BackgroundStringPriority = BackgroundPriority;
				}
			}
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x001B9C7C File Offset: 0x001B7E7C
		public void ApplyColors(string Foreground, string Background, string Detail, int ForegroundPriority, int BackgroundPriority, int DetailPriority)
		{
			if (this.ColorsVisible)
			{
				if (ForegroundPriority > this.ColorStringPriority && !Foreground.IsNullOrEmpty())
				{
					this.ColorString = Foreground;
					this.ColorStringPriority = ForegroundPriority;
				}
				if (BackgroundPriority > this.BackgroundStringPriority && !Background.IsNullOrEmpty())
				{
					this.BackgroundString = Background;
					this.BackgroundStringPriority = BackgroundPriority;
				}
				if (!this.Tile.IsNullOrEmpty() && DetailPriority > this.DetailColorPriority && !Detail.IsNullOrEmpty())
				{
					this.DetailColor = Detail;
					this.DetailColorPriority = DetailPriority;
				}
			}
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x001B9D04 File Offset: 0x001B7F04
		public void ApplyColors(string Foreground, string Detail, int ForegroundPriority, int DetailPriority)
		{
			if (this.ColorsVisible)
			{
				if (ForegroundPriority > this.ColorStringPriority && !Foreground.IsNullOrEmpty())
				{
					this.ColorString = Foreground;
					this.ColorStringPriority = ForegroundPriority;
				}
				if (!this.Tile.IsNullOrEmpty() && DetailPriority > this.DetailColorPriority && !Detail.IsNullOrEmpty())
				{
					this.DetailColor = Detail;
					this.DetailColorPriority = DetailPriority;
				}
			}
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x001B9D66 File Offset: 0x001B7F66
		public void ApplyBackgroundColor(string BackgroundString, int BackgroundStringPriority)
		{
			if (this.ColorsVisible && BackgroundStringPriority > this.BackgroundStringPriority && !BackgroundString.IsNullOrEmpty())
			{
				this.BackgroundString = BackgroundString;
				this.BackgroundStringPriority = BackgroundStringPriority;
			}
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x001B9D8F File Offset: 0x001B7F8F
		public void ApplyDetailColor(string Detail, int DetailPriority)
		{
			if (this.ColorsVisible && DetailPriority > this.ColorStringPriority && !Detail.IsNullOrEmpty())
			{
				this.DetailColor = Detail;
				this.DetailColorPriority = DetailPriority;
			}
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x001B9DB8 File Offset: 0x001B7FB8
		public void ApplyColors(string Foreground, int ForegroundPriority)
		{
			if (this.ColorsVisible && ForegroundPriority > this.ColorStringPriority && !Foreground.IsNullOrEmpty())
			{
				this.ColorString = Foreground;
				this.ColorStringPriority = ForegroundPriority;
			}
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x001B9DE4 File Offset: 0x001B7FE4
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

		// Token: 0x06005B67 RID: 23399 RVA: 0x001B9E4A File Offset: 0x001B804A
		public virtual void Dispose()
		{
			if (this.Builder != null)
			{
				this.Builder = null;
			}
		}

		// Token: 0x04002D12 RID: 11538
		public static readonly int ID = MinEvent.RegisterEvent(typeof(RenderEvent), "ComponentRenderEvent", null, null);

		// Token: 0x04002D13 RID: 11539
		public static readonly int OverlayID = MinEvent.RegisterEvent(typeof(RenderEvent), "OverlayRenderEvent", null, null);

		// Token: 0x04002D14 RID: 11540
		public static readonly int FinalID = MinEvent.RegisterEvent(typeof(RenderEvent), "FinalRenderEvent", null, null);

		// Token: 0x04002D15 RID: 11541
		public int x;

		// Token: 0x04002D16 RID: 11542
		public int y;

		// Token: 0x04002D17 RID: 11543
		public bool WantsToPaint;

		// Token: 0x04002D18 RID: 11544
		public bool DisableFullscreenColorEffects;

		// Token: 0x04002D19 RID: 11545
		public bool Alt;

		// Token: 0x04002D1A RID: 11546
		public bool CustomDraw;

		// Token: 0x04002D1B RID: 11547
		public string WantsBackdrop;

		// Token: 0x04002D1C RID: 11548
		public bool BackdropBleedthrough;

		// Token: 0x04002D1D RID: 11549
		public bool AsIfKnown;

		// Token: 0x04002D1E RID: 11550
		public string _Tile;

		// Token: 0x04002D1F RID: 11551
		public StringBuilder Builder = new StringBuilder();

		// Token: 0x04002D20 RID: 11552
		public string Final;

		// Token: 0x04002D21 RID: 11553
		public string ColorString;

		// Token: 0x04002D22 RID: 11554
		public string RenderString;

		// Token: 0x04002D23 RID: 11555
		public string BackgroundString;

		// Token: 0x04002D24 RID: 11556
		public string DetailColor;

		// Token: 0x04002D25 RID: 11557
		public string Context;

		// Token: 0x04002D26 RID: 11558
		public int ColorStringPriority;

		// Token: 0x04002D27 RID: 11559
		public int DetailColorPriority;

		// Token: 0x04002D28 RID: 11560
		public int BackgroundStringPriority;

		// Token: 0x04002D29 RID: 11561
		public bool Visible;

		// Token: 0x04002D2A RID: 11562
		public bool NoWake;

		// Token: 0x04002D2B RID: 11563
		public bool UI;

		// Token: 0x04002D2C RID: 11564
		public bool HFlip;

		// Token: 0x04002D2D RID: 11565
		public bool VFlip;

		// Token: 0x04002D2E RID: 11566
		public List<ImposterExtra.ImposterInfo> Imposters = new List<ImposterExtra.ImposterInfo>();

		// Token: 0x04002D2F RID: 11567
		public int HighestLayer = -1;

		// Token: 0x04002D30 RID: 11568
		public LightLevel Lit = LightLevel.None;

		// Token: 0x02001A38 RID: 6712
		public static class Pool
		{
			// Token: 0x0600E691 RID: 59025 RVA: 0x00532744 File Offset: 0x00530944
			public static RenderEvent Get()
			{
				RenderEvent.Pool.Entry entry;
				if (!RenderEvent.Pool.Bag.TryTake(out entry))
				{
					entry = new RenderEvent.Pool.Entry();
				}
				entry.Disposed = false;
				return entry;
			}

			// Token: 0x04006866 RID: 26726
			private static ConcurrentBag<RenderEvent.Pool.Entry> Bag = new ConcurrentBag<RenderEvent.Pool.Entry>();

			// Token: 0x02001E8F RID: 7823
			private class Entry : RenderEvent
			{
				// Token: 0x0600F7CD RID: 63437 RVA: 0x0055D20D File Offset: 0x0055B40D
				public override void Dispose()
				{
					if (this.Disposed)
					{
						return;
					}
					base.Reset();
					this.Disposed = true;
					RenderEvent.Pool.Bag.Add(this);
				}

				// Token: 0x040076B7 RID: 30391
				public bool Disposed;
			}
		}
	}
}
