using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000020 RID: 32
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphValueRecord : IEquatable<GlyphValueRecord>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005A08 File Offset: 0x00003C08
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005A20 File Offset: 0x00003C20
		public float xPlacement
		{
			get
			{
				return this.m_XPlacement;
			}
			set
			{
				this.m_XPlacement = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005A2C File Offset: 0x00003C2C
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005A44 File Offset: 0x00003C44
		public float yPlacement
		{
			get
			{
				return this.m_YPlacement;
			}
			set
			{
				this.m_YPlacement = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005A50 File Offset: 0x00003C50
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005A68 File Offset: 0x00003C68
		public float xAdvance
		{
			get
			{
				return this.m_XAdvance;
			}
			set
			{
				this.m_XAdvance = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005A74 File Offset: 0x00003C74
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005A8C File Offset: 0x00003C8C
		public float yAdvance
		{
			get
			{
				return this.m_YAdvance;
			}
			set
			{
				this.m_YAdvance = value;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005A96 File Offset: 0x00003C96
		public GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public static GlyphValueRecord operator +(GlyphValueRecord a, GlyphValueRecord b)
		{
			GlyphValueRecord glyphValueRecord;
			glyphValueRecord.m_XPlacement = a.xPlacement + b.xPlacement;
			glyphValueRecord.m_YPlacement = a.yPlacement + b.yPlacement;
			glyphValueRecord.m_XAdvance = a.xAdvance + b.xAdvance;
			glyphValueRecord.m_YAdvance = a.yAdvance + b.yAdvance;
			return glyphValueRecord;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005B24 File Offset: 0x00003D24
		[ExcludeFromDocs]
		public static GlyphValueRecord operator *(GlyphValueRecord a, float emScale)
		{
			a.m_XPlacement = a.xPlacement * emScale;
			a.m_YPlacement = a.yPlacement * emScale;
			a.m_XAdvance = a.xAdvance * emScale;
			a.m_YAdvance = a.yAdvance * emScale;
			return a;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005B78 File Offset: 0x00003D78
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005B9C File Offset: 0x00003D9C
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public bool Equals(GlyphValueRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public static bool operator ==(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return lhs.m_XPlacement == rhs.m_XPlacement && lhs.m_YPlacement == rhs.m_YPlacement && lhs.m_XAdvance == rhs.m_XAdvance && lhs.m_YAdvance == rhs.m_YAdvance;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005C38 File Offset: 0x00003E38
		public static bool operator !=(GlyphValueRecord lhs, GlyphValueRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000CE RID: 206
		[SerializeField]
		[NativeName("xPlacement")]
		private float m_XPlacement;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		[NativeName("yPlacement")]
		private float m_YPlacement;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		[NativeName("xAdvance")]
		private float m_XAdvance;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		[NativeName("yAdvance")]
		private float m_YAdvance;
	}
}
