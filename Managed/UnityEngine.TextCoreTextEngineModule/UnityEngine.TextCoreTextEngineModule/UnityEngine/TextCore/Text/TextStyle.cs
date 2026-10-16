using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class TextStyle
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000BB8C File Offset: 0x00009D8C
		public static TextStyle NormalStyle
		{
			get
			{
				bool flag = TextStyle.k_NormalStyle == null;
				if (flag)
				{
					TextStyle.k_NormalStyle = new TextStyle("Normal", string.Empty, string.Empty);
				}
				return TextStyle.k_NormalStyle;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value != this.m_Name;
				if (flag)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000BC08 File Offset: 0x00009E08
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000BC20 File Offset: 0x00009E20
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				bool flag = value != this.m_HashCode;
				if (flag)
				{
					this.m_HashCode = value;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000BC48 File Offset: 0x00009E48
		public string styleOpeningDefinition
		{
			get
			{
				return this.m_OpeningDefinition;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000BC60 File Offset: 0x00009E60
		public string styleClosingDefinition
		{
			get
			{
				return this.m_ClosingDefinition;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000BC78 File Offset: 0x00009E78
		public uint[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000BC90 File Offset: 0x00009E90
		public uint[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		internal TextStyle(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
		{
			this.m_Name = styleName;
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(styleName);
			this.m_OpeningDefinition = styleOpeningDefinition;
			this.m_ClosingDefinition = styleClosingDefinition;
			this.RefreshStyle();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000BCDC File Offset: 0x00009EDC
		public void RefreshStyle()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Name);
			int length = this.m_OpeningDefinition.Length;
			this.m_OpeningTagArray = new uint[length];
			this.m_OpeningTagUnicodeArray = new uint[length];
			for (int i = 0; i < length; i++)
			{
				this.m_OpeningTagArray[i] = (uint)this.m_OpeningDefinition[i];
				this.m_OpeningTagUnicodeArray[i] = (uint)this.m_OpeningDefinition[i];
			}
			int length2 = this.m_ClosingDefinition.Length;
			this.m_ClosingTagArray = new uint[length2];
			this.m_ClosingTagUnicodeArray = new uint[length2];
			for (int j = 0; j < length2; j++)
			{
				this.m_ClosingTagArray[j] = (uint)this.m_ClosingDefinition[j];
				this.m_ClosingTagUnicodeArray[j] = (uint)this.m_ClosingDefinition[j];
			}
		}

		// Token: 0x0400017C RID: 380
		internal static TextStyle k_NormalStyle;

		// Token: 0x0400017D RID: 381
		[SerializeField]
		private string m_Name;

		// Token: 0x0400017E RID: 382
		[SerializeField]
		private int m_HashCode;

		// Token: 0x0400017F RID: 383
		[SerializeField]
		private string m_OpeningDefinition;

		// Token: 0x04000180 RID: 384
		[SerializeField]
		private string m_ClosingDefinition;

		// Token: 0x04000181 RID: 385
		[SerializeField]
		private uint[] m_OpeningTagArray;

		// Token: 0x04000182 RID: 386
		[SerializeField]
		private uint[] m_ClosingTagArray;

		// Token: 0x04000183 RID: 387
		[SerializeField]
		internal uint[] m_OpeningTagUnicodeArray;

		// Token: 0x04000184 RID: 388
		[SerializeField]
		internal uint[] m_ClosingTagUnicodeArray;
	}
}
