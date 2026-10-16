using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200002D RID: 45
	[UsedByNativeCode]
	[Serializable]
	internal struct ContextualSubstitutionRecord
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006344 File Offset: 0x00004544
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000635C File Offset: 0x0000455C
		public GlyphIDSequence[] inputSequences
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006368 File Offset: 0x00004568
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006380 File Offset: 0x00004580
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

		// Token: 0x040000EE RID: 238
		[SerializeField]
		[NativeName("inputGlyphSequences")]
		private GlyphIDSequence[] m_InputGlyphSequences;

		// Token: 0x040000EF RID: 239
		[SerializeField]
		[NativeName("sequenceLookupRecords")]
		private SequenceLookupRecord[] m_SequenceLookupRecords;
	}
}
