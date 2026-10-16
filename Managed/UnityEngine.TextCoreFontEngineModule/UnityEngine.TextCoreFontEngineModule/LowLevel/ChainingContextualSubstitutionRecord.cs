using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200002E RID: 46
	[UsedByNativeCode]
	[Serializable]
	internal struct ChainingContextualSubstitutionRecord
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000638C File Offset: 0x0000458C
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000063A4 File Offset: 0x000045A4
		public GlyphIDSequence[] backtrackGlyphSequences
		{
			get
			{
				return this.m_BacktrackGlyphSequences;
			}
			set
			{
				this.m_BacktrackGlyphSequences = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000063B0 File Offset: 0x000045B0
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000063C8 File Offset: 0x000045C8
		public GlyphIDSequence[] inputGlyphSequences
		{
			get
			{
				return this.m_InputGlyphSequences;
			}
			set
			{
				this.m_InputGlyphSequences = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000063D4 File Offset: 0x000045D4
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000063EC File Offset: 0x000045EC
		public GlyphIDSequence[] lookaheadGlyphSequences
		{
			get
			{
				return this.m_LookaheadGlyphSequences;
			}
			set
			{
				this.m_LookaheadGlyphSequences = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000063F8 File Offset: 0x000045F8
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006410 File Offset: 0x00004610
		public SequenceLookupRecord[] sequenceLookupRecords
		{
			get
			{
				return this.m_SequenceLookupRecords;
			}
			set
			{
				this.m_SequenceLookupRecords = value;
			}
		}

		// Token: 0x040000F0 RID: 240
		[SerializeField]
		[NativeName("backtrackGlyphSequences")]
		private GlyphIDSequence[] m_BacktrackGlyphSequences;

		// Token: 0x040000F1 RID: 241
		[NativeName("inputGlyphSequences")]
		[SerializeField]
		private GlyphIDSequence[] m_InputGlyphSequences;

		// Token: 0x040000F2 RID: 242
		[SerializeField]
		[NativeName("lookaheadGlyphSequences")]
		private GlyphIDSequence[] m_LookaheadGlyphSequences;

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		[NativeName("sequenceLookupRecords")]
		private SequenceLookupRecord[] m_SequenceLookupRecords;
	}
}
