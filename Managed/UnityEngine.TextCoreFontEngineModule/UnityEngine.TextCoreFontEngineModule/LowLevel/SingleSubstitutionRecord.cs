using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000027 RID: 39
	[UsedByNativeCode]
	[Serializable]
	internal struct SingleSubstitutionRecord
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006078 File Offset: 0x00004278
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00006090 File Offset: 0x00004290
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000609C File Offset: 0x0000429C
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000060B4 File Offset: 0x000042B4
		public uint substituteGlyphID
		{
			get
			{
				return this.m_SubstituteGlyphID;
			}
			set
			{
				this.m_SubstituteGlyphID = value;
			}
		}

		// Token: 0x040000E3 RID: 227
		[NativeName("targetGlyphID")]
		[SerializeField]
		private uint m_TargetGlyphID;

		// Token: 0x040000E4 RID: 228
		[NativeName("substituteGlyphID")]
		[SerializeField]
		private uint m_SubstituteGlyphID;
	}
}
