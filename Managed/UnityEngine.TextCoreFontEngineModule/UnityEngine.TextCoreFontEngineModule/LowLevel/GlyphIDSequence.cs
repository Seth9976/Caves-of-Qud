using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200002B RID: 43
	[UsedByNativeCode]
	[Serializable]
	internal struct GlyphIDSequence
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000062D8 File Offset: 0x000044D8
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000062F0 File Offset: 0x000044F0
		public uint[] glyphIDs
		{
			get
			{
				return this.m_GlyphIDs;
			}
			set
			{
				this.m_GlyphIDs = value;
			}
		}

		// Token: 0x040000EB RID: 235
		[SerializeField]
		[NativeName("glyphIDs")]
		private uint[] m_GlyphIDs;
	}
}
