using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public struct LigatureSubstitutionRecord
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000248F File Offset: 0x0000068F
		public uint[] componentGlyphIDs
		{
			get
			{
				return this.m_ComponentGlyphIDs;
			}
			set
			{
				this.m_ComponentGlyphIDs = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002498 File Offset: 0x00000698
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000024A0 File Offset: 0x000006A0
		public uint ligatureGlyphID
		{
			get
			{
				return this.m_LigatureGlyphID;
			}
			set
			{
				this.m_LigatureGlyphID = value;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024AC File Offset: 0x000006AC
		public static bool operator ==(LigatureSubstitutionRecord lhs, LigatureSubstitutionRecord rhs)
		{
			if (lhs.ligatureGlyphID != rhs.m_LigatureGlyphID)
			{
				return false;
			}
			int num = lhs.m_ComponentGlyphIDs.Length;
			if (num != rhs.m_ComponentGlyphIDs.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (lhs.m_ComponentGlyphIDs[i] != rhs.m_ComponentGlyphIDs[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002501 File Offset: 0x00000701
		public static bool operator !=(LigatureSubstitutionRecord lhs, LigatureSubstitutionRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0400001C RID: 28
		[SerializeField]
		private uint[] m_ComponentGlyphIDs;

		// Token: 0x0400001D RID: 29
		[SerializeField]
		private uint m_LigatureGlyphID;
	}
}
