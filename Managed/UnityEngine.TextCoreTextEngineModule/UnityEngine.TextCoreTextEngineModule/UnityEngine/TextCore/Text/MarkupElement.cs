using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200006B RID: 107
	internal struct MarkupElement
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00025464 File Offset: 0x00023664
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00025494 File Offset: 0x00023694
		public int NameHashCode
		{
			get
			{
				return (this.m_Attributes == null) ? 0 : this.m_Attributes[0].NameHashCode;
			}
			set
			{
				bool flag = this.m_Attributes == null;
				if (flag)
				{
					this.m_Attributes = new MarkupAttribute[8];
				}
				this.m_Attributes[0].NameHashCode = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000254D0 File Offset: 0x000236D0
		// (set) Token: 0x06000348 RID: 840 RVA: 0x000254FE File Offset: 0x000236FE
		public int ValueHashCode
		{
			get
			{
				return (this.m_Attributes == null) ? 0 : this.m_Attributes[0].ValueHashCode;
			}
			set
			{
				this.m_Attributes[0].ValueHashCode = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00025514 File Offset: 0x00023714
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00025542 File Offset: 0x00023742
		public int ValueStartIndex
		{
			get
			{
				return (this.m_Attributes == null) ? 0 : this.m_Attributes[0].ValueStartIndex;
			}
			set
			{
				this.m_Attributes[0].ValueStartIndex = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00025558 File Offset: 0x00023758
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00025586 File Offset: 0x00023786
		public int ValueLength
		{
			get
			{
				return (this.m_Attributes == null) ? 0 : this.m_Attributes[0].ValueLength;
			}
			set
			{
				this.m_Attributes[0].ValueLength = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0002559C File Offset: 0x0002379C
		// (set) Token: 0x0600034E RID: 846 RVA: 0x000255B4 File Offset: 0x000237B4
		public MarkupAttribute[] Attributes
		{
			get
			{
				return this.m_Attributes;
			}
			set
			{
				this.m_Attributes = value;
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000255C0 File Offset: 0x000237C0
		public MarkupElement(int nameHashCode, int startIndex, int length)
		{
			this.m_Attributes = new MarkupAttribute[8];
			this.m_Attributes[0].NameHashCode = nameHashCode;
			this.m_Attributes[0].ValueStartIndex = startIndex;
			this.m_Attributes[0].ValueLength = length;
		}

		// Token: 0x04000457 RID: 1111
		private MarkupAttribute[] m_Attributes;
	}
}
