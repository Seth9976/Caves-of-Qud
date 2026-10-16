using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000028 RID: 40
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[UsedByNativeCode]
	[Serializable]
	internal struct MultipleSubstitutionRecord
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000060C0 File Offset: 0x000042C0
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000060D8 File Offset: 0x000042D8
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000060E4 File Offset: 0x000042E4
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000060FC File Offset: 0x000042FC
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

		// Token: 0x040000E5 RID: 229
		[NativeName("targetGlyphID")]
		[SerializeField]
		private uint m_TargetGlyphID;

		// Token: 0x040000E6 RID: 230
		[NativeName("substituteGlyphIDs")]
		[SerializeField]
		private uint[] m_SubstituteGlyphIDs;
	}
}
