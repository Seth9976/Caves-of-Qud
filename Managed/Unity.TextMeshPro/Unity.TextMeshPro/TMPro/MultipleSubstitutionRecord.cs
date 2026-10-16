using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public struct MultipleSubstitutionRecord
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002465 File Offset: 0x00000665
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000246D File Offset: 0x0000066D
		public uint targetGlyphID
		{
			get
			{
				return this.m_TargetGlyphID;
			}
			set
			{
				this.m_TargetGlyphID = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002476 File Offset: 0x00000676
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000247E File Offset: 0x0000067E
		public uint[] substituteGlyphIDs
		{
			get
			{
				return this.m_SubstituteGlyphIDs;
			}
			set
			{
				this.m_SubstituteGlyphIDs = value;
			}
		}

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private uint m_TargetGlyphID;

		// Token: 0x0400001B RID: 27
		[SerializeField]
		private uint[] m_SubstituteGlyphIDs;
	}
}
