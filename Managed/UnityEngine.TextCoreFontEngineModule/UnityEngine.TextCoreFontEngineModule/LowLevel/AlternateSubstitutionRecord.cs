using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000029 RID: 41
	[UsedByNativeCode]
	[Serializable]
	internal struct AlternateSubstitutionRecord
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006108 File Offset: 0x00004308
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00006120 File Offset: 0x00004320
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000612C File Offset: 0x0000432C
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00006144 File Offset: 0x00004344
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

		// Token: 0x040000E7 RID: 231
		[NativeName("targetGlyphID")]
		[SerializeField]
		private uint m_TargetGlyphID;

		// Token: 0x040000E8 RID: 232
		[SerializeField]
		[NativeName("substituteGlyphIDs")]
		private uint[] m_SubstituteGlyphIDs;
	}
}
