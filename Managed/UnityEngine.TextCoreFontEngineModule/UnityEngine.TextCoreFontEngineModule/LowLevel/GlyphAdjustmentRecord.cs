using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000021 RID: 33
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphAdjustmentRecord : IEquatable<GlyphAdjustmentRecord>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005C54 File Offset: 0x00003E54
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005C6C File Offset: 0x00003E6C
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005C78 File Offset: 0x00003E78
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00005C90 File Offset: 0x00003E90
		public GlyphValueRecord glyphValueRecord
		{
			get
			{
				return this.m_GlyphValueRecord;
			}
			set
			{
				this.m_GlyphValueRecord = value;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005C9A File Offset: 0x00003E9A
		public GlyphAdjustmentRecord(uint glyphIndex, GlyphValueRecord glyphValueRecord)
		{
			this.m_GlyphIndex = glyphIndex;
			this.m_GlyphValueRecord = glyphValueRecord;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005CAC File Offset: 0x00003EAC
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005CD0 File Offset: 0x00003ED0
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005CF4 File Offset: 0x00003EF4
		[ExcludeFromDocs]
		public bool Equals(GlyphAdjustmentRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005D1C File Offset: 0x00003F1C
		[ExcludeFromDocs]
		public static bool operator ==(GlyphAdjustmentRecord lhs, GlyphAdjustmentRecord rhs)
		{
			return lhs.m_GlyphIndex == rhs.m_GlyphIndex && lhs.m_GlyphValueRecord == rhs.m_GlyphValueRecord;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005D50 File Offset: 0x00003F50
		[ExcludeFromDocs]
		public static bool operator !=(GlyphAdjustmentRecord lhs, GlyphAdjustmentRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000D2 RID: 210
		[SerializeField]
		[NativeName("glyphIndex")]
		private uint m_GlyphIndex;

		// Token: 0x040000D3 RID: 211
		[SerializeField]
		[NativeName("glyphValueRecord")]
		private GlyphValueRecord m_GlyphValueRecord;
	}
}
