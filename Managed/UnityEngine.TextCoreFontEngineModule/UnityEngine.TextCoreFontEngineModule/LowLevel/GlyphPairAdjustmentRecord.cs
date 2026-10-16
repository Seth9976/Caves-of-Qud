using System;
using System.Diagnostics;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000022 RID: 34
	[DebuggerDisplay("First glyphIndex = {m_FirstAdjustmentRecord.m_GlyphIndex},  Second glyphIndex = {m_SecondAdjustmentRecord.m_GlyphIndex}")]
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphPairAdjustmentRecord : IEquatable<GlyphPairAdjustmentRecord>
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005D6C File Offset: 0x00003F6C
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005D84 File Offset: 0x00003F84
		public GlyphAdjustmentRecord firstAdjustmentRecord
		{
			get
			{
				return this.m_FirstAdjustmentRecord;
			}
			set
			{
				this.m_FirstAdjustmentRecord = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005D90 File Offset: 0x00003F90
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public GlyphAdjustmentRecord secondAdjustmentRecord
		{
			get
			{
				return this.m_SecondAdjustmentRecord;
			}
			set
			{
				this.m_SecondAdjustmentRecord = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005DB4 File Offset: 0x00003FB4
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00005DCC File Offset: 0x00003FCC
		public FontFeatureLookupFlags featureLookupFlags
		{
			get
			{
				return this.m_FeatureLookupFlags;
			}
			set
			{
				this.m_FeatureLookupFlags = value;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005DD6 File Offset: 0x00003FD6
		public GlyphPairAdjustmentRecord(GlyphAdjustmentRecord firstAdjustmentRecord, GlyphAdjustmentRecord secondAdjustmentRecord)
		{
			this.m_FirstAdjustmentRecord = firstAdjustmentRecord;
			this.m_SecondAdjustmentRecord = secondAdjustmentRecord;
			this.m_FeatureLookupFlags = FontFeatureLookupFlags.None;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005DF0 File Offset: 0x00003FF0
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005E14 File Offset: 0x00004014
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005E38 File Offset: 0x00004038
		[ExcludeFromDocs]
		public bool Equals(GlyphPairAdjustmentRecord other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005E60 File Offset: 0x00004060
		[ExcludeFromDocs]
		public static bool operator ==(GlyphPairAdjustmentRecord lhs, GlyphPairAdjustmentRecord rhs)
		{
			return lhs.m_FirstAdjustmentRecord == rhs.m_FirstAdjustmentRecord && lhs.m_SecondAdjustmentRecord == rhs.m_SecondAdjustmentRecord;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005E9C File Offset: 0x0000409C
		[ExcludeFromDocs]
		public static bool operator !=(GlyphPairAdjustmentRecord lhs, GlyphPairAdjustmentRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		[NativeName("firstAdjustmentRecord")]
		private GlyphAdjustmentRecord m_FirstAdjustmentRecord;

		// Token: 0x040000D5 RID: 213
		[SerializeField]
		[NativeName("secondAdjustmentRecord")]
		private GlyphAdjustmentRecord m_SecondAdjustmentRecord;

		// Token: 0x040000D6 RID: 214
		[SerializeField]
		private FontFeatureLookupFlags m_FeatureLookupFlags;
	}
}
