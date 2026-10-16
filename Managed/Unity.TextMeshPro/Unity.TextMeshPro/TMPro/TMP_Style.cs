using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public class TMP_Style
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00014188 File Offset: 0x00012388
		public static TMP_Style NormalStyle
		{
			get
			{
				if (TMP_Style.k_NormalStyle == null)
				{
					TMP_Style.k_NormalStyle = new TMP_Style("Normal", string.Empty, string.Empty);
				}
				return TMP_Style.k_NormalStyle;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000141AF File Offset: 0x000123AF
		// (set) Token: 0x06000360 RID: 864 RVA: 0x000141B7 File Offset: 0x000123B7
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (value != this.m_Name)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000141CE File Offset: 0x000123CE
		// (set) Token: 0x06000362 RID: 866 RVA: 0x000141D6 File Offset: 0x000123D6
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				if (value != this.m_HashCode)
				{
					this.m_HashCode = value;
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000141E8 File Offset: 0x000123E8
		public string styleOpeningDefinition
		{
			get
			{
				return this.m_OpeningDefinition;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000141F0 File Offset: 0x000123F0
		public string styleClosingDefinition
		{
			get
			{
				return this.m_ClosingDefinition;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000141F8 File Offset: 0x000123F8
		public uint[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00014200 File Offset: 0x00012400
		public uint[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00014208 File Offset: 0x00012408
		internal TMP_Style(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
		{
			this.m_Name = styleName;
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(styleName);
			this.m_OpeningDefinition = styleOpeningDefinition;
			this.m_ClosingDefinition = styleClosingDefinition;
			this.RefreshStyle();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00014238 File Offset: 0x00012438
		public void RefreshStyle()
		{
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(this.m_Name);
			int length = this.m_OpeningDefinition.Length;
			this.m_OpeningTagArray = new uint[length];
			for (int i = 0; i < length; i++)
			{
				this.m_OpeningTagArray[i] = (uint)this.m_OpeningDefinition[i];
			}
			int length2 = this.m_ClosingDefinition.Length;
			this.m_ClosingTagArray = new uint[length2];
			for (int j = 0; j < length2; j++)
			{
				this.m_ClosingTagArray[j] = (uint)this.m_ClosingDefinition[j];
			}
		}

		// Token: 0x04000311 RID: 785
		internal static TMP_Style k_NormalStyle;

		// Token: 0x04000312 RID: 786
		[SerializeField]
		private string m_Name;

		// Token: 0x04000313 RID: 787
		[SerializeField]
		private int m_HashCode;

		// Token: 0x04000314 RID: 788
		[SerializeField]
		private string m_OpeningDefinition;

		// Token: 0x04000315 RID: 789
		[SerializeField]
		private string m_ClosingDefinition;

		// Token: 0x04000316 RID: 790
		[SerializeField]
		private uint[] m_OpeningTagArray;

		// Token: 0x04000317 RID: 791
		[SerializeField]
		private uint[] m_ClosingTagArray;
	}
}
