using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200002A RID: 42
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule", "UnityEditor.TextCoreTextEngineModule" })]
	[UsedByNativeCode]
	[Serializable]
	internal struct LigatureSubstitutionRecord : IEquatable<LigatureSubstitutionRecord>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006150 File Offset: 0x00004350
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00006168 File Offset: 0x00004368
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00006174 File Offset: 0x00004374
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000618C File Offset: 0x0000438C
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

		// Token: 0x060001A1 RID: 417 RVA: 0x00006198 File Offset: 0x00004398
		public bool Equals(LigatureSubstitutionRecord other)
		{
			return this == other;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000061B8 File Offset: 0x000043B8
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is LigatureSubstitutionRecord)
			{
				LigatureSubstitutionRecord ligatureSubstitutionRecord = (LigatureSubstitutionRecord)obj;
				flag = this.Equals(ligatureSubstitutionRecord);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000061E4 File Offset: 0x000043E4
		public override int GetHashCode()
		{
			return this.m_ComponentGlyphIDs.GetHashCode();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006204 File Offset: 0x00004404
		public static bool operator ==(LigatureSubstitutionRecord lhs, LigatureSubstitutionRecord rhs)
		{
			bool flag = lhs.componentGlyphIDs != null && rhs.componentGlyphIDs != null;
			if (flag)
			{
				int num = lhs.m_ComponentGlyphIDs.Length;
				bool flag2 = num != rhs.m_ComponentGlyphIDs.Length;
				if (flag2)
				{
					return false;
				}
				for (int i = 0; i < num; i++)
				{
					bool flag3 = lhs.m_ComponentGlyphIDs[i] != rhs.m_ComponentGlyphIDs[i];
					if (flag3)
					{
						return false;
					}
				}
			}
			else
			{
				bool flag4 = lhs.componentGlyphIDs != null || rhs.componentGlyphIDs != null;
				if (flag4)
				{
					return false;
				}
			}
			return lhs.ligatureGlyphID == rhs.m_LigatureGlyphID;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000062BC File Offset: 0x000044BC
		public static bool operator !=(LigatureSubstitutionRecord lhs, LigatureSubstitutionRecord rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000E9 RID: 233
		[NativeName("componentGlyphs")]
		[SerializeField]
		private uint[] m_ComponentGlyphIDs;

		// Token: 0x040000EA RID: 234
		[SerializeField]
		[NativeName("ligatureGlyph")]
		private uint m_LigatureGlyphID;
	}
}
