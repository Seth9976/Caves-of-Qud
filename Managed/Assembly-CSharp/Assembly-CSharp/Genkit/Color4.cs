using System;
using System.Collections.Generic;
using UnityEngine;

namespace Genkit
{
	// Token: 0x02000374 RID: 884
	[Serializable]
	public struct Color4 : IEquatable<Color4>, IEqualityComparer<Color4>
	{
		// Token: 0x060020F5 RID: 8437 RVA: 0x000A31D8 File Offset: 0x000A13D8
		public Color4(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000A31F7 File Offset: 0x000A13F7
		public Color4(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x000A3219 File Offset: 0x000A1419
		public Color toUnityColor
		{
			get
			{
				return new Color(this.r, this.g, this.b, this.a);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x000A3238 File Offset: 0x000A1438
		public static Color4 black
		{
			get
			{
				return new Color4(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000A3253 File Offset: 0x000A1453
		public static Color4 white
		{
			get
			{
				return new Color4(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x000A326E File Offset: 0x000A146E
		public static Color4 red
		{
			get
			{
				return new Color4(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000A3289 File Offset: 0x000A1489
		public static Color4 magenta
		{
			get
			{
				return new Color4(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000A32A4 File Offset: 0x000A14A4
		public static Color4 green
		{
			get
			{
				return new Color4(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000A32BF File Offset: 0x000A14BF
		public override bool Equals(object obj)
		{
			return obj.GetType() == typeof(Color4) && this.Equals((Color4)obj);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000A32E6 File Offset: 0x000A14E6
		public bool Equals(Color4 other)
		{
			return this.r == other.r && this.g == other.g && this.b == other.b && this.a == other.a;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000A3322 File Offset: 0x000A1522
		public bool Equals(Color4 x, Color4 y)
		{
			return x.Equals(y);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000A332C File Offset: 0x000A152C
		public override int GetHashCode()
		{
			return this.GetHashCode(this);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000A333A File Offset: 0x000A153A
		public int GetHashCode(Color4 obj)
		{
			return this.r.GetHashCode() ^ (this.g.GetHashCode() << 2) ^ (this.b.GetHashCode() >> 2) ^ (this.a.GetHashCode() << 4);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000A3371 File Offset: 0x000A1571
		public static bool operator ==(Color4 x, Color4 y)
		{
			return x.Equals(y);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000A337B File Offset: 0x000A157B
		public static bool operator !=(Color4 x, Color4 y)
		{
			return !x.Equals(y);
		}

		// Token: 0x040010B0 RID: 4272
		public float r;

		// Token: 0x040010B1 RID: 4273
		public float g;

		// Token: 0x040010B2 RID: 4274
		public float b;

		// Token: 0x040010B3 RID: 4275
		public float a;
	}
}
