using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200020E RID: 526
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit)]
	public struct Color32 : IEquatable<Color32>, IFormattable
	{
		// Token: 0x06001BB5 RID: 7093 RVA: 0x00031DEE File Offset: 0x0002FFEE
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.rgba = 0;
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x00031E18 File Offset: 0x00030018
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)Mathf.Round(Mathf.Clamp01(c.r) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.g) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.b) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00031E8C File Offset: 0x0003008C
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00031ED8 File Offset: 0x000300D8
		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00031F5C File Offset: 0x0003015C
		public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
		{
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x1700048F RID: 1167
		public byte this[int index]
		{
			get
			{
				byte b;
				switch (index)
				{
				case 0:
					b = this.r;
					break;
				case 1:
					b = this.g;
					break;
				case 2:
					b = this.b;
					break;
				case 3:
					b = this.a;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
				}
				return b;
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
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
				}
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000320B0 File Offset: 0x000302B0
		public override int GetHashCode()
		{
			return this.rgba.GetHashCode();
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000320D0 File Offset: 0x000302D0
		public override bool Equals(object other)
		{
			Color32 color;
			bool flag;
			if (other is Color32)
			{
				color = (Color32)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(color);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00032104 File Offset: 0x00030304
		public bool Equals(Color32 other)
		{
			return this.rgba == other.rgba;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00032124 File Offset: 0x00030324
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00032140 File Offset: 0x00030340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0003215C File Offset: 0x0003035C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
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

		// Token: 0x0400086A RID: 2154
		[Ignore(DoesNotContributeToSize = true)]
		[FieldOffset(0)]
		private int rgba;

		// Token: 0x0400086B RID: 2155
		[FieldOffset(0)]
		public byte r;

		// Token: 0x0400086C RID: 2156
		[FieldOffset(1)]
		public byte g;

		// Token: 0x0400086D RID: 2157
		[FieldOffset(2)]
		public byte b;

		// Token: 0x0400086E RID: 2158
		[FieldOffset(3)]
		public byte a;
	}
}
