using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200020D RID: 525
	[NativeHeader("Runtime/Math/Color.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("ColorRGBAf")]
	public struct Color : IEquatable<Color>, IFormattable
	{
		// Token: 0x06001B89 RID: 7049 RVA: 0x000310CF File Offset: 0x0002F2CF
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000310EF File Offset: 0x0002F2EF
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00031114 File Offset: 0x0002F314
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00031130 File Offset: 0x0002F330
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0003114C File Offset: 0x0002F34C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F3";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format, formatProvider),
				this.g.ToString(format, formatProvider),
				this.b.ToString(format, formatProvider),
				this.a.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x000311D4 File Offset: 0x0002F3D4
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00031200 File Offset: 0x0002F400
		public override bool Equals(object other)
		{
			Color color;
			bool flag;
			if (other is Color)
			{
				color = (Color)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(color);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00031234 File Offset: 0x0002F434
		public bool Equals(Color other)
		{
			return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00031294 File Offset: 0x0002F494
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000312E0 File Offset: 0x0002F4E0
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0003132C File Offset: 0x0002F52C
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00031378 File Offset: 0x0002F578
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000313B0 File Offset: 0x0002F5B0
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000313E8 File Offset: 0x0002F5E8
		public static Color operator /(Color a, float b)
		{
			return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00031420 File Offset: 0x0002F620
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs == rhs;
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00031444 File Offset: 0x0002F644
		public static bool operator !=(Color lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x00031460 File Offset: 0x0002F660
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000314D8 File Offset: 0x0002F6D8
		public static Color LerpUnclamped(Color a, Color b, float t)
		{
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00031548 File Offset: 0x0002F748
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal Color RGBMultiplied(float multiplier)
		{
			return new Color(this.r * multiplier, this.g * multiplier, this.b * multiplier, this.a);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00031580 File Offset: 0x0002F780
		internal Color AlphaMultiplied(float multiplier)
		{
			return new Color(this.r, this.g, this.b, this.a * multiplier);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000315B4 File Offset: 0x0002F7B4
		internal Color RGBMultiplied(Color multiplier)
		{
			return new Color(this.r * multiplier.r, this.g * multiplier.g, this.b * multiplier.b, this.a);
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000315F8 File Offset: 0x0002F7F8
		public static Color red
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x00031624 File Offset: 0x0002F824
		public static Color green
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x00031650 File Offset: 0x0002F850
		public static Color blue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0003167C File Offset: 0x0002F87C
		public static Color white
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000316A8 File Offset: 0x0002F8A8
		public static Color black
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001BA3 RID: 7075 RVA: 0x000316D4 File Offset: 0x0002F8D4
		public static Color yellow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x00031700 File Offset: 0x0002F900
		public static Color cyan
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0003172C File Offset: 0x0002F92C
		public static Color magenta
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00031758 File Offset: 0x0002F958
		public static Color gray
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00031784 File Offset: 0x0002F984
		public static Color grey
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000317B0 File Offset: 0x0002F9B0
		public static Color clear
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x000317DC File Offset: 0x0002F9DC
		public float grayscale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0.299f * this.r + 0.587f * this.g + 0.114f * this.b;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00031814 File Offset: 0x0002FA14
		public Color linear
		{
			get
			{
				return new Color(Mathf.GammaToLinearSpace(this.r), Mathf.GammaToLinearSpace(this.g), Mathf.GammaToLinearSpace(this.b), this.a);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x00031854 File Offset: 0x0002FA54
		public Color gamma
		{
			get
			{
				return new Color(Mathf.LinearToGammaSpace(this.r), Mathf.LinearToGammaSpace(this.g), Mathf.LinearToGammaSpace(this.b), this.a);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x00031894 File Offset: 0x0002FA94
		public float maxColorComponent
		{
			get
			{
				return Mathf.Max(Mathf.Max(this.r, this.g), this.b);
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000318C4 File Offset: 0x0002FAC4
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000318F4 File Offset: 0x0002FAF4
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}

		// Token: 0x1700048E RID: 1166
		public float this[int index]
		{
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.r;
					break;
				case 1:
					num = this.g;
					break;
				case 2:
					num = this.b;
					break;
				case 3:
					num = this.a;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color index(" + index.ToString() + ")!");
				}
				return num;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.r = value;
					break;
				case 1:
					this.g = value;
					break;
				case 2:
					this.b = value;
					break;
				case 3:
					this.a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color index(" + index.ToString() + ")!");
				}
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000319FC File Offset: 0x0002FBFC
		public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V)
		{
			bool flag = rgbColor.b > rgbColor.g && rgbColor.b > rgbColor.r;
			if (flag)
			{
				Color.RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
			}
			else
			{
				bool flag2 = rgbColor.g > rgbColor.r;
				if (flag2)
				{
					Color.RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
				}
				else
				{
					Color.RGBToHSVHelper(0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
				}
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00031AA4 File Offset: 0x0002FCA4
		private static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V)
		{
			V = dominantcolor;
			bool flag = V != 0f;
			if (flag)
			{
				bool flag2 = colorone > colortwo;
				float num;
				if (flag2)
				{
					num = colortwo;
				}
				else
				{
					num = colorone;
				}
				float num2 = V - num;
				bool flag3 = num2 != 0f;
				if (flag3)
				{
					S = num2 / V;
					H = offset + (colorone - colortwo) / num2;
				}
				else
				{
					S = 0f;
					H = offset + (colorone - colortwo);
				}
				H /= 6f;
				bool flag4 = H < 0f;
				if (flag4)
				{
					H += 1f;
				}
			}
			else
			{
				S = 0f;
				H = 0f;
			}
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x00031B58 File Offset: 0x0002FD58
		public static Color HSVToRGB(float H, float S, float V)
		{
			return Color.HSVToRGB(H, S, V, true);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00031B74 File Offset: 0x0002FD74
		public static Color HSVToRGB(float H, float S, float V, bool hdr)
		{
			Color white = Color.white;
			bool flag = S == 0f;
			if (flag)
			{
				white.r = V;
				white.g = V;
				white.b = V;
			}
			else
			{
				bool flag2 = V == 0f;
				if (flag2)
				{
					white.r = 0f;
					white.g = 0f;
					white.b = 0f;
				}
				else
				{
					white.r = 0f;
					white.g = 0f;
					white.b = 0f;
					float num = H * 6f;
					int num2 = (int)Mathf.Floor(num);
					float num3 = num - (float)num2;
					float num4 = V * (1f - S);
					float num5 = V * (1f - S * num3);
					float num6 = V * (1f - S * (1f - num3));
					switch (num2)
					{
					case -1:
						white.r = V;
						white.g = num4;
						white.b = num5;
						break;
					case 0:
						white.r = V;
						white.g = num6;
						white.b = num4;
						break;
					case 1:
						white.r = num5;
						white.g = V;
						white.b = num4;
						break;
					case 2:
						white.r = num4;
						white.g = V;
						white.b = num6;
						break;
					case 3:
						white.r = num4;
						white.g = num5;
						white.b = V;
						break;
					case 4:
						white.r = num6;
						white.g = num4;
						white.b = V;
						break;
					case 5:
						white.r = V;
						white.g = num4;
						white.b = num5;
						break;
					case 6:
						white.r = V;
						white.g = num6;
						white.b = num4;
						break;
					}
					bool flag3 = !hdr;
					if (flag3)
					{
						white.r = Mathf.Clamp(white.r, 0f, 1f);
						white.g = Mathf.Clamp(white.g, 0f, 1f);
						white.b = Mathf.Clamp(white.b, 0f, 1f);
					}
				}
			}
			return white;
		}

		// Token: 0x04000866 RID: 2150
		public float r;

		// Token: 0x04000867 RID: 2151
		public float g;

		// Token: 0x04000868 RID: 2152
		public float b;

		// Token: 0x04000869 RID: 2153
		public float a;
	}
}
