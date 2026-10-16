using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200006A RID: 106
	internal struct MarkupAttribute
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000253D4 File Offset: 0x000235D4
		// (set) Token: 0x0600033E RID: 830 RVA: 0x000253EC File Offset: 0x000235EC
		public int NameHashCode
		{
			get
			{
				return this.m_NameHashCode;
			}
			set
			{
				this.m_NameHashCode = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000253F8 File Offset: 0x000235F8
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00025410 File Offset: 0x00023610
		public int ValueHashCode
		{
			get
			{
				return this.m_ValueHashCode;
			}
			set
			{
				this.m_ValueHashCode = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0002541C File Offset: 0x0002361C
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00025434 File Offset: 0x00023634
		public int ValueStartIndex
		{
			get
			{
				return this.m_ValueStartIndex;
			}
			set
			{
				this.m_ValueStartIndex = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00025440 File Offset: 0x00023640
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00025458 File Offset: 0x00023658
		public int ValueLength
		{
			get
			{
				return this.m_ValueLength;
			}
			set
			{
				this.m_ValueLength = value;
			}
		}

		// Token: 0x04000453 RID: 1107
		private int m_NameHashCode;

		// Token: 0x04000454 RID: 1108
		private int m_ValueHashCode;

		// Token: 0x04000455 RID: 1109
		private int m_ValueStartIndex;

		// Token: 0x04000456 RID: 1110
		private int m_ValueLength;
	}
}
