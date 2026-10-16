using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200002C RID: 44
	[UsedByNativeCode]
	[Serializable]
	internal struct SequenceLookupRecord
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000062FC File Offset: 0x000044FC
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00006314 File Offset: 0x00004514
		public uint glyphSequenceIndex
		{
			get
			{
				return this.m_GlyphSequenceIndex;
			}
			set
			{
				this.m_GlyphSequenceIndex = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006320 File Offset: 0x00004520
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006338 File Offset: 0x00004538
		public uint lookupListIndex
		{
			get
			{
				return this.m_LookupListIndex;
			}
			set
			{
				this.m_LookupListIndex = value;
			}
		}

		// Token: 0x040000EC RID: 236
		[SerializeField]
		[NativeName("glyphSequenceIndex")]
		private uint m_GlyphSequenceIndex;

		// Token: 0x040000ED RID: 237
		[NativeName("lookupListIndex")]
		[SerializeField]
		private uint m_LookupListIndex;
	}
}
