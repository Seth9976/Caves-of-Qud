using System;
using System.Collections.Generic;
using UnityEngine;
using XRL;
using XRL.World;

namespace ConsoleLib.Console
{
	// Token: 0x0200152F RID: 5423
	public class ConsoleChar
	{
		// Token: 0x0600D32D RID: 54061 RVA: 0x004E6B14 File Offset: 0x004E4D14
		public ConsoleChar()
		{
			this.requireExtra<ImposterExtra>();
			this.requireExtra<SoundExtra>();
			this.Clear();
			this.TileLayerForeground = new ConsoleChar.IndexedProperty<int, Color>((int i) => this.TileForeground, delegate(int i, Color v)
			{
				this.TileForeground = v;
			});
			this.TileLayerBackground = new ConsoleChar.IndexedProperty<int, Color>((int i) => this.Detail, delegate(int i, Color v)
			{
				this.Detail = v;
			});
			this.TileLayer = new ConsoleChar.IndexedProperty<int, string>((int i) => this.Tile, delegate(int i, string v)
			{
				this.Tile = v;
			});
		}

		// Token: 0x0600D32E RID: 54062 RVA: 0x004E6BDC File Offset: 0x004E4DDC
		public static Color GetColor(char code)
		{
			Color color;
			if (!ColorUtility.ColorMap.TryGetValue(code, out color))
			{
				MetricsManager.LogError("unknown color code " + code.ToString());
				return Color.magenta;
			}
			return color;
		}

		// Token: 0x0600D32F RID: 54063 RVA: 0x004E6C18 File Offset: 0x004E4E18
		public static Color GetColor(string code)
		{
			Color color;
			if (!ColorUtility.ColorAliasMap.TryGetValue(code, out color))
			{
				MetricsManager.LogError("unknown color code " + code);
				return Color.magenta;
			}
			return color;
		}

		// Token: 0x0600D330 RID: 54064 RVA: 0x004E6C4C File Offset: 0x004E4E4C
		public void Clear()
		{
			this._Char = ' ';
			this._Tile = null;
			ColorUtility.ColorCollection color = The.Color;
			this._TileForeground = (this._Foreground = color.DefaultForeground);
			this._TileBackground = (this._Background = color.DefaultBackground);
			this._Detail = color.DefaultDetail;
			this.HFlip = false;
			this.VFlip = false;
			this.BackdropBleedthrough = false;
			this.WantsBackdrop = null;
			if (this.extras != null)
			{
				for (int i = 0; i < this.extras.Count; i++)
				{
					this.extras[i].Clear(false);
				}
			}
		}

		// Token: 0x0600D331 RID: 54065 RVA: 0x004E6CF0 File Offset: 0x004E4EF0
		public override bool Equals(object obj)
		{
			ConsoleChar consoleChar = obj as ConsoleChar;
			return !(consoleChar == null) && consoleChar.Char == this.Char && !(consoleChar._Tile != this._Tile) && !(consoleChar._Foreground != this._Foreground) && !(consoleChar._Background != this._Background) && !(consoleChar._Detail != this._Detail) && !(consoleChar._TileBackground != this._TileBackground) && !(consoleChar._TileForeground != this._TileForeground) && consoleChar.HFlip == this.HFlip && consoleChar.VFlip == this.VFlip && consoleChar.BackdropBleedthrough == this.BackdropBleedthrough && !(consoleChar.WantsBackdrop != this.WantsBackdrop);
		}

		// Token: 0x0600D332 RID: 54066 RVA: 0x004E6DE3 File Offset: 0x004E4FE3
		public override int GetHashCode()
		{
			return new { this.Char, this._Foreground, this._Background, this._Detail, this._TileBackground, this._TileForeground }.GetHashCode();
		}

		// Token: 0x0600D333 RID: 54067 RVA: 0x004E6E13 File Offset: 0x004E5013
		public static bool operator ==(ConsoleChar a, ConsoleChar b)
		{
			if (a == null)
			{
				return b == null;
			}
			return a.Equals(b);
		}

		// Token: 0x0600D334 RID: 54068 RVA: 0x004E6E26 File Offset: 0x004E5026
		public static bool operator !=(ConsoleChar a, ConsoleChar b)
		{
			return !(a == b);
		}

		// Token: 0x0600D335 RID: 54069 RVA: 0x004E6E32 File Offset: 0x004E5032
		public ConsoleChar GetCopy()
		{
			ConsoleChar consoleChar = new ConsoleChar();
			consoleChar.Copy(this);
			return consoleChar;
		}

		// Token: 0x0600D336 RID: 54070 RVA: 0x004E6E40 File Offset: 0x004E5040
		public void Copy(ConsoleChar C)
		{
			this._Char = C._Char;
			this._Tile = C._Tile;
			this._Foreground = C._Foreground;
			this._Background = C._Background;
			this._TileForeground = C._TileForeground;
			this._TileBackground = C._TileBackground;
			this._Detail = C._Detail;
			this.HFlip = C.HFlip;
			this.VFlip = C.VFlip;
			this.BackdropBleedthrough = C.BackdropBleedthrough;
			this.WantsBackdrop = C.WantsBackdrop;
			this.soundExtra.CopyFrom(C.soundExtra);
			this.imposterExtra.CopyFrom(C.imposterExtra);
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x0600D337 RID: 54071 RVA: 0x004E6EF3 File Offset: 0x004E50F3
		// (set) Token: 0x0600D338 RID: 54072 RVA: 0x004E6EFB File Offset: 0x004E50FB
		public Color TileForeground
		{
			get
			{
				return this._TileForeground;
			}
			set
			{
				this._TileForeground = value;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600D339 RID: 54073 RVA: 0x004E6F04 File Offset: 0x004E5104
		// (set) Token: 0x0600D33A RID: 54074 RVA: 0x004E6F0C File Offset: 0x004E510C
		public Color TileBackground
		{
			get
			{
				return this._TileBackground;
			}
			set
			{
				this._TileBackground = value;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x0600D33B RID: 54075 RVA: 0x004E6F15 File Offset: 0x004E5115
		// (set) Token: 0x0600D33C RID: 54076 RVA: 0x004E6F1D File Offset: 0x004E511D
		public string Tile
		{
			get
			{
				return this._Tile;
			}
			set
			{
				this.Char = '\0';
				this._Tile = value;
				this.imposterExtra.Clear(true);
				this.soundExtra.Clear(true);
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600D33D RID: 54077 RVA: 0x004E6F45 File Offset: 0x004E5145
		// (set) Token: 0x0600D33E RID: 54078 RVA: 0x004E6F4D File Offset: 0x004E514D
		public char Char
		{
			get
			{
				return this._Char;
			}
			set
			{
				if (value != '\0')
				{
					this.HFlip = false;
					this.VFlip = false;
					this.imposterExtra.Clear(true);
					this.soundExtra.Clear(true);
				}
				this._Char = value;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (set) Token: 0x0600D33F RID: 54079 RVA: 0x004E6F7F File Offset: 0x004E517F
		public ushort Attributes
		{
			[Obsolete("SetColorsFromOldCharCode(value) instead")]
			set
			{
				this.SetColorsFromOldCharCode(value);
			}
		}

		// Token: 0x0600D340 RID: 54080 RVA: 0x004E6F88 File Offset: 0x004E5188
		public void SetColorsFromOldCharCode(ushort color)
		{
			foreach (KeyValuePair<ushort, char> keyValuePair in ColorUtility.ColorAttributeToCharMap)
			{
				if ((color & keyValuePair.Key) == keyValuePair.Key)
				{
					this.Foreground = ColorUtility.colorFromChar(keyValuePair.Value);
				}
				if ((color & (ushort)(keyValuePair.Key << 5)) == (ushort)(keyValuePair.Key << 5))
				{
					this.Background = ColorUtility.colorFromChar(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600D341 RID: 54081 RVA: 0x004E7024 File Offset: 0x004E5224
		public void Set(RenderEvent Event)
		{
			this._Tile = Event.Tile;
			this.Foreground = Event.GetForegroundColor();
			this.Background = Event.GetBackgroundColor();
			this.Detail = Event.GetDetailColor();
			this.WantsBackdrop = Event.WantsBackdrop;
			this.BackdropBleedthrough = Event.BackdropBleedthrough;
			List<ImposterExtra.ImposterInfo> imposters = this.imposterExtra.imposters;
			imposters.Clear();
			this.soundExtra.Clear(true);
			if (this._Tile != null)
			{
				this._Char = '\0';
				this.BackupChar = Event.RenderString[0];
				this.HFlip = Event.HFlip;
				this.VFlip = Event.VFlip;
				if (Event.Imposters.Count > 0)
				{
					imposters.AddRange(Event.Imposters);
				}
			}
			else
			{
				this._Char = Event.RenderString[0];
				this.BackupChar = '\0';
				this.HFlip = false;
				this.VFlip = false;
			}
			if (!Event.DisableFullscreenColorEffects && Event.Lit == LightLevel.Darkvision)
			{
				this._Background = new Color(this._Background.r, this._Background.g + 0.025f, this._Background.b);
			}
		}

		// Token: 0x0600D342 RID: 54082 RVA: 0x004E7157 File Offset: 0x004E5357
		public void SetDetail(char colorCode)
		{
			this._Detail = ColorUtility.ColorMap[colorCode];
		}

		// Token: 0x0600D343 RID: 54083 RVA: 0x004E716A File Offset: 0x004E536A
		public void SetDetail(Color color)
		{
			this._Detail = color;
		}

		// Token: 0x0600D344 RID: 54084 RVA: 0x004E7173 File Offset: 0x004E5373
		public void SetForeground(char colorCode)
		{
			this._Foreground = ColorUtility.ColorMap[colorCode];
		}

		// Token: 0x0600D345 RID: 54085 RVA: 0x004E7186 File Offset: 0x004E5386
		public void SetForeground(Color color)
		{
			this._Foreground = color;
		}

		// Token: 0x0600D346 RID: 54086 RVA: 0x004E718F File Offset: 0x004E538F
		public void SetBackground(Color color)
		{
			this._Background = color;
		}

		// Token: 0x0600D347 RID: 54087 RVA: 0x004E7198 File Offset: 0x004E5398
		public void SetBackground(char colorCode)
		{
			this._Background = ColorUtility.ColorMap[colorCode];
		}

		// Token: 0x0600D348 RID: 54088 RVA: 0x004E71AC File Offset: 0x004E53AC
		public void SetColors(string Color)
		{
			char c = '\0';
			bool flag = true;
			bool flag2 = true;
			Dictionary<char, Color> colorMap = ColorUtility.ColorMap;
			int num = Color.Length - 1;
			while (num >= 0 && flag && flag2)
			{
				char c2 = Color[num];
				Color color2;
				if (c2 == '^')
				{
					Color color;
					if (flag2 && colorMap.TryGetValue(c, out color))
					{
						this._Background = color;
						flag2 = false;
					}
				}
				else if (c2 == '&' && flag && colorMap.TryGetValue(c, out color2))
				{
					this._Foreground = color2;
					flag = false;
				}
				c = c2;
				num--;
			}
		}

		// Token: 0x0600D349 RID: 54089 RVA: 0x004E7230 File Offset: 0x004E5430
		public void SetColors(RenderEvent E)
		{
			char c = '\0';
			bool flag = true;
			bool flag2 = true;
			Dictionary<char, Color> colorMap = ColorUtility.ColorMap;
			if (E.BackgroundString != null)
			{
				int num = E.BackgroundString.Length - 1;
				while (num >= 0 && flag2)
				{
					char c2 = E.BackgroundString[num];
					Color color;
					if (c2 == '^' && colorMap.TryGetValue(c, out color))
					{
						this._Background = color;
						flag2 = false;
						break;
					}
					c = c2;
					num--;
				}
				c = '0';
			}
			if (E.ColorString != null)
			{
				int num2 = E.ColorString.Length - 1;
				while (num2 >= 0 && (flag || flag2))
				{
					char c2 = E.ColorString[num2];
					Color color3;
					if (c2 == '^')
					{
						Color color2;
						if (flag2 && colorMap.TryGetValue(c, out color2))
						{
							this._Background = color2;
							flag2 = false;
						}
					}
					else if (c2 == '&' && flag && colorMap.TryGetValue(c, out color3))
					{
						this._Foreground = color3;
						flag = false;
					}
					c = c2;
					num2--;
				}
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x0600D34A RID: 54090 RVA: 0x004E731E File Offset: 0x004E551E
		// (set) Token: 0x0600D34B RID: 54091 RVA: 0x004E7326 File Offset: 0x004E5526
		public Color Foreground
		{
			get
			{
				return this._Foreground;
			}
			set
			{
				this._Foreground = value;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600D34C RID: 54092 RVA: 0x004E732F File Offset: 0x004E552F
		// (set) Token: 0x0600D34D RID: 54093 RVA: 0x004E7337 File Offset: 0x004E5537
		public Color Detail
		{
			get
			{
				return this._Detail;
			}
			set
			{
				this._Detail = value;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600D34E RID: 54094 RVA: 0x004E7340 File Offset: 0x004E5540
		// (set) Token: 0x0600D34F RID: 54095 RVA: 0x004E7348 File Offset: 0x004E5548
		public Color Background
		{
			get
			{
				return this._Background;
			}
			set
			{
				this.SetBackground(value);
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600D350 RID: 54096 RVA: 0x004E7354 File Offset: 0x004E5554
		public char ForegroundCode
		{
			get
			{
				char c;
				if (!ColorUtility.ColorToCharMap.TryGetValue(this._Foreground, out c))
				{
					return 'k';
				}
				return c;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600D351 RID: 54097 RVA: 0x004E737C File Offset: 0x004E557C
		public char BackgroundCode
		{
			get
			{
				char c;
				if (!ColorUtility.ColorToCharMap.TryGetValue(this._Background, out c))
				{
					return 'k';
				}
				return c;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600D352 RID: 54098 RVA: 0x004E73A4 File Offset: 0x004E55A4
		public char DetailCode
		{
			get
			{
				char c;
				if (!ColorUtility.ColorToCharMap.TryGetValue(this._Detail, out c))
				{
					return 'k';
				}
				return c;
			}
		}

		// Token: 0x0600D353 RID: 54099 RVA: 0x004E73CC File Offset: 0x004E55CC
		public T requireExtra<T>() where T : IConsoleCharExtra, new()
		{
			if (this.extras == null)
			{
				this.extras = new List<IConsoleCharExtra>();
				this.extraMap = new Dictionary<Type, IConsoleCharExtra>();
			}
			IConsoleCharExtra consoleCharExtra;
			if (this.extraMap.TryGetValue(typeof(T), out consoleCharExtra))
			{
				return consoleCharExtra as T;
			}
			T t = new T();
			this.extras.Add(t);
			this.extraMap.Add(typeof(T), t);
			if (typeof(T) == typeof(ImposterExtra))
			{
				this.imposterExtra = t as ImposterExtra;
			}
			if (typeof(T) == typeof(SoundExtra))
			{
				this.soundExtra = t as SoundExtra;
			}
			return t;
		}

		// Token: 0x0600D354 RID: 54100 RVA: 0x004E74A8 File Offset: 0x004E56A8
		public void BeforeRender(int x, int y, ex3DSprite2 sprite, ScreenBuffer buffer)
		{
			if (this.extras == null)
			{
				return;
			}
			for (int i = 0; i < this.extras.Count; i++)
			{
				this.extras[i].BeforeRender(x, y, this, sprite, buffer);
			}
		}

		// Token: 0x0600D355 RID: 54101 RVA: 0x004E74EC File Offset: 0x004E56EC
		public void AfterRender(int x, int y, ex3DSprite2 sprite, ScreenBuffer buffer)
		{
			if (this.extras == null)
			{
				return;
			}
			for (int i = 0; i < this.extras.Count; i++)
			{
				this.extras[i].AfterRender(x, y, this, sprite, buffer);
			}
		}

		// Token: 0x040053CE RID: 21454
		[Obsolete("Use TileForeground instead.")]
		public ConsoleChar.IndexedProperty<int, Color> TileLayerForeground;

		// Token: 0x040053CF RID: 21455
		[Obsolete("Use TileBackground instead")]
		public ConsoleChar.IndexedProperty<int, Color> TileLayerBackground;

		// Token: 0x040053D0 RID: 21456
		[Obsolete("Use Tile instead")]
		public ConsoleChar.IndexedProperty<int, string> TileLayer;

		// Token: 0x040053D1 RID: 21457
		public Color _Foreground = Color.grey;

		// Token: 0x040053D2 RID: 21458
		public Color _Background = Color.black;

		// Token: 0x040053D3 RID: 21459
		public Color _TileForeground = Color.grey;

		// Token: 0x040053D4 RID: 21460
		public Color _TileBackground = Color.black;

		// Token: 0x040053D5 RID: 21461
		public Color _Detail = Color.magenta;

		// Token: 0x040053D6 RID: 21462
		public bool HFlip;

		// Token: 0x040053D7 RID: 21463
		public bool VFlip;

		// Token: 0x040053D8 RID: 21464
		public bool BackdropBleedthrough;

		// Token: 0x040053D9 RID: 21465
		public string WantsBackdrop;

		// Token: 0x040053DA RID: 21466
		public string _Tile;

		// Token: 0x040053DB RID: 21467
		public char BackupChar;

		// Token: 0x040053DC RID: 21468
		public char _Char;

		// Token: 0x040053DD RID: 21469
		public ImposterExtra imposterExtra;

		// Token: 0x040053DE RID: 21470
		public SoundExtra soundExtra;

		// Token: 0x040053DF RID: 21471
		public List<IConsoleCharExtra> extras;

		// Token: 0x040053E0 RID: 21472
		private Dictionary<Type, IConsoleCharExtra> extraMap;

		// Token: 0x02001E38 RID: 7736
		public class IndexedProperty<TIndex, TValue>
		{
			// Token: 0x0600F6DE RID: 63198 RVA: 0x0055AA75 File Offset: 0x00558C75
			public IndexedProperty(Func<TIndex, TValue> getFunc, Action<TIndex, TValue> setAction)
			{
				this.GetFunc = getFunc;
				this.SetAction = setAction;
			}

			// Token: 0x1700102E RID: 4142
			public TValue this[TIndex i]
			{
				get
				{
					return this.GetFunc(i);
				}
				set
				{
					this.SetAction(i, value);
				}
			}

			// Token: 0x04007612 RID: 30226
			private readonly Action<TIndex, TValue> SetAction;

			// Token: 0x04007613 RID: 30227
			private readonly Func<TIndex, TValue> GetFunc;
		}
	}
}
